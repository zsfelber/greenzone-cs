using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Gui.Chart;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine
{
    public partial class ChartGroupPanel : UserControl
    {
        public ChartGroupPanel()
        {
            InitializeComponent();
        }

        public void Bind(GreenWinFormsMVContext context, ChartGroupController controller)
        {
            this.context = context;
            this.controller = controller;
            new SimpleControlVCBinder(context, this, controller);

            //toolStripContainer1;
            new SimpleControlVCBinder(context, bottomToolStrip, controller.BottomToolStrip);
            new ToolStripButtonVCBinder(context, toggleBottomBarButton1, controller.ToggleBottomBarButton1);
            //new ToolStripLabelVCBinder1(context,toolStripLabel6 , controller.toolStripLabel6);
            //new ToolStripLabelVCBinder1(context,toolStripLabel1 , controller.toolStripLabel1);
            new ToolStripComboBoxVCBinder(context, eaCombo, controller.EaCombo);
            new ToolStripButtonVCBinder(context, openEaButton, controller.OpenEaButton);
            new ToolStripChbuttonVCBinder(context, inTestButton, controller.InTestButton);
            //new ToolStripLabelVCBinder(context,toolStripLabel2 , controller.toolStripLabel2);
            new ToolStripComboBoxVCBinder(context, scriptCombo, controller.ScriptCombo);
            new ToolStripButtonVCBinder(context, openScriptButton, controller.OpenScriptButton);
            new WormBinder(this);
            new ButtonVCBinder(context, toggleBottomBarButton2, controller.ToggleBottomBarButton2);
        }

        GreenWinFormsMVContext context;
        public GreenWinFormsMVContext Context
        {
            get
            {
                return context;
            }
        }

        ChartGroupController controller;
        public ChartGroupController Controller
        {
            get
            {
                return controller;
            }
        }

        class WormBinder : WormSplitContainerVCBinder
        {
            internal WormBinder(ChartGroupPanel form)
                : base(form.context, form.tableLayoutPanel1, form.controller.TableLayoutPanel1)
            {
            }

            protected override void AddChild(Controller child1)
            {
                ChartViewController p = (ChartViewController)child1;
                ChartPanel panel = new ChartPanel();
                panel.Bind((GreenWinFormsMVContext)context, p);
                control.Add(panel);
            }
        }
    }
}
