using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class NormalChartPaneController : ServerNormalChartPaneControllerBase
    {

        internal NormalChartPaneController(GreenRmiManager rmiManager, NormalChartSectionPanelController parent, NormalChartController chart)
            : base(rmiManager, parent, chart)
        {
		}


        protected override void LayOut()
        {
            throw new NotImplementedException();
        }
    }


}
