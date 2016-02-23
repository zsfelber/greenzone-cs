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

    [Indicator("Chaikin's Volatility", null, "Oscillators")]
    class CHV : ServerIndicatorRuntime
    {
        public CHV(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DarkBlue))
        {
            Session.DisplayScale = 2;
        }

        public CHV(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //---- input parameters
        int _SmoothPeriod = 10; public int SmoothPeriod { get { return _SmoothPeriod; } set { _SmoothPeriod = value; } }
        int _ROCPeriod = 10; public int ROCPeriod { get { return _ROCPeriod; } set { _ROCPeriod = value; } }
        MovingAverageMethod _TypeSmooth = (MovingAverageMethod)1; public MovingAverageMethod TypeSmooth { get { return _TypeSmooth; } set { _TypeSmooth = value; } }// 0 - SMA, 1 - EMA
        //---- buffers
        DArr CHVBuffer;
        DArr HLBuffer;
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
            name = "Chaikin Volatility(" + SmoothPeriod + "," + smoothString + ")";

            IndicatorBuffers(2);
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref CHVBuffer);
            SetIndexLabel(0, name);
            SetIndexEmptyValue(0, 0.0);
            SetIndexBuffer(1, ref HLBuffer);
            SetIndexEmptyValue(1, 0.0);
            IndicatorShortName(name);
            IndicatorDigits(1);

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
            double curr_value, shift_value;
            //----
            if (counted_bars < 0) return (-1);

            if (counted_bars == 0)
            {
                limit = Bars - 1;
                for (i = limit; i >= 0; i--)
                {
                    HLBuffer[i] = High[i] - Low[i];
                }
                for (i = limit - 2 * SmoothPeriod; i >= 0; i--)
                {
                    curr_value = iMAOnArray(HLBuffer, 0, SmoothPeriod, 0, TypeSmooth, i);
                    shift_value = iMAOnArray(HLBuffer, 0, SmoothPeriod, 0, TypeSmooth, i + ROCPeriod);
                    CHVBuffer[i] = (curr_value - shift_value) / shift_value * 100;
                }
            }

            if (counted_bars > 0)
            {
                limit = Bars - counted_bars;
                for (i = limit; i >= 0; i--)
                {
                    HLBuffer[i] = High[i] - Low[i];
                }
                for (i = limit; i >= 0; i--)
                {
                    curr_value = iMAOnArray(HLBuffer, 0, SmoothPeriod, 0, TypeSmooth, i);
                    shift_value = iMAOnArray(HLBuffer, 0, SmoothPeriod, 0, TypeSmooth, i + ROCPeriod);
                    CHVBuffer[i] = (curr_value - shift_value) / shift_value * 100;
                }
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
