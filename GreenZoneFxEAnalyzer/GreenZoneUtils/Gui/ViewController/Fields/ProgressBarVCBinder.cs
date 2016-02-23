using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Windows.Forms;

namespace GreenZoneUtil.Gui.ViewController
{
    public class ProgressBarVCBinder : ControlVCBinder1<ProgressBar,ProgressTrackController>
    {
        public ProgressBarVCBinder(WinFormsMVContext context, ProgressBar control, ProgressTrackController controller)
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

            control.Value = controller.Value;
            
            if (controller.Minimum != 0)
            {
                control.Minimum = controller.Minimum;
            }
            else
            {
                controller.Minimum = control.Minimum;
            }
            if (controller.Maximum != 0)
            {
                control.Maximum = controller.Maximum;
            }
            else
            {
                controller.Maximum = control.Maximum;
            }

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
            controller.ValueChanged += new PropertyChangedEventHandler(controller_ValueChanged);
            controller.MinimumChanged += new PropertyChangedEventHandler(controller_MinimumChanged);
            controller.MaximumChanged += new PropertyChangedEventHandler(controller_MaximumChanged);

            control.TextChanged += new EventHandler(control_TextChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }

        void controller_ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Value = controller.Value;
        }

        void controller_MinimumChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Minimum = controller.Minimum;
        }

        void controller_MaximumChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Maximum = controller.Maximum;
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
    }

}
