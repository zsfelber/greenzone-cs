using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;

using GreenZoneFxEngine.ViewController.Properties;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class ChartGroupController : ClientChartGroupControllerBase
    {

        public ChartGroupController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }


        public override void UpdateAllCursor(IClientChartViewController invoker = null)
        {
            foreach (ChartViewController cp in ChartViews)
            {
                if (invoker != cp)
                {
                    cp.UpdateCursor();
                }
            }
        }

        public override void UpdateAllChartAndCursor(IClientChartViewController invoker = null)
        {
            foreach (ChartViewController cp in ChartViews)
            {
                if (invoker != cp)
                {
                    cp.UpdateChartAndCursor();
                }
            }
        }
    }
}
