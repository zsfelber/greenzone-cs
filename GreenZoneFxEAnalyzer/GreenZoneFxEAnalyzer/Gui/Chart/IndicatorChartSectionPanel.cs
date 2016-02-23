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
    public partial class IndicatorChartSectionPanel : ChartSectionPanel
    {
        public IndicatorChartSectionPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, IndicatorChartSectionPanelController controller)
        {
            base.Bind(context, controller);
            chartPane1.Bind(context, (IndicatorChartPaneController)controller.ChartPane);
            new ButtonVCBinder(context, closeButton, controller.CloseButton);
            new ButtonVCBinder(context, propertiesButton, controller.PropertiesButton);
        }
    }
}
