using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class NormalChartSectionPanelController : ServerNormalChartSectionPanelControllerBase
    {

        public NormalChartSectionPanelController(GreenRmiManager rmiManager, NormalChartController parent)
            : base(rmiManager, parent)
        {
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
            ChartPane = new NormalChartPaneController(rmiManager, this, (NormalChartController)parent);
        }



    }


}
