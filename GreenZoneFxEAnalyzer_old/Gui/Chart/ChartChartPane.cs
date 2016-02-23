using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.ComponentModel;
using GreenZoneFxEngine.Types;
using System.Drawing.Drawing2D;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Properties;
using GreenZoneFxEngine.Util;

namespace GreenZoneFxEngine.Gui.Chart
{
    abstract class ChartChartPane : ChartPane
    {
        Chart chart;
        ChartSectionPanel parent;

        protected readonly Dictionary<IndicatorBuffer, IndicatorDrawer> indicatorDrawers = new Dictionary<IndicatorBuffer, IndicatorDrawer>();
        protected readonly List<SeriesBar> seriesBarsTo = new List<SeriesBar>();
        protected readonly List<SeriesBar> seriesBarsFrom = new List<SeriesBar>();

        bool sectionOrZigzag;

        internal override void Init(Chart chart, ChartSectionPanel parent)
        {
            this.chart = chart;
            this.parent = parent;
            base.Init(chart, parent);
        }

        protected override void LayOut()
        {
            if (parent != null && chart != null && Owner != null)
            {
                List<IndicatorBuffer> indicatorBuffersTo = new List<IndicatorBuffer>();
                List<IndicatorBuffer> indicatorBuffersFrom = new List<IndicatorBuffer>();

                IChartOwner owner = Owner;
                SeriesRange seriesRange = parent.SectionRange;

                sectionOrZigzag = false;
                seriesBarsTo.Clear();
                seriesBarsFrom.Clear();

                foreach (var b in indicatorDrawers)
                {
                    if (b.Value != null)
                    {
                        if (b.Key.StyleType == DrawingStyle.DRAW_SECTION || b.Key.StyleType == DrawingStyle.DRAW_ZIGZAG)
                        {
                            sectionOrZigzag = true;
                            indicatorBuffersTo.Add(b.Key);
                            indicatorBuffersFrom.Add(b.Key);
                        }
                    }
                }

                for (int i = seriesRange.OffsetTo + 1; i < owner.sLTime.Length && indicatorBuffersTo.Count > 0; i++)
                {
                    add_bar(seriesBarsTo, indicatorBuffersTo, i);
                }
                seriesBarsTo.Reverse();

                for (int i = seriesRange.OffsetFrom - 1; i >= owner.sLTime.StartIndex && indicatorBuffersFrom.Count > 0; i--)
                {
                    add_bar(seriesBarsFrom, indicatorBuffersFrom, i);
                }
            }

            base.LayOut();
        }

        protected override void add_bar(List<SeriesBar> seriesBars, int i)
        {
            add_bar(seriesBars, null, i);
        }

        void add_bar(List<SeriesBar> seriesBars, List<IndicatorBuffer> indicatorBuffersUntilFirstNonEmpty, int i)
        {
            SeriesRange seriesRange = parent.SectionRange;

            int h = Height;
            double minp = seriesRange.PriceFrom;
            int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;
            double pw = seriesRange.PriceRange;

            SeriesBar bar = CreateBar(i);
            if (bar != null)
            {
                seriesBars.Add(bar);

                Dictionary<IndicatorDrawer, IndicatorBar> map = new Dictionary<IndicatorDrawer, IndicatorBar>();
                foreach (var b in indicatorDrawers)
                {
                    if (b.Value != null)
                    {
                        double value = b.Key.SBuffer[i];

                        if (indicatorBuffersUntilFirstNonEmpty == null || indicatorBuffersUntilFirstNonEmpty.Contains(b.Key))
                        {
                            double y = h - h * (value - minp) / pw;
                            double y0 = Math.Min(h, Math.Max(0, h - h * (-minp) / pw));
                            IndicatorBar ibar = new IndicatorBar(b.Key, (int)y, (int)y0, value);
                            bar.indicatorBars.Add(ibar);
                            map[b.Value] = ibar;

                            if (b.Value.Pair != null)
                            {
                                IndicatorBar ibar2;
                                if (map.TryGetValue(b.Value.Pair, out ibar2))
                                {
                                    ibar.Pair = ibar2;
                                    ibar2.Pair = ibar;
                                }
                            }
                        }
                        else
                        {
                            bar.indicatorBars.Add(null);
                        }

                        if (indicatorBuffersUntilFirstNonEmpty != null && value != b.Key.EmptyValue)
                        {
                            indicatorBuffersUntilFirstNonEmpty.Remove(b.Key);
                        }
                    }
                }
            }
        }


