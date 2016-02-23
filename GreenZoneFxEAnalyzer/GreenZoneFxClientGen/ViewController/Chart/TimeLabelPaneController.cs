using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class TimeLabelPaneController : ClientTimeLabelPaneControllerBase
    {
        public TimeLabelPaneController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }


        protected override void OnPaint(ControllerPaintEventArgs e)
        {
            base.OnPaint(e);
            if (Parent != null && Parent.Owner != null)
            {
                string f = Parent.Owner.SymbolFormat;

                foreach (TimeLabelX l in Parent.TimeLabelXsUpper)
                {
                    e.Graphics.DrawString(l.formattedTime, Fonts[l.importance], StringBrush, l.screenX, 2, StringFormat);
                }
                foreach (TimeLabelX l in Parent.TimeLabelXsLower)
                {
                    try
                    {
                        e.Graphics.DrawLine(Parent.MasterChartSectionPanel.ChartPane.GridPens[l.importance], l.screenX + 1, 0, l.screenX + 1, Font.Height);
                    }
                    catch (OverflowException)
                    {
                    }
                    e.Graphics.DrawString(l.formattedTime, Fonts[l.importance], StringBrush, l.screenX, Font.Height + 2, StringFormat);
                }
            }
        }
    }
}
