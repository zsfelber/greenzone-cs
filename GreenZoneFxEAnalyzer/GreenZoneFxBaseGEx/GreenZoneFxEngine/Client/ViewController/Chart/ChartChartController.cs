using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;


namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ClientChartChartControllerEx : ClientChartChartControllerBase
    {

        public ClientChartChartControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }


        public ClientChartChartControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ClientChartChartControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void CalculateSeriesRangeToFit(bool includeMainChart)
        {
            MasterChartSectionPanel.CalculateSeriesRangeToFit();
            foreach (ClientChartSectionPanelControllerEx  p in ChartSectionPanels)
            {
                p.CalculateSeriesRangeToFit();
            }

            if (includeMainChart)
            {
                ISeriesManagerRuntime seriesManager = ChartRuntime.GuiSeriesManager;
                INormalSeriesManagerCache seriesCache = seriesManager.DefaultCache;
                SeriesRange seriesRange = ChartRuntime.SeriesRange;

                double min = double.MaxValue, max = double.MinValue;
                FindPriceMinMax(ref min, ref max);

                seriesRange.PriceFrom = min;
                seriesRange.PriceTo = max;

                ChartRuntime.SeriesRange = seriesRange;
            }
        }

        public override void ScrollYToPrice()
        {
            ISeriesManagerRuntime seriesManager = ChartRuntime.GuiSeriesManager;
            INormalSeriesManagerCache seriesCache = seriesManager.DefaultCache;
            SeriesRange seriesRange = ChartRuntime.SeriesRange;

            double range = seriesRange.PriceRange;

            double min = double.MaxValue, max = double.MinValue;
            FindPriceMinMax(ref min, ref max);

            seriesRange.PriceFrom = (min + max) / 2 - range / 2;
            seriesRange.PriceTo = (min + max) / 2 + range / 2;

            ChartRuntime.SeriesRange = seriesRange;
        }

        public override void FindPriceMinMax(ref double min, ref double max)
        {
            min = double.MaxValue;
            max = double.MinValue;

            ISeriesManagerRuntime seriesManager = ChartRuntime.GuiSeriesManager;
            INormalSeriesManagerCache seriesCache = seriesManager.DefaultCache;

            SeriesRange seriesRange = ChartRuntime.SeriesRange;

            for (int i = seriesRange.OffsetFrom; i <= seriesRange.OffsetTo; i++)
            {
                if (seriesCache.sLTime.StartIndexP <= i && i < seriesCache.sLTime.Length)
                {
                    if (seriesCache.Period.GetCategory() == TimePeriodCategory.TICKS)
                    {
                        double bid = seriesCache.sBids[i];
                        double ask = seriesCache.sAsks[i];

                        min = Math.Min(min, bid);
                        min = Math.Min(min, ask);

                        max = Math.Max(max, bid);
                        max = Math.Max(max, ask);
                    }
                    else
                    {
                        double open = seriesCache.sOpen[i];
                        double low = seriesCache.sLow[i];
                        double high = seriesCache.sHigh[i];
                        double close = seriesCache.sClose[i];

                        min = Math.Min(min, open);
                        min = Math.Min(min, low);
                        min = Math.Min(min, high);
                        min = Math.Min(min, close);

                        max = Math.Max(max, open);
                        max = Math.Max(max, low);
                        max = Math.Max(max, high);
                        max = Math.Max(max, close);
                    }
                }
            }
        }

        public override void PrintStatus(SeriesBar _bar, IndicatorBar ibar, string f)
        {
            IMainWindowController mainWindowController = ChartGroupPanel.MainWindow;

            if (_bar is PeriodSeriesBar)
            {
                PeriodSeriesBar bar = (PeriodSeriesBar)_bar;

                mainWindowController.OLabel.Text = "O";
                mainWindowController.LLabel.Text = "L";
                mainWindowController.HLabel.Text = "H";
                mainWindowController.CLabel.Text = "C";

                mainWindowController.TimeLabel.Text = bar.time.ToString(GreenZoneUtilsBase.GetShortDateTimePattern());
                mainWindowController.OpenLabel.Text = bar.open.ToString(f);
                mainWindowController.LowLabel.Text = bar.low.ToString(f);
                mainWindowController.HighLabel.Text = bar.high.ToString(f);
                mainWindowController.CloseLabel.Text = bar.close.ToString(f);
                if (ibar == null)
                {
                    mainWindowController.VLabel.Text = "";
                    mainWindowController.ValueLabel.Text = "";
                }
                else
                {
                    if (string.IsNullOrEmpty(ibar.buffer.Label))
                    {
                        mainWindowController.VLabel.Text = ibar.indicator.IndicatorInfo.Name;
                    }
                    else
                    {
                        mainWindowController.VLabel.Text = ibar.buffer.Label;
                    }
                    mainWindowController.ValueLabel.Text = ibar.value.ToString(f);
                }
            }
            else if (_bar is TickSeriesBar)
            {
                TickSeriesBar bar = (TickSeriesBar)_bar;

                mainWindowController.OLabel.Text = "Bid";
                mainWindowController.LLabel.Text = "Ask";
                mainWindowController.HLabel.Text = "Spread";
                mainWindowController.CLabel.Text = "";

                mainWindowController.TimeLabel.Text = bar.time.ToString(GreenZoneUtilsBase.GetShortDateTimePattern());
                mainWindowController.OpenLabel.Text = bar.bid.ToString(f);
                mainWindowController.LowLabel.Text = bar.ask.ToString(f);
                mainWindowController.HighLabel.Text = (bar.ask - bar.bid).ToString(f);

                mainWindowController.CloseLabel.Text = "";

                if (ibar == null)
                {
                    mainWindowController.VLabel.Text = "";
                    mainWindowController.ValueLabel.Text = "";
                }
                else
                {
                    if (string.IsNullOrEmpty(ibar.buffer.Label))
                    {
                        mainWindowController.VLabel.Text = ibar.indicator.IndicatorInfo.Name;
                    }
                    else
                    {
                        mainWindowController.VLabel.Text = ibar.buffer.Label;
                    }
                    mainWindowController.ValueLabel.Text = ibar.value.ToString(f);
                }
            }
            else
            {
                SeriesBar bar = _bar;

                mainWindowController.OLabel.Text = "";
                mainWindowController.LLabel.Text = "";
                mainWindowController.HLabel.Text = "";
                mainWindowController.VLabel.Text = "";

                mainWindowController.TimeLabel.Text = bar.time.ToString(GreenZoneUtilsBase.GetShortDateTimePattern());
                mainWindowController.OpenLabel.Text = "";
                mainWindowController.LowLabel.Text = "";
                mainWindowController.HighLabel.Text = "";
                mainWindowController.ValueLabel.Text = "";

                if (ibar == null)
                {
                    mainWindowController.CLabel.Text = "";
                    mainWindowController.CloseLabel.Text = "";
                }
                else
                {
                    if (string.IsNullOrEmpty(ibar.buffer.Label))
                    {
                        mainWindowController.CLabel.Text = ibar.indicator.IndicatorInfo.Name;
                    }
                    else
                    {
                        mainWindowController.CLabel.Text = ibar.buffer.Label;
                    }
                    mainWindowController.CloseLabel.Text = ibar.value.ToString(f);
                }
            }
        }

    }


}
