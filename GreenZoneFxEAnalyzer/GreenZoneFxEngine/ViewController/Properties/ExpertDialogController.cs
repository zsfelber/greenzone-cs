using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneFxEngine.Etc;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    public class ExpertDialogController : ExpertDialogControllerBase
    {
        internal ExpertDialogController(GreenRmiManager rmiManager, ServerExpertRuntime expertRuntime)
            : base(rmiManager)
        {
            this.expertRuntime = expertRuntime;
            ExpertRuntimePanel = new ExpertPropertiesController(rmiManager, this, expertRuntime);

            OkButton = new ButtonController(rmiManager, this);
            _CancelButton = new ButtonController(rmiManager, this);
            ResetButton = new ButtonController(rmiManager, this);
            LoadButton = new ButtonController(rmiManager, this);
            SaveButton = new ButtonController(rmiManager, this);

            AcceptButton = OkButton;
            CancelButton = _CancelButton;

            OkButton.Pressed += new ControllerEventHandler(okButton_Click);
            CancelButton.Pressed += new ControllerEventHandler(cancelButton_Click);
            ResetButton.Pressed += new ControllerEventHandler(resetButton_Click);
            LoadButton.Pressed += new ControllerEventHandler(loadButton_Click);
            SaveButton.Pressed += new ControllerEventHandler(saveButton_Click);
        }

        readonly ServerExpertRuntime expertRuntime;
        public ServerExpertRuntime ExpertRuntime
        {
            get
            {
                return expertRuntime;
            }
        }

        public new ExpertPropertiesController ExpertRuntimePanel
        {
            get
            {
                return (ExpertPropertiesController)base.ExpertRuntimePanel;
            }
            protected set
            {
                base.ExpertRuntimePanel = value;
            }
        }

        private void okButton_Click(object sender, ControllerEventArgs e)
        {
            ExpertRuntimePanel.Save();
            DialogResult = DialogResult.OK;
            Closing();
        }

        private void cancelButton_Click(object sender, ControllerEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void resetButton_Click(object sender, ControllerEventArgs e)
        {
            ExpertRuntimePanel.Reset();
        }

        private void loadButton_Click(object sender, ControllerEventArgs e)
        {
            ServerExpertRuntime r = ExpertRuntimePanel.EditedExecRuntime;
            EngineUtils.ShowLoadFromSetController(rmiManager, r);
            ExpertRuntimePanel.EditedExecRuntime = r;
        }

        private void saveButton_Click(object sender, ControllerEventArgs e)
        {
            EngineUtils.ShowSaveToSetController(rmiManager, ExpertRuntimePanel.EditedExecRuntime);
        }
    }
}
