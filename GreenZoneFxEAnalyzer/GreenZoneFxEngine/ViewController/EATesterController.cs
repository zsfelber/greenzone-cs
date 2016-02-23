using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.ViewController.Properties;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;


namespace GreenZoneFxEngine.ViewController
{
    
    public class EATesterController : EATesterControllerBase
    {
        private readonly Dictionary<TestType, ListItem<TestType>> testTypeLis = new Dictionary<TestType, ListItem<TestType>>();
        private readonly Dictionary<TimePeriodConst, ListItem<TimePeriodConst>> periodLis = new Dictionary<TimePeriodConst, ListItem<TimePeriodConst>>();

        bool updatingEa = false;
        bool updatingTableSel = false;

        internal EATesterController(GreenRmiManager rmiManager, MainWindowController mainWindow)
            : base(rmiManager, mainWindow.LauncherTabControl)
        {
            MainWindow = mainWindow;
            Text = "EA Tester";

            DataGridView1 = new GridController(rmiManager, this);
            MethodCombo = new ComboController(rmiManager, this, false);
            DataPeriodCombo = new ComboController(rmiManager, this, false);
            ScrollAcrossTabsCb = new ToggleButtonController(rmiManager, this);
            SkipEmptyPeriodsCb = new ToggleButtonController(rmiManager, this);
            UpdateSpreadTickCb = new ToggleButtonController(rmiManager, this);
            SpeedTrackBar = new ProgressTrackController(rmiManager, this);
            ProgressTrackBar1 = new ProgressTrackController(rmiManager, this);
            StartStopButton = new ButtonController(rmiManager, this);
            PropertiesButton = new ButtonController(rmiManager, this);
            PauseButton = new ButtonController(rmiManager, this);
            AddLinkLabel = new ButtonController(rmiManager, this);
            PauseAtButton = new ButtonController(rmiManager, this);
            SnapButton = new ButtonController(rmiManager, this);
            MethodInfLabel = new LabelledController(rmiManager, this);
            EaInfLabel = new LabelledController(rmiManager, this);
            SymbolInfLabel = new LabelledController(rmiManager, this);
            FromInfLabel = new LabelledController(rmiManager, this);
            ToInfLabel = new LabelledController(rmiManager, this);
            MethodLabel = new LabelledController(rmiManager, this);
            DataPeriodLabel = new LabelledController(rmiManager, this);
            ToolTip1 = new ChildControlMap<string>(rmiManager);

            SelectEAColumn = new GridColumnController(rmiManager, DataGridView1, typeof(bool));
            TableTestEasEACol = new GridColumnController(rmiManager, DataGridView1, typeof(string));
            TableTestEasPeriodCol = new GridColumnController(rmiManager, DataGridView1, typeof(TimePeriodConst));
            IconColumn = new GridColumnController(rmiManager, DataGridView1, typeof(int));

            // 
            // SelectEAColumn
            // 
            this.SelectEAColumn.Text = "";
            this.SelectEAColumn.Name = "SelectEAColumn";
            this.SelectEAColumn.DataPropertyName = "Selected";
            // 
            // tableTestEasEA
            // 
            this.TableTestEasEACol.Text = "EA";
            this.TableTestEasEACol.Name = "tableTestEasEA";
            this.TableTestEasEACol.DataPropertyName = "Title";
            // 
            // tableTestEasPeriod
            // 
            this.TableTestEasPeriodCol.Text = "Period";
            this.TableTestEasPeriodCol.Name = "tableTestEasPeriod";
            this.TableTestEasPeriodCol.DataPropertyName = "Period";
            // 
            // IconColumn
            // 
            this.IconColumn.Text = "";
            this.IconColumn.Name = "IconColumn";
            this.IconColumn.DataPropertyName = "Icon";

            foreach (TestType tt in Enum.GetValues(typeof(TestType)))
            {
                ListItem<TestType> ltt = new ListItem<TestType>(tt, tt.GetShortText());
                testTypeLis[tt] = ltt;
                MethodCombo.Add(ltt);
            }

            this.SelectEAColumn.ContentClicked += new ControllerEventHandler(dataGridView1_CellContentClick);
            DataGridView1.DoubleClicked += new ControllerEventHandler(dataGridView1_DoubleClick);
            AddLinkLabel.Pressed += new ControllerEventHandler(addLinkLabel_LinkClicked);
            PropertiesButton.Pressed += new ControllerEventHandler(propertiesButton_Click);
            DataGridView1.CurrentCellChanged += new PropertyChangedEventHandler(dataGridView1_SelectionChanged);
            MethodCombo.SelectedIndexChanged += new PropertyChangedEventHandler(methodCombo_SelectedIndexChanged);
            ScrollAcrossTabsCb.CheckedChanged += new PropertyChangedEventHandler(scrollAcrossTabsCb_CheckedChanged);
            SkipEmptyPeriodsCb.CheckedChanged += new PropertyChangedEventHandler(skipEmptyPeriodsCb_CheckedChanged);
            UpdateSpreadTickCb.CheckedChanged += new PropertyChangedEventHandler(updateSpreadTickCb_CheckedChanged);
            DataPeriodCombo.SelectedIndexChanged += new PropertyChangedEventHandler(dataPeriodCombo_SelectedIndexChanged);
            StartStopButton.Pressed += new ControllerEventHandler(startStopButton_Click);
            PauseButton.Pressed += new ControllerEventHandler(pauseButton_Click);
            SpeedTrackBar.ValueChanged += new PropertyChangedEventHandler(speedTrackBar_Scroll);
            ProgressTrackBar1.CursorValueChanged += new PropertyChangedEventHandler(progressTrackBar1_CursorValueChanged);
            PauseAtButton.Pressed += new ControllerEventHandler(stopAtButton_Click);
            SnapButton.Pressed += new ControllerEventHandler(snapButton_Click);
        }


