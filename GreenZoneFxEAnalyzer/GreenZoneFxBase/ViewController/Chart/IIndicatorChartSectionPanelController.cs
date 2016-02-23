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
    public interface IIndicatorChartSectionPanelController : IChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController CloseButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController PropertiesButton
        {
            get;
            set;
        }

        new SeriesRange SectionRange
        {
            get;
            set;
        }

        IIndicatorRuntime Indicator
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartSectionPanelControllerEx")]
    public interface IClientIndicatorChartSectionPanelController : IClientChartSectionPanelController, IIndicatorChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientIndicatorChartPaneController ChartPane
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientNormalChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientIndicatorRuntime Indicator
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New, typeof(IIndicatorChartSectionPanelController))]
        new SeriesRange SectionRange
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.ChartRuntime;")]
        IClientChartRuntime ChartRuntime
        {
            get;
        }
    }

    [GreenRmi(BaseClass = "ServerChartSectionPanelControllerEx")]
    public interface IServerIndicatorChartSectionPanelController : IServerChartSectionPanelController, IIndicatorChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerIndicatorChartPaneController ChartPane
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerNormalChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerIndicatorRuntime Indicator
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New, typeof(IIndicatorChartSectionPanelController))]
        new SeriesRange SectionRange
        {
            get;
            set;
        }
    }
}
