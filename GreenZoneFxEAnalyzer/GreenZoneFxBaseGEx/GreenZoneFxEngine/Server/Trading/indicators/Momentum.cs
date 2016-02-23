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
    class Momentum : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DodgerBlue
        //---- input parameters
        int _MomPeriod = 14;
        PriceConstant _Applied_Price = PriceConstant.PRICE_CLOSE;

        public int MomPeriod { get { return _MomPeriod; } set { _MomPeriod = value; } }
        public PriceConstant Applied_Price { get { return _Applied_Price; } set { _Applied_Price = value; } }

        //---- buffers
        DArr MomBuffer;

        DArr seriesArray;

        public Momentum(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DodgerBlue))
        {
            Session.DisplayScale = 2;
        }

        public Momentum(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string short_name;
            //---- indicator line
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref MomBuffer);
            //---- name for DataWindow and indicator subwindow label
            short_name = "Mom(" + MomPeriod + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            //----
            SetIndexDrawBegin(0, MomPeriod);
            //----
            seriesArray = GetSeriesArray(Applied_Price);
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
            if (Bars <= MomPeriod) return (0);
            //---- initial zero
            if (counted_bars < 1)
                for (i = 1; i <= MomPeriod; i++) MomBuffer[Bars - i] = 0.0;
            //----
            i = Bars - MomPeriod - 1;
            if (counted_bars >= MomPeriod) i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                MomBuffer[i] = seriesArray[i] * 100 / seriesArray[i + MomPeriod];
                i--;
            }
            return 0;
        }
    }
}