        public new MainWindowController MainWindow
        {
            get
            {
                return (MainWindowController)base.MainWindow;
            }
            protected set
            {
                base.MainWindow = value;
            }
        }


        internal void UpdateEAsInTest()
        {
            try
            {
                updatingEa = true;
                int si = -1;
                TableRow row = (TableRow)DataGridView1.CurrentRow;
                if (row != null)
                {
                    si = row.Index;
                }

                DataGridView1.ClearRows();
                bool hasEnabledExpert = false;
                Dictionary<string, List<ChartGroupController>> cgps = new Dictionary<string, List<ChartGroupController>>();
                foreach (IMainWinTabPageController tp in MainWindow.TabPages)
                {
                    if (tp is ChartGroupController)
                    {
                        ChartGroupController chg = (ChartGroupController)tp;
                        // FIXME
                        // not yet initialized
                        if (chg.ChartViews.Count == 0)
                        {
                            return;
                        }
                        ChartViewController chp = (ChartViewController)chg.ChartViews[0];
                        ServerChartRuntime ch = chp.ChartRuntime;
                        if (ch.Expert != null)
                        {
                            if (ch.Session.AppearsInTest)
                            {
                                hasEnabledExpert = true;
                            }
                            string symbol = ch.Symbol.ToString();
                            List<ChartGroupController> ps;
                            if (!cgps.TryGetValue(symbol, out ps))
                            {
                                ps = new List<ChartGroupController>();
                                cgps[symbol] = ps;
                            }
                            ps.Add(chg);
                        }
                    }
                }

                foreach (var e in cgps)
                {
                    ChartViewController chp0 = null;
                    foreach (ChartGroupController chg in e.Value)
                    {
                        chp0 = (ChartViewController)chg.ChartViews[0];
                        break;
                    }
                    TimePeriodConst testingPeriod0 = chp0.ChartRuntime.SymbolRuntime.BestPeriod;
                    TimePeriodConst testingPeriod = chp0.ChartRuntime.SymbolRuntime.Session.DataPeriod;
                    if (testingPeriod == 0 || testingPeriod0.GetOrdinal() < testingPeriod.GetOrdinal())
                    {
                        testingPeriod = testingPeriod0;
                        chp0.ChartRuntime.SymbolRuntime.Session.DataPeriod = testingPeriod;
                    }

                    TableRow hrow = new TableRow(this, chp0.ChartRuntime.SymbolRuntime, true);

                    foreach (ChartGroupController chg in e.Value)
                    {
                        ChartViewController chp = (ChartViewController)chg.ChartViews[0];

                        ServerChartRuntime ch = chp.ChartRuntime;
                        ServerExpertRuntime expert = ch.Expert;
                        new TableRow(this, chp);
                    }
                }


                if (si >= DataGridView1.Rows.Count)
                {
                    si = DataGridView1.Rows.Count - 1;
                }

                if (si > 0)
                {
                    DataGridView1.CurrentCell = DataGridView1[si, 0];
                }

                var envs = MainWindow.Environment.Session;
                ScrollAcrossTabsCb.Checked = envs.ScrollAcrossCharts;
                SkipEmptyPeriodsCb.Checked = envs.SkipEmptyPeriods;
                UpdateSpreadTickCb.Checked = envs.UpdateSpreadTick;
                SpeedTrackBar.Value = envs.EATestingSpeed;
                ProgressTrackBar1.CursorValue = envs.EATestingTrackBarTick;

                if (hasEnabledExpert)
                {
                    StartStopButton.Enabled = true;
                    UpdateSpreadTickCb.Enabled = true;
                    ScrollAcrossTabsCb.Enabled = true;
                    SkipEmptyPeriodsCb.Enabled = true;
                    SpeedTrackBar.Enabled = true;
                }
                else
                {
                    StartStopButton.Enabled = false;
                    UpdateSpreadTickCb.Enabled = false;
                    ScrollAcrossTabsCb.Enabled = false;
                    SkipEmptyPeriodsCb.Enabled = false;
                    SpeedTrackBar.Enabled = false;
                }
            }
            finally
            {
                updatingEa = false;
                // TODO slightly bad
                dataGridView1_SelectionChanged(null, null);
            }
        }

