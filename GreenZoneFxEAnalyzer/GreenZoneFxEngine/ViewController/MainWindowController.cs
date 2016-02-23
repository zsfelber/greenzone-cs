using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;

using GreenZoneUtil.ViewController;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;
using GreenZoneFxEngine.ViewController.Assistant;
using System.Collections;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class MainWindowController : MainWindowControllerBase
    {
        public event PropertyChangedEventHandler EnvironmentChanged;
        public event PropertyChangedEventHandler OrdersTableChanged;
        public event PropertyChangedEventHandler OrdersOverviewChanged;
        public event PropertyChangedEventHandler AvailableEnvironmentsChanged;
        public event PropertyChangedEventHandler UpdateEnvironmentMenuItemsChanged;

        bool initialized = false;
        private bool sessionSave = false;
        System.Timers.Timer timer1;

        readonly List<IMainWinTabPageController> tabPages = new List<IMainWinTabPageController>();

        public MainWindowController(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
            TabPages = tabPages.AsReadOnly();
            EaTester = new EATesterController(rmiManager, this);
            ScriptRunner = new ScriptRunnerController(rmiManager, this);
            NavigatorController = new NavigatorController(rmiManager, this);
            TabControl1 = new TabController(rmiManager, this);
            LauncherTabControl = new TabController(rmiManager, this, (TabPageController)EaTester, (TabPageController)ScriptRunner);
            SplitContainer2 = new SplitController(rmiManager, this, TabControl1, LauncherTabControl);
            SplitContainer1 = new SplitController(rmiManager, this, (Controller)NavigatorController, SplitContainer2);
            EnvironmentCombo = new ComboController(rmiManager, this, false);
            ToolStripButton1 = new ButtonController(rmiManager, this);
            ToolStripButton2 = new ButtonController(rmiManager, this);
            ToolStripButton3 = new ButtonController(rmiManager, this);
            OrdersOverviewToolStripMenuItem = new ToggleButtonController(rmiManager, this);
            OrdersTableToolStripMenuItem = new ToggleButtonController(rmiManager, this);
            NavigatorToolStripMenuItem = new ToggleButtonController(rmiManager, this);
            EditEnvorinmentsToolStripMenuItem = new ToggleButtonController(rmiManager, this);
            OptionsToolStripMenuItem = new ToggleButtonController(rmiManager, this);
            NewEnvironmentToolStripMenuItem = new ToggleButtonController(rmiManager, this);
            UpdateEnvironmentMenuItem = new ComboController(rmiManager, this, false);
            TimeLabel = new LabelledController(rmiManager, this);
            OpenLabel = new LabelledController(rmiManager, this);
            LowLabel = new LabelledController(rmiManager, this);
            HighLabel = new LabelledController(rmiManager, this);
            CloseLabel = new LabelledController(rmiManager, this);
            ValueLabel = new LabelledController(rmiManager, this);
            StatusLabel = new LabelledController(rmiManager, this);
            ToolStripStatusLabel2 = new LabelledController(rmiManager, this);
            OLabel = new LabelledController(rmiManager, this);
            LLabel = new LabelledController(rmiManager, this);
            HLabel = new LabelledController(rmiManager, this);
            CLabel = new LabelledController(rmiManager, this);
            VLabel = new LabelledController(rmiManager, this);


            UpdateEnvironments();

            string envId = EAnalyzerOptions.Singleton.DefaultEnvironment;
            if (!string.IsNullOrEmpty(envId))
            {
                var env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envId);
                Environment = env;
                LoadScreen();
            }
            else
            {
                UpdateEnvironment();
            }
            this.timer1 = new System.Timers.Timer();
            this.timer1.AutoReset = true;
            this.timer1.Interval = 10000;
            this.timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            this.timer1.Start();

            NavigatorToolStripMenuItem.Pressed += new ControllerEventHandler(navigatorToolStripMenuItem_Click);
            OrdersOverviewToolStripMenuItem.Pressed += new ControllerEventHandler(ordersOverviewToolStripMenuItem_Click);
            TabControl1.AddTabClicked += new ControllerEventHandler(tabControl1_AddTabClicked);
            EditEnvorinmentsToolStripMenuItem.Pressed += new ControllerEventHandler(editEnvorinmentsToolStripMenuItem_Click);
            ToolStripButton1.Pressed += new ControllerEventHandler(toolStripButton1_Click);
            OptionsToolStripMenuItem.Pressed += new ControllerEventHandler(optionsToolStripMenuItem_Click);
            NewEnvironmentToolStripMenuItem.Pressed += new ControllerEventHandler(newEnvironmentToolStripMenuItem_Click);
            EnvironmentCombo.SelectedIndexChanged += new PropertyChangedEventHandler(environmentCombo_SelectedIndexChanged);
            LauncherTabControl.SelectedIndexChanged += new PropertyChangedEventHandler(launcherTabControl_SelectedIndexChanged);
            ToolStripButton2.Pressed += new ControllerEventHandler(toolStripButton2_Click);
            ToolStripButton3.Pressed += new ControllerEventHandler(toolStripButton3_Click);
            SplitContainer2.SplitterDistanceChanged += new PropertyChangedEventHandler(splitContainer2_SplitterMoved);
            SplitContainer1.SplitterDistanceChanged += new PropertyChangedEventHandler(splitContainer1_SplitterMoved);
            FormClosed += new ControllerEventHandler(MainWindowController_FormClosed);

            
            initialized = true;
        }


        ServerEnvironmentRuntime environment;
        public ServerEnvironmentRuntime Environment
        {
            get
            {
                return environment;
            }
            set
            {
                environment = value;
                EnvironmentCombo.SelectedItem = environment;
                if (EnvironmentChanged != null)
                {
                    EnvironmentChanged(this, new PropertyChangedEventArgs("Environment", value));
                }
            }
        }

        List<ServerEnvironmentRuntime> availableEnvironments;
        IList<ServerEnvironmentRuntime> availableEnvironmentsUm;
        public IList<ServerEnvironmentRuntime> AvailableEnvironments
        {
            get
            {
                return availableEnvironmentsUm;
            }
            private set
            {
                availableEnvironments = (List<ServerEnvironmentRuntime>)value;
                availableEnvironmentsUm = availableEnvironments.AsReadOnly();
                EnvironmentCombo.Items = availableEnvironments;
                if (AvailableEnvironmentsChanged != null)
                {
                    AvailableEnvironmentsChanged(this, new PropertyChangedEventArgs("AvailableEnvironments", value));
                }
            }
        }

        List<EnvironmentMenuItemController> updateEnvironmentMenuItems;
        IList<EnvironmentMenuItemController> updateEnvironmentMenuItemsUm;
        public IList<EnvironmentMenuItemController> UpdateEnvironmentMenuItems
        {
            get
            {
                return updateEnvironmentMenuItemsUm;
            }
            private set
            {
                updateEnvironmentMenuItems = (List<EnvironmentMenuItemController>)value;
                updateEnvironmentMenuItemsUm = updateEnvironmentMenuItems.AsReadOnly();
                UpdateEnvironmentMenuItem.Items = updateEnvironmentMenuItems;
                if (UpdateEnvironmentMenuItemsChanged != null)
                {
                    UpdateEnvironmentMenuItemsChanged(this, new PropertyChangedEventArgs("UpdateEnvironmentMenuItems", value));
                }
            }
        }

        public new OrdersTableController OrdersTable
        {
            get
            {
                return (OrdersTableController)base.OrdersTable;
            }
            protected set
            {
                base.OrdersTable = value;
            }
        }

        public new OrdersOverviewController OrdersOverview
        {
            get
            {
                return (OrdersOverviewController)base.OrdersOverview;
            }
            protected set
            {
                base.OrdersOverview = value;
            }
        }

        public new EATesterController EaTester
        {
            get
            {
                return (EATesterController)base.EaTester;
            }
            protected set
            {
                base.EaTester = value;
            }
        }


        internal void UpdateEnvironment()
        {
            if (Environment == null)
            {
                SplitContainer1.Enabled = false;
                ToolStripButton2.Enabled = false;
                EAnalyzerOptions.Singleton.DefaultEnvironment = null;
            }
            else
            {
                SplitContainer1.Enabled = true;
                ToolStripButton2.Enabled = true;
                EAnalyzerOptions.Singleton.DefaultEnvironment = Environment.EnvironmentId;

                LoadScreen();
            }
            EAnalyzerOptions.Singleton.Save();

            TabPageController pAddBtn = TabControl1.Pages[0];
            TabControl1.Clear();
            OrdersTable = null;
            OrdersOverview = null;

            if (Environment != null)
            {
                foreach (var r in Environment.Charts)
                {
                    AddChartGroup(r, false);
                }
            }

            UpdateListInLauncherPanel(false);

            UpdateOrdersOverviewController();
            UpdateOrdersTableController();
        }

        void UpdateEnvironments()
        {
            List<ServerEnvironmentRuntime> environments = new List<ServerEnvironmentRuntime>(EnvironmentRuntimeRoot.Singleton.environments.Values);
            List<EnvironmentMenuItemController> menuItems = new List<EnvironmentMenuItemController>();

            foreach (ServerEnvironmentRuntime env in environments)
            {
                EnvironmentMenuItemController menuItem = new EnvironmentMenuItemController(rmiManager, this, env);
                menuItems.Add(menuItem);
            }
            AvailableEnvironments = environments;
            UpdateEnvironmentMenuItems = menuItems;
        }

        internal void UpdateOrders()
        {
            if (OrdersTable != null)
            {
                OrdersTable.UpdateOrders();
            }
            if (OrdersOverview != null)
            {
                OrdersOverview.UpdateOrders();
            }
        }

        internal ChartGroupController AddChartGroup(ServerChartGroupRuntime chartGroupRuntime = null, bool createdByUser = true)
        {
            if (chartGroupRuntime == null)
            {
                chartGroupRuntime = new ServerChartGroupRuntime(Environment);
                chartGroupRuntime.Session.CursorPosition = 1000;
            }
            ChartGroupController p = new ChartGroupController(rmiManager, this, Environment, chartGroupRuntime);

            foreach (var r in chartGroupRuntime.Charts)
            {
                p.AddChart(r, createdByUser);
            }

            if (createdByUser)
            {
                Environment.AddChart(p.ChartGroupRuntime);
                SaveSession();
            }

            AddTabPanel(p);

            return p;
        }

        internal void RemoveChartGroup(ChartGroupController p)
        {
            if (RemoveTabPanel(p))
            {
                Environment.RemoveChart(p.ChartGroupRuntime);
                SaveSession();
            }
        }

        internal void AddTabPanel(IMainWinTabPageController tabPanel)
        {
            TabControl1.Add((TabPageController)tabPanel);
        }

        internal bool RemoveTabPanel(IMainWinTabPageController tabPanel)
        {
            int ind = TabControl1.SelectedIndex;
            TabControl1.Remove((TabPageController)tabPanel);

            if (TabControl1.TabCount >= 2 && ind == TabControl1.TabCount - 1)
            {
                TabControl1.SelectedIndex = ind - 1;
            }
            else
            {
                TabControl1.SelectedIndex = ind;
            }

            UpdateListInLauncherPanel();
            return true;
        }

        private void AddChart()
        {
            ChartGroupController p = AddChartGroup();
            p.AddChart();
        }

        internal void UpdateListInLauncherPanel(bool visibleOnly = true)
        {
            if (Environment == null)
            {
                return;
            }
            if (!visibleOnly || LauncherTabControl.SelectedIndex == 0)
            {
                EaTester.UpdateEAsInTest();
            }

            if (!visibleOnly || LauncherTabControl.SelectedIndex == 1)
            {
                //scriptRunner.UpdateScriptsInTest();
            }
        }

        internal void UpdateOrdersOverviewController()
        {
            if (Environment != null)
            {
                bool v = Environment.Session.IsOrdersOverviewVisible;
                OrdersOverviewToolStripMenuItem.Checked = v;
                if (v)
                {
                    if (OrdersOverview == null)
                    {
                        // NOTE good:OrdersTable
                        if (OrdersTable == null)
                        {
                            Environment.LoadOrders();
                        }
                        OrdersOverviewController nwOrdersOverview = new OrdersOverviewController(rmiManager, this, Environment);
                        AddTabPanel(nwOrdersOverview);
                        OrdersOverview = nwOrdersOverview;
                    }
                }
                else
                {
                    if (OrdersOverview != null)
                    {
                        RemoveTabPanel(OrdersOverview);
                        OrdersOverview = null;
                    }
                }
            }
        }

        internal void UpdateOrdersTableController()
        {
            if (Environment != null)
            {
                bool v = Environment.Session.IsOrdersTableVisible;
                OrdersTableToolStripMenuItem.Checked = v;
                if (v)
                {
                    if (OrdersTable == null)
                    {
                        // NOTE good:OrdersOverview
                        if (OrdersOverview == null)
                        {
                            Environment.LoadOrders();
                        }
                        OrdersTableController nwOrdersTable = new OrdersTableController(rmiManager, this, Environment);
                        AddTabPanel(nwOrdersTable);
                        OrdersTable = nwOrdersTable;
                    }
                }
                else
                {
                    if (OrdersTable != null)
                    {
                        RemoveTabPanel(OrdersTable);
                        OrdersTable = null;
                    }
                }
            }
        }

        internal void ChartIsInTestCheckedInPanel(ChartGroupController chp, bool isSelected)
        {
            ServerChartRuntime ch = chp.ChartGroupRuntime.MasterChart;

            ch.Session.AppearsInTest = isSelected;
            SaveSession();

            chp.UpdateEAInTest();
            UpdateListInLauncherPanel();
        }

        internal void ChartSelectedInPanel(ChartGroupController chp)
        {
            TabControl1.SelectedTab = chp;
        }

        internal void AddExpertClickedInPanel()
        {
            if (Environment != null)
            {
                AddExpertController d = new AddExpertController(rmiManager, this, Environment);
                d.ShowDialog(this);
                if (d.DialogResult == DialogResult.OK)
                {
                    ChartGroupController p = AddChartGroup();
                    ChartViewController chartPanel = p.AddChart();
                    ServerChartRuntime chart = chartPanel.ChartRuntime;
                    chart.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                    chart.Update(d.Symbol, d.Period);
                    chart.Expert = ServerExpertRuntime.Create(chart, d.Expert, chart.GuiSeriesManager.DefaultCache);
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
                AddScriptController d = new AddScriptController(rmiManager, this, Environment);
                d.ShowDialog(this);
                if (d.DialogResult == DialogResult.OK)
                {
                    ChartGroupController p = AddChartGroup();
                    ChartViewController chartPanel = p.AddChart();
                    ServerChartRuntime chart = chartPanel.ChartRuntime;
                    chart.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                    chart.Update(d.Symbol, d.Period);
                    chart.Script = ServerScriptRuntime.Create(chart, d.Script, chart.GuiSeriesManager.DefaultCache);
                    chartPanel.UpdateAll();
                    p.UpdateEAScript();
                }
            }
        }

        void LoadScreen()
        {
            Point p = Environment.Session.WindowSplitPoint;
            SplitContainer1.SplitterDistance = p.X;
            SplitContainer2.SplitterDistance = p.Y;
            bool n = Environment.Session.IsNavigatorVisible;
            SplitContainer1.Panel1Collapsed = !n;
            NavigatorToolStripMenuItem.Checked = n;
        }

        void updateEnvironmentSplitPoint()
        {
            if (Environment != null)
            {
                Environment.Session.WindowSplitPoint = new Point(SplitContainer1.SplitterDistance, SplitContainer2.SplitterDistance);
            }
        }

        internal void ShowEnvironmentSettingsController()
        {
            EnvironmentSettingsController d = new EnvironmentSettingsController(rmiManager, Environment);

            d.ShowDialog(this);
        }

        internal void StartEnvironmentAssistant(ServerEnvironmentRuntime updatedEnv)
        {
            EnvironmentAssistantController form;
            if (updatedEnv == null)
            {
                form = new EnvironmentAssistantController(rmiManager, null, null, null, null, null, EnvironmentRuntimeRoot.Singleton.Environments);
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
                form = new EnvironmentAssistantController(
                    rmiManager,
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
                if (form.Pages[1].SelectedNextPage is ImportMetatraderPage1Controller)
                {
                    ImportMetatraderPage2Controller imp = (ImportMetatraderPage2Controller)form.Pages[3];

                    ServerEnvironmentRuntime env = EnvironmentRuntimeRoot.Singleton.AddEnvironment(
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
                else if (form.Pages[1].SelectedNextPage is DukascopyPage0Controller)
                {
                    DukascopyPage0Controller imp = (DukascopyPage0Controller)form.Pages[4];

                    string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                    string envDir = eaRootDir + "\\" + imp.EnvironmentName;

                    ServerEnvironmentRuntime old_env;
                    try
                    {
                        old_env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(imp.EnvironmentName);
                    }
                    catch (KeyNotFoundException)
                    {
                        old_env = null;
                    }

                    ServerEnvironmentRuntime env = EnvironmentRuntimeRoot.Singleton.AddEnvironment(
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

        internal void SaveSession()
        {
            sessionSave = true;
        }

        // ---------------------


        private void openToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {

        }

        private void navigatorToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {
            SplitContainer1.Panel1Collapsed = !NavigatorToolStripMenuItem.Checked;
            Environment.Session.IsNavigatorVisible = NavigatorToolStripMenuItem.Checked;
            SaveSession();
        }

        private void ordersOverviewToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {
            if (Environment != null)
            {
                Environment.Session.IsOrdersOverviewVisible = OrdersOverviewToolStripMenuItem.Checked;
                UpdateOrdersOverviewController();
                SaveSession();
            }
        }

        private void ordersTableToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {
            if (Environment != null)
            {
                Environment.Session.IsOrdersTableVisible = OrdersTableToolStripMenuItem.Checked;
                UpdateOrdersTableController();
                SaveSession();
            }
        }

        private void tabControl1_AddTabClicked(object sender, ControllerEventArgs e)
        {
            AddChart();
        }

        private void editEnvorinmentsToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {
            ShowEnvironmentSettingsController();
        }

        private void toolStripButton1_Click(object sender, ControllerEventArgs e)
        {
            ShowEnvironmentSettingsController();
        }

        private void optionsToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {
            EnvironmentSettingsController d = new EnvironmentSettingsController(rmiManager, null);
            d.ShowDialog(this);
        }

        private void newEnvironmentToolStripMenuItem_Click(object sender, ControllerEventArgs e)
        {
            StartEnvironmentAssistant(null);
        }

        private void environmentCombo_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            UpdateEnvironment();
        }

        private void launcherTabControl_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            UpdateListInLauncherPanel();
        }

        private void toolStripButton2_Click(object sender, ControllerEventArgs e)
        {
            if (Environment != null)
            {
                StartEnvironmentAssistant(Environment);
            }
        }

        private void toolStripButton3_Click(object sender, ControllerEventArgs e)
        {
            StartEnvironmentAssistant(null);
        }

        void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sessionSave && Environment != null && Environment.Session != null)
            {
                Environment.Session.Save();
                sessionSave = false;
            }
        }

        private void splitContainer2_SplitterMoved(object sender, PropertyChangedEventArgs e)
        {
            updateEnvironmentSplitPoint();
        }

        private void splitContainer1_SplitterMoved(object sender, PropertyChangedEventArgs e)
        {
            updateEnvironmentSplitPoint();
        }

        private void MainWindowController_FormClosed(object sender, ControllerEventArgs e)
        {
            sessionSave = false;
            if (Environment != null)
            {
                Environment.Session.Save();
            }
        }



        public class EnvironmentMenuItemController : ButtonController
        {
            internal EnvironmentMenuItemController(GreenRmiManager rmiManager, MainWindowController mainWindow, ServerEnvironmentRuntime environment)
                : base(rmiManager, mainWindow)
            {
                this.mainWindow = mainWindow;
                this.environment = environment;
            }

            MainWindowController mainWindow;
            public MainWindowController MainWindow
            {
                get
                {
                    return mainWindow;
                }
            }

            ServerEnvironmentRuntime environment;
            public ServerEnvironmentRuntime Environment
            {
                get
                {
                    return environment;
                }
            }

            public override void Press()
            {
                base.Press();
                mainWindow.StartEnvironmentAssistant(environment);
            }
        }

    }

    public static class AppResources
    {
        public static int None = (int)AppImage.None;
        public static int empty = (int)AppImage.empty;
        public static int Close_16xLG = (int)AppImage.Close_16xLG;
        public static int action_add_16xMD = (int)AppImage.action_add_16xMD;
        public static int action_Cancel_16xSM = (int)AppImage.action_Cancel_16xSM;
        public static int CursorBar_16xLG = (int)AppImage.CursorBar_16xLG;
        public static int CursorBarB_16xLG = (int)AppImage.CursorBarB_16xLG;
        public static int Warning_yellow_7231_12x11  = (int)AppImage.Warning_yellow_7231_12x11;
        public static int Error_red_12x11 = (int)AppImage.Error_red_12x11;
    }

    public enum AppImage
    {
        None,
        empty,
        Close_16xLG,
        action_add_16xMD,
        action_Cancel_16xSM,
        CursorBar_16xLG,
        CursorBarB_16xLG,
        Warning_yellow_7231_12x11,
        Error_red_12x11
    }
}
