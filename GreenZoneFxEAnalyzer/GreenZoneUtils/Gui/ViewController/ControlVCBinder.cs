using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class ControlVCBinder<C, K>
    {

        public ControlVCBinder(WinFormsMVContext context, C control, K controller)
        {
            this.context = context;
            this.control = control;
            this.controller = controller;
        }

        protected readonly WinFormsMVContext context;
        public WinFormsMVContext Context
        {
            get
            {
                return context;
            }
        }

        protected readonly C control;
        public C Control
        {
            get
            {
                return control;
            }
        }

        protected readonly K controller;
        public K Controller
        {
            get
            {
                return controller;
            }
        }


        /////////////////////////////////////////////////////////////////////

    }


    public class ControlVCBinder1<C, K> : ControlVCBinder<C, K>
        where C : Control
        where K : Controller
    {

        public ControlVCBinder1(WinFormsMVContext context, C control, K controller)
            : base(context, control, controller)
        {
            control.Tag = controller;
            controller.BoundControl = control;
            control.Enabled = controller.Enabled;
            control.Visible = controller.Visible;

            if (controller.ForeColor != default(Color))
            {
                control.ForeColor = controller.ForeColor;
            }
            else
            {
                controller.ForeColor = control.ForeColor;
            }
            if (controller.BackColor != default(Color))
            {
                control.BackColor = controller.BackColor;
            }
            else
            {
                controller.BackColor = control.BackColor;
            }
            if (controller.Font != null)
            {
                control.Font = controller.Font;
            }
            else
            {
                controller.Font = control.Font;
            }

            controller.EnabledChanged += new PropertyChangedEventHandler(controller_EnabledChanged);
            controller.ForeColorChanged += new PropertyChangedEventHandler(controller_ForeColorChanged);
            controller.BackColorChanged += new PropertyChangedEventHandler(controller_BackColorChanged);
            controller.VisibleChanged += new PropertyChangedEventHandler(controller_VisibleChanged);
            controller.FontChanged += new PropertyChangedEventHandler(controller_FontChanged);
            //TODO ClientController
            //controller.Updated += new ControllerEventHandler(controller_Updated);

            control.VisibleChanged += new EventHandler(control_VisibleChanged);
            control.Layout += new LayoutEventHandler(control_Layout);
            control.LocationChanged += new EventHandler(control_LocationChanged);
            control.SizeChanged += new EventHandler(control_SizeChanged);
            //TODO ClientController
            //control.Paint += new PaintEventHandler(control_Paint);
            control.MouseClick += new MouseEventHandler(control_MouseClick);
            control.MouseDoubleClick += new MouseEventHandler(control_MouseDoubleClick);
            control.MouseDown += new MouseEventHandler(control_MouseDown);
        }

        void controller_EnabledChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Enabled = controller.Enabled;
        }

        void controller_ForeColorChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ForeColor = controller.ForeColor;
        }

        void controller_BackColorChanged(object sender, PropertyChangedEventArgs e)
        {
            control.BackColor = controller.BackColor;
        }

        bool _vis = false;
        void controller_VisibleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_vis)
            {
                try
                {
                    _vis = true;
                    control.Visible = controller.Visible;
                }
                finally
                {
                    _vis = false;
                }
            }
        }

        void controller_FontChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Font = controller.Font;
        }

        void controller_Updated(object sender, ControllerEventArgs e)
        {
            control.Invalidate();
            control.Update();
        }


        /////////////////////////////////////////////////////////////////////



        void control_VisibleChanged(object sender, EventArgs e)
        {
            if (!_vis)
            {
                try
                {
                    _vis = true;
                    context.LockMainWindow();
                    controller.Visible = control.Visible;
                }
                finally
                {
                    _vis = false;
                    context.UnlockMainWindow();
                }
            }
        }

        void control_Layout(object sender, LayoutEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Layout();
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Size = control.Size;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_LocationChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Location = control.Location;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        //TODO ClientController
        //void control_Paint(object sender, PaintEventArgs e)
        //{
        //    controller.Paint(e);
        //}

        void control_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Click(e.Location);
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.DoubleClick(e.Location);
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.RaiseMouseDown(e.Location);
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }

    public class ControlVCBinder2<C, K> : ControlVCBinder<C, K>
        where C : ToolStripItem
        where K : Controller
    {

        public ControlVCBinder2(WinFormsMVContext context, C control, K controller)
            : base(context, control, controller)
        {
            control.Tag = controller;
            controller.BoundControl = control;
            control.Enabled = controller.Enabled;

            if (controller.ForeColor != default(Color))
            {
                control.ForeColor = controller.ForeColor;
            }
            else
            {
                controller.ForeColor = control.ForeColor;
            }
            if (controller.BackColor != default(Color))
            {
                control.BackColor = controller.BackColor;
            }
            else
            {
                controller.BackColor = control.BackColor;
            }
            if (controller.Font != null)
            {
                control.Font = controller.Font;
            }
            else
            {
                controller.Font = control.Font;
            }

            controller.EnabledChanged += new PropertyChangedEventHandler(controller_EnabledChanged);
            controller.ForeColorChanged += new PropertyChangedEventHandler(controller_ForeColorChanged);
            controller.BackColorChanged += new PropertyChangedEventHandler(controller_BackColorChanged);
            controller.VisibleChanged += new PropertyChangedEventHandler(controller_VisibleChanged);
            controller.FontChanged += new PropertyChangedEventHandler(controller_FontChanged);
        }

        void controller_EnabledChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Enabled = controller.Enabled;
        }

        void controller_ForeColorChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ForeColor = controller.ForeColor;
        }

        void controller_BackColorChanged(object sender, PropertyChangedEventArgs e)
        {
            control.BackColor = controller.BackColor;
        }

        void controller_VisibleChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Visible = controller.Visible;
        }

        void controller_FontChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Font = controller.Font;
        }


        /////////////////////////////////////////////////////////////////////


    }



    public class ControlVCBinder3<C, K> : ControlVCBinder<C, K>
        where C : DataGridViewBand
        where K : Controller
    {

        public ControlVCBinder3(WinFormsMVContext context, C control, K controller)
            : base(context, control, controller)
        {
            control.Tag = controller;
            controller.BoundControl = control;
            controller.VisibleChanged += new PropertyChangedEventHandler(controller_VisibleChanged);
        }

        void controller_VisibleChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Visible = controller.Visible;
        }


        /////////////////////////////////////////////////////////////////////

    }



    public class ControlVCBinder4<C, K> : ControlVCBinder<C, K>
        where C : DataGridViewCell
        where K : GridCellController
    {

        public ControlVCBinder4(WinFormsMVContext context, C control, K controller)
            : base(context, control, controller)
        {
            control.Tag = controller;
            controller.BoundControl = control;
        }


        /////////////////////////////////////////////////////////////////////

    }



    public class ControlVCBinder5<C, K> : ControlVCBinder<C, K>
        where C : TreeNode
        where K : TreeNodeController
    {

        public ControlVCBinder5(WinFormsMVContext context, C control, K controller)
            : base(context, control, controller)
        {
            control.Tag = controller;
            controller.BoundControl = control;
        }

        /////////////////////////////////////////////////////////////////////

    }
}