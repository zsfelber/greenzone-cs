using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface IOrderChartSectionPanelController : IChartSectionPanelController
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return (IOrderChartController)Parent;")]
        IOrderChartController OrderParent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController PropertiesButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return OrderParent.OrdersOverviewPanel;")]
        IOrdersOverviewController OrdersOverviewPanel
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return OrderParent.OrdersView;")]
        IOrdersHistoryView OrdersView
        {
            get;
        }

        new SeriesRange SectionRange
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartSectionPanelControllerEx")]
    public interface IClientOrderChartSectionPanelController : IClientChartSectionPanelController, IOrderChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientOrderChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientOrderChartPaneController ChartPane
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New, typeof(IOrderChartSectionPanelController))]
        new SeriesRange SectionRange
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ServerChartSectionPanelControllerEx")]
    public interface IServerOrderChartSectionPanelController : IServerChartSectionPanelController, IOrderChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrderChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrderChartPaneController ChartPane
        {
            get;
        }
    }
}
