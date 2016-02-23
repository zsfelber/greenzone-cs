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

    //[Indicator("Daily Pivot Points", null, "Trends Indicators")]
    class DailyPivotPoints : ServerIndicatorRuntime
    {
        public DailyPivotPoints(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 2,
                new IndicatorBuffer(0,Color.Blue),
                new IndicatorBuffer(1,Color.Red))
        {
        }

        public DailyPivotPoints(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //---- input parameters
        int _ExtFormula = 0; public int ExtFormula { get { return _ExtFormula; } set { _ExtFormula = value; } }
        int _ExtHowManyDays = 30; public int ExtHowManyDays { get { return _ExtHowManyDays; } set { _ExtHowManyDays = value; } }
        bool _ExtDraw = true; public bool ExtDraw { get { return _ExtDraw; } set { _ExtDraw = value; } }
        //---- buffers
        DArr ExtMapBuffer1;
        DArr ExtMapBuffer2;
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicators
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref ExtMapBuffer1);
            SetIndexEmptyValue(0, 0.0);
            SetIndexStyle(1, DRAW_LINE);
            SetIndexBuffer(1, ref ExtMapBuffer2);
            SetIndexEmptyValue(1, 0.0);
            //---- clear buffers when reinitializing
            if (ArraySize(ExtMapBuffer1) > 0) ArrayInitialize(ExtMapBuffer1, 0.0);
            if (ArraySize(ExtMapBuffer2) > 0) ArrayInitialize(ExtMapBuffer2, 0.0);
            //---- set labels for DataWindow
            if (ExtDraw)
            {
                if (ExtFormula == 0)
                {
                    SetIndexLabel(0, "Pivot");
                    SetIndexLabel(1, null);
                }
                else
                {
                    SetIndexLabel(0, "Resistance");
                    SetIndexLabel(1, "Support");
                }
            }
            else
            {
                SetIndexLabel(0, null);
                SetIndexLabel(1, null);
            }
            //---- force daily data load
            iBars(null, PERIOD_D1);
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator deinitialization function                       |
        //+------------------------------------------------------------------+
        public override int Deinit()
        {
            //---- deleting our lines
            ObjectDelete("Pivot_Line");
            ObjectDelete("R0.5_Line");
            ObjectDelete("R1.0_Line");
            ObjectDelete("R1.5_Line");
            ObjectDelete("R2.0_Line");
            ObjectDelete("R2.5_Line");
            ObjectDelete("R3.0_Line");
            ObjectDelete("S0.5_Line");
            ObjectDelete("S1.0_Line");
            ObjectDelete("S1.5_Line");
            ObjectDelete("S2.0_Line");
            ObjectDelete("S2.5_Line");
            ObjectDelete("S3.0_Line");
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator iteration function                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int counted_bars = IndicatorCounted;
            int begin_bar, first_bar, last_bar, cnt;
            double yesterday_high = 0, yesterday_low = 0, yesterday_close = 0, today_open;
            double P, S = 0, R = 0, S05, R05, S10, R10, S15, R15, S20, R20, S25, R25, S30, R30;
            //---- test parameters
            if (ExtFormula < 0 || ExtFormula > 3) return (-1);
            if (Period >= PERIOD_D1) return (-1);
            //---- if daily data not loaded yet
            cnt = 0;
            while (true)
            {
                if (iTime(null, PERIOD_D1, 0) >= (Time[0] - (int)PERIOD_D1 * 60)) break;
                cnt++;
                if (cnt > 5) return (0);
                Sleep(1000);
            }
            //---- set check beginning
            if (ExtHowManyDays < 1) begin_bar = iBars(null, PERIOD_D1) - 2;
            else begin_bar = ExtHowManyDays - 1;
            //---- case of recounting current pivot only
            if (ExtDraw == false || counted_bars > 0) begin_bar = 0;
            //----
            for (cnt = begin_bar; cnt >= 0; cnt--)
            {
                yesterday_close = iClose(null, PERIOD_D1, cnt + 1);
                today_open = iOpen(null, PERIOD_D1, cnt);
                yesterday_high = iHigh(null, PERIOD_D1, cnt + 1);
                yesterday_low = iLow(null, PERIOD_D1, cnt + 1);
                P = (yesterday_high + yesterday_low + yesterday_close + today_open) / 4;
                switch (ExtFormula)
                {
                    case 1:
                        R = P + P - yesterday_low;
                        S = P + P - yesterday_high;
                        break;
                    case 2:
                        R = P + yesterday_high - yesterday_low;
                        S = P - yesterday_high + yesterday_low;
                        break;
                    case 3:
                        R = P + P - yesterday_low - yesterday_low + yesterday_high;
                        S = P + P - yesterday_high - yesterday_high + yesterday_low;
                        break;
                }
                if (ExtDraw == true)
                {
                    first_bar = iBarShift(null, 0, iTime(null, PERIOD_D1, cnt)) - 1;
                    if (cnt > 0) last_bar = iBarShift(null, 0, iTime(null, PERIOD_D1, cnt - 1)) - 1;
                    else last_bar = 0;
                    while (first_bar >= last_bar)
                    {
                        if (first_bar == last_bar && last_bar > 0) break;
                        if (ExtFormula == 0) ExtMapBuffer1[first_bar] = P;
                        else
                        {
                            ExtMapBuffer1[first_bar] = R;
                            ExtMapBuffer2[first_bar] = S;
                        }
                        first_bar--;
                    }
                }
            }
            P = NormalizeDouble((yesterday_high + yesterday_low + yesterday_close) / 3, Digits);

            R10 = NormalizeDouble((2 * P) - yesterday_low, Digits);
            S10 = NormalizeDouble((2 * P) - yesterday_high, Digits);

            R05 = NormalizeDouble((P + R10) / 2, Digits);
            S05 = NormalizeDouble((P + S10) / 2, Digits);

            R20 = NormalizeDouble(P + (yesterday_high - yesterday_low), Digits);
            S20 = NormalizeDouble(P - (yesterday_high - yesterday_low), Digits);

            R15 = NormalizeDouble((R10 + R20) / 2, Digits);
            S15 = NormalizeDouble((S10 + S20) / 2, Digits);

            R30 = NormalizeDouble(2 * P + (yesterday_high - 2 * yesterday_low), Digits);
            S30 = NormalizeDouble(2 * P - (2 * yesterday_high - yesterday_low), Digits);

            R25 = NormalizeDouble((R20 + R30) / 2, Digits);
            S25 = NormalizeDouble((S20 + S30) / 2, Digits);

            ObjectCreate("Pivot_Line", OBJ_HLINE, 0, 0, P);
            ObjectSet("Pivot_Line", OBJPROP_COLOR, Color.Yellow);
            ObjectSet("Pivot_Line", OBJPROP_STYLE, STYLE_SOLID);
            ObjectSetText("Pivot_Line", "Pivot " + DoubleToStr(P, Digits));

            ObjectCreate("R0.5_Line", OBJ_HLINE, 0, 0, R05);
            ObjectSet("R0.5_Line", OBJPROP_COLOR, Color.GreenYellow);
            ObjectSet("R0.5_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("R0.5_Line", "R0.5 " + DoubleToStr(R05, Digits));

            ObjectCreate("R1.0_Line", OBJ_HLINE, 0, 0, R10);
            ObjectSet("R1.0_Line", OBJPROP_COLOR, Color.YellowGreen);
            ObjectSet("R1.0_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("R1.0_Line", "R1.0 " + DoubleToStr(R10, Digits));

            ObjectCreate("R1.5_Line", OBJ_HLINE, 0, 0, R15);
            ObjectSet("R1.5_Line", OBJPROP_COLOR, Color.GreenYellow);
            ObjectSet("R1.5_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("R1.5_Line", "R1.5 " + DoubleToStr(R15, Digits));

            ObjectCreate("R2.0_Line", OBJ_HLINE, 0, 0, R20);
            ObjectSet("R2.0_Line", OBJPROP_COLOR, Color.YellowGreen);
            ObjectSet("R2.0_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("R2.0_Line", "R2.0 " + DoubleToStr(R20, Digits));

            ObjectCreate("R2.5_Line", OBJ_HLINE, 0, 0, R25);
            ObjectSet("R2.5_Line", OBJPROP_COLOR, Color.GreenYellow);
            ObjectSet("R2.5_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("R2.5_Line", "R2.5 " + DoubleToStr(R25, Digits));

            ObjectCreate("R3.0_Line", OBJ_HLINE, 0, 0, R30);
            ObjectSet("R3.0_Line", OBJPROP_COLOR, Color.YellowGreen);
            ObjectSet("R3.0_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("R3.0_Line", "R3.0 " + DoubleToStr(R30, Digits));

            ObjectCreate("S0.5_Line", OBJ_HLINE, 0, 0, S05);
            ObjectSet("S0.5_Line", OBJPROP_COLOR, Color.Salmon);
            ObjectSet("S0.5_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("S0.5_Line", "S0.5 " + DoubleToStr(S05, Digits));

            ObjectCreate("S1.0_Line", OBJ_HLINE, 0, 0, S10);
            ObjectSet("S1.0_Line", OBJPROP_COLOR, Color.Salmon);
            ObjectSet("S1.0_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("S1.0_Line", "S1.0 " + DoubleToStr(S10, Digits));

            ObjectCreate("S1.5_Line", OBJ_HLINE, 0, 0, S15);
            ObjectSet("S1.5_Line", OBJPROP_COLOR, Color.Salmon);
            ObjectSet("S1.5_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("S1.5_Line", "S1.5 " + DoubleToStr(S15, Digits));

            ObjectCreate("S2.0_Line", OBJ_HLINE, 0, 0, S20);
            ObjectSet("S2.0_Line", OBJPROP_COLOR, Color.Salmon);
            ObjectSet("S2.0_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("S2.0_Line", "S2.0 " + DoubleToStr(S20, Digits));

            ObjectCreate("S2.5_Line", OBJ_HLINE, 0, 0, S25);
            ObjectSet("S2.5_Line", OBJPROP_COLOR, Color.Salmon);
            ObjectSet("S2.5_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("S2.5_Line", "S2.5 " + DoubleToStr(S25, Digits));

            ObjectCreate("S3.0_Line", OBJ_HLINE, 0, 0, S30);
            ObjectSet("S3.0_Line", OBJPROP_COLOR, Color.Salmon);
            ObjectSet("S3.0_Line", OBJPROP_STYLE, STYLE_DOT);
            ObjectSetText("S3.0_Line", "S3.0 " + DoubleToStr(S30, Digits));
            //---- force objects drawing
            ObjectsRedraw();
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
