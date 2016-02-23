using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using GreenZoneUtil.Util;
using System.Collections;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{

    public class ServerOrdersHistoryView : OrdersHistoryViewEx
    {

        public ServerOrdersHistoryView(GreenRmiManager rmiManager, ServerOrdersTable parent, IOrderFilter filter)
            : base(rmiManager)
        {
            Parent = parent;
            Filter = filter;
            AutoSeriesRange = true;
            IsCursorBarConnected = true;
            SeriesRange = new SeriesRange(0, 100, 0, 0);

            ApplyFilters();
        }

        // TODO bind to event
        void parent_ScrolledBarTimeChanged()
        {
            ScrolledBarTime = Parent.ScrolledBarTime;
        }

    }


    public class ServerTradeOrder : TradeOrderEx
    {
        public ServerTradeOrder(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public ServerTradeOrder(GreenRmiManager rmiManager, ITradeOrder order)
            : base(rmiManager)
        {
            Params = order.Params;
        }
    }

    public class ServerHistoryOrder : HistoryOrderEx
    {
        public ServerHistoryOrder(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }
        public ServerHistoryOrder(GreenRmiManager rmiManager, ITradeOrder order)
            : base(rmiManager, order)
        {
            Params = order.Params;
        }
    }

    public class ServerHistoryOrderEtc : HistoryOrderEtcEx
    {

        public ServerHistoryOrderEtc(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public ServerHistoryOrderEtc(GreenRmiManager rmiManager, TradeOperation operation, ITradeOrder order)
            : base(rmiManager, operation, order)
        {
        }
        public ServerHistoryOrderEtc(GreenRmiManager rmiManager, TradeOperation operation, ITradeOrder order, Color color)
            : base(rmiManager,operation,order,color)
        {
        }
    }

    [Serializable]
    public class ServerSymbolSession : SymbolSessionBase
    {
        public ServerSymbolSession(GreenRmiManager rmiManager, symbol symbol)
            : base(rmiManager)
        {
            Symbol = symbol;
        }
    }

}
