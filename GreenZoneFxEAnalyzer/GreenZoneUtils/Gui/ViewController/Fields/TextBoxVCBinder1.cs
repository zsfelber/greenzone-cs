using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class TextBoxVCBinder1 : ControlVCBinder1<TextBox,LabelledController>
    {
        public TextBoxVCBinder1(WinFormsMVContext context, TextBox control, LabelledController controller)
            : base(context, control, controller)
        {
            if (controller.Text != null)
            {
                control.Text = controller.Text;
            }
            else
            {
                controller.Text = control.Text;
            }

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            
            control.TextChanged += new EventHandler(control_TextChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }


        ///////////////////////////////////////////////////////////////////


        void control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Text = control.Text;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
