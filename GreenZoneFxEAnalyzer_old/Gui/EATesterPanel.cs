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
using GreenZoneFxEngine.Properties;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;

namespace GreenZoneFxEngine
{
    public partial class EATesterPanel : UserControl
    {
        Form1 parent;
        private readonly Dictionary<TestType, ListItem<TestType>> testTypeLis = new Dictionary<TestType, ListItem<TestType>>();
        private readonly Dictionary<TimePeriodConst, ListItem<TimePeriodConst>> periodLis = new Dictionary<TimePeriodConst, ListItem<TimePeriodConst>>();

        bool updatingEa = false;
        bool updatingTableSel = false;

        private DataGridViewColumn SelectEAColumn;
        private DataGridViewColumn tableTestEasEACol;
        private DataGridViewTextBoxColumn tableTestEasPeriodCol;
        private DataGridViewImageColumn IconColumn;

        public EATesterPanel()
        {
            InitializeComponent();

            this.SelectEAColumn = new DataGridViewColumn(new CheckBoxCell0());
            this.tableTestEasEACol = new DataGridViewColumn(new TreeNodeWithTextCell());
            this.tableTestEasPeriodCol = new DataGridViewTextBoxColumn();
            this.IconColumn = new DataGridViewImageColumn();

            // 
            // SelectEAColumn
            // 
            this.SelectEAColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.SelectEAColumn.FillWeight = 1F;
            this.SelectEAColumn.Frozen = true;
            this.SelectEAColumn.HeaderText = "";
            this.SelectEAColumn.MinimumWidth = 20;
            this.SelectEAColumn.Name = "SelectEAColumn";
            this.SelectEAColumn.DataPropertyName = "Selected";
            this.SelectEAColumn.ReadOnly = true;
            this.SelectEAColumn.Resizable = DataGridViewTriState.False;
            this.SelectEAColumn.Width = 20;
            // 
            // tableTestEasEA
            // 
            this.tableTestEasEACol.FillWeight = 1F;
            this.tableTestEasEACol.HeaderText = "EA";
            this.tableTestEasEACol.MinimumWidth = 20;
            this.tableTestEasEACol.Name = "tableTestEasEA";
            this.tableTestEasEACol.DataPropertyName = "Title";
            this.tableTestEasEACol.ReadOnly = true;
            // 
            // tableTestEasPeriod
            // 
            this.tableTestEasPeriodCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.tableTestEasPeriodCol.FillWeight = 1F;
            this.tableTestEasPeriodCol.HeaderText = "Period";
            this.tableTestEasPeriodCol.MinimumWidth = 50;
            this.tableTestEasPeriodCol.Name = "tableTestEasPeriod";
            this.tableTestEasPeriodCol.DataPropertyName = "Period";
            this.tableTestEasPeriodCol.ReadOnly = true;
            this.tableTestEasPeriodCol.Resizable = DataGridViewTriState.False;
            this.tableTestEasPeriodCol.Width = 50;
            // 
            // IconColumn
            // 
            this.IconColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.IconColumn.FillWeight = 1F;
            this.IconColumn.HeaderText = "";
            this.IconColumn.MinimumWidth = 14;
            this.IconColumn.Name = "IconColumn";
            this.IconColumn.DataPropertyName = "Icon";
            this.IconColumn.ReadOnly = true;
            this.IconColumn.Width = 14;

            this.dataGridView1.Columns.Add(this.SelectEAColumn);
            this.dataGridView1.Columns.Add(this.tableTestEasEACol);
            this.dataGridView1.Columns.Add(this.tableTestEasPeriodCol);
            this.dataGridView1.Columns.Add(this.IconColumn);

            foreach (TestType tt in Enum.GetValues(typeof(TestType)))
            {
                ListItem<TestType> ltt = new ListItem<TestType>(tt, tt.GetShortText());
                testTypeLis[tt] = ltt;
                methodCombo.Items.Add(ltt);
            }
        }


