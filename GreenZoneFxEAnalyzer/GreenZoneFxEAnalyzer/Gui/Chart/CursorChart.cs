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
    public partial class CursorChart : Chart
    {
        public CursorChart()
        {
            InitializeComponent();
        }

        public override void Bind(GreenWinFormsMVContext context, ChartController controller)
        {
            base.Bind(context, controller);

            new WormBinder(this);
        }


        class WormBinder : WormSplitContainerVCBinder
        {
            internal WormBinder(CursorChart form)
                : base(form.context, form.tableLayoutPanel1, form.controller.TableLayoutPanel1)
            {
            }

            protected override void AddChild(Controller child1)
            {
                ChartSectionPanelController p = (ChartSectionPanelController)child1;
                if (p is CursorChartSectionPanelController)
                {
                    CursorChartSectionPanel panel = new CursorChartSectionPanel();
                    panel.Bind((GreenWinFormsMVContext)context, (CursorChartSectionPanelController)p);
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
