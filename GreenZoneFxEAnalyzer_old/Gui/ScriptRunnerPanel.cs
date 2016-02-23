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
using GreenZoneFxEngine;
using GreenZoneUtil.Util;

namespace GreenZoneFxEngine
{
    public partial class ScriptRunnerPanel : UserControl
    {
        Form1 parent;
        private readonly List<ChartPanel> scriptChartPanels = new List<ChartPanel>();
        private readonly IList<ChartPanel> scriptChartPanels_ro;
        private readonly Dictionary<TestType,ListItem<TestType>> testTypeLis = new Dictionary<TestType,ListItem<TestType>>();

        public ScriptRunnerPanel()
        {
            scriptChartPanels_ro = scriptChartPanels.AsReadOnly();
            InitializeComponent();
        }

        public ChartPanel SelectedChart
        {
            get
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                ChartPanel chp = null;
                if (r != null)
                {
                    chp = scriptChartPanels[r.Index];
                }
                return chp;
            }
        }

        public void Init(Form1 parent)
        {
            this.parent = parent;
        }

        internal IList<ChartPanel> ScriptChartPanels
        {
            get
            {
                return scriptChartPanels_ro;
            }
        }

        internal void UpdateScriptsInTest()
        {
            int si = -1;
            DataGridViewRow r = dataGridView1.CurrentRow;
            if (r != null)
            {
                si = r.Index;
            }

            dataGridView1.Rows.Clear();
            scriptChartPanels.Clear();
            foreach (IForm1TabPanel tp in parent.TabPanels)
            {
                if (tp is ChartGroupPanel)
                {
                    ChartGroupPanel chg = (ChartGroupPanel)tp;
                    foreach (ChartPanel chp in chg.ChartPanels)
                    {
                        ChartRuntime ch = chp.ChartRuntime;
                        if (ch.Script != null)
                        {
                            ScriptRuntime e = ch.Script;
                            scriptChartPanels.Add(chp);
                            dataGridView1.Rows.Add(e.ScriptInfo.ShortTypeName, e.Parent.Symbol, e.Parent.Period.GetShortTxt());
                        }
                    }
                }
            }

            if (si >= dataGridView1.Rows.Count)
            {
                si = dataGridView1.Rows.Count - 1;
            }

            if (si > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[si].Cells[0];
            }

            // TODO slightly bad
            dataGridView1_SelectionChanged(null, null);
        }

        private void Start()
        {
            startStopButton.Text = "&Stop";
        }

        private void Stop()
        {
            startStopButton.Text = "&Start";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ChartPanel chp = SelectedChart;
            if (chp != null)
            {
                parent.ChartSelectedInPanel(chp.ChartGroupPanel);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            parent.AddScriptClickedInPanel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1_DoubleClick(sender, e);

            ChartPanel chp = SelectedChart;
            if (chp != null)
            {
                ChartRuntime ch = chp.ChartRuntime;

                ScriptRuntimeDialog d = new ScriptRuntimeDialog(ch.Script);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ChartPanel chp = SelectedChart;
            if (chp != null)
            {
                ChartRuntime chr = chp.ChartRuntime;
                if (chr.Script != null)
                {
                    scriptInfLabel.Text = chr.Script.ScriptInfo.ShortTypeName;
                    symbolInfLabel.Text = ""+chr.Symbol;
                    periodInfLabel.Text = chr.Period.GetLongTxt();

                    fromInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)chr.Session.From);
                    toInfLabel.Text = GreenZoneUtils.FormatDateTime((DateTime)chr.Session.To);
                }
                else
                {
                    scriptInfLabel.Text = "";
                    symbolInfLabel.Text = "";
                    periodInfLabel.Text = "";
                    fromInfLabel.Text = "";
                    toInfLabel.Text = "";
                }
            }
            else
            {
                scriptInfLabel.Text = "";
                symbolInfLabel.Text = "";
                periodInfLabel.Text = "";
                fromInfLabel.Text = "";
                toInfLabel.Text = "";
            }
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            ChartPanel chp = SelectedChart;
            if (chp != null)
            {
                ChartRuntime ch = chp.ChartRuntime;
                switch (ch.Session.ScriptStartStatus)
                {
                    case StartStatus.NOT_RUNNING:
                        Start();
                        return;
                    case StartStatus.STARTED:
                    case StartStatus.PAUSED:
                        Stop();
                        return;
                    default :
                        throw new NotSupportedException("" + ch.Session.ScriptStartStatus);
                }
            }
        }


    }
}
