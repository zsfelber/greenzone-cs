using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Windows.Forms;

namespace GreenZoneUtil.Gui.ViewController
{
    public class ErrorProviderVCBinder : ControlVCBinder<ErrorProvider,ChildControlMap<string>>
    {
        public ErrorProviderVCBinder(WinFormsMVContext context, ErrorProvider control, ChildControlMap<string> controller)
            : base(context, control, controller)
        {
            control.Tag = controller;
            controller.BoundControl = control;

            controller.ChildAdded += new ListChangedEventHandler(controller_ChildAdded);
            controller.ChildRemoved += new ListChangedEventHandler(controller_ChildRemoved);
        }

        void controller_ChildAdded(object sender, ListChangedEventArgs e)
        {
            Controller c = (Controller)e.Element;
            control.SetError((Control)c.BoundControl, controller[c]);
        }

        void controller_ChildRemoved(object sender, ListChangedEventArgs e)
        {
            Controller c = (Controller)e.Element;
            control.SetError((Control)c.BoundControl, null);
        }
    }
}
