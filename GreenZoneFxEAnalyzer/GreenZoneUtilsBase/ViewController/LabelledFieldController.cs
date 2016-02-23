using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class LabelledFieldController : LabelledController
    {
        public event PropertyChangedEventHandler ValueChanged;

        public LabelledFieldController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public LabelledFieldController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
        }

        public LabelledFieldController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
        }

        public LabelledFieldController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected LabelledFieldController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = (object) info.GetValue("Value", typeof(object));
        }

        object _value;
        const int PROPERTY_16_VALUE_ID = 16;
        public virtual object Value
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
                    changed[PROPERTY_16_VALUE_ID] = true;
                    somethingChanged = true;
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new PropertyChangedEventArgs("Value", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_16_VALUE_ID:
                    return Value;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_VALUE_ID:
                    Value = value;
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
        }
    }

    public class LabelledFieldController<T> : LabelledController
    {
        public event PropertyChangedEventHandler ValueChanged;

        public LabelledFieldController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public LabelledFieldController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
        }

        public LabelledFieldController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
        }

        public LabelledFieldController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected LabelledFieldController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = (T) info.GetValue("Value", typeof(T));
        }

        T _value;
        const int PROPERTY_16_VALUE_ID = 16;
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
                    changed[PROPERTY_16_VALUE_ID] = true;
                    somethingChanged = true;
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, new PropertyChangedEventArgs("Value", value));
                    }
                }
            }
        }


        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_16_VALUE_ID:
                    return Value;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_VALUE_ID:
                    Value = (T)value;
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
        }
    }
}
