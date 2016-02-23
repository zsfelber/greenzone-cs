using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GreenZoneUtil.ViewController;
using System.Windows.Forms;

namespace GreenZoneUtil.Gui.ViewController
{
    public abstract class WinFormsMVContext
    {
        public WinFormsMVContext()
        {
            DialogController.ShowDialogInvoked += new ShowDialogEventHandler(DialogController_ShowDialogInvoked);
        }

        protected virtual void DialogController_ShowDialogInvoked(ShowDialogEventArgs args)
        {
            if (args.Dialog is MessageBoxController)
            {
                MessageBoxEventArgs msg = (MessageBoxEventArgs)args;
                if (args.Invoker != null)
                {
                    args.DialogResult = MessageBox.Show((IWin32Window)args.Invoker.BoundControl, msg.Text, msg.Caption, msg.Buttons, msg.Icon);
                }
                else if (msg.Icon != 0)
                {
                    args.DialogResult = MessageBox.Show(msg.Text, msg.Caption, msg.Buttons, msg.Icon);
                }
                else
                {
                    args.DialogResult = MessageBox.Show(msg.Text, msg.Caption, msg.Buttons);
                }
            }
        }

        public abstract Image GetImage(int imageId);

        public abstract void LockMainWindow();

        public abstract void UnlockMainWindow();
    }
}
