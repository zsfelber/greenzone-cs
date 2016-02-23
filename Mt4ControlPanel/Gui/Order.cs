using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mt4ControlPanelGui
{
    public enum OrderType
    {
        Buy,
        Sell,
        BuyLimit,
        SellLimit,
        BuyStop,
        SellStop,
        CloseOrDelete,
        ReloadAll,
        Modify
    }


    public class OrderDefault
    {
        public static readonly OrderDefault[] DefaultsNew = 
        {
            new OrderDefault("default",          OrderType.Buy,     2000000, 1, 0,   100, 1000),
            new OrderDefault("spike buy",        OrderType.BuyStop, 2000001, 1, 100, 0,   1100),
            new OrderDefault("spike sell",       OrderType.SellStop,2000001, 1, 100, 0,   1100),
            new OrderDefault("trend buy 1500",   OrderType.Buy,     2000002, 1, 0,   200, 1500),
            new OrderDefault("trend sell 1500",  OrderType.Sell,    2000002, 1, 0,   200, 1500),
            new OrderDefault("trend buy 6000",   OrderType.Buy,     2000002, 1, 0,   200, 6000),
            new OrderDefault("trend sell 6000",  OrderType.Sell,    2000002, 1, 0,   200, 6000),
            new OrderDefault("trend buy 10000",  OrderType.Buy,     2000002, 1, 0,   200, 10000),
            new OrderDefault("trend sell 10000", OrderType.Sell,    2000002, 1, 0,   200, 10000),
        };

        public static readonly OrderDefault[] DefaultsModify = 
        {
            new OrderDefault("default",       OrderType.Modify,        Int32.MinValue, Int32.MinValue,  Int32.MinValue, Int32.MinValue, Int32.MinValue),
            new OrderDefault("close 0.1",     OrderType.CloseOrDelete, Int32.MinValue, 0.1,             Int32.MinValue, Int32.MinValue, Int32.MinValue),
            new OrderDefault("set sl noloss", OrderType.Modify,        Int32.MinValue, Int32.MinValue,  Int32.MinValue, -10,            Int32.MinValue),
        };

        public OrderDefault(string name, OrderType defaultType, Int32 defaultMagic, double defaultLots, int defaultLimitPoints, int defaultSlPoints, int defaultTpPoints)
        {
            this.name = name;

            this.defaultType = defaultType;
            this.defaultMagic = defaultMagic;
            this.defaultLots = defaultLots;
            this.defaultLimitPoints = defaultLimitPoints;
            this.defaultSlPoints = defaultSlPoints;
            this.defaultTpPoints = defaultTpPoints;
        }

        OrderType defaultType;
        public OrderType DefaultType
        {
            get
            {
                return defaultType;
            }
            set
            {
                defaultType = value;
                isChanged = true;
            }
        }

        Int32 defaultMagic = 0;
        public Int32 DefaultMagic
        {
            get
            {
                return defaultMagic;
            }
            set
            {
                defaultMagic = value;
                isChanged = true;
            }
        }

        double defaultLots = 0.1;
        public double DefaultLots
        {
            get
            {
                return defaultLots;
            }
            set
            {
                defaultLots = value;
                isChanged = true;
            }
        }

        int defaultLimitPoints;
        public int DefaultLimitPoints
        {
            get
            {
                return defaultLimitPoints;
            }
            set
            {
                defaultLimitPoints = value;
                isChanged = true;
            }
        }

        int defaultSlPoints = 100;
        public int DefaultSlPoints 
        {
            get
            {
                return defaultSlPoints;
            }
            set
            {
                defaultSlPoints = value;
                isChanged = true;
            }
        }

        int defaultTpPoints = 1000;
        public int DefaultTpPoints 
        {
            get
            {
                return defaultTpPoints;
            }
            set
            {
                defaultTpPoints = value;
                isChanged = true;
            }
        }


        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                isChanged = true;
            }
        }

        bool isChanged;
        public bool IsChanged
        {
            get
            {
                return isChanged;
            }
            set
            {
                isChanged = value;
            }
        }

        public OrderDefault Clone()
        {
            return (OrderDefault)base.MemberwiseClone();
        }
    }

    public class Order
    {
        public int Ticket { get; set; }

        static OrderDefault orderDefaults0 = OrderDefault.DefaultsNew[0];
        static OrderDefault orderDefaults = OrderDefault.DefaultsNew[0].Clone();

        public static OrderDefault Default0
        {
            get
            {
                return orderDefaults0;
            }
        }

        public OrderDefault ThisDefault
        {
            get
            {
                return orderDefaults;
            }
            set
            {
                orderDefaults0 = value;

                if (orderDefaults0 != null)
                {
                    orderDefaults = orderDefaults0.Clone();

                    type = orderDefaults.DefaultType;

                    IsValue = false;

                    if (orderDefaults.DefaultSlPoints != Int32.MinValue)
                    {
                        slPoints = orderDefaults.DefaultSlPoints;
                    }

                    if (orderDefaults.DefaultTpPoints != Int32.MinValue)
                    {
                        tpPoints = orderDefaults.DefaultTpPoints;
                    }

                    if (orderDefaults.DefaultLimitPoints != Int32.MinValue)
                    {
                        limitPoints = orderDefaults.DefaultLimitPoints;
                    }

                    if (orderDefaults.DefaultMagic != Int32.MinValue)
                    {
                        magic = orderDefaults.DefaultMagic;
                    }

                    if (orderDefaults.DefaultLots != Int32.MinValue)
                    {
                        lots = orderDefaults.DefaultLots;
                    }

                    Comment = orderDefaults.Name;
                }
            }
        }

        public bool IsChanged
        {
            get
            {
                return orderDefaults.IsChanged;
            }
        }

        OrderType type = orderDefaults.DefaultType;
        public OrderType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public OrderType TypeGui
        {
            get
            {
                return type;
            }
            set
            {
                orderDefaults.DefaultType = type = value;
            }
        }

        double openPrice;
        public double OpenPrice
        {
            get
            {
                if ((IsOpenPricePointsEditable || IsNonPending || openPrice == 0) && GetPrice0() != 0)
                {
                    switch (Type)
                    {
                        case OrderType.BuyLimit:
                        case OrderType.SellStop:
                            openPrice = Math.Round(GetPrice0() - limitPoints * Point, Digits);
                            break;
                        case OrderType.BuyStop:
                        case OrderType.SellLimit:
                            openPrice = Math.Round(GetPrice0() + limitPoints * Point, Digits);
                            break;
                    }
                }
                return openPrice;
            }
            set
            {
                openPrice = value;
            }
        }
        public DateTime OpenTime { get; set; }
        public double ClosePrice { get; set; }
        public DateTime CloseTime { get; set; }
        double sl;
        public double SL
        {
            get
            {
                if ((IsPoints || sl == 0) && GetPrice0() != 0)
                {
                    if (IsBuy)
                    {
                        sl = Math.Round(GetPrice0() - slPoints * Point, Digits);
                    }
                    else
                    {
                        sl = Math.Round(GetPrice0() + slPoints * Point, Digits);
                    }
                }
                return sl;
            }
            set
            {
                sl = value;
            }
        }
        double tp;
        public double TP
        {
            get
            {
                if ((IsPoints || tp == 0) && GetPrice0() != 0)
                {
                    if (IsBuy)
                    {
                        tp = Math.Round(GetPrice0() + tpPoints * Point, Digits);
                    }
                    else
                    {
                        tp = Math.Round(GetPrice0() - tpPoints * Point, Digits);
                    }
                }
                return tp;
            }
            set
            {
                tp = value;
            }
        }

        Int16 slip = 10;
        public Int16 Slip {
            get
            {
                return slip;
            }
            set
            {
                slip = value;
            }
        }

        Int32 magic = orderDefaults.DefaultMagic;
        public Int32 Magic
        {
            get
            {
                return magic;
            }
            set
            {
                magic = value;
            }
        }
        public Int32 MagicGui
        {
            get
            {
                return magic;
            }
            set
            {
                orderDefaults.DefaultMagic = magic = value;
            }
        }

        public string Comment { get; set; }
        public Int16 Digits { get; set; }

        double lots = orderDefaults.DefaultLots;
        public double Lots
        {
            get
            {
                return lots;
            }
            set
            {
                lots = value;
            }
        }
        public double LotsGui
        {
            get
            {
                return lots;
            }
            set
            {
                orderDefaults.DefaultLots = lots = value;
            }
        }

        public string PriceFormat
        {
            get
            {
                return "0.0000000000".Substring(0, Digits + 2);
            }
        }

        public double Point
        {
            get
            {
                return Math.Pow(0.1, Digits);
            }
        }

        public string FormattedBid
        {
            get
            {
                return Bid.ToString(PriceFormat);
            }
        }
        public string FormattedAsk
        {
            get
            {
                return Ask.ToString(PriceFormat);
            }
        }
        public string FormattedOpenPrice
        {
            get
            {
                return OpenPrice.ToString(PriceFormat);
            }
        }
        public string FormattedClosePrice
        {
            get
            {
                return ClosePrice.ToString(PriceFormat);
            }
        }
        public string FormattedSL
        {
            get
            {
                return SL.ToString(PriceFormat);
            }
        }
        public string FormattedTP
        {
            get
            {
                return TP.ToString(PriceFormat);
            }
        }

        double bid;
        public double Bid
        {
            get { return bid; }
            set
            {
                bid = value;
                if (!IsOrderOpened)
                {
                    if (IsSell)
                    {
                        if (IsPoints || IsNonPending)
                        {
                            openPrice = bid;
                        }
                    }
                    else
                    {
                        ClosePrice = bid;
                    }
                }
            }
        }

        double ask;
        public double Ask
        {
            get { return ask; }
            set
            {
                ask = value;
                if (!IsOrderOpened)
                {
                    if (IsBuy)
                    {
                        if (IsPoints || IsNonPending)
                        {
                            openPrice = ask;
                        }
                    }
                    else
                    {
                        ClosePrice = ask;
                    }
                }
            }
        }

        public bool BeingSaved
        {
            get;
            set;
        }

        public bool IsBuy
        {
            get
            {
                switch (Type)
                {
                    case OrderType.Buy:
                    case OrderType.BuyLimit:
                    case OrderType.BuyStop:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                if (value)
                {
                    SetBS(true);
                }
            }
        }

        public bool IsSell
        {
            get
            {
                switch (Type)
                {
                    case OrderType.Sell:
                    case OrderType.SellLimit:
                    case OrderType.SellStop:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                if (value)
                {
                    SetBS(false);
                }
            }
        }

        public bool IsClose
        {
            get
            {
                return Type == OrderType.CloseOrDelete;
            }
        }

        public bool IsNonPending
        {
            get
            {
                switch (Type)
                {
                    case OrderType.Buy:
                    case OrderType.Sell:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                if (value)
                {
                    SetPending(0);
                }
            }
        }

        public bool IsPending
        {
            get
            {
                switch (Type)
                {
                    case OrderType.Buy:
                    case OrderType.Sell:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public bool IsLimit
        {
            get
            {
                switch (Type)
                {
                    case OrderType.BuyLimit:
                    case OrderType.SellLimit:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                if (value)
                {
                    SetPending(1);
                }
            }
        }

        public bool IsStop
        {
            get
            {
                switch (Type)
                {
                    case OrderType.BuyStop:
                    case OrderType.SellStop:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                if (value)
                {
                    SetPending(2);
                }
            }
        }

        public bool IsOrderOpened
        {
            get
            {
                return Ticket > 0;
            }
        }

        public bool IsOrderNew
        {
            get
            {
                return Ticket <= 0;
            }
        }

        public bool IsOrderNormal
        {
            get
            {
                return Type < OrderType.CloseOrDelete;
            }
        }

        public bool IsOpen
        {
            get;
            set;
        }

        public bool IsOpenBid
        {
            get
            {
                return !IsOpen && IsSell;
            }
            set
            {
                IsOpen = !value;
            }
        }

        public bool IsOpenAsk
        {
            get
            {
                return !IsOpen && IsBuy;
            }
            set
            {
                IsOpen = !value;
            }
        }

        public bool IsValue
        {
            get;
            set;
        }

        public bool IsPoints
        {
            get
            {
                return !IsValue;
            }
            set
            {
                if (value)
                {
                    IsValue = false;
                }
            }
        }

        public bool IsOpenPricePointsEditable
        {
            get
            {
                return IsPoints && IsPending;
            }
        }

        public bool IsOpenPriceValueEditable
        {
            get
            {
                return IsValue && IsPending;
            }
        }

        int limitPoints = orderDefaults.DefaultLimitPoints;
        public int LimitPoints
        {
            get
            {
                if (IsOpenPriceValueEditable)
                {
                    limitPoints = (int)Math.Round(Math.Abs(openPrice - GetPrice0()) / Point);
                }
                return limitPoints;
            }
            set
            {
                limitPoints = value;
            }
        }
        public int LimitPointsGui
        {
            get
            {
                return LimitPoints;
            }
            set
            {
                orderDefaults.DefaultLimitPoints = limitPoints = value;
            }
        }

        int slPoints = orderDefaults.DefaultSlPoints;
        public int SLPoints
        {
            get
            {
                if (IsValue)
                {
                    if (IsBuy)
                    {
                        slPoints = (int)Math.Round( (GetPrice0() - sl) / Point);
                    }
                    else
                    {
                        slPoints = (int)Math.Round((sl - GetPrice0()) / Point);
                    }
                }
                return slPoints;
            }
            set
            {
                slPoints = value;
            }
        }
        public int SlPointsGui
        {
            get
            {
                return SLPoints;
            }
            set
            {
                orderDefaults.DefaultSlPoints = slPoints = value;
            }
        }

        int tpPoints = orderDefaults.DefaultTpPoints;
        public int TPPoints
        {
            get
            {
                if (IsValue)
                {
                    tpPoints = (int)Math.Round(Math.Abs(tp - GetPrice0()) / Point);
                }
                return tpPoints;
            }
            set
            {
                tpPoints = value;
            }
        }
        public int TPPointsGui
        {
            get
            {
                return TPPoints;
            }
            set
            {
                orderDefaults.DefaultTpPoints = tpPoints = value;
            }
        }

        double GetPrice0()
        {
            if (IsOpen)
            {
                return openPrice;
            }
            else
            {
                if (IsBuy)
                {
                    return Ask;
                }
                else
                {
                    return Bid;
                }
            }
        }

        void SetBS(bool bs)
        {
            switch (Type)
            {
                case OrderType.Buy:
                case OrderType.Sell:
                    TypeGui = bs ? OrderType.Buy : OrderType.Sell;
                    break;
                case OrderType.BuyLimit:
                case OrderType.SellLimit:
                    TypeGui = bs ? OrderType.BuyLimit : OrderType.SellLimit;
                    break;
                case OrderType.BuyStop:
                case OrderType.SellStop:
                    TypeGui = bs ? OrderType.BuyStop : OrderType.SellStop;
                    break;
            }
            IsOpen = false;
            IsValue = false;
        }

        void SetPending(int p)
        {
            switch (p)
            {
                case 0:
                    TypeGui = IsBuy ? OrderType.Buy : OrderType.Sell;
                    break;
                case 1:
                    TypeGui = IsBuy ? OrderType.BuyLimit : OrderType.SellLimit;
                    break;
                case 2:
                    TypeGui = IsBuy ? OrderType.BuyStop : OrderType.SellStop;
                    break;
            }
        }

        public override int GetHashCode()
        {
            return Ticket;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Ticket == ((Order)obj).Ticket;
        }

        public Order Clone()
        {
            return (Order)base.MemberwiseClone();
        }
    }

    class StartArg
    {
        public StartArg(string symbol, int digits)
        {
            Symbol = symbol;
            Digits = digits;
        }

        public string Symbol { get; set; }
        public int Digits { get; set; }
    }

    class TickTask
    {
        public TickTask(double bid, double ask)
        {
            Bid = bid;
            Ask = ask;
        }

        public double Bid { get; set; }
        public double Ask { get; set; }
    }

    class RecordOrdersTask
    {
        public RecordOrdersTask(int numOrders)
        {
            NumOrders = numOrders;
        }

        public int NumOrders { get; set; }
    }

    class AddOrderTask
    {
        public AddOrderTask(Order order)
        {
            Order = order;
        }

        public Order Order { get; set; }
    }
}