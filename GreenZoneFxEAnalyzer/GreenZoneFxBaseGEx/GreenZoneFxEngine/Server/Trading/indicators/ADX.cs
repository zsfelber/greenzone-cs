using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Util;
using System.Drawing;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{

    [Indicator(null, "Avarage Directional Movement Index", "Trends Indicators")]
    class ADX : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 3
        //#property indicator_color1 LightSeaGreen
        //#property indicator_color2 YellowGreen
        //#property indicator_color3 Wheat
        //---- input parameters
        int _ADXPeriod = 14;
        PriceConstant _Applied_Price = PriceConstant.PRICE_CLOSE;
        public int ADXPeriod { get { return _ADXPeriod; } set { _ADXPeriod = value; } }
        public PriceConstant Applied_Price { get { return _Applied_Price; } set { _Applied_Price = value; } }
        //---- buffers
        DArr ADXBuffer;
        DArr PlusDiBuffer;
        DArr MinusDiBuffer;
        DArr PlusSdiBuffer;
        DArr MinusSdiBuffer;
        DArr TempBuffer;

        DArr seriesArray;

        public ADX(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            Session.DisplayScale = 4;
            IndicatorBuffers(3);
            SetIndexColor(0, Color.LightSeaGreen);
            SetIndexColor(1, Color.YellowGreen);
            SetIndexColor(2, Color.Wheat);
            SetIndexStyle(0, DrawingStyle.DRAW_LINE);
            SetIndexStyle(1, DrawingStyle.DRAW_LINE);
            SetIndexStyle(2, DrawingStyle.DRAW_LINE);
        }

        public ADX(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- 3 additional buffers are used for counting.
            IndicatorBuffers(6);
            //---- indicator buffers
            SetIndexBuffer(0, ref ADXBuffer);
            SetIndexBuffer(1, ref PlusDiBuffer);
            SetIndexBuffer(2, ref MinusDiBuffer);
            SetIndexBuffer(3, ref PlusSdiBuffer);
            SetIndexBuffer(4, ref MinusSdiBuffer);
            SetIndexBuffer(5, ref TempBuffer);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("ADX(" + ADXPeriod + ")");
            SetIndexLabel(0, "ADX");
            SetIndexLabel(1, "+DI");
            SetIndexLabel(2, "-DI");
            //----
            SetIndexDrawBegin(0, ADXPeriod);
            SetIndexDrawBegin(1, ADXPeriod);
            SetIndexDrawBegin(2, ADXPeriod);
            //----
            seriesArray = GetSeriesArray(Applied_Price);

            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Average Directional Movement Index                               |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            double pdm, mdm, tr;
            double price_high, price_low;
            int starti, i, counted_bars = IndicatorCounted;
            //----
            i = Bars - 2;
            PlusSdiBuffer[i + 1] = 0;
            MinusSdiBuffer[i + 1] = 0;
            if (counted_bars >= i) i = Bars - counted_bars - 1;
            starti = i;
            //----
            while (i >= 0)
            {
                price_low = Low[i];
                price_high = High[i];
                //----
                pdm = price_high - High[i + 1];
                mdm = Low[i + 1] - price_low;
                if (pdm < 0) pdm = 0;  // +DM
                if (mdm < 0) mdm = 0;  // -DM
                if (pdm == mdm) { pdm = 0; mdm = 0; }
                else if (pdm < mdm) pdm = 0;
                else if (mdm < pdm) mdm = 0;
                //---- âû÷èñëÿåì èñòèííûé èíòåðâàë
                double num1 = MathAbs(price_high - price_low);
                double num2 = MathAbs(price_high - seriesArray[i + 1]);
                double num3 = MathAbs(price_low - seriesArray[i + 1]);
                tr = MathMax(num1, num2);
                tr = MathMax(tr, num3);
                //---- counting plus/minus direction
                if (tr == 0) { PlusSdiBuffer[i] = 0; MinusSdiBuffer[i] = 0; }
                else { PlusSdiBuffer[i] = 100.0 * pdm / tr; MinusSdiBuffer[i] = 100.0 * mdm / tr; }
                //----
                i--;
            }
            //---- last counted bar will be recounted
            if (counted_bars > 0) counted_bars--;
            int limit = Bars - counted_bars;
            if (limit + 1 > Bars - 1)// zsf
            {
                limit--;
            }
            //---- apply EMA to +DI
            for (i = 0; i <= limit; i++)
                PlusDiBuffer[i] = iMAOnArray(PlusSdiBuffer, Bars, ADXPeriod, 0, MODE_EMA, i);
            //---- apply EMA to -DI
            for (i = 0; i <= limit; i++)
                MinusDiBuffer[i] = iMAOnArray(MinusSdiBuffer, Bars, ADXPeriod, 0, MODE_EMA, i);
            //---- Directional Movement (DX)
            i = Bars - 2;
            TempBuffer[i + 1] = 0;
            i = starti;
            while (i >= 0)
            {
                double div = MathAbs(PlusDiBuffer[i] + MinusDiBuffer[i]);
                if (div == 0.00) TempBuffer[i] = 0;
                else TempBuffer[i] = 100 * (MathAbs(PlusDiBuffer[i] - MinusDiBuffer[i]) / div);
                i--;
            }
            //---- ADX is exponential moving average on DX
            for (i = 0; i < limit; i++)
                ADXBuffer[i] = iMAOnArray(TempBuffer, Bars, ADXPeriod, 0, MODE_EMA, i);
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
