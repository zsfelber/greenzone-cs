using System;
using System.Collections.Generic;

[Serializable]
public class ZsfSymbolConfig : VariableConfig, ICopiable<ZsfSymbolConfig>
{
    private ZsfAccountConfig parent;
    private string symbol;
    private double defaultCurrencyRate;
    private double low;// 1 Low day price. 
    private double high;// 2 High day price. 
    private int time;// 5// The last incoming tick time (last known server time). 
    private double bid;// 9// Last incoming bid price. For the current symbol, it is stored in the predefined variable Bid 
    private double ask;// 10// Last incoming ask price. For the current symbol, it is stored in the predefined variable Ask 
    private double point;// 11// Point size in the quote currency. For the current symbol, it is stored in the predefined variable Point 
    private int digits;// 12// Count of digits after decimal point in the symbol prices. For the current symbol, it is stored in the predefined variable Digits 
    private int spread;// 13// Spread value in points. 
    private int stopLevel;// 14// Stop level in points. 
    private int lotSize;// 15// Lot size in the base currency. 
    private double tickValue;// 16// Tick value in the deposit currency. 
    private double tickSize;// 17// Tick size in the quote currency. 
    private double swapLong;// 18// Swap of the long position. 
    private double swapShort;// 19// Swap of the short position. 
    private int starting;// 20// Market starting date (usually used for futures). 
    private int expiration;// 21// Market expiration date (usually used for futures). 
    private bool tradeAllowed;// 22// Trade is allowed for the symbol. 
    private double minLot;// 23// Minimum permitted amount of a lot. 
    private double lotStep;// 24// Step for changing lots. 
    private double maxLot;// 25// Maximum permitted amount of a lot. 
    private int swapType;// 26// Swap calculation method. 0 - in points; 1 - in the symbol base currency; 2 - by interest; 3 - in the margin currency. 
    private int profitCalcMode;// 27// Profit calculation mode. 0 - Forex; 1 - CFD; 2 - Futures. 
    private int marginCalcMode;// 28// Margin calculation mode. 0 - Forex; 1 - CFD; 2 - Futures; 3 - CFD for indices. 
    private double marginInit;// 29// Initial margin requirements for 1 lot. 
    private double marginMaintenance;// 30// Margin to maintain open positions calculated for 1 lot. 
    private double marginHedged;// 31// Hedged margin calculated for 1 lot. 
    private double marginRequired;// 32// Free margin required to open 1 lot for buying. 
    private int freezeLevel;// 33// Order freeze level in points. If the execution price lies within the range defined by the freeze level, the order cannot be modified, cancelled or closed. 

    private SerializableDictionary<int, ZsfPeriodConfig> periodConfig;

    public ZsfSymbolConfig() : this(null,null)
    {
    }
    public ZsfSymbolConfig(ZsfAccountConfig parent, string symbol)
    {
        this.parent = parent;
        this.symbol = symbol;
        periodConfig = new SerializableDictionary<int, ZsfPeriodConfig>();
    }

    public override object GetId()
    {
        return symbol;
    }

    protected override void SetId(object id)
    {
        symbol = (string)id;
    }

    public ZsfAccountConfig Parent
    {
        get
        {
            return parent;
        }
        set
        {
            if (parent != null)
            {
                throw new Exception("parent != null");
            }
            parent = value;
            OnPropertyChanged("Parent", null, value);
        }
    }

    public string Symbol
    {
        get { return symbol; }
        set
        {
            if (symbol != null)
            {
                throw new Exception("symbol != null");
            }
            symbol = value;
            OnPropertyChanged("Symbol", null, value);
        }
    }

    public double DefaultCurrencyRate
    {
        get { return defaultCurrencyRate; }
        set
        {
            double old = defaultCurrencyRate;
            defaultCurrencyRate = value;
            OnPropertyChanged("DefaultCurrencyRate", old, value);
        }
    }

