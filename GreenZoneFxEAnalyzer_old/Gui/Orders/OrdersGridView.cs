using System.Windows.Forms;
using System.Drawing;
using System;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Trading;
using System.Collections.Generic;

namespace GreenZoneFxEngine.Gui.Orders
{
    public class OrdersGridView : DataGridView
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridViewTextBoxColumn ticketDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expertDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lotsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn openTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn openPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stopLossDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn takeProfitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn closeTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn closePriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commissionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn swapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn profitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expirationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn magicNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn balanceDataGridViewTextBoxColumn;

        private System.Windows.Forms.BindingSource historyOrderEtcBindingSource;

        private System.Windows.Forms.ContextMenuStrip showColumnsCmstr;

        public OrdersGridView()
        {
            this.components = new System.ComponentModel.Container();
            this.historyOrderEtcBindingSource = new System.Windows.Forms.BindingSource(this.components);

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historyOrderEtcBindingSource)).BeginInit();

            this.ticketDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.expertDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.operationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.symbolDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.lotsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.openTimeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.openPriceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.stopLossDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.takeProfitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.closeTimeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.closePriceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.commissionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.swapDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.profitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.expirationDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.magicNumberDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            this.balanceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            // 
            // dataGridView1
            // 
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeRows = false;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoGenerateColumns = false;
            this.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ticketDataGridViewTextBoxColumn,
            this.expertDataGridViewTextBoxColumn,
            this.operationDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.symbolDataGridViewTextBoxColumn,
            this.lotsDataGridViewTextBoxColumn,
            this.openTimeDataGridViewTextBoxColumn,
            this.openPriceDataGridViewTextBoxColumn,
            this.stopLossDataGridViewTextBoxColumn,
            this.takeProfitDataGridViewTextBoxColumn,
            this.closeTimeDataGridViewTextBoxColumn,
            this.closePriceDataGridViewTextBoxColumn,
            this.commissionDataGridViewTextBoxColumn,
            this.swapDataGridViewTextBoxColumn,
            this.profitDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn,
            this.expirationDataGridViewTextBoxColumn,
            this.magicNumberDataGridViewTextBoxColumn,
            this.balanceDataGridViewTextBoxColumn});
            this.ContextMenuStrip = this.showColumnsCmstr;
            this.DataSource = this.historyOrderEtcBindingSource;
            this.Location = new System.Drawing.Point(0, 28);
            this.Name = "dataGridView1";
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.RowTemplate.Height = 18;
            this.Size = new System.Drawing.Size(693, 179);
            this.TabIndex = 24;
            // 
            // ticketDataGridViewTextBoxColumn
            // 
            this.ticketDataGridViewTextBoxColumn.DataPropertyName = "Ticket";
            this.ticketDataGridViewTextBoxColumn.HeaderText = "Ticket";
            this.ticketDataGridViewTextBoxColumn.Name = "ticketDataGridViewTextBoxColumn";
            this.ticketDataGridViewTextBoxColumn.ReadOnly = true;
            this.ticketDataGridViewTextBoxColumn.Width = 62;
            // 
            // expertDataGridViewTextBoxColumn
            // 
            this.expertDataGridViewTextBoxColumn.DataPropertyName = "Expert";
            this.expertDataGridViewTextBoxColumn.HeaderText = "Expert";
            this.expertDataGridViewTextBoxColumn.Name = "expertDataGridViewTextBoxColumn";
            this.expertDataGridViewTextBoxColumn.ReadOnly = true;
            this.expertDataGridViewTextBoxColumn.Width = 62;
            // 
            // operationDataGridViewTextBoxColumn
            // 
            this.operationDataGridViewTextBoxColumn.DataPropertyName = "Operation";
            this.operationDataGridViewTextBoxColumn.HeaderText = "Operation";
            this.operationDataGridViewTextBoxColumn.Name = "operationDataGridViewTextBoxColumn";
            this.operationDataGridViewTextBoxColumn.ReadOnly = true;
            this.operationDataGridViewTextBoxColumn.Width = 78;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeDataGridViewTextBoxColumn.Width = 56;
            // 
            // symbolDataGridViewTextBoxColumn
            // 
            this.symbolDataGridViewTextBoxColumn.DataPropertyName = "Symbol";
            this.symbolDataGridViewTextBoxColumn.HeaderText = "Symbol";
            this.symbolDataGridViewTextBoxColumn.Name = "symbolDataGridViewTextBoxColumn";
            this.symbolDataGridViewTextBoxColumn.ReadOnly = true;
            this.symbolDataGridViewTextBoxColumn.Width = 66;
            // 
            // lotsDataGridViewTextBoxColumn
            // 
            this.lotsDataGridViewTextBoxColumn.DataPropertyName = "Lots";
            this.lotsDataGridViewTextBoxColumn.HeaderText = "Lots";
            this.lotsDataGridViewTextBoxColumn.Name = "lotsDataGridViewTextBoxColumn";
            this.lotsDataGridViewTextBoxColumn.ReadOnly = true;
            this.lotsDataGridViewTextBoxColumn.Width = 52;
            // 
            // openTimeDataGridViewTextBoxColumn
            // 
            this.openTimeDataGridViewTextBoxColumn.DataPropertyName = "OpenTime";
            this.openTimeDataGridViewTextBoxColumn.HeaderText = "Open T";
            this.openTimeDataGridViewTextBoxColumn.Name = "openTimeDataGridViewTextBoxColumn";
            this.openTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.openTimeDataGridViewTextBoxColumn.Width = 68;
            // 
            // openPriceDataGridViewTextBoxColumn
            // 
            this.openPriceDataGridViewTextBoxColumn.DataPropertyName = "OpenPrice";
            this.openPriceDataGridViewTextBoxColumn.HeaderText = "Open";
            this.openPriceDataGridViewTextBoxColumn.Name = "openPriceDataGridViewTextBoxColumn";
            this.openPriceDataGridViewTextBoxColumn.ReadOnly = true;
            this.openPriceDataGridViewTextBoxColumn.Width = 58;
            // 
            // stopLossDataGridViewTextBoxColumn
            // 
            this.stopLossDataGridViewTextBoxColumn.DataPropertyName = "StopLoss";
            this.stopLossDataGridViewTextBoxColumn.HeaderText = "SL";
            this.stopLossDataGridViewTextBoxColumn.Name = "stopLossDataGridViewTextBoxColumn";
            this.stopLossDataGridViewTextBoxColumn.ReadOnly = true;
            this.stopLossDataGridViewTextBoxColumn.Width = 45;
            // 
            // takeProfitDataGridViewTextBoxColumn
            // 
            this.takeProfitDataGridViewTextBoxColumn.DataPropertyName = "TakeProfit";
            this.takeProfitDataGridViewTextBoxColumn.HeaderText = "TP";
            this.takeProfitDataGridViewTextBoxColumn.Name = "takeProfitDataGridViewTextBoxColumn";
            this.takeProfitDataGridViewTextBoxColumn.ReadOnly = true;
            this.takeProfitDataGridViewTextBoxColumn.Width = 46;
            // 
            // closeTimeDataGridViewTextBoxColumn
            // 
            this.closeTimeDataGridViewTextBoxColumn.DataPropertyName = "CloseTime";
            this.closeTimeDataGridViewTextBoxColumn.HeaderText = "Close T";
            this.closeTimeDataGridViewTextBoxColumn.Name = "closeTimeDataGridViewTextBoxColumn";
            this.closeTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.closeTimeDataGridViewTextBoxColumn.Width = 68;
            // 
            // closePriceDataGridViewTextBoxColumn
            // 
            this.closePriceDataGridViewTextBoxColumn.DataPropertyName = "ClosePrice";
            this.closePriceDataGridViewTextBoxColumn.HeaderText = "Close";
            this.closePriceDataGridViewTextBoxColumn.Name = "closePriceDataGridViewTextBoxColumn";
            this.closePriceDataGridViewTextBoxColumn.ReadOnly = true;
            this.closePriceDataGridViewTextBoxColumn.Width = 58;
            // 
            // commissionDataGridViewTextBoxColumn
            // 
            this.commissionDataGridViewTextBoxColumn.DataPropertyName = "Commission";
            this.commissionDataGridViewTextBoxColumn.HeaderText = "Commission";
            this.commissionDataGridViewTextBoxColumn.Name = "commissionDataGridViewTextBoxColumn";
            this.commissionDataGridViewTextBoxColumn.ReadOnly = true;
            this.commissionDataGridViewTextBoxColumn.Width = 87;
            // 
            // swapDataGridViewTextBoxColumn
            // 
            this.swapDataGridViewTextBoxColumn.DataPropertyName = "Swap";
            this.swapDataGridViewTextBoxColumn.HeaderText = "Swap";
            this.swapDataGridViewTextBoxColumn.Name = "swapDataGridViewTextBoxColumn";
            this.swapDataGridViewTextBoxColumn.ReadOnly = true;
            this.swapDataGridViewTextBoxColumn.Width = 59;
            // 
            // profitDataGridViewTextBoxColumn
            // 
            this.profitDataGridViewTextBoxColumn.DataPropertyName = "Profit";
            this.profitDataGridViewTextBoxColumn.HeaderText = "Profit";
            this.profitDataGridViewTextBoxColumn.Name = "profitDataGridViewTextBoxColumn";
            this.profitDataGridViewTextBoxColumn.ReadOnly = true;
            this.profitDataGridViewTextBoxColumn.Width = 56;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.ReadOnly = true;
            this.commentDataGridViewTextBoxColumn.Width = 76;
            // 
            // expirationDataGridViewTextBoxColumn
            // 
            this.expirationDataGridViewTextBoxColumn.DataPropertyName = "Expiration";
            this.expirationDataGridViewTextBoxColumn.HeaderText = "Expiration";
            this.expirationDataGridViewTextBoxColumn.Name = "expirationDataGridViewTextBoxColumn";
            this.expirationDataGridViewTextBoxColumn.ReadOnly = true;
            this.expirationDataGridViewTextBoxColumn.Width = 78;
            // 
            // magicNumberDataGridViewTextBoxColumn
            // 
            this.magicNumberDataGridViewTextBoxColumn.DataPropertyName = "MagicNumber";
            this.magicNumberDataGridViewTextBoxColumn.HeaderText = "MagicNumber";
            this.magicNumberDataGridViewTextBoxColumn.Name = "magicNumberDataGridViewTextBoxColumn";
            this.magicNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.magicNumberDataGridViewTextBoxColumn.Width = 98;
            // 
            // balanceDataGridViewTextBoxColumn
            // 
            this.balanceDataGridViewTextBoxColumn.DataPropertyName = "Balance";
            this.balanceDataGridViewTextBoxColumn.HeaderText = "Balance";
            this.balanceDataGridViewTextBoxColumn.Name = "balanceDataGridViewTextBoxColumn";
            this.balanceDataGridViewTextBoxColumn.ReadOnly = true;
            this.balanceDataGridViewTextBoxColumn.Width = 71;
            // 
            // historyOrderEtcBindingSource
            // 
            this.historyOrderEtcBindingSource.DataSource = typeof(GreenZoneFxEngine.Trading.HistoryOrderEtc);

            this.showColumnsCmstr = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuStrip = this.showColumnsCmstr;
            // 
            // showColumnsCmstr
            // 
            this.showColumnsCmstr.Name = "showColumnsCmstr";
            this.showColumnsCmstr.Size = new System.Drawing.Size(61, 4);

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historyOrderEtcBindingSource)).EndInit();
        }

        OrdersHistoryView view;
        public OrdersHistoryView View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                this.historyOrderEtcBindingSource.DataSource = value.FilteredOrders;
                view.HistoryChanged += new OrdersHistoryView.DHistoryChanged(view_HistoryChanged);
            }
        }

        void view_HistoryChanged(bool groupsChanged)
        {
            if (!groupsChanged)
            {
                this.historyOrderEtcBindingSource.DataSource = view.FilteredOrders;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void UpdateVisibleColumnsPopup() {
            GreenZoneUtils.BuildVisibleColumnsPopup(this, showColumnsCmstr, false);
        }

    }
}
