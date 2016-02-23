using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripMenuItemVCBinder : BaseButtonVCBinder2<ToolStripMenuItem,ButtonController>
    {

        public ToolStripMenuItemVCBinder(WinFormsMVContext context, ToolStripMenuItem control, ButtonController controller)
            : base(context, control, controller)
        {

            control.CheckOnClick = false;
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
