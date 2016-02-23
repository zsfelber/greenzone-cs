using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Drawing;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{

    public enum OrderTypeKind
    {
        DIRECT_TRADE,
        PENDING_TRADE,
        OPERATION
    }

    // Order types
    public enum OrderType
    {
        OP_BUY = 0, // Buying position. 
        OP_SELL = 1, // Selling position. 
        OP_BUYLIMIT = 2, // Buy limit pending position. 
        OP_SELLLIMIT = 3, // Sell limit pending position. 
        OP_BUYSTOP = 4, // Buy stop pending position. 
        OP_SELLSTOP = 5, // Sell stop pending position. 
    }

    // Trade operations 
    public enum TradeOperation
    {
        OP_NONE = 0,
        OP_MODIFY,
        OP_OPEN,
        OP_PENDING_OPENED,
        OP_CLOSE,
        OP_CLOSE_BY_TP,
        OP_CLOSE_BY_SL,
        OP_DELETE_PENDING
    }

    public enum MarketInfoConst
    {
        MODE_LOW = 1, // Low day price. 
        MODE_HIGH = 2, // High day price. 
        MODE_TIME = 5, // The last incoming tick time (last known server time). 
        MODE_BID = 9, // Last incoming bid price. For the current symbol, it is stored in the predefined variable Bid 
        MODE_ASK = 10, // Last incoming ask price. For the current symbol, it is stored in the predefined variable Ask 
        MODE_POINT = 11, // Point size in the quote currency. For the current symbol, it is stored in the predefined variable Point 
        MODE_DIGITS = 12, // Count of digits after decimal point in the symbol prices. For the current symbol, it is stored in the predefined variable Digits 
        MODE_SPREAD = 13, // Spread value in points. 
        MODE_STOPLEVEL = 14, // Stop level in points. 
        MODE_LOTSIZE = 15, // Lot size in the base currency. 
        MODE_TICKVALUE = 16, // Tick value in the deposit currency. 
        MODE_TICKSIZE = 17, // Tick size in the quote currency. 
        MODE_SWAPLONG = 18, // Swap of the long position. 
        MODE_SWAPSHORT = 19, // Swap of the short position. 
        MODE_STARTING = 20, // Market starting date (usually used for futures). 
        MODE_EXPIRATION = 21, // Market expiration date (usually used for futures). 
        MODE_TRADEALLOWED = 22, // Trade is allowed for the symbol. 
        MODE_MINLOT = 23, // Minimum permitted amount of a lot. 
        MODE_LOTSTEP = 24, // Step for changing lots. 
        MODE_MAXLOT = 25, // Maximum permitted amount of a lot. 
        MODE_SWAPTYPE = 26, // Swap calculation method. 0 - in points, 1 - in the symbol base currency, 2 - by interest, 3 - in the margin currency. 
        MODE_PROFITCALCMODE = 27, // Profit calculation mode. 0 - Forex, 1 - CFD, 2 - Futures. 
        MODE_MARGINCALCMODE = 28, // Margin calculation mode. 0 - Forex, 1 - CFD, 2 - Futures, 3 - CFD for indices. 
        MODE_MARGININIT = 29, // Initial margin requirements for 1 lot. 
        MODE_MARGINMAINTENANCE = 30, // Margin to maintain open positions calculated for 1 lot. 
        MODE_MARGINHEDGED = 31, // Hedged margin calculated for 1 lot. 
        MODE_MARGINREQUIRED = 32, // Free margin required to open 1 lot for buying. 
        MODE_FREEZELEVEL = 33, // Order freeze level in points. If the execution price lies within the range defined by the freeze level, the order cannot be modified, cancelled or closed. 
    };

    public enum EnvironmentType
    {
        METATRADER4_OFFLINE, METATRADER5_OFFLINE, DUKASCOPY_TICKDATA_OFFLINE, METATRADER4_ONLINE, 
    }

    public enum Mt4ExecutableType
    {
        SCRIPT, EA, INDICATOR
    }

    public enum ExecutableLoadType
    {
        HARD_CODED, LOAD_FROM_ENVIRONMENT
    }

    public enum TestType
    {
        EVERY_TICK, CONTROL_POINTS, OPEN_PRICES, NOT_IN_TEST
    }

    public enum PorgressSnapMode
    {
        MANUAL=0, SNAP_TO_PROGRESS, STOP_AT_TICK
    }

    public enum StartStatus
    {
        NOT_RUNNING, STARTED, PAUSED, STOPPED, FINISHED, ABORTED
    }

    public enum SwapCalculationMethod
    {
        POINTS,SYMBOL_BASE_CURRENCY,INTEREST,MARGIN_CURRENCY
    }

    public enum ProfitCalculationMode
    {
        FOREX, CFD, FUTURES
    }

    public enum MarginCalculationMode
    {
        FOREX, CFD, FUTURES, CFD_FOR_INDICES
    }

    public enum SlippageMode
    {
        NO_SLIPPAGE, RANDOM_SLIPPAGE, SLIPPAGE_BY_TICK
    }

    public enum IndicatorUpdateState
    {
        NORMAL=0, UPDATING, PENDING_UPDATE
    }

    public static class EnumExtensions
    {
        public static TimePeriodCategory VISIBLE_PERIODS = TimePeriodCategory.MT4 | TimePeriodCategory.MT5 | TimePeriodCategory.SECS | TimePeriodCategory.TICKS;


        public static int GetSecs(this TimePeriodConst period)
        {
            int secs;
            switch (period.GetCategory())
            {
                case TimePeriodCategory.SECS:
                    secs = -(int)period - 100;
                    break;
                case TimePeriodCategory.MT4:
                case TimePeriodCategory.MT5:
                    secs = (int)period * 60;
                    break;
                case TimePeriodCategory.TICKS:
                    secs = 0;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return secs;
        }

        public static TimePeriodConst[] GetPeriods(TimePeriodCategory category)
        {
            List<TimePeriodConst> list = new List<TimePeriodConst>();
            if ((category & TimePeriodCategory.SYSTEM) != 0)
            {
                list.Add(TimePeriodConst.PERIOD_CURRENT);
            }
            if ((category & TimePeriodCategory.TICKS) != 0)
            {
                list.Add(TimePeriodConst.PERIOD_TICK);
                list.Add(TimePeriodConst.PERIOD_TICK_ASK);
            }
            TimePeriodConst[] vs = (TimePeriodConst[])Enum.GetValues(typeof(TimePeriodConst));
            List<TimePeriodConst> rev=new List<TimePeriodConst>(vs);
            rev.Reverse();
            foreach (TimePeriodConst p in rev)
            {
                if ((int)p<-2 && (category & p.GetCategory()) != 0)
                {
                    list.Add(p);
                }
            }
            foreach (TimePeriodConst p in Enum.GetValues(typeof(TimePeriodConst)))
            {
                if (p>0 && (category & p.GetCategory()) != 0)
                {
                    list.Add(p);
                }
            }

            var result = list.ToArray();
            return result;
        }

        public static string GetShortTxt(this ChartType chartType)
        {
            switch (chartType)
            {
                case ChartType.CANDLESTICKS: return "Candle";
                case ChartType.LINE: return "Line";
                case ChartType.OHLC: return "OHLC";
                default: return "?";
            }
        }

        public static string GetLongTxt(this ChartType chartType)
        {
            switch (chartType)
            {
                case ChartType.CANDLESTICKS: return "Candlesticks";
                case ChartType.LINE: return "Line Chart";
                case ChartType.OHLC: return "Open-High-Low-Close";
                default: return "?";
            }
        }

        public static string GetShortTxt(this TimePeriodConst chartType)
        {
            //.GetCategory() == TimePeriodCategory.TICKS
            switch (chartType)
            {
                case TimePeriodConst.PERIOD_CURRENT: return "current";
                case TimePeriodConst.PERIOD_TICK: return "T";
                case TimePeriodConst.PERIOD_M1: return "M1";
                case TimePeriodConst.PERIOD_M5: return "M5";
                case TimePeriodConst.PERIOD_M15: return "M15";
                case TimePeriodConst.PERIOD_M30: return "M30";
                case TimePeriodConst.PERIOD_H1: return "H1";
                case TimePeriodConst.PERIOD_H4: return "H4";
                case TimePeriodConst.PERIOD_D1: return "D1";
                case TimePeriodConst.PERIOD_W1: return "W1";
                case TimePeriodConst.PERIOD_MN1: return "MN1";


                // ticks
                case TimePeriodConst.PERIOD_TICK_ASK: return "TA";
                // MT5 standard
                case TimePeriodConst.PERIOD_M2: return "M2";
                case TimePeriodConst.PERIOD_M3: return "M3";
                case TimePeriodConst.PERIOD_M4: return "M4";
                case TimePeriodConst.PERIOD_M6: return "M6";
                case TimePeriodConst.PERIOD_M10: return "M10";
                case TimePeriodConst.PERIOD_M12: return "M12";
                case TimePeriodConst.PERIOD_M20: return "M20";
                case TimePeriodConst.PERIOD_H2: return "H2";
                case TimePeriodConst.PERIOD_H3: return "H3";
                case TimePeriodConst.PERIOD_H6: return "H6";
                case TimePeriodConst.PERIOD_H8: return "H8";
                case TimePeriodConst.PERIOD_H12: return "H12";
                // secs
                case TimePeriodConst.PERIOD_S1: return "S1";
                case TimePeriodConst.PERIOD_S2: return "S2";
                case TimePeriodConst.PERIOD_S3: return "S3";
                case TimePeriodConst.PERIOD_S4: return "S4";
                case TimePeriodConst.PERIOD_S5: return "S5";
                case TimePeriodConst.PERIOD_S6: return "S6";
                case TimePeriodConst.PERIOD_S10: return "S10";
                case TimePeriodConst.PERIOD_S12: return "S12";
                case TimePeriodConst.PERIOD_S15: return "S15";
                case TimePeriodConst.PERIOD_S20: return "S20";
                case TimePeriodConst.PERIOD_S25: return "S25";
                case TimePeriodConst.PERIOD_S30: return "S30";
                case TimePeriodConst.PERIOD_S35: return "S35";
                case TimePeriodConst.PERIOD_S40: return "S40";
                case TimePeriodConst.PERIOD_S45: return "S45";
                case TimePeriodConst.PERIOD_S50: return "S50";
                case TimePeriodConst.PERIOD_S55: return "S55";

                default: throw new NotSupportedException();
            }
        }

        public static string GetShortTxt(this TradeOperation op)
        {
            switch (op)
            {
                case TradeOperation.OP_NONE: return "";
                case TradeOperation.OP_OPEN: return "Open";
                case TradeOperation.OP_PENDING_OPENED: return "Pending";
                case TradeOperation.OP_CLOSE: return "Close";
                case TradeOperation.OP_CLOSE_BY_SL: return "SL";
                case TradeOperation.OP_CLOSE_BY_TP: return "TP";
                case TradeOperation.OP_DELETE_PENDING: return "Delete";
                case TradeOperation.OP_MODIFY: return "Modify";

                default: throw new NotSupportedException();
            }
        }
            
        public static int GetOrdinal(this TimePeriodConst period)
        {
            switch (period)
            {
                case TimePeriodConst.PERIOD_TICK: return -1;
                case TimePeriodConst.PERIOD_TICK_ASK: return -2;
                case TimePeriodConst.PERIOD_CURRENT: return -3;
                case TimePeriodConst.PERIOD_S1: return 0;
                case TimePeriodConst.PERIOD_S2: return 1;
                case TimePeriodConst.PERIOD_S3: return 2;
                case TimePeriodConst.PERIOD_S4: return 3;
                case TimePeriodConst.PERIOD_S5: return 4;
                case TimePeriodConst.PERIOD_S6: return 5;
                case TimePeriodConst.PERIOD_S10: return 6;
                case TimePeriodConst.PERIOD_S12: return 7;
                case TimePeriodConst.PERIOD_S15: return 8;
                case TimePeriodConst.PERIOD_S20: return 9;
                case TimePeriodConst.PERIOD_S25: return 10;
                case TimePeriodConst.PERIOD_S30: return 11;
                case TimePeriodConst.PERIOD_S35: return 12;
                case TimePeriodConst.PERIOD_S40: return 13;
                case TimePeriodConst.PERIOD_S45: return 14;
                case TimePeriodConst.PERIOD_S50: return 15;
                case TimePeriodConst.PERIOD_S55: return 16;
                case TimePeriodConst.PERIOD_M1: return 17;
                case TimePeriodConst.PERIOD_M2: return 18;
                case TimePeriodConst.PERIOD_M3: return 19;
                case TimePeriodConst.PERIOD_M4: return 20;
                case TimePeriodConst.PERIOD_M5: return 21;
                case TimePeriodConst.PERIOD_M6: return 22;
                case TimePeriodConst.PERIOD_M10: return 23;
                case TimePeriodConst.PERIOD_M12: return 24;
                case TimePeriodConst.PERIOD_M15: return 25;
                case TimePeriodConst.PERIOD_M20: return 26;
                case TimePeriodConst.PERIOD_M30: return 27;
                case TimePeriodConst.PERIOD_H1: return 28;
                case TimePeriodConst.PERIOD_H2: return 29;
                case TimePeriodConst.PERIOD_H3: return 30;
                case TimePeriodConst.PERIOD_H4: return 31;
                case TimePeriodConst.PERIOD_H6: return 32;
                case TimePeriodConst.PERIOD_H8: return 33;
                case TimePeriodConst.PERIOD_H12: return 34;
                case TimePeriodConst.PERIOD_D1: return 35;
                case TimePeriodConst.PERIOD_W1: return 36;
                case TimePeriodConst.PERIOD_MN1: return 37;
                default: throw new NotSupportedException(""+period);
            }
        }

        public static int GetOrdinal(this TestType testType)
        {
            switch (testType)
            {
                case TestType.EVERY_TICK: return 0;
                case TestType.CONTROL_POINTS: return 1;
                case TestType.OPEN_PRICES: return 2;
                default: return -1;
            }
        }

        public static string GetLongTxt(this TimePeriodConst period)
        {
            switch (period)
            {
                case TimePeriodConst.PERIOD_CURRENT: return "Current period";
                case TimePeriodConst.PERIOD_TICK: return "Tick";
                case TimePeriodConst.PERIOD_M1: return "1 Minute";
                case TimePeriodConst.PERIOD_M5: return "5 Minute";
                case TimePeriodConst.PERIOD_M15: return "15 Minute";
                case TimePeriodConst.PERIOD_M30: return "30 Minute";
                case TimePeriodConst.PERIOD_H1: return "1 Hour";
                case TimePeriodConst.PERIOD_H4: return "4 Hour";
                case TimePeriodConst.PERIOD_D1: return "1 Day";
                case TimePeriodConst.PERIOD_W1: return "1 Week";
                case TimePeriodConst.PERIOD_MN1: return "1 Month";
                // ticks
                case TimePeriodConst.PERIOD_TICK_ASK: return "Tick/Ask";
                // MT5 standard
                case TimePeriodConst.PERIOD_M2: return "2 Minute";
                case TimePeriodConst.PERIOD_M3: return "3 Minute";
                case TimePeriodConst.PERIOD_M4: return "4 Minute";
                case TimePeriodConst.PERIOD_M6: return "6 Minute";
                case TimePeriodConst.PERIOD_M10: return "10 Minute";
                case TimePeriodConst.PERIOD_M12: return "12 Minute";
                case TimePeriodConst.PERIOD_M20: return "20 Minute";
                case TimePeriodConst.PERIOD_H2: return "2 Hour";
                case TimePeriodConst.PERIOD_H3: return "3 Hour";
                case TimePeriodConst.PERIOD_H6: return "6 Hour";
                case TimePeriodConst.PERIOD_H8: return "8 Hour";
                case TimePeriodConst.PERIOD_H12: return "12 Hour";
                // secs
                case TimePeriodConst.PERIOD_S1: return "1 Second";
                case TimePeriodConst.PERIOD_S2: return "2 Second";
                case TimePeriodConst.PERIOD_S3: return "3 Second";
                case TimePeriodConst.PERIOD_S4: return "4 Second";
                case TimePeriodConst.PERIOD_S5: return "5 Second";
                case TimePeriodConst.PERIOD_S6: return "6 Second";
                case TimePeriodConst.PERIOD_S10: return "10 Second";
                case TimePeriodConst.PERIOD_S12: return "12 Second";
                case TimePeriodConst.PERIOD_S15: return "15 Second";
                case TimePeriodConst.PERIOD_S20: return "20 Second";
                case TimePeriodConst.PERIOD_S25: return "25 Second";
                case TimePeriodConst.PERIOD_S30: return "30 Second";
                case TimePeriodConst.PERIOD_S35: return "35 Second";
                case TimePeriodConst.PERIOD_S40: return "40 Second";
                case TimePeriodConst.PERIOD_S45: return "45 Second";
                case TimePeriodConst.PERIOD_S50: return "50 Second";
                case TimePeriodConst.PERIOD_S55: return "55 Second";

                default: throw new NotSupportedException("" + period);
            }
        }

        public static string GetShortText(this TestType testType)
        {
            switch (testType)
            {
                case TestType.NOT_IN_TEST: return "Not in test";
                case TestType.EVERY_TICK: return "Every tick";
                case TestType.CONTROL_POINTS: return "Control points";
                case TestType.OPEN_PRICES: return "Open prices";
                default: return "?";
            }
        }

        public static string GetDescription(this TestType testType)
        {
            switch (testType)
            {
                case TestType.NOT_IN_TEST: return "Not in test";
                case TestType.EVERY_TICK: return "the most precise method based on tick data (generating one if it is not available)";
                case TestType.CONTROL_POINTS: return "a very crude methods based on open-low-high-close prices";
                case TestType.OPEN_PRICES: return "quick method on completed bars, only for Expert Advisors that explicitly control bar opening";
                default: return "?";
            }
        }

        public static bool IsOnline(this EnvironmentType et)
        {
            return et == EnvironmentType.METATRADER4_ONLINE;
        }

        public static OrderTypeKind Kind(this OrderType op)
        {
            switch (op)
            {
                case OrderType.OP_BUY:
                case OrderType.OP_SELL:
                    return OrderTypeKind.DIRECT_TRADE;
                case OrderType.OP_BUYLIMIT:
                case OrderType.OP_BUYSTOP:
                case OrderType.OP_SELLLIMIT:
                case OrderType.OP_SELLSTOP:
                    return OrderTypeKind.PENDING_TRADE;
                default:
                    return OrderTypeKind.OPERATION;
            }
        }

        public static bool IsRunning(this StartStatus status)
        {
            switch (status)
            {
                case StartStatus.ABORTED:
                case StartStatus.FINISHED:
                case StartStatus.NOT_RUNNING:
                case StartStatus.STOPPED:
                    return false;
                case StartStatus.PAUSED:
                case StartStatus.STARTED:
                    return true;
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public interface ITradingConst : IRmiBase
    {
    }

    [Serializable]
    public class TradingConst : RmiBase, ITradingConst
    {
        public TradingConst(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public TradingConst(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected TradingConst(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        // TODO change each from int to special enum

        // Series arrays 
        public enum SeriesArrayPool
        {
            MODE_OPEN = 0, // Open price. 
            MODE_LOW = 1, // Low price. 
            MODE_HIGH = 2, // High price. 
            MODE_CLOSE = 3, // Close price. 
            MODE_VOLUME = 4, // Volume, used in iLowest() and iHighest() functions. 
            MODE_TIME = 5 // Bar open time, used in ArrayCopySeries() function. 
        }
        public const SeriesArrayPool MODE_OPEN = (SeriesArrayPool)0; // Open price. 
        public const SeriesArrayPool MODE_LOW = (SeriesArrayPool)1; // Low price. 
        public const SeriesArrayPool MODE_HIGH = (SeriesArrayPool)2; // High price. 
        public const SeriesArrayPool MODE_CLOSE = (SeriesArrayPool)3; // Close price. 
        public const SeriesArrayPool MODE_VOLUME = (SeriesArrayPool)4; // Volume, used in iLowest() and iHighest() functions. 
        public const SeriesArrayPool MODE_TIME = (SeriesArrayPool)5; // Bar open time, used in ArrayCopySeries() function. 

        // Timeframes 
        public const TimePeriodConst PERIOD_TICK = TimePeriodConst.PERIOD_TICK; // 1 tick. 
        public const TimePeriodConst PERIOD_M1 = TimePeriodConst.PERIOD_M1; // 1 minute. 
        public const TimePeriodConst PERIOD_M5 = TimePeriodConst.PERIOD_M5; // 5 minutes. 
        public const TimePeriodConst PERIOD_M15 = TimePeriodConst.PERIOD_M15; // 15 minutes. 
        public const TimePeriodConst PERIOD_M30 = TimePeriodConst.PERIOD_M30; // 30 minutes. 
        public const TimePeriodConst PERIOD_H1 = TimePeriodConst.PERIOD_H1; // 1 hour. 
        public const TimePeriodConst PERIOD_H4 = TimePeriodConst.PERIOD_H4; // 4 hour. 
        public const TimePeriodConst PERIOD_D1 = TimePeriodConst.PERIOD_D1; // Daily. 
        public const TimePeriodConst PERIOD_W1 = TimePeriodConst.PERIOD_W1; // Weekly. 
        public const TimePeriodConst PERIOD_MN1 = TimePeriodConst.PERIOD_MN1; // Monthly. 
        public const TimePeriodConst PERIOD_CURRENT = TimePeriodConst.PERIOD_CURRENT; // Timeframe used on the chart. 

        public const FileIntegerType CHAR_VALUE = (FileIntegerType)1;
        public const FileIntegerType SHORT_VALUE = (FileIntegerType)2;
        public const FileIntegerType LONG_VALUE = (FileIntegerType)4;

        public const FileDoubleType FLOAT_VALUE = (FileDoubleType)4;
        public const FileDoubleType DOUBLE_VALUE = (FileDoubleType)8;

        public enum FileConstraint
        {
            FILE_READ = 1,
            FILE_WRITE = 2,
            FILE_BIN = 4,
            FILE_CSV = 8
        }
        public const FileConstraint FILE_READ = (FileConstraint)1;
        public const FileConstraint FILE_WRITE = (FileConstraint)2;
        public const FileConstraint FILE_BIN = (FileConstraint)4;
        public const FileConstraint FILE_CSV = (FileConstraint)8;

        public enum DateTimeFormat
        {
            TIME_DATE = 1,
            TIME_MINUTES = 2
        }
        public const DateTimeFormat TIME_DATE = (DateTimeFormat)1;
        public const DateTimeFormat TIME_MINUTES = (DateTimeFormat)2;

        public enum TradeSelectMode
        {
            SELECT_BY_POS = 0,
            SELECT_BY_TICKET = 1
        }
        public const TradeSelectMode SELECT_BY_POS = (TradeSelectMode)0;
        public const TradeSelectMode SELECT_BY_TICKET = (TradeSelectMode)1;

        public enum TradePool
        {
            MODE_TRADES = 0,
            MODE_HISTORY = 1,
            MODE_OPERATIONS = 2,
        }
        public const TradePool MODE_TRADES = (TradePool)0;
        public const TradePool MODE_HISTORY = (TradePool)1;

        public const OrderType OP_BUY = (OrderType)0; // Buying position. 
        public const OrderType OP_SELL = (OrderType)1; // Selling position. 
        public const OrderType OP_BUYLIMIT = (OrderType)2; // Buy limit pending position. 
        public const OrderType OP_SELLLIMIT = (OrderType)3; // Sell limit pending position. 
        public const OrderType OP_BUYSTOP = (OrderType)4; // Buy stop pending position. 
        public const OrderType OP_SELLSTOP = (OrderType)5; // Sell stop pending position. 

        // Price constants
        public enum PriceConstant {
            PRICE_CLOSE = 0, // Close price. 
            PRICE_OPEN = 1, // Open price. 
            PRICE_HIGH = 2, // High price. 
            PRICE_LOW = 3, // Low price. 
            PRICE_MEDIAN = 4, // Median price, (high+low)/2. 
            PRICE_TYPICAL = 5, // Typical price, (high+low+close)/3. 
            PRICE_WEIGHTED = 6 // Weighted close price, (high+low+close+close)/4. 
        }
        public const PriceConstant PRICE_CLOSE = (PriceConstant)0; // Close price. 
        public const PriceConstant PRICE_OPEN = (PriceConstant)1; // Open price. 
        public const PriceConstant PRICE_HIGH = (PriceConstant)2; // High price. 
        public const PriceConstant PRICE_LOW = (PriceConstant)3; // Low price. 
        public const PriceConstant PRICE_MEDIAN = (PriceConstant)4; // Median price, (high+low)/2. 
        public const PriceConstant PRICE_TYPICAL = (PriceConstant)5; // Typical price, (high+low+close)/3. 
        public const PriceConstant PRICE_WEIGHTED = (PriceConstant)6; // Weighted close price, (high+low+close+close)/4. 

        public const DrawingStyle DRAW_LINE = (DrawingStyle)0; // Drawing line. 
        public const DrawingStyle DRAW_SECTION = (DrawingStyle)1; // Drawing sections. 
        public const DrawingStyle DRAW_HISTOGRAM = (DrawingStyle)2; // Drawing histogram. 
        public const DrawingStyle DRAW_ARROW = (DrawingStyle)3; // Drawing arrows (symbols). 
        public const DrawingStyle DRAW_ZIGZAG = (DrawingStyle)4; // Drawing sections between even and odd indicator buffers. 
        public const DrawingStyle DRAW_NONE = (DrawingStyle)12; // No drawing. 

        public const DrawingWidth WIDTH_1 = DrawingWidth.WIDTH_1;
        public const DrawingWidth WIDTH_2 = DrawingWidth.WIDTH_2;
        public const DrawingWidth WIDTH_3 = DrawingWidth.WIDTH_3;
        public const DrawingWidth WIDTH_4 = DrawingWidth.WIDTH_4;
        public const DrawingWidth WIDTH_5 = DrawingWidth.WIDTH_5;


        public const DrawingStylesWidth1 STYLE_SOLID = (DrawingStylesWidth1)0; // The pen is solid. 
        public const DrawingStylesWidth1 STYLE_DASH = (DrawingStylesWidth1)1; // The pen is dashed. 
        public const DrawingStylesWidth1 STYLE_DOT = (DrawingStylesWidth1)2; // The pen is dotted. 
        public const DrawingStylesWidth1 STYLE_DASHDOT = (DrawingStylesWidth1)3; // The pen has alternating dashes and dots. 
        public const DrawingStylesWidth1 STYLE_DASHDOTDOT = (DrawingStylesWidth1)4; // The pen has alternating dashes and double dots. 

        // Arrow codes
        public enum ArrowCode {
            SYMBOL_THUMBSUP = 67, // Thumb up symbol (C). 
            SYMBOL_THUMBSDOWN = 68, // Thumb down symbol (D). 
            SYMBOL_ARROWUP = 241, // Arrow up symbol (ñ). 
            SYMBOL_ARROWDOWN = 242, // Arrow down symbol (ò). 
            SYMBOL_STOPSIGN = 251, // Stop sign symbol (û). 
            SYMBOL_CHECKSIGN = 252 // Check sign symbol (ü). 
        }
        public const int SYMBOL_THUMBSUP = 67; // Thumb up symbol (C). 
        public const int SYMBOL_THUMBSDOWN = 68; // Thumb down symbol (D). 
        public const int SYMBOL_ARROWUP = 241; // Arrow up symbol (ñ). 
        public const int SYMBOL_ARROWDOWN = 242; // Arrow down symbol (ò). 
        public const int SYMBOL_STOPSIGN = 251; // Stop sign symbol (û). 
        public const int SYMBOL_CHECKSIGN = 252; // Check sign symbol (ü). 

        // ...Arrow codes (Special)
        public enum ArrowCodeSpecial {
            SYMBOL_UP = 1, // Upwards arrow with tip rightwards (↱). 
            SYMBOL_DOWN = 2, // Downwards arrow with tip rightwards (↳). 
            SYMBOL_LEFT = 3, // Left pointing triangle (◄). 
            SYMBOL_DASH = 4, // En Dash symbol (–). 
            SYMBOL_LEFTPRICE = 5, // Left sided price label. 
            SYMBOL_RIGHTPRICE = 6 // Right sided price label. 
        }
        public const int SYMBOL_UP = 1; // Upwards arrow with tip rightwards (↱). 
        public const int SYMBOL_DOWN = 2; // Downwards arrow with tip rightwards (↳). 
        public const int SYMBOL_LEFT = 3; // Left pointing triangle (◄). 
        public const int SYMBOL_DASH = 4; // En Dash symbol (–). 
        public const int SYMBOL_LEFTPRICE = 5; // Left sided price label. 
        public const int SYMBOL_RIGHTPRICE = 6; // Right sided price label. 

        // Indicator lines
        public enum IndicatorLine {
            MODE_MAIN = 0, // Base indicator line. 
            MODE_SIGNAL = 1, // Signal line. 
            MODE_PLUSDI = 1, // +DI indicator line. 
            MODE_MINUSDI = 2, // -DI indicator line. 
            MODE_UPPER = 1, // Upper line. 
            MODE_LOWER = 2 // Lower line. 
        }
        public const IndicatorLine MODE_MAIN = (IndicatorLine)0; // Base indicator line. 
        public const IndicatorLine MODE_SIGNAL = (IndicatorLine)1; // Signal line. 

        //public const int MODE_MAIN = 0; // Base indicator line. 
        public const IndicatorLine MODE_PLUSDI = (IndicatorLine)1; // +DI indicator line. 
        public const IndicatorLine MODE_MINUSDI = (IndicatorLine)2; // -DI indicator line. 

        public const IndicatorLine MODE_UPPER = (IndicatorLine)1; // Upper line. 
        public const IndicatorLine MODE_LOWER = (IndicatorLine)2; // Lower line. 

        // (Ichimoku Kinko Hyo )
        public enum IchimokuKinkoHyo {
            MODE_TENKANSEN = 1, // Tenkan-sen. 
            MODE_KIJUNSEN = 2, // Kijun-sen. 
            MODE_SENKOUSPANA = 3, // Senkou Span A. 
            MODE_SENKOUSPANB = 4, // Senkou Span B. 
            MODE_CHINKOUSPAN = 5 // Chinkou Span. 
        }
        public const int MODE_TENKANSEN = 1; // Tenkan-sen. 
        public const int MODE_KIJUNSEN = 2; // Kijun-sen. 
        public const int MODE_SENKOUSPANA = 3; // Senkou Span A. 
        public const int MODE_SENKOUSPANB = 4; // Senkou Span B. 
        public const int MODE_CHINKOUSPAN = 5; // Chinkou Span. 

        // Moving Average methods
        public enum MovingAverageMethod {
            MODE_SMA = 0, // Simple moving average, 
            MODE_EMA = 1, // Exponential moving average, 
            MODE_SMMA = 2, // Smoothed moving average, 
            MODE_LWMA = 3 // Linear weighted moving average. 
        }
        public const MovingAverageMethod MODE_SMA = (MovingAverageMethod)0; // Simple moving average, 
        public const MovingAverageMethod MODE_EMA = (MovingAverageMethod)1; // Exponential moving average, 
        public const MovingAverageMethod MODE_SMMA = (MovingAverageMethod)2; // Smoothed moving average, 
        public const MovingAverageMethod MODE_LWMA = (MovingAverageMethod)3; // Linear weighted moving average. 

        // Object types
        public enum GraphObjectType {
            OBJ_NONE = -1,
            OBJ_VLINE = 0, // Vertical line. Uses time part of first coordinate. 
            OBJ_HLINE = 1, // Horizontal line. Uses price part of first coordinate. 
            OBJ_TREND = 2, // Trend line. Uses 2 coordinates. 
            OBJ_TRENDBYANGLE = 3, // Trend by angle. Uses 1 coordinate. To set angle of line use ObjectSet() function. 
            OBJ_REGRESSION = 4, // Regression. Uses time parts of first two coordinates. 
            OBJ_CHANNEL = 5, // Channel. Uses 3 coordinates. 
            OBJ_STDDEVCHANNEL = 6, // Standard deviation channel. Uses time parts of first two coordinates. 
            OBJ_GANNLINE = 7, // Gann line. Uses 2 coordinate, but price part of second coordinate ignored. 
            OBJ_GANNFAN = 8, // Gann fan. Uses 2 coordinate, but price part of second coordinate ignored. 
            OBJ_GANNGRID = 9, // Gann grid. Uses 2 coordinate, but price part of second coordinate ignored. 
            OBJ_FIBO = 10, // Fibonacci retracement. Uses 2 coordinates. 
            OBJ_FIBOTIMES = 11, // Fibonacci time zones. Uses 2 coordinates. 
            OBJ_FIBOFAN = 12, // Fibonacci fan. Uses 2 coordinates. 
            OBJ_FIBOARC = 13, // Fibonacci arcs. Uses 2 coordinates. 
            OBJ_EXPANSION = 14, // Fibonacci expansions. Uses 3 coordinates. 
            OBJ_FIBOCHANNEL = 15, // Fibonacci channel. Uses 3 coordinates. 
            OBJ_RECTANGLE = 16, // Rectangle. Uses 2 coordinates. 
            OBJ_TRIANGLE = 17, // Triangle. Uses 3 coordinates. 
            OBJ_ELLIPSE = 18, // Ellipse. Uses 2 coordinates. 
            OBJ_PITCHFORK = 19, // Andrews pitchfork. Uses 3 coordinates. 
            OBJ_CYCLES = 20, // Cycles. Uses 2 coordinates. 
            OBJ_TEXT = 21, // Text. Uses 1 coordinate. 
            OBJ_ARROW = 22, // Arrows. Uses 1 coordinate. 
            OBJ_LABEL = 23 // Text label. Uses 1 coordinate in pixels. 
        }
        public const GraphObjectType OBJ_VLINE = (GraphObjectType)0; // Vertical line. Uses time part of first coordinate. 
        public const GraphObjectType OBJ_HLINE = (GraphObjectType)1; // Horizontal line. Uses price part of first coordinate. 
        public const GraphObjectType OBJ_TREND = (GraphObjectType)2; // Trend line. Uses 2 coordinates. 
        public const GraphObjectType OBJ_TRENDBYANGLE = (GraphObjectType)3; // Trend by angle. Uses 1 coordinate. To set angle of line use ObjectSet() function. 
        public const GraphObjectType OBJ_REGRESSION = (GraphObjectType)4; // Regression. Uses time parts of first two coordinates. 
        public const GraphObjectType OBJ_CHANNEL = (GraphObjectType)5; // Channel. Uses 3 coordinates. 
        public const GraphObjectType OBJ_STDDEVCHANNEL = (GraphObjectType)6; // Standard deviation channel. Uses time parts of first two coordinates. 
        public const GraphObjectType OBJ_GANNLINE = (GraphObjectType)7; // Gann line. Uses 2 coordinate, but price part of second coordinate ignored. 
        public const GraphObjectType OBJ_GANNFAN = (GraphObjectType)8; // Gann fan. Uses 2 coordinate, but price part of second coordinate ignored. 
        public const GraphObjectType OBJ_GANNGRID = (GraphObjectType)9; // Gann grid. Uses 2 coordinate, but price part of second coordinate ignored. 
        public const GraphObjectType OBJ_FIBO = (GraphObjectType)10; // Fibonacci retracement. Uses 2 coordinates. 
        public const GraphObjectType OBJ_FIBOTIMES = (GraphObjectType)11; // Fibonacci time zones. Uses 2 coordinates. 
        public const GraphObjectType OBJ_FIBOFAN = (GraphObjectType)12; // Fibonacci fan. Uses 2 coordinates. 
        public const GraphObjectType OBJ_FIBOARC = (GraphObjectType)13; // Fibonacci arcs. Uses 2 coordinates. 
        public const GraphObjectType OBJ_EXPANSION = (GraphObjectType)14; // Fibonacci expansions. Uses 3 coordinates. 
        public const GraphObjectType OBJ_FIBOCHANNEL = (GraphObjectType)15; // Fibonacci channel. Uses 3 coordinates. 
        public const GraphObjectType OBJ_RECTANGLE = (GraphObjectType)16; // Rectangle. Uses 2 coordinates. 
        public const GraphObjectType OBJ_TRIANGLE = (GraphObjectType)17; // Triangle. Uses 3 coordinates. 
        public const GraphObjectType OBJ_ELLIPSE = (GraphObjectType)18; // Ellipse. Uses 2 coordinates. 
        public const GraphObjectType OBJ_PITCHFORK = (GraphObjectType)19; // Andrews pitchfork. Uses 3 coordinates. 
        public const GraphObjectType OBJ_CYCLES = (GraphObjectType)20; // Cycles. Uses 2 coordinates. 
        public const GraphObjectType OBJ_TEXT = (GraphObjectType)21; // Text. Uses 1 coordinate. 
        public const GraphObjectType OBJ_ARROW = (GraphObjectType)22; // Arrows. Uses 1 coordinate. 
        public const GraphObjectType OBJ_LABEL = (GraphObjectType)23; // Text label. Uses 1 coordinate in pixels. 

        // Object properties 
        public enum GraphObjectProperty {
            OBJPROP_TIME1 = 0, // datetime Datetime value to set/get first coordinate time part. 
            OBJPROP_PRICE1 = 1, // double Double value to set/get first coordinate price part. 
            OBJPROP_TIME2 = 2, // datetime Datetime value to set/get second coordinate time part. 
            OBJPROP_PRICE2 = 3, // double Double value to set/get second coordinate price part. 
            OBJPROP_TIME3 = 4, // datetime Datetime value to set/get third coordinate time part. 
            OBJPROP_PRICE3 = 5, // double Double value to set/get third coordinate price part. 
            OBJPROP_COLOR = 6, // color Color value to set/get object color. 
            OBJPROP_STYLE = 7, // int Value is one of STYLE_SOLID, STYLE_DASH, STYLE_DOT, STYLE_DASHDOT, STYLE_DASHDOTDOT constants to set/get object line style. 
            OBJPROP_WIDTH = 8, // int Integer value to set/get object line width. Can be from 1 to 5. 
            OBJPROP_BACK = 9, // bool Boolean value to set/get background drawing flag for object. 
            OBJPROP_RAY = 10, // bool Boolean value to set/get ray flag of object. 
            OBJPROP_ELLIPSE = 11, // bool Boolean value to set/get ellipse flag for fibo arcs. 
            OBJPROP_SCALE = 12, // double Double value to set/get scale object property. 
            OBJPROP_ANGLE = 13, // double Double value to set/get angle object property in degrees. 
            OBJPROP_ARROWCODE = 14, // int Integer value or arrow enumeration to set/get arrow code object property. 
            OBJPROP_TIMEFRAMES = 15, // int Value can be one or combination (bitwise addition) of object visibility constants to set/get timeframe object property. 
            OBJPROP_DEVIATION = 16, // double Double value to set/get deviation property for Standard deviation objects. 
            OBJPROP_FONTSIZE = 100, // int Integer value to set/get font size for text objects. 
            OBJPROP_CORNER = 101, // int Integer value to set/get anchor corner property for label objects. Must be from 0-3. 
            OBJPROP_XDISTANCE = 102, // int Integer value to set/get anchor X distance object property in pixels. 
            OBJPROP_YDISTANCE = 103, // int Integer value is to set/get anchor Y distance object property in pixels. 
            OBJPROP_FIBOLEVELS = 200, // int Integer value to set/get Fibonacci object level count. Can be from 0 to 32. 
            OBJPROP_LEVELCOLOR = 201, // color Color value to set/get object level line color. 
            OBJPROP_LEVELSTYLE = 202, // int Value is one of STYLE_SOLID, STYLE_DASH, STYLE_DOT, STYLE_DASHDOT, STYLE_DASHDOTDOT constants to set/get object level line style. 
            OBJPROP_LEVELWIDTH = 203, // int Integer value to set/get object level line width. Can be from 1 to 5. 
            OBJPROP_FIRSTLEVEL = 210 // int Integer value to set/get the value of Fibonacci object level with index n. Index n can be from 0 (number of levels -1), but not larger than 31. 
        }
        public const GraphObjectProperty OBJPROP_TIME1 = (GraphObjectProperty)0; // datetime Datetime value to set/get first coordinate time part. 
        public const GraphObjectProperty OBJPROP_PRICE1 = (GraphObjectProperty)1; // double Double value to set/get first coordinate price part. 
        public const GraphObjectProperty OBJPROP_TIME2 = (GraphObjectProperty)2; // datetime Datetime value to set/get second coordinate time part. 
        public const GraphObjectProperty OBJPROP_PRICE2 = (GraphObjectProperty)3; // double Double value to set/get second coordinate price part. 
        public const GraphObjectProperty OBJPROP_TIME3 = (GraphObjectProperty)4; // datetime Datetime value to set/get third coordinate time part. 
        public const GraphObjectProperty OBJPROP_PRICE3 = (GraphObjectProperty)5; // double Double value to set/get third coordinate price part. 
        public const GraphObjectProperty OBJPROP_COLOR = (GraphObjectProperty)6; // color Color value to set/get object color. 
        public const GraphObjectProperty OBJPROP_STYLE = (GraphObjectProperty)7; // GraphObjectProperty Value is one of STYLE_SOLID, STYLE_DASH, STYLE_DOT, STYLE_DASHDOT, STYLE_DASHDOTDOT constants to set/get object line style. 
        public const GraphObjectProperty OBJPROP_WIDTH = (GraphObjectProperty)8; // GraphObjectProperty Integer value to set/get object line width. Can be from 1 to 5. 
        public const GraphObjectProperty OBJPROP_BACK = (GraphObjectProperty)9; // bool Boolean value to set/get background drawing flag for object. 
        public const GraphObjectProperty OBJPROP_RAY = (GraphObjectProperty)10; // bool Boolean value to set/get ray flag of object. 
        public const GraphObjectProperty OBJPROP_ELLIPSE = (GraphObjectProperty)11; // bool Boolean value to set/get ellipse flag for fibo arcs. 
        public const GraphObjectProperty OBJPROP_SCALE = (GraphObjectProperty)12; // double Double value to set/get scale object property. 
        public const GraphObjectProperty OBJPROP_ANGLE = (GraphObjectProperty)13; // double Double value to set/get angle object property in degrees. 
        public const GraphObjectProperty OBJPROP_ARROWCODE = (GraphObjectProperty)14; // GraphObjectProperty Integer value or arrow enumeration to set/get arrow code object property. 
        public const GraphObjectProperty OBJPROP_TIMEFRAMES = (GraphObjectProperty)15; // GraphObjectProperty Value can be one or combination (bitwise addition) of object visibility constants to set/get timeframe object property. 
        public const GraphObjectProperty OBJPROP_DEVIATION = (GraphObjectProperty)16; // double Double value to set/get deviation property for Standard deviation objects. 
        public const GraphObjectProperty OBJPROP_FONTSIZE = (GraphObjectProperty)100; // GraphObjectProperty Integer value to set/get font size for text objects. 
        public const GraphObjectProperty OBJPROP_CORNER = (GraphObjectProperty)101; // GraphObjectProperty Integer value to set/get anchor corner property for label objects. Must be from 0-3. 
        public const GraphObjectProperty OBJPROP_XDISTANCE = (GraphObjectProperty)102; // GraphObjectProperty Integer value to set/get anchor X distance object property in pixels. 
        public const GraphObjectProperty OBJPROP_YDISTANCE = (GraphObjectProperty)103; // GraphObjectProperty Integer value is to set/get anchor Y distance object property in pixels. 
        public const GraphObjectProperty OBJPROP_FIBOLEVELS = (GraphObjectProperty)200; // GraphObjectProperty Integer value to set/get Fibonacci object level count. Can be from 0 to 32. 
        public const GraphObjectProperty OBJPROP_LEVELCOLOR = (GraphObjectProperty)201; // color Color value to set/get object level line color. 
        public const GraphObjectProperty OBJPROP_LEVELSTYLE = (GraphObjectProperty)202; // GraphObjectProperty Value is one of STYLE_SOLID, STYLE_DASH, STYLE_DOT, STYLE_DASHDOT, STYLE_DASHDOTDOT constants to set/get object level line style. 
        public const GraphObjectProperty OBJPROP_LEVELWIDTH = (GraphObjectProperty)203; // GraphObjectProperty Integer value to set/get object level line width. Can be from 1 to 5. 
        public const GraphObjectProperty OBJPROP_FIRSTLEVEL = (GraphObjectProperty)210; //GraphObjectProperty Integer value to set/get the value of Fibonacci object level with index n. Index n can be from 0 (number of levels -1), but not larger than 31. 

        // Object visibility
        public enum GraphObjectVisibility {
            OBJ_PERIOD_M1 = 0x0001, // Object shown is only on 1-minute charts. 
            OBJ_PERIOD_M5 = 0x0002, // Object shown is only on 5-minute charts. 
            OBJ_PERIOD_M15 = 0x0004, // Object shown is only on 15-minute charts. 
            OBJ_PERIOD_M30 = 0x0008, // Object shown is only on 30-minute charts. 
            OBJ_PERIOD_H1 = 0x0010, // Object shown is only on 1-hour charts. 
            OBJ_PERIOD_H4 = 0x0020, // Object shown is only on 4-hour charts. 
            OBJ_PERIOD_D1 = 0x0040, // Object shown is only on daily charts. 
            OBJ_PERIOD_W1 = 0x0080, // Object shown is only on weekly charts. 
            OBJ_PERIOD_MN1 = 0x0100, // Object shown is only on monthly charts. 
            OBJ_ALL_PERIODS = 0x01FF, // Object shown is on all timeframes. 
            NULL = 0, // Object shown is on all timeframes. 
            EMPTY = -1 // Hidden object on all timeframes. 
        }
        public const GraphObjectVisibility OBJ_PERIOD_M1 = (GraphObjectVisibility)0x0001; // Object shown is only on 1-minute charts. 
        public const GraphObjectVisibility OBJ_PERIOD_M5 = (GraphObjectVisibility)0x0002; // Object shown is only on 5-minute charts. 
        public const GraphObjectVisibility OBJ_PERIOD_M15 = (GraphObjectVisibility)0x0004; // Object shown is only on 15-minute charts. 
        public const GraphObjectVisibility OBJ_PERIOD_M30 = (GraphObjectVisibility)0x0008; // Object shown is only on 30-minute charts. 
        public const GraphObjectVisibility OBJ_PERIOD_H1 = (GraphObjectVisibility)0x0010; // Object shown is only on 1-hour charts. 
        public const GraphObjectVisibility OBJ_PERIOD_H4 = (GraphObjectVisibility)0x0020; // Object shown is only on 4-hour charts. 
        public const GraphObjectVisibility OBJ_PERIOD_D1 = (GraphObjectVisibility)0x0040; // Object shown is only on daily charts. 
        public const GraphObjectVisibility OBJ_PERIOD_W1 = (GraphObjectVisibility)0x0080; // Object shown is only on weekly charts. 
        public const GraphObjectVisibility OBJ_PERIOD_MN1 = (GraphObjectVisibility)0x0100; // Object shown is only on monthly charts. 
        public const GraphObjectVisibility OBJ_ALL_PERIODS = (GraphObjectVisibility)0x01FF; // Object shown is on all timeframes. 
        //public const int NULL = (GraphObjectVisibility)0; // Object shown is on all timeframes. 
        //public const int EMPTY = (GraphObjectVisibility)-1; // Hidden object on all timeframes. 

        // Uninitialize reason codes
        public enum UninitializeReason {
            REASON_0 = 0, // Script finished its execution independently. 
            REASON_REMOVE = 1, // Expert removed from chart. 
            REASON_RECOMPILE = 2, // Expert recompiled. 
            REASON_CHARTCHANGE = 3, // symbol or timeframe changed on the chart. 
            REASON_CHARTCLOSE = 4, // Chart closed. 
            REASON_PARAMETERS = 5, // Inputs parameters was changed by user. 
            REASON_ACCOUNT = 6 // Other account activated. 
        }
        public const UninitializeReason REASON_0 = (UninitializeReason)0; // Script finished its execution independently. 
        public const UninitializeReason REASON_REMOVE = (UninitializeReason)1; // Expert removed from chart. 
        public const UninitializeReason REASON_RECOMPILE = (UninitializeReason)2; // Expert recompiled. 
        public const UninitializeReason REASON_CHARTCHANGE = (UninitializeReason)3; // symbol or timeframe changed on the chart. 
        public const UninitializeReason REASON_CHARTCLOSE = (UninitializeReason)4; // Chart closed. 
        public const UninitializeReason REASON_PARAMETERS = (UninitializeReason)5; // Inputs parameters was changed by user. 
        public const UninitializeReason REASON_ACCOUNT = (UninitializeReason)6; // Other account activated. 

        // Special constants
        public enum SpecialConstant {
            NULL = 0, // Indicates empty state of the string. 
            EMPTY = -1, // Indicates empty state of the parameter. 
            EMPTY_VALUE = 0x7FFFFFFF, // Default custom indicator empty value. 
            CLR_NONE = unchecked((int) 0xFFFFFFFF), // Indicates empty state of colors. 
            WHOLE_ARRAY = 0 // Used with array functions. Indicates that all array elements will be processed. 
        }
        public const int NULL = 0; // Indicates empty state of the string. 
        public const int EMPTY = -1; // Indicates empty state of the parameter. 
        public const int EMPTY_VALUE = 0x7FFFFFFF; // Default custom indicator empty value. 
        public static readonly Color CLR_NONE = default(Color);// 0xFFFFFFFF; // Indicates empty state of colors. 
        public const int WHOLE_ARRAY = 0; // Used with array functions. Indicates that all array elements will be processed. 


        //public const int MODE_LOW =  1; // Low day price. 
        //public const int MODE_HIGH = 2; // High day price. 
        //public const int MODE_TIME = 5; // The last incoming tick time (last known server time). 
        public const MarketInfoConst MODE_BID = (MarketInfoConst)9; // Last incoming bid price. For the current symbol, it is stored in the predefined variable Bid 
        public const MarketInfoConst MODE_ASK = (MarketInfoConst)10; // Last incoming ask price. For the current symbol, it is stored in the predefined variable Ask 
        public const MarketInfoConst MODE_POINT = (MarketInfoConst)11; // Point size in the quote currency. For the current symbol, it is stored in the predefined variable Point 
        public const MarketInfoConst MODE_DIGITS = (MarketInfoConst)12; // Count of digits after decimal point in the symbol prices. For the current symbol, it is stored in the predefined variable Digits 
        public const MarketInfoConst MODE_SPREAD = (MarketInfoConst)13; // Spread value in points. 
        public const MarketInfoConst MODE_STOPLEVEL = (MarketInfoConst)14; // Stop level in points. 
        public const MarketInfoConst MODE_LOTSIZE = (MarketInfoConst)15; // Lot size in the base currency. 
        public const MarketInfoConst MODE_TICKVALUE = (MarketInfoConst)16; // Tick value in the deposit currency. 
        public const MarketInfoConst MODE_TICKSIZE = (MarketInfoConst)17; // Tick size in the quote currency. 
        public const MarketInfoConst MODE_SWAPLONG = (MarketInfoConst)18; // Swap of the long position. 
        public const MarketInfoConst MODE_SWAPSHORT = (MarketInfoConst)19; // Swap of the short position. 
        public const MarketInfoConst MODE_STARTING = (MarketInfoConst)20; // Market starting date (usually used for futures). 
        public const MarketInfoConst MODE_EXPIRATION = (MarketInfoConst)21; // Market expiration date (usually used for futures). 
        public const MarketInfoConst MODE_TRADEALLOWED = (MarketInfoConst)22; // Trade is allowed for the symbol. 
        public const MarketInfoConst MODE_MINLOT = (MarketInfoConst)23; // Minimum permitted amount of a lot. 
        public const MarketInfoConst MODE_LOTSTEP = (MarketInfoConst)24; // Step for changing lots. 
        public const MarketInfoConst MODE_MAXLOT = (MarketInfoConst)25; // Maximum permitted amount of a lot. 
        public const MarketInfoConst MODE_SWAPTYPE = (MarketInfoConst)26; // Swap calculation method. 0 - in points; 1 - in the symbol base currency; 2 - by interest; 3 - in the margin currency. 
        public const MarketInfoConst MODE_PROFITCALCMODE = (MarketInfoConst)27; // Profit calculation mode. 0 - Forex; 1 - CFD; 2 - Futures. 
        public const MarketInfoConst MODE_MARGINCALCMODE = (MarketInfoConst)28; // Margin calculation mode. 0 - Forex; 1 - CFD; 2 - Futures; 3 - CFD for indices. 
        public const MarketInfoConst MODE_MARGININIT = (MarketInfoConst)29; // Initial margin requirements for 1 lot. 
        public const MarketInfoConst MODE_MARGINMAINTENANCE = (MarketInfoConst)30; // Margin to maintain open positions calculated for 1 lot. 
        public const MarketInfoConst MODE_MARGINHEDGED = (MarketInfoConst)31; // Hedged margin calculated for 1 lot. 
        public const MarketInfoConst MODE_MARGINREQUIRED = (MarketInfoConst)32; // Free margin required to open 1 lot for buying. 
        public const MarketInfoConst MODE_FREEZELEVEL = (MarketInfoConst)33; // Order freeze level in points. If the execution price lies within the range defined by the freeze level, the order cannot be modified, cancelled or closed. 

    }


}
