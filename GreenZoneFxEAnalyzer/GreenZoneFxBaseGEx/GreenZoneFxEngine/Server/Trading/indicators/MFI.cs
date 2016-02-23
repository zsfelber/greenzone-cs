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

    [Indicator("Money Flow Index",null,"Volumes")]
    class MFI : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_minimum 0
        //#property indicator_maximum 100
        //#property indicator_level1 20
        //#property indicator_level2 80
        //#property indicator_buffers 1
        //#property indicator_color1 Blue
        //---- input parameters
        int _ExtMFIPeriod = 14;
        public int ExtMFIPeriod { get { return _ExtMFIPeriod; } set { _ExtMFIPeriod = value; } }
        //---- buffers
        DArr ExtMFIBuffer;

        public MFI(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            Session.IndicatorMinimum = 0;
            Session.IndicatorMaximum = 100;
            NumIndicatorLevels = 2;
            SetLevelValue(0, 20);
            SetLevelValue(1, 80);
            NumIndicatorBuffers = 1;
            SetIndexColor(0, Color.Blue);
            SetIndexStyle(0, DrawingStyle.DRAW_LINE);
            Session.DisplayScale = 3;
        }

        public MFI(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            string sShortName;
            //----
            SetIndexBuffer(0, ref ExtMFIBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            //---- name for DataWindow and indicator subwindow label
            sShortName = "MFI(" + ExtMFIPeriod + ")";
            IndicatorShortName(sShortName);
            SetIndexLabel(0, sShortName);
            //---- first values aren't drawn
            SetIndexDrawBegin(0, ExtMFIPeriod);
            //----
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Money Flow Index                                                 |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int i, j, nCountedBars;
            double dPositiveMF, dNegativeMF, dCurrentTP, dPreviousTP;
            //---- insufficient data
            if (Bars <= ExtMFIPeriod) return (0);
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //----
            i = Bars - ExtMFIPeriod - 1;
            if (nCountedBars > ExtMFIPeriod)
                i = Bars - nCountedBars - 1;
            while (i >= 0)
            {
                dPositiveMF = 0.0;
                dNegativeMF = 0.0;
                dCurrentTP = (High[i] + Low[i] + Close[i]) / 3;
                for (j = 0; j < ExtMFIPeriod; j++)
                {
                    dPreviousTP = (High[i + j + 1] + Low[i + j + 1] + Close[i + j + 1]) / 3;
                    if (dCurrentTP > dPreviousTP)
                        dPositiveMF += Volume[i + j] * dCurrentTP;
                    else
                    {
                        if (dCurrentTP < dPreviousTP)
                            dNegativeMF += Volume[i + j] * dCurrentTP;
                    }
                    dCurrentTP = dPreviousTP;
                }
                //----
                if (dNegativeMF != 0.0)
                    ExtMFIBuffer[i] = 100 - 100 / (1 + dPositiveMF / dNegativeMF);
                else
                    ExtMFIBuffer[i] = 100;
                //----
                i--;
            }
            //----
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
