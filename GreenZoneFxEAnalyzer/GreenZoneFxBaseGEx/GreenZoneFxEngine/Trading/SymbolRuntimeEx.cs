using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using GreenZoneUtil.Util;
using System.Collections;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
    public class SymbolContextEx : SymbolContextBase
    {
        internal SymbolContextEx(GreenRmiManager rmiManager, IEnvironmentRuntime parent, symbol symbol)
            : base(rmiManager)
        {
            Parent = parent;
            Symbol = symbol;
        }

        public SymbolContextEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

        protected SymbolContextEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override double GetValue(MarketInfoConst field)
        {
            switch (field)
            {
                case MarketInfoConst.MODE_ASK:
                    return Ask;
                case MarketInfoConst.MODE_BID:
                    return Bid;
                case MarketInfoConst.MODE_DIGITS:
                    return Digits;
                case MarketInfoConst.MODE_EXPIRATION:
                    return (int)Expiration;
                case MarketInfoConst.MODE_FREEZELEVEL:
                    return FreezeLevel;
                case MarketInfoConst.MODE_HIGH:
                    return High;
                case MarketInfoConst.MODE_LOTSIZE:
                    return LotSize;
                case MarketInfoConst.MODE_LOTSTEP:
                    return LotStep;
                case MarketInfoConst.MODE_LOW:
                    return Low;
                case MarketInfoConst.MODE_MARGINCALCMODE:
                    return (int)MarginCalcMode;
                case MarketInfoConst.MODE_MARGINHEDGED:
                    return MarginHedged;
                case MarketInfoConst.MODE_MARGININIT:
                    return MarginInit;
                case MarketInfoConst.MODE_MARGINMAINTENANCE:
                    return MarginMaintenance;
                case MarketInfoConst.MODE_MARGINREQUIRED:
                    return MarginRequired;
                case MarketInfoConst.MODE_MAXLOT:
                    return MaxLot;
                case MarketInfoConst.MODE_MINLOT:
                    return MinLot;
                case MarketInfoConst.MODE_POINT:
                    return Point;
                case MarketInfoConst.MODE_PROFITCALCMODE:
                    return (int)ProfitCalcMode;
                case MarketInfoConst.MODE_SPREAD:
                    return Spread;
                case MarketInfoConst.MODE_STARTING:
                    return (int)Starting;
                case MarketInfoConst.MODE_STOPLEVEL:
                    return StopLevel;
                case MarketInfoConst.MODE_SWAPLONG:
                    return SwapLong;
                case MarketInfoConst.MODE_SWAPSHORT:
                    return SwapShort;
                case MarketInfoConst.MODE_SWAPTYPE:
                    return (int)SwapType;
                case MarketInfoConst.MODE_TICKSIZE:
                    return TickSize;
                case MarketInfoConst.MODE_TICKVALUE:
                    return TickValue;
                case MarketInfoConst.MODE_TIME:
                    return (int)Time;
                case MarketInfoConst.MODE_TRADEALLOWED:
                    return TradeAllowed ? 1 : 0;
                default:
                    throw new ArgumentException("field:" + field);
            }
        }
    }

    public class SymbolRuntimeEx : SymbolRuntimeBase
    {

        internal SymbolRuntimeEx(GreenRmiManager rmiManager, IEnvironmentRuntime parent, ISymbolContext context)
            : base(rmiManager)
        {
            Parent = parent;
            Context = context;
            Session = new ServerSymbolSession(rmiManager, Symbol);
        }

        internal SymbolRuntimeEx(GreenRmiManager rmiManager, IEnvironmentRuntime parent, ISymbolContext context, ISymbolSession session)
            : base(rmiManager)
        {
            Parent = parent;
            Context = context;
            Session = session;
        }

        public SymbolRuntimeEx(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
		}

		public SymbolRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

        protected SymbolRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        public override string SymbolFormat
        {
            get
            {
                string f2 = "0000000000".Substring(0, Context.Digits);
                string f = "0." + f2;
                return f;
            }
        }

        public override TimePeriodConst BestPeriod
        {
            get
            {
                TimePeriodConst period = TimePeriodConst.PERIOD_MN1;
                foreach (var chg in Parent.Charts)
                {
                    // initialized...
                    if (chg.Charts.Count > 0)
                    {
                        IChartRuntime ch = chg.Charts[0];
                        if (ch.SymbolRuntime == this)
                        {
                            if (ch.Period.GetSecs() < period.GetSecs())
                            {
                                if (ch.Period == TimePeriodConst.PERIOD_TICK_ASK)
                                {
                                    period = TimePeriodConst.PERIOD_TICK;
                                }
                                else
                                {
                                    period = ch.Period;
                                }
                            }
                        }
                    }
                }
                if (period == TimePeriodConst.PERIOD_MN1)
                {
                    period = TimePeriodConst.PERIOD_CURRENT;
                }
                return period;
            }
        }


        [Browsable(false)]
        public override List<IHistoryOrderEtc> HistoryOrders
        {
            get
            {
                List<IHistoryOrderEtc> result = new List<IHistoryOrderEtc>(Orders.OrdersHistoryEtc);
                return result;
            }
        }

    }


}
