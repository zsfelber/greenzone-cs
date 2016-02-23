using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine
{
    public class IndicatorPropertiesPanel : ExecPropertiesPanel<IndicatorRuntime, Mt4IndicatorInfo, IndicatorSession, IndicatorAttribute>
    {
        public IndicatorPropertiesPanel() {
            this.Name = "IndicatorPropertiesPanel";
        }
    }
}
