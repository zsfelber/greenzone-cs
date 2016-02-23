using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine.ViewController.Chart
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

        public static bool operator ==(Range r1, Range r2)
        {
            return r1.From == r2.From && r1.To == r2.To;
        }

        public static bool operator !=(Range r1, Range r2)
        {
            return r1.From != r2.From || r1.To != r2.To;
        }

        public override bool Equals(object obj)
        {
            return this==(Range)obj;
        }

        public override int GetHashCode()
        {
            return From + To;
        }
    }

    public class SeriesBar
    {
        public readonly int x1, x2;

        public readonly int seriesIndex;

        public readonly datetime time;

        public readonly List<IndicatorBar> indicatorBars = new List<IndicatorBar>();

        public SeriesBar(int x1, int x2, int seriesIndex, datetime time)
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

        public virtual int CenterY
        {
            get
            {
                return 0;
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

    }


    public class IndicatorBar
    {
        public readonly IIndicatorRuntime indicator;

        public readonly IndicatorBuffer buffer;

        public readonly int y;

        public readonly int y0;

        public readonly double value;

        public IndicatorBar(IIndicatorRuntime indicator, IndicatorBuffer buffer, int y, int y0, double value)
        {
            this.indicator = indicator;
            this.buffer = buffer;
            this.y = y;
            this.y0 = y0;
            this.value = value;
            HistogramY0 = y0;
        }

        public IndicatorBar Pair
        {
            get;
            set;
        }

        public int HistogramY0
        {
            get;
            set;
        }
    }



    public struct PriceLabelY
    {
        public readonly double levelPrice;
        public readonly int screenY;
        public readonly int importance;

        public PriceLabelY(double levelPrice, int screenY, int importance)
        {
            this.levelPrice = levelPrice;
            this.screenY = screenY;
            this.importance = importance;
        }

        public override bool Equals(object obj)
        {
            PriceLabelY o = (PriceLabelY)obj;
            return levelPrice == o.levelPrice && screenY == o.screenY && importance == o.importance;
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
        public readonly datetime levelTime;
        public readonly string formattedTime;
        public readonly int screenX;
        public readonly int importance;

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
}
