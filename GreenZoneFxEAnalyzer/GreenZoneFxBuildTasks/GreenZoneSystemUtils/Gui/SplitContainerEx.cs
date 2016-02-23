using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GreenZoneSystemUtils.Properties;

namespace GreenZoneUtil.Gui
{
    public partial class SplitContainerEx : SplitContainer
    {
        const int imgw = 50;
        const int imgh = 6;

        public SplitContainerEx()
        {
            TabStop = false;
            SetStyle(ControlStyles.Selectable, false);
        }

        public int DividedLength
        {
            get
            {
                return Orientation == Orientation.Horizontal ? Height : Width;
            }
        }

        public int AnotherLength
        {
            get
            {
                return Orientation == Orientation.Horizontal ? Width : Height;
            }
        }

        public new bool IsSplitterFixed
        {
            get
            {
                return base.IsSplitterFixed;
            }
            set
            {
                base.IsSplitterFixed = value;

                if (value)
                {
                    SplitterWidth = imgh;
                }
            }
        }

        CollapseButtons collapseButtons;
        public CollapseButtons CollapseButtons
        {
            get
            {
                return collapseButtons;
            }
            set
            {
                collapseButtons = value;
                Invalidate();
            }
        }

        int panel1NormalSize;
        public int Panel1NormalSize
        {
            get
            {
                return panel1NormalSize;
            }
            set
            {
                panel1NormalSize = value;
                if (value != 0)
                {
                    Panel1MinSize = 0;
                }
            }
        }

        int panel2NormalSize;
        public int Panel2NormalSize
        {
            get
            {
                return panel2NormalSize;
            }
            set
            {
                panel2NormalSize = value;
                if (value != 0)
                {
                    Panel2MinSize = 0;
                }
            }
        }

        bool panel1CollapsedByClient;
        public bool Panel1CollapsedByClient
        {
            get
            {
                if (SplitterDistance == 0)
                {
                    panel1CollapsedByClient = true;
                }
                return panel1CollapsedByClient;
            }
            set
            {
                panel1CollapsedByClient = value;
                if (value)
                {
                    SplitterDistance = 0;
                }
                else
                {
                    SplitterDistance = panel1NormalSize;
                }
            }
        }

        bool panel2CollapsedByClient;
        public bool Panel2CollapsedByClient
        {
            get
            {
                var dl = DividedLength;
                var sW = this.SplitterWidth;
                if (SplitterDistance == dl - sW)
                {
                    panel2CollapsedByClient = true;
                }
                return panel2CollapsedByClient;
            }
            set
            {
                panel2CollapsedByClient = value;
                if (value)
                {
                    var dl = DividedLength;
                    var sW = this.SplitterWidth;
                    SplitterDistance = dl - sW;
                }
                else
                {
                    var dl = DividedLength;
                    var sW = this.SplitterWidth;
                    SplitterDistance = dl - panel2NormalSize - sW;
                }
            }
        }

        int leftGap;
        public int LeftGap
        {
            get
            {
                return leftGap;
            }
            set
            {
                leftGap = value;
                Invalidate();
            }
        }

