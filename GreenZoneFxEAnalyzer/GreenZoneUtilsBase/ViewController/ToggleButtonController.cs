using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class ToggleButtonController : ButtonController
    {
        public event PropertyChangedEventHandler CheckedChanged;
        public event PropertyChangedEventHandler CheckOnClickChanged;

        public ToggleButtonController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ToggleButtonController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
        }

        public ToggleButtonController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
        }

        public ToggleButtonController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ToggleButtonController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Checked = (bool) info.GetValue("Checked", typeof(bool));
            CheckOnClick = (bool) info.GetValue("CheckOnClick", typeof(bool));
        }

        bool _checked;
        const int PROPERTY_16_CHECKED_ID = 16;
        public virtual bool Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    changed[PROPERTY_16_CHECKED_ID] = true;
                    somethingChanged = true;
                    if (CheckedChanged != null)
                    {
                        CheckedChanged(this, new PropertyChangedEventArgs("Checked", value));
                    }
                }
            }
        }

        bool checkOnClick = true;
        const int PROPERTY_17_CHECKONCLICK_ID = 17;
        public bool CheckOnClick
        {
            get
            {
                return checkOnClick;
            }
            set
            {
                if (checkOnClick != value)
                {
                    checkOnClick = value;
                    changed[PROPERTY_17_CHECKONCLICK_ID] = true;
                    somethingChanged = true;
                    if (CheckOnClickChanged != null)
                    {
                        CheckOnClickChanged(this, new PropertyChangedEventArgs("CheckOnClick", value));
                    }
                }
            }
        }

        internal override void OnPress(ControllerEventArgs args)
        {
            base.OnPress(args);
            if (checkOnClick)
            {
                Checked = !_checked;
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_16_CHECKED_ID:
                    return Checked;
                case PROPERTY_17_CHECKONCLICK_ID:
                    return CheckOnClick;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_CHECKED_ID:
                    Checked = (bool)value;
                    break;
                case PROPERTY_17_CHECKONCLICK_ID:
                    CheckOnClick = (bool)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Checked", Checked);
            info.AddValue("CheckOnClick", CheckOnClick);
        }
    }
}
