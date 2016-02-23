using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripButtonVCBinder : BaseButtonVCBinder2<ToolStripButton,ButtonController>
    {

        public ToolStripButtonVCBinder(WinFormsMVContext context, ToolStripButton control, ButtonController controller)
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
