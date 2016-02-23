using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;

using GreenZoneUtil.Util;

namespace GreenZoneUtil.Gui.ViewController
{
    public abstract class WormSplitContainerVCBinder : ControlVCBinder1<WormSplitContainer, MultiSplitController>
    {

        public WormSplitContainerVCBinder(WinFormsMVContext context, WormSplitContainer control, MultiSplitController controller)
            : base(context, control, controller)
        {

            foreach (var child in controller.Controls)
            {
                AddChild(child);
            }

            controller.ChildControlInserted += new ListChangedEventHandler(controller_ChildControlAdded);
            controller.ChildControlRemovedAt += new ListChangedEventHandler(controller_ChildControlRemoved);
            controller.ChildControlRemovedFrom += new ListChangedEventHandler(controller_ChildControlRemovedFrom);
            controller.ControlsChanged += new PropertyChangedEventHandler(controller_ControlsChanged);
        }


        protected abstract void AddChild(Controller child1);

        void controller_ChildControlAdded(object sender, ListChangedEventArgs e)
        {
            Controller child = (Controller)e.Element;
            AddChild(child);
        }


        void controller_ChildControlRemoved(object sender, ListChangedEventArgs e)
        {
            Control child = (Control)((Controller)e.Element).BoundControl;
            control.Remove(child);
        }

        void controller_ChildControlRemovedFrom(object sender, ListChangedEventArgs e)
        {
            Control child = (Control)((Controller)e.Element).BoundControl;
            control.RemoveFrom(child);
        }

        void controller_ControlsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Clear();
            foreach (var child in controller.Controls)
            {
                AddChild(child);
            }
        }



        /////////////////////////////////////////////////////////////////////

        
        
    }
}
