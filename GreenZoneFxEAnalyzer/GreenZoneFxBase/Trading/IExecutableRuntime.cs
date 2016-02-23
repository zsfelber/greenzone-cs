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
using System.Reflection;
using System.IO;
using System.Drawing;
using GreenZoneParser.Reflect;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine.Trading
{
    [GreenRmi]
    public interface IUserRuntime : ITradingConst
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEnvironmentRuntime Environment
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISeriesManagerRuntime SeriesManager
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISeriesManagerCache Cache
        {
            get;
            set;
        }

        // !!!! in parent
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IExecSession Session
        {
            get;
            set;
        }

        // !!!! in parent
        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.ExecutableInfo;")]
        Mt4ExecutableInfo ExecutableInfo
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Dictionary<DArr, IArraySeriesManagerCache> TmpArrayCaches
        {
            get;
            set;
        }

        int IndicatorLastOffset
        {
            get;
            set;
        }

        long LastFileOffset
        {
            get;
            set;
        }

        int LastBufferLength
        {
            get;
            set;
        }

        IndicatorUpdateState UpdateState
        {
            get;
            set;
        }

        object[] GenerateParamArray();

        void CopyParamsTo(IExecRuntime other);

        void CopyTopLevelParamsToSession();

        void LoadFromSet(string file);

        void SaveToSet(string file);

        IExecRuntime Copy();
    }

    [GreenRmi(BaseClass = "UserRuntimeEx")]
    public interface IServerUserRuntime : IUserRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSeriesManagerRuntime SeriesManager
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSeriesManagerCache Cache
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Bars;")]
        int Bars
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Bid;")]
        double Bid
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Ask;")]
        double Ask
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Point;")]
        double Point
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Digits;")]
        int Digits
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Open;")]
        DArr Open
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Low;")]
        DArr Low
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.High;")]
        DArr High
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Close;")]
        DArr Close
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.LTime;")]
        LArr LTime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Time;")]
        LArrAsIArr Time
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Volume;")]
        DArr Volume
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Symbol;")]
        symbol Symbol
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Cache.Period;")]
        TimePeriodConst Period
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.AccountServer;")]
        string AccountServer
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.AccountCurrency;")]
        string AccountCurrency
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.AccountNumber;")]
        int AccountNumber
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.AccountFreeMarginMode;")]
        int AccountFreeMarginMode
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.AccountStopoutLevel;")]
        int AccountStopoutLevel
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.AccountStopoutMode;")]
        int AccountStopoutMode
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.IsConnected;")]
        bool IsConnected
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.IsStopped;")]
        bool IsStopped
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return !Environment.EnvironmentType.IsOnline();")]
        bool IsTesting
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.LastError;")]
        int LastError
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.Session.Time;")]
        datetime TimeCurrent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        IOrder SelectedOrder
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Type;")]
        OrderType OrderType
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Symbol;")]
        symbol OrderSymbol
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Ticket;")]
        int OrderTicket
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Lots;")]
        double OrderLots
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.StopLoss;")]
        double OrderStopLoss
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.TakeProfit;")]
        double OrderTakeProfit
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.OpenTime;")]
        datetime OrderOpenTime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.OpenPrice;")]
        double OrderOpenPrice
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.CloseTime;")]
        datetime OrderCloseTime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.ClosePrice;")]
        double OrderClosePrice
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Comment;")]
        string OrderComment
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Expiration;")]
        datetime OrderExpiration
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.MagicNumber;")]
        int OrderMagicNumber
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Commission;")]
        double OrderCommission
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Profit;")]
        double OrderProfit
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SelectedOrder.Swap;")]
        double OrderSwap
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.Orders.Orders.Count;")]
        int OrdersTotal
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Environment.Orders.OrdersHistory.Count;")]
        int OrdersHistoryTotal
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int IndicatorCounted
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.WindowBarsPerChart;")]
        int WindowBarsPerChart
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.WindowFirstVisibleBar;")]
        int WindowFirstVisibleBar
        {
            get;
        }


        double MarketInfo(symbol symbol, MarketInfoConst type);

        double MarketInfo(string _symbol, MarketInfoConst type);

        int GetLastError();

        string symbol();

        int iBars(string _symbol, TimePeriodConst timeframe);

        int iBarShift(string _symbol, TimePeriodConst timeframe, datetime time, bool exact = false);

        double iClose(string _symbol, TimePeriodConst timeframe, int shift);

        double iHigh(string _symbol, TimePeriodConst timeframe, int shift);

        int Highest(string _symbol, TimePeriodConst timeframe, TradingConst.SeriesArrayPool type, int count = TradingConst.WHOLE_ARRAY, int start = 0);

        int iHighest(string _symbol, TimePeriodConst timeframe, TradingConst.SeriesArrayPool type, int count = TradingConst.WHOLE_ARRAY, int start = 0);

        double iLow(string _symbol, TimePeriodConst timeframe, int shift);

        int Lowest(string _symbol, TimePeriodConst timeframe, TradingConst.SeriesArrayPool type, int count = TradingConst.WHOLE_ARRAY, int start = 0);

        int iLowest(string _symbol, TimePeriodConst timeframe, TradingConst.SeriesArrayPool type, int count = TradingConst.WHOLE_ARRAY, int start = 0);

        double iOpen(string _symbol, TimePeriodConst timeframe, int shift);

        datetime iTime(string _symbol, TimePeriodConst timeframe, int shift);

        double iVolume(string _symbol, TimePeriodConst timeframe, int shift);

        int iBars(symbol symbol, TimePeriodConst timeframe);

        int iBarShift(symbol symbol, TimePeriodConst timeframe, datetime time, bool exact = false);

        double iClose(symbol symbol, TimePeriodConst timeframe, int shift);

        double iHigh(symbol symbol, TimePeriodConst timeframe, int shift);

        int iHighest(symbol symbol, TimePeriodConst timeframe, TradingConst.SeriesArrayPool type, int count = TradingConst.WHOLE_ARRAY, int start = 0);

        double iLow(symbol symbol, TimePeriodConst timeframe, int shift);

        int iLowest(symbol symbol, TimePeriodConst timeframe, TradingConst.SeriesArrayPool type, int count = TradingConst.WHOLE_ARRAY, int start = 0);

        double iOpen(symbol symbol, TimePeriodConst timeframe, int shift);

        datetime iTime(symbol symbol, TimePeriodConst timeframe, int shift);

        double iVolume(symbol symbol, TimePeriodConst timeframe, int shift);

        GeneralFile FileOpen(string filename, TradingConst.FileConstraint mode);

        GeneralFile FileOpen(string filename, TradingConst.FileConstraint mode, params string[] delimiters);

        GeneralFile FileOpenHistory(string filename, TradingConst.FileConstraint mode);

        GeneralFile FileOpenHistory(string filename, TradingConst.FileConstraint mode, params string[] delimiters);

        void FileClose(GeneralFile file);

        void FileDelete(string filename);

        void FileFlush(GeneralFile file);

        bool FileIsEnding(GeneralFile file);

        bool FileIsLineEnding(GeneralFile file);

        int FileReadArray<T>(GeneralFile file, T[] array, int start, int count);

        double FileReadDouble(GeneralFile file, FileDoubleType size = TradingConst.DOUBLE_VALUE);

        int FileReadInteger(GeneralFile file, FileIntegerType size = TradingConst.LONG_VALUE);

        double FileReadNumber(GeneralFile file);

        string FileReadString(GeneralFile file, int length = 0);

        bool FileSeek(GeneralFile file, long offset, SeekOrigin origin);

        long FileSize(GeneralFile file);

        long FileTell(GeneralFile file);

        int FileWrite(GeneralFile file, params object[] args);

        int FileWriteArray<T>(GeneralFile file, T[] args, int start, int count);

        int FileWriteDouble(GeneralFile file, double value, FileDoubleType size = TradingConst.DOUBLE_VALUE);
        
        int FileWriteInteger(GeneralFile file, int value, FileIntegerType size = TradingConst.LONG_VALUE);
        
        int FileWriteString(GeneralFile file, string value, int length);
        
        void Sleep(int millis);

        void Alert(string info);
        
        double MathAbs(double d);
        
        int MathAbs(int d);
        
        int MathMax(int n1, int n2);
        
        int MathMin(int n1, int n2);
        
        double MathMax(double n1, double n2);
        
        double MathMin(double n1, double n2);
        
        int MathRand();
        
        double NormalizeDouble(double d, int precision);
        
        string DoubleToStr(double d, int digits);
        
        void MathSrand(int seed);
        
        double MathRound(double d);
        
        double MathPow(double d, double p);
        
        int StringLen(string str);
        
        string StringSubstr(string text, int start, int length = 0);
        
        string StringConcatenate(params object[] args);
        
        int ArraySize(DArr array);
        
        int ArraySize(IArr array);

        int ArraySize<T>(T[] array);
        
        int ArrayInitialize(double[] array, double value);
        
        int ArrayInitialize(double[,] array, double value);
        
        int ArrayInitialize(DArr array, double value);
        
        int ArrayResize<T>(ref T[] array, int new_size);

        datetime StrToTime(string datestr);
        
        string TimeToStr(datetime date, TradingConst.DateTimeFormat mode = TradingConst.TIME_DATE|TradingConst.TIME_MINUTES);
        
        int TimeYear(datetime date);
        
        int TimeMonth(datetime date);
        
        int TimeDay(datetime date);
        
        DayOfWeek TimeDayOfWeek(datetime date);
        
        int TimeDayOfYear(datetime date);
        
        int TimeHour(datetime date);
        
        int TimeMinute(datetime date);
        
        int TimeSecond(datetime date);
        
        int GetTickCount();
        
        void Print(string msg);
        
        void Print(params object[] msgs);
        
        string orderSymbol();
        
        bool OrderSelect(int index, TradingConst.TradeSelectMode select = TradingConst.SELECT_BY_TICKET, TradingConst.TradePool pool = TradingConst.MODE_TRADES);
        
        IOrder OrderSend(string _symbol, OrderType cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expirationDatetime = default(datetime), Color arrow_color = default(Color));

        IOrder OrderSend(symbol symbol, OrderType cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expirationDatetime = default(datetime), Color arrow_color = default(Color));
        
        IOrder OrderClose(int ticket, double lots, double price, int slippage, Color color = default(Color));

        IOrder OrderDelete(int ticket, Color color = default(Color));

        IOrder OrderModify(int ticket, double price, double stoploss, double takeprofit, datetime expiration, Color color = default(Color));

        double GetIndicatorValue<T>(string _symbol, TimePeriodConst timeframe, TradingConst.IndicatorLine _mode, int shift, params object[] args) where T : IIndicatorRuntime;

        double GetIndicatorValue<T>(string _symbol, TimePeriodConst timeframe, int mode, int shift, params object[] args) where T : IIndicatorRuntime;

        double GetIndicatorValue<T>(symbol symbol, TimePeriodConst timeframe, TradingConst.IndicatorLine _mode, int shift, params object[] args) where T : IIndicatorRuntime;

        double GetIndicatorValue<T>(symbol symbol, TimePeriodConst timeframe, int mode, int shift, params object[] args) where T : IIndicatorRuntime;

        double GetIndicatorValue(string _symbol, TimePeriodConst timeframe, string name, TradingConst.IndicatorLine _mode, int shift, params object[] args);

        double GetIndicatorValue(string _symbol, TimePeriodConst timeframe, string name, int mode, int shift, params object[] args);

        double GetIndicatorValue(symbol symbol, TimePeriodConst timeframe, string name, TradingConst.IndicatorLine _mode, int shift, params object[] args);

        double GetIndicatorValue(symbol symbol, TimePeriodConst timeframe, string name, int mode, int shift, params object[] args);

        IArraySeriesManagerCache GetIndicatorCacheOnArray(DArr priceData);

        double iCustom(string _symbol, TimePeriodConst timeframe, string name, int mode, int shift, params object[] args);

        double iCustom(string _symbol, TimePeriodConst timeframe, string name, TradingConst.IndicatorLine mode, int shift, params object[] args);

        double iCustom(symbol symbol, TimePeriodConst timeframe, string name, TradingConst.IndicatorLine mode, int shift, params object[] args);

        double iAC(string _symbol, TimePeriodConst timeframe, int shift);

        double iAC(symbol symbol, TimePeriodConst timeframe, int shift);

        double iAD(string _symbol, TimePeriodConst timeframe, int shift);

        double iAD(symbol symbol, TimePeriodConst timeframe, int shift);

        double iAlligator(string _symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);

        double iAlligator(symbol symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);

        double iADX(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
 
        double iADX(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
        
        double iATR(string _symbol, TimePeriodConst timeframe, int period, int shift);
        
        double iATR(symbol symbol, TimePeriodConst timeframe, int period, int shift);
        
        double iAO(string _symbol, TimePeriodConst timeframe, int shift);
        
        double iAO(symbol symbol, TimePeriodConst timeframe, int shift);
        
        double iBearsPower(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iBearsPower(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iBands(string _symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);

        double iBands(symbol symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
        
        double iBands(string _symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, TradingConst.PriceConstant applied_price, TradingConst.MovingAverageMethod ma_method, TradingConst.IndicatorLine mode, int shift);
        
        double iBands(symbol symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, TradingConst.PriceConstant applied_price, TradingConst.MovingAverageMethod ma_method, TradingConst.IndicatorLine mode, int shift);
        
        double iBandsOnArray(DArr array, int total, int period, int deviation, int bands_shift, TradingConst.IndicatorLine mode, int shift);
        
        double iBandsOnArray(DArr array, int total, int period, int deviation, int bands_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.IndicatorLine mode, int shift);

        double iBullsPower(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iBullsPower(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iCCI(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iCCI(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iCCIOnArray(DArr array, int total, int period, int shift);
        
        double iDeMarker(string _symbol, TimePeriodConst timeframe, int period, int shift);
        
        double iDeMarker(symbol symbol, TimePeriodConst timeframe, int period, int shift);
        
        double iEnvelopes(string _symbol, TimePeriodConst timeframe, int ma_period, TradingConst.MovingAverageMethod ma_method, int ma_shift, TradingConst.PriceConstant applied_price, double deviation, TradingConst.IndicatorLine mode, int shift);
        
        double iEnvelopes(symbol symbol, TimePeriodConst timeframe, int ma_period, TradingConst.MovingAverageMethod ma_method, int ma_shift, TradingConst.PriceConstant applied_price, double deviation, TradingConst.IndicatorLine mode, int shift);
        
        double iEnvelopesOnArray(DArr array, int total, int ma_period, TradingConst.MovingAverageMethod ma_method, int ma_shift, double deviation, TradingConst.IndicatorLine mode, int shift);
        
        double iForce(string _symbol, TimePeriodConst timeframe, int period, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, int shift);
        
        double iForce(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, int shift);
        
        double iFractals(string _symbol, TimePeriodConst timeframe, TradingConst.IndicatorLine mode, int shift);
        
        double iFractals(symbol symbol, TimePeriodConst timeframe, TradingConst.IndicatorLine mode, int shift);

        double iGator(string _symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
        
        double iGator(symbol symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
        
        double iIchimoku(string _symbol, TimePeriodConst timeframe, int tenkan_sen, int kijun_sen, int senkou_span_b, TradingConst.IndicatorLine mode, int shift);
        
        double iIchimoku(symbol symbol, TimePeriodConst timeframe, int tenkan_sen, int kijun_sen, int senkou_span_b, TradingConst.IndicatorLine mode, int shift);
        
        double iBWMFI(string _symbol, TimePeriodConst timeframe, int shift);
        
        double iBWMFI(symbol symbol, TimePeriodConst timeframe, int shift);
        
        double iMomentum(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);

        double iMomentum(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iMomentumOnArray(DArr array, int total, int period, int shift);
        
        double iMFI(string _symbol, TimePeriodConst timeframe, int period, int shift);

        double iMFI(symbol symbol, TimePeriodConst timeframe, int period, int shift);
        
        double iMA(string _symbol, TimePeriodConst timeframe, int period, int ma_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, int shift);
        
        double iMA(symbol symbol, TimePeriodConst timeframe, int period, int ma_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, int shift);
        
        double iPrice(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant type, int shift);
        
        double iPrice(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant type, int shift);
        
        double iMAOnArray(DArr array, int total, int period, int ma_shift, TradingConst.MovingAverageMethod ma_method, int shift);
        
        double iOsMA(string _symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, TradingConst.PriceConstant applied_price, int shift);
        
        double iOsMA(symbol symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, TradingConst.PriceConstant applied_price, int shift);
        
        double iMACD(string _symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
        
        double iMACD(symbol symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, TradingConst.PriceConstant applied_price, TradingConst.IndicatorLine mode, int shift);
        
        double iOBV(string _symbol, TimePeriodConst timeframe, TradingConst.PriceConstant applied_price, int shift);
        
        double iOBV(symbol symbol, TimePeriodConst timeframe, TradingConst.PriceConstant applied_price, int shift);

        double iSAR(string _symbol, TimePeriodConst timeframe, double step, double maximum, int shift);
        
        double iSAR(symbol symbol, TimePeriodConst timeframe, double step, double maximum, int shift);
        
        double iRSI(string _symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iRSI(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.PriceConstant applied_price, int shift);
        
        double iRSIOnArray(DArr array, int total, int period, int shift);
        
        double iRVI(string _symbol, TimePeriodConst timeframe, int period, TradingConst.IndicatorLine mode, int shift);
        
        double iRVI(symbol symbol, TimePeriodConst timeframe, int period, TradingConst.IndicatorLine mode, int shift);
        
        double iStdDev(string _symbol, TimePeriodConst timeframe, int ma_period, int ma_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, int shift);
        
        double iStdDev(symbol symbol, TimePeriodConst timeframe, int ma_period, int ma_shift, TradingConst.MovingAverageMethod ma_method, TradingConst.PriceConstant applied_price, int shift);
        
        double iStdDevOnArray(DArr array, int total, int ma_period, int ma_shift, TradingConst.MovingAverageMethod ma_method, int shift);
        
        double iStochastic(string _symbol, TimePeriodConst timeframe, int _Kperiod, int _Dperiod, int slowing, int method, int price_field, TradingConst.IndicatorLine mode, int shift);
        
        double iStochastic(symbol symbol, TimePeriodConst timeframe, int _Kperiod, int _Dperiod, int slowing, int method, int price_field, TradingConst.IndicatorLine mode, int shift);
        
        double iWPR(string _symbol, TimePeriodConst timeframe, int period, int shift);

        double iWPR(symbol symbol, TimePeriodConst timeframe, int period, int shift);
        
        int WindowFind(string name);
        
        void WindowRedraw();
        
        bool ObjectCreate(string name, TradingConst.GraphObjectType type, int window, datetime time1, double price1, int time2 = 0, double price2 = 0, int time3 = 0, double price3 = 0);

        TradingConst.GraphObjectType ObjectType(string name);
        
        int ObjectFind(string name);
        
        string ObjectName(TradingConst.GraphObjectProperty index);
        
        int ObjectsTotal(int type = TradingConst.EMPTY);

        bool ObjectMove(string name, int point, datetime time1, double price1);

        double ObjectGet(string name, TradingConst.GraphObjectProperty index);

        bool ObjectSet(string name, TradingConst.GraphObjectProperty index, double value);

        bool ObjectSet(string name, TradingConst.GraphObjectProperty index, int value);
        
        bool ObjectSet(string name, TradingConst.GraphObjectProperty index, DrawingStylesWidth1 value);
        
        bool ObjectSet(string name, TradingConst.GraphObjectProperty index, Color value);
        
        bool ObjectSetText(string name, string text, int font_size = 0, string font = null, Color text_color = default(Color));
        
        int ObjectsDeleteAll(int window = TradingConst.EMPTY, TradingConst.GraphObjectType type = TradingConst.GraphObjectType.OBJ_NONE);
        
        bool ObjectDelete(string name);
        
        void ObjectsRedraw();

        bool CompareDouble(double A, double B);

        int i(bool b);

        double nd(double d);

        string ErrorDescription(int e);

        void printError(string err);

        void printLog(string msg, int severity);
    }

    [GreenRmi(BaseClass = "UserRuntimeEx")]
    public interface IClientUserRuntime : IUserRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartRuntime Parent
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "UserRuntimeEx")]
    public interface IExecRuntime : IUserRuntime
    {

    }

    [GreenRmi(BaseClass = "ServerUserRuntimeEx")]
    public interface IServerExecRuntime : IServerUserRuntime, IExecRuntime
    {
    }

    [GreenRmi]
    public interface IClientExecRuntime : IClientUserRuntime, IExecRuntime
    {
    }

    [GreenRmi]
    public interface IScriptRuntime : IExecRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IScriptSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.ExecutableInfo;")]
        Mt4ExecutableInfo ScriptInfo
        {
            get;
        }
        
        int Start();
    }

    [GreenRmi(BaseClass = "ServerExecRuntimeEx")]
    public interface IServerScriptRuntime : IServerExecRuntime, IScriptRuntime
    {
    }

    [GreenRmi]
    public interface IClientScriptRuntime : IClientExecRuntime, IScriptRuntime
    {
    }

    [GreenRmi]
    public interface IExpertRuntime : IExecRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IExpertSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.ExecutableInfo;")]
        Mt4ExecutableInfo ExpertInfo
        {
            get;
        }

        int Init();

        int Deinit();

        int OnTick();

    }

    [GreenRmi(BaseClass = "ServerExecRuntimeEx")]
    public interface IServerExpertRuntime : IServerExecRuntime, IExpertRuntime
    {
    }

    [GreenRmi]
    public interface IClientExpertRuntime : IClientExecRuntime, IExpertRuntime
    {
    }

    [GreenRmi]
    public interface IIndicatorRuntime : IExecRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IIndicatorSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.ExecutableInfo;")]
        Mt4ExecutableInfo IndicatorInfo
        {
            get;
        }

        IndicatorBuffer[] Buffers
        {
            get;
            set;
        }
        
        IndicatorLevel[] Levels
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int NumIndicatorBuffers
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int NumIndicatorLevels
        {
            get;
            set;
        }

        bool Visible
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Buffers.Length > 0 && Buffers[0].Buffer != null && Buffers[0].Buffer.Length != 0 && Buffers[0].SBuffer != null;")]
        bool Initialized
        {
            get;
        }

        IIndicatorRuntime NewClientInstance
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Buffers[index];")]
        IndicatorBuffer this[int index]
        {
            get;
        }

        [GreenRmiMethod(GreenRmiMethodType.Simple, "return Levels[index];")]
        IndicatorLevel GetLevel(int index);

        void RaiseInstanceChanged(IIndicatorRuntime newInstance);
    }


    [GreenRmi(BaseClass = "ServerExecRuntimeEx")]
    public interface IServerIndicatorRuntime : IServerExecRuntime, IIndicatorRuntime
    {

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        IndicatorId Id
        {
            get;
        }

        void CopyParamsTo(IIndicatorRuntime ind2);

        int Init();

        int Deinit();

        int OnTick();

        void IndicatorBuffers(int count);

        void IndicatorShortName(string shortName);

        void IndicatorDigits(int digits);

        void SetIndicatorBuffer(int index, IndicatorBuffer buffer);

        bool SetIndexBuffer(int index, ref DArr array);

        void SetIndexDrawBegin(int index, int begin);

        void SetIndexShift(int index, int shift);

        void SetIndexStyle(int index, DrawingStyle style);

        void SetIndexStyleWidth(int index, DrawingWidth width);

        void SetIndexLabel(int index, string label);

        void SetIndexArrow(int index, IWingdingsChar arrow);

        void SetIndexColor(int index, Color color);

        void SetIndexEmptyValue(int index, double value);

        void SetIndexStyle(int index, DrawingStyle type, DrawingStylesWidth1 style = DrawingStylesWidth1.STYLE_SOLID, DrawingWidth width = DrawingWidth.WIDTH_1, Color clr = default(Color));

        void SetLevelStyle(DrawingStylesWidth1 draw_style, DrawingWidth line_width, Color clr = default(Color), int levelInd = -1, double value = 0);

        void SetLevelColor(Color clr, int levelInd = -1);

        void SetLevelValue(int level, double value);
    }

    [GreenRmi]
    public interface IClientIndicatorRuntime : IClientExecRuntime, IIndicatorRuntime
    {

    }

    [GreenRmi]
    public interface IExecSession : IRmiBase
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        [GreenRmiNonSerial]
        IEnvironmentRuntime Environment
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        string EnvironmentId
        {
            get;
            set;
        }

        Mt4ExecutableInfo ExecutableInfo
        {
            get;
            set;
        }

        Dictionary<string, object> Parameters
        {
            get;
            set;
        }

        //object GetParameter(string name);

        //void SetParameter(string name, object value);

        void SetParameters(IUserRuntime obj, List<ReflProperty> fields);
    }

    [GreenRmi]
    public interface IScriptSession : IExecSession
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return ExecutableInfo;")]
        Mt4ExecutableInfo ScriptInfo
        {
            get;
        }
    }

    [GreenRmi]
    public interface IExpertSession : IExecSession
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return ExecutableInfo;")]
        Mt4ExecutableInfo ExpertInfo
        {
            get;
        }
    }

    [GreenRmi]
    public interface IIndicatorSession : IExecSession
    {
        List<Dictionary<string, object>> Buffers
        {
            get;
            set;
        }

        List<Dictionary<string, object>> Levels
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int NumBuffers
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int NumLevels
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return ExecutableInfo;")]
        Mt4ExecutableInfo IndicatorInfo
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        string ShortName
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IndicatorWindowType WindowType
        {
            get;
            set;
        }

        double IndicatorMinimum
        {
            get;
            set;
        }

        double IndicatorMaximum
        {
            get;
            set;
        }

        int IndicatorDigits
        {
            get;
            set;
        }

        int DisplayScale
        {
            get;
            set;
        }

        bool EnableScroll
        {
            get;
            set;
        }

    }
}
