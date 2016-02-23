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
    class NormalChartPane : ChartChartPane
    {
        NormalChart chart;
        ChartRuntime chartRuntime;
        NormalChartSectionPanel parent;

        SeriesChartDrawer seriesChartDrawer;

        internal override void Init(Chart chart, ChartSectionPanel parent)
        {
            base.Init(chart, parent);
            this.chart = (NormalChart)chart;
            this.parent = (NormalChartSectionPanel)parent;
            chartRuntime = this.chart.ChartRuntime;
        }

        protected override IChartOwner Owner
        {
            get
            {
                return chart==null?null:chart.ChartRuntime;
            }
        }

        internal override void InvalidateDrawer()
        {
            seriesChartDrawer = null;
        }

        protected override void LayOut()
        {
            if (parent != null && chart != null && Owner != null)
            {
                SeriesRange seriesRange = parent.SectionRange;

                switch (chart.ChartRuntime.Session.ChartType)
                {
                    case ChartType.CANDLESTICKS:
                        if (seriesChartDrawer == null || seriesChartDrawer.ChartType != ChartType.CANDLESTICKS)
                        {
                            seriesChartDrawer = new CandlestickDrawer(ForeColor, BackColor, InactiveColor);
                        }
                        break;
                    case ChartType.OHLC:
                        if (seriesChartDrawer == null || seriesChartDrawer.ChartType != ChartType.OHLC)
                        {
                            seriesChartDrawer = new OHLCDrawer(ForeColor, InactiveColor);
                        }
                        break;
                    case ChartType.LINE:
                        if (seriesChartDrawer == null || seriesChartDrawer.ChartType != ChartType.LINE)
                        {
                            seriesChartDrawer = new LineDrawer(ForeColor, AskColor, InactiveColor);
                        }
                        break;
                }

                indicatorDrawers.Clear();
                bool odd = true;
                IndicatorDrawer pdrawer = null;
                foreach (IndicatorRuntime ind in chart.ChartRuntime.GuiSeriesManager.DefaultCache)
                {
                    if (ind.Visible && ind.Session.WindowType == IndicatorWindowType.CHART_WINDOW)
                    {
                        foreach (IndicatorBuffer b in ind.Buffers)
                        {
                            IndicatorDrawer drawer;
                            if (b.Buffer != null)
                            {
                                drawer = IndicatorDrawer.Create(b);
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
            SeriesManagerCache seriesCache = chart.ChartRuntime.SeriesCache;
            SeriesRange seriesRange = parent.SectionRange;
            SeriesBar bar;

            int j = seriesRange.OffsetTo - i + 1;
            if (seriesCache.sLTime.StartIndex <= i && i < seriesCache.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int h = Height;
                double minp = seriesRange.PriceFrom;
                double pw = seriesRange.PriceRange;

                int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;

                if (chart.ChartRuntime.Period.GetCategory() == TimePeriodCategory.TICKS)
                {
                    datetime time = (datetime)seriesCache.sLTime[i];
                    double ask = seriesCache.sAsks[i];
                    double bid = seriesCache.sBids[i];
                    double yask = h - h * (ask - minp) / pw;
                    double ybid = h - h * (bid - minp) / pw;
                    int x1 = x0 + (j - 1) * xw / bars;
                    int x2 = x0 + j * xw / bars;

                    bar = new TickSeriesBar(x1, x2, (int)yask, (int)ybid, i,
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

                    bar = new PeriodSeriesBar(x1, x2, (int)yo, (int)yl, (int)yh, (int)yc, i,
                                              time, open, low, high, close);
                }
            }
            else
            {
                bar = null;
            }
            return bar;
        }

        protected override void DrawChart(PaintEventArgs e)
        {
            datetime from = Owner.From;
            datetime to = Owner.To;

            SeriesBar bar;
            bool isActive2;

            seriesChartDrawer.DrawStarted(e.Graphics);

            bar = seriesBars[0];
            for (int i = 1; i < seriesBars.Count; i++)
            {
                SeriesBar next = seriesBars[i];
                if (bar != null)
                {
                    bool isActive = from <= bar.time && bar.time <= to;
                    paintBar(e.Graphics, bar, next, isActive);
                }

                bar = next;
            }
            if (bar != null)
            {
                isActive2 = from <= bar.time && bar.time <= to;
                paintBar(e.Graphics, bar, null, isActive2);
            }

            seriesChartDrawer.DrawFinished(e.Graphics);
        }

        void paintBar(Graphics g, SeriesBar bar, SeriesBar next, bool isActive)
        {
            try
            {
                if (chart.ChartRuntime.Period.GetCategory() == TimePeriodCategory.TICKS)
                {
                    TickSeriesBar seriesBar = (TickSeriesBar)bar;
                    TickSeriesBar nextBar = (TickSeriesBar)next;
                    seriesChartDrawer.DrawTick(g, seriesBar.x1, seriesBar.x2, seriesBar.ybid, seriesBar.yask, nextBar == null ? 0 : nextBar.ybid, nextBar == null ? 0 : nextBar.yask, isActive, selectedSeriesBar == seriesBar);
                }
                else
                {
                    PeriodSeriesBar seriesBar = (PeriodSeriesBar)bar;
                    PeriodSeriesBar nextBar = (PeriodSeriesBar)next;
                    seriesChartDrawer.Draw(g, seriesBar.x1, seriesBar.x2, seriesBar.yo, seriesBar.yl, seriesBar.yh, seriesBar.yc, nextBar == null ? 0 : nextBar.yc, isActive, selectedSeriesBar == seriesBar);
                }
            }
            catch (OverflowException)
            {
            }
        }

        protected override void DrawLevels(PaintEventArgs e)
        {
        }
    }
}
