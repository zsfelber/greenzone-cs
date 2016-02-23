using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IMainWinTabPageController : ITabPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IMainWindowController MainWindow
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return MainWindow.Environment;")]
        IEnvironmentRuntime Environment
        {
            get;
        }
    }
}
