using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class IndicatorChartPaneController : ServerIndicatorChartPaneControllerBase
    {
        internal IndicatorChartPaneController(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, NormalChartController chart)
            : base(rmiManager, parent, chart)
        {
        }


        protected override void LayOut()
        {
            throw new NotImplementedException();
        }
    }

}
