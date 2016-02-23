using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneFxEngine.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class NormalChart : NonCursorChart
    {

        public NormalChart()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, NormalChartController controller)
        {
            base.Bind(context, controller);
            //NOTE No! No! No!  bound already from Chart.WormBinder.AddChild
            //masterChartSectionPanel.Bind(context, (NormalChartSectionPanelController)controller.MasterChartSectionPanel);
        }

    }
}
