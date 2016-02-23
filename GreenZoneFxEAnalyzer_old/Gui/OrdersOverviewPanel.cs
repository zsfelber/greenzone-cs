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

namespace GreenZoneFxEngine
{
    public partial class OrdersOverviewPanel : UserControl, IOrdersTabPanel
    {
        private Form1 parent;
        OrdersHistoryView ordersHistoryView;
        bool updateOrdersNextTime = false;

        public OrdersOverviewPanel()
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
            TabPage.Text = "Trading";

            ordersToolbar1.Init(this);
            orderChart1.Init(this);

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

        internal void ShowProperties()
        {
        }

        private void closeChartButton2_Click(object sender, EventArgs e)
        {
            Environment.Session.IsOrdersOverviewVisible = false;
            parent.UpdateOrdersOverviewPanel();
            parent.SaveSession();
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parent.tabControl1.SelectedTab == TabPage)
            {
                ordersToolbar1.UpdateFilterControls();
                if (updateOrdersNextTime)
                {
                    updateOrdersNextTime = false;
                    UpdateOrders();
                }
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

        internal void UpdateOrders(bool groupsChanged = true)
        {
            if (groupsChanged)
            {
                orderChart1.UpdateFilterPanels();
            }
            else
            {
                orderChart1.UpdateSeries();
            }
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
