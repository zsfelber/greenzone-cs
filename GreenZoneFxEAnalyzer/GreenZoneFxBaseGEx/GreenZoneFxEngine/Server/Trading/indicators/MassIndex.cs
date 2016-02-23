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

    [Indicator("Mass Index", null, "Trends Indicators")]
    class MassIndex : ServerIndicatorRuntime
    {
        //#property  indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 Blue
        //#property indicator_level1 27
        //#property indicator_level2 26.5
        //#property indicator_levelcolor Blue
        //---- input parameters
        int _EMAPeriod = 9;
        int _SecondPeriod = 9;
        int _SumPeriod = 25;

        public int EMAPeriod { get { return _EMAPeriod; } set { _EMAPeriod = value; } }
        public int SecondPeriod { get { return _SecondPeriod; } set { _SecondPeriod = value; } }
        public int SumPeriod { get { return _SumPeriod; } set { _SumPeriod = value; } }

        //---- buffers
        DArr MI;
        DArr HL;
        DArr HLaverage;
        DArr EMA2;

        public MassIndex(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.Blue))
        {
            NumIndicatorLevels = 2;
            SetLevelStyle(DrawingStylesWidth1.STYLE_SOLID, DrawingWidth.WIDTH_1, Color.Blue);
            SetLevelValue(0, 27);
            SetLevelValue(1, 26.5);
            Session.DisplayScale = 3;
        }

        public MassIndex(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- indicators
            string name;
            name = "Mass Index(" + EMAPeriod + "," + SecondPeriod + "," + SumPeriod + ")";
            IndicatorBuffers(4);
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref MI);
            SetIndexLabel(0, name);
            SetIndexEmptyValue(0, 0.0);
            SetIndexBuffer(1, ref HL);
            SetIndexEmptyValue(1, 0.0);
            SetIndexBuffer(2, ref HLaverage);
            SetIndexEmptyValue(2, 0.0);
            SetIndexBuffer(3, ref EMA2);
            SetIndexEmptyValue(3, 0.0);
            IndicatorShortName(name);
            IndicatorDigits(2);
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int counted_bars = IndicatorCounted;
            int limit, i;
            //----
            if (counted_bars < 0)
                return (-1);
            if (counted_bars == 0)
            {
                limit = Bars - 1;
                for (i = limit; i >= 0; i--)
                    HL[i] = High[i] - Low[i];
                for (i = limit - EMAPeriod; i >= 0; i--)
                    HLaverage[i] = iMAOnArray(HL, 0, EMAPeriod, 0, MODE_EMA, i);
                for (i = limit - EMAPeriod - SecondPeriod; i >= 0; i--)
                    EMA2[i] = HLaverage[i] / iMAOnArray(HLaverage, 0, SecondPeriod, 0, MODE_EMA, i);
                for (i = limit - EMAPeriod - SecondPeriod - SumPeriod; i >= 0; i--)
                    MI[i] = iMAOnArray(EMA2, 0, SumPeriod, 0, MODE_SMA, i) * SumPeriod;
            }
            if (counted_bars > 0)
            {
                limit = Bars - counted_bars;
                for (i = limit; i >= 0; i--)
                    HL[i] = High[i] - Low[i];
                for (i = limit; i >= 0; i--)
                    HLaverage[i] = iMAOnArray(HL, 0, EMAPeriod, 0, MODE_EMA, i);
                for (i = limit; i >= 0; i--)
                    EMA2[i] = HLaverage[i] / iMAOnArray(HLaverage, 0, SecondPeriod, 0, MODE_EMA, i);
                for (i = limit; i >= 0; i--)
                    MI[i] = iMAOnArray(EMA2, 0, SumPeriod, 0, MODE_SMA, i) * SumPeriod;
            }
            //----
            return 0;
        }
    }
}
