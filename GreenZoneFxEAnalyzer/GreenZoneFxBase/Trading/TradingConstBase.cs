using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Trading
{
    // Drawing styles
    public enum DrawingStyle
    {
        DRAW_LINE = 0, // Drawing line. 
        DRAW_SECTION = 1, // Drawing sections. 
        DRAW_HISTOGRAM = 2, // Drawing histogram. 
        DRAW_ARROW = 3, // Drawing arrows (symbols). 
        DRAW_ZIGZAG = 4, // Drawing sections between even and odd indicator buffers. 
        DRAW_NONE = 12 // No drawing. 
    }

    // ...Drawing styles width=1
    public enum DrawingStylesWidth1
    {
        STYLE_SOLID = 0, // The pen is solid. 
        STYLE_DASH = 1, // The pen is dashed. 
        STYLE_DOT = 2, // The pen is dotted. 
        STYLE_DASHDOT = 3, // The pen has alternating dashes and dots. 
        STYLE_DASHDOTDOT = 4 // The pen has alternating dashes and double dots. 
    }

    // ...Drawing styles width=1
    public enum DrawingWidth
    {
        WIDTH_1 = 1,
        WIDTH_2,
        WIDTH_3,
        WIDTH_4,
        WIDTH_5
    }

    public enum IndicatorWindowType
    {
        CHART_WINDOW, SEPARATE_WINDOW
    }

    public enum ChartType
    {
        CANDLESTICKS, OHLC, LINE
    }

    public enum TimePeriodConst
    {
        // secs
        PERIOD_S55 = -155,
        PERIOD_S50 = -150,
        PERIOD_S45 = -145,
        PERIOD_S40 = -140,
        PERIOD_S35 = -135,
        PERIOD_S30 = -130,
        PERIOD_S25 = -125,
        PERIOD_S20 = -120,
        PERIOD_S15 = -115,
        PERIOD_S12 = -112,
        PERIOD_S10 = -110,
        PERIOD_S6 = -106,
        PERIOD_S5 = -105,
        PERIOD_S4 = -104,
        PERIOD_S3 = -103,
        PERIOD_S2 = -102,
        PERIOD_S1 = -101,
        // ticks
        PERIOD_TICK_ASK = -2,
        PERIOD_TICK = -1,
        // system
        PERIOD_CURRENT = 0,
        // MT4 - MT5 
        PERIOD_M1 = 1,
        PERIOD_M2 = 2,
        PERIOD_M3 = 3,
        PERIOD_M4 = 4,
        PERIOD_M5 = 5,
        PERIOD_M6 = 6,
        PERIOD_M10 = 10,
        PERIOD_M12 = 12,
        PERIOD_M15 = 15,
        PERIOD_M20 = 20,
        PERIOD_M30 = 30,
        PERIOD_H1 = 60,
        PERIOD_H2 = 120,
        PERIOD_H3 = 180,
        PERIOD_H4 = 240,
        PERIOD_H6 = 360,
        PERIOD_H8 = 480,
        PERIOD_H12 = 720,
        PERIOD_D1 = 1440,
        PERIOD_W1 = 10080,
        PERIOD_MN1 = 43200,
    }

    [Flags]
    public enum TimePeriodCategory
    {
        SYSTEM = 1,
        TICKS = 2,
        MT4 = 4,
        MT5 = 8,
        SECS = 16
    }

    public static class EnumExtensions0
    {
        public static TimePeriodCategory GetCategory(this TimePeriodConst period)
        {
            switch (period)
            {
                case TimePeriodConst.PERIOD_CURRENT:
                    return TimePeriodCategory.SYSTEM;
                // ticks
                case TimePeriodConst.PERIOD_TICK_ASK:
                case TimePeriodConst.PERIOD_TICK:
                    return TimePeriodCategory.TICKS;
                // MT4 standard
                case TimePeriodConst.PERIOD_M1:
                case TimePeriodConst.PERIOD_M5:
                case TimePeriodConst.PERIOD_M15:
                case TimePeriodConst.PERIOD_M30:
                case TimePeriodConst.PERIOD_H1:
                case TimePeriodConst.PERIOD_H4:
                case TimePeriodConst.PERIOD_D1:
                case TimePeriodConst.PERIOD_W1:
                case TimePeriodConst.PERIOD_MN1:
                    return TimePeriodCategory.MT4;
                // MT5 standard
                case TimePeriodConst.PERIOD_M2:
                case TimePeriodConst.PERIOD_M3:
                case TimePeriodConst.PERIOD_M4:
                case TimePeriodConst.PERIOD_M6:
                case TimePeriodConst.PERIOD_M10:
                case TimePeriodConst.PERIOD_M12:
                case TimePeriodConst.PERIOD_M20:
                case TimePeriodConst.PERIOD_H2:
                case TimePeriodConst.PERIOD_H3:
                case TimePeriodConst.PERIOD_H6:
                case TimePeriodConst.PERIOD_H8:
                case TimePeriodConst.PERIOD_H12:
                    return TimePeriodCategory.MT5;
                // secs
                case TimePeriodConst.PERIOD_S1:
                case TimePeriodConst.PERIOD_S2:
                case TimePeriodConst.PERIOD_S3:
                case TimePeriodConst.PERIOD_S4:
                case TimePeriodConst.PERIOD_S5:
                case TimePeriodConst.PERIOD_S6:
                case TimePeriodConst.PERIOD_S10:
                case TimePeriodConst.PERIOD_S12:
                case TimePeriodConst.PERIOD_S15:
                case TimePeriodConst.PERIOD_S20:
                case TimePeriodConst.PERIOD_S25:
                case TimePeriodConst.PERIOD_S30:
                case TimePeriodConst.PERIOD_S35:
                case TimePeriodConst.PERIOD_S40:
                case TimePeriodConst.PERIOD_S45:
                case TimePeriodConst.PERIOD_S50:
                case TimePeriodConst.PERIOD_S55:
                    return TimePeriodCategory.SECS;
                default:
                    throw new NotSupportedException("" + period);
            }
        }
    }

    public class TradingConstBase
    {
        public const int EMPTY_VALUE = 0x7FFFFFFF; // Default custom indicator empty value. 
    }
}
