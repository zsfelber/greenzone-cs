using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class SymbolPropertiesController : SymbolPropertiesControllerBase
    {
        public SymbolPropertiesController(GreenRmiManager rmiManager, EnvironmentSettingsController parent, ServerSymbolContext symbol)
            : base(rmiManager, parent, symbol)
        {
        }
    }

}
