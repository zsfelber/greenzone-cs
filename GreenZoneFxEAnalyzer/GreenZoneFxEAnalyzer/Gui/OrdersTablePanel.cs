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
using GreenZoneFxEngine.Gui.Orders;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.ViewController.Orders;

namespace GreenZoneFxEngine
{
    public partial class OrdersTablePanel : UserControl
    {

        public OrdersTablePanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, OrdersTableController controller)
        {
            this.context = context;
            this.controller = controller;
            new SimpleControlVCBinder(context, this, controller);

            ordersToolbar1.Bind(context, controller.OrdersToolbar1);
            new WormBinder(this);
        }

        GreenWinFormsMVContext context;
        public GreenWinFormsMVContext Context
        {
            get
            {
                return context;
            }
        }

        OrdersTableController controller;
        public OrdersTableController Controller
        {
            get
            {
                return controller;
            }
        }

        class WormBinder : WormSplitContainerVCBinder
        {
            internal WormBinder(OrdersTablePanel form)
                : base(form.context, form.wormSplitContainer1, form.controller.WormSplitContainer1)
            {
            }

            protected override void AddChild(Controller child1)
            {
                OrdersGridController p = (OrdersGridController)child1;
                OrdersGridView panel = new OrdersGridView();
                panel.Bind((GreenWinFormsMVContext)context, p);
                control.Add(panel);
            }
        }
    }
}
