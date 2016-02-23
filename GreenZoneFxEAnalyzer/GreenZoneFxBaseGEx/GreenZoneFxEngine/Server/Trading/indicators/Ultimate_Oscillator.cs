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

    [Indicator("Ultimate Oscillator", null, "Oscillators")]
    class Ultimate_Oscillator : ServerIndicatorRuntime
    {
        //----
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 Blue
        //#property indicator_level1 30
        //#property indicator_level2 70
        //#property indicator_levelcolor Blue
        ////#property indicator_maximum 100
        ////#property indicator_minimum 0
        public Ultimate_Oscillator(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.Blue))
        {
            NumIndicatorLevels = 2;
            SetLevelValue(0, 30);
            SetLevelValue(1, 70);
            SetLevelColor(Color.Blue);
            Session.DisplayScale = 4;
        }

        public Ultimate_Oscillator(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //---- input parameters
        int _fastperiod = 7; public int fastperiod { get { return _fastperiod; } set { _fastperiod = value; } }
        int _middleperiod = 14; public int middleperiod { get { return _middleperiod; } set { _middleperiod = value; } }
        int _slowperiod = 28; public int slowperiod { get { return _slowperiod; } set { _slowperiod = value; } }
        int _fastK = 4; public int fastK { get { return _fastK; } set { _fastK = value; } }
        int _middleK = 2; public int middleK { get { return _middleK; } set { _middleK = value; } }
        int _slowK = 1; public int slowK { get { return _slowK; } set { _slowK = value; } }
        //---- buffers
        DArr UOBuffer;
        DArr BPBuffer;
        double divider;
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicators
            string name;
            name = "UOS(" + fastperiod + ", " + middleperiod + ", " + slowperiod + ")";
            IndicatorBuffers(2);
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref UOBuffer);
            SetIndexDrawBegin(0, slowperiod);
            SetIndexBuffer(1, ref BPBuffer);
            IndicatorShortName(name);
            IndicatorDigits(1);
            divider = fastK + middleK + slowK;
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator deinitialization function                       |
        //+------------------------------------------------------------------+
        public override int Deinit()
        {
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator iteration function                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int counted_bars = IndicatorCounted;
            //----
            int i, limit = 0, limit2 = 0;
            double TL, RawUO;
            if (counted_bars == 0)
            {
                limit = Bars - 2;
                limit2 = Bars - slowperiod;
            }
            if (counted_bars > 0)
            {
                limit = Bars - counted_bars;
                limit2 = limit;
            }
            for (i = limit; i >= 0; i--)
            {
                TL = MathMin(Low[i], Close[i + 1]);
                BPBuffer[i] = Close[i] - TL;
            }
            for (i = limit2; i >= 0; i--)
            {
                RawUO = fastK * iMAOnArray(BPBuffer, 0, fastperiod, 0, MODE_SMA, i) /
                        iATR(null, 0, fastperiod, i) +
                        middleK * iMAOnArray(BPBuffer, 0, middleperiod, 0, MODE_SMA, i) /
                        iATR(null, 0, middleperiod, i) +
                        slowK * iMAOnArray(BPBuffer, 0, slowperiod, 0, MODE_SMA, i) /
                        iATR(null, 0, slowperiod, i);
                UOBuffer[i] = RawUO / divider * 100;
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+

    }
}
