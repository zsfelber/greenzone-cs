using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Collections;
using GreenZoneUtil.Util;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class MultiSplitController : Controller
    {
        public event ListChangedEventHandler ChildControlRemovedFrom;

        public MultiSplitController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public MultiSplitController(GreenRmiManager rmiManager, Controller parent, params Controller[] controls)
            : base(rmiManager, parent)
        {
            Controls = new List<Controller>(controls);
        }

        public MultiSplitController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected MultiSplitController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public new int RemoveFrom(Controller child)
        {
            int ind0 = base.RemoveFrom(child);
            if (ChildControlRemovedFrom != null)
            {
                ChildControlRemovedFrom(null, new ListChangedEventArgs(ListChangedType.ItemDeleted, ind0, child));
            }
            return ind0;
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
