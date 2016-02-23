using System.Windows.Forms;
using System.Drawing;
using System;

namespace GreenZoneFxEngine.Gui.Chart
{
    public class TimeLabelPane : Control
    {
        Chart parent;
        Brush stringBrush;
        StringFormat stringFormat;
        Font[] fonts;

        public TimeLabelPane()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            ForeColor = SystemColors.ControlText;
            stringBrush = new SolidBrush(ForeColor);

            stringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
        }

        internal void Init(Chart parent)
        {
            this.parent = parent;
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
                fonts = new Font[7];
                fonts[0] = base.Font;
                fonts[1] = base.Font;
                fonts[2] = base.Font;
                fonts[3] = base.Font;
                fonts[4] = base.Font;
                fonts[5] = new Font(base.Font, FontStyle.Bold);
                fonts[6] = new Font(base.Font, FontStyle.Bold);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (parent != null && parent.Owner != null)
            {
                string f = parent.Owner.SymbolFormat;

                foreach (TimeLabelX l in parent.timeLabelXsUpper)
                {
                    e.Graphics.DrawString(l.formattedTime, fonts[l.importance], stringBrush, l.screenX, 2, stringFormat);
                }
                foreach (TimeLabelX l in parent.timeLabelXsLower)
                {
                    try
                    {
                        e.Graphics.DrawLine(parent.MasterChartSectionPanel.ChartPane.gridPens[l.importance], l.screenX+1, 0, l.screenX+1, Font.Height);
                    }
                    catch (OverflowException)
                    {
                    }
                    e.Graphics.DrawString(l.formattedTime, fonts[l.importance], stringBrush, l.screenX, Font.Height + 2, stringFormat);
                }
            }
        }
    }
}
