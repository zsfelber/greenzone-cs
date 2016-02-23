using System;
using System.Collections.Generic;
using System.Linq;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    // TODO FileNames
    
    public class FileDialogController : DialogController
    {
        public event PropertyChangedEventHandler SelectedPathChanged;
        public event PropertyChangedEventHandler FilterChanged;

        public FileDialogController(GreenRmiManager rmiManager, bool isSave = false)
            : base(rmiManager)
        {
            this.isSave = isSave;
        }

        public FileDialogController(GreenRmiManager rmiManager, string path, bool isSave = false)
            : base(rmiManager)
        {
            this.isSave = isSave;
        }

        public FileDialogController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            this.isSave = (bool)buffer.ChangedProps[PROPERTY_22_ISSAVE_ID];
        }

        protected FileDialogController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            isSave = (bool) info.GetValue("IsSave", typeof(bool));
            SelectedPath = (string) info.GetValue("SelectedPath", typeof(string));
            Filter = (string) info.GetValue("Filter", typeof(string));
        }

        const int PROPERTY_22_ISSAVE_ID = 22;
        readonly bool isSave;
        public bool IsSave 
        {
            get
            {
                return isSave;
            }
        }

        string selectedPath;
        const int PROPERTY_23_SELECTEDPATH_ID = 23;
        public string SelectedPath
        {
            get
            {
                return selectedPath;
            }
            set
            {
                if (selectedPath != value)
                {
                    selectedPath = value;
                    changed[PROPERTY_23_SELECTEDPATH_ID] = true;
                    somethingChanged = true;
                    if (SelectedPathChanged != null)
                    {
                        SelectedPathChanged(this, new PropertyChangedEventArgs("SelectedPath", value));
                    }
                }
            }
        }

        public string FileName
        {
            get
            {
                return selectedPath;
            }
        }

        string filter;
        const int PROPERTY_24_FILTER_ID = 24;
        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (filter != value)
                {
                    filter = value;
                    changed[PROPERTY_24_FILTER_ID] = true;
                    somethingChanged = true;
                    if (FilterChanged != null)
                    {
                        FilterChanged(this, new PropertyChangedEventArgs("Filter", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_22_ISSAVE_ID:
                    return IsSave;
                case PROPERTY_23_SELECTEDPATH_ID:
                    return SelectedPath;
                case PROPERTY_24_FILTER_ID:
                    return Filter;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_22_ISSAVE_ID:
                    break;
                case PROPERTY_23_SELECTEDPATH_ID:
                    SelectedPath = (string)value;
                    break;
                case PROPERTY_24_FILTER_ID:
                    Filter = (string)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("IsSave", IsSave);
            info.AddValue("SelectedPath", SelectedPath);
            info.AddValue("Filter", Filter);
        }
    }
}
