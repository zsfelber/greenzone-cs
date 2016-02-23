using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.IO;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class ImportMetatraderPage1Controller : ImportMetatraderPage1ControllerBase
    {

        public ImportMetatraderPage1Controller(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
		{
        }


    }
}
