using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class IndicatorInfoController : IndicatorInfoControllerBase
    {
        public IndicatorInfoController(GreenRmiManager rmiManager, EnvironmentSettingsController parent, Mt4ExecutableInfo info)
            : base(rmiManager, parent, info)
        {
        }
    }
}
