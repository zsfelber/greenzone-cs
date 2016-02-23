using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class ButtonController : LabelledController
    {
        public event ControllerEventHandler Pressed;


        public ButtonController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ButtonController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
        }

        public ButtonController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
        }

        public ButtonController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ButtonController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public ComboController OwnerItem
        {
            get
            {
                if (Parent is ComboController)
                {
                    return (ComboController)Parent;
                }
                else
                {
                    return null;
                }
            }
        }

        public virtual void Press()
        {
            OnPress(null);
        }
        internal virtual void OnPress(ControllerEventArgs args)
        {
            if (Pressed != null)
            {
                if (args == null)
                {
                    args = new ControllerEventArgs();
                }
                else if (args.Consumed)
                {
                    return;
                }

                Pressed(this, args);
                args.Consume();
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
