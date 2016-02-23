using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using System.Drawing;


namespace GreenZoneUtil.Gui.ViewController
{
    public class SplitContainerVCBinder : ControlVCBinder1<SplitContainer,SplitController>
    {
        public SplitContainerVCBinder(WinFormsMVContext context, SplitContainer control, SplitController controller)
            : base(context, control, controller)
        {

            if (controller.SplitterDistance != 0)
            {
                control.SplitterDistance = controller.SplitterDistance;
            }
            else
            {
                controller.SplitterDistance = control.SplitterDistance;
            }

            panel1Binder = new SplitterPanelVCBinder(context, control.Panel1, controller.Panel1);
            panel2Binder = new SplitterPanelVCBinder(context, control.Panel2, controller.Panel2);

            controller.Panel1CollapsedChanged += new PropertyChangedEventHandler(controller_Panel1Collapsed);
            controller.Panel2CollapsedChanged += new PropertyChangedEventHandler(controller_Panel2Collapsed);

            control.SplitterMoved += new SplitterEventHandler(SplitContainerVC_SplitterMoved);
        }

        void controller_Panel1Collapsed(object sender, PropertyChangedEventArgs e)
        {
            control.Panel1Collapsed = controller.Panel1Collapsed;
        }

        void controller_Panel2Collapsed(object sender, PropertyChangedEventArgs e)
        {
            control.Panel2Collapsed = controller.Panel2Collapsed;
        }

        SplitterPanelVCBinder panel1Binder;
        public SplitterPanelVCBinder Panel1Binder
        {
            get
            {
                return panel1Binder;
            }
        }

        SplitterPanelVCBinder panel2Binder;
        public SplitterPanelVCBinder Panel2Binder
        {
            get
            {
                return panel2Binder;
            }
        }


        /////////////////////////////////////////////////////////////////////


        void SplitContainerVC_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.Panel1Collapsed = control.Panel1Collapsed;
                controller.Panel2Collapsed = control.Panel2Collapsed;
                controller.SplitterDistance = control.SplitterDistance;
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }

    }
}
