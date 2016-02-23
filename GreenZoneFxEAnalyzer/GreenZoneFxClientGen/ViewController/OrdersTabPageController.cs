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

    
    public abstract class OrdersTabPageController : OrdersTabPageControllerBase
    {
        public OrdersTabPageController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }


    }
}
