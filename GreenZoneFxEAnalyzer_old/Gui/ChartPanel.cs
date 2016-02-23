using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Properties;
using GreenZoneFxEngine.Gui.Chart;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine
{
    public partial class ChartPanel : UserControl
    {
        private Form1 form;
        private ChartGroupPanel parent;
        private ToolStripControlHost dtTScomponent1, dtTScomponent2;
        private DateTimePicker fromTimePicker;
        private DateTimePicker toTimePicker;

        EventHandler  dFrom, dTo;

        public ChartPanel()
        {
            InitializeComponent();
            fromTimePicker = new DateTimePicker();
            fromTimePicker.Width = 85;
            fromTimePicker.Format = DateTimePickerFormat.Custom;
            fromTimePicker.CustomFormat = GreenZoneUtils.GetShortDateTimePattern();
            toTimePicker = new DateTimePicker();
            toTimePicker.Width = 85;
            toTimePicker.Format = DateTimePickerFormat.Custom;
            toTimePicker.CustomFormat = GreenZoneUtils.GetShortDateTimePattern();
            dtTScomponent1 = new ToolStripControlHost(fromTimePicker);
            dtTScomponent2 = new ToolStripControlHost(toTimePicker);

            toolStrip1.Items.Add(new ToolStripLabel("From:"));
            toolStrip1.Items.Add(dtTScomponent1);
            toolStrip1.Items.Add(new ToolStripLabel("To:"));
            toolStrip1.Items.Add(dtTScomponent2);

            dFrom = delegate(object sender2, EventArgs e2)
                    {
                        ChartRuntime.Session.From = fromTimePicker.Value;
                        form.UpdateListInLauncherPanel();
                        form.SaveSession();
                    };
            dTo = delegate(object sender2, EventArgs e2)
                    {
                        ChartRuntime.Session.To = toTimePicker.Value;
                        form.UpdateListInLauncherPanel();
                        form.SaveSession();
                    };

            fromTimePicker.ValueChanged += dFrom;
            toTimePicker.ValueChanged += dTo;
        }

        internal void Init(ChartGroupPanel parent, EnvironmentRuntime environment, ChartRuntime chartRuntime)
        {
            this.parent = parent;
            this.form = parent.Form1;
            Environment = environment;
            ChartRuntime = chartRuntime;

            UpdateSymbols();

            UpdateIndicators();

            fromTimePicker.Value = (DateTime)ChartRuntime.Session.From;
            toTimePicker.Value = (DateTime)ChartRuntime.Session.To;
            SetTopBarVisible(ChartRuntime.Session.TopBarVisible);

            if (ChartRuntime.IsMaster)
            {
                closeChartButton1.Image = Resources.Close_16xLG;
                closeChartButton2.Image = Resources.Close_16xLG;
                addChartButton.Enabled = true;
                addChartButton.Image = Resources.action_add_16xMD;
            }
            else
            {
                closeChartButton1.Image = Resources.action_Cancel_16xSM;
                closeChartButton2.Image = Resources.action_Cancel_16xSM;
                addChartButton.Enabled = false;
                addChartButton.Image = null;
            }

            UpdateAutoSeries();

            UpdateConnectButton();

            chart1.Init(this, chartRuntime);

            form.SaveSession();
            form.UpdateListInLauncherPanel();

            Initialized = true;
        }

        private bool Initialized
        {
            get;
            set;
        }

        public EnvironmentRuntime Environment
        {
            get;
            internal set;
        }

        public ChartRuntime ChartRuntime
        {
            get;
            internal set;
        }

        public ChartGroupPanel ChartGroupPanel
        {
            get
            {
                return parent;
            }
        }

        internal void DockIt()
        {
            this.Location = new System.Drawing.Point(0, 0);
            //this.Anchor = AnchorStyles.None;
            this.Size = new Size(0, 0);

            this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        internal void UpdateAll()
        {
            UpdateSymbols();
        }

        void UpdateSymbols()
        {
            symbolDdButton.DropDownItems.Clear();
            addChartButton.DropDownItems.Clear();
            foreach (symbol symbol in Environment.Symbols)
            {
                ToolStripItem mi = new ToolStripMenuItem(symbol.ToString());
                ToolStripMenuItem mi_ac = new ToolStripMenuItem(symbol.ToString());

                Dictionary<string, TimePeriodConst> DTimePeriodConst = new Dictionary<string, TimePeriodConst>();

                foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                {
                    if (TimeSeriesRuntime.IsSeriesAvailable(Environment, symbol, v))
                    {
                        DTimePeriodConst[v.GetLongTxt()] = v;

                        //ToolStripMenuItem mi_ac_2 = new ToolStripMenuItem(v.GetLongTxt(), null, null, symbol.strSymbol);
                        ToolStripItem mi_ac_2 = mi_ac.DropDownItems.Add(v.GetLongTxt());

                        mi_ac_2.Click += new EventHandler(mi_ac_2_Click);
                    }
                }
                if (DTimePeriodConst.Count > 0)
                {
                    symbolDdButton.DropDownItems.Add(mi);
                    mi.Click += new EventHandler(symbolDd_mi_Click);

                    if (symbol == ChartRuntime.Symbol)
                    {
                        mi.PerformClick();
                    }

                    addChartButton.DropDownItems.Add(mi_ac);
                    mi_ac.Tag = DTimePeriodConst;
                    mi_ac.Click += new EventHandler(mi_ac_Click);
                }
            }
        }

        void symbolDd_mi_Click(object sender, EventArgs e)
        {
            foreach (var m0 in symbolDdButton.DropDownItems)
            {
                ((ToolStripMenuItem)m0).Checked = false;
            }
            ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
            misnd.Checked = true;
            symbolDdButton.Text = misnd.Text;

            symbol newSymbol = Environment.GetSymbol(misnd.Text);
            TimePeriodConst newPeriod = ChartRuntime.Period;
            if (!TimeSeriesRuntime.IsSeriesAvailable(Environment, newSymbol, newPeriod))
            {
                foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                {
                    if (TimeSeriesRuntime.IsSeriesAvailable(Environment, newSymbol, v))
                    {
                        newPeriod = v;
                        break;
                    }
                }
            }

            try
            {
                ChartRuntime.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
                ChartRuntime.Update(newSymbol, newPeriod);
                periodDdButton.Text = newPeriod.GetShortTxt();

                string txt = ChartRuntime.Symbol + " " + ChartRuntime.Period.GetShortTxt();

                if (ChartRuntime.IsMaster)
                {
                    parent.TabPage.Text = txt;
                }
                symPerMiniLabel.Text = txt;

                if (Initialized)
                {
                    form.SaveSession();
                    form.UpdateListInLauncherPanel();
                }

                UpdatePeriods();
                UpdateChartTypes();
                UpdateIndicators();
                chart1.UpdateIndicatorPanels();
                chart1.UpdateSeries();
            }
            catch (TimeSeriesException ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to load chart for symbol:" + newSymbol + " period:" + newPeriod, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void mi_ac_2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
            symbol sym = Environment.GetSymbol(misnd.OwnerItem.Text);
            Dictionary<string, TimePeriodConst> DTimePeriodConst = (Dictionary<string, TimePeriodConst>)misnd.OwnerItem.Tag;

            TimePeriodConst v2 = DTimePeriodConst[misnd.Text];
            ChartGroupPanel.AddChart(null, sym, v2);
        }
        void mi_ac_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
            symbol sym = Environment.GetSymbol(misnd.Text);
            ChartGroupPanel.AddChart(null, sym);
        }

        internal void UpdatePeriods()
        {
            periodDdButton.DropDownItems.Clear();
            Dictionary<string, TimePeriodConst> DTimePeriodConst = new Dictionary<string, TimePeriodConst>();
            periodDdButton.Tag = DTimePeriodConst;

            bool addsep;
            addsep = false;
            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(TimePeriodCategory.TICKS))
            {
                add_period(DTimePeriodConst, v);
                addsep = true;
            }

            if (addsep)
            {
                periodDdButton.DropDownItems.Add(new ToolStripSeparator());
            }

            addsep = false;
            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(TimePeriodCategory.SECS))
            {
                add_period(DTimePeriodConst, v);
                addsep = true;
            }

            if (addsep)
            {
                periodDdButton.DropDownItems.Add(new ToolStripSeparator());
            }

            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(TimePeriodCategory.MT4 | TimePeriodCategory.MT5))
            {
                add_period(DTimePeriodConst, v);
            }
        }
        private void add_period(Dictionary<string, TimePeriodConst> DTimePeriodConst, TimePeriodConst period)
        {
            if (TimeSeriesRuntime.IsSeriesAvailable(Environment, ChartRuntime.Symbol, period))
            {

                DTimePeriodConst[period.GetLongTxt()] = period;
                ToolStripItem mi = periodDdButton.DropDownItems.Add(period.GetLongTxt());
                mi.Click += new EventHandler(periodDd_mi_Click);
                if (period == ChartRuntime.Period)
                {
                    mi.PerformClick();
                }
            }
        }

        void periodDd_mi_Click(object sender, EventArgs e)
        {
            foreach (var m0 in periodDdButton.DropDownItems)
            {
                if (m0 is ToolStripMenuItem)
                {
                    ((ToolStripMenuItem)m0).Checked = false;
                }
            }
            ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
            misnd.Checked = true;
            Dictionary<string, TimePeriodConst> DTimePeriodConst = (Dictionary<string, TimePeriodConst>)
                            periodDdButton.Tag;

            TimePeriodConst v2 = DTimePeriodConst[misnd.Text];
            periodDdButton.Image = misnd.Image;
            periodDdButton.Text = v2.GetShortTxt();
            ChartRuntime.SymbolRuntime.Session.DataPeriod = TimePeriodConst.PERIOD_CURRENT;
            ChartRuntime.Period = v2;
            string txt = ChartRuntime.Symbol + " " + ChartRuntime.Period.GetShortTxt();
            if (ChartRuntime.IsMaster)
            {
                parent.TabPage.Text = txt;
            }
            symPerMiniLabel.Text = txt;

            UpdateChartTypes();
            chart1.UpdateIndicatorPanels();
            chart1.UpdateSeries();
            UpdateIndicators();

            if (Initialized)
            {
                form.SaveSession();
                form.UpdateListInLauncherPanel();
            }
        }

        private void UpdateChartTypes()
        {
            chartTypeDdButton.DropDownItems.Clear();
            Dictionary<string, ChartType> DChartType = new Dictionary<string, ChartType>();
            chartTypeDdButton.Tag = DChartType;

            ChartType oldct = ChartRuntime.Session.ChartType;
            if (ChartRuntime.Period.GetCategory() != TimePeriodCategory.TICKS)
            {
                ChartRuntime.Session.ChartType = ChartRuntime.Session.DefaultChartType;
            }

            ToolStripItem mi0 = null;

            foreach (ChartType v in Enum.GetValues(typeof(ChartType)))
            {
                if (SeriesChartDrawer.IsDrawerAvailable(v, ChartRuntime.Period))
                {
                    DChartType[v.GetLongTxt()] = v;
                    ToolStripItem mi = chartTypeDdButton.DropDownItems.Add(v.GetLongTxt());
                    mi.Click += new EventHandler(chartTypeDd_mi_Click);
                    if (v == ChartRuntime.Session.ChartType)
                    {
                        mi.PerformClick();
                    }
                    if (mi0 == null)
                    {
                        mi0 = mi;
                    }
                }
            }

            if (!DChartType.ContainsValue(oldct))
            {
                mi0.PerformClick();
            }
        }

        void chartTypeDd_mi_Click(object sender, EventArgs e)
        {
            foreach (var m0 in chartTypeDdButton.DropDownItems)
            {
                ((ToolStripMenuItem)m0).Checked = false;
            }
            ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
            misnd.Checked = true;
            Dictionary<string, ChartType> DChartType = (Dictionary<string, ChartType>)
                             chartTypeDdButton.Tag;

            ChartType v2 = DChartType[misnd.Text];
            chartTypeDdButton.Image = misnd.Image;
            chartTypeDdButton.Text = v2.GetShortTxt();
            ChartRuntime.Session.ChartType = v2;

            chart1.UpdateSeries();

            if (Initialized)
            {
                form.SaveSession();
            }
        }

        internal void UpdateIndicators()
        {
            indicatorsDdButton.DropDownItems.Clear();

            List<ToolStripMenuItem> l = new List<ToolStripMenuItem>();
            foreach (var ind in ChartRuntime.GuiSeriesManager.DefaultCache)
            {
                if (ind.Visible)
                {
                    Mt4IndicatorInfo info = ind.IndicatorInfo;
                    if (info.SystemType != null)
                    {
                        ToolStripMenuItem mi = new ToolStripMenuItem(info.Name + " " + ind.Session.ShortName, null, null, info.SystemTypeId + "," + GreenZoneUtils.GenerateArgKey(ind.GenerateParamArray()));
                        l.Add(mi);

                        mi.Click += new EventHandler(indicatorsDd_1_mi_Click);

                    }
                }
            }
            l.Sort(compareToolStripItems);
            foreach (var i in l)
            {
                indicatorsDdButton.DropDownItems.Add(i);
            }


            indicatorsDdButton.DropDownItems.Add(new ToolStripSeparator());
            l.Clear();

            SortedDictionary<string, List<ToolStripMenuItem>> cat = new SortedDictionary<string, List<ToolStripMenuItem>>();
            SortedDictionary<string, ToolStripMenuItem> all = new SortedDictionary<string, ToolStripMenuItem>();
            foreach (string s in Environment.Indicators)
            {
                Mt4IndicatorInfo info = Environment.GetIndicatorInfo(s);

                if (info.SystemType != null)
                {
                    ToolStripMenuItem mi = new ToolStripMenuItem(info.FullName, null, null, info.SystemTypeId);

                    List<string> cs = new List<string>();
                    if (info.Categories == null)
                    {
                        cs.Add("zzz");
                    }
                    else
                    {
                        cs.AddRange(info.Categories);
                    }
                    foreach (var c in cs)
                    {
                        List<ToolStripMenuItem> cl;
                        if (!cat.TryGetValue(c, out cl))
                        {
                            cl = new List<ToolStripMenuItem>();
                            cat[c] = cl;
                        }
                        cl.Add(mi);
                    }
                    mi.Click += new EventHandler(indicatorsDd_2_mi_Click);

                    if (!all.ContainsKey(info.FullName))
                    {
                        ToolStripMenuItem miall = new ToolStripMenuItem(info.FullName, null, null, info.SystemTypeId);
                        all[info.FullName] = miall;
                        miall.Click += new EventHandler(indicatorsDd_2_mi_Click);
                    }
                }
            }

            foreach (var e in cat)
            {
                e.Value.Sort(compareToolStripItems);

                ToolStripMenuItem mci;
                if ("zzz" == e.Key)
                {
                    mci = new ToolStripMenuItem("Others");
                }
                else
                {
                    mci = new ToolStripMenuItem(e.Key);
                }

                indicatorsDdButton.DropDownItems.Add(mci);

                foreach (var mi in e.Value)
                {
                    mci.DropDownItems.Add(mi);
                }
            }

            ToolStripMenuItem mciall = new ToolStripMenuItem("All");
            indicatorsDdButton.DropDownItems.Add(mciall);
            foreach (var mi in all.Values)
            {
                mciall.DropDownItems.Add(mi);
            }
        }

        internal void ShowIndicatorProperties(IndicatorRuntime ind)
        {
            IndicatorId id0 = ind.Id;

            IndicatorRuntimeDialog d = new IndicatorRuntimeDialog(ind);
            d.ShowDialog(parent);
            d.Enabled = false;
            d.Close();
            d.Dispose();

            if (d.DialogResult == DialogResult.OK)
            {
                ind = ind.Copy();
                ChartRuntime.ReplaceIndicator(id0, ind);
                ind.Visible = true;

                UpdateIndicators();
                UpdateSeries();
            }
            else if (d.DialogResult == DialogResult.Abort)
            {
                ChartRuntime.RemoveIndicator(id0);
                chart1.RemoveIndicatorPanel(ind);

                UpdateIndicators();
                UpdateSeries();
            }

            form.SaveSession();
        }

        void indicatorsDd_1_mi_Click(object sender, EventArgs e)
        {
            if (Initialized)
            {
                ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
                IndicatorId id = new IndicatorId(Environment, misnd.Name);

                IndicatorRuntime ind = ChartRuntime.GuiSeriesManager.DefaultCache[id];
                ShowIndicatorProperties(ind);
            }
        }

        void indicatorsDd_2_mi_Click(object sender, EventArgs e)
        {
            if (Initialized)
            {
                ToolStripMenuItem misnd = (ToolStripMenuItem)sender;
                Mt4IndicatorInfo info = Environment.GetIndicatorInfo(misnd.Name);

                IndicatorRuntimeDialog d = new IndicatorRuntimeDialog(ChartRuntime, info);
                d.removeButton.Visible = false;
                d.ShowDialog(parent);
                d.Enabled = false;
                d.Close();
                d.Dispose();

                if (d.DialogResult == DialogResult.OK)
                {
                    IndicatorRuntime ind = d.IndicatorRuntime;
                    ChartRuntime.AddIndicator(ind);

                    chart1.AddIndicatorPanel(ind);
                    ind.Visible = true;

                    UpdateIndicators();
                    UpdateSeries();

                    form.SaveSession();
                }
            }
        }


        private void SetTopBarVisible(bool visible, bool save=false)
        {
            ChartRuntime.Session.TopBarVisible = visible;
            toolStrip1.Visible = visible;
            panel2.Visible = !visible;
            panel3.Visible = !visible;

            if (save)
            {
                form.SaveSession();
            }
        }

        private void UpdateConnectButton() {

            if (ChartRuntime.Session.IsCursorBarConnected)
            {
                connectCursorButton.Checked = true;
                connectCursorButton.Image = Resources.CursorBar_16xLG;
            }
            else
            {
                connectCursorButton.Checked = false;
                connectCursorButton.Image = Resources.CursorBarB_16xLG;
            }
        }

        internal void UpdateChartAndCursor()
        {
            chart1.UpdateChartAndCursor();
        }

        internal void UpdateCursor()
        {
            chart1.UpdateCursor();
        }

        internal void DrawCursor()
        {
            chart1.DrawCursor();
        }

        internal void UpdateSeries()
        {
            chart1.UpdateSeries();
        }

        internal void UpdateAutoSeries()
        {
            autoSeriesRangeButton.Checked = ChartRuntime.Session.AutoSeriesRange;
        }


        int compareToolStripItems(ToolStripItem a, ToolStripItem b)
        {
            return a.Text.CompareTo(b.Text);
        }

        private void toolStrip1_EndDrag(object sender, EventArgs e)
        {
            if (toolStrip1.Parent.Parent != toolStripContainer1)
            {
                toolStrip1.Parent = toolStripContainer1.TopToolStripPanel;
            }
        }




        private void toggleTopBarButton1_Click(object sender, EventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void toggleTopBarButton2_Click(object sender, EventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void symPerMiniLabel_Click(object sender, EventArgs e)
        {
            SetTopBarVisible(!ChartRuntime.Session.TopBarVisible, true);
        }

        private void closeChartButton1_Click(object sender, EventArgs e)
        {
            if (ChartRuntime.IsMaster)
            {
                form.RemoveChartGroup(ChartGroupPanel);
            }
            else
            {
                parent.RemoveChart(this);
            }
        }

        private void closeChartButton2_Click(object sender, EventArgs e)
        {
            if (ChartRuntime.IsMaster)
            {
                form.RemoveChartGroup(ChartGroupPanel);
            }
            else
            {
                parent.RemoveChart(this);
            }
        }

        private void addChartButton_ButtonClick(object sender, EventArgs e)
        {
            ChartGroupPanel.AddChart();
        }

        private void connectCursorButton_Click(object sender, EventArgs e)
        {
            if (ChartRuntime.Session.IsCursorBarConnected)
            {
                ChartRuntime.Session.IsCursorBarConnected = false;
                ChartGroupPanel.UpdateAllCursor();
            }
            else
            {
                ChartRuntime.Session.IsCursorBarConnected = true;
                ChartGroupPanel.UpdateAllCursor();
            }
            UpdateCursor();
            form.SaveSession();
            UpdateConnectButton();
        }

        private void autoSeriesRangeButton_Click(object sender, EventArgs e)
        {
            ChartRuntime.Session.AutoSeriesRange = autoSeriesRangeButton.Checked;
            chart1.UpdateSeries();
            form.SaveSession();
        }

    }
}