        int rightGap;
        public int RightGap
        {
            get
            {
                return rightGap;
            }
            set
            {
                rightGap = value;
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Rectangle r1, r2;
            CalcRects(out r1, out r2);

            bool c1 = r1.Contains(e.Location);
            bool c2 = r2.Contains(e.Location);

            if (c1 || c2)
            {
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Arrow;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Rectangle r1, r2;
                CalcRects(out r1, out r2);

                bool c1 = r1.Contains(e.Location);
                bool c2 = r2.Contains(e.Location);
                bool c = c1 || c2;
                if (Panel1CollapsedByClient && c)
                {
                    Panel1CollapsedByClient = false;
                }
                else if (Panel2CollapsedByClient && c)
                {
                    Panel2CollapsedByClient = false;
                }
                else if ((collapseButtons & CollapseButtons.Panel1) != 0 && c1)
                {
                    Panel1CollapsedByClient = true;
                }
                else if ((collapseButtons & CollapseButtons.Panel2) != 0 && c2)
                {
                    Panel2CollapsedByClient = true;
                }
                else
                {
                    base.OnMouseDown(e);
                }
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Rectangle r1, r2;
            CalcRects(out r1, out r2);

            Image im1, im2;
            if (this.Orientation == Orientation.Horizontal)
            {
                im1 = Resources.fixsplit_up;
                im2 = Resources.fixsplit_down;
            }
            else
            {
                im1 = Resources.fixsplit_left;
                im2 = Resources.fixsplit_right;
            }

            if ((collapseButtons & CollapseButtons.Panel1) != 0)
            {
                if (Panel1CollapsedByClient)
                {
                    g.DrawImage(im2, r1);
                }
                else
                {
                    g.DrawImage(im1, r1);
                }
            }

            if ((collapseButtons & CollapseButtons.Panel2) != 0)
            {
                if (Panel2CollapsedByClient)
                {
                    g.DrawImage(im1, r2);
                }
                else
                {
                    g.DrawImage(im2, r2);
                }
            }


            if (!IsSplitterFixed)
            {
                var W = this.Width - leftGap - rightGap;
                var H = this.Height;
                var sW = this.SplitterWidth;
                int d = this.SplitterDistance;

                //paint the three dots'
                Point[] points = new Point[3];

                //calculate the position of the points'
                if (this.Orientation == Orientation.Horizontal)
                {
                    points[0] = new Point(leftGap + (W / 2), d + (sW / 2));
                    points[1] = new Point(points[0].X - 10, points[0].Y);
                    points[2] = new Point(points[0].X + 10, points[0].Y);
                }
                else
                {
                    points[0] = new Point(d + (sW / 2), leftGap + (H / 2));
                    points[1] = new Point(points[0].X, points[0].Y - 10);
                    points[2] = new Point(points[0].X, points[0].Y + 10);
                }

                foreach (Point p in points)
                {
                    p.Offset(-2, -2);
                    g.FillEllipse(SystemBrushes.ControlDark,
                        new Rectangle(p, new Size(3, 3)));

                    p.Offset(1, 1);
                    g.FillEllipse(SystemBrushes.ControlLight,
                        new Rectangle(p, new Size(3, 3)));
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if ((collapseButtons & CollapseButtons.Panel1) != 0)
            {
                if (!Panel1CollapsedByClient && DividedLength != 0)
                {
                    Panel1CollapsedByClient = false;
                }
            }
            if ((collapseButtons & CollapseButtons.Panel2) != 0)
            {
                if (!Panel2CollapsedByClient && DividedLength != 0)
                {
                    Panel2CollapsedByClient = false;
                }
            }
        }

        void CalcRects(out Rectangle r1, out Rectangle r2)
        {
            var w = this.Width - leftGap - rightGap;
            var h = this.Height;
            var d = this.SplitterDistance;

            if (this.Orientation == Orientation.Horizontal)
            {
                if (IsSplitterFixed)
                {
                    if (collapseButtons == CollapseButtons.Both)
                    {
                        r1 = new Rectangle(leftGap + (w / 2) - imgw - 1, d, imgw, imgh);
                        r2 = new Rectangle(leftGap + (w / 2) + 1, d, imgw, imgh);
                    }
                    else
                    {
                        r1 = new Rectangle(leftGap + (w / 2) - imgw/2, d, imgw, imgh);
                        r2 = r1;
                    }
                }
                else
                {
                    r1 = new Rectangle(leftGap + (w / 2) - imgw - 21, d, imgw, imgh);
                    r2 = new Rectangle(leftGap + (w / 2) + 21, d, imgw, imgh);
                }
            }
            else
            {
                if (IsSplitterFixed)
                {
                    if (collapseButtons == CollapseButtons.Both)
                    {
                        r1 = new Rectangle(d, leftGap + (h / 2) - imgh - 1, imgh, imgw);
                        r2 = new Rectangle(d, leftGap + (h / 2) + 1, imgh, imgw);
                    }
                    else
                    {
                        r1 = new Rectangle(d, leftGap + (h / 2) - imgh/2, imgh, imgw);
                        r2 = r1;
                    }
                }
                else
                {
                    r1 = new Rectangle(d, leftGap + (h / 2) - imgh - 21, imgh, imgw);
                    r2 = new Rectangle(d, leftGap + (h / 2) + 21, imgh, imgw);
                }
            }
        }
    }

    [Flags]
    public enum CollapseButtons
    {
        None = 0,
        Panel1 = 1,
        Panel2 = 2,
        Both = 3
    }
}
