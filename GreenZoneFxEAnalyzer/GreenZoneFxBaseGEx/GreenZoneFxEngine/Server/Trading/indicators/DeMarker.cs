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
    [Indicator(null, null, "Oscillators")]
    class DeMarker : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_maximum 1
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue
        //#property indicator_level1 0.3
        //#property indicator_level2 0.7
        //---- input parameters
        int _DeMarkerPeriod = 14;
        public int DeMarkerPeriod { get { return _DeMarkerPeriod; } set { _DeMarkerPeriod = value; } }

        //---- buffers
        DArr DeMarkerBuffer;
        DArr ExtMaxBuffer;
        DArr ExtMinBuffer;

        public DeMarker(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            IndicatorBuffers(1);
            Session.IndicatorMinimum = 0;
            Session.IndicatorMaximum = 1;
            SetIndexColor(0, Color.DodgerBlue);
            SetIndexStyle(0, DrawingStyle.DRAW_LINE);
            NumIndicatorLevels = 2;
            SetLevelValue(0, 0.3);
            SetLevelValue(1, 0.7);
            Session.DisplayScale = 1;
        }

        public DeMarker(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            string short_name;
            //---- 2 additional buffers are used for counting.
            IndicatorBuffers(3);
            SetIndexBuffer(0, ref DeMarkerBuffer);
            SetIndexBuffer(1, ref ExtMaxBuffer);
            SetIndexBuffer(2, ref ExtMinBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            //---- name for DataWindow and indicator subwindow label
            short_name = "DeM(" + DeMarkerPeriod + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            //---- first values aren't drawn
            SetIndexDrawBegin(0, DeMarkerPeriod);
            //----
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| DeMarker                                                         |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            double dNum;
            int i, nCountedBars;
            //---- insufficient data
            if (Bars <= DeMarkerPeriod) return (0);
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //----
            ExtMaxBuffer[Bars - 1] = 0.0;
            ExtMinBuffer[Bars - 1] = 0.0;
            if (nCountedBars > 2) i = Bars - nCountedBars - 1;
            else i = Bars - 2;
            while (i >= 0)
            {
                dNum = High[i] - High[i + 1];
                if (dNum < 0.0) dNum = 0.0;
                ExtMaxBuffer[i] = dNum;

                dNum = Low[i + 1] - Low[i];
                if (dNum < 0.0) dNum = 0.0;
                ExtMinBuffer[i] = dNum;

                i--;
            }
            //---- initial zero
            if (nCountedBars < 1)
                for (i = 1; i <= DeMarkerPeriod; i++)
                    DeMarkerBuffer[Bars - i] = 0.0;
            //----
            i = Bars - DeMarkerPeriod - 1;
            if (nCountedBars >= DeMarkerPeriod) i = Bars - nCountedBars - 1;
            while (i >= 0)
            {
                dNum = iMAOnArray(ExtMaxBuffer, 0, DeMarkerPeriod, 0, MODE_SMA, i) +
                     iMAOnArray(ExtMinBuffer, 0, DeMarkerPeriod, 0, MODE_SMA, i);
                if (dNum != 0.0)
                    DeMarkerBuffer[i] = iMAOnArray(ExtMaxBuffer, 0, DeMarkerPeriod, 0, MODE_SMA, i) / dNum;
                else
                    DeMarkerBuffer[i] = 0.0;

                i--;
            }
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
