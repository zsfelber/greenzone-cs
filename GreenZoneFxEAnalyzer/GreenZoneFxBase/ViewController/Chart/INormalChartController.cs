using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;

using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface INormalChartController : IChartChartController
    {
    }

    [GreenRmi(BaseClass = "ClientChartChartControllerEx")]
    public interface IClientNormalChartController : IClientChartChartController, INormalChartController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartSectionPanelController MasterChartSectionPanel
        {
            get;
        }

    }

    [GreenRmi(BaseClass = "ServerChartChartControllerEx")]
    public interface IServerNormalChartController : IServerChartChartController, INormalChartController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartSectionPanelController MasterChartSectionPanel
        {
            get;
        }

        void AddIndicatorPanel(IServerIndicatorRuntime indicatorRuntime);

        void RemoveIndicatorPanel(IServerIndicatorRuntime indicatorRuntime);

        void RemoveIndicatorPanel(IServerIndicatorChartSectionPanelController p);

        void UpdateIndicatorPanels();
    }
}
