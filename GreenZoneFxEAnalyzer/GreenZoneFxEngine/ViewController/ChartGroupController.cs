using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;

using GreenZoneFxEngine.ViewController.Properties;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class ChartGroupController : ChartGroupControllerBase
    {
        bool initialized;

        private readonly List<IChartViewController> chartViews = new List<IChartViewController>();

        internal ChartGroupController(GreenRmiManager rmiManager, MainWindowController mainWindow, ServerEnvironmentRuntime environment, ServerChartGroupRuntime chartGroupRuntime) :
            base(rmiManager, (TabController)null)
        {
            this.environment = environment;
            MainWindow = mainWindow;
            this.chartGroupRuntime = chartGroupRuntime;
            ChartViews = chartViews.AsReadOnly();
            TableLayoutPanel1 = new MultiSplitController(rmiManager, this);
            ScriptCombo = new ComboController(rmiManager, this, false);
            EaCombo = new ComboController(rmiManager, this, false);
            OpenEaButton = new ButtonController(rmiManager, this);
            OpenScriptButton = new ButtonController(rmiManager, this);
            InTestButton = new ToggleButtonController(rmiManager, this);
            ToggleBottomBarButton1 = new ButtonController(rmiManager, this);
            ToggleBottomBarButton2 = new ButtonController(rmiManager, this);
            BottomToolStrip = new Controller(rmiManager, this);

            this.chartGroupRuntime = chartGroupRuntime;

            mainWindow.SaveSession();
            UpdateEAInTest();
            UpdateScriptInTest();
            mainWindow.UpdateListInLauncherPanel();

            OpenEaButton.Pressed += new ControllerEventHandler(openEaButton_Click);
            OpenScriptButton.Pressed += new ControllerEventHandler(openScriptButton_Click);
            InTestButton.Pressed += new ControllerEventHandler(inTestButton_Click);
            ToggleBottomBarButton1.Pressed += new ControllerEventHandler(toggleBottomBarButton1_Click);
            ToggleBottomBarButton2.Pressed += new ControllerEventHandler(toggleBottomBarButton2_Click);
        }


        internal ChartViewController AddChart(ServerChartRuntime chartRuntime = null, bool createdByUser = true)
        {
            symbol sym;
            try
            {
                sym = Environment.GetSymbol("EURUSD");
            }
            catch (SymbolNotFoundException)
            {
                sym = Environment.Symbols.ToArray()[0];
            }

            ChartViewController chartView = AddChart(chartRuntime, sym, TimePeriodConst.PERIOD_H1, createdByUser);
            return chartView;
        }

        internal ChartViewController AddChart(ServerChartRuntime chartRuntime, symbol symbol, TimePeriodConst period = TimePeriodConst.PERIOD_H1, bool createdByUser = true)
        {
            if (chartRuntime == null)
            {
                chartRuntime = new ServerChartRuntime(ChartGroupRuntime, ChartGroupRuntime.Charts.Count == 0);

                // ! _Period.set -> LoadSeriesArrays -> memberBarTime
                //chartRuntime.Session.ExecutedBarTime = datetime.MaxValue;
                //chartRuntime.Session.ScrolledBarTime = datetime.MaxValue;

                chartRuntime.Symbol = symbol;

                // NOTE LoadSeriesArrays here
                chartRuntime.Period = period;

                if (!chartRuntime.IsMaster)
                {
                    chartRuntime.Session.TopBarVisible = false;
                }

                chartRuntime.Session.IsCursorBarConnected = true;
                chartRuntime.Session.AutoSeriesRange = true;
                chartRuntime.Session.ChartType = ChartType.OHLC;
                SeriesManagerCache seriesCache = chartRuntime.GuiSeriesManager.DefaultCache;

                int cursorPosition = chartRuntime.ParentCursorPosition;

                SeriesRange r;
                if (seriesCache != null)
                {
                    r = new SeriesRange(0, 250, seriesCache.Open[0] * 0.5, seriesCache.Open[0] * 2);
                }
                else
                {
                    r = new SeriesRange(0, 250, 0, 1000);
                }

                r.CursorPosition = cursorPosition;
                chartRuntime.SeriesRange = r;

            }
            ChartViewController p = new ChartViewController(rmiManager, this, Environment, chartRuntime);
            chartViews.Add(p);
            TableLayoutPanel1.Add(p);

            if (createdByUser)
            {
                ChartGroupRuntime.AddChart(p.ChartRuntime);
                MainWindow.SaveSession();
            }
            UpdateTabText();
            UpdateEAScript();

            return p;
        }

        internal void RemoveChart(ChartViewController p)
        {
            chartViews.Remove(p);
            ChartGroupRuntime.RemoveChart(p.ChartRuntime);

            this.TableLayoutPanel1.Remove(p);

            MainWindow.SaveSession();

            MainWindow.UpdateListInLauncherPanel();
        }

        internal void UpdateTabText()
        {
            if (ChartGroupRuntime.MasterChart == null)
            {
                Text = "";
            }
            else
            {
                Text = ChartGroupRuntime.MasterChart.Symbol + " " + ChartGroupRuntime.MasterChart.Period.GetShortTxt();
            }
        }

        internal void UpdateEAScript()
        {
            initialized = false;
            try
            {
                ScriptCombo.Clear();
                ListItem<Mt4ExecutableInfo> selectedScript = new ListItem<Mt4ExecutableInfo>(null, "-- None --");
                ScriptCombo.Add(selectedScript);
                ScriptCombo.SelectedIndex = 0;
                foreach (string s in Environment.Scripts)
                {
                    Mt4ExecutableInfo script = Environment.GetScriptInfo(s);
                    ListItem<Mt4ExecutableInfo> item = new ListItem<Mt4ExecutableInfo>(script, script.Name);
                    ScriptCombo.Add(item);

                    if (ChartGroupRuntime.MasterChart != null && ChartGroupRuntime.MasterChart.Script != null && ChartGroupRuntime.MasterChart.Script.ScriptInfo.Equals(script))
                    {
                        selectedScript = item;
                    }
                }
                ScriptCombo.SelectedIndexChanged +=
                    delegate(object sender2, PropertyChangedEventArgs e2)
                    {
                        ListItem<Mt4ExecutableInfo> item = (ListItem<Mt4ExecutableInfo>)ScriptCombo.SelectedItem;

                        if (initialized)
                        {
                            if (item.Value != null)
                            {
                                ChartGroupRuntime.MasterChart.Script = ServerScriptRuntime.Create(ChartGroupRuntime.MasterChart, item.Value, chartGroupRuntime.MasterChart.GuiSeriesManager.DefaultCache);
                            }
                            else
                            {
                                ChartGroupRuntime.MasterChart.Script = null;
                            }

                            UpdateScriptInTest();

                            if (initialized)
                            {
                                MainWindow.SaveSession();
                                MainWindow.UpdateListInLauncherPanel();
                            }
                        }
                    };
                ScriptCombo.SelectedItem = selectedScript;


                EaCombo.Clear();
                ListItem<Mt4ExecutableInfo> selectedExpert = new ListItem<Mt4ExecutableInfo>(null, "-- None --");
                EaCombo.Add(selectedExpert);
                EaCombo.SelectedIndex = 0;
                foreach (string s in Environment.Experts)
                {
                    Mt4ExecutableInfo expert = Environment.GetExpertInfo(s);
                    ListItem<Mt4ExecutableInfo> item = new ListItem<Mt4ExecutableInfo>(expert, expert.Name);
                    EaCombo.Add(item);

                    if (ChartGroupRuntime.MasterChart != null && ChartGroupRuntime.MasterChart.Expert != null && ChartGroupRuntime.MasterChart.Expert.ExpertInfo.Equals(expert))
                    {
                        selectedExpert = item;
                    }
                }
                EaCombo.SelectedIndexChanged +=
                    delegate(object sender2, PropertyChangedEventArgs e2)
                    {
                        ListItem<Mt4ExecutableInfo> item = (ListItem<Mt4ExecutableInfo>)EaCombo.SelectedItem;

                        if (initialized)
                        {
                            if (item.Value != null)
                            {
                                ChartGroupRuntime.MasterChart.Expert = ServerExpertRuntime.Create(ChartGroupRuntime.MasterChart, item.Value, chartGroupRuntime.MasterChart.GuiSeriesManager.DefaultCache);
                                ChartGroupRuntime.MasterChart.Period = ChartGroupRuntime.MasterChart.Period;

                                if (initialized)
                                {
                                    ChartGroupRuntime.MasterChart.Session.AppearsInTest = true;
                                }
                            }
                            else
                            {
                                ChartGroupRuntime.MasterChart.Expert = null;
                            }
                        }

                        UpdateEAInTest();

                        if (initialized)
                        {
                            MainWindow.SaveSession();
                            MainWindow.UpdateListInLauncherPanel();
                        }
                    };
                EaCombo.SelectedItem = selectedExpert;

                if (ChartGroupRuntime.MasterChart != null)
                {
                    SetBottomBarVisible(ChartGroupRuntime.MasterChart.Session.BottomBarVisible);
                }
                else
                {
                    SetBottomBarVisible(true);
                }
            }
            finally
            {
                initialized = true;
            }
        }

        internal void UpdateEAInTest()
        {
            if (ChartGroupRuntime.MasterChart == null)
            {
                OpenEaButton.Enabled = false;
                InTestButton.Enabled = false;
            }
            else if (ChartGroupRuntime.MasterChart.Expert == null)
            {
                ChartGroupRuntime.MasterChart.Session.AppearsInTest = false;
                MainWindow.SaveSession();

                OpenEaButton.Enabled = false;
                InTestButton.Enabled = false;
            }
            else
            {
                OpenEaButton.Enabled = true;
                InTestButton.Enabled = true;
            }

            if (ChartGroupRuntime.MasterChart != null && ChartGroupRuntime.MasterChart.Session.AppearsInTest)
            {
                InTestButton.Text = "in test";
                InTestButton.Checked = true;
            }
            else
            {
                InTestButton.Text = "not in test";
                InTestButton.Checked = false;
            }
        }

        internal void UpdateScriptInTest()
        {
            if (ChartGroupRuntime.MasterChart == null || ChartGroupRuntime.MasterChart.Script == null)
            {
                OpenScriptButton.Enabled = false;
            }
            else
            {
                OpenScriptButton.Enabled = true;
            }
        }

        private void SetBottomBarVisible(bool visible, bool save = false)
        {
            if (ChartGroupRuntime.MasterChart != null)
            {
                ChartGroupRuntime.MasterChart.Session.BottomBarVisible = visible;
            }
            BottomToolStrip.Visible = visible;
            ToggleBottomBarButton2.Visible = !visible;
            if (save)
            {
                MainWindow.SaveSession();
            }
        }

        ///////////////////////////////////////////////


        private void eaCombo_Click(object sender, ControllerEventArgs e)
        {

        }

        private void scriptCombo_Click(object sender, ControllerEventArgs e)
        {

        }

        private void openEaButton_Click(object sender, ControllerEventArgs e)
        {
            if (ChartGroupRuntime.MasterChart.Expert != null)
            {
                ExpertDialogController d = new ExpertDialogController(rmiManager, ChartGroupRuntime.MasterChart.Expert);
                d.ShowDialog(MainWindow);

                if (d.DialogResult == DialogResult.OK)
                {
                    MainWindow.SaveSession();
                }
            }
        }

        private void inTestButton_Click(object sender, ControllerEventArgs e)
        {
            ChartGroupRuntime.MasterChart.Session.AppearsInTest = InTestButton.Checked;
            MainWindow.SaveSession();

            UpdateEAInTest();
            MainWindow.UpdateListInLauncherPanel();
        }



        private void openScriptButton_Click(object sender, ControllerEventArgs e)
        {
            if (ChartGroupRuntime.MasterChart.Script != null)
            {
                ScriptDialogController d = new ScriptDialogController(rmiManager, ChartGroupRuntime.MasterChart.Script);
                d.ShowDialog(MainWindow);

                if (d.DialogResult == DialogResult.OK)
                {
                    MainWindow.SaveSession();

                    //int max = StartScript();
                    //d.ProgressBar.Enabled = true;
                    //d.ProgressBar.Minimum = 0;
                    //d.ProgressBar.Maximum = max;
                    //for (int i = 1; i <= max; i++)
                    //{
                    //d.ProgressBar.Value += 
                    //StepScript();
                    //}
                }
            }
        }

        private void toggleBottomBarButton1_Click(object sender, ControllerEventArgs e)
        {
            SetBottomBarVisible(!ChartGroupRuntime.MasterChart.Session.BottomBarVisible, true);
        }

        private void toggleBottomBarButton2_Click(object sender, ControllerEventArgs e)
        {
            SetBottomBarVisible(!ChartGroupRuntime.MasterChart.Session.BottomBarVisible, true);
        }

    }
}
