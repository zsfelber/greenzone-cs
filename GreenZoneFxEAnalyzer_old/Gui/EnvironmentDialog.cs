using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.Gui.PropertyGrid;

namespace GreenZoneFxEngine
{
    public partial class EnvironmentDialog : Form
    {
        EnvironmentRuntime envinronment;

        public EnvironmentDialog(EnvironmentRuntime envinronment)
        {
            this.envinronment = envinronment;
            InitializeComponent();

            TreeNode node0 = treeView1.Nodes.Add("Options");
            node0.Expand();

            foreach (string envId in EnvironmentRuntimeRoot.Singleton.Environments)
            {
                var env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envId);

                TreeNode node = node0.Nodes.Add(env.EnvironmentId);
                if (env == envinronment)
                {
                    treeView1.SelectedNode = node;
                }

                foreach (var symbol in env.Symbols)
                {
                    TreeNode child = node.Nodes.Add(symbol.ToString());
                }

                TreeNode scripts = node.Nodes.Add("Scripts");
                TreeNode eas = node.Nodes.Add("Expert Advisors");
                TreeNode indicators = node.Nodes.Add("Indicators");

                foreach (var script in env.Scripts)
                {
                    TreeNode child = scripts.Nodes.Add(script);
                }

                foreach (var ea in env.Experts)
                {
                    TreeNode child = eas.Nodes.Add(ea);
                }

                foreach (var indicator in env.Indicators)
                {
                    TreeNode child = indicators.Nodes.Add(indicator);
                }

                node.Expand();
            }
        }

        TreeNode Current
        {
            get;
            set;
        }

        BufferedPropertyGrid CurrentPanel
        {
            get;
            set;
        }

        void Save()
        {
            if (Current == null)
            {
                throw new NotSupportedException();
            }

            switch (Current.Level)
            {
            case 0:
                EAnalyzerOptions.Singleton.Save();
                break;
            default:
                TreeNode n = Current;
                while (n.Level != 1)
                {
                    n = n.Parent;
                }
                EnvironmentRuntime environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(n.Text);
                envinronment.Save();
                break;
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (CurrentPanel != null && Current != e.Node && CurrentPanel.IsChanged)
            {
                //CurrentPanel.
                DialogResult r;
                if (Current.Level == 0)
                {
                    r = MessageBox.Show("Application should restart, because you changed startup options. Would you like to save changes and restart?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                else
                {
                    r = MessageBox.Show("Would you like to save changes made on " + Current.Text + "?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                switch (r)
                {
                    case DialogResult.Yes:
                        CurrentPanel.Save();
                        Save();
                        if (Current.Level == 0)
                        {
                            Application.Restart();
                            return;
                        }
                        break;
                    case DialogResult.Cancel: e.Cancel = true; return;
                    default: break;
                }

            }

            Current = e.Node;
            TreeNode parentNode;
            EnvironmentRuntime environment;
            switch (Current.Level)
            {
                case 0:
                    splitContainer1.Panel2.Controls.Clear();
                    OptionsPanel optPanel = new OptionsPanel();
                    CurrentPanel = optPanel.propertyGrid1;
                    optPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                    optPanel.Location = new System.Drawing.Point(0, 0);
                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(optPanel);
                    break;

                case 1:
                    environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(Current.Text);

                    EnvironmentPanel envPanel = new EnvironmentPanel(environment);
                    CurrentPanel = envPanel.propertyGrid1;
                    envPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                    envPanel.Location = new System.Drawing.Point(0, 0);
                    splitContainer1.Panel2.Controls.Clear();
                    splitContainer1.Panel2.Controls.Add(envPanel);
                    break;

                case 2:
                    parentNode = Current.Parent;

                    if (Current.Text.Equals("Scripts"))
                    {
                        splitContainer1.Panel2.Controls.Clear();
                        CurrentPanel = null;
                    }
                    else if (Current.Text.Equals("Expert Advisors"))
                    {
                        splitContainer1.Panel2.Controls.Clear();
                        CurrentPanel = null;
                    }
                    else if (Current.Text.Equals("Indicators"))
                    {
                        splitContainer1.Panel2.Controls.Clear();
                        CurrentPanel = null;
                    }
                    else
                    {
                        environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(parentNode.Text);
                        SymbolContext symbolContext = environment.GetSymbolContext(Current.Text);

                        SymbolPanel symbolPanel = new SymbolPanel(symbolContext);
                        CurrentPanel = symbolPanel.propertyGrid1;
                        symbolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                        symbolPanel.Location = new System.Drawing.Point(0, 0);
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(symbolPanel);
                    }
                    break;

                case 3:
                    parentNode = Current.Parent;
                    TreeNode envNode = parentNode.Parent;
                    environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envNode.Text);

                    if (parentNode.Text.Equals("Scripts"))
                    {
                        Mt4ScriptInfo scriptInfo = environment.GetScriptInfo(Current.Text);

                        Mt4ScriptInfoPanel scriptInfoPanel = new Mt4ScriptInfoPanel(scriptInfo);
                        CurrentPanel = scriptInfoPanel.propertyGrid1;
                        scriptInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                        scriptInfoPanel.Location = new System.Drawing.Point(0, 0);
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(scriptInfoPanel);
                    }
                    else if (parentNode.Text.Equals("Expert Advisors"))
                    {
                        Mt4ExpertInfo eaInfo = environment.GetExpertInfo(Current.Text);

                        Mt4ExpertInfoPanel eaInfoPanel = new Mt4ExpertInfoPanel(eaInfo);
                        CurrentPanel = eaInfoPanel.propertyGrid1;
                        eaInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                        eaInfoPanel.Location = new System.Drawing.Point(0, 0);
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(eaInfoPanel);
                    }
                    else if (parentNode.Text.Equals("Indicators"))
                    {
                        Mt4IndicatorInfo indicatorInfo = environment.GetIndicatorInfo(Current.Text);

                        Mt4IndicatorInfoPanel indicatorInfoPanel = new Mt4IndicatorInfoPanel(indicatorInfo);
                        CurrentPanel = indicatorInfoPanel.propertyGrid1;
                        indicatorInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                        indicatorInfoPanel.Location = new System.Drawing.Point(0, 0);
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(indicatorInfoPanel);
                    }
                    break;
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (CurrentPanel != null)
            {
                if (Current.Level == 0 && CurrentPanel.IsChanged)
                {
                    CurrentPanel.Save();
                    Save();
                    Close();
                    MessageBox.Show("Application should restart, because you changed startup options.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                    return;
                }
                else
                {
                    CurrentPanel.Save();
                    Save();
                }
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (CurrentPanel != null)
            {
                CurrentPanel.Reset();
            }
        }

    }
}
