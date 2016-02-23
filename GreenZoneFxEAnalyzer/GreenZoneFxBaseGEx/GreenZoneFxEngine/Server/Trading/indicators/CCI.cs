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

    [Indicator(null, "Commodity Channel Index", "Trends Indicators")]
    class CCI : ServerIndicatorRuntime
    {
        //----
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 LightSeaGreen
        //---- input parameters
        int _CCIPeriod = 14;
        PriceConstant _Applied_Price = PriceConstant.PRICE_CLOSE;

        public int CCIPeriod { get { return _CCIPeriod; } set { _CCIPeriod = value; } }
        public PriceConstant Applied_Price { get { return _Applied_Price; } set { _Applied_Price = value; } }

        //---- buffers
        DArr CCIBuffer;
        DArr RelBuffer;
        DArr DevBuffer;
        DArr MovBuffer;

        DArr seriesArray;

        public CCI(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.LightSeaGreen))
        {
            Session.DisplayScale = 4;
            NumIndicatorLevels = 2;
            SetLevelValue(0, -100);
            SetLevelValue(1, 100);
        }

        public CCI(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            string short_name;
            //---- 3 additional buffers are used for counting.
            IndicatorBuffers(4);
            SetIndexBuffer(1, ref RelBuffer);
            SetIndexBuffer(2, ref DevBuffer);
            SetIndexBuffer(3, ref MovBuffer);
            //---- indicator lines
            SetIndexStyle(0, DRAW_LINE);
            SetIndexBuffer(0, ref CCIBuffer);
            //----
            if (CCIPeriod <= 0)
                CCIPeriod = 14;
            //----
            SetIndexDrawBegin(0, CCIPeriod);

            //---- name for DataWindow and indicator subwindow label
            short_name = "CCI(" + CCIPeriod + ")";
            IndicatorShortName(short_name);
            SetIndexLabel(0, short_name);
            //----
            seriesArray = GetSeriesArray(_Applied_Price);
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, k, counted_bars = IndicatorCounted;
            double price, sum, mul;
            if (CCIPeriod <= 1)
                return (0);
            if (Bars <= CCIPeriod)
                return (0);
            //---- initial zero
            if (counted_bars < 1)
            {
                for (i = 1; i <= CCIPeriod; i++)
                    CCIBuffer[Bars - i] = 0.0;
                for (i = 1; i <= CCIPeriod; i++)
                    DevBuffer[Bars - i] = 0.0;
                for (i = 1; i <= CCIPeriod; i++)
                    MovBuffer[Bars - i] = 0.0;
            }
            //---- last counted bar will be recounted
            int limit = Bars - counted_bars;
            if (counted_bars > 0)
                limit++;
            //---- moving average
            for (i = 0; i < limit; i++)
                MovBuffer[i] = iMA(null, 0, CCIPeriod, 0, MODE_SMA, _Applied_Price, i);
            //---- standard deviations
            i = Bars - CCIPeriod + 1;
            if (counted_bars > CCIPeriod - 1)
                i = Bars - counted_bars - 1;
            mul = 0.015 / CCIPeriod;
            if (i + CCIPeriod - 1 > Bars - 1)//zsf
            {
                i--;
            }
            while (i >= 0)
            {
                sum = 0.0;
                k = i + CCIPeriod - 1;
                while (k >= i)
                {
                    price = seriesArray[k];
                    sum += MathAbs(price - MovBuffer[i]);
                    k--;
                }
                DevBuffer[i] = sum * mul;
                i--;
            }
            i = Bars - CCIPeriod + 1;
            if (counted_bars > CCIPeriod - 1)
                i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                price = seriesArray[i];
                RelBuffer[i] = price - MovBuffer[i];
                i--;
            }
            //---- cci counting
            i = Bars - CCIPeriod + 1;
            if (counted_bars > CCIPeriod - 1)
                i = Bars - counted_bars - 1;
            while (i >= 0)
            {
                if (DevBuffer[i] == 0.0)
                    CCIBuffer[i] = 0.0;
                else
                    CCIBuffer[i] = RelBuffer[i] / DevBuffer[i];
                i--;
            }
            //----
            return 0;
        }
    }
}
