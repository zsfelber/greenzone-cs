using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvironmentAssistant;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using System.IO;
using SevenZip;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;
using GreenZoneFxEngine.Gui;
using GreenZoneUtil.Gui;
using GreenZoneUtil.ViewController;

namespace GreenZoneFxEngine
{
    public partial class Form1 : Form
    {

        public Form1(GreenWinFormsMVContext context)
        {
            this.context = context;

            InitializeComponent();
            Bind();
        }

        void Bind() {
            context.MainWindow = this;
            MainWindowController mainWindowController = context.MainWindowController;

            //new SplitContainerVCBinder(context, treeView1, mainWindowController.NavigatorController.);
            //new SplitContainerVCBinder(context, toolStripContainer1, mainWindowController.;
            //menuStrip1;
            //new SplitContainerVCBinder(context, fileToolStripMenuItem, mainWindowController.fileToolStripMenuItem;
            //new SplitContainerVCBinder(context, importToolStripMenuItem, mainWindowController.importToolStripMenuItem;
            //new SplitContainerVCBinder(context, exportToolStripMenuItem, mainWindowController.exportToolStripMenuItem;
            //new SplitContainerVCBinder(context, viewToolStripMenuItem, mainWindowController.viewToolStripMenuItem;
            //new SplitContainerVCBinder(context, toolsToolStripMenuItem, mainWindowController.toolsToolStripMenuItem;
            //new SplitContainerVCBinder(context, loadSessionToolStripMenuItem, mainWindowController.loadSessionToolStripMenuItem;
            //new SplitContainerVCBinder(context, saveSessionToolStripMenuItem, mainWindowController.saveSessionToolStripMenuItem;
            //toolStripSeparator2
            //new SplitContainerVCBinder(context, showConsoleToolStripMenuItem, mainWindowController.showConsoleToolStripMenuItem;
            //new SplitContainerVCBinder(context, testerProptertiesToolStripMenuItem, mainWindowController.testerProptertiesToolStripMenuItem;
            //new SplitContainerVCBinder(context, helpToolStripMenuItem, mainWindowController.helpToolStripMenuItem;
            //new SplitContainerVCBinder(context, aboutToolStripMenuItem, mainWindowController.aboutToolStripMenuItem;
            //new SplitContainerVCBinder(context, statusStrip1, mainWindowController.statusStrip1;
            //new SplitContainerVCBinder(context, toolStripProgressBar1, mainWindowController.toolStripProgressBar1;
            //new SplitContainerVCBinder(context, toolStrip1, mainWindowController.toolStrip1;
            //new SplitContainerVCBinder(context, toolStripLabel1, mainWindowController.toolStripLabel1;
            //toolStripSeparator1
            //toolStripSeparator3
            //new SplitContainerVCBinder(context, closeLabel, mainWindowController.closeLabel;
            //new SplitContainerVCBinder(context, tabPage1, mainWindowController.tabPage1;
            //new SplitContainerVCBinder(context, eaTesterTab, mainWindowController.eaTesterTab;
            // new SplitContainerVCBinder(context, scriptRunnerTab, mainWindowController.;


            new FormVCBinder(context, this, mainWindowController);

            new SplitContainerVCBinder(context, splitContainer1, mainWindowController.SplitContainer1);
            new ToolStripMenuCheckVCBinder(context, navigatorToolStripMenuItem, mainWindowController.NavigatorToolStripMenuItem);
            new ToolStripMenuItemVCBinder(context, newEnvironmentAssistantToolStripMenuItem, mainWindowController.NewEnvironmentToolStripMenuItem);
            new ToolStripMenuVCBinder(context, updateEnvironmentMenuItem, mainWindowController.UpdateEnvironmentMenuItem);
            new ToolStripMenuItemVCBinder(context, editEnvorinmentsToolStripMenuItem, mainWindowController.EditEnvorinmentsToolStripMenuItem);
            new ToolStripMenuItemVCBinder(context, optionsToolStripMenuItem, mainWindowController.OptionsToolStripMenuItem);
            new ToolStripComboBoxVCBinder(context, environmentCombo, mainWindowController.EnvironmentCombo);
            new ToolStripButtonVCBinder(context, toolStripButton1, mainWindowController.ToolStripButton1);
            new ToolStripButtonVCBinder(context, toolStripButton2, mainWindowController.ToolStripButton2);
            new ToolStripButtonVCBinder(context, toolStripButton3, mainWindowController.ToolStripButton3);
            new SplitContainerVCBinder(context, splitContainer2, mainWindowController.SplitContainer2);
            new TabBinder(this);

            new TabControlVCBinder(context, launcherTabControl, mainWindowController.LauncherTabControl);
            new TabPageVCBinder(context, eaTesterTab, mainWindowController.EaTester);
            new TabPageVCBinder(context, scriptRunnerTab, mainWindowController.ScriptRunner);
            eaTesterPanel1.Bind(context, mainWindowController.EaTester);
            scriptRunnerPanel1.Bind(context, mainWindowController.ScriptRunner);

            new ToolStripLabelVCBinder1(context, timeLabel, mainWindowController.TimeLabel);
            new ToolStripLabelVCBinder1(context, openLabel, mainWindowController.OpenLabel);
            new ToolStripLabelVCBinder1(context, lowLabel, mainWindowController.LowLabel);
            new ToolStripLabelVCBinder1(context, highLabel, mainWindowController.HighLabel);
            new ToolStripLabelVCBinder1(context, closeLabel, mainWindowController.CloseLabel);
            new ToolStripLabelVCBinder1(context, valueLabel, mainWindowController.ValueLabel);
            new ToolStripLabelVCBinder1(context, statusLabel, mainWindowController.StatusLabel);
            new ToolStripLabelVCBinder1(context, toolStripStatusLabel2, mainWindowController.ToolStripStatusLabel2);
            new ToolStripLabelVCBinder1(context, OLabel, mainWindowController.OLabel);
            new ToolStripLabelVCBinder1(context, LLabel, mainWindowController.LLabel);
            new ToolStripLabelVCBinder1(context, HLabel, mainWindowController.HLabel);
            new ToolStripLabelVCBinder1(context, CLabel, mainWindowController.CLabel);
            new ToolStripLabelVCBinder1(context, VLabel, mainWindowController.VLabel);

            new ToolStripMenuCheckVCBinder(context, ordersOverviewToolStripMenuItem, mainWindowController.OrdersOverviewToolStripMenuItem);
            new ToolStripMenuCheckVCBinder(context, ordersTableToolStripMenuItem, mainWindowController.OrdersTableToolStripMenuItem);

        }

