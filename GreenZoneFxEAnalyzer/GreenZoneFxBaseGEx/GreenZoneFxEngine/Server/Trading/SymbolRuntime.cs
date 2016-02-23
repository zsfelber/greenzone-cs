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

namespace GreenZoneFxEngine.Trading
{
    [Serializable]
    public class ServerSymbolContext : ServerSymbolContextBase
    {
        internal ServerSymbolContext(GreenRmiManager rmiManager, IEnvironmentRuntime parent, symbol symbol)
            : base(rmiManager, parent, symbol)
        {
        }

        public override IServerSymbolRuntime Runtime
        {
            get
            {
                if (base.Runtime == null)
                {
                    base.Runtime = new ServerSymbolRuntime(rmiManager, Parent, this);
                }
                return base.Runtime;
            }
            set
            {
                base.Runtime = value;
            }
        }

        public override void Tick(double Bid, double Ask, double Volume)
        {
            this.Low = Math.Min(this.Low, Bid);
            this.High = Math.Max(this.High, Bid);

            this.Bid = Bid;
            this.Ask = Ask;

            Runtime.Tick(Bid, Ask, Volume);
        }

        public override void Bar(TimePeriodConst Period, double Open, double Low, double High, double Close, double Volume, int offset)
        {
            if (Period == TimePeriodConst.PERIOD_D1)
            {
                this.Low = Low;
                this.High = High;
            }
        }

    }


    public class ServerSymbolRuntime : ServerSymbolRuntimeBase
    {

        internal ServerSymbolRuntime(GreenRmiManager rmiManager, IEnvironmentRuntime parent, IServerSymbolContext context)
            : base(rmiManager, parent, context)
        {
            Parent = parent;
            Context = context;
            Session = new ServerSymbolSession(rmiManager, Symbol);
        }

        internal ServerSymbolRuntime(GreenRmiManager rmiManager, IEnvironmentRuntime parent, IServerSymbolContext context, ISymbolSession session)
            : base(rmiManager, parent, context)
        {
            Parent = parent;
            Context = context;
            Session = session;
            Orders = new ServerOrdersTable(rmiManager, this, (IServerOrdersTable)parent.Orders);
        }

        public override TimePeriodConst BestCursorPeriod
        {
            get
            {
                TimePeriodConst[] periods = EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS);
                Array.Reverse(periods);

                TimePeriodConst pperiod = TimePeriodConst.PERIOD_CURRENT;
                foreach (var period in periods)
                {
                    long rc = ServerTimeSeriesRuntimeEx.GetRecordCount(Parent, Symbol, period);
                    if (rc > 10000)
                    {
                        return pperiod;
                    }
                    else if (rc > 100)
                    {
                        pperiod = period;
                    }
                }

                return TimePeriodConst.PERIOD_CURRENT;
            }
        }

        public override ITimeSeriesRuntime LoadSeries(TimePeriodConst period, datetime focusedTime)
        {
            // TODO loaded with environment time
            ITimeSeriesRuntime seriesRuntime = ServerTimeSeriesRuntimeEx.Create(rmiManager, this, period, Online, focusedTime);

            return seriesRuntime;
        }

        // NOTE it mades environment orders/ordersHistory inconsistent temporally
        // should be internal
        // use it through environment.LoadOrders()
        public override void LoadOrders()
        {
            IEnvironmentRuntime environment = Parent;
            string path;
            if ("history".Equals(environment.HistoryDirectory) || string.IsNullOrEmpty(environment.HistoryDirectory))
            {
                path = environment.ImportedFromDirectory + "\\history\\";
            }
            else
            {
                path = environment.ImportedFromDirectory + "\\history\\" + environment.HistoryDirectory + "\\";
            }
            string ordersFile = path + Symbol + ".orders";
            string ordersHistoryFile = path + Symbol + ".history.orders";
            string ordersHistoryEtcFile = path + Symbol + ".history.etc.orders";
            load_ord<ServerTradeOrder>(Orders, TradePool.MODE_TRADES, ordersFile);
            load_ord<ServerHistoryOrder>(Orders, TradePool.MODE_HISTORY, ordersHistoryFile);
            load_ord<ServerHistoryOrderEtc>(Orders, TradePool.MODE_OPERATIONS, ordersHistoryEtcFile);
        }

