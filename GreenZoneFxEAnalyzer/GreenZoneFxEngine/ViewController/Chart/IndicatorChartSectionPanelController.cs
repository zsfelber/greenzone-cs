using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    public class IndicatorChartSectionPanelController : ServerIndicatorChartSectionPanelControllerBase
    {
        ServerIndicatorRuntime.DInstanceChanged ic;

        public IndicatorChartSectionPanelController(GreenRmiManager rmiManager, NormalChartController parent, IServerIndicatorRuntime indicatorRuntime)
            : base(rmiManager, parent)
        {
            CloseButton = new ButtonController(rmiManager, this);
            PropertiesButton = new ButtonController(rmiManager, this);
            this.CloseButton.Pressed += new ControllerEventHandler(this.button1_Click);
            this.PropertiesButton.Pressed += new ControllerEventHandler(this.propertiesButton_Click);

            Indicator = indicatorRuntime;

            indicatorRuntime.InstanceChanged += ic = new ServerIndicatorRuntime.DInstanceChanged(indicatorRuntime_InstanceChanged);
        }

        protected override IPriceLabelPaneController CreatePriceLabelPaneController()
        {
            return new PriceLabelPaneController(rmiManager, this);
        }

        IServerChartRuntime ChartRuntime
        {
            get
            {
                return Parent.ServerChartRuntime;
            }
        }

        public override IndicatorWindowType WindowType
        {
            get
            {
                return IndicatorWindowType.SEPARATE_WINDOW;
            }
        }

        public override string PriceFormat
        {
            get
            {
                if (Indicator == null)
                {
                    return base.PriceFormat;
                }
                else
                {
                    IServerSymbolContext symbolContext = ChartRuntime.SymbolRuntime.Context;

                    int d = Indicator.Session.IndicatorDigits;
                    if (d == 0)
                    {
                        d = symbolContext.Digits - Math.Max(0, Indicator.Session.DisplayScale);
                    }

                    d = Math.Min(d, symbolContext.Digits);
                    if (d > 0)
                    {
                        string f2 = "00000000000000000".Substring(0, d);
                        string f = "0." + f2;
                        return f;
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
        }

        public override int Scale
        {
            get
            {
                if (Indicator == null)
                {
                    return base.Scale;
                }
                else
                {
                    return Indicator.Session.DisplayScale;
                }
            }
        }


        protected override void CreateChartPane(ServerChartControllerEx parent)
        {
            ChartPane = new IndicatorChartPaneController(rmiManager, this, (NormalChartController)parent);
        }

        private void button1_Click(object sender, ControllerEventArgs e)
        {
            IServerNormalChartController parent = Parent;
            parent.ServerChartRuntime.RemoveIndicator(ServerIndicator.Id);
            parent.RemoveIndicatorPanel(ServerIndicator);
            parent.UpdateSeries();
            parent.ChartPanel.UpdateIndicators();
            parent.MainWindow.SaveSession();
        }

        private void propertiesButton_Click(object sender, ControllerEventArgs e)
        {
            NormalChartController parent = Parent;
            parent.ChartPanel.ShowIndicatorProperties(indicatorRuntime);
        }

        void indicatorRuntime_InstanceChanged(IIndicatorRuntime newInstance)
        {
            indicatorRuntime.InstanceChanged -= ic;
            newInstance.InstanceChanged += ic;
            indicatorRuntime = newInstance;
        }
    }

}
