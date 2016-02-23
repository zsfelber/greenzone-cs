using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public abstract class BaseButtonVCBinder<C, K> : ControlVCBinder1<C, K>
        where C : Control
        where K : ButtonController
    {
        public BaseButtonVCBinder(WinFormsMVContext context, C control, K controller)
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
            if (controller.Image != 0)
            {
                Image = context.GetImage(controller.Image);
            }

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.ImageChanged += new PropertyChangedEventHandler(controller_ImageChanged);

            control.Click += new EventHandler(control_Click);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }

        void controller_ImageChanged(object sender, PropertyChangedEventArgs e)
        {
            Image = context.GetImage(controller.Image);
        }


        public abstract Image Image
        {
            get;
            set;
        }

        /////////////////////////////////////////////////////////////////////




        protected virtual void control_Click(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Press();
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }

    public abstract class BaseButtonVCBinder2<C, K> : ControlVCBinder2<C, K>
        where C : ToolStripItem
        where K : ButtonController
    {
        public BaseButtonVCBinder2(WinFormsMVContext context, C control, K controller)
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
            if (controller.Image != 0)
            {
                Image = context.GetImage(controller.Image);
            }

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.ImageChanged += new PropertyChangedEventHandler(controller_ImageChanged);

            control.Click += new EventHandler(control_Click);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }

        void controller_ImageChanged(object sender, PropertyChangedEventArgs e)
        {
            Image = context.GetImage(controller.Image);
        }


        public abstract Image Image
        {
            get;
            set;
        }

        /////////////////////////////////////////////////////////////////////




        protected virtual void control_Click(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Press();
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
