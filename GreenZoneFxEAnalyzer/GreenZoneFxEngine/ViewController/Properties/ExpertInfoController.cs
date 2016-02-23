using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class ExpertInfoController : ExpertInfoControllerBase
    {
        public ExpertInfoController(GreenRmiManager rmiManager, EnvironmentSettingsController parent, Mt4ExecutableInfo info)
            : base(rmiManager, parent, info)
        {
        }
    }
}
