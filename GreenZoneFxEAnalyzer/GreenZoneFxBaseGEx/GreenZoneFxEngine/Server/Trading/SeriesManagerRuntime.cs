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

namespace GreenZoneFxEngine.Trading
{
    public class ServerSeriesManagerUtils
    {
        internal static void processIndicators(Dictionary<IndicatorId,IIndicatorRuntime> Indicators, IIndicatorRuntime indicator, int bufLen, long fileOffset = 0)
        {
            List<IIndicatorRuntime> l;
            if (indicator == null)
            {
                l = new List<IIndicatorRuntime>(Indicators.Values);
            }
            else
            {
                l = new List<IIndicatorRuntime>();
                l.Add(indicator);
            }


            foreach (IServerIndicatorRuntime _ind in l)
            {
                if (_ind.UpdateState == IndicatorUpdateState.PENDING_UPDATE)
                {
                    IServerIndicatorRuntime ind;

                    // if it was initialized and used already, should be deleted
                    if (_ind.Initialized)
                    {
                        ind = (IServerIndicatorRuntime)_ind.Copy();
                        Indicators[_ind.Id] = ind;

                        // !
                        _ind.Deinit();
                        _ind.RaiseInstanceChanged(ind);
                    }
                    else
                    {
                        ind = _ind;
                    }

                    ind.UpdateState = IndicatorUpdateState.UPDATING;
                    ind.IndicatorLastOffset = bufLen;
                    ind.LastFileOffset = fileOffset;
                    ind.LastBufferLength = bufLen;
                    try
                    {
                        // !
                        ind.Init();
                        // NOTE checking whether indicator really has at lst 1 buffer
                        if (ind.Buffers.Length == 0 && ind.Buffers[0].Buffer != null)
                        {
                            throw new NotSupportedException("Invalid indicator : " + ind.IndicatorInfo.Name + " (" + ind.Session.ShortName + "). Indicator should have at least 1 data buffer.");
                        }

                        for (int i = 0; i < ind.NumIndicatorBuffers; i++)
                        {
                            var b = ind.Buffers[i];
                            b.Buffer.SetLengthAndDetachChildren(bufLen);
                            b.Buffer.Clear();
                        }

                        // !
                        ind.OnTick();
                    }
                    finally
                    {
                        ind.TmpArrayCaches.Clear();
                        ind.IndicatorLastOffset = 0;
                        ind.UpdateState = IndicatorUpdateState.NORMAL;
                    }
                }
            }
        }
    }

    public class ServerSeriesManagerRuntimeEx : ServerSeriesManagerRuntimeBase
    {
        public event PropertyChangedEventHandler DefaultChanged;

        internal ServerSeriesManagerRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, bool cursor = false)
            : base(rmiManager)
        {
            Environment = parent.Environment;
            Parent = parent;

            if (cursor)
            {
                UpdateCursorDefault();
            }
            else
            {
                UpdateDefault();
            }
        }

        internal ServerSeriesManagerRuntimeEx(GreenRmiManager rmiManager, IEnvironmentRuntime environment, symbol symbol, TimePeriodConst period, datetime focusedTime)
            : base(rmiManager)
        {
            Environment = environment;
            Parent = null;

            UpdateDefault(symbol, period, focusedTime);
        }

        internal ServerSeriesManagerRuntimeEx(GreenRmiManager rmiManager, IEnvironmentRuntime environment)
             : base(rmiManager)
        {
            Environment = environment;
            Parent = null;
        }

        public override INormalSeriesManagerCache CreateSeriesManagerCache(SymbolPeriodId sp)
        {
            INormalSeriesManagerCache r;
            if (Parent != null)
            {
                r = new ServerNormalSeriesManagerCacheEx(rmiManager, this, sp, FocusedTime);
            }
            else
            {
                r = new ServerStreamSeriesManagerCache(rmiManager, this, sp, FocusedTime);
            }
            return r;
        }

