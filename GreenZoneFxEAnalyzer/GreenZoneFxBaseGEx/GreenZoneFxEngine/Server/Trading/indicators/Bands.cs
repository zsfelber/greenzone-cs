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
    [Indicator("Bollinger Bands", null, "Trends Indicators")]
    class Bands : ServerIndicatorRuntime
    {
        //---- indicator parameters
        int _BandsPeriod = 20;
        int _BandsShift = 0;
        double _BandsDeviations = 2.0;
        PriceConstant _AppliedPrice = PriceConstant.PRICE_CLOSE;
        MovingAverageMethod _MA_Method = 0;
        //---- buffers
        DArr MovingBuffer;
        DArr UpperBuffer;
        DArr LowerBuffer;

        public int BandsPeriod { get { return _BandsPeriod; } set { _BandsPeriod = value; } }
        public int BandsShift { get { return _BandsShift; } set { _BandsShift = value; } }
        public double BandsDeviations { get { return _BandsDeviations; } set { _BandsDeviations = value; } }
        public PriceConstant AppliedPrice { get { return _AppliedPrice; } set { _AppliedPrice = value; } }
        public MovingAverageMethod MA_Method { get { return _MA_Method; } set { _MA_Method = value; } }

        public Bands(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 3,
                new IndicatorBuffer(0,Color.LightSeaGreen),
                new IndicatorBuffer(1,Color.LightSeaGreen),
                new IndicatorBuffer(2,Color.LightSeaGreen))
        {
        }

        public Bands(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref MovingBuffer);
            SetIndexStyle(1, DRAW_LINE);
            SetIndexBuffer(1, ref UpperBuffer);
            SetIndexStyle(2, DRAW_LINE);
            SetIndexBuffer(2, ref LowerBuffer);
            //----
            SetIndexDrawBegin(0, BandsPeriod + BandsShift);
            SetIndexDrawBegin(1, BandsPeriod + BandsShift);
            SetIndexDrawBegin(2, BandsPeriod + BandsShift);

            Session.ShortName = _BandsPeriod + "," + _BandsShift + "," + _BandsDeviations.ToString("#.##");

            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, k, counted_bars = IndicatorCounted;
            double deviation;
            double sum, oldval, newres;
            //----
            if (Bars <= BandsPeriod) return (0);
            //---- initial zero
            if (counted_bars < 1)
                for (i = 1; i <= BandsPeriod; i++)
                {
                    MovingBuffer[Bars - i] = EMPTY_VALUE;
                    UpperBuffer[Bars - i] = EMPTY_VALUE;
                    LowerBuffer[Bars - i] = EMPTY_VALUE;
                }
            //----
            int limit = Bars - counted_bars;
            if (counted_bars > 0) limit++;
            for (i = 0; i < limit; i++)
                MovingBuffer[i] = iMA(null, 0, BandsPeriod, BandsShift, _MA_Method, _AppliedPrice, i);
            //----
            i = Bars - BandsPeriod + 1;
            if (counted_bars > BandsPeriod - 1) i = Bars - counted_bars - 1;
            if (i + BandsPeriod - 1 > Bars - 1) //zsf
            {
                i--;
            }
            while (i >= 0)
            {
                sum = 0.0;
                k = i + BandsPeriod - 1;
                oldval = MovingBuffer[i];
                while (k >= i)
                {
                    newres = Close[k] - oldval;
                    sum += newres * newres;
                    k--;
                }
                deviation = BandsDeviations * Math.Sqrt(sum / BandsPeriod);
                UpperBuffer[i] = oldval + deviation;
                LowerBuffer[i] = oldval - deviation;
                i--;
            }

            return 0;
        }
    }
}
