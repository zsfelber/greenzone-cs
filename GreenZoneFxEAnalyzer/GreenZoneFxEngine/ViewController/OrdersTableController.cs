using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{

    public class OrdersTableController : OrdersTableControllerBase, IOrdersTabPageControllerEx
    {
        List<Dictionary<string, GridColumnController>> colsByName = new List<Dictionary<string, GridColumnController>>();

        bool updateOrdersNextTime = false;

        internal OrdersTableController(GreenRmiManager rmiManager, MainWindowController mainWindow, ServerEnvironmentRuntime environment)
            : base(rmiManager, (TabController)null)
        {
            Text = "Orders table";

            WormSplitContainer1 = new MultiSplitController(rmiManager, this);
            ordersHistoryView = new ServerOrdersHistoryView(environment.OrdersTable, environment.Session.OrderFilter);
            OrdersToolbar1 = new OrdersToolbarController(rmiManager, this);

            UpdateOrders();
            OrdersToolbar1.CloseChartButton2.Pressed += new ControllerEventHandler(closeChartButton2_Click);
            mainWindow.TabControl1.SelectedIndexChanged += new PropertyChangedEventHandler(TabControl1_SelectedIndexChanged);
            ordersHistoryView.HistoryChanged += new ServerOrdersHistoryView.DHistoryChanged(ordersHistoryView_HistoryChanged);
        }

        public new MainWindowController MainWindow
        {
            get
            {
                return (MainWindowController)base.MainWindow;
            }
            protected set
            {
                base.MainWindow = value;
            }
        }

        readonly ServerEnvironmentRuntime environment;
        public ServerEnvironmentRuntime Environment
        {
            get
            {
                return environment;
            }
        }

        readonly ServerOrdersHistoryView ordersHistoryView;
        public ServerOrdersHistoryView Orders
        {
            get
            {
                return ordersHistoryView;
            }
        }


        public new OrdersToolbarController OrdersToolbar1
        {
            get
            {
                return (OrdersToolbarController)base.OrdersToolbar1;
            }
            protected set
            {
                base.OrdersToolbar1 = value;
            }
        }

        private void closeChartButton2_Click(object sender, ControllerEventArgs e)
        {
            Environment.Session.IsOrdersTableVisible = false;
            MainWindow.UpdateOrdersTableController();
            MainWindow.SaveSession();
        }

        bool _TabControl1_SelectedIndexChanged = false;
        void TabControl1_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            _TabControl1_SelectedIndexChanged = true;
            try
            {
                if (MainWindow.TabControl1.SelectedTab == this)
                {
                    OrdersToolbar1.UpdateFilterControls();
                    foreach (var cols in colsByName)
                    {
                        foreach (var col in cols.Values)
                        {
                            col.Visible = true;
                        }
                    }
                    foreach (var c in ordersHistoryView.Filter.HiddenColumns)
                    {
                        foreach (var cols in colsByName)
                        {
                            cols[c].Visible = false;
                        }
                    }
                    foreach (OrdersGridController view in WormSplitContainer1.Controls)
                    {
                        view.UpdateVisibleColumnsPopup();
                    }
                    if (updateOrdersNextTime)
                    {
                        updateOrdersNextTime = false;
                        UpdateOrders();
                    }
                }
            }
            finally
            {
                _TabControl1_SelectedIndexChanged = false;
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

        bool _ordersGridView1_ColumnStateChanged = false;
        void grid_ColumnVisibleChanged(object sender, ControllerEventArgs e)
        {
            if (!_TabControl1_SelectedIndexChanged && !_ordersGridView1_ColumnStateChanged)
            {
                _ordersGridView1_ColumnStateChanged = true;

                try
                {
                    SortedSet<string> hiddenColumns = ordersHistoryView.Filter.HiddenColumns;

                    GridColumnController column = (GridColumnController)sender;
                    if (column.Visible)
                    {
                        hiddenColumns.Add(column.DataPropertyName);
                    }
                    else
                    {
                        hiddenColumns.Remove(column.DataPropertyName);
                    }
                    foreach (var cols in colsByName)
                    {
                        cols[column.DataPropertyName].Visible = column.Visible;
                    }
                }
                finally
                {
                    _ordersGridView1_ColumnStateChanged = false;
                }
            }
        }

        internal void UpdateOrders(bool groupsChanged = true)
        {
            if (groupsChanged)
            {

                colsByName.Clear();
                WormSplitContainer1.Clear();

                add_grid(ordersHistoryView);

                if (!string.IsNullOrEmpty(ordersHistoryView.Filter.GroupBy))
                {
                    foreach (object g in ordersHistoryView.Groups)
                    {
                        OrderFilter filter = ordersHistoryView.Filter.Clone();
                        filter.GroupField = g;
                        ServerOrdersHistoryView view = new ServerOrdersHistoryView(ordersHistoryView.Parent, filter);

                        add_grid(view);
                    }
                }

            }
            else
            {
                // Nothing to do
                // HistoryChanged listener is active in grids
            }
        }

        public void add_grid(ServerOrdersHistoryView view)
        {
            Dictionary<string, GridColumnController> cols = new Dictionary<string, GridColumnController>();
            colsByName.Add(cols);

            OrdersGridController grid = new OrdersGridController(rmiManager, this);
            foreach (GridColumnController col in grid.Columns)
            {
                cols[col.DataPropertyName] = col;
            }
            grid.View = view;
            WormSplitContainer1.Add(grid);

            grid.ColumnVisibleChanged += new PropertyChangedEventHandler(grid_ColumnVisibleChanged);
        }
    }
}
