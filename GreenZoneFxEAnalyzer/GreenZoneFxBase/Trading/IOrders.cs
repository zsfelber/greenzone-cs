using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Drawing;
using System.IO;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine.Trading
{
    [GreenRmi]
    public interface IOrdersTable : ITradingConst
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEnvironmentRuntime Environment
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersTable Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISymbolRuntime SymbolRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SymbolRuntime.Context;")]
        ISymbolContext SymbolContext
        {
            get;
        }


        List<ITradeOrder> Orders
        {
            get;
            set;
        }

        List<IHistoryOrder> OrdersHistory
        {
            get;
            set;
        }

        List<IHistoryOrderEtc> OrdersHistoryEtc
        {
            get;
            set;
        }

        List<IOrdersTable> Children
        {
            get;
            set;
        }

        List<IOrdersHistoryView> ChildrenView
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        datetime ScrolledBarTime
        {
            get;
            set;
        }

        int CursorPosition
        {
            get;
            set;
        }

        IOrder Select(int index, TradingConst.TradeSelectMode select = TradingConst.SELECT_BY_TICKET, TradingConst.TradePool pool = TradingConst.MODE_TRADES);

        O Select<O>(int index, TradingConst.TradeSelectMode select = TradingConst.SELECT_BY_TICKET, TradingConst.TradePool pool = TradingConst.MODE_TRADES) where O : IOrder;

        IOrder Add(symbol symbol, OrderType cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expirationDatetime = default(datetime), Color arrow_color = default(Color));

        IOrder CloseOrDelete(int ticket, OrderTypeKind preferredKind, Color arrow_color);

        bool Modify(ITradeOrder _order, double price, double stoploss, double takeprofit, datetime expiration, Color color = default(Color));
    }

    [GreenRmi]
    public interface IServerOrdersTable : IOrdersTable
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrdersTable Parent
        {
            get;
            set;
        }

        List<O> GetOrders<O>(GreenZoneFxEngine.Trading.TradingConst.TradePool pool) where O : IOrder;

        void Load<O>(BinaryReader r, GreenZoneFxEngine.Trading.TradingConst.TradePool pool) where O : IOrder;

        void Save<O>(BinaryWriter w, GreenZoneFxEngine.Trading.TradingConst.TradePool pool) where O : IOrder;

        void Tick(double Bid, double Ask, double Volume);

        void Add(ITradeOrder order);

        void Add(IHistoryOrder order);

        void Add(IHistoryOrderEtc order);

        void CloseOrDelete(ITradeOrder order);

        void GenerateFromChildren();

        void Generate<O>(List<O> orders, TradingConst.TradePool pool) where O : IOrder;

        void UpdateOrder(ITradeOrder order);

    }

    [GreenRmi]
    public interface IOrderFilter : IRmiBase
    {
        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization="\"\"")]
        string GroupBy
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "true")]
        bool Buy
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "true")]
        bool Sell
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "true")]
        bool Limit
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "true")]
        bool Stop
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "datetime.MinValue")]
        datetime From
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "datetime.MaxValue")]
        datetime To
        {
            get;
            set;
        }

        bool More
        {
            get;
            set;
        }
        
        int Ticket
        {
            get;
            set;
        }
        
        string Symbol
        {
            get;
            set;
        }
        
        TradeOperation Operation
        {
            get;
            set;
        }
        
        string Expert
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "-1")]
        int Magic
        {
            get;
            set;
        }

        string Comment
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldInitialization = "new SortedSet<string>()")]
        SortedSet<string> HiddenColumns
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        object GroupField
        {
            get;
            set;
        }
    }
    
    [GreenRmi]
    public interface IOrdersHistoryView : IRmiBase, IChartOwner
    {
        IList<IHistoryOrderEtc> FilteredOrders
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersTable Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrderFilter Filter
        {
            get;
            set;
        }

        DArr BalanceHistAsDArr
        {
            get;
            set;
        }

        LArr BalanceTimeHistAsLArr
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        IEnumerable<object> Groups
        {
            get;
        }

        // IChartOwner

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new double Point
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new int Digits
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new string SymbolFormat
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new datetime ScrolledBarTime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new datetime ParentScrolledBarTime
        {
            get;
            set;
        }

        new bool IsCursorBarConnected
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new int ParentCursorPosition
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new int CursorPosition
        {
            get;
            set;
        }

        new SeriesRange SeriesRange
        {
            get;
            set;
        }

        new bool AutoSeriesRange
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new long RecordCount
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new long TotalFileOffset
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new datetime From
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new datetime To
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new LArr sLTime
        {
            get;
        }

        //

        new void LoadForward(int offset);
        new void LoadAtTotal(long total_ind);
        void ApplyFilters(IHistoryOrderEtc newOrder = null);
    }

	[GreenRmi]
	public interface IServerOrdersHistoryView : IOrdersHistoryView
	{
	}


    [GreenRmi]
    public interface IOrder : IRmiBase, IParams
    {
        Mt4ExecutableInfo Expert
        {
            get;
            set;
        }

        symbol Symbol
        {
            get;
            set;
        }

        OrderType Type
        {
            get;
            set;
        }

        int Ticket
        {
            get;
            set;
        }

        double Lots
        {
            get;
            set;
        }

        double StopLoss
        {
            get;
            set;
        }

        double TakeProfit
        {
            get;
            set;
        }

        datetime OpenTime
        {
            get;
            set;
        }

        double OpenPrice
        {
            get;
            set;
        }

        double PendingOpenPrice
        {
            get;
            set;
        }

        datetime CloseTime
        {
            get;
            set;
        }

        double ClosePrice
        {
            get;
            set;
        }

        string Comment
        {
            get;
            set;
        }

        datetime Expiration
        {
            get;
            set;
        }

        int MagicNumber
        {
            get;
            set;
        }

        // TODO convert to account currency
        double Commission
        {
            get;
            set;
        }

        // TODO convert to account currency
        double Profit
        {
            get;
            set;
        }

        // TODO convert to account currency
        double Swap
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new Dictionary<string, object> Params
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        object this[string field]
        {
            get;
        }

        void Load(IEnvironmentRuntime env, BinaryReader r);
        void Save(BinaryWriter w);
    }

    [GreenRmi(BaseClass = "OrderEx")]
    public interface ITradeOrder : IOrder
    {
        int Slippage
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "OrderEx")]
    public interface IHistoryOrder : IOrder
    {
    }


    [GreenRmi(BaseClass = "HistoryOrderEx")]
    public interface IHistoryOrderEtc : IHistoryOrder
    {
        TradeOperation Operation
        {
            get;
            set;
        }

        Color Color
        {
            get;
            set;
        }

        double Balance
        {
            get;
            set;
        }
    }

}
