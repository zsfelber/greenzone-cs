using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripSplitButtonVCBinder : ToolStripDDItemVCBinder<ToolStripSplitButton>
    {
        public ToolStripSplitButtonVCBinder(WinFormsMVContext context, ToolStripSplitButton control, ComboController controller)
            : base(context, control, controller)
        {

            control.ButtonClick += new EventHandler(control_ButtonClick);
        }




        /////////////////////////////////////////////////////////////////////


        protected override void control_Click(object sender, EventArgs e)
        {
        }

        void control_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Press();
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
