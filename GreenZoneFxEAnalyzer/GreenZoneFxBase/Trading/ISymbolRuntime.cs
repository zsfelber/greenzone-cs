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
using System.ComponentModel;

namespace GreenZoneFxEngine.Trading
{
    [GreenRmi]
    public interface ISymbolContext : ITradingConst
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEnvironmentRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        symbol Symbol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersTable Orders
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        ISymbolRuntime Runtime
        {
            get;
            set;
        }

        [Description("Low day price. ")]
        [Browsable(false)]
        double Low
        {
            get;
            set;
        }

        [Description("High day price. ")]
        [Browsable(false)]
        double High
        {
            get;
            set;
        }

        [Description("The last incoming tick time (last known server time). ")]
        [Browsable(false)]
        datetime Time
        {
            get;
            set;
        }

        // TODO Update at runtime
        [Description("Last incoming bid price. For the current symbol, it is stored in the predefined variable Bid ")]
        [Browsable(false)]
        double Bid
        {
            get;
            set;
        }

        // TODO Update at runtime
        [Description("Last incoming ask price. For the current symbol, it is stored in the predefined variable Ask ")]
        [Browsable(false)]
        double Ask
        {
            get;
            set;
        }

        [Description("Point size in the quote currency. For the current symbol, it is stored in the predefined variable Point ")]
        double Point
        {
            get;
            set;
        }

        [Description("1 traditional pip in the quote currency. ")]
        double TraditionalPip
        {
            get;
            set;
        }

        [Description("Count of digits after decimal point in the symbol prices. For the current symbol, it is stored in the predefined variable Digits ")]
        int Digits
        {
            get;
            set;
        }

        [Description("Spread value in points. ")]
        int Spread
        {
            get;
            set;
        }

        [Description("Stop level in points. ")]
        int StopLevel
        {
            get;
            set;
        }

        [Description("Lot size in the base currency. ")]
        double LotSize
        {
            get;
            set;
        }

        [Description("Tick value in the deposit currency. ")]
        double TickValue
        {
            get;
            set;
        }

        [Description("Tick size in the quote currency. ")]
        double TickSize
        {
            get;
            set;
        }

        [Description("Swap of the long position. ")]
        double SwapLong
        {
            get;
            set;
        }

        [Description("Swap of the short position. ")]
        double SwapShort
        {
            get;
            set;
        }

        [Description("Market starting date (usually used for futures). ")]
        datetime Starting
        {
            get;
            set;
        }

        [Description("Market expiration date (usually used for futures). ")]
        datetime Expiration
        {
            get;
            set;
        }

        [Description("Trade is allowed for the symbol. ")]
        bool TradeAllowed
        {
            get;
            set;
        }

        [Description("Minimum permitted amount of a lot. ")]
        double MinLot
        {
            get;
            set;
        }

        [Description("Step for changing lots. ")]
        double LotStep
        {
            get;
            set;
        }

        [Description("Maximum permitted amount of a lot. ")]
        double MaxLot
        {
            get;
            set;
        }

        [Description("Swap calculation method. 0 - in points, 1 - in the symbol base currency, 2 - by interest, 3 - in the margin currency. ")]
        SwapCalculationMethod SwapType
        {
            get;
            set;
        }

        [Description("Profit calculation mode. 0 - Forex, 1 - CFD, 2 - Futures. ")]
        ProfitCalculationMode ProfitCalcMode
        {
            get;
            set;
        }

        [Description("Margin calculation mode. 0 - Forex, 1 - CFD, 2 - Futures, 3 - CFD for indices. ")]
        MarginCalculationMode MarginCalcMode
        {
            get;
            set;
        }

        [Description("Initial margin requirements for 1 lot. ")]
        double MarginInit
        {
            get;
            set;
        }

        [Description("Margin to maintain open positions calculated for 1 lot. ")]
        double MarginMaintenance
        {
            get;
            set;
        }

        [Description("Hedged margin calculated for 1 lot. ")]
        double MarginHedged
        {
            get;
            set;
        }

        [Description("Free margin required to open 1 lot for buying. ")]
        double MarginRequired
        {
            get;
            set;
        }

        [Description("Order freeze level in points. If the execution price lies within the range defined by the freeze level, the order cannot be modified, cancelled or closed. ")]
        double FreezeLevel
        {
            get;
            set;
        }

        double GetValue(MarketInfoConst field);
    }

    [GreenRmi(BaseClass = "SymbolContextEx")]
    public interface IServerSymbolContext : ISymbolContext
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSymbolRuntime Runtime
        {
            get;
            set;
        }
        
        void Tick(double Bid, double Ask, double Volume);

        void Bar(TimePeriodConst Period, double Open, double Low, double High, double Close, double Volume, int offset);
    }

    [GreenRmi]
    public interface ISymbolRuntime : ITradingConst
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEnvironmentRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISymbolSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISymbolContext Context
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.Symbol;")]
        symbol Symbol
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersTable Orders
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        List<IHistoryOrderEtc> HistoryOrders
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.EnvironmentType.IsOnline();")]
        bool Online
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        string SymbolFormat
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        TimePeriodConst BestPeriod
        {
            get;
        }

    }

    [GreenRmi(BaseClass = "SymbolRuntimeEx")]
    public interface IServerSymbolRuntime : ISymbolRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSymbolContext Context
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        TimePeriodConst BestCursorPeriod
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrdersTable Orders
        {
            get;
            set;
        }

        ITimeSeriesRuntime LoadSeries(TimePeriodConst period, datetime focusedTime);

        void LoadOrders();

        void SaveOrders();

        void Tick(double Bid, double Ask, double Volume);
    }

    [GreenRmi]
    public interface ISymbolSession : ITradingConst
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        symbol Symbol
        {
            get;
            set;
        }

        [Category("EA testing")]
        datetime EATestingGlobalFrom
        {
            get;
            set;
        }

        [Category("EA testing")]
        datetime EATestingGlobalTo
        {
            get;
            set;
        }

        [Category("EA testing")]
        TestType TestType
        {
            get;
            set;
        }

        [Category("EA testing")]
        TimePeriodConst DataPeriod
        {
            get;
            set;
        }
    }
}
