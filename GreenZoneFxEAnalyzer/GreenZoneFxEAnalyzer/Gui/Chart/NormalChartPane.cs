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
    class NormalChartPane : ChartChartPane
    {
        public void Bind(GreenWinFormsMVContext context, NormalChartPaneController controller)
        {
            base.Bind(context, controller);
        }

    }
}
