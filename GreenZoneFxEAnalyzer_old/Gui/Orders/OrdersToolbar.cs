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
using GreenZoneFxEngine.Util;

namespace GreenZoneFxEngine.Gui
{
    public partial class OrdersToolbar : UserControl
    {
        IOrdersTabPanel parent;
        bool enableEvents = true;

        public OrdersToolbar()
        {
            InitializeComponent();
        }

        internal void Init(IOrdersTabPanel parent)
        {
            this.parent = parent;
            UpdateFilterControls();
        }

        internal void UpdateFilterControls()
        {
            this.enableEvents = false;
            try
            {
                symbolCb.Items.Clear();
                operationCb.Items.Clear();
                expertCb.Items.Clear();
                magicCb.Items.Clear();

                symbolCb.Items.Add(new ListItem<string>(null, "All"));
                foreach (symbol s in parent.Environment.Symbols)
                {
                    symbolCb.Items.Add(new ListItem<string>(s.ToString(), s.ToString()));
                }

                operationCb.Items.Add(new ListItem<TradeOperation>(TradeOperation.OP_NONE, "All"));
                foreach (TradeOperation o in Enum.GetValues(typeof(TradeOperation)))
                {
                    if (o != TradeOperation.OP_NONE)
                    {
                        operationCb.Items.Add(new ListItem<TradeOperation>(o, o.GetShortTxt()));
                    }
                }

                SortedSet<Mt4ExpertInfo> experts = new SortedSet<Mt4ExpertInfo>();
                foreach (ChartGroupRuntime chg in parent.Environment.Charts)
                {
                    foreach (ChartRuntime ch in chg.Charts)
                    {
                        if (ch.Expert != null && ch.Session.AppearsInTest)
                        {
                            experts.Add(ch.Expert.ExpertInfo);
                        }
                    }
                }
                expertCb.Items.Add(new ListItem<string>(null, "All"));
                foreach (var x in experts)
                {
                    expertCb.Items.Add(new ListItem<string>(x.SystemTypeId, x.Name));
                }

                SortedSet<ListItem<int>> magics = new SortedSet<ListItem<int>>();
                foreach (HistoryOrderEtc o in parent.Environment.HistoryOrders)
                {
                    if (o.MagicNumber > 0)
                    {
                        magics.Add(new ListItem<int>(o.MagicNumber, "" + o.MagicNumber));
                    }
                }
                magicCb.Items.Add(new ListItem<int>(-1, "All"));
                magicCb.Items.Add(new ListItem<int>(0, "0 - No Magic"));
                foreach (var i in magics)
                {
                    magicCb.Items.Add(i);
                }

                OrderFilter filter = parent.Orders.Filter;
                groupByCb.SelectedItem = filter.GroupBy;

                buyCheckBox.Checked = filter.Buy;
                sellCheckBox.Checked = filter.Sell;
                limitCheckBox.Checked = filter.Limit;
                stopCheckBox.Checked = filter.Stop;
                showFiltersCb.Checked = filter.More;
                symbolCb.SelectedItem = new ListItem<string>(filter.Symbol, "?");
                operationCb.SelectedItem = new ListItem<TradeOperation>(filter.Operation, "?");
                expertCb.SelectedItem = new ListItem<string>(filter.Expert, "?");
                commentTb.Text = filter.Comment;
                magicCb.SelectedItem = new ListItem<int>(filter.Magic, "?");

                if (filter.From == default(datetime))
                {
                    fromDtp.Value = (DateTime)datetime.MinValue;
                }
                else
                {
                    fromDtp.Value = (DateTime)filter.From;
                }

                if (filter.To == default(datetime))
                {
                    toDtp.Value = (DateTime)datetime.MaxValue;
                }
                else
                {
                    toDtp.Value = (DateTime)filter.To;
                }

                panel1.Visible = showFiltersCb.Checked;
            }
            finally
            {
                this.enableEvents = true;
            }
        }

        void ApplyFilters()
        {
            timer1.Stop();
            parent.Orders.ApplyFilters();
            parent.Form1.UpdateOrders();
            parent.Form1.SaveSession();
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

        private void showFiltersCb_CheckedChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            panel1.Visible = showFiltersCb.Checked;
            filter.More = showFiltersCb.Checked;
            ApplyFiltersLater();
        }

        private void buyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Buy = buyCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void sellCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Sell = sellCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void limitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Limit = limitCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void stopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Stop = stopCheckBox.Checked;
            ApplyFiltersLater();
        }

        private void fromDtp_ValueChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.From = (datetime)fromDtp.Value;
            ApplyFiltersLater();
        }

        private void toDtp_ValueChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.To = (datetime)toDtp.Value;
            ApplyFiltersLater();
        }


        private void symbolCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Symbol = (string)(ListItem<string>)symbolCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void operationCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Operation = (TradeOperation)(ListItem<TradeOperation>)operationCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void expertCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Expert = (string)(ListItem<string>)expertCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void commentTb_TextChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Comment = commentTb.Text;
            ApplyFiltersLater();
        }

        private void magicCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.Magic = (int)(ListItem<int>)magicCb.SelectedItem;
            ApplyFiltersLater();
        }

        private void groupByCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrderFilter filter = parent.Orders.Filter;
            filter.GroupBy = (string)groupByCb.SelectedItem;
            ApplyFiltersLater();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void closeChartButton2_Click(object sender, EventArgs e)
        {

        }

        private void symPerMiniLabel_Click(object sender, EventArgs e)
        {

        }

        private void toggleTopBarButton2_Click(object sender, EventArgs e)
        {

        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            parent.Orders.Filter.Clear();
            UpdateFilterControls();
            ApplyFiltersLater();
        }


    }
}
