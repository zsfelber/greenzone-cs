using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface INormalChartSectionPanelController : IChartSectionPanelController
    {
    }

    [GreenRmi(BaseClass = "ClientChartSectionPanelControllerEx")]
    public interface IClientNormalChartSectionPanelController : IClientChartSectionPanelController, INormalChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientNormalChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartPaneController ChartPane
        {
            get;
        }


    }

    [GreenRmi(BaseClass = "ServerChartSectionPanelControllerEx")]
    public interface IServerNormalChartSectionPanelController : IServerChartSectionPanelController, INormalChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerNormalChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartPaneController ChartPane
        {
            get;
        }


    }
}
