using System;
using System.Collections.Generic;
using System.Linq;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class FolderBrowserController : DialogController
    {
        public event PropertyChangedEventHandler SelectedPathChanged;
        public event PropertyChangedEventHandler RootFolderChanged;

        public FolderBrowserController(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public FolderBrowserController(GreenRmiManager rmiManager, string path)
            : base(rmiManager)
        {
        }

        public FolderBrowserController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected FolderBrowserController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SelectedPath = (string) info.GetValue("SelectedPath", typeof(string));
            RootFolder = (Environment.SpecialFolder)info.GetValue("RootFolder", typeof(Environment.SpecialFolder));
        }

        string selectedPath;
        const int PROPERTY_22_SELECTEDPATH_ID = 22;
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
                    changed[PROPERTY_22_SELECTEDPATH_ID] = true;
                    somethingChanged = true;
                    if (SelectedPathChanged != null)
                    {
                        SelectedPathChanged(this, new PropertyChangedEventArgs("SelectedPath", value));
                    }
                }
            }
        }

        Environment.SpecialFolder rootFolder;
        const int PROPERTY_23_ROOTFOLDER_ID = 23;
        public Environment.SpecialFolder RootFolder
        {
            get
            {
                return rootFolder;
            }
            set
            {
                if (rootFolder != value)
                {
                    rootFolder = value;
                    changed[PROPERTY_23_ROOTFOLDER_ID] = true;
                    somethingChanged = true;
                    if (RootFolderChanged != null)
                    {
                        RootFolderChanged(this, new PropertyChangedEventArgs("RootFolder", value));
                    }
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_22_SELECTEDPATH_ID:
                    return SelectedPath;
                case PROPERTY_23_ROOTFOLDER_ID:
                    return RootFolder;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_22_SELECTEDPATH_ID:
                    SelectedPath = (string)value;
                    break;
                case PROPERTY_23_ROOTFOLDER_ID:
                    RootFolder = (Environment.SpecialFolder)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SelectedPath", SelectedPath);
            info.AddValue("RootFolder", RootFolder);
        }
    }
}
