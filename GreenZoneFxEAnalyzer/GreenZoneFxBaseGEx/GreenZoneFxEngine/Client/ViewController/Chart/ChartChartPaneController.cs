using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ClientChartChartPaneControllerEx : ClientChartChartPaneControllerBase
    {

        public ClientChartChartPaneControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            SeriesBarsFrom = new List<SeriesBar>();
            SeriesBarsTo = new List<SeriesBar>();
        }


        public ClientChartChartPaneControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            SeriesBarsFrom = new List<SeriesBar>();
            SeriesBarsTo = new List<SeriesBar>();
            indicatorDrawersUm = new ReadOnlyDictionary<IndicatorBuffer, IndicatorDrawerController>(indicatorDrawers);
        }

        protected ClientChartChartPaneControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SeriesBarsFrom = new List<SeriesBar>();
            SeriesBarsTo = new List<SeriesBar>();
        }

        protected override void LayOut()
        {
            if (Parent != null && Chart != null && Owner != null)
            {
                List<IndicatorBuffer> indicatorBuffersTo = new List<IndicatorBuffer>();
                List<IndicatorBuffer> indicatorBuffersFrom = new List<IndicatorBuffer>();

                IChartOwner owner = Owner;
                SeriesRange seriesRange = Parent.SectionRange;

                SectionOrZigZag = false;
                SeriesBarsTo.Clear();
                SeriesBarsFrom.Clear();

                foreach (var b in indicatorDrawers)
                {
                    if (b.Value != null)
                    {
                        if (b.Key.StyleType == DrawingStyle.DRAW_SECTION || b.Key.StyleType == DrawingStyle.DRAW_ZIGZAG)
                        {
                            SectionOrZigZag = true;
                            indicatorBuffersTo.Add(b.Key);
                            indicatorBuffersFrom.Add(b.Key);
                        }
                    }
                }

                for (int i = seriesRange.OffsetTo + 1; i < owner.sLTime.Length && indicatorBuffersTo.Count > 0; i++)
                {
                    add_bar(SeriesBarsTo, indicatorBuffersTo, i);
                }
                SeriesBarsTo.Reverse();

                for (int i = seriesRange.OffsetFrom - 1; i >= owner.sLTime.StartIndexP && indicatorBuffersFrom.Count > 0; i--)
                {
                    add_bar(SeriesBarsFrom, indicatorBuffersFrom, i);
                }
            }

            base.LayOut();
        }

        protected override void add_bar(List<SeriesBar> SeriesBars, int i)
        {
            add_bar(SeriesBars, null, i);
        }

        void add_bar(List<SeriesBar> SeriesBars, List<IndicatorBuffer> indicatorBuffersUntilFirstNonEmpty, int i)
        {
            SeriesRange seriesRange = Parent.SectionRange;

            int h = Height - 2;
            double minp = seriesRange.PriceFrom;
            int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;
            double pw = seriesRange.PriceRange;

            SeriesBar bar = CreateBar(i);
            if (bar != null)
            {
                SeriesBars.Add(bar);

                Dictionary<IndicatorDrawerController, IndicatorBar> map = new Dictionary<IndicatorDrawerController, IndicatorBar>();
                foreach (var b in indicatorDrawers)
                {
                    if (b.Value != null)
                    {
                        double value = b.Key.SBuffer[i];

                        if (indicatorBuffersUntilFirstNonEmpty == null || indicatorBuffersUntilFirstNonEmpty.Contains(b.Key))
                        {
                            double y = h - h * (value - minp) / pw;
                            double y0 = Math.Min(h, Math.Max(0, h - h * (-minp) / pw));
                            IndicatorBar ibar = new IndicatorBar(b.Value.indicator, b.Key, (int)y, (int)y0, value);
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

        protected readonly Dictionary<IndicatorBuffer, IndicatorDrawerController> indicatorDrawers = new Dictionary<IndicatorBuffer, IndicatorDrawerController>();
        readonly IDictionary<IndicatorBuffer, IndicatorDrawerController> indicatorDrawersUm;
        public IDictionary<IndicatorBuffer, IndicatorDrawerController> IndicatorDrawers
        {
            get
            {
                return indicatorDrawersUm;
            }
        }

        protected override void DrawChart(ControllerPaintEventArgs e)
        {
            datetime from = Owner.From;
            datetime to = Owner.To;

            SeriesBar bar;
            bool isActive2;

            ChartDrawerController drawer = Drawer;
            if (drawer != null)
            {
                drawer.DrawStarted(e);
            }

            bar = SeriesBars[0];
            for (int i = 1; i < SeriesBars.Count; i++)
            {
                SeriesBar next = SeriesBars[i];
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

            if (drawer != null)
            {
                drawer.DrawFinished(e);
            }
        }

        protected abstract void paintBar(GraphicsController g, SeriesBar bar, SeriesBar next, bool isActive);

        protected override void DrawIndicators(ControllerPaintEventArgs e)
        {
            GraphicsController g = e.Graphics;

            foreach (IndicatorDrawerController indicatorDrawer in indicatorDrawers.Values)
            {
                if (indicatorDrawer != null)
                {
                    try
                    {
                        indicatorDrawer.DrawStarted(e);
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

            List<SeriesBar> SeriesBarsAll = new List<SeriesBar>();
            SeriesBarsAll.AddRange(SeriesBarsTo);
            SeriesBarsAll.AddRange(SeriesBars);
            SeriesBarsAll.AddRange(SeriesBarsFrom);

            if (SeriesBarsAll.Count > 0)
            {
                bar = SeriesBarsAll[0];
                for (int i = 1; i < SeriesBarsAll.Count; i++)
                {
                    SeriesBar next = SeriesBarsAll[i];
                    if (bar != null)
                    {
                        bool isActive = from <= bar.time && bar.time <= to;

                        int j = 0;
                        foreach (IndicatorBar ibar in bar.indicatorBars)
                        {
                            if (ibar != null && ibar.value != ibar.buffer.EmptyValue)
                            {
                                IndicatorBar nexti = next.indicatorBars[j];
                                IndicatorDrawerController indicatorDrawer = indicatorDrawers[ibar.buffer];

                                paintIBar(g, indicatorDrawer, bar, ibar, nexti);
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
                            IndicatorDrawerController indicatorDrawer = indicatorDrawers[ibar.buffer];
                            paintIBar(g, indicatorDrawer, bar, ibar, null);
                        }
                    }
                }
            }

            foreach (IndicatorDrawerController indicatorDrawer in indicatorDrawers.Values)
            {
                if (indicatorDrawer != null)
                {
                    try
                    {
                        indicatorDrawer.DrawFinished(e);
                    }
                    catch (OverflowException)
                    {
                    }
                }
            }

            if (SectionOrZigZag)
            {
                int x1 = SeriesBars[0].x1;
                g.FillRectangle(BgBrush, -1, 0, x1 + 1, Height);

                int x2 = SeriesBars[SeriesBars.Count - 1].x2;
                g.FillRectangle(BgBrush, x2, 0, Width - x2 + 1, Height);
            }
        }


        void paintIBar(GraphicsController g, IndicatorDrawerController indicatorDrawer, SeriesBar bar, IndicatorBar ibar, IndicatorBar nexti)
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
                    indicatorDrawer.Draw(g, bar.x1, bar.x2, ibar.HistogramY0, ibar.y, nexti == null || nexti.value == ibar.buffer.EmptyValue ? 0 : nexti.y, SelectedIndicatorBar==ibar);
                }
                catch (OverflowException)
                {
                }
            }
        }
    }


}
