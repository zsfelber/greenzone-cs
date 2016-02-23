using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Windows.Forms.VisualStyles;
using System.Drawing;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ClientChartPaneControllerEx : ClientChartPaneControllerBase
    {
        public event PropertyChangedEventHandler SliderValueDragged;

        int chartDragStartX;
        int chartDragStartY;

        bool currentDraggedIsSlider;

        Timer dragChartTimer;
        Timer dragSliderTimer;
        protected Point lastDragPoint;
        protected bool draggingChart;
        protected bool draggingSlider;

        EventHandler dragChartTimer_Tick_H;
        EventHandler dragSliderTimer_Tick_H;

        public ClientChartPaneControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            SeriesBars = new List<SeriesBar>();
        }

        public ClientChartPaneControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            SeriesBars = new List<SeriesBar>();
            MouseDown += new ControllerEventHandler(ChartPaneController_MouseDown);
            Dragged += new ControllerEventHandler(ChartPaneController_Dragged);
            LayoutChanged += new ControllerEventHandler(ChartPaneController_LayoutChanged);
        }

        protected ClientChartPaneControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SeriesBars = new List<SeriesBar>();
        }

        public abstract ChartDrawerController Drawer
        {
            get;
        }

        public override bool DragTimerUsed
        {
            get
            {
                return base.DragTimerUsed;
            }
            set
            {
                if (base.DragTimerUsed != value)
                {
                    base.DragTimerUsed = value;
                    if (value)
                    {
                        dragChartTimer = new Timer();
                        dragSliderTimer = new Timer();

                        dragChartTimer.Interval = 100;
                        dragSliderTimer.Interval = 100;

                        dragChartTimer.Start();
                        dragSliderTimer.Start();

                        dragChartTimer_Tick_H = new EventHandler(dragChartTimer_Tick);
                        dragSliderTimer_Tick_H = new EventHandler(dragSliderTimer_Tick);
                        dragChartTimer.Tick += dragChartTimer_Tick_H;
                        dragSliderTimer.Tick += dragSliderTimer_Tick_H;
                    }
                    else
                    {
                        dragChartTimer.Tick -= dragChartTimer_Tick_H;
                        dragSliderTimer.Tick -= dragSliderTimer_Tick_H;
                        dragChartTimer.Dispose();
                        dragSliderTimer.Dispose();
                        dragChartTimer = null;
                        dragSliderTimer = null;
                    }
                }
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override Color AskColor
        {
            get { return base.AskColor; }
            set
            {
                base.AskColor = value;
                Update();
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override Color InactiveColor
        {
            get { return base.InactiveColor; }
            set
            {
                base.InactiveColor = value;
                inactiveBrush = new SolidBrush(value);
                inactivePen = new Pen(value); ;
                Update();
            }
        }

        Brush inactiveBrush;
        public Brush InactiveBrush
        {
            get
            {
                return inactiveBrush;
            }
        }

        Pen inactivePen;
        public Pen InactivePen
        {
            get
            {
                return inactivePen;
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override Color GridColor
        {
            get { return base.GridColor; }
            set
            {
                base.GridColor = value;
                Pen[] gridPens = new Pen[7];
                for (int i = 0; i < 6; i++)
                {
                    Pen gridPen = new Pen(Color.FromArgb(30 + i * 225 / 5, base.GridColor));

                    List<float> ptrn = new List<float>();
                    ptrn.Add(i / 3 + 1);
                    ptrn.Add(6 - i);
                    gridPen.DashPattern = ptrn.ToArray();
                    //gridPen.Width = 4f * (i + 1) / 6f;

                    gridPens[i] = gridPen;
                }
                gridPens[6] = new Pen(gridPens[5].Color);
                GridPens = Array.AsReadOnly(gridPens);
                Update();
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override bool ChartCalcAutoGap
        {
            get { return base.ChartCalcAutoGap; }
            set
            {
                base.ChartCalcAutoGap = value;
                CheckRange();
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override int ChartLeftGap
        {
            get { return base.ChartLeftGap; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value: " + value + "  <0");
                }
                base.ChartLeftGap = value;
                CheckRange();
            }
        }

        [System.ComponentModel.Category("ChartController pane")]
        public override int ChartRightGap
        {
            get { return base.ChartRightGap; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("value: " + value + "  <0");
                }
                base.ChartRightGap = value;
                CheckRange();
            }
        }


        [System.ComponentModel.Category("Slider bar")]
        public override Color SliderBarColor
        {
            get { return base.SliderBarColor; }
            set
            {
                base.SliderBarColor = value;
                barBrush = new SolidBrush(value);
                barPen = new Pen(barBrush);
                Update();
            }
        }

        Brush barBrush;
        public Brush BarBrush
        {
            get
            {
                return barBrush;
            }
        }

        Pen barPen;
        public Pen BarPen
        {
            get
            {
                return barPen;
            }
        }


        [System.ComponentModel.Category("Slider bar")]
        public override int SliderMinimum
        {
            get { return base.SliderMinimum; }
            set
            {
                if (value > SliderMaximum)
                {
                    throw new ArgumentException("value: " + value + "  > MaximumValue:" + SliderMaximum);
                }
                base.SliderMinimum = value;
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override int SliderMaximum
        {
            get { return base.SliderMaximum; }
            set
            {
                if (value < SliderMinimum)
                {
                    throw new ArgumentException("value: " + value + "  < MinimumValue:" + SliderMaximum);
                }
                base.SliderMaximum = value;
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override Range SliderValueRange
        {
            get { return base.SliderValueRange; }
            set
            {
                if (value.To < value.From)
                {
                    throw new ArgumentException("value.To < value.From  " + value);
                }
                base.SliderValueRange = value;
            }
        }


        [System.ComponentModel.Category("Slider bar")]
        public override int SliderValue
        {
            get
            {
                return base.SliderValue;
            }
            set
            {
                if (value < SliderMinimum || value > SliderMaximum)
                {
                    throw new ArgumentOutOfRangeException("value: " + value + "  of  " + SliderMinimum + ".." + SliderMaximum);
                }
                if (base.SliderValue != value)
                {
                    base.SliderValue = value;
                    LayOut();
                    Update();
                }
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override int CpBarValue
        {
            get
            {
                return base.CpBarValue;
            }
            set
            {
                if (base.CpBarValue != value)
                {
                    base.CpBarValue = value;
                    LayOut();
                    Update();
                }
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override bool CpBarVisible
        {
            get
            {
                return base.CpBarVisible;
            }
            set
            {
                if (base.CpBarVisible != value)
                {
                    base.CpBarVisible = value;
                    Update();
                }
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override bool ThumbRectBarVisible
        {
            get
            {
                return base.ThumbRectBarVisible;
            }
            set
            {
                if (base.ThumbRectBarVisible != value)
                {
                    base.ThumbRectBarVisible = value;
                    Update();
                }
            }
        }

        [System.ComponentModel.Category("Slider bar")]
        public override bool SliderThumbVisible
        {
            get
            {
                return base.SliderThumbVisible;
            }
            set
            {
                base.SliderThumbVisible = value;
                Update();
            }
        }

        public override double ChartWindowTopGapValue
        {
            get
            {
                SeriesRange range = Parent.SectionRange;
                double v = range.PriceRange * (Font.Height + 2) / (double)Height;
                return v;
            }
        }

        public override int SeriesBarWidth
        {
            get
            {
                SeriesRange seriesRange = Parent.SectionRange;
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                int w = xw / bars;
                return w;
            }
        }

        SeriesBar selectedSeriesBar;
        public SeriesBar SelectedSeriesBar
        {
            get
            {
                return selectedSeriesBar;
            }
        }

        IndicatorBar selectedIndicatorBar;
        public IndicatorBar SelectedIndicatorBar
        {
            get
            {
                return selectedIndicatorBar;
            }
        }

        public void ClearSeriesBarSelection()
        {
            selectedSeriesBar = null;
            selectedIndicatorBar = null;
        }

        public void SelectSeriesBar(Point mouseOverPt)
        {
            selectedSeriesBar = SearchSeriesBar(mouseOverPt.X);
            selectedIndicatorBar = null;

            if (selectedSeriesBar != null)
            {
                string f = Parent.PriceFormat;

                int y = -100000;

                foreach (var ib in selectedSeriesBar.indicatorBars)
                {
                    if (Math.Abs(ib.y - mouseOverPt.Y) < Math.Abs(y - mouseOverPt.Y))
                    {
                        selectedIndicatorBar = ib;
                        y = ib.y;
                    }
                }

                Chart.PrintStatus(selectedSeriesBar, selectedIndicatorBar, f);
            }
        }

        protected override void LayOut()
        {
            CalcRect();
            BarRectangle = new Rectangle(SliderPosition - 2, 0, BarRectangle.Width, Height);
            CpBarRectangle = new Rectangle(CpBarPosition, 0, CpBarRectangle.Width, Height);

            SeriesBars.Clear();

            IChartOwner owner = Owner;
            if (Parent != null && Chart != null && owner != null)
            {
                SeriesRange seriesRange = Parent.SectionRange;

                for (int i = seriesRange.OffsetTo; i >= seriesRange.OffsetFrom; i--)
                {
                    add_bar(SeriesBars, i);
                }
            }
        }

        protected virtual void add_bar(List<SeriesBar> SeriesBars, int i)
        {
            SeriesBar bar = CreateBar(i);
            if (bar != null)
            {
                SeriesBars.Add(bar);
            }
        }

        protected virtual SeriesBar CreateBar(int i)
        {
            IChartOwner owner = Owner;
            SeriesRange seriesRange = Parent.SectionRange;

            SeriesBar bar;

            int j = seriesRange.OffsetTo - i + 1;
            if (owner.sLTime.StartIndexP <= i && i < owner.sLTime.Length)
            {
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;
                //int h = Height - 2;

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


        public virtual void InvalidateDrawer()
        {
        }

        protected override void OnPaint(ControllerPaintEventArgs e)
        {
            base.OnPaint(e);
            GraphicsController g = e.Graphics;
            if (Chart != null && Owner != null)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                DrawBackground(e);

                DrawGrid(e);

                DrawLevels(e);

                DrawChart(e);

                DrawIndicators(e);
            }

            DrawCursors(e);
        }

        protected virtual void DrawBackground(ControllerPaintEventArgs e)
        {
        }

        protected virtual void DrawChart(ControllerPaintEventArgs e)
        {
        }

        protected virtual void DrawIndicators(ControllerPaintEventArgs e)
        {
        }

        protected virtual void DrawGrid(ControllerPaintEventArgs e)
        {
            GraphicsController g = e.Graphics;

            foreach (PriceLabelY l in Parent.PriceLabelYs)
            {
                try
                {
                    g.DrawLine(GridPens[l.importance], 0, l.screenY, Width, l.screenY);
                }
                catch (OverflowException)
                {
                }
            }

            foreach (TimeLabelX l in Chart.TimeLabelXsUpper)
            {
                try
                {
                    g.DrawLine(GridPens[l.importance], l.screenX, 0, l.screenX, Height);
                }
                catch (OverflowException)
                {
                }
            }

            foreach (TimeLabelX l in Chart.TimeLabelXsLower)
            {
                try
                {
                    g.DrawLine(GridPens[l.importance], l.screenX, 0, l.screenX, Height);
                }
                catch (OverflowException)
                {
                }
            }
        }

        protected virtual void DrawLevels(ControllerPaintEventArgs e)
        {

        }

        protected virtual void DrawCursors(ControllerPaintEventArgs e)
        {
            GraphicsController g = e.Graphics;
            if (CpBarVisible)
            {
                Rectangle cpBarRectangle = CpBarRectangle;
                g.DrawLine(BarPen, cpBarRectangle.X, cpBarRectangle.Y, cpBarRectangle.X, cpBarRectangle.Bottom);
            }

        }



        protected virtual void ChartPaneController_MouseDown(object sender, ControllerEventArgs _e)
        {
            ControllerMouseEventArgs e = (ControllerMouseEventArgs)_e;
            chartDragStartX = e.Point.X;
            chartDragStartY = e.Point.Y;

            currentDraggedIsSlider = SliderContains(e.Point);
        }

        protected virtual void ChartPaneController_Dragged(object sender, ControllerEventArgs _e)
        {
            ControllerMouseEventArgs e = (ControllerMouseEventArgs)_e;
            if (currentDraggedIsSlider)
            {
                DragSlider(e.Point);
            }
            else
            {
                DragChart(e.Point);
            }
        }

        void ChartPaneController_LayoutChanged(object sender, ControllerEventArgs e)
        {
            LayOut();
        }

        public void DragSlider(Point p)
        {
            draggingSlider = true;
            lastDragPoint = p;
            if (!DragTimerUsed)
            {
                dragSliderTimer_Tick(null, null);
            }
        }

        void dragSliderTimer_Tick(object sender, EventArgs e)
        {
            if (draggingSlider)
            {
                draggingSlider = false;
                int x = lastDragPoint.X;
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

                if (Owner.IsCursorBarConnected)
                {
                    Owner.ParentCursorPosition = SliderValue;
                }
                else
                {
                    Owner.CursorPosition = SliderValue;
                }

                Chart.UpdateAllChartAndCursor();

                if (SliderValueDragged != null)
                    SliderValueDragged(this, new PropertyChangedEventArgs("SliderValue", SliderValue));
            }
        }

        public void DragChart(Point p)
        {
            draggingChart = true;
            lastDragPoint = p;
            if (!DragTimerUsed)
            {
                dragChartTimer_Tick(null, null);
            }
        }

        protected virtual void dragChartTimer_Tick(object sender, EventArgs e)
        {
            if (draggingChart)
            {
                draggingChart = false;
                IChartOwner owner = Owner;

                int deltaX, deltaY;
                int deltaIndX;
                double deltaPriceY;

                deltaX = chartDragStartX - lastDragPoint.X;

                SeriesRange seriesRange = Parent.SectionRange;
                int bars = seriesRange.NumBars;
                int xw = ChartEffectiveWidth;

                deltaIndX = deltaX * bars / xw;
                bool _dragX = false;
                bool _dragY = false;
                if (deltaIndX != 0)
                {
                    chartDragStartX -= deltaIndX * xw / bars;

                    if (owner.TotalFileOffset + deltaIndX >= owner.RecordCount)
                    {
                        deltaIndX = (int)(owner.RecordCount - owner.TotalFileOffset - 1);
                    }
                    else if (owner.TotalFileOffset + deltaIndX < 0)
                    {
                        deltaIndX = (int)(-owner.TotalFileOffset);
                    }


                    if (deltaIndX != 0)
                    {
                        owner.LoadForward(deltaIndX);

                        _dragX = true;
                    }
                }

                if (!owner.AutoSeriesRange)
                {
                    deltaY = lastDragPoint.Y - chartDragStartY;

                    SeriesRange r0 = Parent.SectionRange;
                    deltaPriceY = r0.PriceRange * deltaY / (Height - 1);
                    if (Math.Abs(deltaPriceY) >= owner.Point)
                    {
                        SeriesRange r = Parent.DragYRange;
                        SeriesRange r1 = r;
                        r.PriceFrom += deltaPriceY;
                        r.PriceTo += deltaPriceY;
                        Parent.DragYRange = r;
                        deltaPriceY = Parent.DragYRange.PriceTo - r1.PriceTo;
                        chartDragStartY += (int)((Height - 1) * deltaPriceY / r0.PriceRange);

                        _dragY = true;
                    }
                }

                if (_dragX)
                {
                    owner.ParentScrolledBarTime = owner.ScrolledBarTime;
                    Chart.UpdateAllChartAndCursor();
                }
                else if (_dragY)
                {
                    Chart.UpdateAllChartAndCursor();
                }
            }
        }

        public bool SliderContains(Point p)
        {
            return BarRectangle.Contains(p) || ThumbRectangle.Contains(p);
        }

        public SeriesBar SearchSeriesBar(int screenX)
        {
            SeriesRange seriesRange = Parent.SectionRange;
            int bars = seriesRange.NumBars;
            int xw = ChartEffectiveWidth;
            int h = Height - 2;

            int x0 = ChartLeftGap + ChartAutoGap - xw / bars / 2;
            //int x2 = x0 + j * xw / bars;
            int barInd = (screenX - x0) * bars / xw;
            SeriesBar result;
            if (barInd >= 0 && barInd < SeriesBars.Count)
            {
                result = SeriesBars[barInd];
            }
            else
            {
                result = null;
            }

            return result;
        }

        public SeriesBar SearchSeriesBar2(int screenX)
        {
            int a = 0;
            int b = SeriesBars.Count - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                SeriesBar midval = SeriesBars[mid];

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

        public SeriesBar SearchSeriesBar(datetime dt)
        {
            int a = 0;
            int b = SeriesBars.Count - 1;
            while (a <= b)
            {
                int mid = (a + b) / 2;
                SeriesBar midval = SeriesBars[mid];

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
            if (a >= SeriesBars.Count)
            {
                a = SeriesBars.Count - 1;
            }
            return SeriesBars[a];
        }


    }
}