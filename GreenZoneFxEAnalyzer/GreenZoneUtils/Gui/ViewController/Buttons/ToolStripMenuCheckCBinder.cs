﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class ToolStripMenuCheckVCBinder : BaseButtonVCBinder2<ToolStripMenuItem, ToggleButtonController>
    {

        public ToolStripMenuCheckVCBinder(WinFormsMVContext context, ToolStripMenuItem control, ToggleButtonController controller)
            : base(context, control, controller)
        {

            control.Checked = controller.Checked;
            // !
            control.CheckOnClick = false;

            controller.CheckedChanged += new PropertyChangedEventHandler(controller_CheckedChanged);
        }

        public override Image Image
        {
            get
            {
                return control.Image;
            }
            set
            {
                control.Image = value;
            }
        }

        void controller_CheckedChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Checked = controller.Checked;
        }


        /////////////////////////////////////////////////////////////////////


    }

}