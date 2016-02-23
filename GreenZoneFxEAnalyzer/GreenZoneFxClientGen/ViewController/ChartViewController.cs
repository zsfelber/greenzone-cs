using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;

using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class ChartViewController : ClientChartViewControllerBase
    {
        public ChartViewController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        public new NormalChartController Chart1
        {
            get
            {
                return (NormalChartController)base.Chart1;
            }
            protected set
            {
                base.Chart1 = value;
            }
        }

        public new ChartGroupController ChartGroupController
        {
            get
            {
                return (ChartGroupController)base.ChartGroupController;
            }
            protected set
            {
                base.ChartGroupController = value;
            }
        }

        public new CursorChartController CursorChart
        {
            get
            {
                return (CursorChartController)base.CursorChart;
            }
            protected set
            {
                base.CursorChart = value;
            }
        }

        public override void UpdateChartAndCursor()
        {
            Chart1.UpdateChartAndCursor();
        }

        public override void UpdateCursor()
        {
            Chart1.UpdateCursor();
        }

        public override void UpdateSeries()
        {
            Chart1.UpdateSeries();
        }
    }
}