        protected override void DrawIndicators(PaintEventArgs e)
        {
            foreach (IndicatorDrawer indicatorDrawer in indicatorDrawers.Values)
            {
                if (indicatorDrawer != null)
                {
                    try
                    {
                        indicatorDrawer.DrawStarted(e.Graphics);
                    }
                    catch (OverflowException)
                    {
                    }
                }
            }

            SeriesBar bar;
            bool isActive2;

            datetime from = Owner.From;
            datetime to = Owner.To;

            List<SeriesBar> seriesBarsAll = new List<SeriesBar>();
            seriesBarsAll.AddRange(seriesBarsTo);
            seriesBarsAll.AddRange(seriesBars);
            seriesBarsAll.AddRange(seriesBarsFrom);

            if (seriesBarsAll.Count > 0)
            {
                bar = seriesBarsAll[0];
                for (int i = 1; i < seriesBarsAll.Count; i++)
                {
                    SeriesBar next = seriesBarsAll[i];
                    if (bar != null)
                    {
                        bool isActive = from <= bar.time && bar.time <= to;

                        int j = 0;
                        foreach (IndicatorBar ibar in bar.indicatorBars)
                        {
                            if (ibar != null && ibar.value != ibar.buffer.EmptyValue)
                            {
                                IndicatorBar nexti = next.indicatorBars[j];
                                IndicatorDrawer indicatorDrawer = indicatorDrawers[ibar.buffer];

                                paintIBar(e.Graphics, indicatorDrawer, bar, ibar, nexti);
                            }
                            j++;
                        }

                        bar = next;
                    }
                }
                if (bar != null)
                {
                    isActive2 = from <= bar.time && bar.time <= to;
                    foreach (IndicatorBar ibar in bar.indicatorBars)
                    {
                        if (ibar != null && ibar.value != ibar.buffer.EmptyValue)
                        {
                            IndicatorDrawer indicatorDrawer = indicatorDrawers[ibar.buffer];
                            paintIBar(e.Graphics, indicatorDrawer, bar, ibar, null);
                        }
                    }
                }
            }

            foreach (IndicatorDrawer indicatorDrawer in indicatorDrawers.Values)
            {
                if (indicatorDrawer != null)
                {
                    try
                    {
                        indicatorDrawer.DrawFinished(e.Graphics);
                    }
                    catch (OverflowException)
                    {
                    }
                }
            }

            if (sectionOrZigzag)
            {
                int x1 = seriesBars[0].x1;
                e.Graphics.FillRectangle(bgBrush, -1, 0, x1 + 1, Height);

                int x2 = seriesBars[seriesBars.Count - 1].x2;
                e.Graphics.FillRectangle(bgBrush, x2, 0, Width - x2 + 1, Height);
            }
        }

        
        void paintIBar(Graphics g, IndicatorDrawer indicatorDrawer, SeriesBar bar, IndicatorBar ibar, IndicatorBar nexti)
        {
            bool shouldPaint = true;
            if (indicatorDrawer.DrawingStyle == DrawingStyle.DRAW_HISTOGRAM)
            {
                if (indicatorDrawer.Pair != null)
                {
                    if (indicatorDrawer.Pair.DrawingStyle == DrawingStyle.DRAW_HISTOGRAM)
                    {
                        if (ibar.value > ibar.Pair.value)
                        {
                            ibar.HistogramY0 = ibar.Pair.y;
                        }
                        else
                        {
                            shouldPaint = false;
                        }
                    }
                    else
                    {
                        ibar.HistogramY0 = ibar.Pair.y;
                    }
                }
            }

            if (shouldPaint)
            {
                try
                {
                    indicatorDrawer.Draw(g, bar.x1, bar.x2, ibar.HistogramY0, ibar.y, nexti == null || nexti.value == ibar.buffer.EmptyValue ? 0 : nexti.y);
                }
                catch (OverflowException)
                {
                }
            }
        }

    }
}
