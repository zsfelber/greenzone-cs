using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Properties;

namespace GreenZoneFxEngine
{
    public class IndicatorPropertiesPanel : ExecPropertiesPanel<IndicatorRuntime, Mt4IndicatorInfo, IndicatorSession, IndicatorAttribute>
    {
        public IndicatorPropertiesPanel() {
            this.Name = "IndicatorPropertiesPanel";
        }

        public void Bind(GreenWinFormsMVContext context, IndicatorPropertiesController controller)
        {
            base.Bind(context, controller);
        }
    }
}
