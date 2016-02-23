using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Properties;

namespace GreenZoneFxEngine
{
    public class ExpertPropertiesPanel : ExecPropertiesPanel<ExpertRuntime, Mt4ExpertInfo, ExpertSession, ExpertAttribute>
    {
        public ExpertPropertiesPanel() {
            this.Name = "ExpertPropertiesPanel";
        }

        public void Bind(GreenWinFormsMVContext context, ExpertPropertiesController controller)
        {
            base.Bind(context, controller);
        }
    }
}
