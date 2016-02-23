using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using System.Drawing;

namespace GreenZoneFxEngine.Gui.Chart
{
    class OrderChartPane : ChartPane
    {
        readonly List<IndicatorLevelLine> levelLines = new List<IndicatorLevelLine>();

        EnvironmentRuntime environment;
        OrderChartSectionPanel parent;
        OrdersHistoryView ordersView;

        OrderLineDrawer orderLineDrawer;

        internal override void Init(Chart chart, ChartSectionPanel parent)
        {
            base.Init(chart, parent);
            this.parent = (OrderChartSectionPanel)parent;
            environment = chart.Environment;
            ordersView = this.parent.OrdersView;
        }

        protected override IChartOwner Owner
        {
            get
            {
                return ordersView;
            }
        }

        public Color DrawdownColor
        {
            get
            {
                return base.AskColor;
            }
            set
            {
                base.AskColor = value;
            }
        }


        internal override void InvalidateDrawer()
        {
            orderLineDrawer = null;
        }

        protected override void LayOut()
        {
            orderLineDrawer = new OrderLineDrawer(ForeColor, DrawdownColor);
            base.LayOut();
        }

        protected override SeriesBar CreateBar(int i)
        {
            SeriesRange seriesRange = parent.SectionRange;

            SeriesBar bar;

            int j = seriesRange.OffsetTo - i + 1;
            if (ordersView.sLTime.StartIndex <= i && i < ordersView.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int h = Height;
                double minp = seriesRange.PriceFrom;
                double pw = seriesRange.PriceRange;

                int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;

                datetime time = (datetime)ordersView.sLTime[i];
                double balance = ordersView.BalanceHistAsDArr[i];
                int x1 = x0 + (j - 1) * xw / bars;
                int x2 = x0 + j * xw / bars;
                double ybalance = h - h * (balance - minp) / pw;

                bar = new OrderSeriesBar(x1, x2, (int)ybalance, i, time, balance);
            }
            else
            {
                bar = null;
            }

            return bar;
        }
        
        protected override void DrawChart(PaintEventArgs e)
        {
            if (seriesBars.Count > 0)
            {
                datetime from = Owner.From;
                datetime to = Owner.To;

                SeriesBar bar;

                orderLineDrawer.DrawStarted(e.Graphics);

                bar = seriesBars[0];
                for (int i = 1; i < seriesBars.Count; i++)
                {
                    SeriesBar next = seriesBars[i];
                    if (bar != null)
                    {
                        paintBar(e.Graphics, bar, next);
                    }

                    bar = next;
                }
                if (bar != null)
                {
                    paintBar(e.Graphics, bar, null);
                }

                orderLineDrawer.DrawFinished(e.Graphics);
            }
        }

        void paintBar(Graphics g, SeriesBar bar, SeriesBar next)
        {
            try
            {
                OrderSeriesBar seriesBar = (OrderSeriesBar)bar;
                OrderSeriesBar nextBar = (OrderSeriesBar)next;

                // TODO
                bool isOneOfMaxDrawdowns = false;

                orderLineDrawer.Draw(g, seriesBar.x1, seriesBar.x2, seriesBar.ybalance, isOneOfMaxDrawdowns, selectedSeriesBar == seriesBar);
            }
            catch (OverflowException)
            {
            }
        }

        protected override void DrawIndicators(PaintEventArgs e)
        {
        }

        protected override void DrawLevels(PaintEventArgs e)
        {
        }
    }
}
