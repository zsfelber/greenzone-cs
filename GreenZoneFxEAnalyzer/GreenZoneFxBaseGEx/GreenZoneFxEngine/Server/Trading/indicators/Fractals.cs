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
    [Indicator(null, null, "Bill Williams")]
    class Fractals : ServerIndicatorRuntime
    {
        //#property indicator_chart_window
        //#property indicator_buffers 2
        //#property indicator_color1 Red
        //#property indicator_color2 Blue
        //---- input parameters

        //---- buffers
        DArr ExtUpFractalsBuffer;
        DArr ExtDownFractalsBuffer;

        public Fractals(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.CHART_WINDOW;
            IndicatorBuffers(2);
            SetIndexColor(0, Color.Red);
            SetIndexColor(1, Color.Blue);
        }

        public Fractals(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicator buffers mapping  
            SetIndexBuffer(0, ref ExtUpFractalsBuffer);
            SetIndexBuffer(1, ref ExtDownFractalsBuffer);
            //---- drawing settings
            SetIndexStyle(0, DRAW_ARROW);
            SetIndexArrow(0, new WingdingsChar(119));
            SetIndexStyle(1, DRAW_ARROW);
            SetIndexArrow(1, new WingdingsChar(119));
            //----
            SetIndexEmptyValue(0, 0.0);
            SetIndexEmptyValue(1, 0.0);
            //---- name for DataWindow
            SetIndexLabel(0, "Fractal Up");
            SetIndexLabel(1, "Fractal Down");
            //---- initialization done   
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custor indicator deinitialization function                       |
        //+------------------------------------------------------------------+
        public override int Deinit()
        {
            //---- TODO: add your code here

            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator iteration function                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i = 0, nCountedBars;
            bool bFound;
            double dCurrent;
            nCountedBars = IndicatorCounted;
            //---- last counted bar will be recounted    
            if (nCountedBars <= 2)
                i = Bars - nCountedBars - 3;
            if (nCountedBars > 2)
            {
                nCountedBars--;
                i = Bars - nCountedBars - 1;
            }
            //----Up and Down Fractals
            while (i >= 2)
            {
                //----Fractals up
                bFound = false;
                dCurrent = High[i];
                if (dCurrent > High[i + 1] && dCurrent > High[i + 2] && dCurrent > High[i - 1] && dCurrent > High[i - 2])
                {
                    bFound = true;
                    ExtUpFractalsBuffer[i] = dCurrent;
                }
                //----6 bars Fractal
                if (!bFound && (Bars - i - 1) >= 3)
                {
                    if (dCurrent == High[i + 1] && dCurrent > High[i + 2] && dCurrent > High[i + 3] &&
                       dCurrent > High[i - 1] && dCurrent > High[i - 2])
                    {
                        bFound = true;
                        ExtUpFractalsBuffer[i] = dCurrent;
                    }
                }
                //----7 bars Fractal
                if (!bFound && (Bars - i - 1) >= 4)
                {
                    if (dCurrent >= High[i + 1] && dCurrent == High[i + 2] && dCurrent > High[i + 3] && dCurrent > High[i + 4] &&
                       dCurrent > High[i - 1] && dCurrent > High[i - 2])
                    {
                        bFound = true;
                        ExtUpFractalsBuffer[i] = dCurrent;
                    }
                }
                //----8 bars Fractal                          
                if (!bFound && (Bars - i - 1) >= 5)
                {
                    if (dCurrent >= High[i + 1] && dCurrent == High[i + 2] && dCurrent == High[i + 3] && dCurrent > High[i + 4] && dCurrent > High[i + 5] &&
                       dCurrent > High[i - 1] && dCurrent > High[i - 2])
                    {
                        bFound = true;
                        ExtUpFractalsBuffer[i] = dCurrent;
                    }
                }
                //----9 bars Fractal                                        
                if (!bFound && (Bars - i - 1) >= 6)
                {
                    if (dCurrent >= High[i + 1] && dCurrent == High[i + 2] && dCurrent >= High[i + 3] && dCurrent == High[i + 4] && dCurrent > High[i + 5] &&
                       dCurrent > High[i + 6] && dCurrent > High[i - 1] && dCurrent > High[i - 2])
                    {
                        bFound = true;
                        ExtUpFractalsBuffer[i] = dCurrent;
                    }
                }
                //----Fractals down
                bFound = false;
                dCurrent = Low[i];
                if (dCurrent < Low[i + 1] && dCurrent < Low[i + 2] && dCurrent < Low[i - 1] && dCurrent < Low[i - 2])
                {
                    bFound = true;
                    ExtDownFractalsBuffer[i] = dCurrent;
                }
                //----6 bars Fractal
                if (!bFound && (Bars - i - 1) >= 3)
                {
                    if (dCurrent == Low[i + 1] && dCurrent < Low[i + 2] && dCurrent < Low[i + 3] &&
                       dCurrent < Low[i - 1] && dCurrent < Low[i - 2])
                    {
                        bFound = true;
                        ExtDownFractalsBuffer[i] = dCurrent;
                    }
                }
                //----7 bars Fractal
                if (!bFound && (Bars - i - 1) >= 4)
                {
                    if (dCurrent <= Low[i + 1] && dCurrent == Low[i + 2] && dCurrent < Low[i + 3] && dCurrent < Low[i + 4] &&
                       dCurrent < Low[i - 1] && dCurrent < Low[i - 2])
                    {
                        bFound = true;
                        ExtDownFractalsBuffer[i] = dCurrent;
                    }
                }
                //----8 bars Fractal                          
                if (!bFound && (Bars - i - 1) >= 5)
                {
                    if (dCurrent <= Low[i + 1] && dCurrent == Low[i + 2] && dCurrent == Low[i + 3] && dCurrent < Low[i + 4] && dCurrent < Low[i + 5] &&
                       dCurrent < Low[i - 1] && dCurrent < Low[i - 2])
                    {
                        bFound = true;
                        ExtDownFractalsBuffer[i] = dCurrent;
                    }
                }
                //----9 bars Fractal                                        
                if (!bFound && (Bars - i - 1) >= 6)
                {
                    if (dCurrent <= Low[i + 1] && dCurrent == Low[i + 2] && dCurrent <= Low[i + 3] && dCurrent == Low[i + 4] && dCurrent < Low[i + 5] &&
                       dCurrent < Low[i + 6] && dCurrent < Low[i - 1] && dCurrent < Low[i - 2])
                    {
                        bFound = true;
                        ExtDownFractalsBuffer[i] = dCurrent;
                    }
                }
                i--;
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
