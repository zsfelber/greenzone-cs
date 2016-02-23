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

    [Indicator("Force Index", null, "Oscillators")]
    class ForceIndex : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue
        //---- input parameters
        int _ExtForcePeriod = 13;
        public int ExtForcePeriod { get { return _ExtForcePeriod; } set { _ExtForcePeriod = value; } }
        MovingAverageMethod _ExtForceMAMethod = 0;
        public MovingAverageMethod ExtForceMAMethod { get { return _ExtForceMAMethod; } set { _ExtForceMAMethod = value; } }
        PriceConstant _ExtForceAppliedPrice = 0;
        public PriceConstant ExtForceAppliedPrice { get { return _ExtForceAppliedPrice; } set { _ExtForceAppliedPrice = value; } }
        //---- buffers
        DArr ExtForceBuffer;

        public ForceIndex(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache)
        {
            Session.WindowType = IndicatorWindowType.SEPARATE_WINDOW;
            IndicatorBuffers(1);
            SetIndexColor(0, Color.DodgerBlue);
            Session.DisplayScale = 2;
        }

        public ForceIndex(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }
        //+------------------------------------------------------------------+
        //| Custom indicator initialization function                         |
        //+------------------------------------------------------------------+
        public override int Init()
        {
            string sShortName;
            SetIndexBuffer(0, ref ExtForceBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            //---- name for DataWindow and indicator subwindow label
            sShortName = "Force(" + ExtForcePeriod + ")";
            IndicatorShortName(sShortName);
            SetIndexLabel(0, sShortName);
            //---- first values aren't drawn
            SetIndexDrawBegin(0, ExtForcePeriod);
            //----
            return (0);
        }

        public override int Deinit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Force Index indicator                                            |
        //+------------------------------------------------------------------+
        public override int OnTick()
        {
            int nLimit;
            int nCountedBars = IndicatorCounted;
            //---- insufficient data
            if (Bars <= ExtForcePeriod) return (0);
            //---- last counted bar will be recounted
            if (nCountedBars > ExtForcePeriod) nCountedBars--;
            nLimit = Bars - nCountedBars;
            if (nLimit > Bars - 1)//zsf
            {
                nLimit--;
            }
            //---- Force Index counted
            for (int i = 0; i < nLimit; i++)
                ExtForceBuffer[i] = Volume[i] *
                                  (iMA(null, 0, ExtForcePeriod, 0, ExtForceMAMethod, ExtForceAppliedPrice, i) -
                                   iMA(null, 0, ExtForcePeriod, 0, ExtForceMAMethod, ExtForceAppliedPrice, i + 1));
            //---- done
            return (0);
        }
        //+------------------------------------------------------------------+
    }
}
