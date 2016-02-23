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
    
    [GreenRmi]
    public interface IOrdersGridController : IGridController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController TicketColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController ExpertColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController OperationColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController TypeColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController SymbolColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController LotsColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController OpenTimeColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController OpenPriceColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController StopLossColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController TakeProfitColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController CloseTimeColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController ClosePriceColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController CommissionColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController SwapColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController ProfitColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController CommentColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController ExpirationColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController MagicNumberColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController BalanceColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController ShowColumnsCmstr
        {
            get;
            set;
        }

    }
}
