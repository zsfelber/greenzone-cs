using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;

namespace GreenZoneFxEngine
{
    public interface IForm1TabPanel
    {
        TabPage TabPage
        {
            get;
        }
        EnvironmentRuntime Environment
        {
            get;
        }
        Form1 Form1
        {
            get;
        }
        void DockIt();
    }
    public interface IOrdersTabPanel : IForm1TabPanel
    {
        OrdersHistoryView Orders
        {
            get;
        }
    }
}