        // use it through environment.SaveOrders()
        public override void SaveOrders()
        {
            IEnvironmentRuntime environment = Parent;
            string path;
            if ("history".Equals(environment.HistoryDirectory) || string.IsNullOrEmpty(environment.HistoryDirectory))
            {
                path = environment.ImportedFromDirectory + "\\history\\";
            }
            else
            {
                path = environment.ImportedFromDirectory + "\\history\\" + environment.HistoryDirectory + "\\";
            }
            string ordersFile = path + Symbol + ".orders";
            string ordersHistoryFile = path + Symbol + ".history.orders";
            string ordersHistoryEtcFile = path + Symbol + ".history.etc.orders";
            save_ord<ServerTradeOrder>(Orders, TradePool.MODE_TRADES, ordersFile);
            save_ord<ServerHistoryOrder>(Orders, TradePool.MODE_HISTORY, ordersHistoryFile);
            save_ord<ServerHistoryOrderEtc>(Orders, TradePool.MODE_OPERATIONS, ordersHistoryEtcFile);
        }

        void load_ord<O>(IServerOrdersTable tbl, TradePool pool, string file) where O : IOrder
        {
            try
            {
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader r = new BinaryReader(fs, Encoding.ASCII))
                    {
                        tbl.Load<O>(r, pool);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        void save_ord<O>(IServerOrdersTable tbl, TradePool pool, string file) where O : IOrder
        {
            try
            {
                using (FileStream fs = File.Open(file, FileMode.Create, FileAccess.Write))
                {
                    using (BinaryWriter w = new BinaryWriter(fs, Encoding.ASCII))
                    {
                        tbl.Save<O>(w, pool);
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        public override void Tick(double Bid, double Ask, double Volume)
        {
            Orders.Tick(Bid, Ask, Volume);
        }
    }


    public class ServerOrdersTable : ServerOrdersTableBase
    {

        internal ServerOrdersTable(GreenRmiManager rmiManager, IEnvironmentRuntime environment)
            : base(rmiManager)
        {
            Environment = environment;
        }

        internal ServerOrdersTable(GreenRmiManager rmiManager, ServerSymbolRuntime symbolRuntime, IServerOrdersTable parent)
            : base(rmiManager)
        {
            SymbolRuntime = symbolRuntime;
            Environment = symbolRuntime.Parent;
            Parent = parent;
            if (parent != null)
            {
                parent.Children.Add(this);
            }
        }


        public override datetime ScrolledBarTime
        {
            get
            {
                foreach (var v in ChildrenView)
                {
                    return v.ScrolledBarTime;
                }
                return datetime.MinValue;
            }
            set
            {
                foreach (var v in ChildrenView) {
                    v.ScrolledBarTime = value;
                }
            }
        }

        public override List<O> GetOrders<O>(TradePool pool)
        {
            switch (pool)
            {
                case TradePool.MODE_TRADES:
                    return (List<O>)(object)Orders;
                case TradePool.MODE_HISTORY:
                    return (List<O>)(object)OrdersHistory;
                case TradePool.MODE_OPERATIONS:
                    return (List<O>)(object)OrdersHistoryEtc;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void GenerateFromChildren()
        {
            Orders.Clear();
            OrdersHistory.Clear();
            OrdersHistoryEtc.Clear();

            Generate(Orders, TradePool.MODE_TRADES);
            Generate(OrdersHistory, TradePool.MODE_HISTORY);
            Generate(OrdersHistoryEtc, TradePool.MODE_OPERATIONS);
        }

        public override void Generate<O>(List<O> orders, TradePool pool)
        {

            // !!!
            orders.Clear();


            int[] indexes = new int[Children.Count];
            int[] tickets = new int[Children.Count];
            for (int i = 0; i < tickets.Length; i++)
            {
                indexes[i] = 0;
                List<O> os = ((IServerOrdersTable)Children[i]).GetOrders<O>(pool);
                if (os.Count > 0)
                {
                    tickets[i] = os[0].Ticket;
                }
                else
                {
                    tickets[i] = Int32.MaxValue;
                }
            }
            while (true)
            {
                int mini = -1, mint = Int32.MaxValue;
                for (int i = 0; i < tickets.Length; i++)
                {
                    int ticket = tickets[i];
                    if (ticket < mint)
                    {
                        mini = i;
                        mint = ticket;
                    }
                }
                if (mini >= 0)
                {
                    int index = indexes[mini];
                    List<O> os = ((IServerOrdersTable)Children[mini]).GetOrders<O>(pool);
                    O order = os[index];
                    orders.Add(order);

                    index++;
                    indexes[mini] = index;
                    if (index < os.Count)
                    {
                        order = os[index];
                        tickets[mini] = order.Ticket;
                    }
                    else
                    {
                        tickets[mini] = Int32.MaxValue;
                    }
                }
                else
                {
                    break;
                }

            }
        }

        public override void Load<O>(BinaryReader r, TradePool pool)
        {
            List<O> orders = GetOrders<O>(pool);
            orders.Clear();
            while (true)
            {
                try
                {
                    O order = createOrder<O>(pool);
                    order.Load(Environment, r);

                    // NOTE ! not added to history (n?)or environment here
                    orders.Add(order);
                }
                catch (EndOfStreamException)
                {
                    break;
                }
            }
        }

        public override void Save<O>(BinaryWriter w, TradePool pool)
        {
            List<O> orders = GetOrders<O>(pool);
            foreach (O order in orders)
            {
                order.Save(w);
            }
        }

        public override IOrder Select(int index, TradeSelectMode select = SELECT_BY_TICKET, TradePool pool = MODE_TRADES)
        {
            IOrder selectedOrder = Select<IOrder>(index, select, pool);
            return selectedOrder;
        }

        public override O Select<O>(int index, TradeSelectMode select = SELECT_BY_TICKET, TradePool pool = MODE_TRADES)
        {
            List<O> orders = GetOrders<O>(pool);
            O selectedOrder;

            if (select == SELECT_BY_POS)
            {
                if (index >= 0 && index < orders.Count)
                {
                    selectedOrder = orders[index];
                }
                else
                {
                    selectedOrder = default(O);
                }
            }
            else
            {
                O o = createOrder<O>(pool);
                o.Ticket = index;
                int i = orders.BinarySearch(o);
                o = orders[i];
                if (o.Ticket == index)
                {
                    selectedOrder = o;
                }
                else
                {
                    selectedOrder = default(O);
                }
            }

            return selectedOrder;
        }

        public override IOrder Add(symbol symbol, OrderType cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expirationDatetime = default(datetime), Color arrow_color = default(Color))
        {
            ServerTradeOrder order = new ServerTradeOrder(rmiManager);
            order.Ticket = Environment.GenerateTicket();
            order.OpenTime = Environment.Session.Time;

            order.Symbol = symbol;
            order.Type = cmd;
            order.Lots = volume;
            order.OpenPrice = price;
            order.Slippage = slippage;
            order.StopLoss = stoploss;
            order.TakeProfit = takeprofit;
            order.Comment = comment;
            order.MagicNumber = magic;
            order.Expiration = expirationDatetime;

            bool valid = Validate(order);
            // TODO Exception/No Exception context
            if (valid)
            {
                Add(order);
                Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_OPEN, order, arrow_color));
            }
            else
            {
                // TODO Exception/No Exception context
                order = null;
            }
            return order;
        }

        public override void Add(ITradeOrder order)
        {
            Orders.Add(order);

            if (Parent != null)
            {
                Parent.Add(order);
            }
        }

        public override void Add(IHistoryOrder order)
        {
            OrdersHistory.Add(order);

            if (Parent != null)
            {
                Parent.Add(order);
            }
        }

        public override void Add(IHistoryOrderEtc order)
        {
            order.CloseTime = datetime.Now;

            OrdersHistoryEtc.Add(order);

            if (Parent != null)
            {
                Parent.Add(order);
            }
            else
            {
                foreach (var v in ChildrenView)
                {
                    v.ApplyFilters(order);
                }
            }
        }


        public override void CloseOrDelete(ITradeOrder order)
        {
            Orders.Remove(order);
            ServerHistoryOrder h = null;
            if (order.Type == OP_BUY || order.Type == OP_SELL)
            {
                h = new ServerHistoryOrder(rmiManager, order);
                OrdersHistory.Add(h);
            }
            if (Parent != null)
            {
                Parent.CloseOrDelete(order);
            }
        }

        public override IOrder CloseOrDelete(int ticket, OrderTypeKind preferredKind, Color arrow_color)
        {
            ServerTradeOrder o = new ServerTradeOrder(rmiManager);
            o.Ticket = ticket;
            int i = Orders.BinarySearch(o);

            ITradeOrder order = Orders[i];
            if (preferredKind == order.Type.Kind())
            {
                CloseOrDelete(order);
                if (preferredKind == OrderTypeKind.DIRECT_TRADE)
                {
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_CLOSE, order, arrow_color));
                }
                else
                {
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_DELETE_PENDING, order, arrow_color));
                }
            }
            else
            {
                order = null;
            }
            return order;
        }

        public override bool Modify(ITradeOrder _order, double price, double stoploss, double takeprofit, datetime expiration, Color color = default(Color))
        {
            lock (Environment.Locker)
            {
                ServerTradeOrder order = new ServerTradeOrder(rmiManager, _order);

                switch (order.Type)
                {
                    case OP_BUYLIMIT:
                    case OP_BUYSTOP:
                    case OP_SELLLIMIT:
                    case OP_SELLSTOP:
                        order.OpenPrice = price;
                        order.StopLoss = stoploss;
                        order.TakeProfit = takeprofit;
                        order.Expiration = expiration;
                        break;

                    case OP_BUY:
                    case OP_SELL:
                        order.StopLoss = stoploss;
                        order.TakeProfit = takeprofit;
                        break;
                }


                bool valid = Validate(order);
                // TODO Exception/No Exception context
                if (valid)
                {
                    _order.Params = order.Params;
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_MODIFY, order, color));
                }
                else
                {
                    // TODO Exception/No Exception context
                }
                return valid;
            }
        }

