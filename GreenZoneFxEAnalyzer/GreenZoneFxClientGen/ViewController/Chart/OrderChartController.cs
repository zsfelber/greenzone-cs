using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class OrderChartController : ClientOrderChartControllerBase
    {
        public OrderChartController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected override void SaveSessionAsync()
        {
            ((MainWindowController)MainWindow).SaveSessionAsync();
        }

        public override void CalculateSeriesRangeToFit(bool includeMainChart)
        {
            if (includeMainChart)
            {
                MasterChartSectionPanel.CalculateSeriesRangeToFit();
            }
            foreach (ClientChartSectionPanelControllerEx p in ChartSectionPanels)
            {
                p.CalculateSeriesRangeToFit();
            }
        }

        public override void ScrollYToPrice()
        {
            double min = double.MaxValue, max = double.MinValue;

            SeriesRange seriesRange = OrdersView.SeriesRange;

            double range = seriesRange.PriceRange;

            for (int i = seriesRange.OffsetFrom; i <= seriesRange.OffsetTo; i++)
            {
                if (OrdersView.sLTime.StartIndexP <= i && i < OrdersView.sLTime.Length)
                {
                    double balance = OrdersView.BalanceHistAsDArr[i];

                    min = Math.Min(min, balance);
                    max = Math.Max(max, balance);
                }
            }
            seriesRange.PriceFrom = (min + max) / 2 - range / 2;
            seriesRange.PriceTo = (min + max) / 2 + range / 2;

            OrdersView.SeriesRange = seriesRange;
        }

        public override void PrintStatus(SeriesBar _bar, IndicatorBar ibar, string f)
        {
            IMainWindowController mainWindowController = OrdersOverviewPanel.MainWindow;
            OrderSeriesBar bar = (OrderSeriesBar)_bar;
            mainWindowController.TimeLabel.Text = bar.time.ToString(GreenZoneUtilsBase.GetShortDateTimePattern());
            mainWindowController.OLabel.Text = "Balance";
            mainWindowController.LLabel.Text = "";
            mainWindowController.HLabel.Text = "";
            mainWindowController.CLabel.Text = "";
            mainWindowController.VLabel.Text = "";

            mainWindowController.OpenLabel.Text = bar.balance.ToString(f);

            mainWindowController.LowLabel.Text = "";
            mainWindowController.CloseLabel.Text = "";
            mainWindowController.ValueLabel.Text = "";
        }

    }



}
