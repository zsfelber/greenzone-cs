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

    [Indicator(null, "Moving Averages Convergence/Divergence", "Oscillators")]
    class MACD : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property  indicator_separate_window
        //#property  indicator_buffers 2
        //#property  indicator_color1  Silver
        //#property  indicator_color2  Red
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
        DArr MacdBuffer;
        DArr SignalBuffer;

        public MACD(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 2,
                new IndicatorBuffer(0,(DArr)null, DrawingStyle.DRAW_HISTOGRAM, (DrawingWidth)2, Color.Silver),
                new IndicatorBuffer(1,Color.Red))
        {
        }

        public MACD(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- drawing settings
            SetIndexStyle(0, DRAW_HISTOGRAM);
            SetIndexStyle(1, DRAW_LINE);
            SetIndexDrawBegin(1, SignalSMA);
            IndicatorDigits(Digits + 1);
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref MacdBuffer);
            SetIndexBuffer(1, ref SignalBuffer);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("MACD(" + FastEMA + "," + SlowEMA + "," + SignalSMA + ")");
            SetIndexLabel(0, "MACD");
            SetIndexLabel(1, "Signal");
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
            //---- macd counted in the 1-st buffer
            int i;
            for (i = 0; i < limit; i++)
                MacdBuffer[i] = iMA(null, 0, FastEMA, 0, MODE_EMA, Applied_Price, i) - iMA(null, 0, SlowEMA, 0, MODE_EMA, Applied_Price, i);

            //---- signal line counted in the 2-nd buffer
            for (i = 0; i < limit; i++)
                SignalBuffer[i] = iMAOnArray(MacdBuffer, Math.Min(Bars, limit + SignalSMA), SignalSMA, 0, MODE_SMA, i);
            // <- zsf  SignalBuffer[i]=iMAOnArray(MacdBuffer,Bars,SignalSMA,0,MODE_SMA,i);

            //---- done
            return 0;
        }
    }
}
