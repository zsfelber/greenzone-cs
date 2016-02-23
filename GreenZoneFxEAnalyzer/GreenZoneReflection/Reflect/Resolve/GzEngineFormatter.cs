using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace GreenZoneParser.Reflect
{
    public class GzEngineFormatter
    {

        public static string ToString(Type type, object value)
        {
            string result;
            if (type.Equals(typeof(Color)))
            {
                result = ""+((Color)value).ToArgb();
            }
            else if (typeof(Enum).IsAssignableFrom(type))
            {
                result = "" + Convert.ToDecimal(value);
            }
            else if (value == null)
            {
                result = "#null";
            }
            else if (type.Equals(typeof(string)))
            {
                result = (string)value;
            }
            else if (type.Equals(typeof(bool)))
            {
                result = (bool)value ? "1" : "0";
            }
            else
            {
                result = (string)Convert.ChangeType(value, typeof(string));
            }
            return result;
        }

        public static object ToObject(Type type, string value)
        {
            object result;
            if (type.Equals(typeof(Color)))
            {
                int i = Convert.ToInt32(value);
                result = Color.FromArgb(i);
            }
            else if (typeof(Enum).IsAssignableFrom(type))
            {
                decimal i = Convert.ToDecimal(value);
                result = Enum.ToObject(type, i);
            }
            else if ("#null".Equals(value))
            {
                result = null;
            }
            else if (type.Equals(typeof(string)))
            {
                result = value;
            }
            else if (type.Equals(typeof(bool)))
            {
                result = "1".Equals(value);
            }
            else
            {
                result = Convert.ChangeType(value, type);
            }
            return result;
        }
    }
}
