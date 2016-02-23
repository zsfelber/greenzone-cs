using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ButtonVCBinder : BaseButtonVCBinder<Button,ButtonController>
    {

        public ButtonVCBinder(WinFormsMVContext context, Button control, ButtonController controller)
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
    }
}
