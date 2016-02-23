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

namespace GreenZoneFxEngine
{
    public partial class ChartGroupPanel : UserControl, IForm1TabPanel
    {
        private Form1 parent;

        private readonly List<ChartPanel> chartPanels = new List<ChartPanel>();
        private readonly IList<ChartPanel> chartPanels_ro;

        ChartGroupRuntime chartGroupRuntime;

        public ChartGroupPanel()
        {
            chartPanels_ro = chartPanels.AsReadOnly();
            InitializeComponent();
        }

        internal void Init(Form1 parent, EnvironmentRuntime environment, ChartGroupRuntime chartGroupRuntime)
        {
            this.parent = parent;
            Environment = environment;
            this.chartGroupRuntime = chartGroupRuntime;
            TabPage = new TabPage();
            TabPage.BackColor = SystemColors.Control;
            TabPage.Padding = new Padding(0);

            TabPage.Controls.Add(this);
            ChartGroupRuntime = chartGroupRuntime;

            Form1.SaveSession();
            UpdateEAInTest();
            UpdateScriptInTest();
            parent.UpdateListInLauncherPanel();

            UpdateEAScript();
        }

        private bool Initialized
        {
            get;
            set;
        }

        public Form1 Form1
        {
            get
            {
                return parent;
            }
        }

        internal IList<ChartPanel> ChartPanels
        {
            get
            {
                return chartPanels_ro;
            }
        }

        public TabPage TabPage
        {
            get;
            private set;
        }

        public EnvironmentRuntime Environment
        {
            get;
            internal set;
        }

        public ChartGroupRuntime ChartGroupRuntime
        {
            get;
            internal set;
        }

        internal ChartPanel AddChart(ChartRuntime chartRuntime = null, bool createdByUser = true)
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

            ChartPanel chartPanel = AddChart(chartRuntime, sym, TimePeriodConst.PERIOD_H1, createdByUser);
            return chartPanel;
        }

        internal ChartPanel AddChart(ChartRuntime chartRuntime, symbol symbol, TimePeriodConst period = TimePeriodConst.PERIOD_H1, bool createdByUser = true)
        {
            if (chartRuntime == null)
            {
                chartRuntime = new ChartRuntime(ChartGroupRuntime, ChartGroupRuntime.Charts.Count == 0);

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
                SeriesManagerCache seriesCache = chartRuntime.GuiSeriesManager.DefaultCache;

                int cursorPosition = chartRuntime.Group.Session.CursorPosition;

                SeriesRange r;
                if (seriesCache != null)
                {
                    r = new SeriesRange(0, 100, seriesCache.Open[0] * 0.5, seriesCache.Open[0] * 2);
                }
                else
                {
                    r = new SeriesRange(0, 100, 0, 1000);
                }

                r.CursorPosition = cursorPosition;
                chartRuntime.SeriesRange = r;

            }
            ChartPanel p = new ChartPanel();
            chartPanels.Add(p);
            p.Init(this, Environment, chartRuntime);

            p.DockIt();

            LayOutCharts();

            this.tableLayoutPanel1.Add(p);
            
            if (createdByUser)
            {
                ChartGroupRuntime.AddChart(p.ChartRuntime);
                Form1.SaveSession();
            }

            return p;
        }

        internal void RemoveChart(ChartPanel p)
        {
            chartPanels.Remove(p);
            ChartGroupRuntime.RemoveChart(p.ChartRuntime);

            this.tableLayoutPanel1.Remove(p);

            LayOutCharts();

            Form1.SaveSession();

            parent.UpdateListInLauncherPanel();
            //PerformLayout();
        }

