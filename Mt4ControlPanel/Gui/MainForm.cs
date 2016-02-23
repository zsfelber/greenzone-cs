using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

namespace Mt4ControlPanelGui
{
    public partial class MainForm : Form
    {
        delegate void DoTaskCallback(object task);

        DoTaskCallback doTaskCallback;

        static MainForm TheForm;
        static Thread AppThread;
        public static readonly object Lock = new object();
 
        MainForm(string symbol, int digits)
        {
            TheForm = this;

            InitializeComponent();
            Order = new Order();
            bindingSource3.DataSource = OrderDefault.DefaultsNew;

            Text = symbol;
            order.Digits = (Int16)digits;
            doTaskCallback = new DoTaskCallback(DoTask);
        }

        public static void Init(string symbol, int digits)
        {
            AppThread = new Thread(StartApp);
            AppThread.Start(new StartArg(symbol, digits));
        }

        static void StartApp(object _arg)
        {
            StartArg arg = (StartArg)_arg;

            // Enabling Windows XP visual effects before any controls are created
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new MainForm(arg.Symbol, arg.Digits);
            Started = true;

            // Create the main window and run it
            Application.Run(TheForm);
        }

        public static bool Started { get; internal set; }

        public static bool Running
        {
            get
            {
                return Started && AppThread.ThreadState == ThreadState.Running;
            }
        }

        static Order order0 = new Order();
        static Order order;
        public static Order Order
        {
            get
            {
                return order;
            }
            internal set
            {
                order = value;
                order0.Ticket = order.Ticket;
                order0.Type = order.Type;
                order0.IsOpen = order.IsOpen;
                order0.IsValue = order.IsValue;
                order0.OpenTime = order.OpenTime;
                order0.CloseTime = order.CloseTime;
                order0.LimitPoints = order.LimitPoints;
                order0.SLPoints = order.SLPoints;
                order0.TPPoints = order.TPPoints;
                TheForm.bindingSource2.DataSource = order0;
                TheForm.bindingSource2.DataSource = order;
            }
        }

        public static void Tick(double bid, double ask)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (TheForm.InvokeRequired)
            {
                TheForm.Invoke(TheForm.doTaskCallback, new TickTask(bid, ask));
            }
        }

        public static void RecordOrders(int numOrders)
        {
            if (TheForm.InvokeRequired)
            {
                TheForm.Invoke(TheForm.doTaskCallback, new RecordOrdersTask(numOrders));
            }
        }

        public static void AddOrder(Order order)
        {
            if (TheForm.InvokeRequired)
            {
                TheForm.Invoke(TheForm.doTaskCallback, new AddOrderTask(order));
            }
        }

        void Reset()
        {
            bool chgd;
            Order selectedOrder = dataGridView1.SelectedRows.Count==0 ? null : (Order)dataGridView1.SelectedRows[0].DataBoundItem;
            Order newOrder;
            if (selectedOrder == null)
            {
                chgd = bindingSource3.DataSource != OrderDefault.DefaultsNew;
                bindingSource3.DataSource = OrderDefault.DefaultsNew;
                newOrder = new Order();
                newOrder.ThisDefault = Order.Default0;
            }
            else
            {
                chgd = bindingSource3.DataSource != OrderDefault.DefaultsModify;
                bindingSource3.DataSource = OrderDefault.DefaultsModify;
                newOrder = selectedOrder.Clone();
                newOrder.ThisDefault.IsChanged = false;
                newOrder.IsOpen = true;
                newOrder.IsValue = true;
            }
            newOrder.Bid = order.Bid;
            newOrder.Ask = order.Ask;
            newOrder.Digits = order.Digits;
            Order = newOrder;
            if (chgd)
            {
                comboProfile.SelectedIndex = 0;
            }
            Refresh();
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            order.BeingSaved = true;
            Enabled = false;
        }

        private void buttonDelClose_Click(object sender, EventArgs e)
        {
            order.Type = OrderType.CloseOrDelete;
            order.BeingSaved = true;
            Enabled = false;
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            order.Type = OrderType.ReloadAll;
            order.BeingSaved = true;
            Enabled = false;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void buttonNew_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }


        int recNumOrders = 0;
        List<Order> recOrders;

        private void DoTask(object task)
        {

            if (task is TickTask)
            {
                TickTask tickTask = (TickTask)task;

                order.Bid = tickTask.Bid;
                order.Ask = tickTask.Ask;
                Order = order;
                Refresh();
            }
            else if (task is RecordOrdersTask)
            {
                RecordOrdersTask recordOrdersTask = (RecordOrdersTask)task;

                recNumOrders = recordOrdersTask.NumOrders;
                recOrders = new List<Order>();
                dataGridView1.DataSource = null;

                if (recNumOrders == 0)
                {
                    dataGridView1.DataSource = recOrders;
                    recOrders = null;
                    Enabled = true;
                }
            }
            else if (task is AddOrderTask)
            {
                AddOrderTask addOrderTask = (AddOrderTask)task;

                addOrderTask.Order.Digits = order.Digits;
                recOrders.Add(addOrderTask.Order);

                recNumOrders--;

                if (recNumOrders == 0)
                {
                    dataGridView1.DataSource = recOrders;
                    recOrders = null;
                    Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Unsupported task : " + task, "Error");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Reset();
        }

        private void buttonSaveProfile_Click(object sender, EventArgs e)
        {
            PropertyInfo[] fromFields = typeof(OrderDefault).GetProperties();
            PropertyHandler.SetProperties(fromFields, order.ThisDefault, Order.Default0);
            Order.Default0.IsChanged = false;
            order.ThisDefault.IsChanged = false;
            Order = order;
            // TODO save to file
            Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
