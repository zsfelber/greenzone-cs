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

    [Indicator("Awesome Oscillator", null, "Bill Williams")]
    class Awesome : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property  indicator_separate_window
        //#property  indicator_buffers 3
        //#property  indicator_color1  Black
        //#property  indicator_color2  Green
        //#property  indicator_color3  Red

        //---- indicator buffers
        DArr ExtBuffer0;
        DArr ExtBuffer1;
        DArr ExtBuffer2;

        public Awesome(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 3,
                new IndicatorBuffer(0,Color.Black),
                new IndicatorBuffer(1,Color.Green),
                new IndicatorBuffer(2,Color.Red))
        {
            Session.DisplayScale = 1;
        }

        public Awesome(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- drawing settings
            SetIndexStyle(0, DRAW_NONE);
            SetIndexStyle(1, DRAW_HISTOGRAM);
            SetIndexStyle(2, DRAW_HISTOGRAM);
            IndicatorDigits(Digits + 1);
            SetIndexDrawBegin(0, 34);
            SetIndexDrawBegin(1, 34);
            SetIndexDrawBegin(2, 34);
            //---- 3 indicator buffers mapping
            SetIndexBuffer(0, ref ExtBuffer0);
            SetIndexBuffer(1, ref ExtBuffer1);
            SetIndexBuffer(2, ref ExtBuffer2);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("AO");
            SetIndexLabel(1, null);
            SetIndexLabel(2, null);
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
            double prev, current;
            //---- last counted bar will be recounted
            if (counted_bars > 0) counted_bars--;
            limit = Bars - counted_bars;
            if (limit > Bars - 1)
            {
                limit--;
            }
            //---- macd
            int i;
            for (i = 0; i < limit; i++)
                ExtBuffer0[i] = iMA(null, 0, 5, 0, MODE_SMA, PRICE_MEDIAN, i) - iMA(null, 0, 34, 0, MODE_SMA, PRICE_MEDIAN, i);
            //---- dispatch values between 2 buffers
            bool up = true;
            for (i = limit - 1; i >= 0; i--)
            {
                current = ExtBuffer0[i];
                prev = ExtBuffer0[i + 1];
                if (current > prev) up = true;
                if (current < prev) up = false;
                if (!up)
                {
                    ExtBuffer2[i] = current;
                    ExtBuffer1[i] = 0.0;
                }
                else
                {
                    ExtBuffer1[i] = current;
                    ExtBuffer2[i] = 0.0;
                }
            }
            //---- done
            return 0;
        }
    }
}
