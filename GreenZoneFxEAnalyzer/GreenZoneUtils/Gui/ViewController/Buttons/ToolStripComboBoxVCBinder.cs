using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripComboBoxVCBinder : ControlVCBinder2<ToolStripComboBox,ComboController>
    {

        public ToolStripComboBoxVCBinder(WinFormsMVContext context, ToolStripComboBox control, ComboController controller)
            : base(context, control, controller)
        {

            foreach (var item in controller.Items)
            {
                AddItem(item);
            }

            control.SelectedItem = controller.SelectedItem;

            controller.SelectedItemChanged += new PropertyChangedEventHandler(controller_SelectedItemChanged);
            controller.ItemsChanged += new PropertyChangedEventHandler(controller_ItemsChanged);
            controller.ItemAdded += new ListChangedEventHandler(controller_ItemAdded);

            control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
        }

        void AddItem(object item, int index = -1)
        {
            if (index == -1)
            {
                control.Items.Add(item);
            }
            else
            {
                control.Items.Insert(index, item);
            }
        }

        void controller_SelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            control.SelectedItem = controller.SelectedItem;
        }

        void controller_ItemsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Items.Clear();
            foreach (var item in controller.Items)
            {
                AddItem(item);
            }
        }

        void controller_ItemAdded(object sender, ListChangedEventArgs e)
        {
            AddItem(e.Element);
        }

        
        
        ////////////////////////////////////////////////////////////////////////// 



        void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.SelectedItem = control.SelectedItem;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
