using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;


namespace GreenZoneUtil.Gui.ViewController
{
    public class FileDialogVCBinder : FormVCBinder2<FileDialog, FileDialogController>
    {
        public FileDialogVCBinder(WinFormsMVContext context, FileDialog control, FileDialogController controller)
            : base(context, control, controller)
        {
            if (controller.SelectedPath != null)
            {
                control.FileName = controller.SelectedPath;
            }
            else
            {
                controller.SelectedPath = control.FileName;
            }

            if (controller.Filter != null)
            {
                control.Filter = controller.Filter;
            }

            controller.SelectedPathChanged += new PropertyChangedEventHandler(controller_SelectedPathChanged);
            controller.FilterChanged += new PropertyChangedEventHandler(controller_FilterChanged);

            control.FileOk += new System.ComponentModel.CancelEventHandler(control_FileOk);
        }

        void controller_SelectedPathChanged(object sender, PropertyChangedEventArgs e)
        {
            control.FileName = controller.SelectedPath;
        }

        void controller_FilterChanged(object sender, PropertyChangedEventArgs e)
        {
            control.Filter = controller.Filter;
        }


        ////////////////////////////////////////////////////////////////////////////////////


        void control_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                context.LockMainWindow();
                controller.SelectedPath = control.FileName;
                controller.OnClosing(e);
            }
            finally
            {
                context.UnlockMainWindow();
            }
        }
    }
}
