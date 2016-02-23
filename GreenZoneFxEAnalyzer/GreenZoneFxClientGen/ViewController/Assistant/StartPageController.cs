using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class StartPageController : StartPageControllerBase
    {

        public StartPageController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
    }
}
