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

    [Indicator("Price Rate of Change (W)")]
    class ROC_Separate : ServerIndicatorRuntime
    {
        //--------------------------------------------------------------------
        //#property indicator_separate_window // Èíäèê.ðèñóåòñÿ â îòäåëüíîì îêíå
        //#property indicator_buffers 6       // Êîëè÷åñòâî áóôåðîâ
        //#property indicator_color1 Black    // Öâåò ëèíèè 0 áóôåðà
        //#property indicator_color2 DarkOrange//Öâåò ëèíèè 1 áóôåðà
        //#property indicator_color3 Green    // Öâåò ëèíèè 2 áóôåðà
        //#property indicator_color4 Brown    // Öâåò ëèíèè 3 áóôåðà
        //#property indicator_color5 Blue     // Öâåò ëèíèè 4 áóôåðà
        //#property indicator_color6 Red      // Öâåò ëèíèè 5 áóôåðà
        public ROC_Separate(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 6,
                new IndicatorBuffer(0,Color.Black),
                new IndicatorBuffer(1,Color.DarkOrange),
                new IndicatorBuffer(2,Color.Green),
                new IndicatorBuffer(3,Color.Brown),
                new IndicatorBuffer(4,Color.Blue),
                new IndicatorBuffer(5,Color.Red)
)
        {
            SetIndexStyle(0, DrawingStyle.DRAW_LINE);
            SetIndexStyle(1, DrawingStyle.DRAW_LINE);
            SetIndexStyle(2, DrawingStyle.DRAW_LINE);
            SetIndexStyle(3, DrawingStyle.DRAW_LINE);
            SetIndexStyle(4, DrawingStyle.DRAW_LINE);
            SetIndexStyle(5, DrawingStyle.DRAW_LINE);
        }

        public ROC_Separate(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        //--------------------------------------------------------------- 2 --
        int _History = 5000; public int History { get { return _History; } set { _History = value; } }        // Êîëè÷.áàðîâ â ðàñ÷¸òíîé èñòîðèè
        int _Period_MA_1 = 21; public int Period_MA_1 { get { return _Period_MA_1; } set { _Period_MA_1 = value; } }          // Ïåðèîä ðàñ÷¸òíîé ÌÀ
        int _Bars_V = 13; public int Bars_V { get { return _Bars_V; } set { _Bars_V = value; } }          // Êîëè÷.áàðîâ äëÿ ðàñ÷¸òà ñêîðîñò
        int _Aver_Bars = 5; public int Aver_Bars { get { return _Aver_Bars; } set { _Aver_Bars = value; } }           // Êîëè÷. áàðîâ äëÿ ñãëàæèâàíèÿ
        //--------------------------------------------------------------- 3 --
        int
           Period_MA_2, Period_MA_3,       // Ðàñ÷¸òíûå ïåðèîäû ÌÀ äëÿ äð. ÒÔ
           K2, K3;                          // Êîýôôèöèåíòû ñîîòíîøåíèÿ ÒÔ
        DArr
           Line_0,                        // Èíèäèêàòîðí. ìàññèâ îïîðíîé MA
           Line_1, Line_2, Line_3,        // Èíä. ìàññèâû ëèíèé ñêîðîñòè 
           Line_4,                        // Èíèäèêàòîðí. ìàññèâ ñóììàðíûé
           Line_5;                        // Èíä.ìàññèâ ñóììàðíûé ñãëàæåííûé
        double
           Sh_1, Sh_2, Sh_3;                // Êîëè÷.áàðîâ äëÿ ðàñ÷. ñêîðîñòåé
        //--------------------------------------------------------------- 4 --
        public override int Init()                          // Ñïåöèàëüíàÿ ôóíêöèÿ init()
        {
            SetIndexBuffer(0, ref Line_0);        // Íàçíà÷åíèå ìàññèâà áóôåðó
            SetIndexBuffer(1, ref Line_1);        // Íàçíà÷åíèå ìàññèâà áóôåðó
            SetIndexBuffer(2, ref Line_2);        // Íàçíà÷åíèå ìàññèâà áóôåðó
            SetIndexBuffer(3, ref Line_3);        // Íàçíà÷åíèå ìàññèâà áóôåðó
            SetIndexBuffer(4, ref Line_4);        // Íàçíà÷åíèå ìàññèâà áóôåðó
            SetIndexBuffer(5, ref Line_5);        // Íàçíà÷åíèå ìàññèâà áóôåðó
            SetIndexStyle(5, DRAW_LINE, STYLE_SOLID, (DrawingWidth)3);// Ñòèëü ëèíèè
            //--------------------------------------------------------------- 5 --
            switch (Period)                 // Ðàñ÷¸ò êîýôôèöèåíòîâ äëÿ..
            {                              // .. ðàçëè÷íûõ ÒÔ
                case TimePeriodConst.PERIOD_M1: K2 = 5; K3 = 15; break;// Òàéìôðåéì Ì1
                case TimePeriodConst.PERIOD_M5: K2 = 3; K3 = 6; break;// Òàéìôðåéì Ì5
                case TimePeriodConst.PERIOD_M15: K2 = 2; K3 = 4; break;// Òàéìôðåéì Ì15
                case TimePeriodConst.PERIOD_M30: K2 = 2; K3 = 8; break;// Òàéìôðåéì Ì30
                case TimePeriodConst.PERIOD_H1: K2 = 4; K3 = 24; break;// Òàéìôðåéì H1
                case TimePeriodConst.PERIOD_H4: K2 = 6; K3 = 42; break;// Òàéìôðåéì H4
                case TimePeriodConst.PERIOD_D1: K2 = 7; K3 = 30; break;// Òàéìôðåéì D1
                case TimePeriodConst.PERIOD_W1: K2 = 4; K3 = 12; break;// Òàéìôðåéì W1
                case TimePeriodConst.PERIOD_MN1: K2 = 3; K3 = 12; break;// Òàéìôðåéì ÌN
            }
            //--------------------------------------------------------------- 6 --
            Sh_1 = Bars_V;                     // Ïåðèîä èçìåðåí ñêîðîñòè (áàðîâ)
            Sh_2 = K2 * Sh_1;                    // Ïåðèîä èçìåðåí. äëÿ áëèæ. ÒÔ
            Sh_3 = K3 * Sh_1;                    // Ïåðèîä èçìåðåí. äëÿ ñëåä. ÒÔ
            Period_MA_2 = K2 * Period_MA_1;     // Ðàñ÷¸òí.ïåðèîä ÌÀ äëÿ áëèæ. ÒÔ
            Period_MA_3 = K3 * Period_MA_1;     // Ðàñ÷¸òí.ïåðèîä ÌÀ äëÿ ñëåä. ÒÔ
            //--------------------------------------------------------------- 7 --
            return 0;                          // Âûõîä èç ñïåö. ôóíêöèè init()
        }

        public override int Deinit()
        {
            return 0;
        }

        //--------------------------------------------------------------- 8 --
        public override int OnTick()                         // Ñïåöèàëüíàÿ ôóíêöèÿ start()
        {
            //--------------------------------------------------------------- 9 --
            double
            MA_c, MA_p,                      // Òåêóùåå è ïðåäûäóù. çíà÷. ÌÀ
            Sum;                             // Òåõí.ïàðàìåòð äëÿ íàêîïë. ñóììû
            int
            i,                               // Èíäåêñ áàðà
            n,                               // Ôîðìàëüí. ïàðàìåòð(èíäåêñ áàðà)
            Counted_bars;                    // Êîëè÷åñòâî ïðîñ÷èòàííûõ áàðîâ 
            //-------------------------------------------------------------- 10 --
            Counted_bars = IndicatorCounted; // Êîëè÷åñòâî ïðîñ÷èòàííûõ áàðîâ 
            i = Bars - Counted_bars - 1;           // Èíäåêñ ïåðâîãî íåïîñ÷èòàííîãî
            if (i > History - 1)                 // Åñëè ìíîãî áàðîâ òî ..
                i = History - 1;                  // ..ðàññ÷èòûâàòü çàäàííîå êîëè÷.
            //-------------------------------------------------------------- 11 --
            while (i >= 0)                      // Öèêë ïî íåïîñ÷èòàííûì áàðàì
            {
                //-------------------------------------------------------- 12 --
                Line_0[i] = 0;                  // Ãîðèçîíòàëüíàÿ ëèíèÿ îòñ÷¸òà
                //-------------------------------------------------------- 13 --
                MA_c = iMA(null, 0, Period_MA_1, 0, MODE_LWMA, PRICE_TYPICAL, i);
                MA_p = iMA(null, 0, Period_MA_1, 0, MODE_LWMA, PRICE_TYPICAL, i + (int)Sh_1);
                Line_1[i] = MA_c - MA_p;         // Çíà÷åíèå 1 ëèíèè ñêîðîñòè
                //-------------------------------------------------------- 14 --
                MA_c = iMA(null, 0, Period_MA_2, 0, MODE_LWMA, PRICE_TYPICAL, i);
                MA_p = iMA(null, 0, Period_MA_2, 0, MODE_LWMA, PRICE_TYPICAL, i + (int)Sh_2);
                Line_2[i] = MA_c - MA_p;         // Çíà÷åíèå 2 ëèíèè ñêîðîñòè
                //-------------------------------------------------------- 15 --
                MA_c = iMA(null, 0, Period_MA_3, 0, MODE_LWMA, PRICE_TYPICAL, i);
                MA_p = iMA(null, 0, Period_MA_3, 0, MODE_LWMA, PRICE_TYPICAL, i + (int)Sh_3);
                Line_3[i] = MA_c - MA_p;         // Çíà÷åíèå 3 ëèíèè ñêîðîñòè
                //-------------------------------------------------------- 16 --
                Line_4[i] = (Line_1[i] + Line_2[i] + Line_3[i]) / 3;// Ñóììàðíûé ìàññèâ
                //-------------------------------------------------------- 17 --
                if (Aver_Bars < 0)              // Åñëè íåâåðíî çàäàíî ñãëàæèâàíèå
                    Aver_Bars = 0;               // .. òî íå ìåíüøå íóëÿ
                Sum = 0;                        // Òåõíè÷åñêèé ïðè¸ì
                for (n = i; n <= i + Aver_Bars; n++) // Ñóììèðîâàåíèå ïîñëåäíèõ çíà÷åí.
                    Sum = Sum + Line_4[n];       // Íàêîïëåíèå ñóììû ïîñëåäí. çíà÷.
                Line_5[i] = Sum / (Aver_Bars + 1); // Èíäèê. ìàññèâ ñãëàæåííîé ëèíèè
                //-------------------------------------------------------- 18 --
                i--;                          // Ðàñ÷¸ò èíäåêñà ñëåäóþùåãî áàðà
                //-------------------------------------------------------- 19 --
            }
            return 0;                          // Âûõîä èç ñïåö. ô-èè start()
        }
        //-------------------------------------------------------------- 20 --
    }
}
