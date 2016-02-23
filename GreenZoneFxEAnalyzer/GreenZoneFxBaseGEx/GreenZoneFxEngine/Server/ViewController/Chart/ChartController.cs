using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneUtil.Util;

using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public abstract class ServerChartControllerEx : ServerChartControllerBase
    {

        internal readonly List<IChartSectionPanelController> chartSectionPanels;

        public ServerChartControllerEx(GreenRmiManager rmiManager, Controller parent)
            : base(rmiManager, parent)
        {
        }

        public ServerChartControllerEx(GreenRmiManager rmiManager, IMainWinTabPageController tabPanel, IServerChartOwner owner)
            : base(rmiManager, (Controller)tabPanel)
        {
            MainWindow = tabPanel.MainWindow;
            chartSectionPanels = new List<IChartSectionPanelController>();
            ChartSectionPanels = chartSectionPanels.AsReadOnly();
            TableLayoutPanel1 = new MultiSplitController(rmiManager, this);
            TimeLabelPane1 = CreateTimeLabelPaneController();
            ZoomOutVButton = new ButtonController(rmiManager, this);
            ZoomOutHButton = new ButtonController(rmiManager, this);
            ZoomInVButton = new ButtonController(rmiManager, this);
            ZoomInHButton = new ButtonController(rmiManager, this);
            ZoomToFitButton = new ButtonController(rmiManager, this);
            ZoomToScrollPriceButton = new ButtonController(rmiManager, this);

            Owner = owner;
            TabPanel = tabPanel;

            CreateMaster();
        }

        protected ServerChartControllerEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        public ServerChartControllerEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected abstract ITimeLabelPaneController CreateTimeLabelPaneController();


        public override Color SliderBarColor
        {
            get
            {
                return base.SliderBarColor;
            }
            set
            {
                if (base.SliderBarColor != value)
                {
                    base.SliderBarColor = value;
                    MasterChartSectionPanel.ChartPane.SliderBarColor = value;
                    foreach (ServerChartSectionPanelControllerEx p in chartSectionPanels)
                    {
                        p.ChartPane.SliderBarColor = value;
                    }
                }
            }
        }


        protected abstract void CreateMaster();
    }
}