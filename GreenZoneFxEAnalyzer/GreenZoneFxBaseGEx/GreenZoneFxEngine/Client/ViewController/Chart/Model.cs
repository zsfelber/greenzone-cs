using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine.ViewController.Chart
{

    public class PeriodSeriesBar : SeriesBar
    {
        public readonly int yo, yl, yh, yc;

        public readonly double open, low, high, close;

        public PeriodSeriesBar(int x1, int x2, int yo, int yl, int yh, int yc, int seriesIndex,
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

        public override int CenterY
        {
            get
            {
                return (yl + yh) / 2;
            }
        }

        public bool Contains(Point p)
        {
            return x1 <= p.X && p.X <= x2 && yh <= p.Y && p.Y <= yl;
        }
    }

    public class TickSeriesBar : SeriesBar
    {
        public readonly int yask, ybid;

        public readonly double ask, bid;

        public TickSeriesBar(int x1, int x2, int yask, int ybid, int seriesIndex,
                           datetime time, double ask, double bid)
            : base(x1, x2, seriesIndex, time)
        {
            this.yask = yask;
            this.ybid = ybid;
            this.ask = ask;
            this.bid = bid;
        }

        public override int CenterY
        {
            get
            {
                return (yask + ybid) / 2;
            }
        }

        public bool Contains(Point p)
        {
            return x1 <= p.X && p.X <= x2 && yask <= p.Y && p.Y <= ybid;
        }
    }

    public class OrderSeriesBar : SeriesBar
    {
        public readonly int ybalance;

        public readonly double balance;

        public OrderSeriesBar(int x1, int x2, int ybalance, int seriesIndex,
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
    }

    public class CursorSeriesBar : SeriesBar
    {
        public readonly int yclose;

        public readonly double close;

        public CursorSeriesBar(int x1, int x2, int yclose, int seriesIndex,
                           datetime time, double close)
            : base(x1, x2, seriesIndex, time)
        {
            this.yclose = yclose;
            this.close = close;
        }

        public bool Contains(Point p)
        {
            return x1 <= p.X && p.X <= x2;
        }
    }

    public class IndicatorLevelLine : IComparable<IndicatorLevelLine>
    {
        public readonly IndicatorLevel level;

        public readonly int y;

        public readonly double value;

        public readonly IndicatorDrawerController drawer;

        public IndicatorLevelLine(IIndicatorRuntime indicator, IndicatorLevel level, int y, double value)
        {
            this.level = level;
            this.y = y;
            this.value = value;
            drawer = IndicatorDrawerController.Create(indicator, level);
        }

        public int CompareTo(IndicatorLevelLine other)
        {
            return y - other.y;
        }
    }

}
