using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System;

namespace GreenZoneFxEngine.Gui.Chart
{
    class PriceLabelPane : Control
    {
        Chart chart;
        ChartSectionPanel parent;
        Brush stringBrush;
        StringFormat stringFormat;
        Font[] fonts;

        public PriceLabelPane()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            ForeColor = SystemColors.ControlText;
            stringBrush = new SolidBrush(ForeColor);

            stringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
        }

        internal void Init(Chart chart, ChartSectionPanel parent)
        {
            this.chart = chart;
            this.parent = parent;
            
            string f = parent.PriceFormat;
            if (f.Length > 7)
            {
                Font = new Font("Arial Narrow", 7);
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
                fonts = new Font[3];
                fonts[0] = base.Font;
                string f = parent==null?null:parent.PriceFormat;
                if (parent != null && f.Length > 7)
                {
                    fonts[1] = new Font(base.Font, FontStyle.Italic);
                    fonts[2] = new Font(base.Font, FontStyle.Italic | FontStyle.Underline);
                }
                else
                {
                    fonts[1] = new Font(base.Font, FontStyle.Bold);
                    fonts[2] = new Font(base.Font, FontStyle.Bold | FontStyle.Underline);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (chart != null && chart.Owner != null)
            {
                string f = parent.PriceFormat;

                foreach (PriceLabelY l in parent.priceLabelYs)
                {
                    try
                    {
                        e.Graphics.DrawString(l.levelPrice.ToString(f), fonts[l.importance / 2], stringBrush, 2, l.screenY + offset(l.screenY), stringFormat);
                    }
                    catch (OverflowException)
                    {
                    }
                }
            }
        }

        private int offset(int y)
        {
            double d = y / (double)Height;
            double f = - d * Font.Height;
            return (int)f;
        }
    }
}
