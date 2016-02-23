using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;

namespace GreenZoneFxEngine
{
    public partial class OrdersOverviewPanel : UserControl
    {

        public OrdersOverviewPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, OrdersOverviewController controller)
        {
            new SimpleControlVCBinder(context, this, controller);

            orderChart1.Bind(context, controller.OrderChart1);
            ordersToolbar1.Bind(context, controller.OrdersToolbar1);
        }

    }
}
