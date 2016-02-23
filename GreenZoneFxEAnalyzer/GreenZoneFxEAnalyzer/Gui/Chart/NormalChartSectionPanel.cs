using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.ViewController.Chart;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class NormalChartSectionPanel : ChartSectionPanel
    {

        public NormalChartSectionPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, NormalChartSectionPanelController controller)
        {
            base.Bind(context, controller);
            chartPane1.Bind(context, (NormalChartPaneController)controller.ChartPane);
        }
    }
}
