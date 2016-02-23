using System;
using System.Collections.Generic;
using GreenZoneFxEngine.Types;
using System.Collections;
using GreenZoneFxEngine.Util;
using System.Reflection;
using System.ComponentModel;
using GreenZoneUtil.Util;
using System.Text;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{

    public abstract class SeriesManagerRuntimeEx : SeriesManagerRuntimeBase
    {


        //symbol symbol;
        //TimePeriodConst period;
        //public event PropertyChangedEventHandler DefaultChanged;

        public SeriesManagerRuntimeEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public SeriesManagerRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected SeriesManagerRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override INormalSeriesManagerCache this[symbol sym, TimePeriodConst period]
        {
            get
            {
                INormalSeriesManagerCache r;
                SymbolPeriodId sp = new SymbolPeriodId(sym, period);
                if (!SeriesCaches.TryGetValue(sp, out r))
                {
                    r = CreateSeriesManagerCache(sp);
                    SeriesCaches[sp] = r;
                }
                return r;
            }
        }

        public override IIndicatorRuntime this[symbol sym, TimePeriodConst period, Mt4ExecutableInfo ind, params object[] args]
        {
            get
            {
                string key = GreenZoneSysUtilsBase.GenerateArgKey(args);

                IIndicatorRuntime r = this[sym, period, ind, key, args];
                return r;
            }
        }

        IIndicatorRuntime this[symbol sym, TimePeriodConst period, Mt4ExecutableInfo ind, string argKey, object[] args]
        {
            get
            {
                IIndicatorRuntime r = this[sym, period][ind, argKey, args];

                return r;
            }
        }

        public override datetime FocusedTime
        {
            get
            {
                if (DefaultCache == null)
                {
                    foreach (INormalSeriesManagerCache sc in SeriesCaches.Values)
                    {
                        DefaultCache = sc;
                        break;
                    }
                }
                return DefaultCache.FocusedTime;
            }
            set
            {
                foreach (INormalSeriesManagerCache sc in SeriesCaches.Values)
                {
                    sc.Load(value);
                }
            }
        }

    }

    public abstract class SeriesManagerCacheEx : SeriesManagerCacheBase
    {
        public SeriesManagerCacheEx(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
            : base(rmiManager)
        {
            Parent = parent;
            Symbol = symbolPeriod.symbol;
            Period = symbolPeriod.period;

            SymbolRuntime = Environment.SymbolContexts[Symbol].Runtime;

            Bid = SymbolContext.GetValue(MODE_BID);
            Ask = SymbolContext.GetValue(MODE_ASK);
            Point = SymbolContext.GetValue(MODE_POINT);
            Digits = (int)SymbolContext.GetValue(MODE_DIGITS);
        }

        public SeriesManagerCacheEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

		public SeriesManagerCacheEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

        protected SeriesManagerCacheEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}


        public override ISeriesArr GetArray(SeriesArrayPool type)
        {
            switch (type)
            {
                case SeriesArrayPool.MODE_CLOSE: return Close;
                case SeriesArrayPool.MODE_HIGH: return High;
                case SeriesArrayPool.MODE_LOW: return Low;
                case SeriesArrayPool.MODE_OPEN: return Open;
                case SeriesArrayPool.MODE_TIME: return Time;
                case SeriesArrayPool.MODE_VOLUME: return Volume;
                default: throw new NotSupportedException();
            }
        }

        public override IIndicatorRuntime Get(Mt4ExecutableInfo ind, object[] args, bool autoCreate = false)
        {
            string argKey = GreenZoneSysUtilsBase.GenerateArgKey(args);
            IIndicatorRuntime result = Get(ind, argKey, args, autoCreate);
            return result;
        }

        public override IIndicatorRuntime Get(Mt4ExecutableInfo ind, string argKey, object[] args, bool autoCreate = false)
        {
            IIndicatorRuntime r;
            IndicatorId sp = new IndicatorId(ind, argKey);
            if (!Indicators.TryGetValue(sp, out r))
            {
                if (autoCreate)
                {
                    r = AddIndicator(ind, argKey, args);
                }
            }
            else if (r.UpdateState == IndicatorUpdateState.PENDING_UPDATE)
            {
                updateIndicatorsServer(r);
            }

            return r;
        }

        public override IIndicatorRuntime AddIndicator(Mt4ExecutableInfo ind, object[] args)
        {
            string argKey = GreenZoneSysUtilsBase.GenerateArgKey(args);
            IIndicatorRuntime result = AddIndicator(ind, argKey, args);
            return result;
        }

        IIndicatorRuntime AddIndicator(Mt4ExecutableInfo ind, string argKey, object[] args)
        {
            IndicatorId sp = new IndicatorId(ind, argKey);
            IIndicatorRuntime r = (IIndicatorRuntime)UserRuntimeEx.Create(rmiManager, Parent.Parent, ind, this);

            List<ReflProperty> pis = GreenZoneUtilsBase.GetTopLevelProperties(rmiManager.Resolver, r);
            if (pis.Count != args.Length)
            {
                throw new ArgumentException("pis.Length!=args.Length  " + pis.Count + "!=" + args.Length);
            }
            for (int i = 0; i < pis.Count; i++)
            {
                pis[i].SetValue(r, args[i]);
            }
            //if (add)
            //{
            replace_ind(sp, sp, r);
            //}

            updateIndicatorsServer(r);
            return r;
        }

        public override IndicatorId AddIndicator(IIndicatorRuntime indicator, bool forceUpdate = true)
        {
            if (forceUpdate)
            {
                indicator.LastFileOffset = -1;
                indicator.LastBufferLength = -1;
            }
            object[] args = indicator.GenerateParamArray();
            string argKey = GreenZoneSysUtilsBase.GenerateArgKey(args);
            IndicatorId sp = new IndicatorId(indicator.IndicatorInfo, argKey);
            replace_ind(sp, sp, indicator);
            updateIndicatorsServer(indicator);
            return sp;
        }

        public override IndicatorId ReplaceIndicator(IndicatorId sp0, IIndicatorRuntime indicator, bool forceUpdate = true)
        {
            if (forceUpdate)
            {
                indicator.LastFileOffset = -1;
                indicator.LastBufferLength = -1;
            }
            object[] args = indicator.GenerateParamArray();
            string argKey = GreenZoneSysUtilsBase.GenerateArgKey(args);
            IndicatorId sp = new IndicatorId(indicator.IndicatorInfo, argKey);
            replace_ind(sp0, sp, indicator);
            updateIndicatorsServer(indicator);
            return sp;
        }

        void replace_ind(IndicatorId sp0, IndicatorId sp, IIndicatorRuntime indicator)
        {
            IIndicatorRuntime old;
            if (Indicators.TryGetValue(sp0, out old))
            {
                Indicators.Remove(sp0);
                old.RaiseInstanceChanged(indicator);
            }
            Indicators[sp] = indicator;
        }


        public override IIndicatorRuntime RemoveIndicator(Mt4ExecutableInfo ind, object[] args)
        {
            string argKey = GreenZoneSysUtilsBase.GenerateArgKey(args);
            IndicatorId sp = new IndicatorId(ind, argKey);
            IIndicatorRuntime r = Indicators[sp];
            Indicators.Remove(sp);
            return r;
        }

        IIndicatorRuntime RemoveIndicator(Mt4ExecutableInfo ind, string argKey)
        {
            IndicatorId sp = new IndicatorId(ind, argKey);
            IIndicatorRuntime r = Indicators[sp];
            Indicators.Remove(sp);
            return r;
        }

        public override IIndicatorRuntime RemoveIndicator(IndicatorId sp)
        {
            IIndicatorRuntime r = Indicators[sp];
            Indicators.Remove(sp);
            return r;
        }

        public override double GetIndicatorValue<T>(IndicatorLine _mode, int shift, params object[] args)
        {
            double result = GetIndicatorValue(typeof(T).FullName, _mode, shift, args);

            return result;
        }

        public override double GetIndicatorValue<T>(int mode, int shift, params object[] args)
        {
            double result = GetIndicatorValue(typeof(T).FullName, mode, shift, args);

            return result;
        }


        public override double GetIndicatorValue(string name, IndicatorLine _mode, int shift, params object[] args)
        {
            int mode = (int)_mode;
            double result = GetIndicatorValue(name, mode, shift, args);

            return result;
        }

        public override double GetIndicatorValue(string name, int mode, int shift, params object[] args)
        {
            Mt4ExecutableInfo info = Environment.GetIndicatorInfo(name);
            string key = GreenZoneSysUtilsBase.GenerateArgKey(args);
            IIndicatorRuntime ind = Get(info, key, args, true);
            IndicatorBuffer b = ind.Buffers[mode];
            double result = b.Buffer[shift];

            return result;
        }

        protected abstract void updateIndicatorsServer(IIndicatorRuntime indicator);
    }

    public abstract class NormalSeriesManagerCacheEx : NormalSeriesManagerCacheBase
    {

        public NormalSeriesManagerCacheEx(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
            : base(rmiManager, parent, symbolPeriod)
        {
        }

        public NormalSeriesManagerCacheEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected NormalSeriesManagerCacheEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override void Load(datetime focusedTime)
        {
            try
            {
                SeriesRuntime.Load(focusedTime);

                updateBuffers();
            }
            catch (TimeSeriesEOFException)
            {
            }
        }

        public override void LoadForward(int offset)
        {
            try
            {
                SeriesRuntime.LoadForward(offset);

                updateBuffers();
            }
            catch (TimeSeriesEOFException)
            {
            }
        }

        public override void LoadAtTotal(long total_ind)
        {
            try
            {
                SeriesRuntime.LoadAtTotal(total_ind);

                updateBuffers();
            }
            catch (TimeSeriesEOFException)
            {
            }
        }


        public override void fixSeriesRange(ref SeriesRange seriesRange)
        {
            int seriesRangeFrom = seriesRange.OffsetFrom;
            int seriesRangeTo = seriesRange.OffsetTo;
            int offset = IndOffset;
            int _offset = -offset;

            int length = IndCount - offset;

            if (seriesRangeFrom < _offset)
            {
                seriesRange.ChangeOffsetFrom(_offset);
            }
            if (seriesRangeTo - seriesRangeFrom + 1 <= IndCount)
            {
                if (seriesRangeTo >= length)
                {
                    seriesRange.ChangeOffsetTo(length - 1);
                }
            }
        }

        protected void updateBuffers()
        {
            bool fileOffsetChgd = LastUpdatedFileOffset != FileOffset || LastUpdatedBufferLength != IndCount;
            if (fileOffsetChgd)
            {
                updateBuffersServer();
            }
            else
            {
                updateIndicatorsScroll();
            }

            int offset = IndOffset;
            if (Period.GetCategory() == TimePeriodCategory.TICKS)
            {
                ITickTimeSeriesRuntime seriesRuntime = (ITickTimeSeriesRuntime)SeriesRuntime;
                sBids = seriesRuntime.Bid.SubArr(offset, -offset, false);
                sAsks = seriesRuntime.Ask.SubArr(offset, -offset, false);

                if (Period == TimePeriodConst.PERIOD_TICK)
                {
                    sOpen = sLow = sHigh = sClose = sBids;
                }
                else
                {
                    sOpen = sLow = sHigh = sClose = sAsks;
                }
            }
            else
            {
                IPeriodTimeSeriesRuntime seriesRuntime = (IPeriodTimeSeriesRuntime)SeriesRuntime;
                sOpen = seriesRuntime.Open.SubArr(offset, -offset, false);
                sLow = seriesRuntime.Low.SubArr(offset, -offset, false);
                sHigh = seriesRuntime.High.SubArr(offset, -offset, false);
                sClose = seriesRuntime.Close.SubArr(offset, -offset, false);
            }

            sLTime = SeriesRuntime.Begin_time.SubArr(offset, -offset, false);
            sVolume = SeriesRuntime.Volume.SubArr(offset, -offset, false);

            //sTime = new LArrAsIArr(sLTime, false);
        }

        protected void updateIndicatorsScroll()
        {
            // !!!!!!!!!!!!!!!!!!--->
            int offset = IndOffset - BufferFromIndex;

            foreach (IIndicatorRuntime ind in Indicators.Values)
            {
                if (!ind.Initialized || ind.Session.EnableScroll)
                {
                    for (int i = 0; i < ind.NumIndicatorBuffers; i++)
                    {
                        var b = ind.Buffers[i];
                        if (b.Buffer != null)
                        {
                            b.SBuffer = b.Buffer.SubArr(offset, -offset, false);
                        }
                    }
                }
            }
        }

        protected abstract void updateBuffersServer();

    }

}


