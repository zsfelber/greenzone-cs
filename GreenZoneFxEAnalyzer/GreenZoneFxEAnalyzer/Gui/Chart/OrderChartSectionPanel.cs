using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class OrderChartSectionPanel : ChartSectionPanel
    {
        public OrderChartSectionPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, OrderChartSectionPanelController controller)
        {
            base.Bind(context, controller);
            chartPane1.Bind(context, (OrderChartPaneController)controller.ChartPane);
            new ButtonVCBinder(context, propertiesButton, controller.PropertiesButton);
        }
    }
}
