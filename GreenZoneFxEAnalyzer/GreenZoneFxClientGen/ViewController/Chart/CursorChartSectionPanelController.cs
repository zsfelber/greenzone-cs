using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class CursorChartSectionPanelController : ClientCursorChartSectionPanelControllerBase
    {

        public CursorChartSectionPanelController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            UpdateCursor();
        }

        public override SeriesRange DragYRange
        {
            get
            {
                return ((IChartOwner)CursorRuntime.Parent).SeriesRange;
            }
            set
            {
                SeriesRange r = ((IChartOwner)CursorRuntime.Parent).SeriesRange;
                r.PriceFrom = value.PriceFrom;
                r.PriceTo = value.PriceTo;
                ((IChartOwner)CursorRuntime.Parent).SeriesRange = r;
            }
        }

        public override void UpdateCursor()
        {
            SeriesRange sectionRange = SectionRange;
            sectionRange.OffsetFrom = CursorRuntime.sLTime.StartIndexP;
            sectionRange.OffsetTo = CursorRuntime.sLTime.Length - 1;
            SectionRange = sectionRange;
        }

        public override void CalculatePriceLabelYs()
        {
            CalculatePriceLabelYs(15);
        }

        public override void CalculateSeriesRangeToFit()
        {
            SeriesRange sectionRange = CursorRuntime.SeriesRange;

            double min = double.MaxValue, max = double.MinValue;
            FindPriceMinMax(ref min, ref max);

            sectionRange.PriceFrom = min;
            sectionRange.PriceTo = max;
            SectionRange = sectionRange;
        }

        public override void ScrollYToPrice()
        {
            SeriesRange sectionRange = CursorRuntime.SeriesRange;

            double range = sectionRange.PriceRange;

            double min = double.MaxValue, max = double.MinValue;
            FindPriceMinMax(ref min, ref max);

            sectionRange.PriceFrom = (min + max) / 2 - range / 2;
            sectionRange.PriceTo = (min + max) / 2 + range / 2;
            SectionRange = sectionRange;
        }

        public override void FindPriceMinMax(ref double min, ref double max)
        {
            min = double.MaxValue;
            max = double.MinValue;

            INormalSeriesManagerCache seriesCache = CursorRuntime.SeriesCache;
            SeriesRange sectionRange = SectionRange;

            for (int i = sectionRange.OffsetFrom; i <= sectionRange.OffsetTo; i++)
            {
                if (seriesCache.sLTime.StartIndexP <= i && i < seriesCache.sLTime.Length)
                {
                    double high = seriesCache.sHigh[i];
                    double low = seriesCache.sLow[i];

                    min = Math.Min(min, low);
                    max = Math.Max(max, high);
                }
            }
        }
    }

}
