using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{

    public class ScriptPropertiesController : ExecPropertiesController<ServerScriptRuntime, ServerScriptSession, ScriptAttribute>, IScriptPropertiesController
    {
        public ScriptPropertiesController(GreenRmiManager rmiManager, ScriptDialogController parent, ServerScriptRuntime scriptRuntime)
            : base(rmiManager, parent, scriptRuntime)
        {
            this.Name = "ScriptPropertiesController";
        }
    }
}
