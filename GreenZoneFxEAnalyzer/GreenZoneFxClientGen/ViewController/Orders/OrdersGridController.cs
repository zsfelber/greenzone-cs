using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Orders
{
    
    public class OrdersGridController : OrdersGridControllerBase
    {
        public OrdersGridController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

    }
}
