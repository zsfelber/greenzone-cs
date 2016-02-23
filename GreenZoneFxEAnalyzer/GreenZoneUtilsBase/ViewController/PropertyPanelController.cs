using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface IPropertyPanelController : IController
    {
    }

    public class PropertyPanelController : Controller, IPropertyPanelController
    {
        public PropertyPanelController(GreenRmiManager rmiManager, Controller parent, object selectedObject)
            : base(rmiManager, parent)
        {
            propertyGrid1 = new BufferedPropertyGridController(rmiManager, this);
            SelectedObject = selectedObject;
            dependencies.Add(propertyGrid1);
        }

        public PropertyPanelController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            propertyGrid1 = (BufferedPropertyGridController)buffer.ChangedProps[PROPERTY_14_PROPERTYGRID1_ID];
            dependencies.Add(propertyGrid1);
        }

        protected PropertyPanelController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            propertyGrid1 = (BufferedPropertyGridController) info.GetValue("PropertyGrid1", typeof(BufferedPropertyGridController));
            dependencies.Add(propertyGrid1);
        }

        readonly BufferedPropertyGridController propertyGrid1;
        const int PROPERTY_14_PROPERTYGRID1_ID = 14;
        public BufferedPropertyGridController PropertyGrid1
        {
            get
            {
                return propertyGrid1;
            }
        }

        public object SelectedObject
        {
            get
            {
                return propertyGrid1.SelectedObject;
            }
            set
            {
                propertyGrid1.SelectedObject = value;
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_PROPERTYGRID1_ID:
                    return PropertyGrid1;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_PROPERTYGRID1_ID:
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("PropertyGrid1", PropertyGrid1);
        }
    }
}