        private void setDataPeriodTooltip(ServerChartRuntime chr, out string tooltip, out int image)
        {
            TimePeriodConst dataPeriod = chr.SymbolRuntime.Session.DataPeriod;
            if (dataPeriod != TimePeriodConst.PERIOD_CURRENT)
            {
                if (chr.Period.GetOrdinal() >= dataPeriod.GetOrdinal())
                {
                    image = AppResources.empty;
                    tooltip = null;
                }
                else if (chr.Period == TimePeriodConst.PERIOD_TICK_ASK && dataPeriod == TimePeriodConst.PERIOD_TICK)
                {
                    image = AppResources.empty;
                    tooltip = null;
                }
                else
                {
                    image = AppResources.Warning_yellow_7231_12x11;
                    tooltip = "Test data modelling period is not fine enough for modelling Chart period";
                }
            }
            else
            {
                image = AppResources.Error_red_12x11;
                tooltip = "There is no timeframe period on file";
            }
        }

        private void dataGridView1_CellContentClick(object sender, ControllerEventArgs e)
        {
            TableRow row = (TableRow)DataGridView1.CurrentRow;
            ChartViewController chp = row.chartView;
            if (chp != null)
            {
                MainWindow.ChartIsInTestCheckedInPanel(chp.ChartGroupController, !row.Selected);
            }
        }

        private void dataGridView1_DoubleClick(object sender, ControllerEventArgs e)
        {
            TableRow row = (TableRow)DataGridView1.CurrentRow;
            if (row != null)
            {
                ChartViewController chp = row.chartView;
                if (chp != null)
                {
                    MainWindow.ChartSelectedInPanel(chp.ChartGroupController);
                }
            }
        }

        private void addLinkLabel_LinkClicked(object sender, ControllerEventArgs e)
        {
            MainWindow.AddExpertClickedInPanel();
        }

