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

    [Indicator(null, null, "Volumes")]
    class Volumes : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property  indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_buffers 3
        //#property indicator_color1  Black
        //#property indicator_color2  Green
        //#property indicator_color3  Red
        //#property indicator_width2  2
        //#property indicator_width3  2
        //---- indicator buffers
        DArr ExtVolumesBuffer;
        DArr ExtVolumesUpBuffer;
        DArr ExtVolumesDownBuffer;


        public Volumes(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            Session.IndicatorMinimum = 0;
            NumIndicatorBuffers = 3;
            SetIndexColor(0, Color.Black);
            SetIndexColor(1, Color.Green);
            SetIndexColor(2, Color.Red);
            SetIndexStyleWidth(1, DrawingWidth.WIDTH_2);
            SetIndexStyleWidth(2, DrawingWidth.WIDTH_2);
            Session.DisplayScale = 5;
        }

        public Volumes(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtVolumesBuffer);
            SetIndexBuffer(1, ref ExtVolumesUpBuffer);
            SetIndexBuffer(2, ref ExtVolumesDownBuffer);
            //---- drawing settings
            SetIndexStyle(0, DRAW_NONE);
            SetIndexStyle(1, DRAW_HISTOGRAM);
            SetIndexStyle(2, DRAW_HISTOGRAM);
            //---- sets default precision format for indicators visualization
            IndicatorDigits(0);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("Volumes");
            SetIndexLabel(0, "Volumes");
            SetIndexLabel(1, null);
            SetIndexLabel(2, null);
            //---- sets drawing line empty value
            SetIndexEmptyValue(1, 0.0);
            SetIndexEmptyValue(2, 0.0);
            //---- initialization done
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Volumes                                                          |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, nLimit, nCountedBars;
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //---- last counted bar will be recounted
            if (nCountedBars > 0) nCountedBars--;
            nLimit = Bars - nCountedBars;
            //----
            for (i = 0; i < nLimit; i++)
            {
                double dVolume = Volume[i];
                if (i == Bars - 1 || dVolume > Volume[i + 1])
                {
                    ExtVolumesBuffer[i] = dVolume;
                    ExtVolumesUpBuffer[i] = dVolume;
                    ExtVolumesDownBuffer[i] = 0.0;
                }
                else
                {
                    ExtVolumesBuffer[i] = dVolume;
                    ExtVolumesUpBuffer[i] = 0.0;
                    ExtVolumesDownBuffer[i] = dVolume;
                }
            }
            //---- done
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
