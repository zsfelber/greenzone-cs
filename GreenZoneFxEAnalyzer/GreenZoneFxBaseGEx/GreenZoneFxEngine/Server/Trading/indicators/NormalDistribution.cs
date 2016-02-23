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

    [Indicator("Normal Distribution")]
    class NormalDistribution : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_color1 DarkBlue
        //#property indicator_minimum 0
        //---- input parameters
        int n = 1000;
        int pile = 5;

        public int N { get { return n; } set { n = value; } }
        public int Pile { get { return pile; } set { pile = value; } }

        //---- buffers
        DArr ExtMapBuffer1;

        int firstBar;
        int range = 100;
        bool Calculated = false;

        public NormalDistribution(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1,
                new IndicatorBuffer(0,Color.DarkBlue))
        {
            Session.IndicatorMinimum = 0;
            Session.EnableScroll = false;
        }

        public NormalDistribution(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            //---- indicators
            SetIndexStyle(0, DRAW_HISTOGRAM);
            SetIndexBuffer(0, ref ExtMapBuffer1);
            //double Max,Min;

            range = MathMin(WindowBarsPerChart - 1, Bars - 1);
            firstBar = MathMin(WindowFirstVisibleBar, Bars - 1);
            MathSrand(GetTickCount());
            IndicatorShortName(StringConcatenate("N=", N, "  pile=", pile, "       "));
            //----
            return 0;
        }
        public override int Deinit()
        {
            return 0;
        }
        public override int OnTick()
        {
            int i, counted_bars = IndicatorCounted;
            double maxDistribution = 0;
            //----
            int[] section = null;
            ArrayResize(ref section, range);
            if (!Calculated)
            {
                FillSections(section, range, pile, N, ref maxDistribution);
                for (i = 0; i < range; i++)
                {
                    ExtMapBuffer1[firstBar - range + i] = section[i] / maxDistribution;
                }
                Calculated = true;
            }
            //----
            return 0;
        }

        //+------------------------------------------------------------------+
        //|  ïîëó÷èì ÷ëó÷àéíîå ÷èñëî îò 0 äî Maximum                         |
        //+------------------------------------------------------------------+
        double Random(double Maximum)
        {
            int res;
            //----
            res = (int)(MathRand() / 32767.0 * Maximum);
            //----
            return (res);
        }
        //+------------------------------------------------------------------+
        //|  ïîëó÷èì ñðåäíåå îò NumberOfRandom ÷èñåë                         |
        //+------------------------------------------------------------------+
        double GetAverage(int MaxRandom, int NumberOfRandom)
        {
            double res = 0;
            int i;
            //----
            for (i = 0; i < NumberOfRandom; i++)
            {
                res += Random(MaxRandom);
            }
            //----
            return (res / NumberOfRandom);
        }

        //+------------------------------------------------------------------+
        //| çàïîëíèì ñåêöèè ÷àñòîòíîãî ðàñïðåäåëåíèÿ                         |
        //+------------------------------------------------------------------+
        void FillSections(int[] Array, int Range, int Pile, int Balls, ref double max)
        {
            int i, index;
            //----
            for (i = 0; i < Balls; i++)
            {
                index = (int)GetAverage(Range, Pile);
                Array[index]++;
            }

            for (i = 0; i < range; i++) if (Array[i] > max) max = Array[i];

            //----
            return;
        }

    }
}
