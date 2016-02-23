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

    public abstract class OrdersHistoryViewEx : OrdersHistoryViewBase
    {
        public delegate void DHistoryChanged(bool groupsChanged);
        public event DHistoryChanged HistoryChanged;

        public OrdersHistoryViewEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public OrdersHistoryViewEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected OrdersHistoryViewEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        // IChartOwner

        public override double Point
        {
            get
            {
                return 0.01;
            }
        }

        public override int Digits
        {
            get
            {
                return 2;
            }
        }

        public override string SymbolFormat
        {
            get
            {
                return "#";
            }
        }

        public override datetime ScrolledBarTime
        {
            get
            {
                if (BalanceTimeHistAsLArr.Length == 0)
                {
                    return datetime.MinValue;
                }
                else
                {
                    return (datetime)BalanceTimeHistAsLArr[SeriesRange.OffsetTo];
                }
            }
            set
            {
                int ind = BalanceTimeHistAsLArr.BinarySearch((long)value, -1);
                if (ind < 0)
                {
                    ind = ~ind;
                }
                if (ind == BalanceTimeHistAsLArr.Length)
                {
                    ind--;
                }
                SeriesRange r = SeriesRange;
                r.ChangeOffsetFrom(ind);
                SeriesRange = r;
            }
        }

        public override datetime ParentScrolledBarTime
        {
            get
            {
                return Parent.ScrolledBarTime;
            }
            set
            {
                Parent.ScrolledBarTime = value;
            }
        }

        public override int ParentCursorPosition
        {
            get
            {
                return Parent.CursorPosition;
            }
            set
            {
                Parent.CursorPosition = value;
            }
        }

        public override int CursorPosition
        {
            get
            {
                return SeriesRange.CursorPosition;
            }
            set
            {
                SeriesRange r = SeriesRange;
                r.CursorPosition = value;
                SeriesRange = r;
            }
        }

        public override long RecordCount
        {
            get
            {
                return BalanceTimeHistAsLArr.Length;
            }
        }

        public override long TotalFileOffset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override datetime From
        {
            get
            {
                if (BalanceTimeHistAsLArr.Length == 0)
                {
                    return datetime.MaxValue;
                }
                else
                {
                    LArr balanceTimeHistAsLArr = BalanceTimeHistAsLArr;
                    return (datetime)balanceTimeHistAsLArr[balanceTimeHistAsLArr.Length - 1];
                }
            }
        }

        public override datetime To
        {
            get
            {
                if (BalanceTimeHistAsLArr.Length == 0)
                {
                    return datetime.MinValue;
                }
                else
                {
                    return (datetime)BalanceTimeHistAsLArr[0];
                }
            }
        }

        public override LArr sLTime
        {
            get
            {
                return BalanceTimeHistAsLArr;
            }
        }

        public override IEnumerable<object> Groups
        {
            get
            {
                if (string.IsNullOrEmpty(Filter.GroupBy))
                {
                    return null;
                }
                else
                {
                    var r = from o in FilteredOrders
                            select o[Filter.GroupBy];
                    r = r.Distinct();
                    return r;
                }
            }
        }

        public override void LoadForward(int offset)
        {
            int to = SeriesRange.OffsetTo - offset;
            SeriesRange.ChangeOffsetTo(to);
            if (SeriesRange.OffsetFrom < 0)
            {
                throw new TimeSeriesEOFException();
            }
        }

        public override void LoadAtTotal(long total_ind)
        {
            SeriesRange.ChangeOffsetTo((int)total_ind);
            if (SeriesRange.OffsetFrom < 0 || SeriesRange.OffsetTo >= FilteredOrders.Count)
            {
                throw new TimeSeriesEOFException();
            }
        }

        public override void ApplyFilters(IHistoryOrderEtc newOrder = null)
        {
            SortedSet<object> groups0;
            if (HistoryChanged != null)
            {
                var g = Groups;
                if (g != null)
                {
                    groups0 = new SortedSet<object>(g);
                }
                else
                {
                    groups0 = null;
                }
            }
            else
            {
                groups0 = null;
            }

            SortedSet<OrderType> types = new SortedSet<OrderType>();
            if (Filter.Buy)
            {
                types.Add(OrderType.OP_BUY);
                if (Filter.Limit)
                {
                    types.Add(OrderType.OP_BUYLIMIT);
                }
                if (Filter.Stop)
                {
                    types.Add(OrderType.OP_BUYSTOP);
                }
            }
            if (Filter.Sell)
            {
                types.Add(OrderType.OP_SELL);
                if (Filter.Limit)
                {
                    types.Add(OrderType.OP_SELLLIMIT);
                }
                if (Filter.Stop)
                {
                    types.Add(OrderType.OP_SELLSTOP);
                }
            }

            List<IHistoryOrderEtc> os;
            double balance;
            if (newOrder == null)
            {
                FilteredOrders.Clear();
                os = Parent.OrdersHistoryEtc;
                balance = Parent.Environment.StartingBalance;
            }
            else
            {
                os = new List<IHistoryOrderEtc>();
                os.Add(newOrder);

                if (FilteredOrders.Count > 0)
                {
                    balance = FilteredOrders[FilteredOrders.Count - 1].Balance;
                }
                else
                {
                    balance = Parent.Environment.StartingBalance;
                }
            }
            var filtOsQ = from h in os
                          where
                              types.Contains(h.Type) &&
                              (Filter.From <= h.OpenTime && h.OpenTime <= Filter.To ||
                              Filter.From <= h.CloseTime && h.CloseTime <= Filter.To) &&
                              (Filter.Ticket == 0 || Filter.Ticket == h.Ticket) &&
                              (Filter.Symbol == null || h.Symbol.strSymbol.Equals(Filter.Symbol)) &&
                              (Filter.Operation == TradeOperation.OP_NONE || Filter.Operation == h.Operation) &&
                              (Filter.Expert == null || Filter.Expert.Equals(h.Expert.SystemTypeId)) &&
                              (string.IsNullOrEmpty(Filter.Comment) || (h.Comment != null && h.Comment.ToLower().Contains(Filter.Comment.ToLower()))) &&
                              (Filter.Magic == -1 || Filter.Magic == h.MagicNumber)
                          select h;

            int i = FilteredOrders.Count;
            foreach (var o in filtOsQ)
            {
                FilteredOrders.Add(o);
            }

            BalanceHistAsDArr.SetLengthAndDetachChildren(FilteredOrders.Count);
            BalanceTimeHistAsLArr.SetLengthAndDetachChildren(FilteredOrders.Count);

            foreach (var h in filtOsQ)
            {
                balance += h.Profit;
                h.Balance = balance;

                BalanceHistAsDArr[i] = h.Balance;
                BalanceTimeHistAsLArr[i] = (long)h.CloseTime;
                i++;
            }

            if (HistoryChanged != null)
            {
                SortedSet<object> groups;
                var g = Groups;
                if (g != null)
                {
                    groups = new SortedSet<object>(g);
                }
                else
                {
                    groups = null;
                }
                HistoryChanged(groups0 == null ? groups != null : groups == null || groups0.SetEquals(groups));
            }
        }
    }

    public abstract class OrderEx : OrderBase, IComparable<IOrder>
    {
        public OrderEx(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
		}

		public OrderEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

		protected OrderEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
        }

        public override void Load(IEnvironmentRuntime env, BinaryReader r)
        {
            Expert = env.Experts[Encoding.ASCII.GetString(r.ReadBytes(128))];
            Symbol = env.GetSymbol(Encoding.ASCII.GetString(r.ReadBytes(12)));
            Type = (OrderType)r.ReadInt32();
            Ticket = r.ReadInt32();

            Lots = r.ReadDouble();

            StopLoss = r.ReadDouble();
            TakeProfit = r.ReadDouble();

            OpenTime = r.ReadInt32();
            OpenPrice = r.ReadDouble();

            CloseTime = r.ReadInt32();
            ClosePrice = r.ReadDouble();

            Comment = Encoding.ASCII.GetString(r.ReadBytes(64));
            Expiration = r.ReadInt32();
            MagicNumber = r.ReadInt32();

            Commission = r.ReadDouble();
            Profit = r.ReadDouble();
            Swap = r.ReadDouble();
        }

        public override void Save(BinaryWriter w)
        {
            w.Write(Encoding.ASCII.GetBytes(Expert.SystemTypeId.PadRight(128,'\0')));
            w.Write(Encoding.ASCII.GetBytes(Symbol.strSymbol.PadRight(12, '\0')));
            w.Write((int)Type);
            w.Write((int)Ticket);

            w.Write((double)Lots);

            w.Write((double)StopLoss);
            w.Write((double)TakeProfit);

            w.Write((int)OpenTime);
            w.Write((double)OpenPrice);

            w.Write((int)CloseTime);
            w.Write((double)ClosePrice);

            w.Write(Encoding.ASCII.GetBytes(Comment));
            for (int i = Comment.Length; i < 64; i++)
            {
                w.Write((byte)0);
            }

            w.Write((int)Expiration);
            w.Write((int)MagicNumber);

            w.Write((double)Commission);
            w.Write((double)Profit);
            w.Write((double)Swap);
        }

        //void Print();
        //bool Close(double lots, double price, int slippage, int Color = 0);
        //bool CloseBy(int opposite, int Color=0);
        //bool Delete(int Color=0)  ;
        //bool Modify(double price, double stoploss, double takeprofit, datetime expiration, int arrow_color=0);


        public override object this[string field]
        {
            get
            {
                switch (field)
                {
                    case "Expert": return Expert;
                    case "Symbol": return Symbol;
                    case "Type": return Type;
                    case "Ticket": return Ticket;

                    case "Lots": return Lots;

                    case "StopLoss": return StopLoss;
                    case "TakeProfit": return TakeProfit;

                    case "OpenTime": return OpenTime;
                    case "OpenPrice": return OpenPrice;
                    case "PendingOpenPrice": return PendingOpenPrice;

                    case "CloseTime": return CloseTime;
                    case "ClosePrice": return ClosePrice;

                    case "Comment": return Comment;
                    case "Expiration": return Expiration;
                    case "Magic": return MagicNumber;
                    case "MagicNumber": return MagicNumber;

                    // NOTE in second currency of pair
                    case "Commission": return Commission;
                    case "Profit": return Profit;
                    case "Swap": return Swap;
                    default: throw new ArgumentException(field);
                }
            }
        }

        public override Dictionary<string, object> Params
        {
            get
            {
                Dictionary<string, object> r = new Dictionary<string, object>();
                r["Expert"] = Expert;
                r["Symbol"] = Symbol;
                r["Type"] = Type;
                r["Ticket"] = Ticket;

                r["Lots"] = Lots;

                r["StopLoss"] = StopLoss;
                r["TakeProfit"] = TakeProfit;

                r["OpenTime"] = OpenTime;
                r["OpenPrice"] = OpenPrice;
                r["PendingOpenPrice"] = PendingOpenPrice;

                r["CloseTime"] = CloseTime;
                r["ClosePrice"] = ClosePrice;

                r["Comment"] = Comment;
                r["Expiration"] = Expiration;
                r["MagicNumber"] = MagicNumber;

                // NOTE in second currency of pair
                r["Commission"] = Commission;
                r["Profit"] = Profit;
                r["Swap"] = Swap;

                return r;
            }
            set
            {
                Expert = (Mt4ExecutableInfo)value["Expert"];
                Symbol = (symbol)value["Symbol"];
                Type = (OrderType)value["Type"];
                Ticket = (int)value["Ticket"];

                Lots = (double)value["Lots"];

                StopLoss = (double)value["StopLoss"];
                TakeProfit = (double)value["TakeProfit"];

                OpenTime = (datetime)value["OpenTime"];
                OpenPrice = (double)value["OpenPrice"];
                PendingOpenPrice = (double)value["PendingOpenPrice"];

                CloseTime = (datetime)value["CloseTime"];
                ClosePrice = (double)value["ClosePrice"];

                Comment = (string)value["Comment"];
                Expiration = (datetime)value["Expiration"];
                MagicNumber = (int)value["MagicNumber"];

                // NOTE in second currency of pair
                Commission = (double)value["Commission"];
                Profit = (double)value["Profit"];
                Swap = (double)value["Swap"];

            }
        }

        public virtual int CompareTo(IOrder other)
        {
            return Ticket - other.Ticket;
        }
    }

    public class TradeOrderEx : TradeOrderBase
    {
        public TradeOrderEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public TradeOrderEx(GreenRmiManager rmiManager, ITradeOrder order)
            : base(rmiManager)
        {
            Params = order.Params;
        }

        public override Dictionary<string, object> Params
        {
            get
            {
                Dictionary<string, object> r = base.Params;
                r["Slippage"] = Slippage;
                return r;
            }
            set
            {
                base.Params = value;
                Slippage = (int)value["Slippage"];
            }
        }
    }

    public class HistoryOrderEx : HistoryOrderBase
    {
        public HistoryOrderEx(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
		}

        public HistoryOrderEx(GreenRmiManager rmiManager, ITradeOrder order)
            : base(rmiManager)
        {
            Params = order.Params;
        }

        public HistoryOrderEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

        protected HistoryOrderEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
        }

    }


    public class HistoryOrderEtcEx : HistoryOrderEtcBase
    {

        public HistoryOrderEtcEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public HistoryOrderEtcEx(GreenRmiManager rmiManager, TradeOperation operation, ITradeOrder order)
            : base(rmiManager, order)
        {
            Params = order.Params;
            this.Operation = operation;
        }
        public HistoryOrderEtcEx(GreenRmiManager rmiManager, TradeOperation operation, ITradeOrder order, Color color)
            : base(rmiManager, order)
        {
            Params = order.Params;
            this.Operation = operation;
            this.Color = color;
        }

        public override Dictionary<string, object> Params
        {
            get
            {
                Dictionary<string, object> r = base.Params;
                r["Operation"] = Operation;
                r["Color"] = Color;
                return r;
            }
            set
            {
                base.Params = value;
                Operation = (TradeOperation)value["Operation"];
                Color = (Color)value["Color"];
            }
        }

        public override int CompareTo(IOrder other)
        {
            return Math.Sign((long)OpenTime - (long)other.OpenTime);
        }
    }

}
