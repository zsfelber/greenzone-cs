using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class PanelButtonVCBinder : BaseButtonVCBinder<Control, ButtonController>
    {

        public PanelButtonVCBinder(WinFormsMVContext context, Control control, ButtonController controller)
            : base(context, control, controller)
        {
        }

        public override Image Image
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
    }
}
