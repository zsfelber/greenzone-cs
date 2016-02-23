using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class SimpleControlVCBinder : ControlVCBinder1<Control,Controller>
    {
        public SimpleControlVCBinder(WinFormsMVContext context, Control control, Controller controller)
            : base(context, control, controller)
        {
        }
       
    }
}
