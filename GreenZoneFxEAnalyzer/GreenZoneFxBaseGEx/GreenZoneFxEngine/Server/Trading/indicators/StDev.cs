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

    [Indicator("Standard Deviation", null, "Trends Indicators")]
    class StDev : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_buffers 1
        //#property indicator_color1 Blue
        //---- input parameters
        int _ExtStdDevPeriod = 20;
        public int ExtStdDevPeriod { get { return _ExtStdDevPeriod; } set { _ExtStdDevPeriod = value; } }
        MovingAverageMethod _ExtStdDevMAMethod = 0;
        public MovingAverageMethod ExtStdDevMAMethod { get { return _ExtStdDevMAMethod; } set { _ExtStdDevMAMethod = value; } }
        PriceConstant _ExtStdDevAppliedPrice = 0;
        public PriceConstant ExtStdDevAppliedPrice { get { return _ExtStdDevAppliedPrice; } set { _ExtStdDevAppliedPrice = value; } }
        int _ExtStdDevShift = 0;
        public int ExtStdDevShift { get { return _ExtStdDevShift; } set { _ExtStdDevShift = value; } }
        //---- buffers
        DArr ExtStdDevBuffer;

        public StDev(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            Session.IndicatorMinimum = 0;
            NumIndicatorBuffers = 1;
            SetIndexColor(0, Color.Blue);
            SetIndexStyle(0, DrawingStyle.DRAW_LINE);
            Session.DisplayScale = 1;
        }

        public StDev(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            string sShortName;
            //---- indicator buffer mapping
            SetIndexBuffer(0, ref ExtStdDevBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            //---- line shifts when drawing
            SetIndexShift(0, ExtStdDevShift);
            //---- name for DataWindow and indicator subwindow label
            sShortName = "StdDev(" + ExtStdDevPeriod + ")";
            IndicatorShortName(sShortName);
            SetIndexLabel(0, sShortName);
            //---- first values aren't drawn
            SetIndexDrawBegin(0, ExtStdDevPeriod);
            //----
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Standard Deviation                                               |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, j, /*nLimit, */nCountedBars;
            double dAPrice, dAmount, dMovingAverage;
            //---- insufficient data
            if (Bars <= ExtStdDevPeriod) return (0);
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //----Standard Deviation calculation
            i = Bars - ExtStdDevPeriod - 1;
            if (nCountedBars > ExtStdDevPeriod)
                i = Bars - nCountedBars;
            while (i >= 0)
            {
                dAmount = 0.0;
                dMovingAverage = iMA(null, 0, ExtStdDevPeriod, 0, ExtStdDevMAMethod, ExtStdDevAppliedPrice, i);
                for (j = 0; j < ExtStdDevPeriod; j++)
                {
                    dAPrice = GetAppliedPrice(ExtStdDevAppliedPrice, i + j);
                    dAmount += (dAPrice - dMovingAverage) * (dAPrice - dMovingAverage);
                }
                ExtStdDevBuffer[i] = Math.Sqrt(dAmount / ExtStdDevPeriod);
                i--;
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        double GetAppliedPrice(PriceConstant nAppliedPrice, int nIndex)
        {
            double dPrice;
            //----
            switch (nAppliedPrice)
            {
                case (PriceConstant)0: dPrice = Close[nIndex]; break;
                case (PriceConstant)1: dPrice = Open[nIndex]; break;
                case (PriceConstant)2: dPrice = High[nIndex]; break;
                case (PriceConstant)3: dPrice = Low[nIndex]; break;
                case (PriceConstant)4: dPrice = (High[nIndex] + Low[nIndex]) / 2.0; break;
                case (PriceConstant)5: dPrice = (High[nIndex] + Low[nIndex] + Close[nIndex]) / 3.0; break;
                case (PriceConstant)6: dPrice = (High[nIndex] + Low[nIndex] + 2 * Close[nIndex]) / 4.0; break;
                default: dPrice = 0.0; break;
            }
            //----
            return (dPrice);
        }
        //+------------------------------------------------------------------+
    }
}
