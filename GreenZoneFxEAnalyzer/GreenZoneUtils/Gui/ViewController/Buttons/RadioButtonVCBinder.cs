using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class RadioButtonVCBinder : BaseButtonVCBinder<RadioButton, RadioButtonController>
    {

        public RadioButtonVCBinder(WinFormsMVContext context, RadioButton control, RadioButtonController controller)
            : base(context, control, controller)
        {
            control.Checked = controller.Checked;

            controller.CheckedChanged += new PropertyChangedEventHandler(controller_CheckedChanged);
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

        void controller_CheckedChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Checked = controller.Checked;
        }
    }
}
