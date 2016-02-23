using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IOrdersTableController : IOrdersTabPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        MultiSplitController WormSplitContainer1
        {
            get;
            set;
        }
    }
}