        public void Init(Form1 parent)
        {
            this.parent = parent;

            if (parent.Environment != null)
            {
                var envs = parent.Environment.Session;
                scrollAcrossTabsCb.Checked = envs.ScrollAcrossCharts;
                skipEmptyPeriodsCb.Checked = envs.SkipEmptyPeriods;
                updateSpreadTickCb.Checked = envs.UpdateSpreadTick;
                speedTrackBar.Value = envs.EATestingSpeed;
                progressTrackBar1.TickPosition = envs.EATestingTrackBarTick;
            }
        }

        internal void UpdateEAsInTest()
        {
            try
            {
                updatingEa = true;
                int si = -1;
                DataGridViewRow r = dataGridView1.CurrentRow;
                if (r != null)
                {
                    si = r.Index;
                }

                dataGridView1.Rows.Clear();
                bool hasEnabledExpert = false;
                Dictionary<string, List<ChartGroupPanel>> cgps = new Dictionary<string, List<ChartGroupPanel>>();
                foreach (IForm1TabPanel tp in parent.TabPanels)
                {
                    if (tp is ChartGroupPanel)
                    {
                        ChartGroupPanel chg = (ChartGroupPanel)tp;
                        // FIXME
                        // not yet initialized
                        if (chg.ChartPanels.Count == 0)
                        {
                            return;
                        }
                        ChartPanel chp = chg.ChartPanels[0];
                        ChartRuntime ch = chp.ChartRuntime;
                        if (ch.Expert != null)
                        {
                            if (ch.Session.AppearsInTest)
                            {
                                hasEnabledExpert = true;
                            }
                            string symbol = ch.Symbol.ToString();
                            List<ChartGroupPanel> ps;
                            if (!cgps.TryGetValue(symbol, out ps))
                            {
                                ps = new List<ChartGroupPanel>();
                                cgps[symbol] = ps;
                            }
                            ps.Add(chg);
                        }
                    }
                }

                DataGridViewCellStyle noCellStyle = new DataGridViewCellStyle();
                foreach (var e in cgps)
                {
                    ChartPanel chp0 = null;
                    foreach (ChartGroupPanel chg in e.Value)
                    {
                        chp0 = chg.ChartPanels[0];
                        break;
                    }
                    TimePeriodConst testingPeriod0 = chp0.ChartRuntime.SymbolRuntime.BestPeriod;
                    TimePeriodConst testingPeriod = chp0.ChartRuntime.SymbolRuntime.Session.DataPeriod;
                    if (testingPeriod == 0 || testingPeriod0.GetOrdinal() < testingPeriod.GetOrdinal())
                    {
                        testingPeriod = testingPeriod0;
                        chp0.ChartRuntime.SymbolRuntime.Session.DataPeriod = testingPeriod;
                    }

                    TableRow hrow = new TableRow(this, chp0.ChartRuntime.SymbolRuntime);

                    foreach (ChartGroupPanel chg in e.Value)
                    {
                        ChartPanel chp = chg.ChartPanels[0];

                        ChartRuntime ch = chp.ChartRuntime;
                        ExpertRuntime expert = ch.Expert;
                        new TableRow(this, chp);
                    }
                    hrow.isLast = true;
                }


                if (si >= dataGridView1.Rows.Count)
                {
                    si = dataGridView1.Rows.Count - 1;
                }

                if (si > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[si].Cells[0];
                }

                var envs = parent.Environment.Session;
                scrollAcrossTabsCb.Checked = envs.ScrollAcrossCharts;
                skipEmptyPeriodsCb.Checked = envs.SkipEmptyPeriods;
                updateSpreadTickCb.Checked = envs.UpdateSpreadTick;
                speedTrackBar.Value = envs.EATestingSpeed;
                progressTrackBar1.TickPosition = envs.EATestingTrackBarTick;

                if (hasEnabledExpert)
                {
                    startStopButton.Enabled = true;
                    updateSpreadTickCb.Enabled = true;
                    scrollAcrossTabsCb.Enabled = true;
                    skipEmptyPeriodsCb.Enabled = true;
                    speedTrackBar.Enabled = true;
                }
                else
                {
                    startStopButton.Enabled = false;
                    updateSpreadTickCb.Enabled = false;
                    scrollAcrossTabsCb.Enabled = false;
                    skipEmptyPeriodsCb.Enabled = false;
                    speedTrackBar.Enabled = false;
                }
            }
            finally
            {
                updatingEa = false;
                // TODO slightly bad
                dataGridView1_SelectionChanged(null, null);
            }
        }

