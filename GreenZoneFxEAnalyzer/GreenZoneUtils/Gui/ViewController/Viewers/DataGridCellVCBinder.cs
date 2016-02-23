using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class DataGridCellVCBinder : ControlVCBinder4<DataGridViewCell,GridCellController>
    {

        public DataGridCellVCBinder(WinFormsMVContext context, DataGridViewCell control, GridCellController controller)
            : base(context, control, controller)
        {
            controller.ValueChanged += new PropertyChangedEventHandler(controller_ValueChanged);
        }

        void controller_ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Value = controller.Value;
        }


        /////////////////////////////////////////////////////////////////////
        
        
    }
}
