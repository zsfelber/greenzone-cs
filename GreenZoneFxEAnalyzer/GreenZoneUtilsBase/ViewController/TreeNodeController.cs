using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    
    public class TreeNodeController : LabelledController
    {
        public event PropertyChangedEventHandler ExpandedChanged;
        public event PropertyChangedEventHandler LevelChanged;

        protected TreeNodeController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            Clear();
        }

        public TreeNodeController(GreenRmiManager rmiManager, Controller parent, string text)
            : base(rmiManager, parent, text)
        {
            Clear();
        }

        public TreeNodeController(GreenRmiManager rmiManager, Controller parent, string text, int image)
            : base(rmiManager, parent, text, image)
        {
            Clear();
        }

        public TreeNodeController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            Clear();
        }

        protected TreeNodeController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Expanded = (bool)info.GetValue("Expanded",typeof(bool));
            Level = (int)info.GetValue("Level",typeof(int));
        }

        bool expanded = false;
        const int PROPERTY_16_EXPANDED_ID = 16;
        public virtual bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                if (expanded != value)
                {
                    expanded = value;
                    changed[PROPERTY_16_EXPANDED_ID] = true;
                    somethingChanged = true;
                    if (ExpandedChanged != null)
                    {
                        ExpandedChanged(this, new PropertyChangedEventArgs("Expanded", value));
                    }
                }
            }
        }

        public override IList<Controller> Controls
        {
            get
            {
                return base.Controls;
            }
            set
            {
                base.Controls = value;
                nodes = new ReadOnlyBridgeCollection<TreeNodeController>((IList)value);
            }
        }

        IList<TreeNodeController> nodes;
        public IList<TreeNodeController> Nodes
        {
            get
            {
                return nodes;
            }
        }

        int level = -1;
        const int PROPERTY_17_LEVEL_ID = 17;
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                if (level != value)
                {
                    level = value;
                    changed[PROPERTY_17_LEVEL_ID] = true;
                    somethingChanged = true;
                    if (LevelChanged != null)
                    {
                        LevelChanged(this, new PropertyChangedEventArgs("Level", value));
                    }
                    foreach (var n in Nodes)
                    {
                        n.Parent = this;
                        n.Level = level + 1;
                    }
                }
            }
        }

        public new TreeNodeController Parent
        {
            get
            {
                return (TreeNodeController)base.Parent;
            }
            protected set
            {
                base.Parent = value;
            }
        }

        public override void Add(Controller child)
        {
            TreeNodeController node = (TreeNodeController)child;
            base.Add(node);
            node.Level = level + 1;
        }
        public override void Insert(int index, Controller child)
        {
            TreeNodeController node = (TreeNodeController)child;
            base.Insert(index, node);
            node.Level = level + 1;
        }
        public override void Remove(Controller node)
        {
            base.Remove((TreeNodeController)node);
        }

        public virtual void Expand()
        {
            Expanded = true;
        }

        public virtual void Collapse()
        {
            Expanded = false;
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_16_EXPANDED_ID:
                    return Expanded;
                case PROPERTY_17_LEVEL_ID:
                    return Level;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_16_EXPANDED_ID:
                    Expanded = (bool)value;
                    break;
                case PROPERTY_17_LEVEL_ID:
                    Level = (int)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Expanded", Expanded);
            info.AddValue("Level", Level);
        }
    }
}
