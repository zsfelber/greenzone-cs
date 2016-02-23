using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class GridCellController : LabelledFieldController
    {
        public event ControllerEventHandler ContentClicked;
        public event ControllerEventHandler ContentDoubleClicked;

        public GridCellController(GreenRmiManager rmiManager, GridRowController row)
            : base(rmiManager, (Controller)null)
        {
            this.position = new GridCellPosition(row, column);
            this.row = row;
            this.column = row.NextColumn;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GridRowController row, string text)
            : base(rmiManager, null, text)
        {
            this.position = new GridCellPosition(row, column);
            this.row = row;
            this.column = row.NextColumn;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GridRowController row, string text, int image)
            : base(rmiManager, null, text, image)
        {
            this.position = new GridCellPosition(row, column);
            this.row = row;
            this.column = row.NextColumn;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }


        public GridCellController(GreenRmiManager rmiManager, GridRowController row, GridColumnController column)
            : base(rmiManager, (Controller)null)
        {
            this.position = new GridCellPosition(row, column);
            this.row = row;
            this.column = column;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GridRowController row, GridColumnController column, string text)
            : base(rmiManager, null, text)
        {
            this.position = new GridCellPosition(row, column);
            this.row = row;
            this.column = column;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GridRowController row, GridColumnController column, string text, int image)
            : base(rmiManager, null, text, image)
        {
            this.position = new GridCellPosition(row, column);
            this.row = row;
            this.column = column;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }


        public GridCellController(GreenRmiManager rmiManager, GridCellPosition position)
            : base(rmiManager, (Controller)null)
        {
            this.position = position;
            this.row = position.Row;
            this.column = position.Column;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GridCellPosition position, string text)
            : base(rmiManager, null, text)
        {
            this.position = position;
            this.row = position.Row;
            this.column = position.Column;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GridCellPosition position, string text, int image)
            : base(rmiManager, null, text, image)
        {
            this.position = position;
            this.row = position.Row;
            this.column = position.Column;
            row.Add(this);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        public GridCellController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            this.row = (GridRowController)buffer.ChangedProps[PROPERTY_17_ROW_ID];
            this.column = (GridColumnController)buffer.ChangedProps[PROPERTY_18_COLUMN_ID];
            this.position = new GridCellPosition(this.row, this.column);
            dependencies.Add(row);
            dependencies.Add(column);
        }

        protected GridCellController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            row = (GridRowController) info.GetValue("Row", typeof(GridRowController));
            column = (GridColumnController) info.GetValue("Column", typeof(GridColumnController));
            dependencies.Add(row);
            dependencies.Add(column);
        }

        
        readonly GridCellPosition position;
        public GridCellPosition Position
        {
            get
            {
                return position;
            }
        }

        readonly GridRowController row;
        const int PROPERTY_17_ROW_ID = 17;
        public GridRowController Row
        {
            get
            {
                return row;
            }
        }

        readonly GridColumnController column;
        const int PROPERTY_18_COLUMN_ID = 18;
        public GridColumnController Column
        {
            get
            {
                return column;
            }
        }

        public override object Value
        {
            get
            {
                if (row.DataBoundObject != null)
                {
                    object v = GreenZoneUtilsBase.GetProperty(rmiManager.Resolver, row.DataBoundObject, column.DataPropertyName);
                    return v;
                }
                else
                {
                    return base.Value;
                }
            }
            set
            {
                if (row.DataBoundObject != null)
                {
                    GreenZoneUtilsBase.SetProperty(rmiManager.Resolver, row.DataBoundObject, column.DataPropertyName, value);
                }
                else
                {
                    base.Value = value;
                }
            }
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
            }
            Row.OnContentClick(args);
            Column.OnContentClick(args);
            args.Consume();
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
            }
            Row.OnContentDoubleClick(args);
            Column.OnContentDoubleClick(args);
            args.Consume();
        }
        
        internal override void OnClick(ControllerEventArgs args)
        {
            base.OnClick(args);
            Row.OnClick(args);
            Column.OnClick(args);
        }

        internal override void OnDoubleClick(ControllerEventArgs args)
        {
            base.OnDoubleClick(args);
            Row.OnDoubleClick(args);
            Column.OnDoubleClick(args);
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_17_ROW_ID:
                    return Row;
                case PROPERTY_18_COLUMN_ID:
                    return Column;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_17_ROW_ID:
                    break;
                case PROPERTY_18_COLUMN_ID:
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Row", Row);
            info.AddValue("Column", Column);
        }
    }

    public struct GridCellPosition
    {
        public GridCellPosition(GridRowController row, GridColumnController column)
        {
            this.row = row;
            this.column = column;
        }

        readonly GridRowController row;
        public GridRowController Row
        {
            get
            {
                return row;
            }
        }

        readonly GridColumnController column;
        public GridColumnController Column
        {
            get
            {
                return column;
            }
        }

        public override bool Equals(object obj)
        {
            GridCellPosition o = (GridCellPosition)obj;
            return row == o.row && column == o.column;
        }

        public override int GetHashCode()
        {
            return row.GetHashCode() + column.GetHashCode();
        }
    }
}
