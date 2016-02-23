using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ContextMenuStripVCBinder : ToolStripDDItemVCBinder1<ContextMenuStrip>
    {
        public ContextMenuStripVCBinder(WinFormsMVContext context, ContextMenuStrip control, ComboController controller)
            : base(context, control, controller)
        {
            controller.Visible = false;
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

        public override ToolStripItemCollection DdItems
        {
            get
            {
                return control.Items;
            }
        }
    }
}
