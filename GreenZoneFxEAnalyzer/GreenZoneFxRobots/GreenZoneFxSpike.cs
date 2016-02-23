using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Drawing;
using GreenZoneFxEngine.Types;

//namespace GreenZoneFxRobots
namespace GreenZoneFxEngine.Trading
{
    [Expert]
    public class GreenZoneFxSpike : ExpertRuntime
    {
        string _Configuration = "================ Configuration";
        double _Lots = 0.1;
        double _thres_deviation = 0;
        int _thres_pips = 350;
        double _thres_coeff_bid = 0;
        int _period_open = 10;
        TimePeriodConst _timeframe_open = PERIOD_M1;
        int _close_opposite_pips = 200;
        int _period_close = 3;
        TimePeriodConst _timeframe_close = PERIOD_M1;
        int _tp = 1000;
        int _sl = 80;
        int _limit = 30;
        int _trail_sens = 5;
        int _multi_order_step = 100;
        double _multi_order_step_mul = 0.8;
        int _Slippage = 3;
        int _Magic = 12023;
        string _comment = "BollIntraMin_1.0";
        int _max_orders = 10;
        bool _reverse = true;
        bool _trail_pending_orders = true;
        int _expiration_secs = 0;
        Color _BuyColor = Color.Lime;
        Color _SellColor = Color.Orange;

        public string Configuration { get { return _Configuration; } set { _Configuration = value; } }
        public double Lots { get { return _Lots; } set { _Lots = value; } }
        public double thres_deviation { get { return _thres_deviation; } set { _thres_deviation = value; } }
        public int thres_pips { get { return _thres_pips; } set { _thres_pips = value; } }
        public double thres_coeff_bid { get { return _thres_coeff_bid; } set { _thres_coeff_bid = value; } }
        public int period_open { get { return _period_open; } set { _period_open = value; } }
        public TimePeriodConst timeframe_open { get { return _timeframe_open; } set { _timeframe_open = value; } }
        public int close_opposite_pips { get { return _close_opposite_pips; } set { _close_opposite_pips = value; } }
        public int period_close { get { return _period_close; } set { _period_close = value; } }
        public TimePeriodConst timeframe_close { get { return _timeframe_close; } set { _timeframe_close = value; } }
        public int tp { get { return _tp; } set { _tp = value; } }
        public int sl { get { return _sl; } set { _sl = value; } }
        public int limit { get { return _limit; } set { _limit = value; } }
        public int trail_sens { get { return _trail_sens; } set { _trail_sens = value; } }
        public int multi_order_step { get { return _multi_order_step; } set { _multi_order_step = value; } }
        public double multi_order_step_mul { get { return _multi_order_step_mul; } set { _multi_order_step_mul = value; } }
        public int Slippage { get { return _Slippage; } set { _Slippage = value; } }
        public int Magic { get { return _Magic; } set { _Magic = value; } }
        public string comment { get { return _comment; } set { _comment = value; } }
        public int max_orders { get { return _max_orders; } set { _max_orders = value; } }
        public bool reverse { get { return _reverse; } set { _reverse = value; } }
        public bool trail_pending_orders { get { return _trail_pending_orders; } set { _trail_pending_orders = value; } }
        public int expiration_secs { get { return _expiration_secs; } set { _expiration_secs = value; } }
        public Color BuyColor { get { return _BuyColor; } set { _BuyColor = value; } }
        public Color SellColor { get { return _SellColor; } set { _SellColor = value; } }

        double point;

        public GreenZoneFxSpike(ServerChartRuntime parent, Mt4ExpertInfo executableInfo, SeriesManagerCache icache)
            : base(parent, executableInfo, icache)
        {
        }

        public GreenZoneFxSpike(ServerChartRuntime parent, ExpertSession session, SeriesManagerCache icache)
            : base(parent, session, icache)
        {
        }

        public override int Init()
        {
            if (Digits % 2 == 0)
            {
                point = Point / 10;
            }
            else
            {
                point = Point;
            }
            return 0;
        }

        public override int Deinit()
        {
            return 0;
        }

