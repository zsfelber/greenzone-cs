﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;

using System.Drawing;

namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripLabelVCBinder1 : ControlVCBinder2<ToolStripLabel, LabelledController>
    {
        public ToolStripLabelVCBinder1(WinFormsMVContext context, ToolStripLabel control, LabelledController controller)
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

            controller.TextChanged += new PropertyChangedEventHandler(controller_TextChanged);
        }

        void controller_TextChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Text = controller.Text;
        }


        ///////////////////////////////////////////////////////////////////


    }
}
