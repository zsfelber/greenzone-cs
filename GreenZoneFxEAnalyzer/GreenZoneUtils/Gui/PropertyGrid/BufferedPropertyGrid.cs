using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.Util;

namespace GreenZoneUtil.Gui.PropertyGrid
{
    // TODO PropertyTab
    // TODO SelectedObjects
    // NOTE Not required when using Controllers (BufferedPropertyGridController + plain System.Windows.Forms.PropertyGrid)
    /*
    public class BufferedPropertyGrid : System.Windows.Forms.PropertyGrid
    {
        ICloneable original;

        public BufferedPropertyGrid()
        {
        }

        public ICloneable OriginalSelectedObject
        {
            get
            {
                return original;
            }
        }

        public new ICloneable SelectedObject
        {
            get
            {
                return (ICloneable)base.SelectedObject;
            }
            set
            {
                original = value;
                if (value != null)
                {
                    base.SelectedObject = value.Clone();
                }
                else
                {
                    base.SelectedObject = null;
                }
            }
        }

        public bool IsChanged
        {
            get
            {
                bool eq = GreenZoneUtils.PropertiesEqual(SelectedObject, original, true);
                return !eq;
            }
        }

        public void Save()
        {
            GreenZoneUtils.CopyProperties(SelectedObject, original);
        }

        public void Reset()
        {
            SelectedObject = original;
        }
    }
     * */
}
