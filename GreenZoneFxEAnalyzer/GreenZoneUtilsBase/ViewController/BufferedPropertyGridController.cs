using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.Util;

using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;
using GreenZoneParser.Reflect;

namespace GreenZoneUtil.ViewController
{
    // TODO PropertyTab
    // TODO SelectedObjects
    
    public class BufferedPropertyGridController : Controller
    {
        public event PropertyChangedEventHandler SelectedObjectChanged;
        public event PropertyChangedEventHandler PropertySortChanged;

        public BufferedPropertyGridController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public BufferedPropertyGridController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected BufferedPropertyGridController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SelectedObject = info.GetValue("SelectedObject", typeof(object));
            PropertySort = (PropertySort)info.GetValue("PropertySort", typeof(PropertySort));
            originalProperties = (List<Property>)info.GetValue("OriginalProperties", typeof(List<Property>));
            properties = (List<Property>)info.GetValue("Properties", typeof(List<Property>));
        }

        object selectedObject;
        public object SelectedObject
        {
            get
            {
                return selectedObject;
            }
            set
            {
                if (selectedObject != value)
                {
                    selectedObject = value;
                    extractProperties();
                    if (SelectedObjectChanged != null)
                    {
                        SelectedObjectChanged(this, new PropertyChangedEventArgs("SelectedObject", value));
                    }
                }
            }
        }

        PropertySort propertySort;
        const int PROPERTY_14_PROPERTYSORT_ID = 14;
        public PropertySort PropertySort
        {
            get
            {
                return propertySort;
            }
            set
            {
                if (propertySort != value)
                {
                    propertySort = value;
                    changed[PROPERTY_14_PROPERTYSORT_ID] = true;
                    somethingChanged = true;
                    if (PropertySortChanged != null)
                    {
                        PropertySortChanged(this, new PropertyChangedEventArgs("PropertySort", value));
                    }
                }
            }
        }

        List<Property> originalProperties;
        const int PROPERTY_15_ORIGINALPROPERTIES_ID = 15;
        public List<Property> OriginalProperties
        {
            get
            {
                return originalProperties;
            }
        }

        List<Property> properties;
        const int PROPERTY_16_PROPERTIES_ID = 16;
        public List<Property> Properties
        {
            get
            {
                return properties;
            }
        }

        public bool IsChanged
        {
            get
            {
                bool eq = GreenZoneUtilsBase.PropertiesEqual(properties, originalProperties);
                return !eq;
            }
        }

        BindingFlags bindingFlags = BindingFlags.Default;
        public BindingFlags BindingFlags
        {
            get
            {
                return bindingFlags;
            }
            set
            {
                bindingFlags = value;
            }
        }

        public void Save()
        {
            GreenZoneUtilsBase.CopyProperties(rmiManager.Resolver, properties, selectedObject);
        }

        public void Save(object selectedObject)
        {
            GreenZoneUtilsBase.CopyProperties(rmiManager.Resolver, properties, selectedObject);
        }

        public void Reset()
        {
            properties = GreenZoneUtilsBase.CloneProperties(originalProperties);
        }

        public void SetProperties(List<Property> properties)
        {
            originalProperties = properties;
            Reset();
        }

        void extractProperties()
        {
            originalProperties = GreenZoneUtilsBase.GetMyProperties(rmiManager.Resolver, selectedObject, bindingFlags, true);
            Reset();
        }


        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_PROPERTYSORT_ID:
                    return PropertySort;
                case PROPERTY_15_ORIGINALPROPERTIES_ID:
                    return OriginalProperties;
                case PROPERTY_16_PROPERTIES_ID:
                    return Properties;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_PROPERTYSORT_ID:
                    PropertySort = (PropertySort)value;
                    break;
                case PROPERTY_15_ORIGINALPROPERTIES_ID:
                    originalProperties = (List<Property>)value;
                    break;
                case PROPERTY_16_PROPERTIES_ID:
                    properties = (List<Property>)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SelectedObject", SelectedObject);
            info.AddValue("PropertySort", PropertySort);
            info.AddValue("OriginalProperties", OriginalProperties);
            info.AddValue("Properties", Properties);
        }
    }
}
