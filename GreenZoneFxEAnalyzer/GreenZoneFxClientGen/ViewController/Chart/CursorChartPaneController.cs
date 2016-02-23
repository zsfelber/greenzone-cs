using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneUtil.ViewController;
using System.Drawing.Drawing2D;

using GreenZoneFxEngine.Util;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class CursorChartPaneController : ClientCursorChartPaneControllerBase
    {

        CursorLineDrawerController cursorChartDrawer;
        bool currentDraggedIsCursor;
        Point lastDragPtToOrigin;

        internal CursorChartPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            SizeChanged += new PropertyChangedEventHandler(CursorChartController_SizeChanged);
        }


        public override ChartDrawerController Drawer
        {
            get
            {
                return cursorChartDrawer;
            }
        }

        public override Font YearFont
        {
            get
            {
                return base.YearFont;
            }
            set
            {
                base.YearFont = value;
                Update();
            }
        }

        public override Font MonthFont
        {
            get
            {
                return base.MonthFont;
            }
            set
            {
                base.MonthFont = value;
                Update();
            }
        }
        
        public override Color GradientColor
        {
            get
            {
                return base.GradientColor;
            }
            set
            {
                base.GradientColor = value;
                UpdateGradient();
            }
        }

        LinearGradientBrush gradientBrush;
        public LinearGradientBrush GradientBrush
        {
            get
            {
                return gradientBrush;
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                UpdateGradient();
            }
        }

        public override Color ChartFrameColor
        {
            get
            {
                return base.ChartFrameColor;
            }
            set
            {
                base.ChartFrameColor = value;
                chartFramePen = new Pen(value);
            }
        }

        Pen chartFramePen;
        public Pen ChartFramePen
        {
            get
            {
                return chartFramePen;
            }
        }

        protected override void LayOut()
        {
            if (Parent != null && Chart != null && Owner != null)
            {
                if (cursorChartDrawer == null)
                {
                    cursorChartDrawer = new CursorLineDrawerController(ForeColor, InactiveColor);
                }
            }

            base.LayOut();

            if (SeriesBars.Count > 0)
            {
                SeriesRange chartSeriesRange = ((IChartOwner)CursorRuntime.Parent).SeriesRange;
                LArr chartTime = CursorRuntime.Parent.sLTime;
                datetime d1 = (datetime)chartTime[chartSeriesRange.OffsetTo];
                datetime d2 = (datetime)chartTime[chartSeriesRange.OffsetFrom];

                SeriesBar b1 = SearchSeriesBar(d1);
                SeriesBar b2 = SearchSeriesBar(d2);

                SeriesRange seriesRange = Parent.SectionRange;
                double p1 = (seriesRange.PriceTo - chartSeriesRange.PriceTo) / seriesRange.PriceRange;
                double dp = (chartSeriesRange.PriceTo - chartSeriesRange.PriceFrom) / seriesRange.PriceRange;
                int h = Height - 3;

                ChartFrameRect = new Rectangle(b1.x1, (int)Math.Round(h * p1), Math.Max(3, b2.x2 - b1.x1), Math.Max(3, (int)Math.Round(h * dp)));

                if (Chart.ChartPanel != null)
                {
                    double cp0Value = (double)Chart.ChartPanel.Chart1.MasterChartSectionPanel.ChartPane.CpBarValue / 1000.0;

                    CpBarRectangle = new Rectangle(ChartFrameRect.X + 1 + (int)Math.Round(cp0Value * (ChartFrameRect.Width - 2)), 0, CpBarRectangle.Width, Height);
                }
            }
        }

        protected override SeriesBar CreateBar(int i)
        {
            INormalSeriesManagerCache seriesCache = CursorRuntime.SeriesCache;
            SeriesRange seriesRange = Parent.SectionRange;

            SeriesBar bar;

            int j = seriesCache.sLTime.Length - 1 - i;
            if (seriesCache.sLTime.StartIndexP <= i && i < seriesCache.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int h = Height - 3;
                double minp = seriesRange.PriceFrom;
                double pw = seriesRange.PriceRange;

                int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;

                datetime time = (datetime)seriesCache.sLTime[i];
                double close = seriesCache.sClose[i];
                double yclose = h - h * (close - minp) / pw;
                int x1 = x0 + (j - 1) * xw / bars;
                int x2 = x0 + j * xw / bars;

                bar = new CursorSeriesBar(x1, x2, 1 + (int)Math.Round(yclose), i, time, close);
            }
            else
            {
                bar = null;
            }
            return bar;
        }

        public override void InvalidateDrawer()
        {
            cursorChartDrawer = null;
        }

        protected override void DrawBackground(ControllerPaintEventArgs e)
        {
            e.Graphics.FillRectangle(gradientBrush, 0, 0, Width, Height);
            e.Graphics.FillRectangle(BgBrush, ChartFrameRect);
        }

        protected override void DrawGrid(ControllerPaintEventArgs e)
        {
            base.DrawGrid(e);

            GraphicsController g = e.Graphics;

            foreach (TimeLabelX l in Chart.TimeLabelXsUpper)
            {
                try
                {
                    if (l.importance == 4)
                    {
                        g.DrawString(l.formattedTime, YearFont, FgBrush, l.screenX + 2, Height / 2 - YearFont.Height / 2);
                    }

                    int m = l.levelTime.Month;
                    if (m % 2 == 0)
                    {
                        g.DrawString(GreenZoneUtilsBase.NumberToRoman(m), MonthFont, FgBrush, l.screenX + 2, Height / 2 + YearFont.Height / 2 + 2);
                    }
                    else
                    {
                        g.DrawString(GreenZoneUtilsBase.NumberToRoman(m), MonthFont, FgBrush, l.screenX + 2, Height / 2 + YearFont.Height / 2 + MonthFont.Height - 2);
                    }
                    
                }
                catch (OverflowException)
                {
                }
            }
        }

        protected override void DrawChart(ControllerPaintEventArgs e)
        {
            datetime from = CursorRuntime.From;
            datetime to = CursorRuntime.To;

            SeriesBar bar;
            bool isActive2;

            Drawer.DrawStarted(e);

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

            Drawer.DrawFinished(e);
        }

        protected override void DrawCursors(ControllerPaintEventArgs e)
        {
            base.DrawCursors(e);

            if (ChartFrameRect != null)
            {
                e.Graphics.DrawRectangle(chartFramePen, ChartFrameRect);
            }
        }

        void paintBar(GraphicsController g, SeriesBar bar, SeriesBar next, bool isActive)
        {
            try
            {
                CursorSeriesBar seriesBar = (CursorSeriesBar)bar;
                cursorChartDrawer.Draw(g, seriesBar.x1, seriesBar.x2, seriesBar.yclose, SelectedSeriesBar == seriesBar, isActive);
            }
            catch (OverflowException)
            {
            }
        }


        void UpdateGradient()
        {
            if (Width != 0 && Height != 0)
            {
                Rectangle rc = new Rectangle(0, 0, Width, Height);
                gradientBrush = new LinearGradientBrush(rc, BackColor, GradientColor, 45f);
            }
        }


        //////////////////////////////////////////////////////////////////////



        protected override void ChartPaneController_MouseDown(object sender, ControllerEventArgs _e)
        {
            base.ChartPaneController_MouseDown(sender, _e);
            ControllerMouseEventArgs e = (ControllerMouseEventArgs)_e;

            lastDragPoint = e.Point;
            currentDraggedIsCursor = ChartFrameRect.Contains(lastDragPoint);
            Point originPoint = ChartFrameRect.Location;
            originPoint.X = CpBarRectangle.X;
            lastDragPtToOrigin = minus(lastDragPoint, originPoint);
        }

        protected override void ChartPaneController_Dragged(object sender, ControllerEventArgs _e)
        {
            if (currentDraggedIsCursor)
            {
                ControllerMouseEventArgs e = (ControllerMouseEventArgs)_e;
                DragChart(e.Point);
            }
        }

        protected override void dragChartTimer_Tick(object sender, EventArgs e)
        {
            if (draggingChart)
            {
                draggingChart = false;
                Point originPoint = minus(lastDragPoint, lastDragPtToOrigin);

                SeriesRange seriesRange = Parent.SectionRange;

                int barIndex = (int)Math.Round((double)(SeriesBars.Count - 1) * ((double)originPoint.X - ChartMinimumX) / (double)ChartEffectiveWidth);
                barIndex = Math.Max(0, barIndex);
                barIndex = Math.Min(SeriesBars.Count - 1, barIndex);
                datetime barTime = SeriesBars[barIndex].time;

                CursorRuntime.ParentScrolledBarTime = barTime;

                if (!CursorRuntime.AutoSeriesRange)
                {
                    SeriesRange chartSeriesRange = ((IChartOwner)CursorRuntime.Parent).SeriesRange;

                    double priceTo = seriesRange.PriceTo - seriesRange.PriceRange * originPoint.Y / (Height - 2);
                    priceTo = Math.Max(seriesRange.PriceFrom + chartSeriesRange.PriceRange, priceTo);
                    priceTo = Math.Min(seriesRange.PriceTo, priceTo);
                    double priceFrom = priceTo - chartSeriesRange.PriceRange;

                    if (Math.Abs(chartSeriesRange.PriceFrom - priceFrom) >= CursorRuntime.Point)
                    {
                        SeriesRange r = Parent.DragYRange;
                        r.PriceFrom = priceFrom;
                        r.PriceTo = priceTo;
                        Parent.DragYRange = r;
                    }
                }

                Chart.UpdateAllChartAndCursor();
            }
        }

        void CursorChartController_SizeChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateGradient();
        }

        Point minus(Point p1, Point p2)
        {
            Point delta = new Point(p1.X - p2.X, p1.Y - p2.Y);
            return delta;
        }
    }


}
