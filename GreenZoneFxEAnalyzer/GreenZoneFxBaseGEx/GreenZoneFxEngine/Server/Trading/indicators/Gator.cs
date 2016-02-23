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

    [Indicator("Gator Oscillator", null, "Bill Williams")]
    class Gator : ServerIndicatorRuntime
    {
        //********************************************************************
        // Attention! Following correlations must abide:
        // 1.ExtJawsPeriod>ExtTeethPeriod>ExtLipsPeriod;
        // 2.ExtJawsShift>ExtTeethShift>ExtLipsShift;
        // 3.ExtJawsPeriod>ExtJawsShift;
        // 4.ExtTeethPeriod>ExtTeethShift;
        // 5.ExtLipsPeriod>ExtLipsShift.
        //********************************************************************
        //---- indicator settings
        //#property  indicator_separate_window
        //#property indicator_buffers 6
        //#property indicator_color1 Black
        //#property indicator_color2 Red
        //#property indicator_color3 Green
        //#property indicator_color4 Black
        //#property indicator_color5 Red
        //#property indicator_color6 Green
        //---- input parameters
        int _ExtJawsPeriod = 13;
        public int ExtJawsPeriod { get { return _ExtJawsPeriod; } set { _ExtJawsPeriod = value; } }
        int _ExtJawsShift = 8;
        public int ExtJawsShift { get { return _ExtJawsShift; } set { _ExtJawsShift = value; } }
        int _ExtTeethPeriod = 8;
        public int ExtTeethPeriod { get { return _ExtTeethPeriod; } set { _ExtTeethPeriod = value; } }
        int _ExtTeethShift = 5;
        public int ExtTeethShift { get { return _ExtTeethShift; } set { _ExtTeethShift = value; } }
        int _ExtLipsPeriod = 5;
        public int ExtLipsPeriod { get { return _ExtLipsPeriod; } set { _ExtLipsPeriod = value; } }
        int _ExtLipsShift = 3;
        public int ExtLipsShift { get { return _ExtLipsShift; } set { _ExtLipsShift = value; } }
        MovingAverageMethod _ExtMAMethod = (MovingAverageMethod)2;
        public MovingAverageMethod ExtMAMethod { get { return _ExtMAMethod; } set { _ExtMAMethod = value; } }
        PriceConstant _ExtAppliedPrice = PRICE_MEDIAN;
        public PriceConstant ExtAppliedPrice { get { return _ExtAppliedPrice; } set { _ExtAppliedPrice = value; } }
        //---- indicator buffers
        DArr ExtUpBuffer;
        DArr ExtUpRedBuffer;
        DArr ExtUpGreenBuffer;
        DArr ExtDownBuffer;
        DArr ExtDownRedBuffer;
        DArr ExtDownGreenBuffer;

        public Gator(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            IndicatorBuffers(6);
            SetIndexColor(0, Color.Black);
            SetIndexColor(1, Color.Red);
            SetIndexColor(2, Color.Green);
            SetIndexColor(3, Color.Black);
            SetIndexColor(4, Color.Red);
            SetIndexColor(5, Color.Green);
        }

        public Gator(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtUpBuffer);
            SetIndexBuffer(1, ref ExtUpRedBuffer);
            SetIndexBuffer(2, ref ExtUpGreenBuffer);
            SetIndexBuffer(3, ref ExtDownBuffer);
            SetIndexBuffer(4, ref ExtDownRedBuffer);
            SetIndexBuffer(5, ref ExtDownGreenBuffer);
            //----
            IndicatorDigits(Digits + 1);
            //---- sets first bar from what index will be drawn
            SetIndexDrawBegin(1, ExtJawsPeriod + ExtJawsShift - ExtTeethShift);
            SetIndexDrawBegin(2, ExtJawsPeriod + ExtJawsShift - ExtTeethShift);
            SetIndexDrawBegin(4, ExtTeethPeriod + ExtTeethShift - ExtLipsShift);
            SetIndexDrawBegin(5, ExtTeethPeriod + ExtTeethShift - ExtLipsShift);
            //---- line shifts when drawing
            SetIndexShift(0, ExtTeethShift);
            SetIndexShift(1, ExtTeethShift);
            SetIndexShift(2, ExtTeethShift);
            SetIndexShift(3, ExtLipsShift);
            SetIndexShift(4, ExtLipsShift);
            SetIndexShift(5, ExtLipsShift);
            //---- drawing settings
            SetIndexStyle(0, DRAW_NONE);
            SetIndexStyle(1, DRAW_HISTOGRAM);
            SetIndexStyle(2, DRAW_HISTOGRAM);
            SetIndexStyle(3, DRAW_NONE);
            SetIndexStyle(4, DRAW_HISTOGRAM);
            SetIndexStyle(5, DRAW_HISTOGRAM);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("Gator(" + ExtJawsPeriod + "," + ExtTeethPeriod + "," + ExtLipsPeriod + ")");
            SetIndexLabel(0, "GatorUp");
            SetIndexLabel(1, null);
            SetIndexLabel(2, null);
            SetIndexLabel(3, "GatorDown");
            SetIndexLabel(4, null);
            SetIndexLabel(5, null);
            //---- sets drawing line empty value
            SetIndexEmptyValue(1, 0.0);
            SetIndexEmptyValue(2, 0.0);
            SetIndexEmptyValue(4, 0.0);
            SetIndexEmptyValue(5, 0.0);
            //---- initialization done
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Gator Oscillator                                                 |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, nLimit;
            double dPrev, dCurrent;
            //---- bars count that does not changed after last indicator launch.
            int nCountedBars = IndicatorCounted;
            //---- last counted bar will be recounted
            if (nCountedBars <= ExtTeethPeriod + ExtTeethShift - ExtLipsShift)
                nLimit = Bars - (ExtTeethPeriod + ExtTeethShift - ExtLipsShift);
            else
                nLimit = Bars - nCountedBars - 1;
            //---- moving averages absolute difference
            for (i = 0; i < nLimit + 1; i++)
            {
                dCurrent = iMA(null, 0, ExtTeethPeriod, ExtTeethShift - ExtLipsShift, ExtMAMethod, ExtAppliedPrice, i) -
                         iMA(null, 0, ExtLipsPeriod, 0, ExtMAMethod, ExtAppliedPrice, i);
                if (dCurrent <= 0.0) ExtDownBuffer[i] = dCurrent;
                else ExtDownBuffer[i] = -dCurrent;
            }
            //---- dispatch values between 2 lower buffers
            for (i = nLimit - 1; i >= 0; i--)
            {
                dPrev = ExtDownBuffer[i + 1];
                dCurrent = ExtDownBuffer[i];
                if (dCurrent < dPrev)
                {
                    ExtDownRedBuffer[i] = 0.0;
                    ExtDownGreenBuffer[i] = dCurrent;
                }
                if (dCurrent > dPrev)
                {
                    ExtDownRedBuffer[i] = dCurrent;
                    ExtDownGreenBuffer[i] = 0.0;
                }
                //---- arbitrage
                if (dCurrent == dPrev)
                {
                    if (ExtDownRedBuffer[i + 1] < 0.0) ExtDownRedBuffer[i] = dCurrent;
                    else ExtDownGreenBuffer[i] = dCurrent;
                }
            }
            //---- last counted bar will be recounted
            if (nCountedBars <= ExtJawsPeriod + ExtJawsShift - ExtTeethShift)
                nLimit = Bars - (ExtJawsPeriod + ExtJawsShift - ExtTeethShift);
            else
                nLimit = Bars - nCountedBars - 1;
            //---- moving averages absolute difference
            for (i = 0; i < nLimit + 1; i++)
            {
                dCurrent = iMA(null, 0, ExtJawsPeriod, ExtJawsShift - ExtTeethShift, ExtMAMethod, ExtAppliedPrice, i) -
                             iMA(null, 0, ExtTeethPeriod, 0, ExtMAMethod, ExtAppliedPrice, i);
                if (dCurrent >= 0.0) ExtUpBuffer[i] = dCurrent;
                else ExtUpBuffer[i] = -dCurrent;
            }
            //---- dispatch values between 2 upper buffers
            for (i = nLimit - 1; i >= 0; i--)
            {
                dPrev = ExtUpBuffer[i + 1];
                dCurrent = ExtUpBuffer[i];
                if (dCurrent > dPrev)
                {
                    ExtUpRedBuffer[i] = 0.0;
                    ExtUpGreenBuffer[i] = dCurrent;
                }
                if (dCurrent < dPrev)
                {
                    ExtUpRedBuffer[i] = dCurrent;
                    ExtUpGreenBuffer[i] = 0.0;
                }
                //---- arbitrage
                if (dCurrent == dPrev)
                {
                    if (ExtUpGreenBuffer[i + 1] > 0.0) ExtUpGreenBuffer[i] = dCurrent;
                    else ExtUpRedBuffer[i] = dCurrent;
                }
            }
            //---- done
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