        public void DockIt()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
            if (ChartGroupRuntime.MasterChart == null)
            {
                TabPage.Text = "";
            }
            else
            {
                TabPage.Text = ChartGroupRuntime.MasterChart.Symbol + " " + ChartGroupRuntime.MasterChart.Period.GetShortTxt();
            }
        }

        internal void UpdateEAScript()
        {
            Initialized = false;
            try
            {
                scriptCombo.Items.Clear();
                ListItem<Mt4ScriptInfo> selectedScript = new ListItem<Mt4ScriptInfo>(null, "-- None --");
                scriptCombo.Items.Add(selectedScript);
                scriptCombo.SelectedIndex = 0;
                foreach (string s in Environment.Scripts)
                {
                    Mt4ScriptInfo script = Environment.GetScriptInfo(s);
                    ListItem<Mt4ScriptInfo> item = new ListItem<Mt4ScriptInfo>(script, script.Name);
                    scriptCombo.Items.Add(item);

                    if (ChartGroupRuntime.MasterChart != null && ChartGroupRuntime.MasterChart.Script != null && ChartGroupRuntime.MasterChart.Script.ScriptInfo.Equals(script))
                    {
                        selectedScript = item;
                    }
                }
                scriptCombo.SelectedIndexChanged +=
                    delegate(object sender2, EventArgs e2)
                    {
                        ListItem<Mt4ScriptInfo> item = (ListItem<Mt4ScriptInfo>)scriptCombo.SelectedItem;

                        if (Initialized)
                        {
                            if (item.Value != null)
                            {
                                ChartGroupRuntime.MasterChart.Script = ScriptRuntime.Create(ChartGroupRuntime.MasterChart, item.Value, chartGroupRuntime.MasterChart.GuiSeriesManager.DefaultCache);
                            }
                            else
                            {
                                ChartGroupRuntime.MasterChart.Script = null;
                            }

                            UpdateScriptInTest();

                            if (Initialized)
                            {
                                Form1.SaveSession();
                                parent.UpdateListInLauncherPanel();
                            }
                        }
                    };
                scriptCombo.SelectedItem = selectedScript;


                eaCombo.Items.Clear();
                ListItem<Mt4ExpertInfo> selectedExpert = new ListItem<Mt4ExpertInfo>(null, "-- None --");
                eaCombo.Items.Add(selectedExpert);
                eaCombo.SelectedIndex = 0;
                foreach (string s in Environment.Experts)
                {
                    Mt4ExpertInfo expert = Environment.GetExpertInfo(s);
                    ListItem<Mt4ExpertInfo> item = new ListItem<Mt4ExpertInfo>(expert, expert.Name);
                    eaCombo.Items.Add(item);

                    if (ChartGroupRuntime.MasterChart != null && ChartGroupRuntime.MasterChart.Expert != null && ChartGroupRuntime.MasterChart.Expert.ExpertInfo.Equals(expert))
                    {
                        selectedExpert = item;
                    }
                }
                eaCombo.SelectedIndexChanged +=
                    delegate(object sender2, EventArgs e2)
                    {
                        ListItem<Mt4ExpertInfo> item = (ListItem<Mt4ExpertInfo>)eaCombo.SelectedItem;

                        if (Initialized)
                        {
                            if (item.Value != null)
                            {
                                ChartGroupRuntime.MasterChart.Expert = ExpertRuntime.Create(ChartGroupRuntime.MasterChart, item.Value, chartGroupRuntime.MasterChart.GuiSeriesManager.DefaultCache);
                                ChartGroupRuntime.MasterChart.Period = ChartGroupRuntime.MasterChart.Period;

                                if (Initialized)
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

                        if (Initialized)
                        {
                            Form1.SaveSession();
                            parent.UpdateListInLauncherPanel();
                        }
                    };
                eaCombo.SelectedItem = selectedExpert;

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
                Initialized = true;
            }
        }

        internal void UpdateEAInTest()
        {
            if (ChartGroupRuntime.MasterChart == null)
            {
                openEaButton.Enabled = false;
                inTestButton.Enabled = false;
            }
            else if (ChartGroupRuntime.MasterChart.Expert == null)
            {
                ChartGroupRuntime.MasterChart.Session.AppearsInTest = false;
                Form1.SaveSession();

                openEaButton.Enabled = false;
                inTestButton.Enabled = false;
            }
            else
            {
                openEaButton.Enabled = true;
                inTestButton.Enabled = true;
            }

            if (ChartGroupRuntime.MasterChart != null && ChartGroupRuntime.MasterChart.Session.AppearsInTest)
            {
                inTestButton.Text = "in test";
                inTestButton.Checked = true;
            }
            else
            {
                inTestButton.Text = "not in test";
                inTestButton.Checked = false;
            }
        }

        internal void UpdateScriptInTest()
        {
            if (ChartGroupRuntime.MasterChart == null || ChartGroupRuntime.MasterChart.Script == null)
            {
                openScriptButton.Enabled = false;
            }
            else
            {
                openScriptButton.Enabled = true;
            }
        }
        
        private void SetBottomBarVisible(bool visible, bool save = false)
        {
            if (ChartGroupRuntime.MasterChart != null)
            {
                ChartGroupRuntime.MasterChart.Session.BottomBarVisible = visible;
            }
            toolStrip2.Visible = visible;
            toggleBottomBarButton2.Visible = !visible;
            if (save)
            {
                Form1.SaveSession();
            }
        }

        private void LayOutCharts()
        {

        }

        internal void UpdateAllCursor(ChartPanel invoker = null)
        {
            foreach (var cp in chartPanels)
            {
                if (invoker != cp)
                {
                    cp.UpdateCursor();
                }
            }
            debug();
        }

        internal void DrawAllCursor(ChartPanel invoker = null)
        {
            foreach (var cp in chartPanels)
            {
                if (invoker != cp)
                {
                    cp.DrawCursor();
                }
            }
            debug();
        }

        internal void SetAllScrolledTime(ChartPanel invoker, datetime focusedTime)
        {
            foreach (var cp in chartPanels)
            {
                if (invoker != cp && cp.ChartRuntime.Session.IsCursorBarConnected)
                {
                    cp.ChartRuntime.ScrolledBarTime = focusedTime;
                    cp.UpdateChartAndCursor();
                }
            }

            debug();
        }

        internal void debug()
        {
#if(DEBUG)
            NormalSeriesManagerCache cache = chartGroupRuntime.MasterChart.GuiSeriesManager.DefaultCache;
            parent.statusLabel.Text = "f:" + GreenZoneUtils.FormatDateTime((DateTime)cache.FocusedTime) + " fi:" + cache.FileOffset + " bufi:" + cache.IndOffset + " len:" + cache.IndCount;
#endif
        }

        private void eaCombo_Click(object sender, EventArgs e)
        {

        }

        private void scriptCombo_Click(object sender, EventArgs e)
        {

        }

        private void openEaButton_Click(object sender, EventArgs e)
        {
            if (ChartGroupRuntime.MasterChart.Expert != null)
            {
                ExpertRuntimeDialog d = new ExpertRuntimeDialog(ChartGroupRuntime.MasterChart.Expert);
                d.ShowDialog(parent);
                d.Enabled = false;

                if (d.DialogResult == DialogResult.OK)
                {
                    Form1.SaveSession();
                }
                d.Close();
                d.Dispose();
            }
        }

        private void inTestButton_Click(object sender, EventArgs e)
        {
            ChartGroupRuntime.MasterChart.Session.AppearsInTest = inTestButton.Checked;
            Form1.SaveSession();

            UpdateEAInTest();
            parent.UpdateListInLauncherPanel();
        }

        private void toolStripDropDownButton7_Click(object sender, EventArgs e)
        {
            toolStrip2.Visible = !toolStrip2.Visible;
        }

        private void toolStripDropDownButton8_Click(object sender, EventArgs e)
        {
            toolStrip2.Visible = !toolStrip2.Visible;
        }



        private void openScriptButton_Click(object sender, EventArgs e)
        {
            if (ChartGroupRuntime.MasterChart.Script != null)
            {
                ScriptRuntimeDialog d = new ScriptRuntimeDialog(ChartGroupRuntime.MasterChart.Script);
                d.ShowDialog(parent);
                d.Enabled = false;

                if (d.DialogResult == DialogResult.OK)
                {
                    Form1.SaveSession();

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
                d.Close();
                d.Dispose();
            }
        }

        private void toggleBottomBarButton1_Click(object sender, EventArgs e)
        {
            SetBottomBarVisible(!ChartGroupRuntime.MasterChart.Session.BottomBarVisible, true);
        }

        private void toggleBottomBarButton2_Click(object sender, EventArgs e)
        {
            SetBottomBarVisible(!ChartGroupRuntime.MasterChart.Session.BottomBarVisible, true);
        }

    }
}
