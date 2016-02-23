using System;
using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class ListController : GridController
    {
        public ListController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            Clear();
        }

        public ListController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Clear();
        }

        protected ListController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public override void Clear()
        {
            ClearRows();
        }

        public override void ClearColumns()
        {
            throw new NotSupportedException();
        }

        public override void Add(GridColumnController column)
        {
            throw new NotSupportedException();
        }

        public override void Insert(int index, GridColumnController column)
        {
            throw new NotSupportedException();
        }

        public override void Remove(GridColumnController column)
        {
            throw new NotSupportedException();
        }

        public override void RemoveColumnAt(int index)
        {
            throw new NotSupportedException();
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
