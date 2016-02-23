using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class OrderChartSectionPanelController : ServerOrderChartSectionPanelControllerBase
    {
        public OrderChartSectionPanelController(GreenRmiManager rmiManager, OrderChartController parent, ServerOrdersHistoryView ordersView)
            : base(rmiManager, parent)
        {
            PropertiesButton = new ButtonController(rmiManager, this);

            PropertiesButton.Pressed += new ControllerEventHandler(propertiesButton_Click);
        }

        protected override IPriceLabelPaneController CreatePriceLabelPaneController()
        {
            return new PriceLabelPaneController(rmiManager, this);
        }

        public override IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.SEPARATE_WINDOW;
            }
        }

        public override string PriceFormat
        {
            get
            {
                return OrdersView.SymbolFormat;
            }
        }

        public override int Scale
        {
            get
            {
                return 0;
            }
        }

        public new OrdersOverviewController OrdersOverviewPanel
        {
            get
            {
                return (OrdersOverviewController)Parent.OrdersOverviewPanel;
            }
        }

        protected override void CreateChartPane(ServerChartControllerEx parent)
        {
            ChartPane = new OrderChartPaneController(rmiManager, this, (OrderChartController)parent);
        }

        private void propertiesButton_Click(object sender, ControllerEventArgs e)
        {
            OrdersOverviewPanel.ShowProperties();
        }
    }


}
