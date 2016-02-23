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

    [Indicator("Moving Average of Oscillator", null, "Oscillators")]
    class OsMA : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property  indicator_separate_window
        //#property  indicator_buffers 1
        //#property  indicator_color1  Silver
        //#property  indicator_width1  2
        //---- indicator parameters
        int _FastEMA = 12;
        int _SlowEMA = 26;
        int _SignalSMA = 9;
        PriceConstant _Applied_Price = PriceConstant.PRICE_CLOSE;

        public int FastEMA { get { return _FastEMA; } set { _FastEMA = value; } }
        public int SlowEMA { get { return _SlowEMA; } set { _SlowEMA = value; } }
        public int SignalSMA { get { return _SignalSMA; } set { _SignalSMA = value; } }
        public PriceConstant Applied_Price { get { return _Applied_Price; } set { _Applied_Price = value; } }

        //---- indicator buffers
        DArr OsmaBuffer;
        DArr MacdBuffer;
        DArr SignalBuffer;

        public OsMA(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,(DArr)null, DrawingStyle.DRAW_HISTOGRAM, (DrawingWidth)2, Color.Silver))
        {
        }

        public OsMA(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- 2 additional buffers are used for counting.
            IndicatorBuffers(3);
            //---- drawing settings
            SetIndexStyle(0, DRAW_HISTOGRAM);
            SetIndexDrawBegin(0, SignalSMA);
            IndicatorDigits(Digits + 2);
            //---- 3 indicator buffers mapping
            SetIndexBuffer(0, ref OsmaBuffer);
            SetIndexBuffer(1, ref MacdBuffer);
            SetIndexBuffer(2, ref SignalBuffer);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("OsMA(" + FastEMA + "," + SlowEMA + "," + SignalSMA + ")");
            //---- initialization done
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int limit;
            int counted_bars = IndicatorCounted;
            //---- last counted bar will be recounted
            if (counted_bars > 0) counted_bars--;
            limit = Bars - counted_bars;
            //---- macd counted in the 1-st additional buffer
            int i;
            for (i = 0; i < limit; i++)
                MacdBuffer[i] = iMA(null, 0, FastEMA, 0, MODE_EMA, Applied_Price, i) - iMA(null, 0, SlowEMA, 0, MODE_EMA, Applied_Price, i);
            //---- signal line counted in the 2-nd additional buffer
            for (i = 0; i < limit; i++)
                SignalBuffer[i] = iMAOnArray(MacdBuffer, Bars, SignalSMA, 0, MODE_SMA, i);
            //---- main loop
            for (i = 0; i < limit; i++)
                OsmaBuffer[i] = MacdBuffer[i] - SignalBuffer[i];
            //---- done
            return 0;
        }
    }
}
