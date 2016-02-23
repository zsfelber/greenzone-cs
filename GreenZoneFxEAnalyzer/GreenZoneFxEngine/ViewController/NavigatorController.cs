using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class NavigatorController : NavigatorControllerBase
    {
        internal NavigatorController(GreenRmiManager rmiManager, MainWindowController mainWindow)
            : base(rmiManager, mainWindow)
        {
            MainWindow = mainWindow;
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
