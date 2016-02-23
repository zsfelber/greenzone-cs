using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class OrderChartPaneController : ServerOrderChartPaneControllerBase
    {

        internal OrderChartPaneController(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, OrderChartController chart)
            : base(rmiManager, parent, chart)
        {
        }

        new ServerChartSectionPanelControllerEx Parent
        {
            get
            {
                return (ServerChartSectionPanelControllerEx)parent;
            }
        }

        new OrderChartController Chart
        {
            get
            {
                return (OrderChartController)base.Chart;
            }
        }

        ServerOrdersHistoryView OrdersView
        {
            get
            {
                return Chart.OrdersView;
            }
        }


        protected override void LayOut()
        {
            throw new NotImplementedException();
        }
    }



}
