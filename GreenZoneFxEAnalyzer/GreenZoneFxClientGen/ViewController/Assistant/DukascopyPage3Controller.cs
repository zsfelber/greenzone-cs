using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Threading;
using GreenZoneUtil.Util;
using System.Globalization;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{

    public class DukascopyPage3Controller : DukascopyPage3ControllerBase
    {

        public DukascopyPage3Controller(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
    }
}
