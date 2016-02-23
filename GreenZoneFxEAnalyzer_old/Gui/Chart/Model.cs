using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine.Gui.Chart
{

    [Serializable]
    public struct Range
    {
        public Range(int from, int to)
            : this()
        {
            From = from;
            To = to;
        }

        public int From
        {
            get;
            set;
        }
        public int To
        {
            get;
            set;
        }
    }


    public struct PriceLabelY
    {
        internal readonly double levelPrice;
        internal readonly int screenY;
        internal readonly int importance;

        public PriceLabelY(double levelPrice, int screenY, int importance)
        {
            this.levelPrice = levelPrice;
            this.screenY = screenY;
            this.importance = importance;
        }

        public override bool Equals(object obj)
        {
            PriceLabelY o = (PriceLabelY)obj;
            return levelPrice==o.levelPrice && screenY == o.screenY && importance == o.importance;
        }

        public override int GetHashCode()
        {
            return levelPrice.GetHashCode();
        }

        public static bool operator ==(PriceLabelY a, PriceLabelY b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(PriceLabelY a, PriceLabelY b)
        {
            return !a.Equals(b);
        }
    }

    public struct TimeLabelX
    {
        internal readonly datetime levelTime;
        internal readonly string formattedTime;
        internal readonly int screenX;
        internal readonly int importance;

        public TimeLabelX(datetime levelTime, string formattedTime, int screenX, int importance)
        {
            this.levelTime = levelTime;
            this.formattedTime = formattedTime;
            this.screenX = screenX;
            this.importance = importance;
        }

        public override bool Equals(object obj)
        {
            TimeLabelX o = (TimeLabelX)obj;
            return levelTime.Equals(o.levelTime) && screenX == o.screenX && importance == o.importance;
        }

        public override int GetHashCode()
        {
            return levelTime.GetHashCode();
        }

        public static bool operator ==(TimeLabelX a, TimeLabelX b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(TimeLabelX a, TimeLabelX b)
        {
            return !a.Equals(b);
        }
    }

    public class SeriesBar
    {
        internal readonly int x1, x2;

        internal readonly int seriesIndex;

        internal readonly datetime time;

        internal readonly List<IndicatorBar> indicatorBars = new List<IndicatorBar>();

        internal SeriesBar(int x1, int x2, int seriesIndex,datetime time)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.seriesIndex = seriesIndex;
            this.time = time;
        }

        public int Width
        {
            get
            {
                return x2 - x1;
            }
        }

        public bool ContainsX(Point p)
        {
            return x1 <= p.X && p.X <= x2;
        }

        public int CompareX(int x)
        {
            if (x2 < x)
            {
                return -1;
            }
            else if (x1 > x)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int CompareDt(datetime dt)
        {
            if (time < dt)
            {
                return -1;
            }
            else if (time > dt)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public virtual void PrintStatus(Chart chart, string f)
        {
        }
    }

    public class PeriodSeriesBar : SeriesBar
    {
        internal readonly int yo, yl, yh, yc;

        internal readonly double open, low, high, close;

        internal PeriodSeriesBar(int x1, int x2, int yo, int yl, int yh, int yc, int seriesIndex,
                           datetime time, double open, double low, double high, double close)
            : base(x1, x2, seriesIndex, time)
        {
            this.yo = yo;
            this.yl = yl;
            this.yh = yh;
            this.yc = yc;
            this.open = open;
            this.low = low;
            this.high = high;
            this.close = close;
        }

        public bool Contains(Point p)
        {
            return x1 <= p.X && p.X <= x2 && yh <= p.Y && p.Y <= yl;
        }

        public override void PrintStatus(Chart chart, string f)
        {
            chart.Form1.OLabel.Text = "O";
            chart.Form1.LLabel.Text = "L";
            chart.Form1.HLabel.Text = "H";
            chart.Form1.CLabel.Text = "C";

            chart.Form1.openLabel.Text = open.ToString(f);
            chart.Form1.lowLabel.Text = low.ToString(f);
            chart.Form1.highLabel.Text = high.ToString(f);
            chart.Form1.closeLabel.Text = close.ToString(f);
        }
    }

    public class TickSeriesBar : SeriesBar
    {
        internal readonly int yask, ybid;

        internal readonly double ask, bid;

        internal TickSeriesBar(int x1, int x2, int yask, int ybid, int seriesIndex,
                           datetime time, double ask, double bid)
            : base(x1, x2, seriesIndex, time)
        {
            this.yask = yask;
            this.ybid = ybid;
            this.ask = ask;
            this.bid = bid;
        }

        public bool Contains(Point p)
        {
            return x1 <= p.X && p.X <= x2 && yask <= p.Y && p.Y <= ybid;
        }

        public override void PrintStatus(Chart chart, string f)
        {
            chart.Form1.OLabel.Text = "Bid";
            chart.Form1.LLabel.Text = "";
            chart.Form1.HLabel.Text = "Ask";
            chart.Form1.CLabel.Text = "";

            chart.Form1.openLabel.Text = bid.ToString(f);
            chart.Form1.highLabel.Text = ask.ToString(f);

            chart.Form1.lowLabel.Text = "";
            chart.Form1.closeLabel.Text = "";
        }
    }

    public class OrderSeriesBar : SeriesBar
    {
        internal readonly int ybalance;

        internal readonly double balance;

        internal OrderSeriesBar(int x1, int x2, int ybalance, int seriesIndex,
                           datetime time, double balance)
            : base(x1, x2, seriesIndex, time)
        {
            this.ybalance = ybalance;
            this.balance = balance;
        }

        public bool Contains(Point p)
        {
            return x1 <= p.X && p.X <= x2;
        }

        public override void PrintStatus(Chart chart, string f)
        {
            chart.Form1.OLabel.Text = "Balance";
            chart.Form1.LLabel.Text = "";
            chart.Form1.HLabel.Text = "";
            chart.Form1.CLabel.Text = "";

            chart.Form1.openLabel.Text = balance.ToString(f);

            chart.Form1.lowLabel.Text = "";
            chart.Form1.closeLabel.Text = "";
        }
    }

    public class IndicatorBar
    {
        internal readonly IndicatorBuffer buffer;

        internal readonly int y;

        internal readonly int y0;

        internal readonly double value;

        internal IndicatorBar(IndicatorBuffer buffer, int y, int y0, double value)
        {
            this.buffer = buffer;
            this.y = y;
            this.y0 = y0;
            this.value = value;
            HistogramY0 = y0;
        }

        internal IndicatorBar Pair
        {
            get;
            set;
        }

        internal int HistogramY0
        {
            get;
            set;
        }
    }

    public class IndicatorLevelLine : IComparable<IndicatorLevelLine>
    {
        internal readonly IndicatorLevel level;

        internal readonly int y;

        internal readonly double value;

        internal readonly IndicatorDrawer drawer;

        
        internal IndicatorLevelLine(IndicatorLevel level, int y, double value)
        {
            this.level = level;
            this.y = y;
            this.value = value;
            drawer = IndicatorDrawer.Create(level);
        }

        public int CompareTo(IndicatorLevelLine other)
        {
            return y - other.y;
        }
    }
}
