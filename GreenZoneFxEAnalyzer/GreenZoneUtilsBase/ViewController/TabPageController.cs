using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface ITabPageController : ILabelledController
    {
    }

    public class TabPageController : LabelledController, ITabPageController
    {
        public event PropertyChangedEventHandler ContentChanged;

        public TabPageController(GreenRmiManager rmiManager, TabController parent)
            : base(rmiManager, parent)
        {
        }

        public TabPageController(GreenRmiManager rmiManager, TabController parent, string text, Controller content)
            : base(rmiManager, parent, text)
        {
            Content = content;
        }

        public TabPageController(GreenRmiManager rmiManager, TabController parent, string text, int image, Controller content)
            : base(rmiManager, parent, text, image)
        {
            Content = content;
        }

        public TabPageController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected TabPageController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Controller Content
        {
            get
            {
                if (Controls.Count > 0)
                {
                    return Controls[0];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (Content != value)
                {
                    if (Content != null)
                    {
                        base.Remove(Content);
                    }
                    Content = value;
                    if (ContentChanged != null)
                    {
                        ContentChanged(this, new PropertyChangedEventArgs("Content", value));
                    }
                    base.Add(value);
                }
            }
        }


        public override void Add(Controller control)
        {
            if (Controls.Count > 0)
            {
                throw new NotSupportedException();
            }
        }

        public override void Insert(int index, Controller control)
        {
            throw new NotSupportedException();
        }

        public override void Remove(Controller control)
        {
            throw new NotSupportedException();
        }

        public override void RemoveAt(int index)
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
