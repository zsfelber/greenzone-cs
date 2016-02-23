using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class CoolTabControlVCBinder : TabControlVCBinder
    {
        public CoolTabControlVCBinder(WinFormsMVContext context, CoolTabControl control, TabController controller)
            : base(context, control, controller)
        {
            control.AddTabClick += new EventHandler(CoolTabControlVCBinder_AddTabClick);
        }



        /////////////////////////////////////////////////////////////////////



        void CoolTabControlVCBinder_AddTabClick(object sender, EventArgs e)
        {
            try
            {
                controller.AddTabClick();
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
