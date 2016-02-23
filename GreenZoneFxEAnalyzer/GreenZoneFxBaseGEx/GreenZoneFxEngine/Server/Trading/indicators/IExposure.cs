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

    //[Indicator("Exposure")]
    class IExposure : ServerIndicatorRuntime
    {
        //#property indicator_separate_window
        //#property indicator_buffers 1
        //#property indicator_minimum 0.0
        //#property indicator_maximum 0.1

        const int SYMBOLS_MAX = 1024;
        const int DEALS = 0;
        const int BUY_LOTS = 1;
        const int BUY_PRICE = 2;
        const int SELL_LOTS = 3;
        const int SELL_PRICE = 4;
        const int NET_LOTS = 5;
        const int PROFIT = 6;

        Color _ExtColor = Color.LightSeaGreen;

        public Color ExtColor { get { return _ExtColor; } set { _ExtColor = value; } }

        string ExtName = "Exposure";
        symbol[] ExtSymbols = new symbol[SYMBOLS_MAX];
        int ExtSymbolsTotal = 0;
        double[,] ExtSymbolsSummaries = new double[SYMBOLS_MAX, 7];
        int ExtLines = -1;
        string[] ExtCols ={"Symbol",
                            "Deals",
                            "Buy lots",
                            "Buy price",
                            "Sell lots",
                            "Sell price",
                            "Net lots",
                            "Profit"};
        int[] ExtShifts = { 10, 80, 130, 180, 260, 310, 390, 460 };
        int ExtVertShift = 14;
        DArr ExtMapBuffer;

        public IExposure(GreenRmiManager rmiManager, IChartRuntime parent, Mt4ExecutableInfo info, ISeriesManagerCache icache)
            : base(rmiManager, parent, info, icache, IndicatorWindowType.SEPARATE_WINDOW, 1, 0.0, 0.1,
                new IndicatorBuffer(0,Color.Transparent))
        {
        }

        public IExposure(GreenRmiManager rmiManager, IChartRuntime parent, IIndicatorSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public override int Init()
        {
            IndicatorShortName(ExtName);
            SetIndexBuffer(0, ref ExtMapBuffer);
            SetIndexStyle(0, DRAW_NONE);
            IndicatorDigits(0);
            SetIndexEmptyValue(0, 0.0);
            return 0;
        }
        public override int Deinit()
        {
            int windex = WindowFind(ExtName);
            if (windex > 0) ObjectsDeleteAll(windex);
            return 0;
        }
        public override int OnTick()
        {
            string name;
            int i, col, line, windex = WindowFind(ExtName);
            //----
            if (windex < 0) return 0;
            //---- header line
            if (ExtLines < 0)
            {
                for (col = 0; col < 8; col++)
                {
                    name = "Head_" + col;
                    if (ObjectCreate(name, OBJ_LABEL, windex, 0, 0))
                    {
                        ObjectSet(name, OBJPROP_XDISTANCE, ExtShifts[col]);
                        ObjectSet(name, OBJPROP_YDISTANCE, ExtVertShift);
                        ObjectSetText(name, ExtCols[col], 9, "Arial", ExtColor);
                    }
                }
                ExtLines = 0;
            }
            //----
            ArrayInitialize(ExtSymbolsSummaries, 0.0);
            int total = Analyze();
            if (total > 0)
            {
                line = 0;
                for (i = 0; i < ExtSymbolsTotal; i++)
                {
                    if (ExtSymbolsSummaries[i, DEALS] <= 0) continue;
                    line++;
                    //---- add line
                    if (line > ExtLines)
                    {
                        int y_dist = ExtVertShift * (line + 1) + 1;
                        for (col = 0; col < 8; col++)
                        {
                            name = "Line_" + line + "_" + col;
                            if (ObjectCreate(name, OBJ_LABEL, windex, 0, 0))
                            {
                                ObjectSet(name, OBJPROP_XDISTANCE, ExtShifts[col]);
                                ObjectSet(name, OBJPROP_YDISTANCE, y_dist);
                            }
                        }
                        ExtLines++;
                    }
                    //---- set line
                    int digits = (int)MarketInfo(ExtSymbols[i], MODE_DIGITS);
                    double buy_lots = ExtSymbolsSummaries[i, BUY_LOTS];
                    double sell_lots = ExtSymbolsSummaries[i, SELL_LOTS];
                    double buy_price = 0.0;
                    double sell_price = 0.0;
                    if (buy_lots != 0) buy_price = ExtSymbolsSummaries[i, BUY_PRICE] / buy_lots;
                    if (sell_lots != 0) sell_price = ExtSymbolsSummaries[i, SELL_PRICE] / sell_lots;
                    name = "Line_" + line + "_0";
                    ObjectSetText(name, (string)ExtSymbols[i], 9, "Arial", ExtColor);
                    name = "Line_" + line + "_1";
                    ObjectSetText(name, DoubleToStr(ExtSymbolsSummaries[i, DEALS], 0), 9, "Arial", ExtColor);
                    name = "Line_" + line + "_2";
                    ObjectSetText(name, DoubleToStr(buy_lots, 2), 9, "Arial", ExtColor);
                    name = "Line_" + line + "_3";
                    ObjectSetText(name, DoubleToStr(buy_price, digits), 9, "Arial", ExtColor);
                    name = "Line_" + line + "_4";
                    ObjectSetText(name, DoubleToStr(sell_lots, 2), 9, "Arial", ExtColor);
                    name = "Line_" + line + "_5";
                    ObjectSetText(name, DoubleToStr(sell_price, digits), 9, "Arial", ExtColor);
                    name = "Line_" + line + "_6";
                    ObjectSetText(name, DoubleToStr(buy_lots - sell_lots, 2), 9, "Arial", ExtColor);
                    name = "Line_" + line + "_7";
                    ObjectSetText(name, DoubleToStr(ExtSymbolsSummaries[i, PROFIT], 2), 9, "Arial", ExtColor);
                }
            }
            //---- remove lines
            if (total < ExtLines)
            {
                for (line = ExtLines; line > total; line--)
                {
                    name = "Line_" + line + "_0";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_1";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_2";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_3";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_4";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_5";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_6";
                    ObjectSetText(name, "");
                    name = "Line_" + line + "_7";
                    ObjectSetText(name, "");
                }
            }
            //---- to avoid minimum==maximum
            ExtMapBuffer[Bars - 1] = -1;
            //----
            return 0;
        }
        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        int Analyze()
        {
            double profit;
            int i, index, total = OrdersTotal;
            OrderType type;
            //----
            for (i = 0; i < total; i++)
            {
                if (!OrderSelect(i, SELECT_BY_POS)) continue;
                type = OrderType;
                if (type != OP_BUY && type != OP_SELL) continue;
                index = SymbolsIndex(OrderSymbol);
                if (index < 0 || index >= SYMBOLS_MAX) continue;
                //----
                ExtSymbolsSummaries[index, DEALS]++;
                profit = OrderProfit + OrderCommission + OrderSwap;
                ExtSymbolsSummaries[index, PROFIT] += profit;
                if (type == OP_BUY)
                {
                    ExtSymbolsSummaries[index, BUY_LOTS] += OrderLots;
                    ExtSymbolsSummaries[index, BUY_PRICE] += OrderOpenPrice * OrderLots;
                }
                else
                {
                    ExtSymbolsSummaries[index, SELL_LOTS] += OrderLots;
                    ExtSymbolsSummaries[index, SELL_PRICE] += OrderOpenPrice * OrderLots;
                }
            }
            //----
            total = 0;
            for (i = 0; i < ExtSymbolsTotal; i++)
            {
                if (ExtSymbolsSummaries[i, DEALS] > 0) total++;
            }
            //----
            return (total);
        }

        //+------------------------------------------------------------------+
        //|                                                                  |
        //+------------------------------------------------------------------+
        int SymbolsIndex(symbol symbol)
        {
            bool found = false;
            //----
            int i;
            for (i = 0; i < ExtSymbolsTotal; i++)
            {
                if (symbol == ExtSymbols[i])
                {
                    found = true;
                    break;
                }
            }
            //----
            if (found) return (i);
            if (ExtSymbolsTotal >= SYMBOLS_MAX) return (-1);
            //----
            i = ExtSymbolsTotal;
            ExtSymbolsTotal++;
            ExtSymbols[i] = symbol;
            ExtSymbolsSummaries[i, DEALS] = 0;
            ExtSymbolsSummaries[i, BUY_LOTS] = 0;
            ExtSymbolsSummaries[i, BUY_PRICE] = 0;
            ExtSymbolsSummaries[i, SELL_LOTS] = 0;
            ExtSymbolsSummaries[i, SELL_PRICE] = 0;
            ExtSymbolsSummaries[i, NET_LOTS] = 0;
            ExtSymbolsSummaries[i, PROFIT] = 0;
            //----
            return (i);
        }
    }
}
