using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;


namespace GreenZoneUtil.Gui.ViewController
{
    public class FolderBrowserVCBinder : FormVCBinder2<FolderBrowserDialog, FolderBrowserController>
    {
        public FolderBrowserVCBinder(WinFormsMVContext context, FolderBrowserDialog control, FolderBrowserController controller)
            : base(context, control, controller)
        {
            if (controller.SelectedPath != null)
            {
                control.SelectedPath = controller.SelectedPath;
            }
            else
            {
                controller.SelectedPath = control.SelectedPath;
            }

            if (controller.RootFolder != default(Environment.SpecialFolder))
            {
                control.RootFolder = controller.RootFolder;
            } else {
                controller.RootFolder = control.RootFolder;
            }

            controller.SelectedPathChanged += new PropertyChangedEventHandler(controller_SelectedPathChanged);
            controller.RootFolderChanged += new PropertyChangedEventHandler(controller_RootFolderChanged);
        }

        void controller_SelectedPathChanged(object sender, PropertyChangedEventArgs e)
        {
            control.SelectedPath = controller.SelectedPath;
        }

        void controller_RootFolderChanged(object sender, PropertyChangedEventArgs e)
        {
            control.RootFolder = controller.RootFolder;
        }


        ////////////////////////////////////////////////////////////////////////////////////


    }
}
