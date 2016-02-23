using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    // TODO Min Max
    public class NumericUpDownVCBinder : ControlVCBinder1<NumericUpDown,FieldController<int>>
    {
        public NumericUpDownVCBinder(WinFormsMVContext context, NumericUpDown control, FieldController<int> controller)
            : base(context, control, controller)
        {
            control.Value = controller.Value;

            controller.ValueChanged += new PropertyChangedEventHandler(controller_TextChanged);

            control.ValueChanged += new EventHandler(control_TextChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Value = controller.Value;
        }


        ///////////////////////////////////////////////////////////////////


        void control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Value = (int)control.Value;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

    }
}
