using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class NormalChartSectionPanelController : ClientNormalChartSectionPanelControllerBase
    {

        public NormalChartSectionPanelController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            UpdateCursor();
        }

        public override IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.CHART_WINDOW;
            }
        }

        public override SeriesRange DragYRange
        {
            get
            {
                return Owner.SeriesRange;
            }
            set
            {
                SeriesRange totalRange = Parent.ChartPanel.CursorChart.MasterChartSectionPanel.SectionRange;
                if (value.PriceTo > totalRange.PriceTo)
                {
                    double range = value.PriceRange;
                    value.PriceFrom = totalRange.PriceTo - range;
                    value.PriceTo = totalRange.PriceTo;
                }
                else if (value.PriceFrom < totalRange.PriceFrom)
                {
                    double range = value.PriceRange;
                    value.PriceFrom = totalRange.PriceFrom;
                    value.PriceTo = totalRange.PriceFrom + range;
                }
                Owner.SeriesRange = value;
            }
        }

        public override void CalculateSeriesRangeToFit()
        {
        }


    }


}
