using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class OrderChartSectionPanelController : ClientOrderChartSectionPanelControllerBase
    {

        public OrderChartSectionPanelController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            UpdateCursor();
        }

        public override IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.SEPARATE_WINDOW;
            }
        }

        public override string PriceFormat
        {
            get
            {
                return OrdersView.SymbolFormat;
            }
        }

        public override int Scale
        {
            get
            {
                return 0;
            }
        }

        public override void CalculateSeriesRangeToFit()
        {
            double min = double.MaxValue, max = double.MinValue;

            SeriesRange seriesRange = OrdersView.SeriesRange;

            for (int i = seriesRange.OffsetFrom; i <= seriesRange.OffsetTo; i++)
            {
                if (OrdersView.sLTime.StartIndexP <= i && i < OrdersView.sLTime.Length)
                {
                    double balance = OrdersView.BalanceHistAsDArr[i];

                    min = Math.Min(min, balance);
                    max = Math.Max(max, balance);
                }
            }
            seriesRange.PriceFrom = min;
            seriesRange.PriceTo = max;

            OrdersView.SeriesRange = seriesRange;
        }

        public override void UpdateCursor()
        {
            SectionRange.ChangeOffsetFrom(OrdersView.SeriesRange.OffsetFrom);
            base.UpdateCursor();
        }
    }


}
