using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface IIndicatorChartPaneController : IChartChartPaneController
    {
        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Font LevelFont
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartChartPaneControllerEx")]
    public interface IClientIndicatorChartPaneController : IClientChartChartPaneController, IIndicatorChartPaneController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientIndicatorChartSectionPanelController Parent
        {
            get;
        }
    }

    [GreenRmi]
    public interface IServerIndicatorChartPaneController : IServerChartChartPaneController, IIndicatorChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerIndicatorChartSectionPanelController Parent
        {
            get;
        }
    }
}
