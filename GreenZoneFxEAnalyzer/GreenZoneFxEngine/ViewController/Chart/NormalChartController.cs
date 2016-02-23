using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;

using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class NormalChartController : ServerNormalChartControllerBase
    {
        

        public NormalChartController(GreenRmiManager rmiManager, ChartViewController chartPanel, ServerChartRuntime chartRuntime)
            : base(rmiManager, chartPanel, chartRuntime)
        {
            TableLayoutPanel1.Add((Controller)MasterChartSectionPanel);

            UpdateIndicatorPanels();
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
            MasterChartSectionPanel = new NormalChartSectionPanelController(rmiManager, this);
        }

        public override void AddIndicatorPanel(IServerIndicatorRuntime indicatorRuntime)
        {
            if (indicatorRuntime.Session.WindowType == IndicatorWindowType.SEPARATE_WINDOW)
            {
                IndicatorChartSectionPanelController p = new IndicatorChartSectionPanelController(rmiManager, this, indicatorRuntime);
                chartSectionPanels.Add(p);

                TableLayoutPanel1.Add(p);
            }
        }

        public override void RemoveIndicatorPanel(IServerIndicatorRuntime indicatorRuntime)
        {
            if (indicatorRuntime.Session.WindowType == IndicatorWindowType.SEPARATE_WINDOW)
            {
                foreach (IndicatorChartSectionPanelController p in chartSectionPanels)
                {
                    if (p.Indicator == indicatorRuntime)
                    {
                        chartSectionPanels.Remove(p);
                        this.TableLayoutPanel1.Remove(p);
                        break;
                    }
                }
            }
        }

        public override void RemoveIndicatorPanel(IServerIndicatorChartSectionPanelController p)
        {
            chartSectionPanels.Remove(p);
            this.TableLayoutPanel1.Remove(p);
        }

        public override void UpdateIndicatorPanels()
        {
            //foreach (IndicatorChartSectionPanel p in chartSectionPanels)
            //{
            //    RemoveIndicatorPanel(p);
            //}
            if (chartSectionPanels.Count > 0)
            {
                this.TableLayoutPanel1.RemoveFrom((Controller)chartSectionPanels[0]);
                chartSectionPanels.Clear();
            }
            foreach (IServerIndicatorRuntime ind in ChartRuntime.GuiSeriesManager.DefaultCache)
            {
                if (ind.Visible)
                {
                    AddIndicatorPanel(ind);
                }
            }
        }

    }


}