    public double Low { 
        get { return low; }
        protected set
        { 
            double old=low;
            low = value;
            OnPropertyChanged("Low", old, value);
        }
    }
    public double High { 
        get { return high; }
        protected set
        { 
            double old=high;
            high = value;
            OnPropertyChanged("High", old, value);
        }
    }
    public int Time { 
        get { return time; }
        protected set
        { 
            int old=time;
            time = value;
            OnPropertyChanged("Time", old, value);
        }
    }
    public double Bid { 
        get { return bid; }
        protected set
        { 
            double old=bid;
            bid = value;
            OnPropertyChanged("Bid", old, value);
        }
    }
    public double Ask { 
        get { return ask; }
        protected set
        { 
            double old=ask;
            ask = value;
            OnPropertyChanged("Ask", old, value);
        }
    }
    public double Point { 
        get { return point; }
        set { 
            double old=point;
            point = value;
            OnPropertyChanged("Point", old, value);
        }
    }
    public int Digits { 
        get { return digits; }
        set { 
            int old=digits;
            digits = value;
            OnPropertyChanged("Digits", old, value);
        }
    }
    public int Spread { 
        get { return spread; }
        protected set
        { 
            int old=spread;
            spread = value;
            OnPropertyChanged("Spread", old, value);
        }
    }
    public int StopLevel { 
        get { return stopLevel; }
        set { 
            int old=stopLevel;
            stopLevel = value;
            OnPropertyChanged("StopLevel", old, value);
        }
    }
    public int LotSize { 
        get { return lotSize; }
        set { 
            int old=lotSize;
            lotSize = value;
            OnPropertyChanged("LotSize", old, value);
        }
    }
    public double TickValue { 
        get { return tickValue; }
        set { 
            double old=tickValue;
            tickValue = value;
            OnPropertyChanged("TickValue", old, value);
        }
    }
    public double TickSize { 
        get { return tickSize; }
        set { 
            double old=tickSize;
            tickSize = value;
            OnPropertyChanged("TickSize", old, value);
        }
    }
    public double SwapLong { 
        get { return swapLong; }
        set { 
            double old=swapLong;
            swapLong = value;
            OnPropertyChanged("SwapLong", old, value);
        }
    }
    public double SwapShort { 
        get { return swapShort; }
        set { 
            double old=swapShort;
            swapShort = value;
            OnPropertyChanged("SwapShort", old, value);
        }
    }
    public int Starting { 
        get { return starting; }
        set { 
            int old=starting;
            starting = value;
            OnPropertyChanged("Starting", old, value);
        }
    }
    public int Expiration { 
        get { return expiration; }
        set { 
            int old=expiration;
            expiration = value;
            OnPropertyChanged("Expiration", old, value);
        }
    }
    public bool TradeAllowed { 
        get { return tradeAllowed; }
        set { 
            bool old=tradeAllowed;
            tradeAllowed = value;
            OnPropertyChanged("TradeAllowed", old, value);
        }
    }
    public double MinLot { 
        get { return minLot; }
        set { 
            double old=minLot;
            minLot = value;
            OnPropertyChanged("MinLot", old, value);
        }
    }
    public double LotStep { 
        get { return lotStep; }
        set { 
            double old=lotStep;
            lotStep = value;
            OnPropertyChanged("LotStep", old, value);
        }
    }
    public double MaxLot { 
        get { return maxLot; }
        set { 
            double old=maxLot;
            maxLot = value;
            OnPropertyChanged("MaxLot", old, value);
        }
    }
    public int SwapType { 
        get { return swapType; }
        set { 
            int old=swapType;
            swapType = value;
            OnPropertyChanged("SwapType", old, value);
        }
    }
    public int ProfitCalcMode { 
        get { return profitCalcMode; }
        set { 
            int old=profitCalcMode;
            profitCalcMode = value;
            OnPropertyChanged("ProfitCalcMode", old, value);
        }
    }
    public int MarginCalcMode { 
        get { return marginCalcMode; }
        set { 
            int old=marginCalcMode;
            marginCalcMode = value;
            OnPropertyChanged("MarginCalcMode", old, value);
        }
    }
    public double MarginInit { 
        get { return marginInit; }
        set { 
            double old=marginInit;
            marginInit = value;
            OnPropertyChanged("MarginInit", old, value);
        }
    }
    public double MarginMaintenance { 
        get { return marginMaintenance; }
        set { 
            double old=marginMaintenance;
            marginMaintenance = value;
            OnPropertyChanged("MarginMaintenance", old, value);
        }
    }
    public double MarginHedged { 
        get { return marginHedged; }
        set { 
            double old=marginHedged;
            marginHedged = value;
            OnPropertyChanged("MarginHedged", old, value);
        }
    }
    public double MarginRequired { 
        get { return marginRequired; }
        set { 
            double old=marginRequired;
            marginRequired = value;
            OnPropertyChanged("MarginRequired", old, value);
        }
    }
    public int FreezeLevel { 
        get { return freezeLevel; }
        set { 
            int old=freezeLevel;
            freezeLevel = value;
            OnPropertyChanged("FreezeLevel", old, value);
        }
    }

    public SerializableDictionary<int, ZsfPeriodConfig> PeriodConfig
    {
        get
        {
            return periodConfig;
        }
        //set
        //	symbolConfig = value;
        //}
    }

    public virtual void Copy(ZsfSymbolConfig other, CopyMode copyMode = CopyMode.ALL)
	{
        if (symbol != other.symbol)
        {
            throw new Exception("ZsfSymbolConfig.Copy  symbol:" + symbol + " doesn't match to other " + other.symbol);
        }

        Copy((VariableConfig)other, copyMode);
        ReplacePeriodConfig(other.periodConfig, copyMode);

        DefaultCurrencyRate = other.defaultCurrencyRate;
        if (copyMode == CopyMode.RUNTIME_ONLY || copyMode == CopyMode.ALL)
        {
            Low = other.low;
            High = other.high;
            Time = other.time;
            Bid = other.bid;
            Ask = other.ask;
            Spread = other.spread;
        }
        Point = other.point;
        Digits = other.digits;
        StopLevel = other.stopLevel;
        LotSize = other.lotSize;
        TickValue = other.tickValue;
        TickSize = other.tickSize;
        SwapLong = other.swapLong;
        SwapShort = other.swapShort;
        Starting = other.starting;
        Expiration = other.expiration;
        TradeAllowed = other.tradeAllowed;
        MinLot = other.minLot;
        LotStep = other.lotStep;
        MaxLot = other.maxLot;
        SwapType = other.swapType;
        ProfitCalcMode = other.profitCalcMode;
        MarginCalcMode = other.marginCalcMode;
        MarginInit = other.marginInit;
        MarginMaintenance = other.marginMaintenance;
        MarginHedged = other.marginHedged;
        MarginRequired = other.marginRequired;
        FreezeLevel = other.freezeLevel;
    }

    public void ClearPeriodConfig()
    {
        ClearXXX(periodConfig, "PeriodConfig");
    }

    public void ReplacePeriodConfig(Dictionary<int, ZsfPeriodConfig> _periodConfig, CopyMode copyMode)
    {
        ReplaceXXX(periodConfig, "PeriodConfig", _periodConfig, copyMode);
    }


    public void SetPeriodConfig(int period, ZsfPeriodConfig _periodConfig, CopyMode copyMode)
    {
        SetXXX(periodConfig, "PeriodConfig", period, _periodConfig, copyMode);
    }

    public void RemovePeriodConfig(int period)
    {
        RemoveXXX(periodConfig, "PeriodConfig", period);
    }
}