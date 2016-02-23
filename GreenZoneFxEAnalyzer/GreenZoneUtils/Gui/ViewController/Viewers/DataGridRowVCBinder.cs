using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class DataGridRowVCBinder : ControlVCBinder3<DataGridViewRow,GridRowController>
    {

        public DataGridRowVCBinder(WinFormsMVContext context, DataGridViewRow control, GridRowController controller)
            : base(context, control, controller)
        {
            int i = 0;
            foreach (var child in controller.Cells)
            {
                BindCell(i, child);
                i++;
            }

            controller.CellInserted += new ListChangedEventHandler(controller_CellInserted);
            controller.CellRemovedAt += new ListChangedEventHandler(controller_CellRemovedAt);
        }

        protected virtual void BindCell(int index, GridCellController child1)
        {
            DataGridViewCell controlCell = control.Cells[index];
            DataGridCellVCBinder binder = new DataGridCellVCBinder(context, controlCell, child1);
        }

        void controller_CellInserted(object sender, ListChangedEventArgs e)
        {
            GridCellController child = (GridCellController)controller.Cells[e.NewIndex];
            BindCell(e.NewIndex, child);
        }

        void controller_CellRemovedAt(object sender, ListChangedEventArgs e)
        {
            DataGridViewCell child = control.Cells[e.NewIndex];
            control.Cells.Remove(child);
        }


        /////////////////////////////////////////////////////////////////////
        
        
    }
}
