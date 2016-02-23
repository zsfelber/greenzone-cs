using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class DateTimePickerVCBinder : ControlVCBinder1<DateTimePicker,FieldController<DateTime>>
    {
        public DateTimePickerVCBinder(WinFormsMVContext context, DateTimePicker control, FieldController<DateTime> controller)
            : base(context, control, controller)
        {
            if (controller.Value != default(DateTime))
            {
                control.Value = controller.Value;
            }
            else
            {
                controller.Value = control.Value;
            }

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
                controller.Value = control.Value;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

    }
}
