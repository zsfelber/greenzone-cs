using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.Util;
using System.Collections;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class GridRowController : LabelledFieldController
    {
        public event ControllerEventHandler ContentClicked;
        public event ControllerEventHandler ContentDoubleClicked;
        public event PropertyChangedEventHandler CellsChanged;
        public event ListChangedEventHandler CellChanged;
        public event ListChangedEventHandler CellInserted;
        public event ListChangedEventHandler CellRemovedAt;

        public GridRowController(GreenRmiManager rmiManager, GridController parent)
            : base(rmiManager, (Controller)null)
        {
            parent.Add(this);
            dependencies.Add(parent);
        }

        public GridRowController(GreenRmiManager rmiManager, object data)
            : base(rmiManager, (Controller)null)
        {
            DataBoundObject = data;
        }

        public GridRowController(GreenRmiManager rmiManager, GridController parent, string text)
            : base(rmiManager, null, text)
        {
            parent.Add(this);
            dependencies.Add(parent);
        }

        public GridRowController(GreenRmiManager rmiManager, GridController parent, string text, int image)
            : base(rmiManager, null, text, image)
        {
            parent.Add(this);
            dependencies.Add(parent);
        }

        public GridRowController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Parent.Add(this);
            //dependencies.Add(Parent);
        }

        protected GridRowController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Cells = (IList<GridCellController>) info.GetValue("Cells", typeof(IList<GridCellController>));
        }

        public override Controller Parent
        {
            get
            {
                return base.Parent;
            }
            protected set
            {
                base.Parent = value;
                if (DataBoundObject != null && cells == null)
                {
                    cells = new List<GridCellController>();
                    foreach (var c in Grid.Columns)
                    {
                        GridCellController cell = new GridCellController(rmiManager, new GridCellPosition(this, c));
                    }
                }
            }
        }
        
        public GridController Grid
        {
            get
            {
                return (GridController)base.Parent;
            }
        }

        public override object Value
        {
            get
            {
                return DataBoundObject;
            }
            set
            {
                DataBoundObject = value;
            }
        }

        public object DataBoundObject
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
                if (value != null && Parent != null)
                {
                    cells = new List<GridCellController>();
                    foreach (var c in Grid.Columns)
                    {
                        GridCellController cell = new GridCellController(rmiManager, new GridCellPosition(this, c));
                    }
                }
            }
        }

        List<GridCellController> cells;
        IList<GridCellController> cellsUm;
        const int PROPERTY_17_CELLS_ID = 17;
        public IList<GridCellController> Cells
        {
            get
            {
                return cellsUm;
            }
            set
            {
                if (cells != value)
                {
                    cells = (List<GridCellController>)value;
                    cellsUm = cells.AsReadOnly();
                    changed[PROPERTY_17_CELLS_ID] = true;
                    somethingChanged = true;
                    if (CellsChanged != null)
                    {
                        CellsChanged(this, new PropertyChangedEventArgs("Cells", value));
                    }
                }
            }
        }

        public int Index
        {
            get
            {
                int ind = Grid.Rows.IndexOf(this);
                return ind;
            }
        }

        public GridCellController this[int column]
        {
            get
            {
                return cells[column];
            }
            set
            {
                cells[column] = value;
                if (CellChanged != null)
                {
                    CellChanged(this, new ListChangedEventArgs(ListChangedType.ItemChanged, column, value));
                }
            }
        }

        public GridColumnController LastColumn
        {
            get {
                GridColumnController column = Grid.Columns[cells.Count-1];
                return column;
            }
        }

        public GridColumnController NextColumn
        {
            get {
                GridColumnController column = Grid.Columns[cells.Count];
                return column;
            }
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



        public void AddItem(object item)
        {
            if (DataBoundObject != null)
            {
                throw new NotSupportedException("DataBoundObject != null");
            }
            GridCellController cell = new GridCellController(rmiManager, this);
            cell.Value = item;
            Add(cell);
        }

        public void InsertItem(int index, object item)
        {
            if (DataBoundObject != null)
            {
                throw new NotSupportedException("DataBoundObject != null");
            }
            GridCellController cell = new GridCellController(rmiManager, this);
            cell.Value = item;
            Insert(index, cell);
        }

        public void RemoveItem(object item)
        {
            if (DataBoundObject != null)
            {
                throw new NotSupportedException("DataBoundObject != null");
            }
            foreach (var cell in cells)
            {
                if (cell.Value == item)
                {
                    Remove(cell);
                    break;
                }
            }
        }

        public void Add(GridCellController cell)
        {
            cells.Remove(cell);
            cells.Add(cell);
            if (CellInserted != null)
            {
                CellInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, cells.Count - 1, cell));
            }
            base.Add(cell);
        }

        public void Insert(int index, GridCellController cell)
        {
            cells.Remove(cell);
            cells.Insert(index, cell);
            if (CellInserted != null)
            {
                CellInserted(null, new ListChangedEventArgs(ListChangedType.ItemAdded, index, cell));
            }
            base.Insert(index, cell);
        }

        public void Remove(GridCellController cell)
        {
            int remind = cells.IndexOf(cell);
            cells.Remove(cell);
            if (CellRemovedAt != null)
            {
                CellRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, remind, cell));
            }
            base.Remove(cell);
        }

        public override void RemoveAt(int index)
        {
            GridCellController remitm = cells[index];
            cells.RemoveAt(index);
            if (CellRemovedAt != null)
            {
                CellRemovedAt(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, index, remitm));
            }
            base.RemoveAt(index);
        }

        public virtual void ContentClick()
        {
            OnContentClick(null);
        }

        public virtual void ContentDoubleClick()
        {
            OnContentDoubleClick(null);
        }

        internal virtual void OnContentClick(ControllerEventArgs args)
        {
            if (ContentClicked != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }

                ContentClicked(this, args);
                args.Consume();
            }
        }

        internal virtual void OnContentDoubleClick(ControllerEventArgs args)
        {
            if (ContentDoubleClicked != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }

                ContentDoubleClicked(this, args);
                args.Consume();
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_17_CELLS_ID:
                    return Cells;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_17_CELLS_ID:
                    Cells = new BridgeCollection<GridCellController>((ArrayList)value);
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Cells", Cells);
        }
    }
}
