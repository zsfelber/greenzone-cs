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

    [Indicator("Relative Vigor Index", null, "Oscillators")]
    class RVI : ServerIndicatorRuntime
    {
        //---- indicator settings
        //#property  indicator_separate_window
        //#property  indicator_buffers 2
        //#property  indicator_color1  Green
        //#property  indicator_color2  Red
        //---- indicator parameters
        int _ExtRVIPeriod = 10;
        public int ExtRVIPeriod { get { return _ExtRVIPeriod; } set { _ExtRVIPeriod = value; } }
        //---- indicator buffers
        DArr ExtRVIBuffer;
        DArr ExtRVISignalBuffer;


        public RVI(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            NumIndicatorBuffers = 2;
            SetIndexColor(0, Color.Green);
            SetIndexColor(1, Color.Red);
            Session.DisplayScale = 3;
        }

        public RVI(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ExtRVIBuffer);
            SetIndexBuffer(1, ref ExtRVISignalBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            SetIndexStyle(1, DRAW_LINE);
            //---- drawing settings   
            SetIndexDrawBegin(0, ExtRVIPeriod + 3);
            SetIndexDrawBegin(1, ExtRVIPeriod + 7);
            //---- name for DataWindow and indicator subwindow label
            IndicatorShortName("RVI(" + ExtRVIPeriod + ")");
            SetIndexLabel(0, "RVI");
            SetIndexLabel(1, "RVIS");
            //---- initialization done
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Relativ Vigor Index                                              |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, j, nLimit, nCountedBars;
            double dValueUp, dValueDown, dNum, dDeNum;
            //----
            if (Bars <= ExtRVIPeriod + 8) return (0);
            //----
            nCountedBars = IndicatorCounted;
            //---- check for possible errors
            if (nCountedBars < 0) return (-1);
            //---- last counted bar will be recounted
            nLimit = Bars - ExtRVIPeriod - 4;
            if (nCountedBars > ExtRVIPeriod + 4)
                nLimit = Bars - nCountedBars;
            //---- RVI counted in the 1-st buffer
            for (i = 0; i <= nLimit; i++)
            {
                dNum = 0.0;
                dDeNum = 0.0;
                for (j = i; j < i + ExtRVIPeriod; j++)
                {
                    dValueUp = ((Close[j] - Open[j]) + 2 * (Close[j + 1] - Open[j + 1]) + 2 * (Close[j + 2] - Open[j + 2]) + (Close[j + 3] - Open[j + 3])) / 6;
                    dValueDown = ((High[j] - Low[j]) + 2 * (High[j + 1] - Low[j + 1]) + 2 * (High[j + 2] - Low[j + 2]) + (High[j + 3] - Low[j + 3])) / 6;
                    dNum += dValueUp;
                    dDeNum += dValueDown;
                }
                if (dDeNum != 0.0)
                    ExtRVIBuffer[i] = dNum / dDeNum;
                else
                    ExtRVIBuffer[i] = dNum;
            }
            //---- signal line counted in the 2-nd buffer
            nLimit = Bars - ExtRVIPeriod - 7;
            if (nCountedBars > ExtRVIPeriod + 8)
                nLimit = Bars - nCountedBars + 1;
            for (i = 0; i <= nLimit; i++)
                ExtRVISignalBuffer[i] = (ExtRVIBuffer[i] + 2 * ExtRVIBuffer[i + 1] + 2 * ExtRVIBuffer[i + 2] + ExtRVIBuffer[i + 3]) / 6;
            //----
            return (0);
        }
        //+------------------------------------------------------------------+   
    }
}
