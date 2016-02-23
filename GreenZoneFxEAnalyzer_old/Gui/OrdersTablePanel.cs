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

namespace GreenZoneFxEngine
{
    public partial class OrdersTablePanel : UserControl, IOrdersTabPanel
    {
        private Form1 parent;
        List<Dictionary<string, DataGridViewColumn>> colsByName = new List<Dictionary<string, DataGridViewColumn>>();
        OrdersHistoryView ordersHistoryView;
        bool updateOrdersNextTime = false;

        public OrdersTablePanel()
        {
            InitializeComponent();
        }

        internal void Init(Form1 parent, EnvironmentRuntime environment)
        {
            this.parent = parent;
            ordersHistoryView = new OrdersHistoryView(environment.OrdersTable, environment.Session.OrderFilter);

            Environment = environment;
            TabPage = new TabPage();
            TabPage.BackColor = SystemColors.Control;
            TabPage.Padding = new Padding(0);

            TabPage.Controls.Add(this);
            TabPage.Text = "Orders";

            ordersToolbar1.Init(this);

            UpdateOrders();
            ordersToolbar1.closeChartButton2.Click += new EventHandler(closeChartButton2_Click);
            parent.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            ordersHistoryView.HistoryChanged += new OrdersHistoryView.DHistoryChanged(ordersHistoryView_HistoryChanged);
        }

        public OrdersHistoryView Orders
        {
            get
            {
                return ordersHistoryView;
            }
        }

        private void closeChartButton2_Click(object sender, EventArgs e)
        {
            Environment.Session.IsOrdersTableVisible = false;
            parent.UpdateOrdersTablePanel();
            parent.SaveSession();
        }

        bool _tabControl1_SelectedIndexChanged = false;
        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tabControl1_SelectedIndexChanged = true;
            SuspendLayout();
            TWinFct.LockControlUpdate(this);
            try
            {
                if (parent.tabControl1.SelectedTab == TabPage)
                {
                    ordersToolbar1.UpdateFilterControls();
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
                    foreach (OrdersGridView view in wormSplitContainer1.ChildControls)
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
                _tabControl1_SelectedIndexChanged = false;
                TWinFct.UnLockControlUpdate(this);
                ResumeLayout();
            }
        }

        void ordersHistoryView_HistoryChanged(bool groupsChanged)
        {
            if (parent.tabControl1.SelectedTab == TabPage)
            {
                UpdateOrders(groupsChanged);
            }
            else
            {
                updateOrdersNextTime = true;
            }
        }

        bool _ordersGridView1_ColumnStateChanged = false;
        void ordersGridView1_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            if (!_tabControl1_SelectedIndexChanged && !_ordersGridView1_ColumnStateChanged && e.StateChanged == DataGridViewElementStates.Visible)
            {
                _ordersGridView1_ColumnStateChanged = true;
                SuspendLayout();
                TWinFct.LockControlUpdate(this);

                try
                {
                    SortedSet<string> hiddenColumns = ordersHistoryView.Filter.HiddenColumns;

                    DataGridViewColumn column = (DataGridViewColumn)sender;
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
                    TWinFct.UnLockControlUpdate(this);
                    ResumeLayout();
                }
            }
        }

        internal void UpdateOrders(bool groupsChanged = true)
        {
            if (groupsChanged)
            {
                SuspendLayout();
                TWinFct.LockControlUpdate(this);

                try
                {
                    colsByName.Clear();
                    wormSplitContainer1.Clear();

                    add_grid(ordersHistoryView);

                    if (!string.IsNullOrEmpty(ordersHistoryView.Filter.GroupBy))
                    {
                        foreach (object g in ordersHistoryView.Groups)
                        {
                            OrderFilter filter = ordersHistoryView.Filter.Clone();
                            filter.GroupField = g;
                            OrdersHistoryView view = new OrdersHistoryView(ordersHistoryView.Parent, filter);

                            add_grid(view);
                        }
                    }

                }
                finally
                {
                    TWinFct.UnLockControlUpdate(this);
                    ResumeLayout();
                }
            }
            else
            {
                // Nothing to do
                // HistoryChanged listener is active in grids
            }
        }

        public void add_grid(OrdersHistoryView view)
        {
            Dictionary<string, DataGridViewColumn> cols = new Dictionary<string, DataGridViewColumn>();
            colsByName.Add(cols);

            OrdersGridView grid = new OrdersGridView();
            foreach (DataGridViewColumn col in grid.Columns)
            {
                cols[col.DataPropertyName] = col;
            }
            grid.View = view;
            wormSplitContainer1.Add(grid);

            grid.ColumnStateChanged += new DataGridViewColumnStateChangedEventHandler(ordersGridView1_ColumnStateChanged);
        }

        public Form1 Form1
        {
            get
            {
                return parent;
            }
        }

        public TabPage TabPage
        {
            get;
            private set;
        }

        public EnvironmentRuntime Environment
        {
            get;
            internal set;
        }

        public void DockIt()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
        }




    }
}
