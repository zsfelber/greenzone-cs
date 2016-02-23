using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{

    public class IndicatorPropertiesController : ExecPropertiesController<ServerIndicatorRuntime, ServerIndicatorSession, IndicatorAttribute>, IIndicatorPropertiesController
    {
        public IndicatorPropertiesController(GreenRmiManager rmiManager, TabPageController parent)
            : base(rmiManager, parent, null)
        {
            this.Name = "IndicatorPropertiesController";
        }
    }
}
