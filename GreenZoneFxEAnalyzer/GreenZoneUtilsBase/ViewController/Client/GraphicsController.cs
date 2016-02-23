using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GreenZoneUtil.ViewController
{
    public abstract class GraphicsController
    {

        public abstract SmoothingMode SmoothingMode
        {
            get;
            set;
        }

        public abstract void DrawLine(Pen pen, int x1, int y1, int x2, int y2);

        public abstract void DrawString(string s, Font font, Brush brush, float x, float y);

        public abstract void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat stringFormat);

        public abstract void DrawCurve(Pen pen, Point[] points, float tension);

        public abstract void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle);

        public abstract void DrawEllipse(Pen pen, int x, int y, int width, int height);

        public abstract void DrawRectangle(Pen pen, int x, int y, int width, int height);

        public abstract void DrawRectangle(Pen pen, Rectangle rect);

        public abstract void FillRectangle(Brush brush, int x, int y, int width, int height);

        public abstract void FillRectangle(Brush brush, Rectangle r);

    }

    public class OfflineGraphicsController : GraphicsController
    {
        public OfflineGraphicsController()
        {
        }

        SmoothingMode smoothingMode;
        public override SmoothingMode SmoothingMode
        {
            get
            {
                return smoothingMode;
            }
            set
            {
                smoothingMode = value;
            }
        }

        public override void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            //TODO
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            //TODO
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat stringFormat)
        {
            //TODO
        }

        public override void DrawCurve(Pen pen, Point[] points, float tension)
        {
            //TODO
        }

        public override void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            //TODO
        }

        public override void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            //TODO
        }

        public override void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            //TODO
        }

        public override void DrawRectangle(Pen pen, Rectangle rect)
        {
            //TODO
        }

        public override void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            //TODO
        }

        public override void FillRectangle(Brush brush, Rectangle r)
        {
            //TODO
        }
    }

    public class WinFormsGraphicsController : GraphicsController
    {
        public WinFormsGraphicsController(Graphics graphics)
        {
            this.graphics = graphics;
        }

        readonly Graphics graphics;
        public Graphics Graphics
        {
            get
            {
                return graphics;
            }
        }

        public override SmoothingMode SmoothingMode
        {
            get
            {
                return graphics.SmoothingMode;
            }
            set
            {
                graphics.SmoothingMode = value;
            }
        }

        public override void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            graphics.DrawString(s, font, brush, x, y);
        }

        public override void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat stringFormat)
        {
            graphics.DrawString(s, font, brush, x, y, stringFormat);
        }

        public override void DrawCurve(Pen pen, Point[] points, float tension)
        {
            graphics.DrawCurve(pen, points, tension);
        }

        public override void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            graphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        public override void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            graphics.DrawEllipse(pen, x, y, width, height);
        }

        public override void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            graphics.DrawRectangle(pen, x, y, width, height);
        }

        public override void DrawRectangle(Pen pen, Rectangle rect)
        {
            graphics.DrawRectangle(pen, rect);
        }

        public override void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            graphics.FillRectangle(brush, x, y, width, height);
        }

        public override void FillRectangle(Brush brush, Rectangle r)
        {
            graphics.FillRectangle(brush, r);
        }
    }
}
