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

    [Indicator(null, null, "Bill Williams")]
    class Alligator : ServerIndicatorRuntime
    {
        //#property indicator_chart_window
        //#property indicator_buffers 3
        //#property indicator_color1 Blue
        //#property indicator_color2 Red
        //#property indicator_color3 Lime

        //---- input parameters
        int _JawsPeriod = 13;
        int _JawsShift = 8;
        int _TeethPeriod = 8;
        int _TeethShift = 5;
        int _LipsPeriod = 5;
        int _LipsShift = 3;
        MovingAverageMethod _MA_method = MovingAverageMethod.MODE_SMA;
        PriceConstant _appliedPrice = PriceConstant.PRICE_CLOSE;

        public int JawsPeriod { get { return _JawsPeriod; } set { _JawsPeriod = value; } }
        public int JawsShift { get { return _JawsShift; } set { _JawsShift = value; } }
        public int TeethPeriod { get { return _TeethPeriod; } set { _TeethPeriod = value; } }
        public int TeethShift { get { return _TeethShift; } set { _TeethShift = value; } }
        public int LipsPeriod { get { return _LipsPeriod; } set { _LipsPeriod = value; } }
        public int LipsShift { get { return _LipsShift; } set { _LipsShift = value; } }
        public MovingAverageMethod MA_method { get { return _MA_method; } set { _MA_method = value; } }
        public PriceConstant appliedPrice { get { return _appliedPrice; } set { _appliedPrice = value; } }

        //---- indicator buffers
        DArr ExtBlueBuffer;
        DArr ExtRedBuffer;
        DArr ExtLimeBuffer;

        public Alligator(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 3,
                new IndicatorBuffer(0,Color.Blue),
                new IndicatorBuffer(1,Color.Red),
                new IndicatorBuffer(2,Color.Lime))
        {
        }

        public Alligator(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- line shifts when drawing
            SetIndexShift(0, JawsShift);
            SetIndexShift(1, TeethShift);
            SetIndexShift(2, LipsShift);
            //---- first positions skipped when drawing
            SetIndexDrawBegin(0, JawsShift + JawsPeriod);
            SetIndexDrawBegin(1, TeethShift + TeethPeriod);
            SetIndexDrawBegin(2, LipsShift + LipsPeriod);
            //---- 3 indicator buffers mapping
            SetIndexBuffer(0, ref ExtBlueBuffer);
            SetIndexBuffer(1, ref ExtRedBuffer);
            SetIndexBuffer(2, ref ExtLimeBuffer);
            //---- drawing settings
            SetIndexStyle(0, DRAW_LINE);
            SetIndexStyle(1, DRAW_LINE);
            SetIndexStyle(2, DRAW_LINE);
            //---- index labels
            SetIndexLabel(0, "Gator Jaws");
            SetIndexLabel(1, "Gator Teeth");
            SetIndexLabel(2, "Gator Lips");
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
            //---- check for possible errors
            if (counted_bars < 0) return (-1);
            //---- last counted bar will be recounted
            if (counted_bars > 0) counted_bars--;
            limit = Bars - counted_bars;
            //---- main loop
            for (int i = 0; i < limit; i++)
            {
                //---- ma_shift set to 0 because SetIndexShift called abowe
                ExtBlueBuffer[i] = iMA(null, 0, JawsPeriod, 0, MODE_SMMA, PRICE_MEDIAN, i);
                ExtRedBuffer[i] = iMA(null, 0, TeethPeriod, 0, MODE_SMMA, PRICE_MEDIAN, i);
                ExtLimeBuffer[i] = iMA(null, 0, LipsPeriod, 0, MODE_SMMA, PRICE_MEDIAN, i);
            }
            //---- done
            return 0;
        }
    }
}
