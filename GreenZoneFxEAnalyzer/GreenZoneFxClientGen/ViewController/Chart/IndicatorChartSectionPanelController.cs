using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class IndicatorChartSectionPanelController : ClientIndicatorChartSectionPanelControllerBase
    {
        public IndicatorChartSectionPanelController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
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


        public override void CalculateSeriesRangeToFit()
        {
            IIndicatorRuntime indicatorRuntime = Indicator;
            if (indicatorRuntime != null)
            {
                double min = double.MaxValue, max = double.MinValue;

                SeriesRange sectionRange = ChartRuntime.SeriesRange;

                if (indicatorRuntime.Levels != null)
                {
                    foreach (var b in indicatorRuntime.Levels)
                    {
                        min = Math.Min(min, b.Value);
                        max = Math.Max(max, b.Value);
                    }
                }
                for (int i = sectionRange.OffsetFrom; i <= sectionRange.OffsetTo; i++)
                {
                    if (ChartRuntime.sLTime.StartIndexP <= i && i < ChartRuntime.sLTime.Length)
                    {
                        foreach (var b in indicatorRuntime.Buffers)
                        {
                            if (b.Buffer != null && b.StyleType != DrawingStyle.DRAW_NONE)
                            {
                                double v = b.SBuffer[i];
                                min = Math.Min(min, v);
                                max = Math.Max(max, v);
                            }
                        }
                    }
                }
                sectionRange.PriceFrom = min;
                sectionRange.PriceTo = max;
                if (indicatorRuntime.Levels != null)
                {
                    foreach (var b in indicatorRuntime.Levels)
                    {
                        max = Math.Max(max, b.Value + ChartPane.ChartWindowTopGapValue);
                    }
                }
                if (indicatorRuntime.Session.IndicatorMinimum != Double.MinValue)
                {
                    min = indicatorRuntime.Session.IndicatorMinimum;
                }
                if (indicatorRuntime.Session.IndicatorMaximum != Double.MaxValue)
                {
                    max = indicatorRuntime.Session.IndicatorMaximum;
                }
                sectionRange.PriceFrom = min;
                sectionRange.PriceTo = max;

                SectionRange = sectionRange;
            }
        }

        public override void UpdateCursor()
        {
            SeriesRange sectionRange = SectionRange;
            sectionRange.ChangeOffsetFrom(ChartRuntime.SeriesRange.OffsetFrom);
            SectionRange = sectionRange;
            base.UpdateCursor();
        }
    }

}
