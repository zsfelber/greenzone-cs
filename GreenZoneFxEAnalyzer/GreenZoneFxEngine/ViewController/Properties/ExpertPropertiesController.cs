using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class ExpertPropertiesController : ExecPropertiesController<ServerExpertRuntime, ServerExpertSession, ExpertAttribute>, IExpertPropertiesController
    {
        public ExpertPropertiesController(GreenRmiManager rmiManager, ExpertDialogController parent, ServerExpertRuntime expertRuntime)
            : base(rmiManager, parent, expertRuntime)
        {
            this.Name = "ExpertPropertiesController";
        }
    }
}
