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

    [Indicator(null, null, "Trends Indicators")]
    class ZigZag : ServerIndicatorRuntime
    {
        //#property indicator_chart_window
        //#property indicator_buffers 1
        //#property indicator_color1 Red
        //---- indicator parameters
        int _ExtDepth = 12;
        int _ExtDeviation = 5;
        int _ExtBackstep = 3;

        public int ExtDepth { get { return _ExtDepth; } set { _ExtDepth = value; } }
        public int ExtDeviation { get { return _ExtDeviation; } set { _ExtDeviation = value; } }
        public int ExtBackstep { get { return _ExtBackstep; } set { _ExtBackstep = value; } }

        //---- indicator buffers
        DArr ZigzagBuffer;
        DArr HighMapBuffer;
        DArr LowMapBuffer;
        int level = 3; // recounting's depth 
        bool downloadhistory = false;

        public ZigZag(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.CHART_WINDOW, 1,
                new IndicatorBuffer(0,Color.Red))
        {
        }

        public ZigZag(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            IndicatorBuffers(3);
            //---- drawing settings
            SetIndexStyle(0, DRAW_SECTION);
            //---- indicator buffers mapping
            SetIndexBuffer(0, ref ZigzagBuffer);
            SetIndexBuffer(1, ref HighMapBuffer);
            SetIndexBuffer(2, ref LowMapBuffer);
            SetIndexEmptyValue(0, 0.0);

            //---- indicator short name
            IndicatorShortName("ZigZag(" + ExtDepth + "," + ExtDeviation + "," + ExtBackstep + ")");
            //---- initialization done
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i = 0, counted_bars = IndicatorCounted;
            int limit = 0, counterZ = 0, whatlookfor = 0;
            int shift, back, lasthighpos = 0, lastlowpos = 0;
            double val, res;
            int ival;
            double curlow = 0, curhigh = 0, lasthigh = 0, lastlow = 0;

            if (counted_bars == 0 && downloadhistory) // history was downloaded
            {
                ArrayInitialize(ZigzagBuffer, 0.0);
                ArrayInitialize(HighMapBuffer, 0.0);
                ArrayInitialize(LowMapBuffer, 0.0);
            }
            if (counted_bars == 0)
            {
                limit = Bars - ExtDepth;
                downloadhistory = true;
            }
            if (counted_bars > 0)
            {
                while (counterZ < level && i < 100)
                {
                    res = ZigzagBuffer[i];
                    if (res != 0) counterZ++;
                    i++;
                }
                i--;
                limit = i;
                if (LowMapBuffer[i] != 0)
                {
                    curlow = LowMapBuffer[i];
                    whatlookfor = 1;
                }
                else
                {
                    curhigh = HighMapBuffer[i];
                    whatlookfor = -1;
                }
                for (i = limit - 1; i >= 0; i--)
                {
                    ZigzagBuffer[i] = 0.0;
                    LowMapBuffer[i] = 0.0;
                    HighMapBuffer[i] = 0.0;
                }
            }

            for (shift = limit; shift >= 0; shift--)
            {
                ival = iLowest(null, 0, MODE_LOW, ExtDepth, shift);
                val = Low[ival];
                if (val == lastlow) val = 0.0;
                else
                {
                    lastlow = val;
                    if ((Low[shift] - val) > (ExtDeviation * Point)) val = 0.0;
                    else
                    {
                        for (back = 1; back <= ExtBackstep; back++)
                        {
                            res = LowMapBuffer[shift + back];
                            if ((res != 0) && (res > val)) LowMapBuffer[shift + back] = 0.0;
                        }
                    }
                }
                //if (Low[shift]==val) // zsf ->
                if (shift == ival)
                    LowMapBuffer[shift] = val;
                else
                    LowMapBuffer[shift] = 0.0;
                //--- high
                ival = iHighest(null, 0, MODE_HIGH, ExtDepth, shift);
                val = High[ival];
                if (val == lasthigh) val = 0.0;
                else
                {
                    lasthigh = val;
                    if ((val - High[shift]) > (ExtDeviation * Point)) val = 0.0;
                    else
                    {
                        for (back = 1; back <= ExtBackstep; back++)
                        {
                            res = HighMapBuffer[shift + back];
                            if ((res != 0) && (res < val)) HighMapBuffer[shift + back] = 0.0;
                        }
                    }
                }
                // if (High[shift]==val) // zsf ->
                if (shift == ival)
                    HighMapBuffer[shift] = val;
                else
                    HighMapBuffer[shift] = 0.0;
            }

            // final cutting 
            if (whatlookfor == 0)
            {
                lastlow = 0;
                lasthigh = 0;
            }
            else
            {
                lastlow = curlow;
                lasthigh = curhigh;
            }

            for (shift = limit; shift >= 0; shift--)
            {
                res = 0.0;
                switch (whatlookfor)
                {
                    case 0: // look for peak or lawn 
                        if (lastlow == 0 && lasthigh == 0)
                        {
                            if (HighMapBuffer[shift] != 0)
                            {
                                lasthigh = High[shift];
                                lasthighpos = shift;
                                whatlookfor = -1;
                                ZigzagBuffer[shift] = lasthigh;
                                res = 1;
                            }
                            if (LowMapBuffer[shift] != 0)
                            {
                                lastlow = Low[shift];
                                lastlowpos = shift;
                                whatlookfor = 1;
                                ZigzagBuffer[shift] = lastlow;
                                res = 1;
                            }
                        }
                        break;
                    case 1: // look for peak
                        if (LowMapBuffer[shift] != 0.0 && LowMapBuffer[shift] < lastlow && HighMapBuffer[shift] == 0.0)
                        {
                            ZigzagBuffer[lastlowpos] = 0.0;
                            lastlowpos = shift;
                            lastlow = LowMapBuffer[shift];
                            ZigzagBuffer[shift] = lastlow;
                            res = 1;
                        }
                        if (HighMapBuffer[shift] != 0.0 && LowMapBuffer[shift] == 0.0)
                        {
                            lasthigh = HighMapBuffer[shift];
                            lasthighpos = shift;
                            ZigzagBuffer[shift] = lasthigh;
                            whatlookfor = -1;
                            res = 1;
                        }
                        break;
                    case -1: // look for lawn
                        if (HighMapBuffer[shift] != 0.0 && HighMapBuffer[shift] > lasthigh && LowMapBuffer[shift] == 0.0)
                        {
                            ZigzagBuffer[lasthighpos] = 0.0;
                            lasthighpos = shift;
                            lasthigh = HighMapBuffer[shift];
                            ZigzagBuffer[shift] = lasthigh;
                        }
                        if (LowMapBuffer[shift] != 0.0 && HighMapBuffer[shift] == 0.0)
                        {
                            lastlow = LowMapBuffer[shift];
                            lastlowpos = shift;
                            ZigzagBuffer[shift] = lastlow;
                            whatlookfor = 1;
                        }
                        break;
                    default: return 0;
                }
            }

            return 0;
        }
    }
}
