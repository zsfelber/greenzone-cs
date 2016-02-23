using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class RadioButtonController : ToggleButtonController
    {

        public RadioButtonController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public RadioButtonController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
        }

        public RadioButtonController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
        }

        public RadioButtonController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected RadioButtonController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
            set
            {
                if (base.Checked != value)
                {
                    if (value)
                    {
                        foreach (var c in Parent.Controls)
                        {
                            if (c is RadioButtonController && c != this)
                            {
                                RadioButtonController r = (RadioButtonController)c;
                                r.Checked = false;
                            }
                        }
                    }
                    base.Checked = value;
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