        public override void AddExpert(IServerChartRuntime c)
        {
            ServerStreamSeriesManagerCache cache = (ServerStreamSeriesManagerCache)this[c.Symbol, c.Period];
            cache.AddExpert(c);
        }

        public override void UpdateDefault()
        {
            UpdateDefault(Parent.Symbol, Parent.Period, Parent.ScrolledBarTime);
        }

        public override void UpdateCursorDefault()
        {
            IServerSymbolRuntime symbolRuntime = (IServerSymbolRuntime)Parent.SymbolRuntime;
            TimePeriodConst cperiod = symbolRuntime.BestCursorPeriod;
            UpdateDefault(symbolRuntime.Symbol, cperiod, Parent.ScrolledBarTime);
        }

        public override void UpdateDefault(symbol symbol, TimePeriodConst period, datetime focusedTime)
        {
            SymbolPeriodId spdef = new SymbolPeriodId(symbol, period);

            INormalSeriesManagerCache newCache;
            if (!SeriesCaches.TryGetValue(spdef, out newCache))
            {
                if (Parent != null)
                {
                    newCache = new ServerNormalSeriesManagerCacheEx(rmiManager, this, spdef, focusedTime);
                }
                else
                {
                    newCache = new ServerStreamSeriesManagerCache(rmiManager, this, spdef, focusedTime);
                }
                DefaultCache = (IServerNormalSeriesManagerCache)newCache;
                SeriesCaches[spdef] = DefaultCache;
            }
            else
            {
                DefaultCache = (IServerNormalSeriesManagerCache)newCache;
                DefaultCache.Load(focusedTime);
            }


            if (DefaultChanged != null)
            {
                DefaultChanged(this, new PropertyChangedEventArgs("DefaultCache"));
            }
        }

        public override void Tick(symbol symbol, double Bid, double Ask, double volume)
        {
            foreach (ServerStreamSeriesManagerCache sc in SeriesCaches.Values)
            {
                if (sc.Symbol == symbol)
                {
                    sc.Tick(Bid, Ask, volume);
                }
            }
        }

