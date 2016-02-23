using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.ComponentModel;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;
using GreenZoneUtil.Util;
using System.Reflection;
using System.Drawing;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{
    public class ChartGroupRuntimeEx : ChartGroupRuntimeBase
    {

        public ChartGroupRuntimeEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public ChartGroupRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

		protected ChartGroupRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override IChartRuntime MasterChart
        {
            get
            {
                return Charts.Count >= 1 ? Charts[0] : null;
            }
        }

        public override IChartRuntime FirstConnectedChart
        {
            get
            {
                foreach (var c in Charts)
                {
                    if (c.Session.IsCursorBarConnected)
                    {
                        return c;
                    }
                }
                return null;
            }
        }

        public override datetime ScrolledBarTime
        {
            get
            {
                foreach (IClientChartRuntime ch in Charts)
                {
                    if (ch.Session.IsCursorBarConnected)
                    {
                        return ((IChartOwner)ch).ScrolledBarTime;
                    }
                }
                return datetime.MaxValue;
            }
            set
            {
                foreach (IClientChartRuntime ch in Charts)
                {
                    if (ch.Session.IsCursorBarConnected)
                    {
                        ((IChartOwner)ch).ScrolledBarTime = value;
                    }
                }
            }
        }
    }


    public class ChartCursorRuntimeEx : ChartCursorRuntimeBase
    {
        public ChartCursorRuntimeEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public ChartCursorRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ChartCursorRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override void UpdateSeriesManager()
        {
            try
            {
                SeriesManager.UpdateCursorDefault();
                UpdateSeriesRange();
            }
            catch (TimeSeriesException ex)
            {
                throw ex;
            }
        }

        public override void UpdateSeriesRange()
        {
            LArr slTime = SeriesCache.sLTime;
            SeriesRange = new SeriesRange(slTime.StartIndexP, slTime.Length - 1, 0, 0);
        }
    }



    public class EnvironmentRuntimeEx : EnvironmentRuntimeBase 
    {
        readonly object locker = new object();

        // TODO load starting value
        int currentTicket;

        public EnvironmentRuntimeEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public EnvironmentRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected EnvironmentRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override object Locker
        {
            get
            {
                return locker;
            }
        }

        public override symbol GetSymbol(string symbol)
        {
            foreach (var s in SymbolContexts)
            {
                if (s.Key.strSymbol.Equals(symbol))
                {
                    return s.Key;
                }
            }
            throw new SymbolNotFoundException(symbol);
        }

        public override double MarketInfo(symbol symbol, MarketInfoConst type)
        {
            return SymbolContexts[symbol].GetValue(type);
        }

        public override ISymbolContext GetSymbolContext(symbol symbol)
        {
            return SymbolContexts[symbol];
        }

        public override ISymbolContext GetSymbolContext(string symbol)
        {
            return SymbolContexts[GetSymbol(symbol)];
        }

        public override Mt4ExecutableInfo GetScriptInfo(string systemTypeId)
        {
            Mt4ExecutableInfo r;
            if (!Scripts.TryGetValue(systemTypeId, out r))
            {
                r = Scripts["GreenZoneFxEngine.Trading." + systemTypeId];
            }
            return r;
        }

        public override Mt4ExecutableInfo GetExpertInfo(string systemTypeId)
        {
            Mt4ExecutableInfo r;
            if (!Experts.TryGetValue(systemTypeId, out r))
            {
                r = Experts["GreenZoneFxEngine.Trading." + systemTypeId];
            }
            return r;
        }

        public override Mt4ExecutableInfo GetIndicatorInfo(string systemTypeId)
        {
            Mt4ExecutableInfo r;
            if (!Indicators.TryGetValue(systemTypeId, out r))
            {
                r = Indicators["GreenZoneFxEngine.Trading." + systemTypeId];
            }
            return r;
        }

        public override Mt4ExecutableInfo GetExecutableInfo(string systemTypeId)
        {
            if (Scripts.ContainsKey(systemTypeId))
            {
                return Scripts[systemTypeId];
            }
            else if (Experts.ContainsKey(systemTypeId))
            {
                return Experts[systemTypeId];
            }
            else if (Indicators.ContainsKey(systemTypeId))
            {
                return Indicators[systemTypeId];
            }
            else
            {
                throw new KeyNotFoundException(systemTypeId);
            }
        }

        public override int GenerateTicket()
        {
            lock (locker)
            {
                return ++currentTicket;
            }
        }
    }


}
