using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class TabControlVCBinder : ControlVCBinder1<TabControl, TabController>
    {
        int nosel = 0;

        public TabControlVCBinder(WinFormsMVContext context, TabControl control, TabController controller)
            : base(context, control, controller)
        {

            controller.ChildControlInserted += new ListChangedEventHandler(controller_ChildControlInserted);
            controller.ChildControlRemovedAt += new ListChangedEventHandler(controller_ChildControlRemovedAt);
            controller.ControlsChanged += new PropertyChangedEventHandler(controller_ControlsChanged);
            controller.SelectedIndexChanged += new PropertyChangedEventHandler(controller_SelectedIndexChanged);

            control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
        }

        protected virtual void AddChild(TabPageController child1, int index = -1)
        {
            nosel++;
            try
            {
                TabPage newPage = new TabPage(child1.Text);
                TabPageVCBinder binder = new TabPageVCBinder(context, newPage, child1);
                if (index == -1)
                {
                    control.TabPages.Add(newPage);
                }
                else
                {
                    control.TabPages.Insert(index, newPage);
                }
            }
            finally
            {
                nosel--;
            }
        }

        void controller_ChildControlInserted(object sender, ListChangedEventArgs e)
        {
            TabPageController child = controller.Pages[e.NewIndex];
            AddChild(child, e.NewIndex);
        }


        void controller_ChildControlRemovedAt(object sender, ListChangedEventArgs e)
        {
            nosel++;
            try
            {
                TabPage child = control.TabPages[e.NewIndex];
                control.TabPages.Remove(child);
            }
            finally
            {
                nosel--;
            }
        }

        void controller_ControlsChanged(object sender, PropertyChangedEventArgs e)
        {
            nosel++;
            try
            {
                control.TabPages.Clear();
                foreach (var child in controller.Pages)
                {
                    AddChild(child);
                }
            }
            finally
            {
                nosel--;
            }
        }

        void controller_SelectedIndexChanged(object sender, PropertyChangedEventArgs e)
        {
            control.SelectedIndex = controller.SelectedIndex;
        }


        /////////////////////////////////////////////////////////////////////


        void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                if (nosel == 0)
                {
                    controller.SelectedIndex = control.SelectedIndex;
                }
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

    }
}
