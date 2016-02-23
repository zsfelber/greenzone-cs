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

namespace GreenZoneFxEngine
{
    public partial class Form1 : Form
    {

        OrdersTablePanel ordersTablePanel;
        OrdersOverviewPanel ordersOverviewPanel;

        private bool sessionSave = false;

        public Form1()
        {
            InitializeComponent();
            eaTesterPanel1.Init(this);
            scriptRunnerPanel1.Init(this);

            UpdateEnvironments();

            string envId = EAnalyzerOptions.Singleton.DefaultEnvironment;
            if (!string.IsNullOrEmpty(envId))
            {
                var env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envId);
                environmentCombo.SelectedItem = envId;//env ->Id<- !!
                LoadScreen();
            }
            else
            {
                UpdateEnvironment();
            }
            timer1.Start();
            Initialized = true;
        }

        private bool Initialized
        {
            get;
            set;
        }

        public EnvironmentRuntime Environment
        {
            get
            {
                string envId = (string)environmentCombo.SelectedItem;
                if (envId == null)
                {
                    return null;
                }
                else
                {
                    var env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envId);
                    return env;
                }
            }
        }

        public List<IForm1TabPanel> TabPanels
        {
            get
            {
                List<IForm1TabPanel> r = new List<IForm1TabPanel>();
                for (int i = 0; i < tabControl1.TabCount - 1; i++)
                {
                    IForm1TabPanel p = (IForm1TabPanel)tabControl1.TabPages[i].Controls[0];
                    r.Add(p);
                }
                return r;
            }
        }

        internal void SaveSession()
        {
            sessionSave = true;
        }

        internal void UpdateEnvironment()
        {
            if (Environment == null)
            {
                splitContainer1.Enabled = false;
                toolStripButton2.Enabled = false;
                EAnalyzerOptions.Singleton.DefaultEnvironment = null;
            }
            else
            {
                splitContainer1.Enabled = true;
                toolStripButton2.Enabled = true;
                EAnalyzerOptions.Singleton.DefaultEnvironment = Environment.EnvironmentId;

                LoadScreen();
            }
            EAnalyzerOptions.Singleton.Save();

            TabPage pAddBtn = tabControl1.TabPages[0];
            tabControl1.TabPages.Clear();
            tabControl1.PerformLayout();
            ordersTablePanel = null;
            ordersOverviewPanel = null;

            if (Environment != null)
            {
                foreach (var r in Environment.Charts)
                {
                    AddChartGroup(r, false, true);
                }
            }

            UpdateListInLauncherPanel(false);

            UpdateOrdersOverviewPanel(true);
            UpdateOrdersTablePanel(true);

            tabControl1.TabPages.Add(pAddBtn);
            tabControl1.PerformLayout();
            RefreshAddChartButton();
        }

        internal void UpdateEnvironments()
        {
            environmentCombo.Items.Clear();
            environmentCombo.Width = 200;
            updateEnvironmentMenuItem.DropDownItems.Clear();
            Label cwlab = new Label();
            int maxw = 0;
            foreach (string envId in EnvironmentRuntimeRoot.Singleton.Environments)
            {
                var env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envId);
                environmentCombo.Items.Add(env.EnvironmentId);
                cwlab.Text = env.EnvironmentId;
                maxw = Math.Max(maxw, cwlab.PreferredWidth);
                ToolStripItem mi = updateEnvironmentMenuItem.DropDownItems.Add(env.EnvironmentId);
                mi.Click +=
                    delegate(object sender2, EventArgs e2)
                    {
                        StartEnvironmentAssistant(env);
                    };
            }
            environmentCombo.Width = maxw + 30;
        }

        internal void UpdateOrders()
        {
            if (ordersTablePanel != null)
            {
                ordersTablePanel.UpdateOrders();
            }
            if (ordersOverviewPanel != null)
            {
                ordersOverviewPanel.UpdateOrders();
            }
        }

        internal ChartGroupPanel AddChartGroup(ChartGroupRuntime chartGroupRuntime = null, bool createdByUser = true, bool addToEnd = false)
        {
            if (chartGroupRuntime == null)
            {
                chartGroupRuntime = new ChartGroupRuntime(Environment);
                chartGroupRuntime.Session.CursorPosition = 1000;
            }
            ChartGroupPanel p = new ChartGroupPanel();
            p.Init(this, Environment, chartGroupRuntime);
            foreach (var r in chartGroupRuntime.Charts)
            {
                p.AddChart(r, createdByUser);
            }

            if (createdByUser)
            {
                Environment.AddChart(p.ChartGroupRuntime);
                SaveSession();
            }

            AddTabPanel(p, addToEnd);

            return p;
        }

        internal void RemoveChartGroup(ChartGroupPanel p)
        {
            if (RemoveTabPanel(p))
            {
                Environment.RemoveChart(p.ChartGroupRuntime);
                SaveSession();
            }
        }

        internal void AddTabPanel(IForm1TabPanel tabPanel, bool addToEnd = false)
        {
            SuspendLayout();
            TWinFct.LockControlUpdate(this);

            try
            {
                tabPanel.DockIt();

                string txt = tabPanel.TabPage.Text;
                tabPanel.TabPage.Text = "                                          ";
                if (addToEnd)
                {
                    tabControl1.TabPages.Add(tabPanel.TabPage);
                }
                else
                {
                    int tabIndex = tabControl1.TabCount - 1;
                    tabControl1.TabPages.Insert(tabIndex, tabPanel.TabPage);
                    tabControl1.SelectedIndex = tabIndex;
                }
                tabControl1.PerformLayout();

                try
                {
                    tabPanel.TabPage.Text = txt;
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                tabPanel.DockIt();

                if (!addToEnd)
                {
                    RefreshAddChartButton();
                }
            }
            finally
            {
                TWinFct.UnLockControlUpdate(this);
                ResumeLayout();
            }
        }

        internal bool RemoveTabPanel(IForm1TabPanel tabPanel)
        {
            SuspendLayout();
            TWinFct.LockControlUpdate(this);

            try
            {

                int ind = tabControl1.SelectedIndex;
                tabControl1.TabPages.Remove(tabPanel.TabPage);
                tabControl1.PerformLayout();

                RefreshAddChartButton();

                if (tabControl1.TabCount >= 2 && ind == tabControl1.TabCount - 1)
                {
                    tabControl1.SelectedIndex = ind - 1;
                }
                else
                {
                    tabControl1.SelectedIndex = ind;
                }

                UpdateListInLauncherPanel();
                return true;
            }
            finally
            {
                TWinFct.UnLockControlUpdate(this);
                ResumeLayout();
            }
        }

        private void AddChart()
        {
            ChartGroupPanel p = AddChartGroup();
            p.AddChart();
        }

        internal void UpdateListInLauncherPanel(bool visibleOnly = true)
        {
            if (Environment == null)
            {
                return;
            }
            if (!visibleOnly || launcherTabControl.SelectedIndex == 0)
            {
                eaTesterPanel1.UpdateEAsInTest();
            }

            if (!visibleOnly || launcherTabControl.SelectedIndex == 1)
            {
                scriptRunnerPanel1.UpdateScriptsInTest();
            }
        }

        internal void UpdateOrdersOverviewPanel(bool addToEnd = false)
        {
            if (Environment != null)
            {
                bool v = Environment.Session.IsOrdersOverviewVisible;
                ordersOverviewToolStripMenuItem.Checked = v;
                if (v)
                {
                    if (ordersOverviewPanel == null)
                    {
                        // NOTE good:ordersTablePanel
                        if (ordersTablePanel == null)
                        {
                            Environment.LoadOrders();
                        }
                        ordersOverviewPanel = new OrdersOverviewPanel();
                        ordersOverviewPanel.Init(this, Environment);
                        AddTabPanel(ordersOverviewPanel, addToEnd);
                    }
                }
                else
                {
                    if (ordersOverviewPanel != null)
                    {
                        RemoveTabPanel(ordersOverviewPanel);
                        ordersOverviewPanel = null;
                    }
                }
            }
        }

        internal void UpdateOrdersTablePanel(bool addToEnd = false)
        {
            if (Environment != null)
            {
                bool v = Environment.Session.IsOrdersTableVisible;
                ordersTableToolStripMenuItem.Checked = v;
                if (v)
                {
                    if (ordersTablePanel == null)
                    {
                        // NOTE good:ordersOverviewPanel
                        if (ordersOverviewPanel == null)
                        {
                            Environment.LoadOrders();
                        }
                        ordersTablePanel = new OrdersTablePanel();
                        ordersTablePanel.Init(this, Environment);
                        AddTabPanel(ordersTablePanel, addToEnd);
                    }
                }
                else
                {
                    if (ordersTablePanel != null)
                    {
                        RemoveTabPanel(ordersTablePanel);
                        ordersTablePanel = null;
                    }
                }
            }
        }

        void RefreshAddChartButton()
        {
            if (tabControl1.TabCount > 1)
            {
                tabControl1.TabPages[tabControl1.TabCount - 1].Text = "Add chart";
                tabControl1.TabPages[tabControl1.TabCount - 1].ImageIndex = 0;
            }
            else
            {
                tabControl1.TabPages[tabControl1.TabCount - 1].Text = "";
                tabControl1.TabPages[tabControl1.TabCount - 1].ImageIndex = -1;
            }
        }

        internal void ChartIsInTestCheckedInPanel(ChartGroupPanel chp, bool isSelected)
        {
            ChartRuntime ch = chp.ChartGroupRuntime.MasterChart;

            ch.Session.AppearsInTest = isSelected;
            SaveSession();

            chp.UpdateEAInTest();
            UpdateListInLauncherPanel();
        }

        internal void ChartSelectedInPanel(ChartGroupPanel chp)
        {
            tabControl1.SelectedTab = chp.TabPage;
        }

        internal void AddExpertClickedInPanel()
        {
            if (Environment != null)
            {
                AddExpertDialog d = new AddExpertDialog(this, Environment);
                d.ShowDialog(this);
                if (d.DialogResult == DialogResult.OK)
                {
                    ChartGroupPanel p = AddChartGroup();
                    ChartPanel chartPanel = p.AddChart();
                    ChartRuntime chart = chartPanel.ChartRuntime;
                    chart.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                    chart.Update(d.Symbol, d.Period);
                    chart.Expert = ExpertRuntime.Create(chart, d.Expert, chart.GuiSeriesManager.DefaultCache);
                    chart.Session.AppearsInTest = true;
                    chartPanel.UpdateAll();
                    p.UpdateEAScript();
                }
            }
        }

        internal void AddScriptClickedInPanel()
        {
            if (Environment != null)
            {
                AddScriptDialog d = new AddScriptDialog(this, Environment);
                d.ShowDialog(this);
                if (d.DialogResult == DialogResult.OK)
                {
                    ChartGroupPanel p = AddChartGroup();
                    ChartPanel chartPanel = p.AddChart();
                    ChartRuntime chart = chartPanel.ChartRuntime;
                    chart.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                    chart.Update(d.Symbol, d.Period);
                    chart.Script = ScriptRuntime.Create(chart, d.Script, chart.GuiSeriesManager.DefaultCache);
                    chartPanel.UpdateAll();
                    p.UpdateEAScript();
                }
            }
        }

        void LoadScreen()
        {
            Point p = Environment.Session.WindowSplitPoint;
            splitContainer1.SplitterDistance = p.X;
            splitContainer2.SplitterDistance = p.Y;
            bool n = Environment.Session.IsNavigatorVisible;
            splitContainer1.Panel1Collapsed = !n;
            navigatorToolStripMenuItem.Checked = n;
        }

        void updateEnvironmentSplitPoint()
        {
            if (Environment != null)
            {
                Environment.Session.WindowSplitPoint = new Point(splitContainer1.SplitterDistance, splitContainer2.SplitterDistance);
            }
        }

        internal void ShowEnvironmentDialog()
        {
            EnvironmentDialog d = new EnvironmentDialog(Environment);

            d.ShowDialog(this);
        }

        internal void StartEnvironmentAssistant(EnvironmentRuntime updatedEnv)
        {
            EnvironmentAssistantForm form;
            if (updatedEnv == null)
            {
                form = new EnvironmentAssistantForm(null, null, null, null, null, EnvironmentRuntimeRoot.Singleton.Environments);
            }
            else
            {
                string et;
                switch (updatedEnv.EnvironmentType)
                {
                    case EnvironmentType.METATRADER4_OFFLINE:
                        et = "Metatrader 4";
                        break;
                    case EnvironmentType.METATRADER4_ONLINE:
                        et = "Metatrader 4";
                        break;
                    case EnvironmentType.METATRADER5_OFFLINE:
                        et = "Metatrader 5";
                        break;
                    case EnvironmentType.DUKASCOPY_TICKDATA_OFFLINE:
                        et = "Dukascopy";
                        break;
                    default:
                        throw new NotSupportedException();
                }
                form = new EnvironmentAssistantForm(
                    updatedEnv.EnvironmentId,
                    updatedEnv.ImportedFromDirectory,
                    et,
                    updatedEnv.HistoryDirectory,
                    new string[] { updatedEnv.AccountServer, updatedEnv.AccountCompany, updatedEnv.AccountName, "" + updatedEnv.AccountNumber, updatedEnv.AccountCurrency, "" + updatedEnv.AccountLeverage },
                    EnvironmentRuntimeRoot.Singleton.Environments);
            }
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                if (form.Pages[1].SelectedNextPage is ImportMetatraderPage1)
                {
                    ImportMetatraderPage2 imp = (ImportMetatraderPage2)form.Pages[3];

                    EnvironmentRuntime env = EnvironmentRuntimeRoot.Singleton.AddEnvironment(
                        imp.EnvironmentName, imp.ImportedMetatarderVersion,
                        imp.ImportedMetatarderDir, imp.HistoryDirectory, imp.ImportedParameters
                        );


                    for (int i = 9; i < imp.ImportedParameters.Length; i++)
                    {
                        string[] symbolRow = imp.ImportedParameters[i].Split(',');
                        env.AddSymbol(symbolRow);
                    }

                    env.Save();
                    MessageBox.Show("Application should restart, because you changed startup options.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
                else if (form.Pages[1].SelectedNextPage is DukascopyPage0)
                {
                    DukascopyPage0 imp = (DukascopyPage0)form.Pages[4];

                    string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                    string envDir = eaRootDir + "\\" + imp.EnvironmentName;

                    EnvironmentRuntime old_env;
                    try
                    {
                        old_env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(imp.EnvironmentName);
                    }
                    catch (KeyNotFoundException)
                    {
                        old_env = null;
                    }

                    EnvironmentRuntime env = EnvironmentRuntimeRoot.Singleton.AddEnvironment(
                        imp.EnvironmentName, "" + EnvironmentType.DUKASCOPY_TICKDATA_OFFLINE,
                        envDir, "history", imp.ImportedParameters
                        );

                    foreach (var s in imp.SelectedSymbols)
                    {
                        //page1.ImportedParameters[i] = point + "," + digits;
                        string[] symbolRow = new string[24];
                        for (int i = 0; i < 24; i++)
                        {
                            symbolRow[i] = "0";
                        }
                        symbolRow[0] = s;
                        symbolRow[5] = "100000";
                        symbolRow[12] = "1";
                        symbolRow[13] = "0.01";
                        symbolRow[14] = "0.01";

                        // TODO Digits
                        if (s.StartsWith("USDJPY"))
                        {
                            symbolRow[1] = "0.001";
                            symbolRow[2] = "3";
                        }
                        else
                        {
                            symbolRow[1] = "0.00001";
                            symbolRow[2] = "5";
                        }
                        env.AddSymbol(symbolRow);
                    }
                    if (old_env != null)
                    {
                        try
                        {
                            env.Session = old_env.Session;
                        }
                        catch (Exception)
                        {
                        }
                    }


                    env.Save();
                    MessageBox.Show("Application should restart, because you changed startup options.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                }
            }
        }

        // ---------------------


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void navigatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !navigatorToolStripMenuItem.Checked;
            Environment.Session.IsNavigatorVisible = navigatorToolStripMenuItem.Checked;
            SaveSession();
        }

        private void ordersOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Environment != null)
            {
                Environment.Session.IsOrdersOverviewVisible = ordersOverviewToolStripMenuItem.Checked;
                UpdateOrdersOverviewPanel();
                SaveSession();
            }
        }

        private void ordersTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Environment != null)
            {
                Environment.Session.IsOrdersTableVisible = ordersTableToolStripMenuItem.Checked;
                UpdateOrdersTablePanel();
                SaveSession();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddChart();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Initialized)
            {
                if (tabControl1.TabCount >= 2 && tabControl1.SelectedIndex == tabControl1.TabCount - 1)
                {
                    AddChart();
                }
            }
        }

        private void editEnvorinmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEnvironmentDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ShowEnvironmentDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnvironmentDialog d = new EnvironmentDialog(null);
            d.ShowDialog(this);
        }

        private void newEnvironmentAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartEnvironmentAssistant(null);
        }

        private void environmentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnvironment();
        }

        private void launcherTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListInLauncherPanel();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Environment != null)
            {
                StartEnvironmentAssistant(Environment);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            StartEnvironmentAssistant(null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sessionSave && Environment != null && Environment.Session != null)
            {
                Environment.Session.Save();
                sessionSave = false;
            }
        }

        private void splitContainer2_Layout(object sender, LayoutEventArgs e)
        {
            if (splitContainer2.SplitterDistance < splitContainer2.Height - 280)
            {
                splitContainer2.SplitterDistance = splitContainer2.Height - 280;
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer2.SplitterDistance < splitContainer2.Height - 280)
            {
                splitContainer2.SplitterDistance = splitContainer2.Height - 280;
            }
            updateEnvironmentSplitPoint();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            updateEnvironmentSplitPoint();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            sessionSave = false;
            if (Environment != null)
            {
                Environment.Session.Save();
            }
        }


    }
}
