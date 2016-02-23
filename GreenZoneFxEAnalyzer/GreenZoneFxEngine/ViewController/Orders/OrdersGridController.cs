using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Orders
{
    
    public class OrdersGridController : OrdersGridControllerBase
    {
        public OrdersGridController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            // 
            // showColumnsCmstr
            // 
            this.showColumnsCmstr.Name = "showColumnsCmstr";
            this.showColumnsCmstr = new ComboController(rmiManager, this, false);

            this.ticketColumn = new GridColumnController(rmiManager, this, typeof(int));
            this.expertColumn = new GridColumnController(rmiManager, this, typeof(string));
            this.operationColumn = new GridColumnController(rmiManager, this, typeof(datetime));
            this.typeColumn = new GridColumnController(rmiManager, this, typeof(string));
            this.symbolColumn = new GridColumnController(rmiManager, this, typeof(string));
            this.lotsColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.openTimeColumn = new GridColumnController(rmiManager, this, typeof(datetime));
            this.openPriceColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.stopLossColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.takeProfitColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.closeTimeColumn = new GridColumnController(rmiManager, this, typeof(datetime));
            this.closePriceColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.commissionColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.swapColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.profitColumn = new GridColumnController(rmiManager, this, typeof(double));
            this.commentColumn = new GridColumnController(rmiManager, this, typeof(string));
            this.expirationColumn = new GridColumnController(rmiManager, this, typeof(datetime));
            this.magicNumberColumn = new GridColumnController(rmiManager, this, typeof(int));
            this.balanceColumn = new GridColumnController(rmiManager, this, typeof(double));
            // 
            // dataGridView1
            // 
            this.PopupMenu = this.showColumnsCmstr;
            this.Name = "dataGridView1";
            // 
            // ticketGridColumnController
            // 
            this.ticketColumn.DataPropertyName = "Ticket";
            this.ticketColumn.Text = "Ticket";
            this.ticketColumn.Name = "ticketGridColumnController";
            // 
            // expertGridColumnController
            // 
            this.expertColumn.DataPropertyName = "Expert";
            this.expertColumn.Text = "Expert";
            this.expertColumn.Name = "expertGridColumnController";
            // 
            // operationGridColumnController
            // 
            this.operationColumn.DataPropertyName = "Operation";
            this.operationColumn.Text = "Operation";
            this.operationColumn.Name = "operationGridColumnController";
            // 
            // typeGridColumnController
            // 
            this.typeColumn.DataPropertyName = "Type";
            this.typeColumn.Text = "Type";
            this.typeColumn.Name = "typeGridColumnController";
            // 
            // symbolGridColumnController
            // 
            this.symbolColumn.DataPropertyName = "Symbol";
            this.symbolColumn.Text = "Symbol";
            this.symbolColumn.Name = "symbolGridColumnController";
            // 
            // lotsGridColumnController
            // 
            this.lotsColumn.DataPropertyName = "Lots";
            this.lotsColumn.Text = "Lots";
            this.lotsColumn.Name = "lotsGridColumnController";
            // 
            // openTimeGridColumnController
            // 
            this.openTimeColumn.DataPropertyName = "OpenTime";
            this.openTimeColumn.Text = "Open T";
            this.openTimeColumn.Name = "openTimeGridColumnController";
            // 
            // openPriceGridColumnController
            // 
            this.openPriceColumn.DataPropertyName = "OpenPrice";
            this.openPriceColumn.Text = "Open";
            this.openPriceColumn.Name = "openPriceGridColumnController";
            // 
            // stopLossGridColumnController
            // 
            this.stopLossColumn.DataPropertyName = "StopLoss";
            this.stopLossColumn.Text = "SL";
            this.stopLossColumn.Name = "stopLossGridColumnController";
            // 
            // takeProfitGridColumnController
            // 
            this.takeProfitColumn.DataPropertyName = "TakeProfit";
            this.takeProfitColumn.Text = "TP";
            this.takeProfitColumn.Name = "takeProfitGridColumnController";
            // 
            // closeTimeGridColumnController
            // 
            this.closeTimeColumn.DataPropertyName = "CloseTime";
            this.closeTimeColumn.Text = "Close T";
            this.closeTimeColumn.Name = "closeTimeGridColumnController";
            // 
            // closePriceGridColumnController
            // 
            this.closePriceColumn.DataPropertyName = "ClosePrice";
            this.closePriceColumn.Text = "Close";
            this.closePriceColumn.Name = "closePriceGridColumnController";
            // 
            // commissionGridColumnController
            // 
            this.commissionColumn.DataPropertyName = "Commission";
            this.commissionColumn.Text = "Commission";
            this.commissionColumn.Name = "commissionGridColumnController";
            // 
            // swapGridColumnController
            // 
            this.swapColumn.DataPropertyName = "Swap";
            this.swapColumn.Text = "Swap";
            this.swapColumn.Name = "swapGridColumnController";
            // 
            // profitGridColumnController
            // 
            this.profitColumn.DataPropertyName = "Profit";
            this.profitColumn.Text = "Profit";
            this.profitColumn.Name = "profitGridColumnController";
            // 
            // commentGridColumnController
            // 
            this.commentColumn.DataPropertyName = "Comment";
            this.commentColumn.Text = "Comment";
            this.commentColumn.Name = "commentGridColumnController";
            // 
            // expirationGridColumnController
            // 
            this.expirationColumn.DataPropertyName = "Expiration";
            this.expirationColumn.Text = "Expiration";
            this.expirationColumn.Name = "expirationGridColumnController";
            // 
            // magicNumberGridColumnController
            // 
            this.magicNumberColumn.DataPropertyName = "MagicNumber";
            this.magicNumberColumn.Text = "MagicNumber";
            this.magicNumberColumn.Name = "magicNumberGridColumnController";
            // 
            // balanceGridColumnController
            // 
            this.balanceColumn.DataPropertyName = "Balance";
            this.balanceColumn.Text = "Balance";
            this.balanceColumn.Name = "balanceGridColumnController";

        }

        ServerOrdersHistoryView view;
        public ServerOrdersHistoryView View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                Items = (IList<object>)value.FilteredOrders;
                view.HistoryChanged += new ServerOrdersHistoryView.DHistoryChanged(view_HistoryChanged);
            }
        }

        readonly GridColumnController ticketColumn;
        public GridColumnController TicketColumn
        {
            get
            {
                return ticketColumn;
            }
        }

        readonly GridColumnController expertColumn;
        public GridColumnController ExpertColumn
        {
            get
            {
                return expertColumn;
            }
        }

        readonly GridColumnController operationColumn;
        public GridColumnController OperationColumn
        {
            get
            {
                return operationColumn;
            }
        }

        readonly GridColumnController typeColumn;
        public GridColumnController TypeColumn
        {
            get
            {
                return typeColumn;
            }
        }

        readonly GridColumnController symbolColumn;
        public GridColumnController SymbolColumn
        {
            get
            {
                return symbolColumn;
            }
        }

        readonly GridColumnController lotsColumn;
        public GridColumnController LotsColumn
        {
            get
            {
                return lotsColumn;
            }
        }

        readonly GridColumnController openTimeColumn;
        public GridColumnController OpenTimeColumn
        {
            get
            {
                return openTimeColumn;
            }
        }

        readonly GridColumnController openPriceColumn;
        public GridColumnController OpenPriceColumn
        {
            get
            {
                return openPriceColumn;
            }
        }

        readonly GridColumnController stopLossColumn;
        public GridColumnController StopLossColumn
        {
            get
            {
                return stopLossColumn;
            }
        }

        readonly GridColumnController takeProfitColumn;
        public GridColumnController TakeProfitColumn
        {
            get
            {
                return takeProfitColumn;
            }
        }

        readonly GridColumnController closeTimeColumn;
        public GridColumnController CloseTimeColumn
        {
            get
            {
                return closeTimeColumn;
            }
        }

        readonly GridColumnController closePriceColumn;
        public GridColumnController ClosePriceColumn
        {
            get
            {
                return closePriceColumn;
            }
        }

        readonly GridColumnController commissionColumn;
        public GridColumnController CommissionColumn
        {
            get
            {
                return commissionColumn;
            }
        }

        readonly GridColumnController swapColumn;
        public GridColumnController SwapColumn
        {
            get
            {
                return swapColumn;
            }
        }

        readonly GridColumnController profitColumn;
        public GridColumnController ProfitColumn
        {
            get
            {
                return profitColumn;
            }
        }

        readonly GridColumnController commentColumn;
        public GridColumnController CommentColumn
        {
            get
            {
                return commentColumn;
            }
        }

        readonly GridColumnController expirationColumn;
        public GridColumnController ExpirationColumn
        {
            get
            {
                return expirationColumn;
            }
        }

        readonly GridColumnController magicNumberColumn;
        public GridColumnController MagicNumberColumn
        {
            get
            {
                return magicNumberColumn;
            }
        }

        readonly GridColumnController balanceColumn;
        public GridColumnController BalanceColumn
        {
            get
            {
                return balanceColumn;
            }
        }

        readonly ComboController showColumnsCmstr;
        public ComboController ShowColumnsCmstr
        {
            get
            {
                return showColumnsCmstr;
            }
        }

        void view_HistoryChanged(bool groupsChanged)
        {
            if (!groupsChanged)
            {
                Items = (IList<object>)view.FilteredOrders;
            }
        }

        public void UpdateVisibleColumnsPopup()
        {
            GreenZoneUtils.BuildVisibleColumnsModel(rmiManager, this, showColumnsCmstr, false);
        }
    }
}
