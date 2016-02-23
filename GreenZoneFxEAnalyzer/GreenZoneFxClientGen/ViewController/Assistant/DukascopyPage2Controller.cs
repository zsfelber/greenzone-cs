using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Threading;
using GreenZoneFxEngine.Types;
using System.Net;
using GreenZoneFxEngine.Trading;
using System.IO;

using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{

    public class DukascopyPage2Controller : DukascopyPage2ControllerBase
    {

        public DukascopyPage2Controller(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

   }
}