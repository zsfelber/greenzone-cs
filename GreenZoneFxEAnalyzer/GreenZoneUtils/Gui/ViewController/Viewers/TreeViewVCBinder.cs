using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class TreeViewVCBinder : ControlVCBinder1<TreeView, TreeController>
    {
        public TreeViewVCBinder(WinFormsMVContext context, TreeView control, TreeController controller)
            : base(context, control, controller)
        {

            foreach (var child in controller.Nodes)
            {
                AddChild(child);
            }

            controller.Root.ChildControlInserted += new ListChangedEventHandler(controller_ChildAdded);
            controller.Root.ChildControlRemovedAt += new ListChangedEventHandler(controller_ChildRemovedAt);
            controller.Root.ControlsChanged += new PropertyChangedEventHandler(controller_ChildsChanged);
            controller.SelectedNodeChanged += new PropertyChangedEventHandler(controller_SelectedNodeChanged);

            control.AfterSelect += new TreeViewEventHandler(control_AfterSelect);
            control.BeforeSelect += new TreeViewCancelEventHandler(control_BeforeSelect);
        }

        protected virtual void AddChild(TreeNodeController child1)
        {
            TreeNode newChild = new TreeNode();
            TreeNodeVCBinder binder = new TreeNodeVCBinder(context, newChild, child1);
            control.Nodes.Add(newChild);
        }

        void controller_ChildAdded(object sender, ListChangedEventArgs e)
        {
            TreeNodeController child = (TreeNodeController)e.Element;
            AddChild(child);
        }

        void controller_ChildRemovedAt(object sender, ListChangedEventArgs e)
        {
            control.Nodes.RemoveAt(e.NewIndex);
        }

        void controller_ChildsChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Nodes.Clear();
            foreach (var child in controller.Nodes)
            {
                AddChild(child);
            }
        }

        bool _sel = false;
        void controller_SelectedNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_sel)
            {
                try
                {
                    _sel = true;
                    if (controller.SelectedNode == null)
                    {
                        control.SelectedNode = null;
                    }
                    else
                    {
                        control.SelectedNode = (TreeNode)controller.SelectedNode.BoundControl;
                    }
                }
                finally
                {
                    _sel = false;
                }
            }
        }



        /////////////////////////////////////////////////////////////////////


        void control_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                if (!_sel)
                {
                    try
                    {
                        _sel = true;
                        controller.SelectedNode = (TreeNodeController)e.Node.Tag;
                    }
                    catch (OperationCanceledException)
                    {
                        e.Cancel = true;
                    }
                    finally
                    {
                        _sel = false;
                    }
                }
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

    }
}