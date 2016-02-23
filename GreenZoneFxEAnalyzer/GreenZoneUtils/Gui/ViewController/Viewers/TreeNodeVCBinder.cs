using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class TreeNodeVCBinder : ControlVCBinder5<TreeNode,TreeNodeController>
    {

        public TreeNodeVCBinder(WinFormsMVContext context, TreeNode control, TreeNodeController controller)
            : base(context, control, controller)
        {

            if (controller.Text != null)
            {
                control.Text = controller.Text;
            }
            else
            {
                controller.Text = control.Text;
            }
            control.ImageIndex = controller.Image;

            foreach (var child in controller.Nodes)
            {
                AddChild(child);
            }

            controller.ChildControlInserted += new ListChangedEventHandler(controller_ChildAdded);
            controller.ChildControlRemovedAt += new ListChangedEventHandler(controller_ChildRemovedAt);
            controller.ControlsChanged += new PropertyChangedEventHandler(controller_ChildsChanged);

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.ImageChanged += new PropertyChangedEventHandler(controller_ImageChanged);
            controller.ExpandedChanged += new PropertyChangedEventHandler(controller_ExpandedChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }

        void controller_ImageChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ImageIndex = controller.Image;
        }

        void controller_ExpandedChanged(object sender, PropertyChangedEventArgs e)
        {
            if (controller.Expanded)
            {
                control.Expand();
            }
            else
            {
                control.Collapse();
            }
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

        /////////////////////////////////////////////////////////////////////
        
        
    }
}
