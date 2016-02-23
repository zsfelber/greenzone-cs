using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace GreenZoneUtil.Util
{
    public delegate void SimpleDelegate();
    public delegate void SimpleIntDelegate(int i);

    public class GreenZoneSysUtilsBase
    {
        static readonly Regex rxfindnum = new Regex(@"\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static readonly Regex rxfindnumbg = new Regex(@"^\d+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static List<T> AsList<T>(params T[] array)
        {
            List<T> result = new List<T>(array);
            return result;
        }

        public static T[] AddToArray<T>(T[] inputArr, params T[] additionalItems)
        {
            T[] result = new T[inputArr.Length + additionalItems.Length];
            inputArr.CopyTo(result, 0);
            additionalItems.CopyTo(result, inputArr.Length);
            return result;
        }

        public static int FindNumber(string txt, ref int startPos, ref int length)
        {
            return findRgxNum(rxfindnum, txt, ref startPos, ref length);
        }

        public static int FindNumberBegin(string txt, ref int startPos, ref int length)
        {
            return findRgxNum(rxfindnumbg, txt, ref startPos, ref length);
        }

        static int findRgxNum(Regex rx, string txt, ref int startPos, ref int length)
        {
            int _startPos = startPos;
            if (startPos > 0)
            {
                txt = txt.Substring(startPos);
                startPos = 0;
            }
            Match match;
            if (length > 0)
            {
                match = rx.Match(txt, startPos, length);
            }
            else
            {
                match = rx.Match(txt, startPos);
            }

            if (match.Success)
            {
                startPos = match.Index;
                length = match.Length;
                int result = Convert.ToInt32(txt.Substring(startPos, length));
                startPos += _startPos;
                return result;
            }
            else
            {
                startPos = -1;
                length = -1;
                return -1;
            }
        }

        public static string FormatDateTime(DateTime from)
        {
            string f1;
            if (from != default(DateTime))
            {
                f1 = from.ToShortDateString() + " " + from.ToShortTimeString();
            }
            else
            {
                f1 = "";
            }

            return f1;
        }

        public static string GetIntervalStr(DateTime from, DateTime to)
        {
            string f1, f2;
            if (from != default(DateTime))
            {
                f1 = from.ToShortDateString() + " " + from.ToShortTimeString();
            }
            else
            {
                f1 = "-oo";
            }

            if (to != default(DateTime))
            {
                f2 = to.ToShortDateString() + " " + to.ToShortTimeString();
            }
            else
            {
                f2 = "+oo";
            }

            string result = f1 + "  ..  " + f2;
            return result;
        }

        public static string GetShortDateTimePattern()
        {
            string pattern = DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortTimePattern;

            return pattern;
        }

        public static string GenerateArgKey(object[] args)
        {
            StringBuilder b = new StringBuilder();
            foreach (var arg in args)
            {
                b.Append(arg);
                b.Append(",");
            }
            if (b.Length > 0)
            {
                b.Length = b.Length - 1;
            }
            string key = b.ToString();
            return key;
        }

        public static string InstallDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string UserHomeDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
        }

        public static string NumberToRoman(int number)
        {
            if (number < 0 || number > 3999)
            {
                throw new ArgumentException("Value must be in the range 0 – 3999.");
            }
            if (number == 0) return "N";

            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] numerals = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            StringBuilder result = new StringBuilder();

            // Loop through each of the values to diminish the number
            for (int i = 0; i < 13; i++)
            {

                // If the number being converted is less than the test value, append
                // the corresponding numeral or numeral pair to the resultant string
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }

            }

            return result.ToString();
        }

        public static int toi(object o, int d = 0)
        {
            return o == null ? d : (int)o;
        }

        public static double tod(object o, double d = 0)
        {
            return o == null ? d : (double)o;
        }

        public static T to<T>(object o, T d = default(T))
        {
            return o == null ? d : (T)o;
        }

        public static string EscapeXml(object _input)
        {
            if (_input == null)
            {
                return null;
            }
            else
            {
                string input = _input.ToString();

                StringBuilder result = new StringBuilder(input.Length + 2);
                foreach (var c in input)
                {
                    switch (c)
                    {
                        case '\'': result.Append(@"\'"); break;
                        case '\"': result.Append("\\\""); break;
                        case '\\': result.Append(@"\\"); break;
                        case '\0': result.Append(@"\0"); break;
                        case '\a': result.Append(@"\a"); break;
                        case '\b': result.Append(@"\b"); break;
                        case '\f': result.Append(@"\f"); break;
                        case '\n': result.Append(@"\n"); break;
                        case '\r': result.Append(@"\r"); break;
                        case '\t': result.Append(@"\t"); break;
                        case '\v': result.Append(@"\v"); break;
                        default:
                            // ASCII printable character
                            if (c >= 0x20 && c <= 0x7e)
                            {
                                result.Append(c);
                                // As UTF16 escaped character
                            }
                            else
                            {
                                result.Append(@"\u");
                                result.Append(((int)c).ToString("x4"));
                            }
                            break;
                    }
                }
                return result.ToString();
            }
        }
    
    }

}