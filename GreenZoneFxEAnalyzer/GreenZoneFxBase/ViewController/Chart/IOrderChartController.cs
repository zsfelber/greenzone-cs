using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface IOrderChartController : IChartController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersOverviewController OrdersOverviewPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrdersHistoryView OrdersView
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartControllerEx")]
    public interface IClientOrderChartController : IClientChartController, IOrderChartController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientOrderChartSectionPanelController MasterChartSectionPanel
        {
            get;
        }
    }

    [GreenRmi(BaseClass = "ServerChartControllerEx")]
    public interface IServerOrderChartController : IServerChartController, IOrderChartController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrderChartSectionPanelController MasterChartSectionPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        IServerOrdersHistoryView OrdersView
        {
            get;
            set;
        }
    }
}
