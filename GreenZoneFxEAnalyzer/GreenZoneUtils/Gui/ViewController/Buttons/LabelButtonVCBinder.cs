using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class LabelButtonVCBinder : BaseButtonVCBinder<Label, ButtonController>
    {

        public LabelButtonVCBinder(WinFormsMVContext context, Label control, ButtonController controller)
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
