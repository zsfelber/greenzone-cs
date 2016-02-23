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
    public partial class CursorChartSectionPanel : ChartSectionPanel
    {

        public CursorChartSectionPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, CursorChartSectionPanelController controller)
        {
            base.Bind(context, controller);
            chartPane1.Bind(context, (CursorChartPaneController)controller.ChartPane);
        }
    }
}
