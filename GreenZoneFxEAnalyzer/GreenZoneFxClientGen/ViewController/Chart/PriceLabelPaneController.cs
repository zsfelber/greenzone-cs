using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class PriceLabelPaneController : ClientPriceLabelPaneControllerBase
    {
        public PriceLabelPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected override void OnPaint(ControllerPaintEventArgs e)
        {
            base.OnPaint(e);
            if (Parent != null && Parent.Parent != null && Parent.Owner != null)
            {
                string f = Parent.PriceFormat;

                foreach (PriceLabelY l in Parent.PriceLabelYs)
                {
                    try
                    {
                        e.Graphics.DrawString(l.levelPrice.ToString(f), Fonts[l.importance / 2], StringBrush, 2, l.screenY + offset(l.screenY), StringFormat);
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
            double f = -d * Font.Height;
            return (int)f;
        }
    }
}
