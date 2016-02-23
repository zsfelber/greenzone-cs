using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Etc;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class ScriptDialogController : ScriptDialogControllerBase
    {
        internal ScriptDialogController(GreenRmiManager rmiManager, ServerScriptRuntime scriptRuntime)
            : base(rmiManager)
        {
            this.scriptRuntime = scriptRuntime;
            ScriptRuntimePanel = new ScriptPropertiesController(rmiManager, this, scriptRuntime);

            OkButton = new ButtonController(rmiManager, this);
            _CancelButton = new ButtonController(rmiManager, this);
            ResetButton = new ButtonController(rmiManager, this);
            LoadButton = new ButtonController(rmiManager, this);
            SaveButton = new ButtonController(rmiManager, this);

            AcceptButton = OkButton;
            CancelButton = _CancelButton;

            OkButton.Pressed += new ControllerEventHandler(okButton_Click);
            _CancelButton.Pressed += new ControllerEventHandler(cancelButton_Click);
            ResetButton.Pressed += new ControllerEventHandler(resetButton_Click);
            LoadButton.Pressed += new ControllerEventHandler(loadButton_Click);
            SaveButton.Pressed += new ControllerEventHandler(saveButton_Click);
        }

        readonly ServerScriptRuntime scriptRuntime;
        public ServerScriptRuntime ScriptRuntime
        {
            get
            {
                return scriptRuntime;
            }
        }

        public new ScriptPropertiesController ScriptRuntimePanel
        {
            get
            {
                return (ScriptPropertiesController)base.ScriptRuntimePanel;
            }
            protected set
            {
                base.ScriptRuntimePanel = value;
            }
        }

        private void okButton_Click(object sender, ControllerEventArgs e)
        {
            ScriptRuntimePanel.Save();
            DialogResult = DialogResult.OK;
            Closing();
        }

        private void cancelButton_Click(object sender, ControllerEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void resetButton_Click(object sender, ControllerEventArgs e)
        {
            ScriptRuntimePanel.Reset();
        }

        private void loadButton_Click(object sender, ControllerEventArgs e)
        {
            ServerScriptRuntime r = ScriptRuntimePanel.EditedExecRuntime;
            EngineUtils.ShowLoadFromSetController(rmiManager, r);
            ScriptRuntimePanel.EditedExecRuntime = r;
        }

        private void saveButton_Click(object sender, ControllerEventArgs e)
        {
            EngineUtils.ShowSaveToSetController(rmiManager, ScriptRuntimePanel.EditedExecRuntime);
        }
    }
}
