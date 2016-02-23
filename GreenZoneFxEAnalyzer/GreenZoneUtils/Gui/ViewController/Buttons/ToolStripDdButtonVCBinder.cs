using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripDropDownButtonVCBinder : ToolStripDDItemVCBinder<ToolStripDropDownButton>
    {
        public ToolStripDropDownButtonVCBinder(WinFormsMVContext context, ToolStripDropDownButton control, ComboController controller)
            : base(context, control, controller)
        {
        }
    }
}
