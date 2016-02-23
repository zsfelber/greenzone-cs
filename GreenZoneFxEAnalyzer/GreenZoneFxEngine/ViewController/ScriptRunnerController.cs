using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class ScriptRunnerController : ScriptRunnerControllerBase
    {
        internal ScriptRunnerController(GreenRmiManager rmiManager, MainWindowController mainWindow)
            : base(rmiManager, mainWindow.LauncherTabControl)
        {
            MainWindow = mainWindow;
            Text = "Script Runner";
        }

        public new MainWindowController MainWindow
        {
            get
            {
                return (MainWindowController)base.MainWindow;
            }
            protected set
            {
                base.MainWindow = value;
            }
        }

    }
}
