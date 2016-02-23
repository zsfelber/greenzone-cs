using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    public class EnvironmentSettingsController : EnvironmentSettingsControllerBase
    {
        internal EnvironmentSettingsController(GreenRmiManager rmiManager, ServerEnvironmentRuntime environment)
            : base(rmiManager)
        {
            this.environment = environment;

            SplitContainer1 = new SplitController(rmiManager, this);
            TreeView1 = new TreeController(rmiManager, SplitContainer1.Panel1);
            SaveButton = new ButtonController(rmiManager, this);
            CancelButton = new ButtonController(rmiManager, this);
            ResetButton = new ButtonController(rmiManager, this);


            TreeNodeController node0 = new TreeNodeController(rmiManager, TreeView1, "Options");
            node0.Expand();

            foreach (string envId in EnvironmentRuntimeRoot.Singleton.Environments)
            {
                var env = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envId);

                TreeNodeController node = new TreeNodeController(rmiManager, node0, env.EnvironmentId);
                if (env == environment)
                {
                    TreeView1.SelectedNode = node;
                }

                foreach (var symbol in env.Symbols)
                {
                    TreeNodeController child = new TreeNodeController(rmiManager, node, symbol.ToString());
                }

                TreeNodeController scripts = new TreeNodeController(rmiManager, node, "Scripts");
                TreeNodeController eas = new TreeNodeController(rmiManager, node, "Expert Advisors");
                TreeNodeController indicators = new TreeNodeController(rmiManager, node, "Indicators");

                foreach (var script in env.Scripts)
                {
                    TreeNodeController child = new TreeNodeController(rmiManager, scripts, script);
                }

                foreach (var ea in env.Experts)
                {
                    TreeNodeController child = new TreeNodeController(rmiManager, eas, ea);
                }

                foreach (var indicator in env.Indicators)
                {
                    TreeNodeController child = new TreeNodeController(rmiManager, indicators, indicator);
                }

                node.Expand();
            }

            SaveButton.Pressed += new ControllerEventHandler(saveButton_Click);
            //CancelButton.Pressed += new EventHandler(cancelButton_Click);
            ResetButton.Pressed += new ControllerEventHandler(resetButton_Click);
            TreeView1.SelectedNodeChanging += new PropertyChangingEventHandlerC(treeView1_SelectedNodeChanging);
        }


        ServerEnvironmentRuntime environment;
        public ServerEnvironmentRuntime Environment
        {
            get
            {
                return environment;
            }
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
                    TreeNodeController n = Current;
                    while (n.Level != 1)
                    {
                        n = n.Parent;
                    }
                    ServerEnvironmentRuntime environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(n.Text);
                    environment.Save();
                    break;
            }
        }

        void treeView1_SelectedNodeChanging(object sender, PropertyChangingEventArgsC e)
        {
            if (CurrentPanel != null && Current != sender && CurrentPanel.IsChanged)
            {
                //CurrentPanel.
                DialogResult r;
                if (Current.Level == 0)
                {
                    r = MessageBoxController.Show(rmiManager, "Application should restart, because you changed startup options. Would you like to save changes and restart?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                else
                {
                    r = MessageBoxController.Show(rmiManager, "Would you like to save changes made on " + Current.Text + "?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

            Current = (TreeNodeController)sender;
            TreeNodeController parentNode;
            ServerEnvironmentRuntime environment;
            switch (Current.Level)
            {
                case 0:
                    SplitContainer1.Panel2.Clear();
                    ProgramOptionsController optPanel = new ProgramOptionsController(rmiManager, this);
                    CurrentPanel = optPanel.PropertyGrid1;
                    SplitContainer1.Panel2.Clear();
                    SplitContainer1.Panel2.Add(optPanel);
                    break;

                case 1:
                    environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(Current.Text);

                    EnvironmentPropsController envPanel = new EnvironmentPropsController(rmiManager, this, environment);
                    CurrentPanel = envPanel.PropertyGrid1;
                    SplitContainer1.Panel2.Clear();
                    SplitContainer1.Panel2.Add(envPanel);
                    break;

                case 2:
                    parentNode = Current.Parent;

                    if (Current.Text.Equals("Scripts"))
                    {
                        SplitContainer1.Panel2.Clear();
                        CurrentPanel = null;
                    }
                    else if (Current.Text.Equals("Expert Advisors"))
                    {
                        SplitContainer1.Panel2.Clear();
                        CurrentPanel = null;
                    }
                    else if (Current.Text.Equals("Indicators"))
                    {
                        SplitContainer1.Panel2.Clear();
                        CurrentPanel = null;
                    }
                    else
                    {
                        environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(parentNode.Text);
                        ServerSymbolContext symbolContext = environment.GetSymbolContext(Current.Text);

                        SymbolPropertiesController symbolPanel = new SymbolPropertiesController(rmiManager, this, symbolContext);
                        CurrentPanel = symbolPanel.PropertyGrid1;
                        SplitContainer1.Panel2.Clear();
                        SplitContainer1.Panel2.Add(symbolPanel);
                    }
                    break;

                case 3:
                    parentNode = Current.Parent;
                    TreeNodeController envNode = parentNode.Parent;
                    environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(envNode.Text);

                    if (parentNode.Text.Equals("Scripts"))
                    {
                        Mt4ExecutableInfo scriptInfo = environment.GetScriptInfo(Current.Text);

                        ScriptInfoController scriptInfoController = new ScriptInfoController(rmiManager, this, scriptInfo);
                        CurrentPanel = scriptInfoController.PropertyGrid1;
                        SplitContainer1.Panel2.Clear();
                        SplitContainer1.Panel2.Add(scriptInfoController);
                    }
                    else if (parentNode.Text.Equals("Expert Advisors"))
                    {
                        Mt4ExecutableInfo eaInfo = environment.GetExpertInfo(Current.Text);

                        ExpertInfoController eaInfoController = new ExpertInfoController(rmiManager, this, eaInfo);
                        CurrentPanel = eaInfoController.PropertyGrid1;
                        SplitContainer1.Panel2.Clear();
                        SplitContainer1.Panel2.Add(eaInfoController);
                    }
                    else if (parentNode.Text.Equals("Indicators"))
                    {
                        Mt4ExecutableInfo indicatorInfo = environment.GetIndicatorInfo(Current.Text);

                        IndicatorInfoController indicatorInfoController = new IndicatorInfoController(rmiManager, this, indicatorInfo);
                        CurrentPanel = indicatorInfoController.PropertyGrid1;
                        SplitContainer1.Panel2.Clear();
                        SplitContainer1.Panel2.Add(indicatorInfoController);
                    }
                    break;
            }

        }

        private void saveButton_Click(object sender, ControllerEventArgs e)
        {
            if (CurrentPanel != null)
            {
                if (Current.Level == 0 && CurrentPanel.IsChanged)
                {
                    CurrentPanel.Save();
                    Save();
                    ForceClose();
                    MessageBoxController.Show(rmiManager, "Application should restart, because you changed startup options.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void resetButton_Click(object sender, ControllerEventArgs e)
        {
            if (CurrentPanel != null)
            {
                CurrentPanel.Reset();
            }
        }
    }
}
