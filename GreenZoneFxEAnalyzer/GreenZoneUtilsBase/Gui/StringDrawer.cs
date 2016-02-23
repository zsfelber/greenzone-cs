using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GreenZoneUtil.Gui
{
    class StringDrawer : Control
    {
        Brush brush;

        public StringDrawer()
        {
            brush = new SolidBrush(ForeColor);
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                brush = new SolidBrush(value);
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString(Text, Font, brush, Padding.Left, Padding.Top);
        }
    }
}