        public override void DeinitAll()
        {
            foreach (IServerNormalSeriesManagerCache sc in SeriesCaches.Values)
            {
                sc.DeinitAll();
            }
        }
    }

    public class ServerNormalSeriesManagerCacheEx : ServerNormalSeriesManagerCacheBase
    {
        internal DArr _Open, _Low, _High, _Close;
        internal DArr _Bids, _Asks;
        internal LArr _LTime;
        //internal LArrAsIArr _Time;
        internal DArr _Volume;

        internal ServerNormalSeriesManagerCacheEx(GreenRmiManager rmiManager, ServerSeriesManagerRuntimeEx parent, SymbolPeriodId symbolPeriod, datetime focusedTime)
            : base(rmiManager, parent, symbolPeriod)
        {

            SeriesRuntime = SymbolRuntime.LoadSeries(Period, focusedTime);
            updateBuffers();
        }

        public ServerNormalSeriesManagerCacheEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

        protected ServerNormalSeriesManagerCacheEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        ~ServerNormalSeriesManagerCacheEx()
        {
            // !!!!!!!!!!!!!!!!!!
            DeinitAll();
        }

        public override void DeinitAll()
        {
            foreach (ServerIndicatorRuntime ind in this)
            {
                ind.Deinit();
            }
        }


        protected override void updateBuffersServer()
        {
            int from = BufferFromIndex;

            //NOTE reverse indexing
            if (Period.GetCategory() == TimePeriodCategory.TICKS)
            {
                ServerTickTimeSeriesRuntime seriesRuntime = (ServerTickTimeSeriesRuntime)SeriesRuntime;
                _Bids = (DArr)seriesRuntime.Bid.SubArr(from);
                _Asks = (DArr)seriesRuntime.Ask.SubArr(from);

                Bids = (DArr)seriesRuntime.Bid.SubArr(from, 0, false);
                Asks = (DArr)seriesRuntime.Ask.SubArr(from, 0, false);

                if (Period == TimePeriodConst.PERIOD_TICK)
                {
                    _Open = _Low = _High = _Close = _Bids;
                    Open = Low = High = Close = Bids;
                }
                else
                {
                    _Open = _Low = _High = _Close = _Asks;
                    Open = Low = High = Close = Asks;
                }
            }
            else
            {
                ServerPeriodTimeSeriesRuntime seriesRuntime = (ServerPeriodTimeSeriesRuntime)SeriesRuntime;
                _Open = (DArr)seriesRuntime.Open.SubArr(from);
                _Low = (DArr)seriesRuntime.Low.SubArr(from);
                _High = (DArr)seriesRuntime.High.SubArr(from);
                _Close = (DArr)seriesRuntime.Close.SubArr(from);

                Open = (DArr)seriesRuntime.Open.SubArr(from, 0, false);
                Low = (DArr)seriesRuntime.Low.SubArr(from, 0, false);
                High = (DArr)seriesRuntime.High.SubArr(from, 0, false);
                Close = (DArr)seriesRuntime.Close.SubArr(from, 0, false);
            }

            _LTime = (LArr)SeriesRuntime.Begin_time.SubArr(from);
            _Volume = (DArr)SeriesRuntime.Volume.SubArr(from);

            LTime = (LArr)SeriesRuntime.Begin_time.SubArr(from, 0, false);
            Volume = (DArr)SeriesRuntime.Volume.SubArr(from, 0, false);

            //_Time = new LArrAsIArr(_LTime, false);
            Time = new LArrAsIArr((LArr)LTime, false);

            LastUpdatedFileOffset = FileOffset;
            LastUpdatedBufferLength = Bars;

            updateIndicatorsServer();
        }

        protected override void updateIndicatorsServer(IIndicatorRuntime indicator = null)
        {
            List<IIndicatorRuntime> l;
            if (indicator == null)
            {
                l = new List<IIndicatorRuntime>(Indicators.Values);
            }
            else
            {
                l = new List<IIndicatorRuntime>();
                l.Add(indicator);
            }

            foreach (ServerIndicatorRuntime ind in l)
            {
                if (ind.UpdateState == IndicatorUpdateState.NORMAL && (ind.LastFileOffset != FileOffset || ind.LastBufferLength != Bars) && (!ind.Initialized || ind.Session.EnableScroll))
                {
                    ind.UpdateState = IndicatorUpdateState.PENDING_UPDATE;
                }
            }

            processIndicators(indicator, Bars, FileOffset);

            updateIndicatorsScroll();
        }

        protected void processIndicators(IIndicatorRuntime indicator, int bufLen, long fileOffset = 0)
        {
            ServerSeriesManagerUtils.processIndicators(Indicators, indicator, bufLen, fileOffset);
        }
    }


    public class ServerStreamSeriesManagerCache : ServerStreamSeriesManagerCacheBase
    {
        protected readonly List<ServerExpertRuntime> experts = new List<ServerExpertRuntime>();

        internal ServerStreamSeriesManagerCache(GreenRmiManager rmiManager, ServerSeriesManagerRuntimeEx parent, SymbolPeriodId symbolPeriod, datetime focusedTime)
            : base(rmiManager, parent, symbolPeriod, focusedTime)
        {
        }

        public override void DeinitAll()
        {
            base.DeinitAll();

            foreach (ServerExpertRuntime x in experts)
            {
                x.Deinit();
            }
        }

        public override int Bars
        {
            get
            {
                return SeriesRuntime.IndCount - SeriesRuntime.IndOffset;
            }
        }

        public override int BufferFromIndex
        {
            get
            {
                return SeriesRuntime.IndOffset;
            }
        }

        public override void AddExpert(IServerChartRuntime c)
        {
            if (c.Period != Period || c.Symbol != Symbol)
            {
                throw new NotSupportedException("Attempt adding a chart expert to a different period / symbol cache:" + c.Period + " " + Period + "   " + c.Symbol + " " + Symbol);
            }
            ServerExpertRuntime x = (ServerExpertRuntime)ServerExpertRuntime.Create(rmiManager, c, c.Expert.ExpertInfo, this);
            c.Expert.CopyParamsTo(x);

            // !!!!!!!!!!!!!!!!
            x.Init();

            experts.Add(x);
        }


        public override void Bar(TimePeriodConst period, double open, double low, double high, double close, double volume, int offset)
        {
            if (period.GetSecs() < 1)
            {
                throw new NotSupportedException("No Bars supported for period:" + period);
            }
            if (Environment.EnvironmentType.IsOnline() || offset == 0)
            {
                _Open[offset] = open;
                _Low[offset] = low;
                _High[offset] = high;
                _Close[offset] = close;
                _Volume[offset] = volume;
            }
            else
            {
                StringBuilder err = new StringBuilder();
                app(err, "open", Open[offset], open);
                app(err, "low", Low[offset], low);
                app(err, "high", High[offset], high);
                app(err, "close", Close[offset], close);
                app(err, "volume", Volume[offset], volume);
                if (err.Length > 0)
                {
                    throw new NotSupportedException("Bar mismatch. offset:" + offset + " time:" + GreenZoneSysUtilsBase.FormatDateTime((DateTime)(datetime)LTime[offset]) + "\n" + err);
                }
            }

            foreach (var ind in this)
            {
                ind.IndicatorLastOffset = offset;
            }
        }

        void app(StringBuilder b, string label, double oldv, double newv)
        {
            if (oldv != newv)
            {
                b.Append(label + "  old:" + oldv.ToString("#.#") + " new:" + newv.ToString("#.#"));
                b.Append('\n');
            }
        }

        public override void Tick(double Bid, double Ask, double volume)
        {
            this.Bid = Bid;
            this.Ask = Ask;

            if (Period.GetCategory() != TimePeriodCategory.TICKS)
            {
                _Close[0] = Bid;
                _High[0] = Math.Max(High[0], Bid);
                _Low[0] = Math.Min(Low[0], Bid);
                _Volume[0] += volume;
            }

            foreach (ServerIndicatorRuntime ind in this)
            {
                ind.OnTick();
                ind.IndicatorLastOffset = 0;
            }
            foreach (ServerExpertRuntime x in experts)
            {
                x.OnTick();
            }
        }
    }

    public class ServerArraySeriesManagerCache : ServerArraySeriesManagerCacheBase
    {

        DArr priceArray;

        internal ServerArraySeriesManagerCache(GreenRmiManager rmiManager, IServerSeriesManagerRuntime parent, SymbolPeriodId symbolPeriod, DArr priceArray)
            : base(rmiManager, parent, symbolPeriod)
        {
            this.priceArray = priceArray;

            this.Close = this.priceArray;
        }

        ~ServerArraySeriesManagerCache()
        {
            // !!!!!!!!!!!!!!!!!!
            DeinitAll();
        }

        public override void DeinitAll()
        {
            foreach (ServerIndicatorRuntime ind in this)
            {
                ind.Deinit();
            }
        }

        public override int Bars
        {
            get
            {
                return priceArray.Length;
            }
        }

        protected override void updateIndicatorsServer(IIndicatorRuntime indicator = null)
        {
            List<IIndicatorRuntime> l;
            if (indicator == null)
            {
                l = new List<IIndicatorRuntime>(Indicators.Values);
            }
            else
            {
                l = new List<IIndicatorRuntime>();
                l.Add(indicator);
            }

            foreach (IIndicatorRuntime ind in l)
            {
                if (!ind.Initialized || ind.Session.EnableScroll)
                {
                    ind.UpdateState = IndicatorUpdateState.PENDING_UPDATE;
                }
            }

            processIndicators(indicator, priceArray.Length);
        }

        protected void processIndicators(IIndicatorRuntime indicator, int bufLen, long fileOffset = 0)
        {
            ServerSeriesManagerUtils.processIndicators(Indicators, indicator, bufLen, fileOffset);
        }
    }



}


