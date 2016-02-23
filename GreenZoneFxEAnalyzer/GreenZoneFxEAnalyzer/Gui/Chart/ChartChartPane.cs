using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.ComponentModel;
using GreenZoneFxEngine.Types;
using System.Drawing.Drawing2D;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Properties;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.ViewController.Chart;

namespace GreenZoneFxEngine.Gui.Chart
{
    abstract class ChartChartPane : ChartPane
    {
        ChartChartPaneController controller;

        public void Bind(GreenWinFormsMVContext context, ChartChartPaneController controller)
        {
            this.controller = controller;
            base.Bind(context, controller);
        }


    }
}
