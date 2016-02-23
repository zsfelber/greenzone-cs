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
    public class CursorChartSectionPanelController : ServerCursorChartSectionPanelControllerBase
    {

        public CursorChartSectionPanelController(GreenRmiManager rmiManager, CursorChartController parent)
            : base(rmiManager, parent)
        {
            SectionRange = new SeriesRange();
            PriceLabelPane1.PlainFonts = true;
            PriceLabelPane1.Font = new Font("Arial", 7);
        }

        protected override IPriceLabelPaneController CreatePriceLabelPaneController()
        {
            return new PriceLabelPaneController(rmiManager, this);
        }

        public override IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.CHART_WINDOW;
            }
        }

        protected override void CreateChartPane(ServerChartControllerEx parent)
        {
            ChartPane = new CursorChartPaneController(rmiManager, this, (CursorChartController)parent);
        }

    }

}
