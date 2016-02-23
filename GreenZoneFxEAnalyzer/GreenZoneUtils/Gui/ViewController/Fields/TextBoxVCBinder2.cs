using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class TextBoxVCBinder2 : ControlVCBinder1<TextBox,FieldController<string>>
    {
        public TextBoxVCBinder2(WinFormsMVContext context, TextBox control, FieldController<string> controller)
            : base(context, control, controller)
        {
            control.Text = controller.Value;
            control.ReadOnly = controller.ReadOnly;

            controller.ValueChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.ReadOnlyChanged += new PropertyChangedEventHandler(controller_ReadOnlyChanged);

            control.TextChanged += new EventHandler(control_TextChanged);
        }


        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Value;
        }

        void controller_ReadOnlyChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ReadOnly = controller.ReadOnly;
        }

        ///////////////////////////////////////////////////////////////////


        void control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Value = control.Text;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
