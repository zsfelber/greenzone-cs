using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using GreenZoneFxEngine.Trading;
using System.Drawing.Drawing2D;
using GreenZoneUtil.ViewController;
using System.Windows.Forms;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ChartDrawerController : TradingConstBase
    {
        public virtual void DrawStarted(ControllerPaintEventArgs e)
        {
        }

        public virtual void DrawFinished(ControllerPaintEventArgs e)
        {
        }
    }

    public abstract class SeriesChartDrawerController : ChartDrawerController
    {
        public abstract void Draw(GraphicsController g, int x1, int x2, int yopen, int ylow, int yhigh, int yclose, int ynextclose, bool isActive, bool isSelected);

        public abstract void DrawTick(GraphicsController g, int x1, int x2, int ybid, int yask, int ynextbid, int ynextask, bool isActive, bool isSelected);

        public abstract ChartType ChartType
        {
            get;
        }

        public static bool IsDrawerControllerAvailable(ChartType chartType, TimePeriodConst period)
        {
            if (period.GetCategory() == TimePeriodCategory.TICKS)
            {
                return chartType == ChartType.LINE;
            }
            else
            {
                return true;
            }
        }
    }

    public class CandlestickDrawerController : SeriesChartDrawerController
    {
        Color positiveColor;
        Brush positiveBrush;
        Pen positivePen;
        Pen selectedPen;
        Color negativeColor;
        Brush negativeBrush;
        Pen negativePen;

        Color inactiveColor;
        Brush inactiveBrush;
        Pen inactivePen;

        public CandlestickDrawerController(Color positiveColor, Color negativeColor, Color inactiveColor)
        {
            PositiveColor = positiveColor;
            NegativeColor = negativeColor;
            InactiveColor = inactiveColor;
        }

        public override ChartType ChartType
        {
            get
            {
                return ChartType.CANDLESTICKS;
            }
        }

        public Color PositiveColor
        {
            get { return positiveColor; }
            private set
            {
                positiveColor = value;
                positiveBrush = new SolidBrush(positiveColor);
                positivePen = new Pen(positiveBrush);
                selectedPen = new Pen(positiveColor, positivePen.Width + 1);
            }
        }

        public Color NegativeColor
        {
            get { return negativeColor; }
            private set
            {
                negativeColor = value;
                negativeBrush = new SolidBrush(negativeColor);
                negativePen = new Pen(negativeBrush);
            }
        }

        public Color InactiveColor
        {
            get { return inactiveColor; }
            private set
            {
                inactiveColor = value;
                inactiveBrush = new SolidBrush(inactiveColor);
                inactivePen = new Pen(inactiveBrush);
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int yopen, int ylow, int yhigh, int yclose, int ynextclose, bool isActive, bool isSelected)
        {
            int gap1 = 0, gap2 = 0;

            if (x2 - x1 >= 15)
            {
                gap1 = 3;
                gap2 = 2;
            }
            else if (x2 - x1 >= 10)
            {
                gap1 = 2;
                gap2 = 2;
            }
            else if (x2 - x1 >= 6)
            {
                gap1 = 2;
                gap2 = 1;
            }
            else if (x2 - x1 >= 3)
            {
                gap1 = 1;
                gap2 = 1;
            }
            else
            {
                gap1 = 0;
                gap2 = 0;
            }

            if (isSelected)
            {
                if (gap1 > 0)
                    gap1--;
                if (gap2 > 0)
                    gap2--;
            }
            x1 += gap1;
            x2 -= gap2;


            int w = x2 - x1;
            int w2 = w / 2;

            Pen pen = isActive ? (isSelected ? selectedPen : positivePen) : inactivePen;
            Brush brush = isActive ? positiveBrush : inactiveBrush;

            switch (Math.Sign(yopen - yclose))
            {
                case 1:
                    g.DrawRectangle(pen, x1, yclose, w, yopen - yclose);
                    g.FillRectangle(negativeBrush, x1 + 1, yclose + 1, w - 2, yopen - yclose - 2);
                    g.DrawLine(pen, x1 + w2, yhigh, x1 + w2, yclose);
                    g.DrawLine(pen, x1 + w2, yopen, x1 + w2, ylow);
                    break;
                case -1:
                    g.FillRectangle(brush, x1, yopen, w, yclose - yopen);
                    g.DrawLine(pen, x1 + w2, yhigh, x1 + w2, yopen);
                    g.DrawLine(pen, x1 + w2, yclose, x1 + w2, ylow);
                    break;
                default:
                    g.DrawLine(pen, x1, yclose, x2, yclose);
                    g.DrawLine(pen, x1 + w2, yhigh, x1 + w2, yclose);
                    g.DrawLine(pen, x1 + w2, yopen, x1 + w2, ylow);
                    break;
            }
        }

        public override void DrawTick(GraphicsController g, int x1, int x2, int ybid, int yask, int ynextbid, int ynextask, bool isActive, bool isSelected)
        {
            throw new NotSupportedException(); 
        }
    }


    public class OHLCDrawerController : SeriesChartDrawerController
    {
        Color positiveColor;
        Brush positiveBrush;
        Pen positivePen;
        Pen selectedPen;

        Color inactiveColor;
        Brush inactiveBrush;
        Pen inactivePen;

        public OHLCDrawerController(Color positiveColor, Color inactiveColor)
        {
            PositiveColor = positiveColor;
            InactiveColor = inactiveColor;
        }

        public override ChartType ChartType
        {
            get
            {
                return ChartType.OHLC;
            }
        }

        public Color PositiveColor
        {
            get { return positiveColor; }
            private set
            {
                positiveColor = value;
                positiveBrush = new SolidBrush(positiveColor);
                positivePen = new Pen(positiveBrush);
                selectedPen = new Pen(positiveColor, positivePen.Width + 1);
            }
        }

        public Color InactiveColor
        {
            get { return inactiveColor; }
            private set
            {
                inactiveColor = value;
                inactiveBrush = new SolidBrush(inactiveColor);
                inactivePen = new Pen(inactiveBrush);
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int yopen, int ylow, int yhigh, int yclose, int ynextclose, bool isActive, bool isSelected)
        {
            int gap1 = 0, gap2 = 0;

            if (x2 - x1 >= 15)
            {
                gap1 = 2;
                gap2 = 2;
            }
            else if (x2 - x1 >= 10)
            {
                gap1 = 2;
                gap2 = 1;
            }
            else if (x2 - x1 >= 6)
            {
                gap1 = 1;
                gap2 = 1;
            }
            else if (x2 - x1 >= 3)
            {
                gap1 = 1;
                gap2 = 0;
            }
            else
            {
                gap1 = 0;
                gap2 = 0;
            }

            x1 += gap1;
            x2 -= gap2;


            int w = x2 - x1;
            int w2 = Math.Max(2, w / 2);
            int w4 = Math.Max(1, Math.Max(gap1 + gap2, w2 / 2));

            Pen pen = isActive ? (isSelected ? selectedPen : positivePen) : inactivePen;
            if (isSelected)
            {
                w4 = Math.Max(3, w4);
            }
            g.DrawLine(pen, x1 + w2, yhigh, x1 + w2, ylow);
            g.DrawLine(pen, x1 + w2 - w4, yopen, x1 + w2, yopen);
            g.DrawLine(pen, x1 + w2, yclose, x1 + w2 + w4, yclose);

        }

        public override void DrawTick(GraphicsController g, int x1, int x2, int ybid, int yask, int ynextbid, int ynextask, bool isActive, bool isSelected)
        {
            throw new NotSupportedException();
        }
    }


    public class LineDrawerController : SeriesChartDrawerController
    {
        Color bidColor;
        Brush bidBrush;
        Pen bidPen;

        Color askColor;
        Brush askBrush;
        Pen askPen;

        Color inactiveColor;
        Brush inactiveBrush;
        Pen inactivePen;

        protected readonly List<Point> bidPoints;
        protected readonly List<Point> askPoints;
        protected readonly List<Point> inactivePoints;

        public LineDrawerController(Color bidColor, Color askColor, Color inactiveColor)
        {
            NormalColor = bidColor;
            AskColor = askColor;
            InactiveColor = inactiveColor;
            bidPoints = new List<Point>();
            askPoints = new List<Point>();
            inactivePoints = new List<Point>();
        }

        public override ChartType ChartType
        {
            get
            {
                return ChartType.LINE;
            }
        }

        public Color NormalColor
        {
            get { return bidColor; }
            private set
            {
                bidColor = value;
                bidBrush = new SolidBrush(bidColor);
                bidPen = new Pen(bidBrush);
            }
        }

        public Color AskColor
        {
            get { return askColor; }
            private set
            {
                askColor = value;
                askBrush = new SolidBrush(askColor);
                askPen = new Pen(askBrush);
            }
        }

        public Color InactiveColor
        {
            get { return inactiveColor; }
            private set
            {
                inactiveColor = value;
                inactiveBrush = new SolidBrush(inactiveColor);
                inactivePen = new Pen(inactiveBrush);
            }
        }

        public override void DrawStarted(ControllerPaintEventArgs e)
        {
            bidPoints.Clear();
            askPoints.Clear();
            inactivePoints.Clear();
        }

        public override void DrawFinished(ControllerPaintEventArgs e)
        {
            draw_curve(e.Graphics, bidPen, bidPoints);
            draw_curve(e.Graphics, askPen, askPoints);
            draw_curve(e.Graphics, inactivePen, inactivePoints);
        }

        void draw_curve(GraphicsController g, Pen pen, List<Point> points)
        {
            if (points.Count > 0)
            {
                try
                {
                    g.DrawCurve(pen, points.ToArray(), 0);
                }
                catch (OverflowException)
                {
                }
                catch (ArgumentException)
                {
                }
                points.Clear();
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int yopen, int ylow, int yhigh, int yclose, int ynextclose, bool isActive, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            if (isActive)
            {
                if (inactivePoints.Count > 0)
                {
                    bidPoints.Add(inactivePoints[inactivePoints.Count-1]);
                }
                bidPoints.Add(new Point(x1 + half, yclose));
                if (isSelected)
                {
                    g.DrawPie(bidPen, x1 + half - 2, yclose - 2, 4, 4, 0, 360);
                }
            }
            else
            {
                if (bidPoints.Count > 0)
                {
                    inactivePoints.Add(bidPoints[bidPoints.Count - 1]);
                }
                inactivePoints.Add(new Point(x1 + half, yclose));
                if (isSelected)
                {
                    g.DrawPie(inactivePen, x1 + half - 2, yclose - 2, 4, 4, 0, 360);
                }
            }
        }

        public override void DrawTick(GraphicsController g, int x1, int x2, int ybid, int yask, int ynextbid, int ynextask, bool isActive, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            askPoints.Add(new Point(x1 + half, yask));
            if (isSelected)
            {
                g.DrawPie(askPen, x1 + half - 2, yask - 2, 4, 4, 0, 360);
            }
            if (isActive)
            {
                if (inactivePoints.Count > 0)
                {
                    bidPoints.Add(inactivePoints[inactivePoints.Count - 1]);
                }
                bidPoints.Add(new Point(x1 + half, ybid));
                if (isSelected)
                {
                    g.DrawPie(bidPen, x1 + half - 2, ybid - 2, 4, 4, 0, 360);
                }
            }
            else
            {
                if (bidPoints.Count > 0)
                {
                    inactivePoints.Add(bidPoints[bidPoints.Count - 1]);
                }
                inactivePoints.Add(new Point(x1 + half, ybid));
                if (isSelected)
                {
                    g.DrawPie(inactivePen, x1 + half - 2, ybid - 2, 4, 4, 0, 360);
                }
            }
        }
    }





    public abstract class IndicatorDrawerController : ChartDrawerController
    {
        protected Color normalColor;
        protected Brush normalBrush;
        protected Pen normalPen;
        protected Pen vastagPen;

        public readonly IIndicatorRuntime indicator;
        public readonly IndicatorEntry indicatorEntry;

        protected readonly List<Point> points;

        public IndicatorDrawerController(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
        {
            this.indicator = indicator;
            this.indicatorEntry = indicatorEntry;
            NormalColor = indicatorEntry.StyleColor;
            points = new List<Point>();
        }

        public IndicatorDrawerController Pair
        {
            get;
            set;
        }

        public IndicatorEntry Entry
        {
            get
            {
                return indicatorEntry;
            }
        }

        public IndicatorBuffer Buffer
        {
            get
            {
                IndicatorBuffer buf = indicatorEntry as IndicatorBuffer;
                return buf;
            }
        }

        public Color NormalColor
        {
            get { return normalColor; }
            private set
            {
                normalColor = value;
                normalBrush = new SolidBrush(normalColor);
                normalPen = new Pen(normalBrush, (int)indicatorEntry.StyleWidth);
                vastagPen = new Pen(normalBrush, (int)Math.Max((int)indicatorEntry.StyleWidth + 2, (int)((int)indicatorEntry.StyleWidth * 1.3333)));
                if (indicatorEntry.StyleWidth == DrawingWidth.WIDTH_1)
                {
                    switch (indicatorEntry.StyleCode)
                    {
                        case DrawingStylesWidth1.STYLE_DASH: normalPen.DashPattern = new float[]{4,3}; break;
                        case DrawingStylesWidth1.STYLE_DASHDOT: normalPen.DashPattern = new float[] { 4, 3, 1, 2 }; break;
                        case DrawingStylesWidth1.STYLE_DASHDOTDOT: normalPen.DashPattern = new float[] { 4, 3, 1, 2, 1, 2 }; break;
                        case DrawingStylesWidth1.STYLE_DOT: normalPen.DashPattern = new float[] { 1, 2 }; break;
                        case DrawingStylesWidth1.STYLE_SOLID: normalPen.DashStyle = DashStyle.Solid; break;
                        default: throw new NotSupportedException();
                    }
                }
                else
                {
                    switch (indicatorEntry.StyleCode)
                    {
                        case DrawingStylesWidth1.STYLE_DASH: normalPen.DashStyle = DashStyle.Dash; break;
                        case DrawingStylesWidth1.STYLE_DASHDOT: normalPen.DashStyle = DashStyle.DashDot; break;
                        case DrawingStylesWidth1.STYLE_DASHDOTDOT: normalPen.DashStyle = DashStyle.DashDotDot; break;
                        case DrawingStylesWidth1.STYLE_DOT: normalPen.DashStyle = DashStyle.Dot; break;
                        case DrawingStylesWidth1.STYLE_SOLID: normalPen.DashStyle = DashStyle.Solid; break;
                        default: throw new NotSupportedException();
                    }

                }
            }
        }

        public Brush NormalBrush
        {
            get
            {
                return normalBrush;
            }
        }

        public Pen NormalPen
        {
            get
            {
                return normalPen;
            }
        }

        public static IndicatorDrawerController Create(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
        {
            switch (indicatorEntry.StyleType)
            {
                case DrawingStyle.DRAW_LINE: return new IndicatorLineDrawerController(indicator, indicatorEntry);
                case DrawingStyle.DRAW_HISTOGRAM: return new IndicatorHistogramDrawerController(indicator, indicatorEntry);
                case DrawingStyle.DRAW_ARROW: return new IndicatorArrowDrawerController(indicator, indicatorEntry);
                case DrawingStyle.DRAW_ZIGZAG: return new IndicatorZigZagDrawerController(indicator, indicatorEntry);
                case DrawingStyle.DRAW_SECTION: return new IndicatorSectionDrawerController(indicator, indicatorEntry);
                case DrawingStyle.DRAW_NONE: return null;
                default: throw new NotSupportedException();
            }
        }

        public override void DrawStarted(ControllerPaintEventArgs e)
        {
            points.Clear();
        }

        public override void DrawFinished(ControllerPaintEventArgs e)
        {
            if (points.Count > 0)
            {
                GraphicsController g = e.Graphics;
                try
                {
                    g.DrawCurve(normalPen, points.ToArray(), 0);
                }
                catch (OverflowException)
                {
                }
                catch (ArgumentException)
                {
                }
                points.Clear();
            }
        }

        public abstract DrawingStyle DrawingStyle
        {
            get;
        }

        public abstract void Draw(GraphicsController g, int x1, int x2, int y0, int y1, int y2, bool isSelected);
    }

    public class IndicatorLineDrawerController : IndicatorDrawerController
    {
        public IndicatorLineDrawerController(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
            : base(indicator, indicatorEntry)
        {
        }

        public override DrawingStyle DrawingStyle
        {
            get
            {
                return DrawingStyle.DRAW_LINE;
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int y0, int y1, int y2, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            if (isSelected)
            {
                g.DrawPie(normalPen, x1 + half - 2, y1 - 2, 4, 4, 0, 360);
            }
            points.Add(new Point(x1 + half, y1));
        }
    }

    public class IndicatorHistogramDrawerController : IndicatorDrawerController
    {
        public IndicatorHistogramDrawerController(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
            : base(indicator, indicatorEntry)
        {
        }

        public override DrawingStyle DrawingStyle
        {
            get
            {
                return DrawingStyle.DRAW_HISTOGRAM;
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int y0, int y1, int y2, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            if (isSelected)
            {
                g.DrawLine(vastagPen, x1 + half, y0, x1 + half, y1);
            }
            else
            {
                g.DrawLine(normalPen, x1 + half, y0, x1 + half, y1);
            }
        }
    }

    public class IndicatorZigZagDrawerController : IndicatorDrawerController
    {
        public IndicatorZigZagDrawerController(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
            : base(indicator, indicatorEntry)
        {
        }

        public override DrawingStyle DrawingStyle
        {
            get
            {
                return DrawingStyle.DRAW_ZIGZAG;
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int y0, int y1, int y2, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            if (isSelected)
            {
                g.DrawPie(normalPen, x1 + half - 2, y1 - 2, 4, 4, 0, 360);
            }
            points.Add(new Point(x1 + half, y1));
        }
    }

    public class IndicatorSectionDrawerController : IndicatorDrawerController
    {
        public IndicatorSectionDrawerController(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
            : base(indicator, indicatorEntry)
        {
        }

        public override DrawingStyle DrawingStyle
        {
            get
            {
                return DrawingStyle.DRAW_SECTION;
            }
        }


        public override void Draw(GraphicsController g, int x1, int x2, int y0, int y1, int y2, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            if (isSelected)
            {
                g.DrawPie(normalPen, x1 + half - 2, y1 - 2, 4, 4, 0, 360);
            }
            points.Add(new Point(x1 + half, y1));
        }
    }

    public class IndicatorArrowDrawerController : IndicatorDrawerController
    {
        IndicatorBuffer indicatorBuffer;
        string arrow;
        Font font;

        public IndicatorArrowDrawerController(IIndicatorRuntime indicator, IndicatorEntry indicatorEntry)
            : base(indicator, indicatorEntry)
        {
            this.indicatorBuffer = (IndicatorBuffer)indicatorEntry;
            this.arrow = "" + (char)this.indicatorBuffer.Arrow.CharCode;
            font = new Font("Wingdings", 12);
        }

        public override DrawingStyle DrawingStyle
        {
            get
            {
                return DrawingStyle.DRAW_ARROW;
            }
        }

        public override void Draw(GraphicsController g, int x1, int x2, int y0, int y1, int y2, bool isSelected)
        {
            int fontSz = x2 - x1;
            if (font.Size != fontSz)
            {
                font = new Font("Wingdings", fontSz + 3);
            }
            g.DrawString(arrow, font, normalBrush, x1, y1);
            if (isSelected)
            {
                int txtWidth = TextRenderer.MeasureText(arrow, font).Width;

                g.DrawEllipse(normalPen, x1 - 1, y1 - 1, txtWidth + 2, font.Height + 2);
            }
        }
    }





    public class OrderLineDrawerController : ChartDrawerController
    {
        Color normalColor;
        Brush normalBrush;
        Pen normalPen;

        Color drawdownColor;
        Brush drawdownBrush;
        Pen drawdownPen;

        protected readonly List<Point> normalPoints;
        protected readonly List<Point> drawdownPoints;

        public OrderLineDrawerController(Color normalColor, Color drawdownColor)
        {
            NormalColor = normalColor;
            DrawdownColor = drawdownColor;
            normalPoints = new List<Point>();
            drawdownPoints = new List<Point>();
        }

        public Color NormalColor
        {
            get { return normalColor; }
            private set
            {
                normalColor = value;
                normalBrush = new SolidBrush(normalColor);
                normalPen = new Pen(normalBrush);
            }
        }

        public Color DrawdownColor
        {
            get { return drawdownColor; }
            private set
            {
                drawdownColor = value;
                drawdownBrush = new SolidBrush(drawdownColor);
                drawdownPen = new Pen(drawdownBrush);
            }
        }

        public override void DrawStarted(ControllerPaintEventArgs e)
        {
            normalPoints.Clear();
            drawdownPoints.Clear();
        }

        public override void DrawFinished(ControllerPaintEventArgs e)
        {
            GraphicsController g = e.Graphics;
            draw_curve(g, normalPen, normalPoints);
            draw_curve(g, drawdownPen, drawdownPoints);
        }

        void draw_curve(GraphicsController g, Pen pen, List<Point> points)
        {
            if (points.Count > 0)
            {
                try
                {
                    g.DrawCurve(pen, points.ToArray(), 0);
                }
                catch (OverflowException)
                {
                }
                catch (ArgumentException)
                {
                }
                points.Clear();
            }
        }

        public void Draw(GraphicsController g, int x1, int x2, int ybalance, bool isDrawdown, bool isSelected)
        {
            int half = (x2 - x1) / 2;
            if (isDrawdown)
            {
                if (normalPoints.Count > 0)
                {
                    drawdownPoints.Add(normalPoints[normalPoints.Count - 1]);
                }
                drawdownPoints.Add(new Point(x1 + half, ybalance));
                if (isSelected)
                {
                    g.DrawPie(drawdownPen, x1 + half - 2, ybalance - 2, 4, 4, 0, 360);
                }
            }
            else
            {
                if (drawdownPoints.Count > 0)
                {
                    normalPoints.Add(drawdownPoints[drawdownPoints.Count - 1]);
                }
                normalPoints.Add(new Point(x1 + half, ybalance));
                if (isSelected)
                {
                    g.DrawPie(normalPen, x1 + half - 2, ybalance - 2, 4, 4, 0, 360);
                }
            }

        }

    }










    public class CursorLineDrawerController : ChartDrawerController
    {
        Color normalColor;
        Brush normalBrush;
        Pen normalPen;

        Color inactiveColor;
        Brush inactiveBrush;
        Pen inactivePen;

        protected readonly List<Point> normalPoints;
        protected readonly List<Point> inactivePoints;

        public CursorLineDrawerController(Color normalColor, Color inactiveColor)
        {
            NormalColor = normalColor;
            InactiveColor = inactiveColor;
            normalPoints = new List<Point>();
            inactivePoints = new List<Point>();
        }

        public Color NormalColor
        {
            get { return normalColor; }
            private set
            {
                normalColor = value;
                normalBrush = new SolidBrush(normalColor);
                normalPen = new Pen(normalBrush);
            }
        }

        public Color InactiveColor
        {
            get { return inactiveColor; }
            private set
            {
                inactiveColor = value;
                inactiveBrush = new SolidBrush(inactiveColor);
                inactivePen = new Pen(inactiveBrush);
            }
        }

        public override void DrawStarted(ControllerPaintEventArgs e)
        {
            normalPoints.Clear();
            inactivePoints.Clear();
        }

        public override void DrawFinished(ControllerPaintEventArgs e)
        {
            GraphicsController g = e.Graphics;
            draw_curve(g, normalPen, normalPoints);
            draw_curve(g, inactivePen, inactivePoints);
        }

        void draw_curve(GraphicsController g, Pen pen, List<Point> points)
        {
            if (points.Count > 0)
            {
                try
                {
                    g.DrawCurve(pen, points.ToArray(), 0);
                }
                catch (OverflowException)
                {
                }
                catch (ArgumentException)
                {
                }
                points.Clear();
            }
        }

        public void Draw(GraphicsController g, int x1, int x2, int yclose, bool isSelected, bool isActive)
        {
            int half = (x2 - x1) / 2;

            if (isActive)
            {
                if (inactivePoints.Count > 0)
                {
                    normalPoints.Add(inactivePoints[inactivePoints.Count - 1]);
                }
                normalPoints.Add(new Point(x1 + half, yclose));
                if (isSelected)
                {
                    g.DrawPie(normalPen, x1 + half - 2, yclose - 2, 4, 4, 0, 360);
                }
            }
            else
            {
                if (normalPoints.Count > 0)
                {
                    inactivePoints.Add(normalPoints[normalPoints.Count - 1]);
                }
                inactivePoints.Add(new Point(x1 + half, yclose));
                if (isSelected)
                {
                    g.DrawPie(inactivePen, x1 + half - 2, yclose - 2, 4, 4, 0, 360);
                }
            }

        }

    }
}
