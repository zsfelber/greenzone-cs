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
    [Indicator("Bears Power", null, "Trends Indicators")]
    class Bears : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 Silver

        //---- input parameters
        int _BearsPeriod = 13;
        PriceConstant _Applied_Price = PriceConstant.PRICE_CLOSE;

        public int BearsPeriod { get { return _BearsPeriod; } set { _BearsPeriod = value; } }
        public PriceConstant Applied_Price { get { return _Applied_Price; } set { _Applied_Price = value; } }

        //---- buffers
        DArr BearsBuffer;
        DArr TempBuffer;

        public Bears(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.Silver))
        {
        }

        public Bears(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string short_name;
            //---- 1 additional buffer used for counting.
            IndicatorBuffers(2);
            IndicatorDigits(Digits);
            //---- indicator line
            SetIndexStyle(0, DRAW_HISTOGRAM);
            SetIndexBuffer(0, ref BearsBuffer);
            SetIndexBuffer(1, ref TempBuffer);
            //---- name for DataWindow and indicator subwindow label
            short_name = "Bears(" + BearsPeriod + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
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
            if (Bars <= BearsPeriod) return (0);
            //----
            int limit = Bars - counted_bars;
            if (counted_bars > 0) limit++;
            for (i = 0; i < limit; i++)
                TempBuffer[i] = iMA(null, 0, BearsPeriod, 0, MODE_EMA, Applied_Price, i);
            //----
            i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                BearsBuffer[i] = Low[i] - TempBuffer[i];
                i--;
            }
            //----
            return 0;
        }
    }
}
