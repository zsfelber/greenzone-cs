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

    [Indicator("Detrended Price Oscillator", null, "Oscillators")]
    class DPO : ServerIndicatorRuntime
    {
        public DPO(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DodgerBlue))
        {
        }

        public DPO(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //----
        int _x_prd = 14; public int x_prd { get { return _x_prd; } set { _x_prd = value; } }
        //int _CountBars = 300; public int CountBars { get { return _CountBars; } set { _CountBars = value; } }
        //---- buffers
        DArr dpo;
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            string short_name;
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref dpo);
            //---- name for DataWindow and indicator subwindow label
            short_name = "DPO(" + x_prd + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            //----
            //if (CountBars >= Bars)
            //    CountBars = Bars;
            //SetIndexDrawBegin(0, Bars - CountBars + x_prd + 1);
            //----
            return (0);
        }

        public override int Deinit()
        {
            return 0;
        }

        //+------------------------------------------------------------------+
        //| DPO                                                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, counted_bars = IndicatorCounted;
            double t_prd;
            //----
            if (Bars <= x_prd)
                return (0);
            //---- initial zero
            if (counted_bars < x_prd)
            {
                for (i = 1; i <= x_prd; i++)
                    //dpo[CountBars - i] = 0.0;
                    dpo[Bars - i] = 0.0;
            }
            //----
            //i = CountBars - x_prd - 1;
            i = Bars - x_prd - 1;
            t_prd = x_prd / 2 + 1;
            //----
            while (i >= 0)
            {
                dpo[i] = Close[i] - iMA(null, 0, x_prd, (int)t_prd, MODE_SMA, PRICE_CLOSE, i);
                i--;
            }
            return (0);
        }
        //+------------------------------------------------------------------+

    }
}
