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

    [Indicator("Stochastic Oscillator", null, "Oscillators")]
    class Stochastic : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_maximum 100
        //#property indicator_buffers 2
        //#property indicator_color1 LightSeaGreen
        //#property indicator_color2 Red
        //---- input parameters
        int _KPeriod = 5;
        int _DPeriod = 3;
        int _Slowing = 3;

        public int KPeriod { get { return _KPeriod; } set { _KPeriod = value; } }
        public int DPeriod { get { return _DPeriod; } set { _DPeriod = value; } }
        public int Slowing { get { return _Slowing; } set { _Slowing = value; } }

        //---- buffers
        DArr MainBuffer;
        DArr SignalBuffer;
        DArr HighesBuffer;
        DArr LowesBuffer;
        //----
        int draw_begin1 = 0;
        int draw_begin2 = 0;


        public Stochastic(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 2, 0, 100,
                new IndicatorBuffer(0,Color.LightSeaGreen),
                new IndicatorBuffer(1,Color.Red))
        {
            Session.DisplayScale = 3;
            NumIndicatorLevels = 2;
            SetLevelValue(0, 20);
            SetLevelValue(1, 80);
        }

        public Stochastic(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string short_name;
            //---- 2 additional buffers are used for counting.
            IndicatorBuffers(4);
            SetIndexBuffer(2, ref HighesBuffer);
            SetIndexBuffer(3, ref LowesBuffer);
            //---- indicator lines
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref MainBuffer);
            SetIndexStyle(1, DRAW_LINE);
            SetIndexBuffer(1, ref SignalBuffer);
            //---- name for DataWindow and indicator subwindow label
            short_name = "Sto(" + KPeriod + "," + DPeriod + "," + Slowing + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            SetIndexLabel(1, "Signal");
            //----
            draw_begin1 = KPeriod + Slowing;
            draw_begin2 = draw_begin1 + DPeriod;
            SetIndexDrawBegin(0, draw_begin1);
            SetIndexDrawBegin(1, draw_begin2);
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
            double price;
            //----
            if (Bars <= draw_begin2) return (0);
            //---- initial zero
            if (counted_bars < 1)
            {
                for (i = 1; i <= draw_begin1; i++) MainBuffer[Bars - i] = 0;
                for (i = 1; i <= draw_begin2; i++) SignalBuffer[Bars - i] = 0;
            }
            //---- minimums counting
            i = Bars - KPeriod;
            if (counted_bars > KPeriod) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                double min = 1000000;
                k = i + KPeriod - 1;
                while (k >= i)
                {
                    price = Low[k];
                    if (min > price) min = price;
                    k--;
                }
                LowesBuffer[i] = min;
                i--;
            }
            //---- maximums counting
            i = Bars - KPeriod;
            if (counted_bars > KPeriod) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                double max = -1000000;
                k = i + KPeriod - 1;
                while (k >= i)
                {
                    price = High[k];
                    if (max < price) max = price;
                    k--;
                }
                HighesBuffer[i] = max;
                i--;
            }
            //---- %K line
            i = Bars - draw_begin1;
            if (counted_bars > draw_begin1) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                double sumlow = 0.0;
                double sumhigh = 0.0;
                for (k = (i + Slowing - 1); k >= i; k--)
                {
                    sumlow += Close[k] - LowesBuffer[k];
                    sumhigh += HighesBuffer[k] - LowesBuffer[k];
                }
                if (sumhigh == 0.0) MainBuffer[i] = 100.0;
                else MainBuffer[i] = sumlow / sumhigh * 100;
                i--;
            }
            //---- last counted bar will be recounted
            if (counted_bars > 0) counted_bars--;
            int limit = Bars - counted_bars;
            //---- signal line is simple movimg average
            for (i = 0; i < limit; i++)
                SignalBuffer[i] = iMAOnArray(MainBuffer, Bars, DPeriod, 0, MODE_SMA, i);
            //----
            return 0;
        }
    }
}
