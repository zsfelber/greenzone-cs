using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class PriceLabelPaneController : PriceLabelPaneControllerBase
    {

        public PriceLabelPaneController(GreenRmiManager rmiManager, IServerChartSectionPanelController parent)
            : base(rmiManager, (Controller)parent)
        {
            this.parent = parent;
            StringFormat = new StringFormat(StringFormatFlags.MeasureTrailingSpaces);
            ForeColor = Color.Black;
            string f = parent.PriceFormat;
            if (f.Length > 7)
            {
                Font = new Font("Arial Narrow", 7);
            }
            SupportsPaint = true;
        }

        public PriceLabelPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        readonly ServerChartSectionPanelControllerEx parent;
        public new ServerChartSectionPanelControllerEx Parent
        {
            get
            {
                return parent;
            }
        }

        Font font;
        public override Font Font
        {
            get
            {
                return font;
            }
            set
            {
                base.Font = value;
                font = value;
                Font[] fonts = new Font[3];
                fonts[0] = base.Font;
                if (PlainFonts)
                {
                    fonts[1] = base.Font;
                    fonts[2] = base.Font;
                }
                else
                {
                    string f = parent == null ? null : parent.PriceFormat;
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

        public override bool PlainFonts
        {
            get
            {
                return base.PlainFonts;
            }
            set
            {
                base.PlainFonts = value;
                if (font != null)
                {
                    Font = font;
                }
            }
        }

    }
}
