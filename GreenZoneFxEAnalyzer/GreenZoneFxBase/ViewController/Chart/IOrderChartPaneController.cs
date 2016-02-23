using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface IOrderChartPaneController : IChartPaneController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IOrderChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IOrderChartController Chart
        {
            get;
        }

        Color DrawdownColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.OrdersView;")]
        IOrdersHistoryView OrdersView
        {
            get;
        }
    }

    [GreenRmi(BaseClass = "ClientChartPaneControllerEx")]
    public interface IClientOrderChartPaneController : IClientChartPaneController, IOrderChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientOrderChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientOrderChartController Chart
        {
            get;
        }

    }

    [GreenRmi(BaseClass = "ServerChartPaneControllerEx")]
    public interface IServerOrderChartPaneController : IServerChartPaneController, IOrderChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrderChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrderChartController Chart
        {
            get;
        }

    }
}
