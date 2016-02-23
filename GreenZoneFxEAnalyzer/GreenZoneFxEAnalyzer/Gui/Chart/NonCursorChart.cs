using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine.Gui.Chart
{
    public partial class NonCursorChart : Chart
    {
        public NonCursorChart()
        {
            InitializeComponent();
        }

        public override void Bind(GreenWinFormsMVContext context, ChartController controller)
        {
            base.Bind(context, controller);

            this.timeLabelPane1.Bind(context, controller.TimeLabelPane1);
            new WormBinder(this);
            new ButtonVCBinder(context, zoomOutVButton, controller.ZoomOutVButton);
            new ButtonVCBinder(context, zoomOutHButton, controller.ZoomOutHButton);
            new ButtonVCBinder(context, zoomInVButton, controller.ZoomInVButton);
            new ButtonVCBinder(context, zoomInHButton, controller.ZoomInHButton);
            new ButtonVCBinder(context, zoomToFitButton, controller.ZoomToFitButton);
            new ButtonVCBinder(context, zoomToScrollPriceButton, controller.ZoomToScrollPriceButton);
        }


        class WormBinder : WormSplitContainerVCBinder
        {
            internal WormBinder(NonCursorChart form)
                : base(form.context, form.tableLayoutPanel1, form.controller.TableLayoutPanel1)
            {
            }

            protected override void AddChild(Controller child1)
            {
                ChartSectionPanelController p = (ChartSectionPanelController)child1;
                if (p is NormalChartSectionPanelController)
                {
                    NormalChartSectionPanel panel = new NormalChartSectionPanel();
                    panel.Bind((GreenWinFormsMVContext)context, (NormalChartSectionPanelController)p);
                    control.Add(panel);
                }
                else if (p is IndicatorChartSectionPanelController)
                {
                    IndicatorChartSectionPanel panel = new IndicatorChartSectionPanel();
                    panel.Bind((GreenWinFormsMVContext)context, (IndicatorChartSectionPanelController)p);
                    control.Add(panel);
                }
                else if (p is OrderChartSectionPanelController)
                {
                    OrderChartSectionPanel panel = new OrderChartSectionPanel();
                    panel.Bind((GreenWinFormsMVContext)context, (OrderChartSectionPanelController)p);
                    control.Add(panel);
                }
                else
                {
                    throw new NotSupportedException("" + p);
                }
            }
        }
    }
}
