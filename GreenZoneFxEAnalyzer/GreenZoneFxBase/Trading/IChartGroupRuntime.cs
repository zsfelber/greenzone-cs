using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{
    
    [GreenRmi]
    public interface IChartGroupRuntime : IRmiBase
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEnvironmentRuntime Environment
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartGroupSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IList<IChartRuntime> Charts
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        datetime ScrolledBarTime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        IChartRuntime MasterChart
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        IChartRuntime FirstConnectedChart
        {
            get;
        }
    }

    [GreenRmi(BaseClass = "ChartGroupRuntimeEx")]
    public interface IClientChartGroupRuntime : IChartGroupRuntime
    {

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int CursorPosition
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ChartGroupRuntimeEx")]
    public interface IServerChartGroupRuntime : IChartGroupRuntime
    {

        void AddChart(IServerChartRuntime chart);

        void RemoveChart(IServerChartRuntime chart);

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartRuntime MasterChart
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartRuntime FirstConnectedChart
        {
            get;
        }
    }
}
