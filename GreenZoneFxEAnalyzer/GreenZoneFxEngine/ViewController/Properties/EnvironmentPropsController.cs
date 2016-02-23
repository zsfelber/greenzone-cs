using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class EnvironmentPropsController : EnvironmentPropsControllerBase
    {
        public EnvironmentPropsController(GreenRmiManager rmiManager, EnvironmentSettingsController parent, ServerEnvironmentRuntime environment)
            : base(rmiManager, parent, environment)
        {
        }
    }
}
