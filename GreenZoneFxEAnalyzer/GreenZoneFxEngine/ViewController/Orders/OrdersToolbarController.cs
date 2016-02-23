using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Timers;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Orders
{
    
    public class OrdersToolbarController : OrdersToolbarControllerBase
    {
        IOrdersTabPageControllerEx parent;
        bool enableEvents = true;

        public OrdersToolbarController(GreenRmiManager rmiManager, IOrdersTabPageControllerEx parent)
            : base(rmiManager, (Controller)parent)
        {
            this.parent = parent;
            SymbolCb = new ComboController(rmiManager, this, false);
            OperationCb = new ComboController(rmiManager, this, false);
            ExpertCb = new ComboController(rmiManager, this, false);
            MagicCb = new ComboController(rmiManager, this, false);
            GroupByCb = new ComboController(rmiManager, this, false);
            BuyCheckBox = new ToggleButtonController(rmiManager, this);
            SellCheckBox = new ToggleButtonController(rmiManager, this);
            LimitCheckBox = new ToggleButtonController(rmiManager, this);
            StopCheckBox = new ToggleButtonController(rmiManager, this);
            ShowFiltersCheckBox = new ToggleButtonController(rmiManager, this);
            CommentTb = new LabelledController(rmiManager, this);
            FromDtp = new FieldController<DateTime>(rmiManager, this);
            ToDtp = new FieldController<DateTime>(rmiManager, this);
            ResetButton = new ButtonController(rmiManager, this);
            CloseChartButton2 = new ButtonController(rmiManager, this);
            Panel1 = new Controller(rmiManager, this);
            timer1 = new Timer();
            timer1.AutoReset = false;

            ShowFiltersCheckBox.CheckedChanged += new PropertyChangedEventHandler(showFiltersCb_CheckedChanged);
            BuyCheckBox.CheckedChanged += new PropertyChangedEventHandler(buyCheckBox_CheckedChanged);
            SellCheckBox.CheckedChanged += new PropertyChangedEventHandler(sellCheckBox_CheckedChanged);
            LimitCheckBox.CheckedChanged += new PropertyChangedEventHandler(limitCheckBox_CheckedChanged);
            StopCheckBox.CheckedChanged += new PropertyChangedEventHandler(stopCheckBox_CheckedChanged);
            FromDtp.ValueChanged += new PropertyChangedEventHandler(fromDtp_ValueChanged);
            ToDtp.ValueChanged += new PropertyChangedEventHandler(toDtp_ValueChanged);
            SymbolCb.SelectedIndexChanged += new PropertyChangedEventHandler(symbolCb_SelectedIndexChanged);
            OperationCb.SelectedIndexChanged += new PropertyChangedEventHandler(operationCb_SelectedIndexChanged);
            ExpertCb.SelectedIndexChanged += new PropertyChangedEventHandler(expertCb_SelectedIndexChanged);
            MagicCb.SelectedIndexChanged += new PropertyChangedEventHandler(magicCb_SelectedIndexChanged);
            GroupByCb.SelectedIndexChanged += new PropertyChangedEventHandler(groupByCb_SelectedIndexChanged);
            CommentTb.TextChanged += new PropertyChangedEventHandler(commentTb_TextChanged);
            ResetButton.Pressed += new ControllerEventHandler(resetButton_Click);
            CloseChartButton2.Pressed += new ControllerEventHandler(closeChartButton2_Click);
            timer1.Elapsed += new ElapsedEventHandler(timer1_Tick);

            UpdateFilterControls();
        }

        internal void UpdateFilterControls()
        {
            this.enableEvents = false;
            try
            {
                SymbolCb.Clear();
                OperationCb.Clear();
                ExpertCb.Clear();
                MagicCb.Clear();

                SymbolCb.Add(new ListItem<string>(null, "All"));
                foreach (symbol s in parent.Environment.Symbols)
                {
                    SymbolCb.Add(new ListItem<string>(s.ToString(), s.ToString()));
                }

                OperationCb.Add(new ListItem<TradeOperation>(TradeOperation.OP_NONE, "All"));
                foreach (TradeOperation o in Enum.GetValues(typeof(TradeOperation)))
                {
                    if (o != TradeOperation.OP_NONE)
                    {
                        OperationCb.Add(new ListItem<TradeOperation>(o, o.GetShortTxt()));
                    }
                }

                SortedSet<Mt4ExecutableInfo> experts = new SortedSet<Mt4ExecutableInfo>();
                foreach (ServerChartGroupRuntime chg in parent.Environment.Charts)
                {
                    foreach (ServerChartRuntime ch in chg.Charts)
                    {
                        if (ch.Expert != null && ch.Session.AppearsInTest)
                        {
                            experts.Add(ch.Expert.ExpertInfo);
                        }
                    }
                }
                ExpertCb.Add(new ListItem<string>(null, "All"));
                foreach (var x in experts)
                {
                    ExpertCb.Add(new ListItem<string>(x.SystemTypeId, x.Name));
                }

                SortedSet<ListItem<int>> magics = new SortedSet<ListItem<int>>();
                foreach (ServerHistoryOrderEtc o in parent.Environment.HistoryOrders)
                {
                    if (o.MagicNumber > 0)
                    {
                        magics.Add(new ListItem<int>(o.MagicNumber, "" + o.MagicNumber));
                    }
                }
                MagicCb.Add(new ListItem<int>(-1, "All"));
                MagicCb.Add(new ListItem<int>(0, "0 - No Magic"));
                foreach (var i in magics)
                {
                    MagicCb.Add(i);
                }

                OrderFilter filter = parent.Orders.Filter;
                GroupByCb.SelectedItem = filter.GroupBy;

                BuyCheckBox.Checked = filter.Buy;
                SellCheckBox.Checked = filter.Sell;
                LimitCheckBox.Checked = filter.Limit;
                StopCheckBox.Checked = filter.Stop;
                ShowFiltersCheckBox.Checked = filter.More;
                SymbolCb.SelectedItem = new ListItem<string>(filter.Symbol, "?");
                OperationCb.SelectedItem = new ListItem<TradeOperation>(filter.Operation, "?");
                ExpertCb.SelectedItem = new ListItem<string>(filter.Expert, "?");
                CommentTb.Text = filter.Comment;
                MagicCb.SelectedItem = new ListItem<int>(filter.Magic, "?");

                if (filter.From == default(datetime))
                {
                    FromDtp.Value = (DateTime)datetime.MinValue;
                }
                else
                {
                    FromDtp.Value = (DateTime)filter.From;
                }

                if (filter.To == default(datetime))
                {
                    ToDtp.Value = (DateTime)datetime.MaxValue;
                }
                else
                {
                    ToDtp.Value = (DateTime)filter.To;
                }

                Panel1.Visible = ShowFiltersCheckBox.Checked;
            }
            finally
            {
                this.enableEvents = true;
            }
        }

        readonly Timer timer1;
        public Timer Timer1
        {
            get
            {
                return timer1;
            }
        }

        void ApplyFilters()
        {
            timer1.Stop();
            parent.Orders.ApplyFilters();
            parent.MainWindow.UpdateOrders();
            parent.MainWindow.SaveSession();
        }

        void ApplyFiltersLater()
        {
            if (enableEvents)
            {
                timer1.Stop();
                timer1.Interval = 2000;
                timer1.Start();
            }
        }

        private void showFiltersCb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            Panel1.Visible = ShowFiltersCheckBox.Checked;
            filter.More = ShowFiltersCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void buyCheckBox_CheckedChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Buy = BuyCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void sellCheckBox_CheckedChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Sell = SellCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void limitCheckBox_CheckedChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Limit = LimitCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void stopCheckBox_CheckedChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Stop = StopCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void fromDtp_ValueChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.From = (datetime)FromDtp.Value;
            ApplyFiltersLater();
        }

        private void toDtp_ValueChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.To = (datetime)ToDtp.Value;
            ApplyFiltersLater();
        }


        private void symbolCb_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Symbol = (string)(ListItem<string>)SymbolCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void operationCb_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Operation = (TradeOperation)(ListItem<TradeOperation>)OperationCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void expertCb_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Expert = (string)(ListItem<string>)ExpertCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void magicCb_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Magic = (int)(ListItem<int>)MagicCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void groupByCb_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.GroupBy = (string)GroupByCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void commentTb_TextChanged(object sender, ControllerEventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Comment = CommentTb.Text;
            ApplyFiltersLater();
        }

        private void closeChartButton2_Click(object sender, ControllerEventArgs e)
        {

        }

        private void symPerMiniLabel_Click(object sender, ControllerEventArgs e)
        {

        }

        private void toggleTopBarButton2_Click(object sender, ControllerEventArgs e)
        {

        }

        private void resetButton_Click(object sender, ControllerEventArgs e)
        {
            parent.Orders.Filter.Clear();
            UpdateFilterControls();
            ApplyFiltersLater();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
