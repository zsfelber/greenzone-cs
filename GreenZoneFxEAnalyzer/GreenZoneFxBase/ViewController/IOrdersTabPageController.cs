using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{

    
    [GreenRmi]
    public interface IOrdersTabPageController : IMainWinTabPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersToolbarController OrdersToolbar1
        {
            get;
            set;
        }

    }
}
