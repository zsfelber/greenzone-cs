using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    // TODO multiselect
    
    public class TreeController : Controller
    {
        public event PropertyChangingEventHandlerC SelectedNodeChanging;
        public event PropertyChangedEventHandler SelectedNodeChanged;
        public event PropertyChangedEventHandler SelectedNodeCanceled;

        public TreeController(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
            root = new TreeNodeController(rmiManager, null, null);
            root.Expanded = true;
            //root.Parent = this;
            Clear();
            _addDeps();
        }

        public TreeController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            root = (TreeNodeController)buffer.ChangedProps[PROPERTY_15_ROOT_ID];
            _addDeps();
        }

        protected TreeController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SelectedNode = (TreeNodeController) info.GetValue("SelectedNode", typeof(TreeNodeController));
            root = (TreeNodeController) info.GetValue("Root", typeof(TreeNodeController));
            _addDeps();
        }

        void _addDeps()
        {
            dependencies.Add(root);
        }

        TreeNodeController selectedNode;
        const int PROPERTY_14_SELECTEDNODE_ID = 14;
        public TreeNodeController SelectedNode
        {
            get
            {
                return selectedNode;
            }
            set
            {
                if (selectedNode != value)
                {
                    if (SelectedNodeChanging != null)
                    {
                        PropertyChangingEventArgsC args = new PropertyChangingEventArgsC("SelectedNode", value);
                        SelectedNodeChanging(value, args);
                        if (args.Cancel)
                        {
                            if (SelectedNodeCanceled != null)
                            {
                                SelectedNodeCanceled(this, new PropertyChangedEventArgs("SelectedNode", selectedNode));
                            }
                            return;
                        }
                    }
                    selectedNode = value;
                    changed[PROPERTY_14_SELECTEDNODE_ID] = true;
                    somethingChanged = true;
                    if (SelectedNodeChanged != null)
                    {
                        SelectedNodeChanged(this, new PropertyChangedEventArgs("SelectedNode", value));
                    }
                }
            }
        }

        readonly TreeNodeController root;
        const int PROPERTY_15_ROOT_ID = 15;
        public TreeNodeController Root
        {
            get
            {
                return root;
            }
        }

        public IList<TreeNodeController> Nodes
        {
            get
            {
                return root.Nodes;
            }
        }

        public override void Add(Controller control)
        {
            root.Add(control);
        }

        public override void Insert(int index, Controller control)
        {
            root.Insert(index, control);
        }

        public override void Remove(Controller control)
        {
            root.Remove(control);
        }

        public override void RemoveAt(int index)
        {
            root.RemoveAt(index);
        }

        public void Add(TreeNodeController node)
        {
            root.Add(node);
        }

        public void Remove(TreeNodeController node)
        {
            root.Remove(node);
        }

        public override void Clear()
        {
            if (root != null)
            {
                root.Clear();
            }
        }

        
        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_SELECTEDNODE_ID:
                    return SelectedNode;
                case PROPERTY_15_ROOT_ID:
                    return Root;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_SELECTEDNODE_ID:
                    SelectedNode = (TreeNodeController)value;
                    break;
                case PROPERTY_15_ROOT_ID:
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SelectedNode", SelectedNode);
            info.AddValue("Root", Root);
        }
    }

}
