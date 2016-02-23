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

    [Indicator(null, "Market Facilitation Index", "Bill Williams")]
    class MFI_BW : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property  indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_buffers 5
        //#property indicator_color1  Black
        //#property indicator_color2  Lime
        //#property indicator_color3  SaddleBrown
        //#property indicator_color4  Blue
        //#property indicator_color5  Pink
        //#property indicator_width2  2
        //#property indicator_width3  2
        //#property indicator_width4  2
        //#property indicator_width5  2
        //---- indicator buffers
        DArr ExtMFIBuffer;
        DArr ExtMFIUpVUpBuffer;
        DArr ExtMFIDownVDownBuffer;
        DArr ExtMFIUpVDownBuffer;
        DArr ExtMFIDownVUpBuffer;


        public MFI_BW(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            Session.IndicatorMinimum = 0;
            IndicatorBuffers(5);
            SetIndexColor(0, Color.Black);
            SetIndexColor(1, Color.Lime);
            SetIndexColor(2, Color.SaddleBrown);
            SetIndexColor(3, Color.Blue);
            SetIndexColor(4, Color.Pink);
            SetIndexStyleWidth(1, DrawingWidth.WIDTH_2);
            SetIndexStyleWidth(2, DrawingWidth.WIDTH_2);
            SetIndexStyleWidth(3, DrawingWidth.WIDTH_2);
            SetIndexStyleWidth(4, DrawingWidth.WIDTH_2);
            Session.DisplayScale = 1;
        }

        public MFI_BW(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtMFIBuffer);
            SetIndexBuffer(1, ref ExtMFIUpVUpBuffer);
            SetIndexBuffer(2, ref ExtMFIDownVDownBuffer);
            SetIndexBuffer(3, ref ExtMFIUpVDownBuffer);
            SetIndexBuffer(4, ref ExtMFIDownVUpBuffer);
            //---- drawing settings
            SetIndexStyle(0, DRAW_NONE);
            SetIndexStyle(1, DRAW_HISTOGRAM);
            SetIndexStyle(2, DRAW_HISTOGRAM);
            SetIndexStyle(3, DRAW_HISTOGRAM);
            SetIndexStyle(4, DRAW_HISTOGRAM);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("BW MFI");
            SetIndexLabel(0, "BW MFI");
            SetIndexLabel(1, null);
            SetIndexLabel(2, null);
            SetIndexLabel(3, null);
            SetIndexLabel(4, null);
            //---- sets drawing line empty value
            SetIndexEmptyValue(0, 0.0);
            SetIndexEmptyValue(1, 0.0);
            SetIndexEmptyValue(2, 0.0);
            SetIndexEmptyValue(3, 0.0);
            SetIndexEmptyValue(4, 0.0);
            //---- initialization done
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| BW Market Facilitation Index                                     |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, nLimit, nCountedBars;
            bool bMfiUp = true, bVolUp = true;
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //---- last counted bar will be recounted
            if (nCountedBars > 0) nCountedBars--;
            nLimit = Bars - nCountedBars;
            //---- Market Facilitation Index calculation
            for (i = 0; i < nLimit; i++)
            {
                if (CompareDouble(Volume[i], 0.0))
                {
                    Print(Volume[i]);
                    if (i == Bars - 1) ExtMFIBuffer[i] = 0.0;
                    else ExtMFIBuffer[i] = ExtMFIBuffer[i + 1];
                }
                else ExtMFIBuffer[i] = (High[i] - Low[i]) / (Volume[i] * Point);
            }
            //---- upanddown flags setting
            if (nCountedBars > 1)
            {
                //---- analyze previous bar before recounted bar
                i = nLimit + 1;
                if (ExtMFIUpVUpBuffer[i] != 0.0)
                {
                    bMfiUp = true;
                    bVolUp = true;
                }
                if (ExtMFIDownVDownBuffer[i] != 0.0)
                {
                    bMfiUp = false;
                    bVolUp = false;
                }
                if (ExtMFIUpVDownBuffer[i] != 0.0)
                {
                    bMfiUp = true;
                    bVolUp = false;
                }
                if (ExtMFIDownVUpBuffer[i] != 0.0)
                {
                    bMfiUp = false;
                    bVolUp = true;
                }
            }
            //---- dispatch values between 4 buffers
            for (i = nLimit - 1; i >= 0; i--)
            {
                if (i < Bars - 1)
                {
                    if (ExtMFIBuffer[i] > ExtMFIBuffer[i + 1]) bMfiUp = true;
                    if (ExtMFIBuffer[i] < ExtMFIBuffer[i + 1]) bMfiUp = false;
                    if (Volume[i] > Volume[i + 1]) bVolUp = true;
                    if (Volume[i] < Volume[i + 1]) bVolUp = false;
                }
                if (bMfiUp && bVolUp)
                {
                    ExtMFIUpVUpBuffer[i] = ExtMFIBuffer[i];
                    ExtMFIDownVDownBuffer[i] = 0.0;
                    ExtMFIUpVDownBuffer[i] = 0.0;
                    ExtMFIDownVUpBuffer[i] = 0.0;
                    continue;
                }
                if (!bMfiUp && !bVolUp)
                {
                    ExtMFIUpVUpBuffer[i] = 0.0;
                    ExtMFIDownVDownBuffer[i] = ExtMFIBuffer[i];
                    ExtMFIUpVDownBuffer[i] = 0.0;
                    ExtMFIDownVUpBuffer[i] = 0.0;
                    continue;
                }
                if (bMfiUp && !bVolUp)
                {
                    ExtMFIUpVUpBuffer[i] = 0.0;
                    ExtMFIDownVDownBuffer[i] = 0.0;
                    ExtMFIUpVDownBuffer[i] = ExtMFIBuffer[i];
                    ExtMFIDownVUpBuffer[i] = 0.0;
                    continue;
                }
                if (!bMfiUp && bVolUp)
                {
                    ExtMFIUpVUpBuffer[i] = 0.0;
                    ExtMFIDownVDownBuffer[i] = 0.0;
                    ExtMFIUpVDownBuffer[i] = 0.0;
                    ExtMFIDownVUpBuffer[i] = ExtMFIBuffer[i];
                    continue;
                }
            }
            //---- done
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
