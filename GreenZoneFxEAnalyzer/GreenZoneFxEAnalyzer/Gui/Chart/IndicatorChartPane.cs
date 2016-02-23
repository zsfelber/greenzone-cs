using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.ViewController.Chart;

namespace GreenZoneFxEngine.Gui.Chart
{
    class IndicatorChartPane : ChartChartPane
    {
        public void Bind(GreenWinFormsMVContext context, IndicatorChartPaneController controller)
        {
            base.Bind(context, controller);
        }

    }
}