        public override int OnTick()
        {
            int i;

            double[] Params = { 0, 0, 2, 3, 1, 0, 0, 0 };
            Params[2] = sl * point;
            Params[3] = tp * point;
            //Params[7]=expiration_secs;
            manageTrailingStop(Magic, sl * point, trail_sens * point, Params);

            if (trail_pending_orders)
            {
                Params[0] = 0;
                Params[1] = 0;
                Params[2] = limit * point;
                Params[3] = tp * point;
                Params[4] = 0;
                Params[5] = 1;
                Params[6] = 1;
                manageTrailingStop(Magic, limit * point, trail_sens * point, Params);
            }

            double lowTradeOpen = Params[1];
            double highTradeOpen = Params[2];

            bool gap = false;
            datetime tm;
            datetime tic = TimeCurrent;
            int p;
            if (IsTesting)
            {
                p = period_open - 2;
            }
            else
            {
                p = 0;
            }
            for (i = p; i < period_open; i++)
            {
                tm = iTime(Symbol, timeframe_open, i);
                if (tm == 0 || iOpen(Symbol, timeframe_open, i) == 0 || iLow(Symbol, timeframe_open, i) == 0 || iHigh(Symbol, timeframe_open, i) == 0 || iClose(Symbol, timeframe_open, i) == 0)
                {
                    gap = true;
                    break;
                }
                else if (tm < tic - (i + 1) * (int)timeframe_open * 120)
                {
                    gap = true;
                    break;
                }
            }

            if (gap)
            {
                Print("WARNING Graph gap found  " + TimeToStr(tic) + " offset:" + i);
            }
            else
            {
                double ma = iBands(Symbol, timeframe_open, period_open, 1, 0, PRICE_OPEN, MODE_MAIN, 0);
                double upper = iBands(Symbol, timeframe_open, period_open, 1, 0, PRICE_OPEN, MODE_UPPER, 0);
                double deviation = upper - ma;

                int numOrders = (int)NormalizeDouble(Params[0], 0);
                if (numOrders < max_orders)
                {
                    if (period_close != 0)
                    {
                        double mao = iMA(Symbol, timeframe_close, period_close, 0, MODE_SMA, PRICE_OPEN, 0);
                        if (Bid - mao >= close_opposite_pips * point)
                        {
                            manualCloseBuys(Magic, BuyColor);
                        }
                        else if (mao - Bid >= close_opposite_pips * point)
                        {
                            manualCloseSells(Magic, SellColor);
                        }
                    }

                    Order order;
                    datetime exp;
                    if (expiration_secs >= 600)
                    {
                        exp = TimeCurrent + expiration_secs;
                    }
                    else
                    {
                        exp = 0;
                    }

                    if (Bid - ma >= thres_deviation * deviation + thres_pips * point + thres_coeff_bid * Bid)
                    {
                        if (reverse)
                        {
                            if (Bid > highTradeOpen + multi_order_step * point * MathPow(multi_order_step_mul, numOrders))
                            {
                                order = OrderSend(Symbol, OP_SELLSTOP, Lots, Bid - limit * point, Slippage, Ask - limit * point + sl * point, Ask - limit * point - tp * point, comment, Magic, exp, SellColor);
                            }
                        }
                        else
                        {
                            if (Ask < lowTradeOpen - multi_order_step * point * MathPow(multi_order_step_mul, numOrders))
                            {
                                order = OrderSend(Symbol, OP_BUYLIMIT, Lots, Ask - limit * point, Slippage, Bid - limit * point - sl * point, Bid - limit * point + tp * point, comment, Magic, exp, SellColor);
                            }
                        }
                    }
                    else if (ma - Bid >= thres_deviation * deviation + thres_pips * point + thres_coeff_bid * Bid)
                    {
                        if (reverse)
                        {
                            if (Ask < lowTradeOpen - multi_order_step * point * MathPow(multi_order_step_mul, numOrders))
                            {
                                order = OrderSend(Symbol, OP_BUYSTOP, Lots, Ask + limit * point, Slippage, Bid + limit * point - sl * point, Bid + limit * point + tp * point, comment, Magic, exp, BuyColor);
                            }
                        }
                        else
                        {
                            if (Bid > highTradeOpen + multi_order_step * point * MathPow(multi_order_step_mul, numOrders))
                            {
                                order = OrderSend(Symbol, OP_SELLLIMIT, Lots, Bid + limit * point, Slippage, Ask + limit * point + sl * point, Ask + limit * point - tp * point, comment, Magic, exp, BuyColor);
                            }
                        }
                    }
                }
            }
            return 0;
        }

