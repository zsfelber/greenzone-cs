using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{

    [GreenRmi]
    public interface ICursorChartSectionPanelController : IChartSectionPanelController
    {

        new SeriesRange SectionRange
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartSectionPanelControllerEx")]
    public interface IClientCursorChartSectionPanelController : IClientChartSectionPanelController, ICursorChartSectionPanelController
    {
        [GreenRmiField(GreenRmiFieldType.New, typeof(ICursorChartSectionPanelController))]
        new SeriesRange SectionRange
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientCursorChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientCursorChartPaneController ChartPane
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IClientChartCursorRuntime)Owner;")]
        IClientChartCursorRuntime CursorRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return IndicatorWindowType.CHART_WINDOW;")]
        new IndicatorWindowType WindowType
        {
            get;
        }

        void ScrollYToPrice();
        
        void FindPriceMinMax(ref double min, ref double max);

    }

    [GreenRmi(BaseClass = "ServerChartSectionPanelControllerEx")]
    public interface IServerCursorChartSectionPanelController : IServerChartSectionPanelController, ICursorChartSectionPanelController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerCursorChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerCursorChartPaneController ChartPane
        {
            get;
        }
    }
}