        private void propertiesButton_Click(object sender, ControllerEventArgs e)
        {
            TableRow row = (TableRow)DataGridView1.CurrentRow;
            if (row != null)
            {
                ChartViewController chp = row.chartView;
                ServerChartRuntime chr = chp.ChartRuntime;
                if (chr.Expert != null)
                {
                    ExpertDialogController d = new ExpertDialogController(rmiManager, chr.Expert);
                    d.ShowDialog(MainWindow);

                    if (d.DialogResult == DialogResult.OK)
                    {
                        MainWindow.SaveSession();
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, ControllerEventArgs e)
        {
            try
            {
                if (!updatingEa)
                {
                    updatingTableSel = true;

                    TableRow row = (TableRow)DataGridView1.CurrentRow;
                    if (row != null && DataGridView1.RowCount > 0)
                    {
                        ChartViewController chp = row.chartView;
                        ServerChartRuntime chr;
                        TimePeriodConst testingPeriod0 = row.symbolRuntime.BestPeriod;

                        // NOTE just switching, uninitialized
                        if (testingPeriod0 == TimePeriodConst.PERIOD_CURRENT)
                        {
                            return;
                        }
                        TimePeriodConst testingPeriod = row.symbolRuntime.Session.DataPeriod;
                        TimePeriodConst period;
                        symbol symbol = row.symbolRuntime.Symbol;

                        if (chp != null)
                        {
                            chr = chp.ChartRuntime;
                            period = chr.Period;

                            periodLis.Clear();
                            DataPeriodCombo.Clear();

                            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                            {
                                if (ServerTimeSeriesRuntimeEx.IsSeriesAvailable(MainWindow.Environment, symbol, v))
                                {
                                    ListItem<TimePeriodConst> li = new ListItem<TimePeriodConst>(v, v.GetLongTxt());
                                    DataPeriodCombo.Add(li);
                                    periodLis[v] = li;
                                }
                            }

                            TestType tt = chr.SymbolRuntime.Session.TestType;
                            MethodCombo.Items.Remove(testTypeLis[TestType.NOT_IN_TEST]);
                            MethodCombo.SelectedItem = testTypeLis[tt];
                            MethodInfLabel.Text = tt.GetDescription();
                            MethodCombo.Enabled = false;
                            DataPeriodCombo.Enabled = true;

                            EaInfLabel.Text = chr.Expert.ExpertInfo.ShortTypeName;
                            SymbolInfLabel.Text = "" + chr.Symbol;

                            FromInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)chr.Session.From);
                            ToInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)chr.Session.To);

                            string tooltip;
                            int image;
                            setDataPeriodTooltip(chr, out tooltip, out image);
                            if (period != TimePeriodConst.PERIOD_CURRENT)
                            {
                                DataPeriodCombo.SelectedItem = periodLis[period];
                            }
                            else
                            {
                                DataPeriodCombo.SelectedItem = null;
                            }
                            ToolTip1[DataPeriodLabel] = tooltip;
                            DataPeriodLabel.Image = image;

                            PropertiesButton.Enabled = true;
                        }
                        else
                        {
                            period = testingPeriod;

                            periodLis.Clear();
                            DataPeriodCombo.Clear();
                            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                            {
                                if (v != TimePeriodConst.PERIOD_TICK_ASK && v.GetOrdinal() <= testingPeriod0.GetOrdinal() && ServerTimeSeriesRuntimeEx.IsSeriesAvailable(MainWindow.Environment, symbol, v))
                                {
                                    ListItem<TimePeriodConst> li = new ListItem<TimePeriodConst>(v, v.GetLongTxt());
                                    DataPeriodCombo.Add(li);
                                    periodLis[v] = li;
                                }
                            }

                            TestType tt = row.symbolRuntime.Session.TestType;
                            MethodCombo.Items.Remove(testTypeLis[TestType.NOT_IN_TEST]);
                            MethodCombo.SelectedItem = testTypeLis[tt];
                            MethodInfLabel.Text = tt.GetDescription();
                            MethodCombo.Enabled = true;
                            DataPeriodCombo.Enabled = true;

                            EaInfLabel.Text = "";
                            SymbolInfLabel.Text = "" + symbol;
                            FromInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)row.symbolRuntime.Session.EATestingGlobalFrom);
                            ToInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)row.symbolRuntime.Session.EATestingGlobalTo);

                            string tooltip;
                            int image;
                            image = AppResources.empty;
                            tooltip = null;

                            if (period != TimePeriodConst.PERIOD_CURRENT)
                            {
                                DataPeriodCombo.SelectedItem = periodLis[period];
                            }
                            else
                            {
                                DataPeriodCombo.SelectedItem = null;
                            }
                            ToolTip1[DataPeriodLabel] = tooltip;
                            DataPeriodLabel.Image = image;

