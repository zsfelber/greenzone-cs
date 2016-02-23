using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ProgressTrackBarVCBinder : ControlVCBinder1<ProgressTrackBar, ProgressTrackController>
    {
        public ProgressTrackBarVCBinder(WinFormsMVContext context, ProgressTrackBar control, ProgressTrackController controller)
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
            control.TickPosition = controller.CursorValue;
            control.ProgressValue = controller.Value;

            if (controller.Maximum != 0)
            {
                control.MaxProgress = controller.Maximum;
            }
            else
            {
                controller.Maximum = control.MaxProgress;
            }

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.CursorValueChanged += new PropertyChangedEventHandler(controller_CursorValueChanged);
            controller.ValueChanged += new PropertyChangedEventHandler(controller_ValueChanged);
            controller.MaximumChanged += new PropertyChangedEventHandler(controller_MaximumChanged);

            control.TextChanged += new EventHandler(control_TextChanged);
            control.TickPositionChanged += new System.ComponentModel.PropertyChangedEventHandler(control_TickPositionChanged);
            control.ProgressValueChanged += new System.ComponentModel.PropertyChangedEventHandler(control_ProgressValueChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }

        void controller_CursorValueChanged(object sender, PropertyChangedEventArgs e)
        {
            control.TickPosition = controller.CursorValue;
        }

        void controller_ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            control.ProgressValue = controller.Value;
        }

        void controller_MaximumChanged(object sender, PropertyChangedEventArgs e)
        {
            control.MaxProgress = controller.Maximum;
        }


        ///////////////////////////////////////////////////////////////////


        void control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Text = control.Text;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_TickPositionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.CursorValue = control.TickPosition;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

        void control_ProgressValueChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Value = control.ProgressValue;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }

}
