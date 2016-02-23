using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Types
{
    [Serializable]
    public struct symbol : IComparable<symbol>
    {
        public readonly string strSymbol;

        public readonly int index;

        public symbol(string strSymbol, int index)
        {
            this.strSymbol = strSymbol;
            this.index = index;
        }

        public static explicit operator string(symbol value)
        {
            return value.strSymbol;
        }

        public static bool operator <(symbol t1, symbol t2)
        {
            return t1.CompareTo(t2) < 0;
        }

        public static bool operator <=(symbol t1, symbol t2)
        {
            return t1.CompareTo(t2) <= 0;
        }

        public static bool operator >(symbol t1, symbol t2)
        {
            return t1.CompareTo(t2) > 0;
        }

        public static bool operator >=(symbol t1, symbol t2)
        {
            return t1.CompareTo(t2) >= 0;
        }

        public static bool operator ==(symbol t1, symbol t2)
        {
            return t1.CompareTo(t2) == 0;
        }

        public static bool operator !=(symbol t1, symbol t2)
        {
            return t1.CompareTo(t2) != 0;
        }

        public static bool operator ==(string t1, symbol t2)
        {
            return t1.Equals(t2.strSymbol);
        }

        public static bool operator !=(string t1, symbol t2)
        {
            return !t1.Equals(t2.strSymbol);
        }

        public static bool operator ==(symbol t1, string t2)
        {
            return t1.strSymbol.Equals(t2);
        }

        public static bool operator !=(symbol t1, string t2)
        {
            return !t1.strSymbol.Equals(t2);
        }

        public int CompareTo(symbol o)
        {
            if (o == null)
            {
                return 1;
            }
            else
            {
                return strSymbol.CompareTo(o.strSymbol);
            }
        }

        public override string ToString()
        {
            return strSymbol;
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            else if (o is symbol)
            {
                return strSymbol.Equals(((symbol)o).strSymbol);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return index;
        }

    }
}
