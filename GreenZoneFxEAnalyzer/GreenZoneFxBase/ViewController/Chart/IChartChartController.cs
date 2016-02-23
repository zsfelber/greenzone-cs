using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;


namespace GreenZoneFxEngine.ViewController.Chart
{
    [GreenRmi]
    public interface IChartChartController : IChartController
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
    public interface IClientChartChartController : IClientChartController, IChartChartController
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

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IClientChartRuntime)Owner;")]
        new IClientChartRuntime ChartRuntime
        {
            get;
        }

        void FindPriceMinMax(ref double min, ref double max);
    }

    [GreenRmi(BaseClass = "ServerChartControllerEx")]
    public interface IServerChartChartController : IServerChartController, IChartChartController
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return (IServerChartRuntime)Owner;")]
        new IServerChartRuntime ChartRuntime
        {
            get;
        }
    }
}
