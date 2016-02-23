using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Collections;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class ChildControlMap<T> : RmiBase
    {
        public event ListChangedEventHandler ChildAdded;
        public event ListChangedEventHandler ChildRemoved;
        public event PropertyChangedEventHandler BoundControlChanged;

        public ChildControlMap(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            childrenData = new Dictionary<Controller, T>();
            childrenDataUm = new ReadOnlyDictionary<Controller, T>(childrenData);
        }

        public ChildControlMap(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            childrenData = (Dictionary<Controller, T>)buffer.ChangedProps[PROPERTY_1_CHILDRENDATA_ID];
            childrenDataUm = new ReadOnlyDictionary<Controller, T>(childrenData);
        }

        protected ChildControlMap(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            childrenData = (Dictionary<Controller,T>) info.GetValue("ChildrenData", typeof(Dictionary<Controller,T>));
            childrenDataUm = new ReadOnlyDictionary<Controller, T>(childrenData);
        }

        readonly Dictionary<Controller, T> childrenData;
        readonly IDictionary<Controller, T> childrenDataUm;
        const int PROPERTY_1_CHILDRENDATA_ID = 1;
        public IDictionary<Controller, T> ChildrenData
        {
            get
            {
                return childrenDataUm;
            }
        }

        public T this[Controller child]
        {
            get
            {
                return childrenData[child];
            }
            set
            {
                childrenData[child] = value;
                if (value != null)
                {
                    if (ChildAdded != null)
                    {
                        ChildAdded(this, new ListChangedEventArgs(ListChangedType.ItemAdded, 0, child));
                    }
                }
                else
                {
                    if (ChildRemoved != null)
                    {
                        ChildRemoved(this, new ListChangedEventArgs(ListChangedType.ItemDeleted, 0, child));
                    }
                }
            }
        }

        object boundControl;
        const int PROPERTY_2_BOUNDCONTROL_ID = 2;
        public object BoundControl
        {
            get
            {
                return boundControl;
            }
            set
            {
                if (boundControl != value)
                {
                    boundControl = value;
                    changed[PROPERTY_2_BOUNDCONTROL_ID] = true;
                    if (BoundControlChanged != null)
                    {
                        BoundControlChanged(this, new PropertyChangedEventArgs("BoundControl", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_1_CHILDRENDATA_ID:
                    return ChildrenData;
                case PROPERTY_2_BOUNDCONTROL_ID:
                    return BoundControl;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_1_CHILDRENDATA_ID:
                    childrenData.Clear();
                    foreach (DictionaryEntry e in (ArrayList)value)
                    {
                        childrenData[(Controller)e.Key] = (T)e.Value;
                    }
                    break;
                case PROPERTY_2_BOUNDCONTROL_ID:
                    BoundControl = value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ChildrenData", ChildrenData);
        }
    }
}
