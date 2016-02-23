using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface IChartChartPaneController : IChartPaneController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        bool SectionOrZigZag
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartPaneControllerEx")]
    public interface IClientChartChartPaneController : IClientChartPaneController, IChartChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartChartController Chart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.ChartRuntime;")]
        IClientChartRuntime ChartRuntime
        {
            get;
        }

        List<SeriesBar> SeriesBarsFrom
        {
            get;
            set;
        }

        List<SeriesBar> SeriesBarsTo
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ServerChartPaneControllerEx")]
    public interface IServerChartChartPaneController : IServerChartPaneController, IChartChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartChartController Chart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.ChartRuntime;")]
        IServerChartRuntime ChartRuntime
        {
            get;
        }
    }
}