        void manageTrailingStop(int Magic, double TrailingStop, double sensitivity, double[] Params)
        {
            double highTradeOpen = 0;
            double lowTradeOpen = 1000000;
            int numBuys = 0, numSells = 0;
            double buyLots = 0, sellLots = 0;

            double spread = Ask - Bid;
            if (Params[0] == 0)
            {
                Params[0] = Bid;
            }
            if (Params[1] == 0)
            {
                Params[1] = Ask;
            }
            if (Params[2] == 0)
            {
                Params[2] = TrailingStop;
            }
            //if (Params[3]==0) {
            //   !!! Params[3] = TrailingStop;
            //}
            double sl = nd(Params[2]);
            double tp = nd(Params[3]);

            int expiration_secs = (int)NormalizeDouble(Params[7], 0);
            datetime exp;
            if (expiration_secs > 0)
            {
                exp = TimeCurrent + expiration_secs;
            }
            else
            {
                exp = 0;
            }
            double sl_narrow = nd(Params[8]);

            for (int i = OrdersTotal - 1; i >= 0; i--)
            {
                if (OrderSelect(i, SELECT_BY_POS, MODE_TRADES))
                {
                    if (OrderSymbol == Symbol && OrderMagicNumber == Magic && OrderCloseTime == 0)
                    {
                        if (exp > 0 && TimeCurrent >= exp)
                        {
                            switch (OrderType)
                            {
                                case OP_BUY:
                                case OP_SELL:
                                    //OrderClose(OrderTicket,OrderLots,OrderClosePrice,100000);
                                    break;
                                default: OrderDelete(OrderTicket);
                                    break;
                            }
                        }
                        else
                        {
                            double newPrice = nd(OrderOpenPrice);
                            double newSl = nd(OrderStopLoss);
                            double newSl0;
                            double newTp = nd(OrderTakeProfit);

                            bool manage;
                            if (MathRound(Params[4]) > 1)
                            {
                                manage = (MathRound(Params[4]) == OrderTicket);
                            }
                            else if (MathRound(Params[4]) == -1)
                            {
                                manage = newSl == 0 || newTp == 0;
                            }
                            else
                            {
                                manage = MathRound(Params[4]) > 0;
                            }

                            double sld;
                            switch (OrderType)
                            {
                                case OP_BUY:
                                    if (manage)
                                    {
                                        newSl0 = nd(OrderOpenPrice - spread - sl);
                                        if (newSl == 0)
                                        {
                                            newSl = newSl0;
                                        }
                                        else if (sl_narrow != 0)
                                        {
                                            sld = (Bid - newSl0) / (tp + sl);
                                            newSl = Bid - (sl + (sl_narrow - sl) * sld);
                                            newSl = nd(MathMax(OrderStopLoss, newSl));
                                        }
                                        else
                                        {
                                            newSl = nd(MathMax(OrderStopLoss, Params[0] - TrailingStop));
                                        }

                                        if (newTp == 0)
                                        {
                                            newTp = nd(OrderOpenPrice - spread + tp);
                                        }
                                    }
                                    numBuys++;
                                    buyLots += OrderLots;
                                    break;

                                case OP_BUYLIMIT:
                                    if (MathRound(Params[5]) != 0)
                                    {
                                        newPrice = nd(MathMax(OrderOpenPrice, Params[1] - TrailingStop));
                                        newSl = nd(newPrice - spread - sl);
                                        newTp = nd(newPrice - spread + tp);
                                    }
                                    numBuys++;
                                    buyLots += OrderLots;
                                    break;

                                case OP_BUYSTOP:
                                    if (MathRound(Params[6]) != 0)
                                    {
                                        newPrice = nd(MathMin(OrderOpenPrice, Params[1] + TrailingStop));
                                        newSl = nd(newPrice - spread - sl);
                                        newTp = nd(newPrice - spread + tp);
                                    }
                                    numBuys++;
                                    buyLots += OrderLots;
                                    break;



                                case OP_SELL:
                                    if (manage)
                                    {
                                        newSl0 = nd(OrderOpenPrice + spread + sl);
                                        if (newSl == 0)
                                        {
                                            newSl = newSl0;
                                        }
                                        else if (sl_narrow != 0)
                                        {
                                            sld = (newSl0 - Ask) / (tp + sl);
                                            newSl = Ask + (sl + (sl_narrow - sl) * sld);
                                            newSl = nd(MathMin(OrderStopLoss, newSl));
                                        }
                                        else
                                        {
                                            newSl = nd(MathMin(OrderStopLoss, Params[1] + TrailingStop));
                                        }

                                        if (newTp == 0)
                                        {
                                            newTp = nd(OrderOpenPrice + spread - tp);
                                        }
                                    }
                                    numSells++;
                                    sellLots += OrderLots;
                                    break;

                                case OP_SELLLIMIT:
                                    if (MathRound(Params[5]) != 0)
                                    {
                                        newPrice = nd(MathMin(OrderOpenPrice, Params[0] + TrailingStop));
                                        newSl = nd(newPrice + spread + sl);
                                        newTp = nd(newPrice + spread - tp);
                                    }
                                    numSells++;
                                    sellLots += OrderLots;
                                    break;

                                case OP_SELLSTOP:
                                    if (MathRound(Params[6]) != 0)
                                    {
                                        newPrice = nd(MathMax(OrderOpenPrice, Params[0] - TrailingStop));
                                        newSl = nd(newPrice + spread + sl);
                                        newTp = nd(newPrice + spread - tp);
                                    }
                                    numSells++;
                                    sellLots += OrderLots;
                                    break;
                            }

                            lowTradeOpen = MathMin(lowTradeOpen, OrderOpenPrice);
                            highTradeOpen = MathMax(highTradeOpen, OrderOpenPrice);

                            if (manage || MathRound(Params[5]) != 0 || MathRound(Params[6]) != 0)
                            {

                                if (nd(MathAbs(newPrice - OrderOpenPrice)) > sensitivity ||
                                      nd(MathAbs(newSl - OrderStopLoss)) > sensitivity ||
                                      nd(MathAbs(newTp - OrderTakeProfit)) > sensitivity)
                                {

                                    if (OrderExpiration == 0)
                                    {
                                        OrderModify(OrderTicket, newPrice, newSl, newTp, 0);
                                    }
                                    else
                                    {
                                        OrderModify(OrderTicket, newPrice, newSl, newTp, TimeCurrent + (OrderExpiration - OrderOpenTime));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Params[0] = numBuys + numSells;
            Params[1] = numBuys;
            Params[2] = numSells;
            Params[3] = lowTradeOpen;
            Params[4] = highTradeOpen;
            Params[5] = buyLots + sellLots;
            Params[6] = buyLots;
            Params[7] = sellLots;
        }

        void manualCloseBuys(int Magic, Color BuyColor)
        {
            for (int i = 0; i < OrdersTotal; i++)
            {
                OrderSelect(i, SELECT_BY_POS);
                if (OrderType == OP_BUY && OrderSymbol == Symbol && OrderMagicNumber == Magic && OrderCloseTime == 0)
                {
                    OrderClose(OrderTicket, OrderLots, OrderClosePrice, 100, BuyColor);
                    i = -1;
                }
            }
        }

        void manualCloseSells(int Magic, Color SellColor)
        {
            for (int i = 0; i < OrdersTotal; i++)
            {
                OrderSelect(i, SELECT_BY_POS);
                if (OrderType == OP_SELL && OrderSymbol == Symbol && OrderMagicNumber == Magic && OrderCloseTime == 0)
                {
                    OrderClose(OrderTicket, OrderLots, OrderClosePrice, 100, SellColor);
                    i = -1;
                }
            }
        }

    }
}
