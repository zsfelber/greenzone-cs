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

    [Indicator("Moving Average", null, "Trends Indicators")]
    class MovingAvg : ServerIndicatorRuntime
    {
        int _MA_Period = 13;
        int _MA_Shift = 0;
        MovingAverageMethod _MA_Method = 0;
        PriceConstant _PriceType = 0;
        public int MA_Period { get { return _MA_Period; } set { _MA_Period = value; } }
        public int MA_Shift { get { return _MA_Shift; } set { _MA_Shift = value; } }
        public MovingAverageMethod MA_Method { get { return _MA_Method; } set { _MA_Method = value; } }
        public PriceConstant PriceType { get { return _PriceType; } set { _PriceType = value; } }

        //---- indicator buffers
        DArr ExtMapBuffer;
        //----
        int ExtCountedBars = 0;

        DArr seriesArray;

        public MovingAvg(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 1,
            new IndicatorBuffer(0,Color.Red))
        {
        }

        public MovingAvg(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            int draw_begin;
            string short_name;
            //---- drawing settings
            SetIndexStyle(0, DRAW_LINE);
            SetIndexShift(0, MA_Shift);
            IndicatorDigits((int)MarketInfo(Symbol, MODE_DIGITS));
            if (MA_Period < 2) MA_Period = 13;
            draw_begin = MA_Period - 1;
            //---- indicator short name
            switch (MA_Method)
            {
                case MovingAverageMethod.MODE_SMA:
                    short_name = "SMA(";
                    break;
                case MovingAverageMethod.MODE_EMA:
                    short_name = "EMA(";
                    draw_begin = 0;
                    break;
                case MovingAverageMethod.MODE_SMMA:
                    short_name = "SMMA(";
                    break;
                case MovingAverageMethod.MODE_LWMA:
                    short_name = "LWMA(";
                    break;
                default: throw new NotSupportedException();
            }
            seriesArray = GetSeriesArray(PriceType);

            IndicatorShortName(short_name + MA_Period + ")");
            SetIndexDrawBegin(0, draw_begin);
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtMapBuffer);
            //---- initialization done

            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            if (Bars <= MA_Period) return (0);
            ExtCountedBars = IndicatorCounted;
            //---- check for possible errors
            if (ExtCountedBars < 0) return (-1);
            //---- last counted bar will be recounted
            if (ExtCountedBars > 0) ExtCountedBars--;
            //----
            switch (MA_Method)
            {
                case MovingAverageMethod.MODE_SMA: sma(); break;
                case MovingAverageMethod.MODE_EMA: ema(); break;
                case MovingAverageMethod.MODE_SMMA: smma(); break;
                case MovingAverageMethod.MODE_LWMA: lwma(); break;
            }

            return 0;
        }

        //+------------------------------------------------------------------+
        //| Simple Moving Average                                            |
        //+------------------------------------------------------------------+
        void sma()
        {
            double sum = 0;
            int i, pos = Bars - ExtCountedBars - 1;
            //---- initial accumulation
            if (pos < MA_Period) pos = MA_Period;
            for (i = 1; i < MA_Period; i++, pos--)
                sum += seriesArray[pos];
            //---- main calculation loop
            while (pos >= 0)
            {
                sum += seriesArray[pos];
                ExtMapBuffer[pos] = sum / MA_Period;
                sum -= seriesArray[pos + MA_Period - 1];
                pos--;
            }
            //---- zero initial bars
            if (ExtCountedBars < 1)
                for (i = 1; i < MA_Period; i++) ExtMapBuffer[Bars - i] = 0;
        }
        //+------------------------------------------------------------------+
        //| Exponential Moving Average                                       |
        //+------------------------------------------------------------------+
        void ema()
        {
            double pr = 2.0 / (MA_Period + 1);
            int pos = Bars - 2;
            if (ExtCountedBars > 2) pos = Bars - ExtCountedBars - 1;
            //---- main calculation loop
            while (pos >= 0)
            {
                if (pos == Bars - 2) ExtMapBuffer[pos + 1] = seriesArray[pos + 1];
                ExtMapBuffer[pos] = seriesArray[pos] * pr + ExtMapBuffer[pos + 1] * (1 - pr);
                pos--;
            }
        }
        //+------------------------------------------------------------------+
        //| Smoothed Moving Average                                          |
        //+------------------------------------------------------------------+
        void smma()
        {
            double sum = 0;
            int i, k, pos = Bars - ExtCountedBars + 1;
            //---- main calculation loop
            pos = Bars - MA_Period;
            if (pos > Bars - ExtCountedBars) pos = Bars - ExtCountedBars;
            while (pos >= 0)
            {
                if (pos == Bars - MA_Period)
                {
                    //---- initial accumulation
                    for (i = 0, k = pos; i < MA_Period; i++, k++)
                    {
                        sum += seriesArray[k];
                        //---- zero initial bars
                        ExtMapBuffer[k] = 0;
                    }
                }
                else sum = ExtMapBuffer[pos + 1] * (MA_Period - 1) + seriesArray[pos];
                ExtMapBuffer[pos] = sum / MA_Period;
                pos--;
            }
        }
        //+------------------------------------------------------------------+
        //| Linear Weighted Moving Average                                   |
        //+------------------------------------------------------------------+
        void lwma()
        {
            double sum = 0.0, lsum = 0.0;
            double price;
            int i, weight = 0, pos = Bars - ExtCountedBars - 1;
            //---- initial accumulation
            if (pos < MA_Period) pos = MA_Period;
            for (i = 1; i <= MA_Period; i++, pos--)
            {
                price = seriesArray[pos];
                sum += price * i;
                lsum += price;
                weight += i;
            }
            //---- main calculation loop
            pos++;
            i = pos + MA_Period;
            while (pos >= 0)
            {
                ExtMapBuffer[pos] = sum / weight;
                if (pos == 0) break;
                pos--;
                i--;
                price = seriesArray[pos];
                sum = sum - lsum + price * MA_Period;
                lsum -= seriesArray[i];
                lsum += price;
            }
            //---- zero initial bars
            if (ExtCountedBars < 1)
                for (i = 1; i < MA_Period; i++) ExtMapBuffer[Bars - i] = 0;
        }
        //+------------------------------------------------------------------+
    }
}
