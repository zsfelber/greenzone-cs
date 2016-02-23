using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class OrderChartPaneController : ClientOrderChartPaneControllerBase
    {
        OrderLineDrawerController orderLineDrawer;

        public OrderChartPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        public override ChartDrawerController Drawer
        {
            get
            {
                return orderLineDrawer;
            }
        }

        public override void InvalidateDrawer()
        {
            orderLineDrawer = null;
        }

        protected override SeriesBar CreateBar(int i)
        {
            SeriesRange seriesRange = Parent.SectionRange;

            SeriesBar bar;

            int j = seriesRange.OffsetTo - i + 1;
            if (OrdersView.sLTime.StartIndexP <= i && i < OrdersView.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int h = Height - 2;
                double minp = seriesRange.PriceFrom;
                double pw = seriesRange.PriceRange;

                int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;

                datetime time = (datetime)OrdersView.sLTime[i];
                double balance = OrdersView.BalanceHistAsDArr[i];
                int x1 = x0 + (j - 1) * xw / bars;
                int x2 = x0 + j * xw / bars;
                double ybalance = h - h * (balance - minp) / pw;

                bar = new OrderSeriesBar(x1, x2, (int)Math.Round(ybalance), i, time, balance);
            }
            else
            {
                bar = null;
            }

            return bar;
        }

        protected override void DrawChart(ControllerPaintEventArgs e)
        {
            if (SeriesBars.Count > 0)
            {
                GraphicsController g = e.Graphics;
                datetime from = Owner.From;
                datetime to = Owner.To;

                SeriesBar bar;

                orderLineDrawer.DrawStarted(e);

                bar = SeriesBars[0];
                for (int i = 1; i < SeriesBars.Count; i++)
                {
                    SeriesBar next = SeriesBars[i];
                    if (bar != null)
                    {
                        paintBar(g, bar, next);
                    }

                    bar = next;
                }
                if (bar != null)
                {
                    paintBar(g, bar, null);
                }

                orderLineDrawer.DrawFinished(e);
            }
        }

        void paintBar(GraphicsController g, SeriesBar bar, SeriesBar next)
        {
            try
            {
                OrderSeriesBar seriesBar = (OrderSeriesBar)bar;
                OrderSeriesBar nextBar = (OrderSeriesBar)next;

                // TODO
                bool isOneOfMaxDrawdowns = false;

                orderLineDrawer.Draw(g, seriesBar.x1, seriesBar.x2, seriesBar.ybalance, isOneOfMaxDrawdowns, SelectedSeriesBar == seriesBar);
            }
            catch (OverflowException)
            {
            }
        }

        protected override void DrawIndicators(ControllerPaintEventArgs e)
        {
        }


    }



}
