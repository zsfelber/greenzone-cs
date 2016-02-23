using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.Gui.ViewController;
using System.Drawing;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneFxEngine.Properties;
using System.Windows.Forms;
using EnvironmentAssistant;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine
{
    public class GreenWinFormsMVContext : WinFormsMVContext
    {
        Timer lockWinTimer;

        public GreenWinFormsMVContext(MainWindowController mainWindowController)
        {
            this.mainWindowController = mainWindowController;
            lockWinTimer = new Timer();
            lockWinTimer.Interval = 50;
            lockWinTimer.Tick += new EventHandler(lockWinTimer_Tick);
        }

        readonly MainWindowController mainWindowController;
        public MainWindowController MainWindowController
        {
            get
            {
                return mainWindowController;
            }
        }

        Form1 mainWindow;
        internal Form1 MainWindow
        {
            get
            {
                return mainWindow;
            }
            set
            {
                mainWindow = value;
            }
        }

        protected override void DialogController_ShowDialogInvoked(ShowDialogEventArgs args)
        {
            if (args.Dialog is AddScriptController)
            {
                AddScriptDialog d = new AddScriptDialog();
                d.Bind(this, (AddScriptController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else if (args.Dialog is AddExpertController)
            {
                AddExpertDialog d = new AddExpertDialog();
                d.Bind(this, (AddExpertController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else if (args.Dialog is EnvironmentAssistantController)
            {
                EnvironmentAssistantForm d = new EnvironmentAssistantForm();
                d.Bind(this, (EnvironmentAssistantController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else if (args.Dialog is EnvironmentSettingsController)
            {
                EnvironmentDialog d = new EnvironmentDialog();
                d.Bind(this, (EnvironmentSettingsController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else if (args.Dialog is ExpertDialogController)
            {
                ExpertRuntimeDialog d = new ExpertRuntimeDialog();
                d.Bind(this, (ExpertDialogController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else if (args.Dialog is ScriptDialogController)
            {
                ScriptRuntimeDialog d = new ScriptRuntimeDialog();
                d.Bind(this, (ScriptDialogController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else if (args.Dialog is IndicatorDialogController)
            {
                IndicatorRuntimeDialog d = new IndicatorRuntimeDialog();
                d.Bind(this, (IndicatorDialogController)args.Dialog);
                IWin32Window invoker = (IWin32Window)args.Invoker.BoundControl;
                args.DialogResult = d.ShowDialog(invoker);
            }
            else
            {
                base.DialogController_ShowDialogInvoked(args);
            }
        }

        public override Image GetImage(int imageId)
        {
            switch ((AppImage)imageId)
            {
                case AppImage.action_add_16xMD: return Resources.action_add_16xMD;
                case AppImage.action_Cancel_16xSM: return Resources.action_Cancel_16xSM;
                case AppImage.Close_16xLG: return Resources.Close_16xLG;
                case AppImage.CursorBar_16xLG: return Resources.CursorBar_16xLG;
                case AppImage.CursorBarB_16xLG: return Resources.CursorBarB_16xLG;
                case AppImage.empty: return Resources.empty;
                case AppImage.Error_red_12x11: return Resources.Error_red_12x11;
                case AppImage.None: return null;
                case AppImage.Warning_yellow_7231_12x11: return Resources.Warning_yellow_7231_12x11;
            }
            return null;
        }

        bool mainWinLocked = false;
        bool toUnlock = false;
        public override void LockMainWindow()
        {
            if (!mainWinLocked)
            {
                mainWinLocked = true;
                TWinFct.LockControlUpdate(mainWindow);
            }
            else
            {
                toUnlock = false;
            }
        }

        public override void UnlockMainWindow()
        {
            toUnlock = true;
            lockWinTimer.Start();
        }


        void lockWinTimer_Tick(object sender, EventArgs e)
        {
            if (toUnlock)
            {
                toUnlock = false;
                mainWinLocked = false;
                lockWinTimer.Stop();
                TWinFct.UnLockControlUpdate(mainWindow);
            }
        }
    }
}
