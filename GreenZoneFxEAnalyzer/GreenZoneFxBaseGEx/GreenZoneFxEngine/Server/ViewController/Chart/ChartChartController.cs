using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;


namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ServerChartChartControllerEx : ServerChartChartControllerBase
    {

        public ServerChartChartControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ServerChartChartControllerEx(GreenRmiManager rmiManager, IChartViewController chartPanel, IChartRuntime chartRuntime)
            : base(rmiManager, chartPanel.ChartGroupController, chartRuntime)
        {
            ChartPanel = chartPanel;
            ChartGroupPanel = chartPanel.ChartGroupController;
        }

        public ServerChartChartControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected ServerChartChartControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }


}