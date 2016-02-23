using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class DataGridColumnVCBinder : ControlVCBinder3<DataGridViewColumn,GridColumnController>
    {

        public DataGridColumnVCBinder(WinFormsMVContext context, DataGridViewColumn control, GridColumnController controller)
            : base(context, control, controller)
        {

            control.HeaderText = controller.Text;

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.HeaderText = controller.Text;
        }


        /////////////////////////////////////////////////////////////////////
        
        
    }
}
