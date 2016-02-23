using System;
using System.Collections.Generic;
using GreenZoneFxEngine.Types;
using System.Collections;
using GreenZoneFxEngine.Util;
using System.Reflection;
using System.ComponentModel;
using GreenZoneUtil.Util;
using System.Text;

namespace GreenZoneFxEngine.Trading
{


    public class SymbolPeriodId
    {
        public readonly symbol symbol;
        public readonly TimePeriodConst period;

        public SymbolPeriodId(symbol symbol, TimePeriodConst period)
        {
            this.symbol = symbol;
            this.period = period;
        }

        public int CompareTo(SymbolPeriodId o)
        {
            int r;
            if (o == null)
            {
                return 1;
            }
            else if (0 != (r = symbol.CompareTo(o.symbol)))
            {
                return r;
            }
            else if (0 != (r = (period - o.period)))
            {
                return r;
            }
            return 0;
        }

        public override string ToString()
        {
            return symbol + " " + period.GetShortTxt();
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            else if (o is SymbolPeriodId)
            {
                SymbolPeriodId so = (SymbolPeriodId)o;
                return symbol.Equals(so.symbol) && period == so.period;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (int)period;
        }
    }

    [Serializable]
    public class IndicatorId
    {
        internal readonly Mt4ExecutableInfo indicator;
        internal readonly string argKey;

        public IndicatorId(IEnvironmentRuntime environment, string key)
        {
            int i = key.IndexOf(",");
            this.indicator = environment.GetIndicatorInfo(key.Substring(0,i));
            this.argKey = key.Substring(i + 1);
        }

        public IndicatorId(Mt4ExecutableInfo indicator, string argKey)
        {
            this.indicator = indicator;
            this.argKey = argKey;
        }

        public int CompareTo(IndicatorId o)
        {
            int r;
            if (o == null)
            {
                return 1;
            }
            else if (0 != (r = argKey.CompareTo(o.argKey)))
            {
                return r;
            }
            else
            {
                r = indicator.SystemTypeId.CompareTo(o.indicator.SystemTypeId);
                return r;
            }
        }

        public override string ToString()
        {
            return indicator.ShortTypeName + "(" + argKey + ")";
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            else if (o is IndicatorId)
            {
                IndicatorId so = (IndicatorId)o;
                return indicator.Equals(so.indicator) && argKey.Equals(so.argKey);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return argKey.GetHashCode();
        }
    }

    [Serializable]
    public class SymbolPeriodIndicatorId
    {
        public readonly symbol Symbol;
        public readonly TimePeriodConst Period;
        public readonly Mt4ExecutableInfo Indicator;
        public string ArgKey;

        public SymbolPeriodIndicatorId(symbol symbol, TimePeriodConst period, Mt4ExecutableInfo indicator, string argKey)
        {
            this.Symbol = symbol;
            this.Period = period;
            this.Indicator = indicator;
            this.ArgKey = argKey;
        }

        public SymbolPeriodIndicatorId(symbol symbol, TimePeriodConst period, IndicatorId ind)
        {
            this.Symbol = symbol;
            this.Period = period;
            this.Indicator = ind.indicator;
            this.ArgKey = ind.argKey;
        }

        internal SymbolPeriodId SymbolPeriodId
        {
            get
            {
                return new SymbolPeriodId(Symbol, Period);
            }
        }
        public int CompareTo(SymbolPeriodIndicatorId o)
        {
            int r;
            if (o == null)
            {
                return 1;
            }
            else if (0 != (r = Symbol.CompareTo(o.Symbol)))
            {
                return r;
            }
            else if (0 != (r = (Period - o.Period)))
            {
                return r;
            }
            else if (0 != (r = ArgKey.CompareTo(o.ArgKey)))
            {
                return r;
            }
            else
            {
                r = Indicator.SystemTypeId.CompareTo(o.Indicator.SystemTypeId);
                return r;
            }
        }

        public override string ToString()
        {
            return Symbol + " " + Period.GetShortTxt() + " " + Indicator.ShortTypeName + "(" + ArgKey + ")";
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            else if (o is SymbolPeriodIndicatorId)
            {
                SymbolPeriodIndicatorId so = (SymbolPeriodIndicatorId)o;
                return Symbol.Equals(so.Symbol) && Period == so.Period && Indicator.Equals(so.Indicator) && ArgKey.Equals(so.ArgKey);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ArgKey.GetHashCode();
        }
    }



}


