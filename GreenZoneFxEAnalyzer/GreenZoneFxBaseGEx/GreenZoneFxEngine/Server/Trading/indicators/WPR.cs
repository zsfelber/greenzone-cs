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

    [Indicator(null, "Williams' Percent Range", "Oscillators")]
    class WPR : ServerIndicatorRuntime
    {
        //----
        //#property indicator_separate_window
        //#property indicator_minimum -100
        //#property indicator_maximum 0
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue
        //#property indicator_level1 -20
        //#property indicator_level2 -80
        //---- input parameters
        int _ExtWPRPeriod = 14;
        public int ExtWPRPeriod { get { return _ExtWPRPeriod; } set { _ExtWPRPeriod = value; } }
        //---- buffers
        DArr ExtWPRBuffer;

        public WPR(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            Session.IndicatorMinimum = -100;
            Session.IndicatorMaximum = 0;
            NumIndicatorBuffers = 1;
            SetIndexColor(0, Color.DodgerBlue);
            NumIndicatorLevels = 2;
            SetLevelValue(0, -20);
            SetLevelValue(1, -80);
            Session.DisplayScale = 3;
        }

        public WPR(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
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
            SetIndexBuffer(0, ref ExtWPRBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            //---- name for DataWindow and indicator subwindow label
            sShortName = "%R(" + ExtWPRPeriod + ")";
            IndicatorShortName(sShortName);
            SetIndexLabel(0, sShortName);
            //---- first values aren't drawn
            SetIndexDrawBegin(0, ExtWPRPeriod);
            //----
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Williams’ Percent Range                                          |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, /*nLimit, */nCountedBars;
            //---- insufficient data
            if (Bars <= ExtWPRPeriod)
                return (0);
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //----Williams’ Percent Range calculation
            i = Bars - ExtWPRPeriod - 1;
            if (nCountedBars > ExtWPRPeriod)
                i = Bars - nCountedBars - 1;
            while (i >= 0)
            {
                double dMaxHigh = High[Highest(null, 0, MODE_HIGH, ExtWPRPeriod, i)];
                double dMinLow = Low[Lowest(null, 0, MODE_LOW, ExtWPRPeriod, i)];
                if (!CompareDouble((dMaxHigh - dMinLow), 0.0))
                    ExtWPRBuffer[i] = -100 * (dMaxHigh - Close[i]) / (dMaxHigh - dMinLow);
                i--;
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+ 
    }
}
