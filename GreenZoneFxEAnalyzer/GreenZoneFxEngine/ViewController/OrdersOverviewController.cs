using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class OrdersOverviewController : OrdersOverviewControllerBase, IOrdersTabPageControllerEx
    {
        bool updateOrdersNextTime = false;

        internal OrdersOverviewController(GreenRmiManager rmiManager, MainWindowController mainWindow, ServerEnvironmentRuntime environment) :
            base(rmiManager, (TabController)null)
        {
            Text = "Orders graph";
            MainWindow = mainWindow;
            this.environment = environment;
            OrderChart1 = new OrderChartController(rmiManager, this);
            ordersHistoryView = new ServerOrdersHistoryView(environment.OrdersTable, environment.Session.OrderFilter);
            OrdersToolbar1 = new OrdersToolbarController(rmiManager, this);

            UpdateOrders();
            OrdersToolbar1.CloseChartButton2.Pressed += new ControllerEventHandler(closeChartButton2_Click);
            mainWindow.TabControl1.SelectedIndexChanged += new PropertyChangedEventHandler(tabControl1_SelectedIndexChanged);
            ordersHistoryView.HistoryChanged += new ServerOrdersHistoryView.DHistoryChanged(ordersHistoryView_HistoryChanged);
        }

        internal void ShowProperties()
        {
        }

        private void closeChartButton2_Click(object sender, ControllerEventArgs e)
        {
            Environment.Session.IsOrdersOverviewVisible = false;
            MainWindow.UpdateOrdersOverviewController();
            MainWindow.SaveSession();
        }

        void tabControl1_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            if (MainWindow.TabControl1.SelectedTab == this)
            {
                OrdersToolbar1.UpdateFilterControls();
                if (updateOrdersNextTime)
                {
                    updateOrdersNextTime = false;
                    UpdateOrders();
                }
            }
        }

        void ordersHistoryView_HistoryChanged(bool groupsChanged)
        {
            if (MainWindow.TabControl1.SelectedTab == this)
            {
                UpdateOrders(groupsChanged);
            }
            else
            {
                updateOrdersNextTime = true;
            }
        }

        internal void UpdateOrders(bool groupsChanged = true)
        {
            if (groupsChanged)
            {
                OrderChart1.UpdateFilterPanels();
            }
            else
            {
                OrderChart1.UpdateSeries();
            }
        }
    }
}
