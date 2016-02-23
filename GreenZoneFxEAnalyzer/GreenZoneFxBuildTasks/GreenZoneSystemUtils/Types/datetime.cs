using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace GreenZoneFxEngine.Types
{
    [Serializable]
    public struct datetime : IComparable<datetime>
    {
        public static readonly DateTime _EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly datetime EPOCH = new datetime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static readonly datetime MinValue = new datetime(0);
        public static readonly datetime MaxValue = new datetime(2100, 12, 31, 23, 59, 59, DateTimeKind.Utc);

        DateTime dt;

        public datetime(DateTime dt)
        {
            this.dt = dt.AddTicks(-(dt.Ticks % 10));
        }

        public datetime(datetime dt)
        {
            this.dt = dt.dt;
        }

        public datetime(int y, int M, int d, int h, int m, int s, DateTimeKind dtk)
        {
            dt = new DateTime(y, M, d, h, m, s, dtk);
        }

        public datetime(int epochTime)
        {
            dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(epochTime);
        }

        public datetime(long bitShiftEpochTimeWithMillis)
        {
            int secs = (int)(bitShiftEpochTimeWithMillis >> 32);
            int millis = (int)((bitShiftEpochTimeWithMillis << 32) >> 32);

            dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(secs);
            dt = dt.AddMilliseconds(millis);
        }

        public static implicit operator datetime(int value)
        {
            return new datetime(value);
        }

        public static explicit operator datetime(long value)
        {
            return new datetime(value);
        }

        public static explicit operator int(datetime value)
        {
            int secs = (int)(value.dt - _EPOCH).TotalSeconds;
            return secs;
        }

        public static explicit operator long(datetime value)
        {
            int secs = (int)(value.dt - _EPOCH).TotalSeconds;
            int millis = value.Millisecond;
            long result = ((long)secs << 32) + millis;

            return result;
        }

        public static implicit operator datetime(DateTime value)
        {
            return new datetime(value);
        }

        public static explicit operator DateTime(datetime value)
        {
            return value.dt;
        }

        public static datetime operator +(datetime d1, int delta)
        {
            return new datetime((d1 - EPOCH) + delta);
        }

        public static datetime operator -(datetime d1, int delta)
        {
            return new datetime((d1 - EPOCH) - delta);
        }

        public static int operator *(datetime d1, int mul)
        {
            return (d1 - EPOCH) * mul;
        }

        public static int operator /(datetime d1, int div)
        {
            return (d1 - EPOCH) / div;
        }

        public static int operator %(datetime d1, int mod)
        {
            return (d1 - EPOCH) % mod;
        }

        public static int operator -(datetime d1, datetime d2)
        {
            return (int)(d1.dt - d2.dt).TotalSeconds;
        }

        public static bool operator <(datetime t1, datetime t2)
        {
            return Subtract(t1.dt, t2.dt) < 0;
        }

        public static bool operator <=(datetime t1, datetime t2)
        {
            return Subtract(t1.dt, t2.dt) <= 0;
        }

        public static bool operator >(datetime t1, datetime t2)
        {
            return Subtract(t1.dt, t2.dt) > 0;
        }

        public static bool operator >=(datetime t1, datetime t2)
        {
            return Subtract(t1.dt, t2.dt) >= 0;
        }

        public static bool operator ==(datetime t1, datetime t2)
        {
            return Subtract(t1.dt, t2.dt) == 0;
        }

        public static bool operator !=(datetime t1, datetime t2)
        {
            return Subtract(t1.dt, t2.dt) != 0;
        }

        public static long Subtract(datetime d1, datetime d2)
        {
            return (long)(d1.dt - d2.dt).TotalMilliseconds;
        }

        public int CompareTo(datetime o)
        {
            return dt.CompareTo(o.dt);
        }

        public static datetime ParseExact(string s, string format, IFormatProvider provider)
        {
            return new datetime(DateTime.ParseExact(s, format, provider));
        }

        public static datetime Min(datetime d1, datetime d2)
        {
            if (d1 <= d2)
            {
                return d1;
            }
            else
            {
                return d2;
            }
        }

        public static datetime Max(datetime d1, datetime d2)
        {
            if (d1 >= d2)
            {
                return d1;
            }
            else
            {
                return d2;
            }
        }

        public static datetime Now { get { return new datetime(DateTime.Now); } }

        public int Day { get { return dt.Day; } }

        public DayOfWeek DayOfWeek { get { return dt.DayOfWeek; } }

        public int DayOfYear { get { return dt.DayOfYear; } }

        public int Hour { get { return dt.Hour; } }

        public int Minute { get { return dt.Minute; } }

        public int Month { get { return dt.Month; } }

        public int Second { get { return dt.Second; } }

        public int Millisecond { get { return dt.Millisecond; } }

        public int Year { get { return dt.Year; } }

        public int WeekOfYear
        {
            get
            {
                GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
                return cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            }
        }

        public int GetWeekOfYear(CalendarWeekRule weekRule, DayOfWeek firstDayOfWeek)
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(dt, weekRule, firstDayOfWeek);
        }

        public datetime AddMilliseconds(double value)
        {
            return new datetime(dt.AddMilliseconds(value));
        }

        public datetime AddSeconds(double value)
        {
            return new datetime(dt.AddSeconds(value));
        }

        public datetime AddMinutes(double value)
        {
            return new datetime(dt.AddMinutes(value));
        }

        public datetime AddHours(double value)
        {
            return new datetime(dt.AddHours(value));
        }

        public datetime AddDays(double value)
        {
            return new datetime(dt.AddDays(value));
        }

        public datetime AddMonths(int value)
        {
            return new datetime(dt.AddMonths(value));
        }

        public datetime AddYears(int value)
        {
            return new datetime(dt.AddYears(value));
        }

        public override bool Equals(object o)
        {
            return Subtract(dt, ((datetime)o).dt) == 0;
        }

        public override int GetHashCode()
        {
            return dt.GetHashCode();
        }

        public override string ToString()
        {
            return dt.ToString();
        }

        public string ToString(string format)
        {
            return dt.ToString(format);
        }

        public string ToShortDateString()
        {
            return dt.ToShortDateString();
        }

        public string ToLongDateString()
        {
            return dt.ToLongDateString();
        }

        public string ToShortTimeString()
        {
            return dt.ToShortTimeString();
        }

        public string ToLongTimeString()
        {
            return dt.ToLongTimeString();
        }
    }
}
