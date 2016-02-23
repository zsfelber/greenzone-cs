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

    [Indicator("Accumulative Swing Index", null, "Trends Indicators")]
    class ASI : ServerIndicatorRuntime
    {
        //----
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DarkBlue        
        public ASI(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DarkBlue))
        {
            Session.DisplayScale = 5;
        }

        public ASI(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //---- input parameters
        double _T = 300.0; public double T { get { return _T; } set { _T = value; } }
        //---- buffers
        DArr ExtMapBuffer1;
        DArr SIBuffer;
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicators
            IndicatorBuffers(2);
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref ExtMapBuffer1);
            SetIndexLabel(0, "Accumulation Swing Index");
            SetIndexBuffer(1, ref SIBuffer);
            SetIndexEmptyValue(0, 0.0);
            SetIndexEmptyValue(1, 0.0);
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
            //----
            int i, limit = 0;
            double R, K, TR, ER = 0, SH, Tpoints;
            if (counted_bars == 0)
                limit = Bars - 1;
            if (counted_bars > 0)
                limit = Bars - counted_bars;
            Tpoints = T * MarketInfo(Symbol, MODE_POINT);
            if (limit + 1 > Bars - 1)//zsf
            {
                limit--;
            }
            for (i = limit; i >= 0; i--)
            {
                TR = iATR(Symbol, 0, 1, i);
                if (Close[i + 1] >= Low[i] && Close[i + 1] <= High[i])
                    ER = 0;
                else
                {
                    if (Close[i + 1] > High[i])
                        ER = MathAbs(High[i] - Close[i + 1]);
                    if (Close[i + 1] < Low[i])
                        ER = MathAbs(Low[i] - Close[i + 1]);
                }
                K = MathMax(MathAbs(High[i] - Close[i + 1]), MathAbs(Low[i] - Close[i + 1]));
                SH = MathAbs(Close[i + 1] - Open[i + 1]);
                R = TR - 0.5 * ER + 0.25 * SH;
                if (R == 0)
                    SIBuffer[i] = 0;
                else
                    SIBuffer[i] = 50 * (Close[i] - Close[i + 1] + 0.5 * (Close[i] - Open[i]) +
                                  0.25 * (Close[i + 1] - Open[i + 1])) * (K / Tpoints) / R;
                ExtMapBuffer1[i] = ExtMapBuffer1[i + 1] + SIBuffer[i];
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+    
    }
}
