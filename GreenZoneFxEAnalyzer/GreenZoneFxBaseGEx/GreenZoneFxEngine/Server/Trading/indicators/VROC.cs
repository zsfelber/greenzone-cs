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

    [Indicator("Volume Rate of Change", null, "Volumes")]
    class VROC : ServerIndicatorRuntime
    {
        public VROC(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.Green))
        {
            Session.DisplayScale = 4;
        }

        public VROC(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //---- input parameters
        int _PeriodROC = 25; public int PeriodROC { get { return _PeriodROC; } set { _PeriodROC = value; } }
        //---- buffers
        DArr ExtMapBuffer1;
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicators
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref ExtMapBuffer1);
            IndicatorShortName("VROC(" + PeriodROC + ")");
            IndicatorDigits(2);
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator deinitialization function                       |
        //+------------------------------------------------------------------+
        public override int Deinit()
        {
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
        //| Custom indicator iteration function                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int counted_bars = IndicatorCounted;
            int i, limit = 0;
            //----
            if (counted_bars == 0)
                limit = Bars - PeriodROC;
            if (counted_bars > 0)
                limit = Bars - counted_bars;
            limit--;
            for (i = limit; i >= 0; i--)
                ExtMapBuffer1[i] = (Volume[i] - Volume[i + PeriodROC]) /
                                   Volume[i + PeriodROC] * 100;
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
