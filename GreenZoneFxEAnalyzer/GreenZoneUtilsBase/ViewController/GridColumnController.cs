using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class GridColumnController : LabelledController
    {
        public event PropertyChangedEventHandler DataPropertyNameChanged;
        public event ControllerEventHandler ContentClicked;
        public event ControllerEventHandler ContentDoubleClicked;

        public GridColumnController(GreenRmiManager rmiManager, GridController parent, Type columnType)
            : base(rmiManager, (Controller)null)
        {
            this.columnType = columnType;
            parent.Add(this);
            dependencies.Add(parent);
        }

        public GridColumnController(GreenRmiManager rmiManager, GridController parent, Type columnType, string text)
            : base(rmiManager, null, text)
        {
            this.columnType = columnType;
            parent.Add(this);
            dependencies.Add(parent);
        }

        public GridColumnController(GreenRmiManager rmiManager, GridController parent, Type columnType, string text, int image)
            : base(rmiManager, null, text, image)
        {
            this.columnType = columnType;
            parent.Add(this);
            dependencies.Add(parent);
        }

        public GridColumnController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Type columnType = (Type)buffer.ChangedProps[PROPERTY_16_COLUMNTYPE_ID];
            this.columnType = columnType;
            Parent.Add(this);
            //dependencies.Add(parent);
        }

        protected GridColumnController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            columnType = (Type) info.GetValue("ColumnType", typeof(Type));
            DataPropertyName = (string) info.GetValue("DataPropertyName", typeof(string));
            dependencies.Add(Parent);
        }

        public new GridController Parent
        {
            get
            {
                return (GridController)base.Parent;
            }
        }

        readonly Type columnType;
        const int PROPERTY_16_COLUMNTYPE_ID = 16;
        public Type ColumnType
        {
            get
            {
                return columnType;
            }
        }

        string dataPropertyName;
        const int PROPERTY_17_DATAPROPERTYNAME_ID = 17;
        public string DataPropertyName
        {
            get
            {
                return dataPropertyName;
            }
            set
            {
                if (dataPropertyName != value)
                {
                    dataPropertyName = value;
                    changed[PROPERTY_17_DATAPROPERTYNAME_ID] = true;
                    somethingChanged = true;
                    if (DataPropertyNameChanged != null)
                    {
                        DataPropertyNameChanged(this, new PropertyChangedEventArgs("DataPropertyName", value));
                    }
                }
            }
        }

        public int Index
        {
            get
            {
                int ind = Parent.Columns.IndexOf(this);
                return ind;
            }
        }

        public GridCellController this[int row]
        {
            get
            {
                return Parent[row][Index];
            }
            set
            {
                Parent[row][Index] = value;
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
                case PROPERTY_16_COLUMNTYPE_ID:
                    return ColumnType;
                case PROPERTY_17_DATAPROPERTYNAME_ID:
                    return DataPropertyName;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_COLUMNTYPE_ID:
                    break;
                case PROPERTY_17_DATAPROPERTYNAME_ID:
                    DataPropertyName = (string)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ColumnType", ColumnType);
            info.AddValue("DataPropertyName", DataPropertyName);
        }
  
    }
}
