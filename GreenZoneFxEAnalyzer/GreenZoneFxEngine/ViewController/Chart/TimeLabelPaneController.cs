using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class TimeLabelPaneController : TimeLabelPaneControllerBase
    {

        public TimeLabelPaneController(GreenRmiManager rmiManager, IServerChartController parent)
            : base(rmiManager, (Controller)parent)
        {
            StringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
            ForeColor = Color.Black;
            SupportsPaint = true;
        }
        public TimeLabelPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
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
                Font[] fonts = new Font[7];
                fonts[0] = base.Font;
                fonts[1] = base.Font;
                fonts[2] = base.Font;
                fonts[3] = base.Font;
                fonts[4] = base.Font;
                fonts[5] = new Font(base.Font, FontStyle.Bold);
                fonts[6] = new Font(base.Font, FontStyle.Bold);
                Fonts = fonts;
            }
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
                StringBrush = new SolidBrush(ForeColor);
            }
        }

    }
}