        public override void UpdateOrder(ITradeOrder order)
        {
            // TODO Update at runtime
            double Bid = SymbolContext.Bid;
            // TODO Update at runtime
            double Ask = SymbolContext.Ask;

            switch (order.Type)
            {
                case OP_BUYLIMIT:
                    order.ClosePrice = Bid;
                    order.PendingOpenPrice = Ask;
                    break;
                case OP_BUYSTOP:
                    order.ClosePrice = Bid;
                    order.PendingOpenPrice = Ask;
                    break;
                case OP_SELLLIMIT:
                    order.ClosePrice = Ask;
                    order.PendingOpenPrice = Bid;
                    break;
                case OP_SELLSTOP:
                    order.ClosePrice = Ask;
                    order.PendingOpenPrice = Bid;
                    break;


                case OP_BUY:
                    order.ClosePrice = Bid;
                    order.Profit = order.ClosePrice - order.OpenPrice;
                    break;

                case OP_SELL:
                    order.ClosePrice = Ask;
                    order.Profit = order.ClosePrice - order.OpenPrice;
                    break;
            }
        }

        public override void Tick(double Bid, double Ask, double Volume)
        {

            lock (Environment.Locker)
            {

                // TODO process orders   etc??

                foreach (ServerTradeOrder order in Orders)
                {
                    UpdateOrder(order);

                    switch (order.Type)
                    {
                        case OP_BUYLIMIT:
                            if (order.OpenPrice <= Ask)
                            {
                                GenerateBuyFromPending(order, Ask);
                            }
                            break;
                        case OP_BUYSTOP:
                            if (order.OpenPrice >= Ask)
                            {
                                GenerateBuyFromPending(order, Ask);
                            }
                            break;
                        case OP_SELLLIMIT:
                            if (order.OpenPrice >= Bid)
                            {
                                GenerateSellFromPending(order, Bid);
                            }
                            break;
                        case OP_SELLSTOP:
                            if (order.OpenPrice <= Bid)
                            {
                                GenerateSellFromPending(order, Bid);
                            }
                            break;


                        case OP_BUY:
                            if (order.ClosePrice <= order.StopLoss)
                            {
                                StopByStopLoss(order);
                            }
                            else if (order.ClosePrice >= order.TakeProfit)
                            {
                                StopByTakeProfit(order);
                            }
                            break;

                        case OP_SELL:
                            if (order.ClosePrice >= order.StopLoss)
                            {
                                StopByStopLoss(order);
                            }
                            else if (order.ClosePrice <= order.TakeProfit)
                            {
                                StopByTakeProfit(order);
                            }
                            break;
                    }

                    UpdateOrder(order);
                }
            }
        }


