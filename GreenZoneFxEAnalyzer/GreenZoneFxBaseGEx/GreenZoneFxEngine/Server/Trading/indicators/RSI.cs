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

    [Indicator("Relative Strength Index", null, "Oscillators")]
    class RSI : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_maximum 100
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue
        //---- input parameters
        int _RSIPeriod = 14;
        PriceConstant _AppliedPrice = 0;

        public PriceConstant AppliedPrice { get { return _AppliedPrice; } set { _AppliedPrice = value; } }

        public int RSIPeriod { get { return _RSIPeriod; } set { _RSIPeriod = value; } }

        //---- buffers
        DArr RSIBuffer;
        DArr PosBuffer;
        DArr NegBuffer;

        DArr seriesArray;

        public RSI(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1, 0, 100,
                new IndicatorBuffer(0,Color.DodgerBlue))
        {
            Session.DisplayScale = 3;
            NumIndicatorLevels = 2;
            SetLevelValue(0, 30);
            SetLevelValue(1, 70);
        }

        public RSI(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string short_name;
            //---- 2 additional buffers are used for counting.
            IndicatorBuffers(3);
            SetIndexBuffer(1, ref PosBuffer);
            SetIndexBuffer(2, ref NegBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref RSIBuffer);
            //---- name for DataWindow and indicator subwindow label
            short_name = "RSI(" + RSIPeriod + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            //----
            SetIndexDrawBegin(0, RSIPeriod);
            //----
            seriesArray = GetSeriesArray(AppliedPrice);
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, counted_bars = IndicatorCounted;
            double rel, negative, positive;
            //----
            if (Bars <= RSIPeriod) return (0);
            //---- initial zero
            if (counted_bars < 1)
                for (i = 1; i <= RSIPeriod; i++) RSIBuffer[Bars - i] = 0.0;
            //----
            i = Bars - RSIPeriod - 1;
            if (counted_bars >= RSIPeriod) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                double sumn = 0.0, sump = 0.0;
                if (i == Bars - RSIPeriod - 1)
                {
                    int k = Bars - 2;
                    //---- initial accumulation
                    while (k >= i)
                    {
                        rel = seriesArray[k] - seriesArray[k + 1];
                        if (rel > 0) sump += rel;
                        else sumn -= rel;
                        k--;
                    }
                    positive = sump / RSIPeriod;
                    negative = sumn / RSIPeriod;
                }
                else
                {
                    //---- smoothed moving average
                    rel = seriesArray[i] - seriesArray[i + 1];
                    if (rel > 0) sump = rel;
                    else sumn = -rel;
                    positive = (PosBuffer[i + 1] * (RSIPeriod - 1) + sump) / RSIPeriod;
                    negative = (NegBuffer[i + 1] * (RSIPeriod - 1) + sumn) / RSIPeriod;
                }
                PosBuffer[i] = positive;
                NegBuffer[i] = negative;
                if (negative == 0.0) RSIBuffer[i] = 0.0;
                else RSIBuffer[i] = 100.0 - 100.0 / (1 + positive / negative);
                i--;
            }
            //----
            return 0;
        }
    }
}
