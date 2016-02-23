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

    [Indicator("Chaikin Oscillator", null, "Oscillators")]
    class CHO : ServerIndicatorRuntime
    {

        //#property  indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DarkBlue
        public CHO(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DarkBlue))
        {
            Session.DisplayScale = 5;
        }

        public CHO(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //---- input parameters
        int _SlowPeriod = 10; public int SlowPeriod { get { return _SlowPeriod; } set { _SlowPeriod = value; } }
        int _FastPeriod = 3; public int FastPeriod { get { return _FastPeriod; } set { _FastPeriod = value; } }
        MovingAverageMethod _TypeSmooth = (MovingAverageMethod)1; public MovingAverageMethod TypeSmooth { get { return _TypeSmooth; } set { _TypeSmooth = value; } }// 0 - SMA, 1 - EMA
        //---- buffers
        DArr CHOBuffer;
        DArr ADBuffer;
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicators
            string name, smoothString;
            //if (TypeSmooth<0 || TypeSmooth>1) TypeSmooth=1;
            //if (TypeSmooth==0) smoothString="SMA"; else smoothString="EMA";
            smoothString = "" + TypeSmooth;
            name = "Chaikin Oscillator(" + SlowPeriod + "," + FastPeriod + "," + smoothString + ")";

            IndicatorBuffers(2);
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref CHOBuffer);
            SetIndexLabel(0, name);
            SetIndexEmptyValue(0, 0.0);
            SetIndexBuffer(1, ref ADBuffer);
            SetIndexEmptyValue(1, 0.0);
            IndicatorShortName(name);

            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator deinitialization function                       |
        //+------------------------------------------------------------------+
        public override int Deinit()
        {
            //----

            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator iteration function                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int counted_bars = IndicatorCounted;
            int limit, i;
            //----
            if (counted_bars < 0) return (-1);

            if (counted_bars == 0)
            {
                limit = Bars - 1;
                for (i = limit; i >= 0; i--)
                {
                    ADBuffer[i] = iAD(null, 0, i);
                }
                for (i = limit - SlowPeriod; i >= 0; i--)
                {
                    CHOBuffer[i] = iMAOnArray(ADBuffer, 0, FastPeriod, 0, TypeSmooth, i) - iMAOnArray(ADBuffer, 0, SlowPeriod, 0, TypeSmooth, i);
                }
            }

            if (counted_bars > 0)
            {
                limit = Bars - counted_bars;
                for (i = limit; i >= 0; i--)
                {
                    ADBuffer[i] = iAD(null, 0, i);
                }
                for (i = limit; i >= 0; i--)
                {
                    CHOBuffer[i] = iMAOnArray(ADBuffer, 0, FastPeriod, 0, TypeSmooth, i) - iMAOnArray(ADBuffer, 0, SlowPeriod, 0, TypeSmooth, i);
                }
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
