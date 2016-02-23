using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;

using GreenZoneUtil.Util;

namespace GreenZoneUtil.Gui.ViewController
{
    public class FormVCBinder : ControlVCBinder1<Form, FormController>
    {
        PropertyChangedEventHandler controller_AcceptButton_BoundControlChanged;
        PropertyChangedEventHandler controller_CancelButton_BoundControlChanged;

        public FormVCBinder(WinFormsMVContext context, Form control, FormController controller)
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
                control.Icon = GreenZoneUtils.ImageToIcon(context.GetImage(controller.Image));
            }
            if (controller.AcceptButton != null)
            {
                control.AcceptButton = (Button)controller.AcceptButton.BoundControl;
                controller_AcceptButton_BoundControlChanged = new PropertyChangedEventHandler(AcceptButton_BoundControlChanged);
                controller.AcceptButton.BoundControlChanged += controller_AcceptButton_BoundControlChanged;
            }
            else if (control.AcceptButton != null)
            {
                controller.AcceptButton = (ButtonController)((Control)control.AcceptButton).Tag;
            }

            if (controller.CancelButton != null)
            {
                control.CancelButton = (Button)controller.CancelButton.BoundControl;
                controller_CancelButton_BoundControlChanged = new PropertyChangedEventHandler(CancelButton_BoundControlChanged);
                controller.CancelButton.BoundControlChanged += controller_CancelButton_BoundControlChanged;
            }
            else if (control.AcceptButton != null)
            {
                controller.CancelButton = (ButtonController)((Control)control.CancelButton).Tag;
            }

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.ImageChanged += new PropertyChangedEventHandler(controller_ImageChanged);
            control.MinimizeBox = controller.AllowMinimize;
            control.MaximizeBox = controller.AllowMaximize;
            control.ShowInTaskbar = controller.ShowInTaskbar;

            controller.FormClosed += new ControllerEventHandler(controller_FormClosed);
            controller.AcceptButtonChanged += new PropertyChangedEventHandler(controller_AcceptButtonChanged);
            controller.CancelButtonChanged += new PropertyChangedEventHandler(controller_CancelButtonChanged);
            controller.DialogResultChanged += new PropertyChangedEventHandler(controller_DialogResultChanged);
            controller.AllowMinimizeChanged += new PropertyChangedEventHandler(controller_AllowMinimizeChanged);
            controller.AllowMaximizeChanged += new PropertyChangedEventHandler(controller_AllowMaximizeChanged);
            controller.ShowInTaskbarChanged += new PropertyChangedEventHandler(controller_ShowInTaskbarChanged);

            control.FormClosing += new FormClosingEventHandler(control_FormClosing);
            control.FormClosed += new FormClosedEventHandler(control_FormClosed);
            control.Load += new EventHandler(control_Load);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }

        void controller_ImageChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Icon = GreenZoneUtils.ImageToIcon(context.GetImage(controller.Image));
        }

        bool _controller_FormClosed = false;
        void controller_FormClosed(object sender, ControllerEventArgs e)
        {
            if (!_controller_FormClosed)
            {
                _controller_FormClosed = true;
                control.Close();
            }
        }

        void controller_AcceptButtonChanged(object sender, PropertyChangedEventArgs e)
        {
            if (controller_AcceptButton_BoundControlChanged != null)
            {
                controller.AcceptButton.BoundControlChanged -= controller_AcceptButton_BoundControlChanged;
            }
            control.AcceptButton = (Button)controller.AcceptButton.BoundControl;
            controller_AcceptButton_BoundControlChanged = new PropertyChangedEventHandler(AcceptButton_BoundControlChanged);
            controller.AcceptButton.BoundControlChanged += controller_AcceptButton_BoundControlChanged;
        }

        void controller_CancelButtonChanged(object sender, PropertyChangedEventArgs e)
        {
            if (controller_CancelButton_BoundControlChanged != null)
            {
                controller.CancelButton.BoundControlChanged -= controller_CancelButton_BoundControlChanged;
            }
            control.CancelButton = (Button)controller.CancelButton.BoundControl;
            controller_CancelButton_BoundControlChanged = new PropertyChangedEventHandler(CancelButton_BoundControlChanged);
            controller.CancelButton.BoundControlChanged += controller_CancelButton_BoundControlChanged;
        }

        void AcceptButton_BoundControlChanged(object sender, PropertyChangedEventArgs e)
        {
            control.AcceptButton = (Button)controller.AcceptButton.BoundControl;
        }

        void controller_DialogResultChanged(object sender, PropertyChangedEventArgs e)
        {
            control.DialogResult = controller.DialogResult;
        }

        void CancelButton_BoundControlChanged(object sender, PropertyChangedEventArgs e)
        {
            control.CancelButton = (Button)controller.CancelButton.BoundControl;
        }

        void controller_AllowMinimizeChanged(object sender, PropertyChangedEventArgs e)
        {
            control.MinimizeBox = controller.AllowMinimize;
        }

        void controller_AllowMaximizeChanged(object sender, PropertyChangedEventArgs e)
        {
            control.MaximizeBox = controller.AllowMaximize;
        }

        void controller_ShowInTaskbarChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ShowInTaskbar = controller.ShowInTaskbar;
        }




        ////////////////////////////////////////////////////////////////////////


        void control_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                if (!_controller_FormClosed)
                {
                    controller.OnClosing(e);
                }
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                if (!_controller_FormClosed)
                {
                    controller.ForceClose();
                }
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_Load(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Load();
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

    }


    public class FormVCBinder2<C, K> : ControlVCBinder<C, K>
        where C : CommonDialog
        where K : FormController
    {
        public FormVCBinder2(WinFormsMVContext context, C control, K controller)
            : base(context, control, controller)
        {
        }

        ////////////////////////////////////////////////////////////////////////

    }


}