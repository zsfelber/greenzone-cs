using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.Util;
using System.Windows.Forms;
using System.Collections;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    // TODO Buffer for big tables
    public interface IGridController : IController
    {
    }

    public class GridController : Controller, IGridController
    {
        public event PropertyChangedEventHandler RowsChanged;
        public event ListChangedEventHandler RowInserted;
        public event ListChangedEventHandler RowRemovedAt;
        public event PropertyChangedEventHandler SelectedRowChanged;
        public event PropertyChangedEventHandler SelectedRowsChanged;
        public event PropertyChangedEventHandler ColumnsChanged;
        public event ListChangedEventHandler ColumnInserted;
        public event ListChangedEventHandler ColumnRemovedAt;
        public event PropertyChangedEventHandler ColumnVisibleChanged;
        public event PropertyChangedEventHandler ColumnEnabledChanged;
        public event PropertyChangedEventHandler ColumnNameChanged;
        public event ControllerEventHandler ColumnContentClicked;
        public event ControllerEventHandler ColumnContentDoubleClicked;
        public event PropertyChangedEventHandler SelectedColumnChanged;
        public event PropertyChangedEventHandler SelectedColumnsChanged;
        public event PropertyChangedEventHandler SelectedCellsChanged;
        public event PropertyChangedEventHandler SelectedCellChanged;
        public event PropertyChangedEventHandler CurrentCellChanged;

        public PropertyChangedEventHandler columnVisibleChanged;
        public PropertyChangedEventHandler columnEnabledChanged;
        public PropertyChangedEventHandler columnNameChanged;
        public ControllerEventHandler columnContentClicked;
        public ControllerEventHandler columnContentDoubleClicked;

        public GridController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            rowsUm = rows.AsReadOnly();
            columnsUm = columns.AsReadOnly();
            columnVisibleChanged = new PropertyChangedEventHandler(column_VisibleChanged);
            columnEnabledChanged = new PropertyChangedEventHandler(column_EnabledChanged);
            columnNameChanged = new PropertyChangedEventHandler(column_NameChanged);
            columnContentClicked = new ControllerEventHandler(column_ContentClicked);
            columnContentDoubleClicked = new ControllerEventHandler(column_ContentDoubleClicked);
        }

        public GridController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            rowsUm = rows.AsReadOnly();
            columnsUm = columns.AsReadOnly();
            columnVisibleChanged = new PropertyChangedEventHandler(column_VisibleChanged);
            columnEnabledChanged = new PropertyChangedEventHandler(column_EnabledChanged);
            columnNameChanged = new PropertyChangedEventHandler(column_NameChanged);
            columnContentClicked = new ControllerEventHandler(column_ContentClicked);
            columnContentDoubleClicked = new ControllerEventHandler(column_ContentDoubleClicked);
        }

        protected GridController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Rows = (IList<GridRowController>) info.GetValue("Rows", typeof(IList<GridRowController>));
            Columns = (IList<GridColumnController>) info.GetValue("Columns", typeof(IList<GridColumnController>));
            SelectedRows = (IList<GridRowController>) info.GetValue("SelectedRows", typeof(IList<GridRowController>));
            SelectedColumns = (IList<GridColumnController>) info.GetValue("SelectedColumns", typeof(IList<GridColumnController>));
            SelectedCells = (IList<GridCellController>) info.GetValue("SelectedCells", typeof(IList<GridCellController>));
            CurrentCell = (GridCellController) info.GetValue("CurrentCell", typeof(GridCellController));
        }


        List<GridRowController> rows = new List<GridRowController>();
        IList<GridRowController> rowsUm;
        const int PROPERTY_14_ROWS_ID = 14;
        public IList<GridRowController> Rows
        {
            get
            {
                return rowsUm;
            }
            set
            {
                if (rows != value)
                {
                    rows = (List<GridRowController>)value;
                    rowsUm = rows.AsReadOnly();
                    changed[PROPERTY_14_ROWS_ID] = true;
                    somethingChanged = true;
                    if (RowsChanged != null)
                    {
                        RowsChanged(this, new PropertyChangedEventArgs("Rows", value));
                    }
                }
            }
        }

        List<GridColumnController> columns= new List<GridColumnController>();
        IList<GridColumnController> columnsUm;
        const int PROPERTY_15_COLUMNS_ID = 15;
        public IList<GridColumnController> Columns
        {
            get
            {
                return columnsUm;
            }
            set
            {
                if (columns != value)
                {
                    if (columns != null)
                    {
                        foreach (var column in columns)
                        {
                            column.VisibleChanged -= columnVisibleChanged;
                            column.EnabledChanged -= columnEnabledChanged;
                            column.NameChanged -= columnNameChanged;
                            column.ContentClicked -= columnContentClicked;
                            column.ContentDoubleClicked -= columnContentDoubleClicked;
                        }
                    }
                    columns = (List<GridColumnController>)value;
                    columnsUm = columns.AsReadOnly();
                    changed[PROPERTY_15_COLUMNS_ID] = true;
                    somethingChanged = true;
                    if (ColumnsChanged != null)
                    {
                        ColumnsChanged(this, new PropertyChangedEventArgs("Columns", value));
                    }
                    foreach (var column in columns)
                    {
                        column.VisibleChanged += columnVisibleChanged;
                        column.EnabledChanged += columnEnabledChanged;
                        column.NameChanged += columnNameChanged;
                        column.ContentClicked += columnContentClicked;
                        column.ContentDoubleClicked += columnContentDoubleClicked;
                    }
                }
            }
        }

        List<GridRowController> selectedRows;
        IList<GridRowController> selectedRowsUm;
        const int PROPERTY_16_SELECTEDROWS_ID = 16;
        public IList<GridRowController> SelectedRows
        {
            get
            {
                return selectedRowsUm;
            }
            set
            {
                if (selectedRows != value)
                {
                    selectedRows = (List<GridRowController>)value;
                    selectedRowsUm = selectedRows.AsReadOnly();
                    changed[PROPERTY_16_SELECTEDROWS_ID] = true;
                    somethingChanged = true;
                    if (SelectedRowsChanged != null)
                    {
                        SelectedRowsChanged(this, new PropertyChangedEventArgs("SelectedRows", value));
                    }
                    if (value.Count == 1 && SelectedRowChanged != null)
                    {
                        SelectedRowChanged(this, new PropertyChangedEventArgs("SelectedRow", value));
                    }
                }
            }
        }

        List<GridColumnController> selectedColumns;
        IList<GridColumnController> selectedColumnsUm;
        const int PROPERTY_17_SELECTEDCOLUMNS_ID = 17;
        public IList<GridColumnController> SelectedColumns
        {
            get
            {
                return selectedColumnsUm;
            }
            set
            {
                if (selectedColumns != value)
                {
                    selectedColumns = (List<GridColumnController>)value;
                    selectedColumnsUm = selectedColumns.AsReadOnly();
                    changed[PROPERTY_17_SELECTEDCOLUMNS_ID] = true;
                    somethingChanged = true;
                    if (SelectedColumnsChanged != null)
                    {
                        SelectedColumnsChanged(this, new PropertyChangedEventArgs("SelectedColumns", value));
                    }
                    if (value.Count == 1 && SelectedColumnChanged != null)
                    {
                        SelectedColumnChanged(this, new PropertyChangedEventArgs("SelectedColumn", value));
                    }
                }
            }
        }

        List<GridCellController> selectedCells;
        IList<GridCellController> selectedCellsUm;
        const int PROPERTY_18_SELECTEDCELLS_ID = 18;
        public IList<GridCellController> SelectedCells
        {
            get
            {
                return selectedCells;
            }
            set
            {
                if (selectedCells != value)
                {
                    selectedCells = (List<GridCellController>)value;
                    selectedCellsUm = selectedCells.AsReadOnly();
                    changed[PROPERTY_18_SELECTEDCELLS_ID] = true;
                    somethingChanged = true;
                    if (SelectedCellsChanged != null)
                    {
                        SelectedCellsChanged(this, new PropertyChangedEventArgs("SelectedCells", value));
                    }
                    if (value.Count == 1 && SelectedCellChanged != null)
                    {
                        SelectedCellChanged(this, new PropertyChangedEventArgs("SelectedCell", value));
                    }
                }
            }
        }

        public GridRowController SelectedRow
        {
            get
            {
                if (selectedRows.Count == 0)
                {
                    return null;
                }
                else
                {
                    return selectedRows[0];
                }
            }
            set
            {
                if (selectedRows.Count != 1 || selectedRows[0] != value)
                {
                    selectedRows = new List<GridRowController>();
                    selectedRows.Add(value);
                    if (SelectedRowChanged != null)
                    {
                        SelectedRowChanged(this, new PropertyChangedEventArgs("SelectedRow", value));
                    }
                    if (SelectedRowsChanged != null)
                    {
                        SelectedRowsChanged(this, new PropertyChangedEventArgs("SelectedRows", value));
                    }
                }
            }
        }


        public GridColumnController SelectedColumn
        {
            get
            {
                if (selectedColumns.Count == 0)
                {
                    return null;
                }
                else
                {
                    return selectedColumns[0];
                }
            }
            set
            {
                if (selectedColumns.Count != 1 || selectedColumns[0] != value)
                {
                    selectedColumns = new List<GridColumnController>();
                    selectedColumns.Add(value);
                    if (SelectedColumnChanged != null)
                    {
                        SelectedColumnChanged(this, new PropertyChangedEventArgs("SelectedColumn", value));
                    }
                    if (SelectedColumnsChanged != null)
                    {
                        SelectedColumnsChanged(this, new PropertyChangedEventArgs("SelectedColumns", value));
                    }
                }
            }
        }

        public GridCellController SelectedCell
        {
            get
            {
                if (selectedCells.Count == 0)
                {
                    return null;
                }
                else
                {
                    return selectedCells[0];
                }
            }
            set
            {
                if (selectedCells.Count != 1 || selectedCells[0] != value)
                {
                    selectedCells = new List<GridCellController>();
                    selectedCells.Add(value);
                    if (SelectedCellChanged != null)
                    {
                        SelectedCellChanged(this, new PropertyChangedEventArgs("SelectedCell", value));
                    }
                    if (SelectedCellsChanged != null)
                    {
                        SelectedCellsChanged(this, new PropertyChangedEventArgs("SelectedCells", value));
                    }
                }
            }
        }

        public GridRowController CurrentRow
        {
            get
            {
                if (currentCell != null)
                {
                    return currentCell.Row;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (currentCell != null)
                {
                    CurrentCell = new GridCellController(rmiManager, new GridCellPosition(value, currentCell.Column));
                }
                else
                {
                    CurrentCell = new GridCellController(rmiManager, new GridCellPosition(value, null));
                }
            }
        }

        public GridColumnController CurrentColumn
        {
            get
            {
                if (currentCell != null)
                {
                    return currentCell.Column;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (currentCell != null)
                {
                    CurrentCell = new GridCellController(rmiManager, new GridCellPosition(currentCell.Row, value));
                }
                else
                {
                    CurrentCell = new GridCellController(rmiManager, new GridCellPosition(null, value));
                }
            }
        }

        GridCellController currentCell;
        const int PROPERTY_19_CURRENTCELL_ID = 19;
        public GridCellController CurrentCell
        {
            get
            {
                return currentCell;
            }
            set
            {
                currentCell = value;
                changed[PROPERTY_19_CURRENTCELL_ID] = true;
                somethingChanged = true;
                if (CurrentCellChanged != null)
                {
                    CurrentCellChanged(this, new PropertyChangedEventArgs("CurrentCell", value));
                }
            }
        }

        public IList<object> Items
        {
            get
            {
                List<object> items = new List<object>();
                foreach (var r in rows)
                {
                    items.Add(r.DataBoundObject);
                }
                return items;
            }
            set
            {
                ClearRows();
                foreach (var item in value)
                {
                    GridRowController row = new GridRowController(rmiManager, item);
                    Add(row);
                }
            }
        }

        public IList SelectedItems
        {
            get
            {
                List<object> selectedItems = new List<object>();
                foreach (var r in selectedRows)
                {
                    selectedItems.Add(r.DataBoundObject);
                }
                return selectedItems;
            }
            set
            {
                List<GridRowController> selectedRows = new List<GridRowController>();
                foreach (var row in rows)
                {
                    if (value.Contains(row.DataBoundObject))
                    {
                        selectedRows.Add(row);
                    }
                }
                SelectedRows = selectedRows;
            }
        }

        public object SelectedItem
        {
            get
            {
                if (selectedRows.Count == 0)
                {
                    return null;
                }
                else
                {
                    return selectedRows[0].DataBoundObject;
                }
            }
            set
            {
                if (this.selectedRows.Count != 1 || this.selectedRows[0].DataBoundObject != value)
                {
                    List<GridRowController> selectedRows = new List<GridRowController>();
                    foreach (var row in rows)
                    {
                        if (row.DataBoundObject == value)
                        {
                            selectedRows.Add(row);
                            break;
                        }
                    }
                    SelectedRows = selectedRows;
                }
            }
        }

        public int RowCount
        {
            get
            {
                return rows.Count;
            }
        }

        public int ColumnCount
        {
            get
            {
                return columns.Count;
            }
        }

        public GridRowController this[int row]
        {
            get
            {
                return rows[row];
            }
        }

        public GridCellController this[int row, int column]
        {
            get
            {
                return rows[row][column];
            }
        }

        public virtual void ClearRows()
        {
            Controls = new List<Controller>(columns);
            Rows = new List<GridRowController>();
            SelectedRows = new List<GridRowController>();
            SelectedCells = new List<GridCellController>();
        }

        public virtual void ClearColumns()
        {
            Controls = new List<Controller>(rows);
            Columns = new List<GridColumnController>();
            SelectedColumns = new List<GridColumnController>();
            SelectedCells = new List<GridCellController>();
        }

        public override void Clear()
        {
            base.Clear();
            Rows = new List<GridRowController>();
            Columns = new List<GridColumnController>();
            SelectedRows = new List<GridRowController>();
            SelectedColumns = new List<GridColumnController>();
            SelectedCells = new List<GridCellController>();
        }

        public virtual void ClearSelection()
        {
            SelectedRows = new List<GridRowController>();
            SelectedColumns = new List<GridColumnController>();
            SelectedCells = new List<GridCellController>();
        }

        public override void Add(Controller control)
        {
            throw new NotSupportedException();
        }

        public override void Insert(int index, Controller control)
        {
            throw new NotSupportedException();
        }

        public override void Remove(Controller control)
        {
            throw new NotSupportedException();
        }

        public override void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }



        public void AddItem(object item)
        {
            GridRowController row = new GridRowController(rmiManager, item);
            Add(row);
        }

        public void InsertItem(int index, object item)
        {
            GridRowController row = new GridRowController(rmiManager, item);
            Insert(index, row);
        }

        public void RemoveItem(object item)
        {
            foreach (var row in rows)
            {
                if (row.DataBoundObject == item)
                {
                    Remove(row);
                    break;
                }
            }
        }


        public void Add(GridRowController row)
        {
            rows.Remove(row);
            rows.Add(row);
            if (RowInserted != null)
            {
                RowInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, rows.Count - 1, row));
            }
            base.Add(row);
        }

        public void Insert(int index, GridRowController row)
        {
            rows.Remove(row);
            rows.Insert(index, row);
            if (RowInserted != null)
            {
                RowInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, index, row));
            }
            //! Add
            base.Add(row);
        }

        public void Remove(GridRowController row)
        {
            int remind = rows.IndexOf(row);
            rows.Remove(row);
            if (RowRemovedAt != null)
            {
                RowRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, remind, row));
            }
            base.Remove(row);
        }

        public void RemoveRowAt(int index)
        {
            GridRowController remitm = rows[index];
            rows.RemoveAt(index);
            if (RowRemovedAt != null)
            {
                RowRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, index, remitm));
            }
            //!Remove
            base.Remove(remitm);
        }



        public virtual void Add(GridColumnController column)
        {
            column.VisibleChanged -= columnVisibleChanged;
            column.EnabledChanged -= columnEnabledChanged;
            column.NameChanged -= columnNameChanged;
            column.ContentClicked -= columnContentClicked;
            column.ContentDoubleClicked -= columnContentDoubleClicked;
            column.VisibleChanged += columnVisibleChanged;
            column.EnabledChanged += columnEnabledChanged;
            column.NameChanged += columnNameChanged;
            column.ContentClicked += columnContentClicked;
            column.ContentDoubleClicked += columnContentDoubleClicked;

            columns.Remove(column);
            columns.Add(column);
            if (ColumnInserted != null)
            {
                ColumnInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, columns.Count - 1, column));
            }
            base.Add(column);
        }

        public virtual void Insert(int index, GridColumnController column)
        {
            column.VisibleChanged -= columnVisibleChanged;
            column.EnabledChanged -= columnEnabledChanged;
            column.NameChanged -= columnNameChanged;
            column.ContentClicked -= columnContentClicked;
            column.ContentDoubleClicked -= columnContentDoubleClicked;
            column.VisibleChanged += columnVisibleChanged;
            column.EnabledChanged += columnEnabledChanged;
            column.NameChanged += columnNameChanged;
            column.ContentClicked += columnContentClicked;
            column.ContentDoubleClicked += columnContentDoubleClicked;

            columns.Remove(column);
            columns.Insert(index, column);
            if (ColumnInserted != null)
            {
                ColumnInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, index, column));
            }
            //! Add
            base.Add(column);
        }

        public virtual void Remove(GridColumnController column)
        {
            int remind = columns.IndexOf(column);
            columns.Remove(column);
            if (ColumnRemovedAt != null)
            {
                ColumnRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, remind, column));
            }
            base.Remove(column);
        }

        public virtual void RemoveColumnAt(int index)
        {
            GridColumnController remitm = columns[index];
            columns.RemoveAt(index);
            if (ColumnRemovedAt != null)
            {
                ColumnRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, index, remitm));
            }
            //!Remove
            base.Remove(remitm);
        }


        ////////////////////////////////////////////////////////




        void column_VisibleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ColumnVisibleChanged != null)
            {
                ColumnVisibleChanged(sender, e);
            }
        }

        void column_EnabledChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ColumnEnabledChanged != null)
            {
                ColumnEnabledChanged(sender, e);
            }
        }

        void column_NameChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ColumnNameChanged != null)
            {
                ColumnNameChanged(sender, e);
            }
        }

        void column_ContentClicked(object sender, ControllerEventArgs e)
        {
            if (ColumnContentClicked != null)
            {
                ColumnContentClicked(sender, e);
            }
        }
        void column_ContentDoubleClicked(object sender, ControllerEventArgs e)
        {
            if (ColumnContentDoubleClicked != null)
            {
                ColumnContentDoubleClicked(sender, e);
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_ROWS_ID:
                    return Rows;
                case PROPERTY_15_COLUMNS_ID:
                    return Columns;
                case PROPERTY_16_SELECTEDROWS_ID:
                    return SelectedRows;
                case PROPERTY_17_SELECTEDCOLUMNS_ID:
                    return SelectedColumns;
                case PROPERTY_18_SELECTEDCELLS_ID:
                    return SelectedCells;
                case PROPERTY_19_CURRENTCELL_ID:
                    return CurrentCell;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_ROWS_ID:
                    Rows = new BridgeCollection<GridRowController>((ArrayList)value);
                    break;
                case PROPERTY_15_COLUMNS_ID:
                    Columns = new BridgeCollection<GridColumnController>((ArrayList)value);
                    break;
                case PROPERTY_16_SELECTEDROWS_ID:
                    SelectedRows = new BridgeCollection<GridRowController>((ArrayList)value);
                    break;
                case PROPERTY_17_SELECTEDCOLUMNS_ID:
                    SelectedColumns = new BridgeCollection<GridColumnController>((ArrayList)value);
                    break;
                case PROPERTY_18_SELECTEDCELLS_ID:
                    SelectedCells = new BridgeCollection<GridCellController>((ArrayList)value);
                    break;
                case PROPERTY_19_CURRENTCELL_ID:
                    CurrentCell = (GridCellController)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Rows", Rows);
            info.AddValue("Columns", Columns);
            info.AddValue("SelectedRows", SelectedRows);
            info.AddValue("SelectedColumns", SelectedColumns);
            info.AddValue("SelectedCells", SelectedCells);
            info.AddValue("CurrentCell", CurrentCell);
        }
    }
}