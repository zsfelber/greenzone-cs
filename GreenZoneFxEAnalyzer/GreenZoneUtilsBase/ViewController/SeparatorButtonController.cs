using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class SeparatorButtonController : ButtonController
    {
        public SeparatorButtonController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public SeparatorButtonController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected SeparatorButtonController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
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
