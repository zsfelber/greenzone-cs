using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;

using GreenZoneFxEngine.Types;
using System.Drawing;
using System.Drawing.Drawing2D;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface ICursorChartController : IChartController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartGroupController ChartGroupPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartViewController ChartPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return ChartRuntime.CursorRuntime;")]
        IChartCursorRuntime CursorRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IChartRuntime)Owner;")]
        IChartRuntime ChartRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return ChartRuntime.Group;")]
        IChartGroupRuntime ChartGroupRuntime
        {
            get;
        }

    }

    [GreenRmi(BaseClass = "ClientChartControllerEx")]
    public interface IClientCursorChartController : IClientChartController, ICursorChartController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartGroupController ChartGroupPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartViewController ChartPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return ChartRuntime.CursorRuntime;")]
        new IClientChartCursorRuntime CursorRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IClientChartRuntime)Owner;")]
        new IClientChartRuntime ChartRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IClientChartGroupRuntime)ChartRuntime.Group;")]
        new IClientChartGroupRuntime ChartGroupRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientCursorChartSectionPanelController MasterChartSectionPanel
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ServerChartControllerEx")]
    public interface IServerCursorChartController : IServerChartController, ICursorChartController
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return ChartRuntime.CursorRuntime;")]
        new IServerChartCursorRuntime CursorRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IServerChartRuntime)Owner;")]
        new IServerChartRuntime ChartRuntime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerCursorChartSectionPanelController MasterChartSectionPanel
        {
            get;
            set;
        }

    }
}
