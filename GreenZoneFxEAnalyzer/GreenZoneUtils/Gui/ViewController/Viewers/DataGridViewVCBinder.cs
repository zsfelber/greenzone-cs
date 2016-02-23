using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class DataGridViewVCBinder : ControlVCBinder1<DataGridView, GridController>
    {
        public DataGridViewVCBinder(WinFormsMVContext context, DataGridView control, GridController controller)
            : base(context, control, controller)
        {

            foreach (var child in controller.Rows)
            {
                AddRow(child);
            }

            controller.RowInserted += new ListChangedEventHandler(controller_RowAdded);
            controller.RowRemovedAt += new ListChangedEventHandler(controller_RowRemovedAt);
            controller.RowsChanged += new PropertyChangedEventHandler(controller_RowsChanged);
            controller.SelectedRowsChanged += new PropertyChangedEventHandler(controller_SelectedRowsChanged);
            controller.SelectedColumnsChanged += new PropertyChangedEventHandler(controller_SelectedColumnsChanged);
            controller.CurrentCellChanged += new PropertyChangedEventHandler(controller_CurrentCellChanged);

            control.SelectionChanged += new EventHandler(control_SelectionChanged);
            control.CurrentCellChanged += new EventHandler(control_CurrentCellChanged);
        }

        protected virtual void AddRow(GridRowController child1)
        {
            DataGridViewRow newRow = new DataGridViewRow();
            DataGridRowVCBinder binder = new DataGridRowVCBinder(context, newRow, child1);
            control.Rows.Add(newRow);
        }

        void controller_RowAdded(object sender, ListChangedEventArgs e)
        {
            GridRowController child = (GridRowController)e.Element;
            AddRow(child);
        }

        void controller_RowRemovedAt(object sender, ListChangedEventArgs e)
        {
            DataGridViewRow child = control.Rows[e.NewIndex];
            control.Rows.Remove(child);
        }

        void controller_RowsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Rows.Clear();
            foreach (var child in controller.Rows)
            {
                AddRow(child);
            }
        }

        void controller_SelectedRowsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ClearSelection();
            foreach (var r in controller.SelectedRows)
            {
                control.Rows[r.Index].Selected = true;
            }
        }


        void controller_SelectedColumnsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ClearSelection();
            foreach (var c in controller.SelectedColumns)
            {
                control.Columns[c.Index].Selected = true;
            }
        }



        void controller_CurrentCellChanged(object sender, PropertyChangedEventArgs e)
        {
            control.CurrentCell = control.Rows[controller.CurrentRow.Index].Cells[controller.CurrentColumn.Index];
        }

        /////////////////////////////////////////////////////////////////////



        void control_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.ClearSelection();

                if (control.SelectionMode == DataGridViewSelectionMode.FullRowSelect || control.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
                {
                    List<GridRowController> selectedRows = new List<GridRowController>();
                    foreach (DataGridViewRow r in control.SelectedRows)
                    {
                        selectedRows.Add((GridRowController)r.Tag);
                    }
                    controller.SelectedRows = selectedRows;
                }

                if (control.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || control.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
                {
                    List<GridColumnController> selectedColumns = new List<GridColumnController>();
                    foreach (DataGridViewColumn c in control.SelectedColumns)
                    {
                        selectedColumns.Add((GridColumnController)c.Tag);
                    }
                    controller.SelectedColumns = selectedColumns;
                }

                if (control.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect || control.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect || control.SelectionMode == DataGridViewSelectionMode.CellSelect)
                {
                    List<GridCellController> selectedCells = new List<GridCellController>();
                    foreach (DataGridViewCell c in control.SelectedCells)
                    {
                        selectedCells.Add((GridCellController)c.Tag);
                    }
                    controller.SelectedCells = selectedCells;
                }
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.CurrentCell = (GridCellController)control.CurrentCell.Tag;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }


}
