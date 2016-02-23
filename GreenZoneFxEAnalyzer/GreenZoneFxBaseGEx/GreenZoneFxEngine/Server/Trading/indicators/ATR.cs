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

    [Indicator(null, "Average True Range", "Oscillators")]
    class ATR : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue

        //---- input parameters
        int _AtrPeriod = 14;
        public int AtrPeriod { get { return _AtrPeriod; } set { _AtrPeriod = value; } }

        //---- buffers
        DArr AtrBuffer;
        DArr TempBuffer;

        public ATR(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DodgerBlue))
        {
            Session.DisplayScale = -1;
        }

        public ATR(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string short_name;
            //---- 1 additional buffer used for counting.
            IndicatorBuffers(2);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref AtrBuffer);
            SetIndexBuffer(1, ref TempBuffer);
            //---- name for DataWindow and indicator subwindow label
            short_name = "ATR(" + AtrPeriod + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            //----
            SetIndexDrawBegin(0, AtrPeriod);
            //----

            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, counted_bars = IndicatorCounted;
            //----
            if (Bars <= AtrPeriod) return (0);
            //---- initial zero
            if (counted_bars < 1)
                for (i = 1; i <= AtrPeriod; i++) AtrBuffer[Bars - i] = 0.0;
            //----
            i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                double high = High[i];
                double low = Low[i];
                if (i == Bars - 1) TempBuffer[i] = high - low;
                else
                {
                    double prevclose = Close[i + 1];
                    TempBuffer[i] = Math.Max(high, prevclose) - Math.Min(low, prevclose);
                }
                i--;
            }
            //----
            if (counted_bars > 0) counted_bars--;
            int limit = Bars - counted_bars;
            for (i = 0; i < limit; i++)
                AtrBuffer[i] = iMAOnArray(TempBuffer, Bars, AtrPeriod, 0, MODE_SMA, i);
            //----
            return 0;
        }
    }
}
