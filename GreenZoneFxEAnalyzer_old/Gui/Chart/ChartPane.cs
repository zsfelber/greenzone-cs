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
    abstract class ChartPane : Panel
    {
        enum DraggingState
        {
            INACTIVE,
            SLIDER_OVER,
            SLIDER_DRAGGED,
            SLIDER_PRESSED,
            CHART_DRAGGED,
            CHART_PRESSED
        }

        public event PropertyChangedEventHandler SliderValueChanged;
        public event PropertyChangedEventHandler SliderValueDragged;

        Chart chart;
        ChartSectionPanel parent;

        bool calcAutoGap = false;
        int leftGap = 0;
        int rightGap = 0;
        Color askColor = Color.Red;
        Color inactiveColor = Color.Gray;
        protected Brush bgBrush;
        protected Brush fgBrush;
        Brush inactiveBrush;
        Pen inactivePen;
        Color gridColor = Color.Green;
        internal Pen[] gridPens;

        Color sliderBarColor;
        Range sliderMinMax = new Range(0, 100);
        int sliderValue = 50;
        bool sliderThumbVisible = true;
        bool cpBarVisible;
        int cpBarValue;

        TrackBarThumbState thumbState = TrackBarThumbState.Normal;
        Rectangle thumbRectangle = new Rectangle();
        Rectangle barRectangle = new Rectangle();
        Rectangle cpBarRectangle = new Rectangle();
        protected readonly List<SeriesBar> seriesBars = new List<SeriesBar>();
        protected readonly IList<SeriesBar> seriesBarsUm;
        protected SeriesBar selectedSeriesBar = null;
        bool thumbInitiliazed = false;
        Brush barBrush;
        Pen barPen;

        int chartDragStartX;
        int chartDragStartY;

        public ChartPane()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);

            seriesBarsUm = seriesBars.AsReadOnly();
            SliderBarColor = Color.Gray;
            base.ForeColor = Color.Black;
            cpBarVisible = false;

            MouseDown += new MouseEventHandler(CursorBar_MouseDown);
            MouseUp += new MouseEventHandler(CursorBar_MouseUp);
            MouseMove += new MouseEventHandler(CursorBar_MouseMove);
            MouseLeave += new EventHandler(CursorBar_MouseLeave);
            Layout += new LayoutEventHandler(ChartPane_Layout);
            ParentChanged += new EventHandler(ChartPane_ParentChanged);

            DragState = DraggingState.INACTIVE;
            leftGap = 0;
            rightGap = 0;

            barRectangle.Width = 4;
            cpBarRectangle.Width = 1;
        }

        internal virtual void Init(Chart chart, ChartSectionPanel parent)
        {
            this.chart = chart;
            this.parent = parent;
        }

        DraggingState DragState
        {
            get;
            set;
        }

        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                InvalidateDrawer();
                bgBrush = new SolidBrush(value);
                Invalidate();
                Update();
            }
        }

        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                InvalidateDrawer();
                fgBrush = new SolidBrush(value);
                Invalidate();
                Update();
            }
        }

        [Category("Chart pane")]
        public Color AskColor
        {
            get { return askColor; }
            set
            {
                askColor = value;
                InvalidateDrawer();
                Invalidate();
                Update();
            }
        }

        [Category("Chart pane")]
        public Color InactiveColor
        {
            get { return inactiveColor; }
            set
            {
                inactiveColor = value;
                inactiveBrush = new SolidBrush(inactiveColor);
                inactivePen = new Pen(inactiveBrush);
                InvalidateDrawer();
                Invalidate();
                Update();
            }
        }

        [Category("Chart pane")]
        public Color GridColor
        {
            get { return gridColor; }
            set
            {
                gridColor = value;

                gridPens = new Pen[7];
                for (int i = 0; i < 6; i++)
                {
                    Pen gridPen = new Pen(Color.FromArgb(30 + i * 225 / 5, gridColor));

                    List<float> ptrn = new List<float>();
                    ptrn.Add(i / 3 + 1);
                    ptrn.Add(6 - i);
                    gridPen.DashPattern = ptrn.ToArray();
                    //gridPen.Width = 4f * (i + 1) / 6f;

                    gridPens[i] = gridPen;
                }
                gridPens[6] = new Pen(gridPens[5].Color);

                InvalidateDrawer();
                Invalidate();
                Update();
            }
        }

        Font levelFont;
        public Font LevelFont
        {
            get
            {
                return levelFont;
            }
            set
            {
                levelFont = value;
                Update();
            }
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                Update();
            }
        }


        [Category("Chart pane")]
        public bool ChartCalcAutoGap
        {
            get { return calcAutoGap; }
            set
            {
                calcAutoGap = value;
                CheckRange();
            }
        }

        [Category("Chart pane")]
        public int ChartLeftGap
        {
            get { return leftGap; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value: " + value + "  <0");
                }
                leftGap = value;
                CheckRange();
            }
        }

        [Category("Chart pane")]
        public int ChartRightGap
        {
            get { return rightGap; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value: " + value + "  <0");
                }
                rightGap = value;
                CheckRange();
            }
        }


        [Category("Chart pane")]
        public int ChartMinimumX
        {
            get
            {
                return ChartLeftGap + ChartAutoGap;
            }
        }

        [Category("Chart pane")]
        public int ChartMaximumX
        {
            get
            {
                if (Parent == null)
                {
                    return 100;
                }
                else
                {
                    return Width - ChartRightGap - ChartAutoGap - 1;
                }
            }
        }

        [Category("Chart pane")]
        public int ChartEffectiveWidth
        {
            get
            {
                return ChartMaximumX - ChartMinimumX + 1;
            }
        }

        [Category("Chart pane")]
        public IList<SeriesBar> SeriesBars
        {
            get
            {
                return seriesBarsUm;
            }
        }

        [Category("Slider bar")]
        public Color SliderBarColor
        {
            get { return sliderBarColor; }
            set
            {
                sliderBarColor = value;
                barBrush = new SolidBrush(sliderBarColor);
                barPen = new Pen(barBrush);
                Invalidate();
                Update();
            }
        }


        [Category("Slider bar")]
        public int SliderMinimum
        {
            get { return sliderMinMax.From; }
            set
            {
                if (value > SliderMaximum)
                {
                    throw new ArgumentException("value: " + value + "  > MaximumValue:" + SliderMaximum);
                }
                sliderMinMax.From = value;
            }
        }

        [Category("Slider bar")]
        public int SliderMaximum
        {
            get { return sliderMinMax.To; }
            set
            {
                if (value < SliderMinimum)
                {
                    throw new ArgumentException("value: " + value + "  < MinimumValue:" + SliderMaximum);
                }
                sliderMinMax.To = value;
            }
        }

        [Category("Slider bar")]
        public Range SliderValueRange
        {
            get { return sliderMinMax; }
            internal set
            {
                if (value.To < value.From)
                {
                    throw new ArgumentException("value.To < value.From  " + value);
                }
                sliderMinMax = value;
            }
        }

        
        [Category("Slider bar")]
        public int SliderValue
        {
            get
            {
                return sliderValue;
            }
            set
            {
                if (value < SliderMinimum || value > SliderMaximum)
                {
                    throw new ArgumentOutOfRangeException("value: " + value + "  of  " + SliderMinimum + ".." + SliderMaximum);
                }
                if (sliderValue != value)
                {
                    sliderValue = value;
                    LayOut();
                    Invalidate();
                    Update();
                    if (SliderValueChanged != null)
                        SliderValueChanged(this, new PropertyChangedEventArgs("SliderValue"));
                }
            }
        }

        [Category("Slider bar")]
        public int SliderPosition
        {
            get
            {
                return thumbRectangle.X + SliderDefaultAutoGap;
            }
            private set
            {
                if (value < ChartMinimumX || value > ChartMaximumX)
                {
                    throw new ArgumentOutOfRangeException("value: " + value + "  of  " + ChartMinimumX + ".." + ChartMaximumX);
                }
                int x = value - SliderDefaultAutoGap;
                if (x != thumbRectangle.X)
                {
                    thumbRectangle.X = x;
                    CalcValue();
                    LayOut();
                    Invalidate();
                    Update();
                    if (SliderValueChanged != null)
                        SliderValueChanged(this, new PropertyChangedEventArgs("SliderValue"));
                }
            }
        }

        [Category("Slider bar")]
        public int CpBarValue
        {
            get {
                return cpBarValue;
            }
            set {
                if (cpBarValue != value)
                {
                    cpBarValue = value;
                    LayOut();
                    Invalidate();
                    Update();
                }
            }
        }

        [Category("Slider bar")]
        public int CpBarPosition
        {
            get
            {
                double x = (double)ChartMinimumX + ((double)ChartEffectiveWidth) * ((double)cpBarValue - SliderMinimum) / ((double)SliderMaximum - SliderMinimum) - (double)SliderDefaultAutoGap;
                return (int)x;
            }
        }

        [Category("Slider bar")]
        public bool CpBarVisible
        {
            get
            {
                return cpBarVisible;
            }
            set
            {
                if (cpBarVisible != value)
                {
                    cpBarVisible = value;
                    LayOut();
                    Invalidate();
                    Update();
                }
            }
        }

        [Category("Slider bar")]
        public bool SliderThumbVisible
        {
            get
            {
                return sliderThumbVisible;
            }
            set
            {
                sliderThumbVisible = value;
                Invalidate();
                Update();
            }
        }

        [Category("Chart pane")]
        public int ChartAutoGap
        {
            get
            {
                if (calcAutoGap) {
                    return thumbRectangle.Width / 2;
                } else {
                    return 0;
                }
            }
        }

        [Category("Slider bar")]
        public int SliderDefaultAutoGap
        {
            get
            {
                return thumbRectangle.Width / 2;
            }
        }

        internal double ChartWindowTopGapValue
        {
            get
            {
                SeriesRange range = parent.SectionRange;
                double v = range.PriceRange * (Font.Height + 2) / (double)Height;
                return v;
            }
        }

        internal int SeriesBarWidth
        {
            get
            {
                SeriesRange seriesRange = parent.SectionRange;
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int w = xw / bars;
                return w;
            }
        }

        protected virtual void LayOut()
        {
            if (!thumbInitiliazed)
            {
                if (TrackBarRenderer.IsSupported)
                {
                    using (var g = CreateGraphics())
                    {
                        thumbRectangle.Size = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);
                        g.Dispose();
                    }
                }
                else
                {
                    thumbRectangle.Width = 11;//7
                    thumbRectangle.Height = 21;//13
                }
                thumbRectangle.Height = (int)(thumbRectangle.Height * 0.8);
                thumbRectangle.Width = (int)(thumbRectangle.Width * 0.8);
                thumbInitiliazed = true;
            }
            CalcRect();
            barRectangle.X = SliderPosition - 2;
            barRectangle.Y = 0;
            barRectangle.Height = Height;

            cpBarRectangle.X = CpBarPosition;
            cpBarRectangle.Y = 0;
            cpBarRectangle.Height = Height;

            seriesBars.Clear();

            IChartOwner owner = Owner;
            if (parent != null && chart != null && owner != null)
            {
                SeriesRange seriesRange = parent.SectionRange;

                for (int i = seriesRange.OffsetTo; i >= seriesRange.OffsetFrom; i--)
                {
                    add_bar(seriesBars, i);
                }
            }
        }

        protected virtual void add_bar(List<SeriesBar> seriesBars, int i)
        {
            SeriesBar bar = CreateBar(i);
            if (bar != null)
            {
                seriesBars.Add(bar);
            }
        }

        protected virtual SeriesBar CreateBar(int i)
        {
            IChartOwner owner = Owner;
            SeriesRange seriesRange = parent.SectionRange;

            SeriesBar bar;

            int j = seriesRange.OffsetTo - i + 1;
            if (owner.sLTime.StartIndex <= i && i < owner.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int h = Height;

                int x0 = ChartLeftGap + ChartAutoGap;// -xw / bars / 2;

                datetime time = (datetime)owner.sLTime[i];
                int x1 = x0 + (j - 1) * xw / bars;
                int x2 = x0 + j * xw / bars;

                bar = new SeriesBar(x1, x2, i, time);
            }
            else
            {
                bar = null;
            }

            return bar;
        }


        void CheckRange()
        {
            int x = SliderPosition;
            if (x < ChartMinimumX)
            {
                x = ChartMinimumX;
            }
            else if (x > ChartMaximumX)
            {
                x = ChartMaximumX;
            }
            SliderPosition = x;
        }

        void CalcRect()
        {
            double x = (double)ChartMinimumX + ((double)ChartEffectiveWidth) * ((double)sliderValue - SliderMinimum) / ((double)SliderMaximum - SliderMinimum) - (double)SliderDefaultAutoGap;
            thumbRectangle.X = (int)x;
        }

        void CalcValue()
        {
            double x = (double)SliderMinimum + ((double)SliderMaximum - SliderMinimum) * ((double)SliderPosition - ChartMinimumX) / ((double)ChartEffectiveWidth);
            sliderValue = (int)x;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (chart != null && Owner != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                DrawChart(e);

                DrawIndicators(e);

                DrawGrid(e);

                DrawLevels(e);
            }

            DrawCursors(e);
        }

        void DrawGrid(PaintEventArgs e)
        {
            foreach (PriceLabelY l in parent.priceLabelYs)
            {
                try
                {
                    e.Graphics.DrawLine(gridPens[l.importance], 0, l.screenY, Width, l.screenY);
                }
                catch (OverflowException)
                {
                }
            }

            foreach (TimeLabelX l in chart.timeLabelXsUpper)
            {
                try
                {
                    e.Graphics.DrawLine(gridPens[l.importance], l.screenX, 0, l.screenX, Height);
                }
                catch (OverflowException)
                {
                }
            }
            foreach (TimeLabelX l in chart.timeLabelXsLower)
            {
                try
                {
                    e.Graphics.DrawLine(gridPens[l.importance], l.screenX, 0, l.screenX, Height);
                }
                catch (OverflowException)
                {
                }
            }
        }

        void DrawCursors(PaintEventArgs e)
        {
            if (cpBarVisible)
            {
                e.Graphics.DrawLine(barPen, cpBarRectangle.X, cpBarRectangle.Y, cpBarRectangle.X, cpBarRectangle.Bottom);
            }

            if (thumbInitiliazed)
            {
                e.Graphics.FillRectangle(barBrush, barRectangle);

                //e.Graphics.DrawLine(linePen, thumbRectangle.X + AutoGap, 0, thumbRectangle.X + AutoGap, Height);

                if (sliderThumbVisible)
                {
                    if (TrackBarRenderer.IsSupported)
                    {
                        TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, thumbRectangle, thumbState);
                    }
                    else
                    {
                        e.Graphics.DrawImage(Resources.downthumb, thumbRectangle);
                    }
                }
            }
        }

        void CursorBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (DragState == DraggingState.INACTIVE || DragState == DraggingState.SLIDER_OVER)
            {
                if (SliderContains(e.Location))
                {
                    DragState = DraggingState.SLIDER_PRESSED;
                    thumbState = TrackBarThumbState.Pressed;
                    Invalidate();
                    Update();
                }
                else
                {
                    DragState = DraggingState.CHART_PRESSED;
                    thumbState = TrackBarThumbState.Normal;
                    chartDragStartX = e.X;
                    chartDragStartY = e.Y;
                    Invalidate();
                    Update();
                }
            }
        }

        void CursorBar_MouseUp(object sender, MouseEventArgs e)
        {
            switch (DragState)
            {
                case DraggingState.SLIDER_PRESSED:
                    DragState = DraggingState.INACTIVE;
                    thumbState = TrackBarThumbState.Normal;
                    Invalidate();
                    Update();
                    break;
                case DraggingState.SLIDER_DRAGGED:
                    DragState = DraggingState.INACTIVE;
                    thumbState = TrackBarThumbState.Normal;
                    Invalidate();
                    Update();
                    DragSlider(e);
                    break;
                case DraggingState.CHART_PRESSED:
                    DragState = DraggingState.INACTIVE;
                    break;
                case DraggingState.CHART_DRAGGED:
                    DragState = DraggingState.INACTIVE;
                    DragChart(e);
                    break;
                default:
                    break;
            }
        }

        void CursorBar_MouseMove(object sender, MouseEventArgs e)
        {
            switch (DragState)
            {
                case DraggingState.SLIDER_PRESSED:
                    DragState = DraggingState.SLIDER_DRAGGED;
                    DragSlider(e);
                    break;
                case DraggingState.SLIDER_DRAGGED:
                    DragSlider(e);
                    break;
                case DraggingState.SLIDER_OVER:
                    if (!SliderContains(e.Location))
                    {
                        DragState = DraggingState.INACTIVE;
                        thumbState = TrackBarThumbState.Normal;
                        Invalidate();
                        Update();
                    }
                    break;
                case DraggingState.CHART_PRESSED:
                    DragState = DraggingState.CHART_DRAGGED;
                    DragChart(e);
                    break;
                case DraggingState.CHART_DRAGGED:
                    DragChart(e);
                    break;
                case DraggingState.INACTIVE:
                    if (SliderContains(e.Location))
                    {
                        DragState = DraggingState.SLIDER_OVER;
                        thumbState = TrackBarThumbState.Hot;
                        Invalidate();
                        Update();
                    }

                    SeriesBar old = selectedSeriesBar;
                    selectedSeriesBar = SearchSeriesBar(e.X);

                    if (selectedSeriesBar != null ) {
                        string f = Owner.SymbolFormat;

                        selectedSeriesBar.PrintStatus(chart, f);
                    }

                    if (selectedSeriesBar != old)
                    {
                        Invalidate();
                        Update();
                    }

                    break;
                default:
                    break;
            }
        }

        void CursorBar_MouseLeave(object sender, EventArgs e)
        {
            switch (DragState)
            {
                case DraggingState.SLIDER_PRESSED:
                    break;
                case DraggingState.SLIDER_DRAGGED:
                    break;
                case DraggingState.SLIDER_OVER:
                    DragState = DraggingState.INACTIVE;
                    thumbState = TrackBarThumbState.Normal;
                    Invalidate();
                    Update();

                    if (selectedSeriesBar != null)
                    {
                        selectedSeriesBar = null;
                        Invalidate();
                        Update();
                    }
                    break;
                case DraggingState.CHART_PRESSED:
                    break;
                case DraggingState.CHART_DRAGGED:
                    break;
                case DraggingState.INACTIVE:
                    if (selectedSeriesBar != null)
                    {
                        selectedSeriesBar = null;
                        Invalidate();
                        Update();
                    }
                    break;
                default:
                    break;
            }
        }


        void ChartPane_Layout(object sender, LayoutEventArgs e)
        {
            LayOut();
        }

        void ChartPane_ParentChanged(object sender, EventArgs e)
        {
            LayOut();
            Invalidate();
            Update();
        }


        void DragSlider(MouseEventArgs e)
        {
            int x = e.X;
            if (x < ChartMinimumX)
            {
                SliderPosition = ChartMinimumX;
            }
            else if (x > ChartMaximumX)
            {
                SliderPosition = ChartMaximumX;
            }
            else
            {
                SliderPosition = x;
            }

            if (SliderValueDragged != null)
                SliderValueDragged(this, new PropertyChangedEventArgs("SliderValue"));
        }

        void DragChart(MouseEventArgs e)
        {
            IChartOwner owner = Owner;

            int deltaX, deltaY;
            int deltaIndX;
            double deltaPriceY;

            deltaX = chartDragStartX - e.X;

            SeriesRange seriesRange = parent.SectionRange;
            int bars = seriesRange.NumBars;
            int xw = ChartEffectiveWidth;

            deltaIndX = deltaX * bars / xw;
            bool _setAllFocusTime = false;
            bool _updateSeries = false;
            if (deltaIndX != 0)
            {
                try
                {
                    if (deltaIndX < 0)
                    {
                        owner.LoadForward(deltaIndX - owner.SeriesRange.OffsetTo);
                        owner.LoadForward(owner.SeriesRange.OffsetTo);
                    }
                    else
                    {
                        owner.LoadForward(deltaIndX);
                    }

                    owner.ScrolledBarTime = (datetime)owner.sLTime[0];

                    chartDragStartX -= deltaIndX * xw / bars;

                    _setAllFocusTime = true;
                }
                catch (TimeSeriesEOFException)
                {
                    if (deltaIndX > 1)
                    {
                        owner.LoadAtTotal(owner.RecordCount - 1);
                        owner.ScrolledBarTime = (datetime)owner.sLTime[0];

                        _setAllFocusTime = true;
                    }
                    else if (deltaIndX < -1)
                    {
                        owner.LoadAtTotal(owner.SeriesRange.OffsetTo);
                        owner.ScrolledBarTime = (datetime)owner.sLTime[0];

                        _setAllFocusTime = true;
                    }
                }
            }

            if (!owner.AutoSeriesRange)
            {
                deltaY = e.Y - chartDragStartY;

                SeriesRange r = owner.SeriesRange;
                deltaPriceY = r.PriceRange * deltaY / Height;
                if (Math.Abs(deltaPriceY) >= owner.Point)
                {
                    r.PriceFrom += deltaPriceY;
                    r.PriceTo += deltaPriceY;
                    owner.SeriesRange = r;

                    chartDragStartY += (int)(Height * deltaPriceY / r.PriceRange);

                    _updateSeries = true;
                }
            }

            if (_setAllFocusTime)
            {
                chart.SetAllFocusTime();
            }
            else if (_updateSeries)
            {
                chart.UpdateSeries();
            }

        }

        bool SliderContains(Point p)
        {
            return barRectangle.Contains(p) || thumbRectangle.Contains(p);
        }

        SeriesBar SearchSeriesBar(int screenX)
        {
            SeriesRange seriesRange = parent.SectionRange;
            int bars = seriesRange.NumBars;
            int xw = ChartEffectiveWidth;
            int h = Height;

            int x0 = leftGap + ChartAutoGap - xw / bars / 2;
            //int x2 = x0 + j * xw / bars;
            int barInd = (screenX - x0) * bars / xw;
            SeriesBar result;
            if (barInd >= 0 && barInd < seriesBars.Count)
            {
                result = seriesBars[barInd];
            }
            else
            {
                result = null;
            }

            return result;
        }

        SeriesBar SearchSeriesBar2(int screenX)
        {
            int a = 0;
            int b = seriesBars.Count - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                SeriesBar midval = seriesBars[mid];

                switch (midval == null ? -1 : midval.CompareX(screenX))
                {
                    case 0:
                        return midval;
                    case -1:
                        a = mid + 1;
                        break;
                    default:
                        b = mid - 1;
                        break;
                }
            }
            return null;
        }

        internal SeriesBar SearchSeriesBar(datetime dt)
        {
            int a = 0;
            int b = seriesBars.Count - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                SeriesBar midval = seriesBars[mid];

                switch (midval == null ? -1 : midval.CompareDt(dt))
                {
                    case 0:
                        return midval;
                    case -1:
                        a = mid + 1;
                        break;
                    default:
                        b = mid - 1;
                        break;
                }
            }
            return null;
        }

    
    
        internal abstract void InvalidateDrawer();
        protected abstract void DrawChart(PaintEventArgs e);
        protected abstract void DrawIndicators(PaintEventArgs e);
        protected abstract void DrawLevels(PaintEventArgs e);

        protected abstract IChartOwner Owner
        {
            get;
        }
    }
}
