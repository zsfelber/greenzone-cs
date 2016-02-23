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

    [Indicator("Price and Volume Trend", null, "Volumes")]
    class PVT : ServerIndicatorRuntime
    {
        //----
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue
        //---- input parameters
        PriceConstant _ExtPVTAppliedPrice = 0;

        public PriceConstant ExtPVTAppliedPrice { get { return _ExtPVTAppliedPrice; } set { _ExtPVTAppliedPrice = value; } }

        //---- buffers
        DArr ExtPVTBuffer;

        public PVT(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DodgerBlue))
        {
            Session.DisplayScale = 3;
        }

        public PVT(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string sShortName;
            //---- indicator buffer mapping
            SetIndexBuffer(0, ref ExtPVTBuffer);
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            //---- sets default precision format for indicators visualization
            IndicatorDigits(0);
            //---- name for DataWindow and indicator subwindow label
            sShortName = "OBV";
            IndicatorShortName(sShortName);
            //----
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, nLimit, nCountedBars;
            double dCurrentPrice, dPreviousPrice;
            //---- bars count that does not changed after last indicator launch.
            nCountedBars = IndicatorCounted;
            //---- last counted bar will be recounted
            if (nCountedBars > 0)
                nCountedBars--;
            nLimit = Bars - nCountedBars - 1;
            //---- 
            for (i = nLimit; i >= 0; i--)
            {
                if (i == Bars - 1)
                    ExtPVTBuffer[i] = Volume[i];
                else
                {
                    dCurrentPrice = GetAppliedPrice(ExtPVTAppliedPrice, i);
                    dPreviousPrice = GetAppliedPrice(ExtPVTAppliedPrice, i + 1);
                    ExtPVTBuffer[i] = ExtPVTBuffer[i + 1] + Volume[i] * (dCurrentPrice -
                                        dPreviousPrice) / dPreviousPrice;
                }
            }
            //----
            return 0;
        }
        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        double GetAppliedPrice(PriceConstant nAppliedPrice, int nIndex)
        {
            double dPrice;
            //----
            switch (nAppliedPrice)
            {
                case (PriceConstant)0: dPrice = Close[nIndex]; break;
                case (PriceConstant)1: dPrice = Open[nIndex]; break;
                case (PriceConstant)2: dPrice = High[nIndex]; break;
                case (PriceConstant)3: dPrice = Low[nIndex]; break;
                case (PriceConstant)4: dPrice = (High[nIndex] + Low[nIndex]) / 2.0; break;
                case (PriceConstant)5: dPrice = (High[nIndex] + Low[nIndex] + Close[nIndex]) / 3.0; break;
                case (PriceConstant)6: dPrice = (High[nIndex] + Low[nIndex] + 2 * Close[nIndex]) / 4.0; break;
                default: dPrice = 0.0; break;
            }
            //----
            return (dPrice);
        }
    }
}
