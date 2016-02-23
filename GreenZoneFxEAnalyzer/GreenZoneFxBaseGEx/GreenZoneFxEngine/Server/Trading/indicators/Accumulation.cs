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

    [Indicator("Accumulation/Distribution", null, "Trends Indicators", "Volumes")]
    class Accumulation : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 LightSeaGreen


        //---- buffers
        DArr ExtMapBuffer1;

        public Accumulation(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.LightSeaGreen))
        {
            Session.DisplayScale = 6;
        }

        public Accumulation(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            IndicatorShortName("A/D");
            //---- indicators
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref ExtMapBuffer1);
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
            i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                double high = High[i];
                double low = Low[i];
                double open = Open[i];
                double close = Close[i];
                ExtMapBuffer1[i] = (close - low) - (high - close);
                if (ExtMapBuffer1[i] != 0)
                {
                    double diff = high - low;
                    if (0 == diff)
                        ExtMapBuffer1[i] = 0;
                    else
                    {
                        ExtMapBuffer1[i] /= diff;
                        ExtMapBuffer1[i] *= Volume[i];
                    }
                }
                if (i < Bars - 1) ExtMapBuffer1[i] += ExtMapBuffer1[i + 1];
                i--;
            }
            return 0;
        }
    }
}