        private void setDataPeriodTooltip(ChartRuntime chr, out string tooltip, out Image image)
        {
            TimePeriodConst dataPeriod = chr.SymbolRuntime.Session.DataPeriod;
            if (dataPeriod != TimePeriodConst.PERIOD_CURRENT)
            {
                if (chr.Period.GetOrdinal() >= dataPeriod.GetOrdinal())
                {
                    image = Resources.empty;
                    tooltip = null;
                }
                else if (chr.Period == TimePeriodConst.PERIOD_TICK_ASK && dataPeriod == TimePeriodConst.PERIOD_TICK)
                {
                    image = Resources.empty;
                    tooltip = null;
                }
                else
                {
                    image = Resources.Warning_yellow_7231_12x11;
                    tooltip = "Test data modelling period is not fine enough for modelling Chart period";
                }
            }
            else
            {
                image = Resources.Error_red_12x11;
                tooltip = "There is no timeframe period on file";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                TableRow row = (TableRow)dataGridView1.CurrentRow.Tag;
                ChartPanel chp = row.chartPanel;
                if (chp != null)
                {
                    parent.ChartIsInTestCheckedInPanel(chp.ChartGroupPanel, !row.Selected);
                }
            }

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow r = dataGridView1.CurrentRow;
            if (r != null)
            {
                TableRow row = (TableRow)r.Tag;
                ChartPanel chp = row.chartPanel;
                if (chp != null)
                {
                    parent.ChartSelectedInPanel(chp.ChartGroupPanel);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            parent.AddExpertClickedInPanel();
        }

        private void propertiesButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow r = dataGridView1.CurrentRow;
            if (r != null)
            {
                TableRow row = (TableRow)r.Tag;
                ChartPanel chp = row.chartPanel;
                ChartRuntime chr = chp.ChartRuntime;
                if (chr.Expert != null)
                {
                    ExpertRuntimeDialog d = new ExpertRuntimeDialog(chr.Expert);
                    d.ShowDialog(parent);
                    d.Enabled = false;

                    if (d.DialogResult == DialogResult.OK)
                    {
                        parent.SaveSession();
                    }
                    d.Close();
                    d.Dispose();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!updatingEa)
                {
                    updatingTableSel = true;

                    DataGridViewRow r = dataGridView1.CurrentRow;
                    if (r != null && dataGridView1.RowCount > 0)
                    {
                        TableRow row = (TableRow)r.Tag;
                        ChartPanel chp = row.chartPanel;
                        ChartRuntime chr;
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
                            dataPeriodCombo.Items.Clear();

                            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                            {
                                if (TimeSeriesRuntime.IsSeriesAvailable(parent.Environment, symbol, v))
                                {
                                    ListItem<TimePeriodConst> li = new ListItem<TimePeriodConst>(v, v.GetLongTxt());
                                    dataPeriodCombo.Items.Add(li);
                                    periodLis[v] = li;
                                }
                            }

                            TestType tt = chr.SymbolRuntime.Session.TestType;
                            methodCombo.Items.Remove(testTypeLis[TestType.NOT_IN_TEST]);
                            methodCombo.SelectedItem = testTypeLis[tt];
                            methodInfLabel.Text = tt.GetDescription();
                            methodCombo.Enabled = false;
                            dataPeriodCombo.Enabled = true;

                            eaInfLabel.Text = chr.Expert.ExpertInfo.ShortTypeName;
                            symbolInfLabel.Text = "" + chr.Symbol;

                            fromInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)chr.Session.From);
                            toInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)chr.Session.To);

                            string tooltip;
                            Image image;
                            setDataPeriodTooltip(chr, out tooltip, out image);
                            if (period != TimePeriodConst.PERIOD_CURRENT)
                            {
                                dataPeriodCombo.SelectedItem = periodLis[period];
                            }
                            else
                            {
                                dataPeriodCombo.SelectedItem = null;
                            }
                            toolTip1.SetToolTip(dataPeriodLabel, tooltip);
                            dataPeriodLabel.Image = image;

                            propertiesButton.Enabled = true;
                        }
                        else
                        {
                            period = testingPeriod;

                            periodLis.Clear();
                            dataPeriodCombo.Items.Clear();
                            foreach (TimePeriodConst v in EnumExtensions.GetPeriods(EnumExtensions.VISIBLE_PERIODS))
                            {
                                if (v != TimePeriodConst.PERIOD_TICK_ASK && v.GetOrdinal() <= testingPeriod0.GetOrdinal() && TimeSeriesRuntime.IsSeriesAvailable(parent.Environment, symbol, v))
                                {
                                    ListItem<TimePeriodConst> li = new ListItem<TimePeriodConst>(v, v.GetLongTxt());
                                    dataPeriodCombo.Items.Add(li);
                                    periodLis[v] = li;
                                }
                            }

                            TestType tt = row.symbolRuntime.Session.TestType;
                            methodCombo.Items.Remove(testTypeLis[TestType.NOT_IN_TEST]);
                            methodCombo.SelectedItem = testTypeLis[tt];
                            methodInfLabel.Text = tt.GetDescription();
                            methodCombo.Enabled = true;
                            dataPeriodCombo.Enabled = true;

                            eaInfLabel.Text = "";
                            symbolInfLabel.Text = "" + symbol;
                            fromInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)row.symbolRuntime.Session.EATestingGlobalFrom);
                            toInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)row.symbolRuntime.Session.EATestingGlobalTo);

                            string tooltip;
                            Image image;
                            image = Resources.empty;
                            tooltip = null;

                            if (period != TimePeriodConst.PERIOD_CURRENT)
                            {
                                dataPeriodCombo.SelectedItem = periodLis[period];
                            }
                            else
                            {
                                dataPeriodCombo.SelectedItem = null;
                            }
                            toolTip1.SetToolTip(dataPeriodLabel, tooltip);
                            dataPeriodLabel.Image = image;

                            propertiesButton.Enabled = false;
                        }

                    }
                    else
                    {
                        methodCombo.Enabled = false;
                        dataPeriodCombo.Enabled = false;
                        dataPeriodLabel.Image = null;
                        toolTip1.SetToolTip(dataPeriodLabel, null);
                        eaInfLabel.Text = "";
                        symbolInfLabel.Text = "";
                        dataPeriodCombo.Text = "";
                        fromInfLabel.Text = "";
                        toInfLabel.Text = "";
                        if (!methodCombo.Items.Contains(testTypeLis[TestType.NOT_IN_TEST]))
                        {
                            methodCombo.Items.Add(testTypeLis[TestType.NOT_IN_TEST]);
                        }
                        methodCombo.SelectedItem = testTypeLis[TestType.NOT_IN_TEST];
                        propertiesButton.Enabled = false;
                    }
                }
            }
            finally
            {
                updatingTableSel = false;
            }
        }

        private void methodCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingTableSel)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                if (r != null && dataGridView1.RowCount > 0)
                {
                    TableRow row = (TableRow)r.Tag;
                    ChartPanel chp = row.chartPanel;

                    if (methodCombo.SelectedItem is ListItem<TestType>)
                    {
                        ListItem<TestType> ltt = (ListItem<TestType>)methodCombo.SelectedItem;
                        if (ltt != null)
                        {
                            row.symbolRuntime.Session.TestType = ltt.Value;
                            methodInfLabel.Text = ltt.Value.GetDescription();

                            SymbolRuntime sr = row.symbolRuntime;
                            if (row.symbolRuntime.Session.DataPeriod == TimePeriodConst.PERIOD_CURRENT)
                            {
                                methodLabel.Image = Resources.Error_red_12x11;
                                toolTip1.SetToolTip(methodLabel, "There is no timeframe period on file");
                            }
                            else if (!TimeSeriesRuntime.IsSeriesAvailable(parent.Environment, sr.Symbol, sr.Session.DataPeriod))
                            {
                                methodLabel.Image = Resources.Warning_yellow_7231_12x11;
                                toolTip1.SetToolTip(methodLabel, "Selected chart period is not on file (" + sr.Session.DataPeriod.GetShortTxt() + ")");
                            }
                            else
                            {
                                methodLabel.Image = null;
                                toolTip1.SetToolTip(methodLabel, null);
                            }
                            parent.SaveSession();

                            UpdateEAsInTest();
                        }
                    }
                }
            }
        }


        private void scrollAcrossTabsCb_CheckedChanged(object sender, EventArgs e)
        {
            parent.Environment.Session.ScrollAcrossCharts = scrollAcrossTabsCb.Checked;
            parent.SaveSession();
        }

        private void skipEmptyPeriodsCb_CheckedChanged(object sender, EventArgs e)
        {
            parent.Environment.Session.SkipEmptyPeriods = skipEmptyPeriodsCb.Checked;
            parent.SaveSession();
        }

        private void updateSpreadTickCb_CheckedChanged(object sender, EventArgs e)
        {
            parent.Environment.Session.UpdateSpreadTick = updateSpreadTickCb.Checked;
            parent.SaveSession();
        }

        private void dataPeriodCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!updatingTableSel)
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                if (r != null)
                {
                    TableRow row = (TableRow)r.Tag;
                    ChartPanel chp = row.chartPanel;
                    if (dataPeriodCombo.SelectedItem is ListItem<TimePeriodConst>)
                    {
                        ListItem<TimePeriodConst> dpt = (ListItem<TimePeriodConst>)dataPeriodCombo.SelectedItem;
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
                                parent.SaveSession();

                                // TODO slightly bad
                                UpdateEAsInTest();
                            }
                        }
                    }
                }
            }
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if (parent.Environment.Session.EAStartStatus.IsRunning())
            {
                startStopButton.Text = "&Start";
                parent.Environment.TestManager.AddRequest(EaTestRequest.STOP_EA_TEST);
                pauseButton.Enabled = false;
                progressTrackBar1.Enabled = false;
            }
            else
            {
                startStopButton.Text = "&Stop";
                parent.Environment.TestManager.AddRequest(EaTestRequest.START_EA_TEST);
                pauseButton.Enabled = true;
                progressTrackBar1.Enabled = true;
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (parent.Environment.Session.EAStartStatus.IsRunning())
            {
                parent.Environment.TestManager.AddRequest(EaTestRequest.PAUSE_EA_TEST);
                if (parent.Environment.Session.EAStartStatus == StartStatus.PAUSED)
                {
                    pauseButton.Text = "&Continue";
                }
                else
                {
                    pauseButton.Text = "&Pause";
                }
            }
        }

        private void speedTrackBar_Scroll(object sender, EventArgs e)
        {
            parent.Environment.Session.EATestingSpeed = speedTrackBar.Value;
        }

        private void progressTrackBar1_TickPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            parent.Environment.Session.EATestingTrackBarTick = progressTrackBar1.TickPosition;
        }

        private void stopAtButton_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void snapButton_Click(object sender, EventArgs e)
        {
            // TODO
        }

        internal class TableRow
        {
            EATesterPanel _;
            internal ChartPanel chartPanel;
            internal ChartRuntime chartRuntime;
            internal SymbolRuntime symbolRuntime;
            internal ExpertRuntime expert;
            internal bool isLast;
            string tooltip;
            Image image;

            DataGridViewRow row;
            DataGridViewCellStyle symbolCellStyle;

            internal TableRow(EATesterPanel _, ChartPanel chartPanel)
            {
                this._ = _;
                this.chartPanel = chartPanel;
                this.chartRuntime = chartPanel.ChartRuntime;
                this.symbolRuntime = this.chartRuntime.SymbolRuntime;
                this.expert = this.chartRuntime.Expert;

                _.setDataPeriodTooltip(chartRuntime, out tooltip, out image);

                row = _.dataGridView1.Rows[_.dataGridView1.Rows.Add(Selected, Title, Period, Icon)];
                row.Tag = this;
            }
            internal TableRow(EATesterPanel _, SymbolRuntime symbolRuntime)
            {
                this._ = _;
                this.symbolRuntime = symbolRuntime;

                image = Resources.empty;
                row = _.dataGridView1.Rows[_.dataGridView1.Rows.Add(Selected, Title, Period, Icon)];
                row.Tag = this;

                symbolCellStyle = new DataGridViewCellStyle();
                symbolCellStyle.Font = new Font(_.dataGridView1.Font, FontStyle.Bold);
                row.Cells[1].Style = symbolCellStyle;
                row.Cells[2].Style = symbolCellStyle;
            }


            //int i0 = dataGridView1.Rows.Add(false, e.Key + " : " + chp0.ChartRuntime.SymbolRuntime.Session.TestType.GetShortText(), "", Resources.empty);
            //int i = dataGridView1.Rows.Add(expert.Parent.Session.AppearsInTest, expert.ExpertInfo.FullName, period.GetShortTxt(), null);
            public bool Selected
            {
                get
                {
                    if (chartPanel == null || expert == null)
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
                    if (chartPanel == null)
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
            public Image Icon
            {
                get
                {
                    return image;
                }
            }
        }

        class TreeNodeWithTextCell : DataGridViewTextBoxCell
        {
            Brush brush;
            Pen bgPen;
            Pen selPen;
            Pen gridPen;

            public TreeNodeWithTextCell()
                : base()
            {
            }

            protected override void Paint(
                Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates cellState,
                Object value,
                Object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts baseParts
            )
            {
                if (brush == null)
                {
                    brush = new SolidBrush(cellStyle.ForeColor);
                    bgPen = new Pen(cellStyle.BackColor);
                    selPen = new Pen(cellStyle.SelectionBackColor);
                    gridPen = new Pen(DataGridView.GridColor);
                }

                EATesterPanel eaPanel = (EATesterPanel)DataGridView.Parent;
                TableRow row = (TableRow)DataGridView.Rows[rowIndex].Tag;

                DataGridViewPaintParts parts = DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.Focus;
                //DataGridViewPaintParts parts = DataGridViewPaintParts.All;

                if (DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.None)
                {
                    Pen pen1 = row.isLast ? gridPen : cellState == DataGridViewElementStates.Selected ? selPen : bgPen;
                    Pen pen2 = gridPen;
                    graphics.DrawLine(pen1, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right - 1, cellBounds.Bottom - 1);
                    graphics.DrawLine(pen2, cellBounds.Right - 1, 0, cellBounds.Right - 1, cellBounds.Bottom - 1);
                }

                if (row.chartPanel == null)
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);
                }
                else if (!row.isLast)
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "     " + formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);

                    graphics.DrawImage(Resources.treenode_mid_20, cellBounds.X + 5, cellBounds.Y, 11, 20);
                }
                else
                {
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, "     " + formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);

                    graphics.DrawImage(Resources.treenode_bot_20, cellBounds.X + 5, cellBounds.Y, 11, 20);
                }
            }
        }
        class CheckBoxCell0 : DataGridViewCheckBoxCell
        {
            public CheckBoxCell0()
                : base()
            {
            }
            protected override void Paint(
                Graphics graphics,
                Rectangle clipBounds,
                Rectangle cellBounds,
                int rowIndex,
                DataGridViewElementStates cellState,
                Object value,
                Object formattedValue,
                string errorText,
                DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts baseParts
            )
            {
                EATesterPanel eaPanel = (EATesterPanel)DataGridView.Parent;
                TableRow row = (TableRow)DataGridView.Rows[rowIndex].Tag;
                DataGridViewPaintParts parts;
                if (DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.None)
                {
                    parts = DataGridViewPaintParts.Border;
                }
                else
                {
                    parts = 0;
                }

                if (row.chartPanel != null)
                {
                    parts |= DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.SelectionBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.Focus;
                }
                else
                {
                    parts |= DataGridViewPaintParts.Background | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.SelectionBackground;
                }

                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, parts & baseParts);
            }
        }
    }

}
