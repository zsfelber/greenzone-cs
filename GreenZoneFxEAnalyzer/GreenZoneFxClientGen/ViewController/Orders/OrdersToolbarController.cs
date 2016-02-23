using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Timers;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Orders
{
    
    public class OrdersToolbarController : OrdersToolbarControllerBase
    {

        public OrdersToolbarController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }
    }
}
