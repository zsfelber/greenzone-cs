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

    [Indicator("Heiken Ashi")]
    class HeikenAshi : ServerIndicatorRuntime
    {
        //#property indicator_chart_window
        //#property indicator_buffers 4
        //#property indicator_color1 Red
        //#property indicator_color2 White
        //#property indicator_color3 Red
        //#property indicator_color4 White
        //#property indicator_width1 1
        //#property indicator_width2 1
        //#property indicator_width3 3
        //#property indicator_width4 3

        //----
        Color color1 = Color.Red;
        Color color2 = Color.Silver;
        Color color3 = Color.Red;
        Color color4 = Color.Silver;

        public Color Color1 { get { return color1; } set { color1 = value; } }
        public Color Color2 { get { return color2; } set { color2 = value; } }
        public Color Color3 { get { return color3; } set { color3 = value; } }
        public Color Color4 { get { return color4; } set { color4 = value; } }

        //---- buffers
        DArr ExtMapBuffer1;
        DArr ExtMapBuffer2;
        DArr ExtMapBuffer3;
        DArr ExtMapBuffer4;

        //----
        int ExtCountedBars = 0;

        public HeikenAshi(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 4,
                new IndicatorBuffer(0,(DArr)null, DrawingStyle.DRAW_HISTOGRAM, (DrawingWidth)1, Color.Red),
                new IndicatorBuffer(1,(DArr)null, DrawingStyle.DRAW_HISTOGRAM, (DrawingWidth)1, Color.Silver),
                new IndicatorBuffer(2,(DArr)null, DrawingStyle.DRAW_HISTOGRAM, (DrawingWidth)3, Color.Red),
                new IndicatorBuffer(3,(DArr)null, DrawingStyle.DRAW_HISTOGRAM, (DrawingWidth)3, Color.Silver))
        {
        }

        public HeikenAshi(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- indicators
            SetIndexStyle(0, DRAW_HISTOGRAM, 0, (DrawingWidth)1, color1);
            SetIndexBuffer(0, ref ExtMapBuffer1);
            SetIndexStyle(1, DRAW_HISTOGRAM, 0, (DrawingWidth)1, color2);
            SetIndexBuffer(1, ref ExtMapBuffer2);
            SetIndexStyle(2, DRAW_HISTOGRAM, 0, (DrawingWidth)3, color3);
            SetIndexBuffer(2, ref ExtMapBuffer3);
            SetIndexStyle(3, DRAW_HISTOGRAM, 0, (DrawingWidth)3, color4);
            SetIndexBuffer(3, ref ExtMapBuffer4);
            //----
            SetIndexDrawBegin(0, 10);
            SetIndexDrawBegin(1, 10);
            SetIndexDrawBegin(2, 10);
            SetIndexDrawBegin(3, 10);
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtMapBuffer1);
            SetIndexBuffer(1, ref ExtMapBuffer2);
            SetIndexBuffer(2, ref ExtMapBuffer3);
            SetIndexBuffer(3, ref ExtMapBuffer4);
            //---- initialization done
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            double haOpen, haHigh, haLow, haClose;
            if (Bars <= 10) return (0);
            ExtCountedBars = IndicatorCounted;
            //---- check for possible errors
            if (ExtCountedBars < 0) return (-1);
            //---- last counted bar will be recounted
            if (ExtCountedBars > 0) ExtCountedBars--;
            int pos = Bars - ExtCountedBars - 1;
            if (pos + 1 > Bars - 1)//zsf
            {
                pos = Bars - 2;
            }
            while (pos >= 0)
            {
                haOpen = (ExtMapBuffer3[pos + 1] + ExtMapBuffer4[pos + 1]) / 2;
                haClose = (Open[pos] + High[pos] + Low[pos] + Close[pos]) / 4;
                haHigh = Math.Max(High[pos], Math.Max(haOpen, haClose));
                haLow = Math.Min(Low[pos], Math.Min(haOpen, haClose));
                if (haOpen < haClose)
                {
                    ExtMapBuffer1[pos] = haLow;
                    ExtMapBuffer2[pos] = haHigh;
                }
                else
                {
                    ExtMapBuffer1[pos] = haHigh;
                    ExtMapBuffer2[pos] = haLow;
                }
                ExtMapBuffer3[pos] = haOpen;
                ExtMapBuffer4[pos] = haClose;
                pos--;
            }
            //----
            return 0;
        }
    }
}
