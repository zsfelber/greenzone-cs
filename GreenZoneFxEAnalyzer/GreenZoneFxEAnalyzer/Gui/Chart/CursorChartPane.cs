using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneFxEngine.ViewController.Chart;

namespace GreenZoneFxEngine.Gui.Chart
{
    class CursorChartPane : ChartPane
    {
        CursorChartPaneController ccontroller;

        public void Bind(GreenWinFormsMVContext context, CursorChartPaneController controller)
        {
            ccontroller = controller;
            base.Bind(context, controller);
        }

        protected override void UpdateMovingCursors(Point p)
        {
            if (ccontroller.ChartFrameRect.Contains(p))
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
