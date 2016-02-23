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

    [Indicator(null, null, "Oscillators")]
    class Envelopes : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property indicator_chart_window
        //#property indicator_buffers 2
        //#property indicator_color1 Blue
        //#property indicator_color2 Red
        //---- indicator parameters
        int _MA_Period = 14;
        public int MA_Period { get { return _MA_Period; } set { _MA_Period = value; } }
        int _MA_Shift = 0;
        public int MA_Shift { get { return _MA_Shift; } set { _MA_Shift = value; } }
        MovingAverageMethod _MA_Method = 0;
        public MovingAverageMethod MA_Method { get { return _MA_Method; } set { _MA_Method = value; } }
        PriceConstant _Applied_Price = 0;
        public PriceConstant Applied_Price { get { return _Applied_Price; } set { _Applied_Price = value; } }
        double _Deviation = 0.1;
        public double Deviation { get { return _Deviation; } set { _Deviation = value; } }
        //---- indicator buffers
        DArr ExtMapBuffer1;
        DArr ExtMapBuffer2;
        //----
        int ExtCountedBars = 0;

        public Envelopes(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.CHART_WINDOW;
            IndicatorBuffers(2);
            SetIndexColor(0, Color.Blue);
            SetIndexColor(1, Color.Red);
        }

        public Envelopes(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            int draw_begin;
            //string short_name;
            //---- drawing settings
            SetIndexStyle(0, DRAW_LINE);
            SetIndexStyle(1, DRAW_LINE);
            SetIndexShift(0, MA_Shift);
            SetIndexShift(1, MA_Shift);
            IndicatorDigits((int)MarketInfo(Symbol, MODE_DIGITS));
            if (MA_Period < 2) MA_Period = 14;
            draw_begin = MA_Period - 1;
            //---- indicator short name
            IndicatorShortName("Env(" + MA_Period + ")");
            SetIndexLabel(0, "Env(" + MA_Period + ")Upper");
            SetIndexLabel(1, "Env(" + MA_Period + ")Lower");
            SetIndexDrawBegin(0, draw_begin);
            SetIndexDrawBegin(1, draw_begin);
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtMapBuffer1);
            SetIndexBuffer(1, ref ExtMapBuffer2);
            if (Deviation < 0.1) Deviation = 0.1;
            if (Deviation > 100.0) Deviation = 100.0;
            //---- initialization done
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Custom indicator iteration function                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int limit;
            if (Bars <= MA_Period) return (0);
            ExtCountedBars = IndicatorCounted;
            //---- check for possible errors
            if (ExtCountedBars < 0) return (-1);
            //---- last counted bar will be recounted
            if (ExtCountedBars > 0) ExtCountedBars--;
            limit = Bars - ExtCountedBars;
            //---- EnvelopesM counted in the buffers
            for (int i = 0; i < limit; i++)
            {
                ExtMapBuffer1[i] = (1 + Deviation / 100) * iMA(null, 0, MA_Period, 0, MA_Method, Applied_Price, i);
                ExtMapBuffer2[i] = (1 - Deviation / 100) * iMA(null, 0, MA_Period, 0, MA_Method, Applied_Price, i);
            }
            //---- done
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
