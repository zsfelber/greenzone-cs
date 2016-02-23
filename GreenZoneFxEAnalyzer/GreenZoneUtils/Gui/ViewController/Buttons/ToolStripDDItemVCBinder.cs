using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public abstract class ToolStripDDItemVCBinder0<C> : BaseButtonVCBinder2<C, ComboController>
        where C : ToolStripItem
    {
        public ToolStripDDItemVCBinder0(WinFormsMVContext context, C control, ComboController controller)
            : base(context, control, controller)
        {

            foreach (var child in controller.Controls)
            {
                AddChild(child);
            }

            controller.ChildControlInserted += new ListChangedEventHandler(controller_ChildControlInserted);
            controller.ChildControlRemovedAt += new ListChangedEventHandler(controller_ChildControlRemovedAt);
            controller.ControlsChanged += new PropertyChangedEventHandler(controller_ControlsChanged);
        }

        public abstract ToolStripItemCollection DdItems {
            get;
        }

        void AddChild(Controller child, int index = -1)
        {
            if (child is SeparatorButtonController)
            {
                ToolStripSeparator s = new ToolStripSeparator();
                if (index == -1)
                {
                    DdItems.Add(s);
                }
                else
                {
                    DdItems.Insert(index, s);
                }
            }
            else if (child is ButtonController)
            {
                ToolStripMenuItem childControl;
                if (child is ComboController)
                {
                    childControl = new ToolStripMenuItem();
                    ToolStripMenuVCBinder binder = new ToolStripMenuVCBinder(context, childControl, (ComboController)child);
                }
                else if (child is ToggleButtonController)
                {
                    childControl = new ToolStripMenuItem();
                    ToolStripMenuCheckVCBinder binder = new ToolStripMenuCheckVCBinder(context, childControl, (ToggleButtonController)child);
                }
                else
                {
                    childControl = new ToolStripMenuItem();
                    ToolStripMenuItemVCBinder binder = new ToolStripMenuItemVCBinder(context, childControl, (ButtonController)child);
                }
                if (index == -1)
                {
                    DdItems.Add(childControl);
                }
                else
                {
                    DdItems.Insert(index, childControl);
                }
            }
        }

        void controller_ChildControlInserted(object sender, ListChangedEventArgs e)
        {
            Controller child = (Controller)controller.Controls[e.NewIndex];
            AddChild(child, e.NewIndex);
        }

        void controller_ChildControlRemovedAt(object sender, ListChangedEventArgs e)
        {
            DdItems.RemoveAt(e.NewIndex);
        }

        void controller_ControlsChanged(object sender, PropertyChangedEventArgs e)
        {
            DdItems.Clear();
            foreach (var child in controller.Controls)
            {
                AddChild(child);
            }
        }


        /////////////////////////////////////////////////////////////////////


    }


    public abstract class ToolStripDDItemVCBinder1<C> : BaseButtonVCBinder<C, ComboController>
        where C : Control
    {
        public ToolStripDDItemVCBinder1(WinFormsMVContext context, C control, ComboController controller)
            : base(context, control, controller)
        {

            foreach (var child in controller.Controls)
            {
                AddChild(child);
            }

            controller.ChildControlInserted += new ListChangedEventHandler(controller_ChildControlInserted);
            controller.ChildControlRemovedAt += new ListChangedEventHandler(controller_ChildControlRemovedAt);
            controller.ControlsChanged += new PropertyChangedEventHandler(controller_ControlsChanged);
        }

        public abstract ToolStripItemCollection DdItems
        {
            get;
        }

        void AddChild(Controller child, int index = -1)
        {
            if (child is SeparatorButtonController)
            {
                ToolStripSeparator s = new ToolStripSeparator();
                if (index == -1)
                {
                    DdItems.Add(s);
                }
                else
                {
                    DdItems.Insert(index, s);
                }
            }
            else if (child is ButtonController)
            {
                ToolStripMenuItem childControl;
                if (child is ComboController)
                {
                    childControl = new ToolStripMenuItem();
                    ToolStripMenuVCBinder binder = new ToolStripMenuVCBinder(context, childControl, (ComboController)child);
                }
                else if (child is ToggleButtonController)
                {
                    childControl = new ToolStripMenuItem();
                    ToolStripMenuCheckVCBinder binder = new ToolStripMenuCheckVCBinder(context, childControl, (ToggleButtonController)child);
                }
                else
                {
                    childControl = new ToolStripMenuItem();
                    ToolStripMenuItemVCBinder binder = new ToolStripMenuItemVCBinder(context, childControl, (ButtonController)child);
                }
                if (index == -1)
                {
                    DdItems.Add(childControl);
                }
                else
                {
                    DdItems.Insert(index, childControl);
                }
            }
        }

        void controller_ChildControlInserted(object sender, ListChangedEventArgs e)
        {
            Controller child = (Controller)controller.Controls[e.NewIndex];
            AddChild(child, e.NewIndex);
        }

        void controller_ChildControlRemovedAt(object sender, ListChangedEventArgs e)
        {
            DdItems.RemoveAt(e.NewIndex);
        }

        void controller_ControlsChanged(object sender, PropertyChangedEventArgs e)
        {
            DdItems.Clear();
            foreach (var child in controller.Controls)
            {
                AddChild(child);
            }
        }


        /////////////////////////////////////////////////////////////////////


    }

    
    
    
    public class ToolStripDDItemVCBinder<C> : ToolStripDDItemVCBinder0<C>
        where C : ToolStripDropDownItem
    {
        public ToolStripDDItemVCBinder(WinFormsMVContext context, C control, ComboController controller)
            : base(context, control, controller)
        {
        }

        public override Image Image
        {
            get
            {
                return control.Image;
            }
            set
            {
                control.Image = value;
            }
        }

        public override ToolStripItemCollection DdItems
        {
            get
            {
                return control.DropDownItems;
            }
        }

    }
}
