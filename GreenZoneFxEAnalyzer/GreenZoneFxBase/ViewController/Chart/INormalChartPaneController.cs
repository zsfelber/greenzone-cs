using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface INormalChartPaneController : IChartChartPaneController
    {
    }

    [GreenRmi(BaseClass = "ClientChartChartPaneControllerEx")]
    public interface IClientNormalChartPaneController : IClientChartChartPaneController, INormalChartPaneController
    {
    }

    [GreenRmi]
    public interface IServerNormalChartPaneController : IServerChartChartPaneController, INormalChartPaneController
    {
    }
}
