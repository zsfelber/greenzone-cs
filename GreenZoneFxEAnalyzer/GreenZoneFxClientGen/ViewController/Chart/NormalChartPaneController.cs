using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class NormalChartPaneController : ClientNormalChartPaneControllerBase
    {
        
        SeriesChartDrawerController seriesChartDrawer;

        public NormalChartPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
		}

        public override ChartDrawerController Drawer
        {
            get
            {
                return seriesChartDrawer;
            }
        }

        protected override void LayOut()
        {
            if (Parent != null && Chart != null && Owner != null)
            {
                if (seriesChartDrawer == null || seriesChartDrawer.ChartType != ChartRuntime.Session.ChartType)
                {
                    switch (ChartRuntime.Session.ChartType) {
                        case ChartType.CANDLESTICKS:
                            seriesChartDrawer = new CandlestickDrawerController(ForeColor, BackColor, InactiveColor);
                            break;
                        case ChartType.LINE:
                            seriesChartDrawer = new LineDrawerController(ForeColor, AskColor, InactiveColor);
                            break;
                        case ChartType.OHLC:
                            seriesChartDrawer = new OHLCDrawerController(ForeColor, InactiveColor);
                            break;
                    }
                }

                SeriesRange seriesRange = Parent.SectionRange;

                indicatorDrawers.Clear();
                bool odd = true;
                IndicatorDrawerController pdrawer = null;
                foreach (IndicatorRuntime ind in ChartRuntime.GuiSeriesManager.DefaultCache.Indicators.Values)
                {
                    if (ind.Visible && ind.Session.WindowType == IndicatorWindowType.CHART_WINDOW)
                    {
                        foreach (IndicatorBuffer b in ind.Buffers)
                        {
                            IndicatorDrawerController drawer;
                            if (b.Buffer != null)
                            {
                                drawer = IndicatorDrawerController.Create(ind, b);
                                indicatorDrawers[b] = drawer;
                                if (!odd &&
                                    drawer != null &&
                                    pdrawer != null &&
                                    (drawer.DrawingStyle == DrawingStyle.DRAW_HISTOGRAM ||
                                     pdrawer.DrawingStyle == DrawingStyle.DRAW_HISTOGRAM))
                                {
                                    pdrawer.Pair = drawer;
                                    drawer.Pair = pdrawer;
                                }
                            }
                            else
                            {
                                drawer = null;
                            }
                            pdrawer = drawer;
                            odd = !odd;
                        }
                        pdrawer = null;
                        odd = true;
                    }
                }
            }


            base.LayOut();
        }

        protected override SeriesBar CreateBar(int i)
        {
            ISeriesManagerCache seriesCache = ChartRuntime.SeriesCache;
            SeriesRange seriesRange = Parent.SectionRange;
            SeriesBar bar;

            int j = seriesRange.OffsetTo - i + 1;
            if (seriesCache.sLTime.StartIndexP <= i && i < seriesCache.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int h = Height - 2;
                double minp = seriesRange.PriceFrom;
                double pw = seriesRange.PriceRange;

                int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;

                if (ChartRuntime.Period.GetCategory() == TimePeriodCategory.TICKS)
                {
                    datetime time = (datetime)seriesCache.sLTime[i];
                    double ask = seriesCache.sAsks[i];
                    double bid = seriesCache.sBids[i];
                    double yask = h - h * (ask - minp) / pw;
                    double ybid = h - h * (bid - minp) / pw;
                    int x1 = x0 + (j - 1) * xw / bars;
                    int x2 = x0 + j * xw / bars;

                    bar = new TickSeriesBar(x1, x2, (int)Math.Round(yask), (int)Math.Round(ybid), i,
                                            time, ask, bid);
                }
                else
                {
                    datetime time = (datetime)seriesCache.sLTime[i];
                    double open = seriesCache.sOpen[i];
                    double low = seriesCache.sLow[i];
                    double high = seriesCache.sHigh[i];
                    double close = seriesCache.sClose[i];
                    double yo = h - h * (open - minp) / pw;
                    double yl = h - h * (low - minp) / pw;
                    double yh = h - h * (high - minp) / pw;
                    double yc = h - h * (close - minp) / pw;
                    int x1 = x0 + (j - 1) * xw / bars;
                    int x2 = x0 + j * xw / bars;

                    bar = new PeriodSeriesBar(x1, x2, (int)Math.Round(yo), (int)Math.Round(yl), (int)Math.Round(yh), (int)Math.Round(yc), i,
                                              time, open, low, high, close);
                }
            }
            else
            {
                bar = null;
            }
            return bar;
        }

        public override void InvalidateDrawer()
        {
            seriesChartDrawer = null;
        }

        protected override void paintBar(GraphicsController g, SeriesBar bar, SeriesBar next, bool isActive)
        {
            try
            {
                if (ChartRuntime.Period.GetCategory() == TimePeriodCategory.TICKS)
                {
                    TickSeriesBar seriesBar = (TickSeriesBar)bar;
                    TickSeriesBar nextBar = (TickSeriesBar)next;
                    seriesChartDrawer.DrawTick(g, seriesBar.x1, seriesBar.x2, seriesBar.ybid, seriesBar.yask, nextBar == null ? 0 : nextBar.ybid, nextBar == null ? 0 : nextBar.yask, isActive, SelectedSeriesBar == seriesBar);
                }
                else
                {
                    PeriodSeriesBar seriesBar = (PeriodSeriesBar)bar;
                    PeriodSeriesBar nextBar = (PeriodSeriesBar)next;
                    seriesChartDrawer.Draw(g, seriesBar.x1, seriesBar.x2, seriesBar.yo, seriesBar.yl, seriesBar.yh, seriesBar.yc, nextBar == null ? 0 : nextBar.yc, isActive, SelectedSeriesBar == seriesBar);
                }
            }
            catch (OverflowException)
            {
            }
        }

    }


}
