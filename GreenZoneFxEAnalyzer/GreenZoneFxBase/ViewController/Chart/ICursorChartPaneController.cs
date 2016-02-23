using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Drawing;
using GreenZoneUtil.ViewController;
using System.Drawing.Drawing2D;

using GreenZoneFxEngine.Util;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface ICursorChartPaneController : IChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Font YearFont
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Font MonthFont
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color GradientColor
        {
            get;
            set;
        }

        new Color BackColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color ChartFrameColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Rectangle ChartFrameRect
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ClientChartPaneControllerEx")]
    public interface IClientCursorChartPaneController : IClientChartPaneController, ICursorChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientCursorChartController Chart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientCursorChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.CursorRuntime;")]
        IClientChartCursorRuntime CursorRuntime
        {
            get;
        }
    }

    [GreenRmi(BaseClass = "ServerChartPaneControllerEx")]
    public interface IServerCursorChartPaneController : IServerChartPaneController, ICursorChartPaneController
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerCursorChartController Chart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerCursorChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.CursorRuntime;")]
        IServerChartCursorRuntime CursorRuntime
        {
            get;
        }

    }
}