                            PropertiesButton.Enabled = false;
                        }

                    }
                    else
                    {
                        MethodCombo.Enabled = false;
                        DataPeriodCombo.Enabled = false;
                        DataPeriodLabel.Image = AppResources.None;
                        ToolTip1[DataPeriodLabel] = null;
                        EaInfLabel.Text = "";
                        SymbolInfLabel.Text = "";
                        DataPeriodCombo.Text = "";
                        FromInfLabel.Text = "";
                        ToInfLabel.Text = "";
                        if (!MethodCombo.Items.Contains(testTypeLis[TestType.NOT_IN_TEST]))
                        {
                            MethodCombo.Add(testTypeLis[TestType.NOT_IN_TEST]);
                        }
                        MethodCombo.SelectedItem = testTypeLis[TestType.NOT_IN_TEST];
                        PropertiesButton.Enabled = false;
                    }
                }
            }
            finally
            {
                updatingTableSel = false;
            }
        }

        private void methodCombo_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            if (!updatingTableSel)
            {
                TableRow row = (TableRow)DataGridView1.CurrentRow;
                if (row != null && DataGridView1.RowCount > 0)
                {
                    ChartViewController chp = row.chartView;

                    if (MethodCombo.SelectedItem is ListItem<TestType>)
                    {
                        ListItem<TestType> ltt = (ListItem<TestType>)MethodCombo.SelectedItem;
                        if (ltt != null)
                        {
                            row.symbolRuntime.Session.TestType = ltt.Value;
                            MethodInfLabel.Text = ltt.Value.GetDescription();

                            ServerSymbolRuntime sr = row.symbolRuntime;
                            if (row.symbolRuntime.Session.DataPeriod == TimePeriodConst.PERIOD_CURRENT)
                            {
                                MethodLabel.Image = AppResources.Error_red_12x11;
                                ToolTip1[MethodLabel] = "There is no timeframe period on file";
                            }
                            else if (!ServerTimeSeriesRuntimeEx.IsSeriesAvailable(MainWindow.Environment, sr.Symbol, sr.Session.DataPeriod))
                            {
                                MethodLabel.Image = AppResources.Warning_yellow_7231_12x11;
                                ToolTip1[MethodLabel] = "Selected chart period is not on file (" + sr.Session.DataPeriod.GetShortTxt() + ")";
                            }
                            else
                            {
                                MethodLabel.Image = AppResources.None;
                                ToolTip1[MethodLabel] = null;
                            }
                            MainWindow.SaveSession();

                            UpdateEAsInTest();
                        }
                    }
                }
            }
        }

        private void scrollAcrossTabsCb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            MainWindow.Environment.Session.ScrollAcrossCharts = ScrollAcrossTabsCb.Checked;
            MainWindow.SaveSession();
        }

        private void skipEmptyPeriodsCb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            MainWindow.Environment.Session.SkipEmptyPeriods = SkipEmptyPeriodsCb.Checked;
            MainWindow.SaveSession();
        }

        private void updateSpreadTickCb_CheckedChanged(object sender, ControllerEventArgs e)
        {
            MainWindow.Environment.Session.UpdateSpreadTick = UpdateSpreadTickCb.Checked;
            MainWindow.SaveSession();
        }

        private void dataPeriodCombo_SelectedIndexChanged(object sender, ControllerEventArgs e)
        {
            if (!updatingTableSel)
            {
                TableRow row = (TableRow)DataGridView1.CurrentRow;
                if (row != null)
                {
                    ChartViewController chp = row.chartView;
                    if (DataPeriodCombo.SelectedItem is ListItem<TimePeriodConst>)
                    {
                        ListItem<TimePeriodConst> dpt = (ListItem<TimePeriodConst>)DataPeriodCombo.SelectedItem;
                        if (dpt != null)
                        {
                            if (chp != null)
                            {
                                chp.ChartRuntime.Period = dpt.Value;
                                row.symbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                                chp.UpdatePeriods();
                            }
                            else
                            {
                                row.symbolRuntime.Session.DataPeriod = dpt.Value;
                                MainWindow.SaveSession();

                                // TODO slightly bad
                                UpdateEAsInTest();
                            }
                        }
                    }
                }
            }
        }

        private void startStopButton_Click(object sender, ControllerEventArgs e)
        {
            if (MainWindow.Environment.Session.EAStartStatus.IsRunning())
            {
                StartStopButton.Text = "&Start";
                MainWindow.Environment.TestManager.AddRequest(EaTestRequest.STOP_EA_TEST);
                PauseButton.Enabled = false;
                ProgressTrackBar1.Enabled = false;
            }
            else
            {
                StartStopButton.Text = "&Stop";
                MainWindow.Environment.TestManager.AddRequest(EaTestRequest.START_EA_TEST);
                PauseButton.Enabled = true;
                ProgressTrackBar1.Enabled = true;
            }
        }

        private void pauseButton_Click(object sender, ControllerEventArgs e)
        {
            if (MainWindow.Environment.Session.EAStartStatus.IsRunning())
            {
                MainWindow.Environment.TestManager.AddRequest(EaTestRequest.PAUSE_EA_TEST);
                if (MainWindow.Environment.Session.EAStartStatus == StartStatus.PAUSED)
                {
                    PauseButton.Text = "&Continue";
                }
                else
                {
                    PauseButton.Text = "&Pause";
                }
            }
        }

        private void speedTrackBar_Scroll(object sender, ControllerEventArgs e)
        {
            MainWindow.Environment.Session.EATestingSpeed = SpeedTrackBar.Value;
        }

        private void progressTrackBar1_CursorValueChanged(object sender, PropertyChangedEventArgs e)
        {
            MainWindow.Environment.Session.EATestingTrackBarTick = ProgressTrackBar1.CursorValue;
        }

        private void stopAtButton_Click(object sender, ControllerEventArgs e)
        {
            // TODO
        }

        private void snapButton_Click(object sender, ControllerEventArgs e)
        {
            // TODO
        }


        public class TableRow : GridRowController
        {
            EATesterController _;
            public readonly ChartViewController chartView;
            public readonly ServerChartRuntime chartRuntime;
            public readonly ServerSymbolRuntime symbolRuntime;
            public readonly ServerExpertRuntime expert;
            public readonly bool isLast;
            public readonly string tooltip;
            public readonly int image;


            internal TableRow(EATesterController _, ChartViewController chartView)
                : base(_.rmiManager, _.DataGridView1)
            {
                this._ = _;
                this.chartView = chartView;
                this.chartRuntime = chartView.ChartRuntime;
                this.symbolRuntime = this.chartRuntime.SymbolRuntime;
                this.expert = this.chartRuntime.Expert;

                _.setDataPeriodTooltip(chartRuntime, out tooltip, out image);
                DataBoundObject = this;
            }
            internal TableRow(EATesterController _, ServerSymbolRuntime symbolRuntime, bool isLast=false)
                : base(_.rmiManager, _.DataGridView1)
            {
                this._ = _;
                this.symbolRuntime = symbolRuntime;

                image = AppResources.empty;
                DataBoundObject = this;
                this.isLast = isLast;
            }

            public ChartViewController ChartView
            {
                get
                {
                    return chartView;
                }
            }

            public bool IsLast
            {
                get
                {
                    return isLast;
                }
            }

            //int i0 = DataGridView1.Rows.Add(false, e.Key + " : " + chp0.ChartRuntime.SymbolRuntime.Session.TestType.GetShortText(), "", AppResources.empty);
            //int i = DataGridView1.Rows.Add(expert.Parent.Session.AppearsInTest, expert.ExpertInfo.FullName, period.GetShortTxt(), null);
            public bool Selected
            {
                get
                {
                    if (chartView == null || expert == null)
                    {
                        return false;
                    }
                    else
                    {
                        return expert.Parent.Session.AppearsInTest;
                    }
                }
            }
            public string Title
            {
                get
                {
                    if (chartView == null)
                    {
                        return symbolRuntime.Symbol + " : " + symbolRuntime.Session.TestType.GetShortText();
                    }
                    else if (expert != null)
                    {
                        return expert.ExpertInfo.FullName;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            public string Period
            {
                get
                {
                    if (chartRuntime != null)
                    {
                        return chartRuntime.Period.GetShortTxt();
                    }
                    else
                    {
                        return symbolRuntime.Session.DataPeriod.GetShortTxt();
                    }
                }
            }
            public int Icon
            {
                get
                {
                    return image;
                }
            }
        }
    }
}