        readonly GreenWinFormsMVContext context;
        public GreenWinFormsMVContext Context
        {
            get
            {
                return context;
            }
        }

        class TabBinder : CoolTabControlVCBinder
        {
            internal TabBinder(Form1 form)
                : base(form.context, form.tabControl1, form.context.MainWindowController.TabControl1)
            {
            }
            protected override void AddChild(TabPageController child1, int index = -1)
            {
                base.AddChild(child1);
                if (index == -1)
                {
                    index = control.TabCount - 1;
                }
                TabPage newPage = control.TabPages[index];
                MainWinTabPageController tabPanel = (MainWinTabPageController)child1;
                if (tabPanel is ChartGroupController)
                {
                    ChartGroupPanel p = new ChartGroupPanel();
                    p.Location = new Point(0, 0);
                    p.Dock = DockStyle.Fill;
                    newPage.Controls.Add(p);
                    p.Bind((GreenWinFormsMVContext)context, (ChartGroupController)tabPanel);
                }
                else if (tabPanel is OrdersOverviewController)
                {
                    OrdersOverviewPanel p = new OrdersOverviewPanel();
                    p.Location = new Point(0, 0);
                    p.Dock = DockStyle.Fill;
                    newPage.Controls.Add(p);
                    p.Bind((GreenWinFormsMVContext)context, (OrdersOverviewController)tabPanel);
                }
                else if (tabPanel is OrdersTableController)
                {
                    OrdersTablePanel p = new OrdersTablePanel();
                    p.Location = new Point(0, 0);
                    p.Dock = DockStyle.Fill;
                    newPage.Controls.Add(p);
                    p.Bind((GreenWinFormsMVContext)context, (OrdersTableController)tabPanel);
                }
                else
                {
                    throw new NotSupportedException("tabPanel : " + tabPanel);
                }
            }
        }
    }
}
