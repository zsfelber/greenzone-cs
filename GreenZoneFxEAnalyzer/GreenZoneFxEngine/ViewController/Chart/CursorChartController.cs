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
    public class CursorChartController : ServerCursorChartControllerBase
    {

        public CursorChartController(GreenRmiManager rmiManager, ChartViewController chartPanel, ServerChartCursorRuntime cursorRuntime)
            : base(rmiManager, chartPanel.ChartGroupController, cursorRuntime.Parent)
        {
            ChartPanel = chartPanel;
            ChartGroupPanel = chartPanel.ChartGroupController;
            this.TableLayoutPanel1.Add((Controller)MasterChartSectionPanel);
        }

        protected override ITimeLabelPaneController CreateTimeLabelPaneController()
        {
            return new TimeLabelPaneController(rmiManager, this);
        }


        public new ChartViewController ChartPanel
        {
            get
            {
                return (ChartViewController)base.ChartPanel;
            }
            protected set
            {
                base.ChartPanel = value;
            }
        }

        public new ChartGroupController ChartGroupPanel
        {
            get
            {
                return (ChartGroupController)base.ChartGroupPanel;
            }
            protected set
            {
                base.ChartGroupPanel = value;
            }
        }


        protected override void CreateMaster()
        {
            MasterChartSectionPanel = new CursorChartSectionPanelController(rmiManager, this);
        }

    }

}
