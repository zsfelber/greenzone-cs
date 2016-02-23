using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class ScriptInfoController : ScriptInfoControllerBase
    {
        public ScriptInfoController(GreenRmiManager rmiManager, EnvironmentSettingsController parent, Mt4ExecutableInfo info)
            : base(rmiManager, parent, info)
        {
        }
    }
}
