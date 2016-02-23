using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class ProgressTrackController : LabelledFieldController<int>
    {
        public event PropertyChangedEventHandler CursorValueChanged;
        public event PropertyChangedEventHandler MinimumChanged;
        public event PropertyChangedEventHandler MaximumChanged;

        public ProgressTrackController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ProgressTrackController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ProgressTrackController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            CursorValue = (int) info.GetValue("CursorValue", typeof(int));
            Minimum = (int) info.GetValue("Minimum", typeof(int));
            Maximum = (int) info.GetValue("Maximum", typeof(int));
        }

        int cursorValue;
        const int PROPERTY_17_CURSORVALUE_ID = 17;
        public int CursorValue
        {
            get
            {
                return cursorValue;
            }
            set
            {
                if (cursorValue != value)
                {
                    cursorValue = value;
                    changed[PROPERTY_17_CURSORVALUE_ID] = true;
                    somethingChanged = true;
                    if (CursorValueChanged != null)
                    {
                        CursorValueChanged(this, new PropertyChangedEventArgs("CursorValue", value));
                    }
                }
            }
        }

        int minimum;
        const int PROPERTY_18_MINIMUM_ID = 18;
        public int Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                if (minimum != value)
                {
                    minimum = value;
                    changed[PROPERTY_18_MINIMUM_ID] = true;
                    somethingChanged = true;
                    if (MinimumChanged != null)
                    {
                        MinimumChanged(this, new PropertyChangedEventArgs("Minimum", value));
                    }
                }
            }
        }

        int maximum;
        const int PROPERTY_19_MAXIMUM_ID = 19;
        public int Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    changed[PROPERTY_19_MAXIMUM_ID] = true;
                    somethingChanged = true;
                    if (MaximumChanged != null)
                    {
                        MaximumChanged(this, new PropertyChangedEventArgs("Maximum", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_17_CURSORVALUE_ID:
                    return CursorValue;
                case PROPERTY_18_MINIMUM_ID:
                    return Minimum;
                case PROPERTY_19_MAXIMUM_ID:
                    return Maximum;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_17_CURSORVALUE_ID:
                    CursorValue = (int)value;
                    break;
                case PROPERTY_18_MINIMUM_ID:
                    Minimum = (int)value;
                    break;
                case PROPERTY_19_MAXIMUM_ID:
                    Maximum = (int)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CursorValue", CursorValue);
            info.AddValue("Minimum", Minimum);
            info.AddValue("Maximum", Maximum);
        }
    }
}
