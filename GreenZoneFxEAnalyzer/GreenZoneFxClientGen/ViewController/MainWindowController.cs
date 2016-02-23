using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;

using GreenZoneUtil.ViewController;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;
using GreenZoneFxEngine.ViewController.Assistant;
using System.Collections;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class MainWindowController : MainWindowControllerBase
    {
        public MainWindowController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }


        public void SaveSessionAsync()
        {
            rmiManager.InvokeMethodFar(this, 1, null, /*sync:*/false);
        }

    }

}
