using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class LabelVCBinder2 : ControlVCBinder1<Label,FieldController<string>>
    {
        public LabelVCBinder2(WinFormsMVContext context, Label control, FieldController<string> controller)
            : base(context, control, controller)
        {
            control.Text = controller.Value;

            controller.ValueChanged += new PropertyChangedEventHandler(controller_TextChanged);

        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Value;
        }


        ///////////////////////////////////////////////////////////////////


    }
}
