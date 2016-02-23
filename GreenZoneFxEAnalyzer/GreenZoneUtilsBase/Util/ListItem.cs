using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GreenZoneFxEngine.Util
{
    public class ListItem<T> : IComparable<ListItem<T>> where T : IComparable
    {
        public ListItem(T value, string text)
        {
            Value = value;
            Text = text;
        }

        public static implicit operator T(ListItem<T> itm)
        {
            return itm == null ? default(T) : itm.Value;
        }

        public T Value
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return Value == null;
            }
            else if (obj is ListItem<T>)
            {
                ListItem<T> o = (ListItem<T>)obj;
                return Value == null ? o.Value == null : Value.Equals(o.Value);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public int CompareTo(ListItem<T> other)
        {
            return Value == null ? (other == null || other.Value == null ? 0 : -1) : Value.CompareTo(other.Value);
        }
    }
}
