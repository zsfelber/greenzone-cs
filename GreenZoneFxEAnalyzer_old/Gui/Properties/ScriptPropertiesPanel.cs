using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine
{
    public class ScriptPropertiesPanel : ExecPropertiesPanel<ScriptRuntime, Mt4ScriptInfo, ScriptSession, ScriptAttribute>
    {
        public ScriptPropertiesPanel() {
            this.Name = "ScriptPropertiesPanel";
        }
    }
}
