using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class SplitterPanelVCBinder : ControlVCBinder1<SplitterPanel,Controller>
    {
        public SplitterPanelVCBinder(WinFormsMVContext context, SplitterPanel control, Controller controller)
            : base(context, control, controller)
        {
        }

    }
}
