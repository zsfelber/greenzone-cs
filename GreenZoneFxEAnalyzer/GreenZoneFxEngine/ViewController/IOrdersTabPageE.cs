using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine.ViewController
{
    public interface IOrdersTabPageControllerEx : IOrdersTabPageController
    {
        new MainWindowController MainWindow
        {
            get;
        }

        ServerEnvironmentRuntime Environment
        {
            get;
        }
        ServerOrdersHistoryView Orders
        {
            get;
        }
            
    }
}
