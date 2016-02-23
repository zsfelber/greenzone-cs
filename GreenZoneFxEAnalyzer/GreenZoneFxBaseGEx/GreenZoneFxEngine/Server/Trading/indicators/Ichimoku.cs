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

    [Indicator("Ichimoku Kinko Hyo", null, "Oscillators")]
    class Ichimoku : ServerIndicatorRuntime
    {
        //#property indicator_chart_window
        //#property indicator_buffers 7
        //#property indicator_color1 Red
        //#property indicator_color2 Blue
        //#property indicator_color3 SandyBrown
        //#property indicator_color4 Thistle
        //#property indicator_color5 Lime
        //#property indicator_color6 SandyBrown
        //#property indicator_color7 Thistle
        //---- input parameters
        int _Tenkan = 9;
        int _Kijun = 26;
        int _Senkou = 52;

        public int Tenkan { get { return _Tenkan; } set { _Tenkan = value; } }
        public int Kijun { get { return _Kijun; } set { _Kijun = value; } }
        public int Senkou { get { return _Senkou; } set { _Senkou = value; } }

        //---- buffers
        DArr Tenkan_Buffer;
        DArr Kijun_Buffer;
        DArr SpanA_Buffer;
        DArr SpanB_Buffer;
        DArr Chinkou_Buffer;
        DArr SpanA2_Buffer;
        DArr SpanB2_Buffer;

        //----
        int a_begin;

        public Ichimoku(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 7,
                new IndicatorBuffer(0,Color.Red),
                new IndicatorBuffer(1,Color.Blue),
                new IndicatorBuffer(2,Color.SandyBrown),
                new IndicatorBuffer(3,Color.DarkOrchid),
                new IndicatorBuffer(4,Color.Lime),
                new IndicatorBuffer(5,Color.SandyBrown),
                new IndicatorBuffer(6,Color.DarkOrchid))
        {
        }

        public Ichimoku(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //----
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref Tenkan_Buffer);
            SetIndexDrawBegin(0, Tenkan - 1);
            SetIndexLabel(0, "Tenkan Sen");
            //----
            SetIndexStyle(1, DRAW_LINE);
            SetIndexBuffer(1, ref Kijun_Buffer);
            SetIndexDrawBegin(1, Kijun - 1);
            SetIndexLabel(1, "Kijun Sen");
            //----
            a_begin = Kijun; if (a_begin < Tenkan) a_begin = Tenkan;
            SetIndexStyle(2, DRAW_HISTOGRAM, STYLE_DOT);
            SetIndexBuffer(2, ref SpanA_Buffer);
            SetIndexDrawBegin(2, Kijun + a_begin - 1);
            SetIndexShift(2, Kijun);
            SetIndexLabel(2, null);
            SetIndexStyle(5, DRAW_LINE, STYLE_DOT);
            SetIndexBuffer(5, ref SpanA2_Buffer);
            SetIndexDrawBegin(5, Kijun + a_begin - 1);
            SetIndexShift(5, Kijun);
            SetIndexLabel(5, "Senkou Span A");
            //----
            SetIndexStyle(3, DRAW_HISTOGRAM, STYLE_DOT);
            SetIndexBuffer(3, ref SpanB_Buffer);
            SetIndexDrawBegin(3, Kijun + Senkou - 1);
            SetIndexShift(3, Kijun);
            SetIndexLabel(3, null);
            SetIndexStyle(6, DRAW_LINE, STYLE_DOT);
            SetIndexBuffer(6, ref SpanB2_Buffer);
            SetIndexDrawBegin(6, Kijun + Senkou - 1);
            SetIndexShift(6, Kijun);
            SetIndexLabel(6, "Senkou Span B");
            //----
            SetIndexStyle(4, DRAW_LINE);
            SetIndexBuffer(4, ref Chinkou_Buffer);
            SetIndexShift(4, -Kijun);
            SetIndexLabel(4, "Chinkou Span");
            //----
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, k;
            int counted_bars = IndicatorCounted;
            double high, low, price;
            //----
            if (Bars <= Tenkan || Bars <= Kijun || Bars <= Senkou) return (0);
            //---- initial zero
            if (counted_bars < 1)
            {
                for (i = 1; i <= Tenkan; i++) Tenkan_Buffer[Bars - i] = 0;
                for (i = 1; i <= Kijun; i++) Kijun_Buffer[Bars - i] = 0;
                for (i = 1; i <= a_begin; i++) { SpanA_Buffer[Bars - i] = 0; SpanA2_Buffer[Bars - i] = 0; }
                for (i = 1; i <= Senkou; i++) { SpanB_Buffer[Bars - i] = 0; SpanB2_Buffer[Bars - i] = 0; }
            }
            //---- Tenkan Sen
            i = Bars - Tenkan;
            if (counted_bars > Tenkan) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                high = High[i]; low = Low[i]; k = i - 1 + Tenkan;
                while (k >= i)
                {
                    price = High[k];
                    if (high < price) high = price;
                    price = Low[k];
                    if (low > price) low = price;
                    k--;
                }
                Tenkan_Buffer[i] = (high + low) / 2;
                i--;
            }
            //---- Kijun Sen
            i = Bars - Kijun;
            if (counted_bars > Kijun) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                high = High[i]; low = Low[i]; k = i - 1 + Kijun;
                while (k >= i)
                {
                    price = High[k];
                    if (high < price) high = price;
                    price = Low[k];
                    if (low > price) low = price;
                    k--;
                }
                Kijun_Buffer[i] = (high + low) / 2;
                i--;
            }
            //---- Senkou Span A
            i = Bars - a_begin + 1;
            if (counted_bars > a_begin - 1) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                price = (Kijun_Buffer[i] + Tenkan_Buffer[i]) / 2;
                SpanA_Buffer[i] = price;
                SpanA2_Buffer[i] = price;
                i--;
            }
            //---- Senkou Span B
            i = Bars - Senkou;
            if (counted_bars > Senkou) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                high = High[i]; low = Low[i]; k = i - 1 + Senkou;
                while (k >= i)
                {
                    price = High[k];
                    if (high < price) high = price;
                    price = Low[k];
                    if (low > price) low = price;
                    k--;
                }
                price = (high + low) / 2;
                SpanB_Buffer[i] = price;
                SpanB2_Buffer[i] = price;
                i--;
            }
            //---- Chinkou Span
            i = Bars - 1;
            if (counted_bars > 1) i = Bars - counted_bars - 1;
            while (i >= 0) { Chinkou_Buffer[i] = Close[i]; i--; }
            //----
            return 0;
        }
    }
}
