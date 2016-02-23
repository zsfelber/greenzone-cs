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
    class OrderChartPane : ChartPane
    {

        public void Bind(GreenWinFormsMVContext context, OrderChartPaneController controller)
        {
            base.Bind(context, controller);
        }

    }
}