        //////////////////////////////////////////////////////

        O createOrder<O>(TradePool pool) where O : IOrder
        {
            IOrder order;
            switch (pool)
            {
                case TradePool.MODE_TRADES:
                    order = new ServerTradeOrder(rmiManager);
                    break;
                case TradePool.MODE_HISTORY:
                    order = new ServerHistoryOrder(rmiManager);
                    break;
                case TradePool.MODE_OPERATIONS:
                    order = new ServerHistoryOrderEtc(rmiManager);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return (O)order;
        }

        private bool Validate(ServerTradeOrder order)
        {
            bool valid;
            double v, v1, v2;

            // TODO Exception/No Exception context
            //      generate Exceptions here

            switch (order.Type)
            {
                case OP_BUYLIMIT:
                    v1 = order.OpenPrice + SymbolContext.StopLevel * SymbolContext.Point;
                    v2 = order.OpenPrice - SymbolContext.StopLevel * SymbolContext.Point;
                    valid = order.PendingOpenPrice >= v1;
                    valid &= order.StopLoss <= v2;
                    break;
                case OP_BUYSTOP:
                    v = order.OpenPrice - SymbolContext.StopLevel * SymbolContext.Point;
                    valid = order.PendingOpenPrice <= v;
                    valid &= order.StopLoss <= v;
                    break;
                case OP_SELLLIMIT:
                    v1 = order.OpenPrice - SymbolContext.StopLevel * SymbolContext.Point;
                    v2 = order.OpenPrice + SymbolContext.StopLevel * SymbolContext.Point;
                    valid = order.PendingOpenPrice <= v1;
                    valid &= order.StopLoss >= v2;
                    break;
                case OP_SELLSTOP:
                    v = order.OpenPrice + SymbolContext.StopLevel * SymbolContext.Point;
                    valid = order.PendingOpenPrice >= v;
                    valid &= order.StopLoss >= v;
                    break;


                case OP_BUY:
                    v = order.OpenPrice - SymbolContext.StopLevel * SymbolContext.Point;
                    valid = order.StopLoss <= v;
                    break;

                case OP_SELL:
                    v = order.OpenPrice + SymbolContext.StopLevel * SymbolContext.Point;
                    valid = order.StopLoss >= v;
                    break;

                default:
                    throw new NotSupportedException("" + order.Type);
            }

            return valid;
        }

        private void GenerateBuyFromPending(ServerTradeOrder order, double Ask)
        {
            Orders.Remove(order);

            double delta = 0;

            switch (Environment.SlippageMode)
            {
                case SlippageMode.NO_SLIPPAGE:
                    order.Slippage = 0;
                    order.Type = OrderType.OP_BUY;
                    Add(order);
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_PENDING_OPENED, order));
                    return;

                case SlippageMode.SLIPPAGE_BY_TICK:
                    order.Slippage = 0;
                    order.Type = OrderType.OP_BUY;
                    order.OpenPrice = Ask;
                    Add(order);
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_PENDING_OPENED, order));
                    return;

                case SlippageMode.RANDOM_SLIPPAGE:
                    delta = Ask - order.OpenPrice;
                    double rnd = new Random(Environment.RandomSeed + (int)(Ask / 0.00001)).NextDouble() * delta;

                    order.Slippage = 0;
                    order.Type = OrderType.OP_BUY;
                    order.OpenPrice = Math.Round(order.OpenPrice + rnd, SymbolContext.Digits);
                    Add(order);
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_PENDING_OPENED, order));
                    return;
            }
        }

        private void GenerateSellFromPending(ServerTradeOrder order, double Bid)
        {
            Orders.Remove(order);

            double delta = 0;

            switch (Environment.SlippageMode)
            {
                case SlippageMode.NO_SLIPPAGE:
                    order.Slippage = 0;
                    order.Type = OrderType.OP_SELL;
                    Add(order);
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_PENDING_OPENED, order));
                    return;

                case SlippageMode.SLIPPAGE_BY_TICK:
                    order.Slippage = 0;
                    order.Type = OrderType.OP_SELL;
                    order.OpenPrice = Bid;
                    Add(order);
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_PENDING_OPENED, order));
                    return;

                case SlippageMode.RANDOM_SLIPPAGE:
                    delta = Bid - order.OpenPrice;
                    double rnd = new Random(Environment.RandomSeed + (int)(Bid / 0.00001)).NextDouble() * delta;

                    order.Slippage = 0;
                    order.Type = OrderType.OP_SELL;
                    order.OpenPrice = Math.Round(order.OpenPrice + rnd, SymbolContext.Digits);
                    Add(order);
                    Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_PENDING_OPENED, order));

                    return;
            }
        }

        private void StopByStopLoss(ServerTradeOrder order)
        {
            order.CloseTime = Environment.Session.Time;

            double delta = 0;

            switch (Environment.SlippageMode)
            {
                case SlippageMode.NO_SLIPPAGE:
                    order.ClosePrice = order.StopLoss;
                    break;
                case SlippageMode.SLIPPAGE_BY_TICK:
                    break;
                case SlippageMode.RANDOM_SLIPPAGE:
                    delta = order.StopLoss - order.ClosePrice;
                    double rnd = new Random(Environment.RandomSeed + (int)(order.ClosePrice / 0.00001)).NextDouble() * delta;

                    order.ClosePrice = Math.Round(order.ClosePrice + rnd, SymbolContext.Digits);
                    break;
            }

            Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_CLOSE_BY_SL, order));
            CloseOrDelete(order);
        }

        private void StopByTakeProfit(ServerTradeOrder order)
        {
            order.CloseTime = Environment.Session.Time;

            double delta = 0;

            switch (Environment.SlippageMode)
            {
                case SlippageMode.NO_SLIPPAGE:
                    order.ClosePrice = order.TakeProfit;
                    break;
                case SlippageMode.SLIPPAGE_BY_TICK:
                    break;
                case SlippageMode.RANDOM_SLIPPAGE:
                    delta = order.TakeProfit - order.ClosePrice;
                    double rnd = new Random(Environment.RandomSeed + (int)(order.ClosePrice / 0.00001)).NextDouble() * delta;

                    order.ClosePrice = Math.Round(order.ClosePrice + rnd, SymbolContext.Digits);
                    break;
            }

            Add(new ServerHistoryOrderEtc(rmiManager, TradeOperation.OP_CLOSE_BY_TP, order));
            CloseOrDelete(order);
        }

    }


}
