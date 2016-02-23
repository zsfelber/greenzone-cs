using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class FieldController : Controller
    {
        public event PropertyChangedEventHandler ValueChanged;
        public event PropertyChangedEventHandler ReadOnlyChanged;

        public FieldController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public FieldController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected FieldController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = (object)info.GetValue("Value", typeof(object));
            ReadOnly = (bool) info.GetValue("ReadOnly", typeof(bool));
        }

        object _value;
        const int PROPERTY_14_VALUE_ID = 14;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    changed[PROPERTY_14_VALUE_ID] = true;
                    somethingChanged = true;
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new PropertyChangedEventArgs("Value", value));
                    }
                }
            }
        }

        bool readOnly = true;
        const int PROPERTY_15_READONLY_ID = 15;
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                if (readOnly != value)
                {
                    readOnly = value;
                    changed[PROPERTY_15_READONLY_ID] = true;
                    somethingChanged = true;
                    if (ReadOnlyChanged != null)
                    {
                        ReadOnlyChanged(this, new PropertyChangedEventArgs("ReadOnly", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_VALUE_ID:
                    return Value;
                case PROPERTY_15_READONLY_ID:
                    return ReadOnly;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_VALUE_ID:
                    Value = value;
                    break;
                case PROPERTY_15_READONLY_ID:
                    ReadOnly = (bool)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Value", Value);
            info.AddValue("ReadOnly", ReadOnly);
        }
    }

    public class FieldController<T> : Controller
    {
        public event PropertyChangedEventHandler ValueChanged;
        public event PropertyChangedEventHandler ReadOnlyChanged;

        public FieldController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        protected FieldController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = (T) info.GetValue("Value", typeof(T));
            ReadOnly = (bool) info.GetValue("ReadOnly", typeof(bool));
        }

        T _value;
        const int PROPERTY_14_VALUE_ID = 14;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!Comparer<T>.Equals(_value, value))
                {
                    _value = value;
                    changed[PROPERTY_14_VALUE_ID] = true;
                    somethingChanged = true;
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new PropertyChangedEventArgs("Value", value));
                    }
                }
            }
        }

        bool readOnly = false;
        const int PROPERTY_15_READONLY_ID = 15;
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                if (readOnly != value)
                {
                    readOnly = value;
                    changed[PROPERTY_15_READONLY_ID] = true;
                    somethingChanged = true;
                    if (ReadOnlyChanged != null)
                    {
                        ReadOnlyChanged(this, new PropertyChangedEventArgs("ReadOnly", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_VALUE_ID:
                    return Value;
                case PROPERTY_15_READONLY_ID:
                    return ReadOnly;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_VALUE_ID:
                    Value = (T)value;
                    break;
                case PROPERTY_15_READONLY_ID:
                    ReadOnly = (bool)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Value", Value);
            info.AddValue("ReadOnly", ReadOnly);
        }
    }
}
