using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IOrdersOverviewController : IOrdersTabPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrderChartController OrderChart1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal)]
        IOrdersHistoryView Orders
        {
            get;
        }
    }

    [GreenRmi]
    public interface IServerOrdersOverviewController : IOrdersOverviewController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerEnvironmentRuntime Environment
        {
            get;
        }

    }
}
