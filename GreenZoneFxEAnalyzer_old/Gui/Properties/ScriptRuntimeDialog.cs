using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Etc;

namespace GreenZoneFxEngine
{
    public partial class ScriptRuntimeDialog : Form
    {
        public ScriptRuntimeDialog(ScriptRuntime scriptRuntime)
        {
            InitializeComponent();
            scriptRuntimePanel.Set(scriptRuntime);
            Text = scriptRuntime.ScriptInfo.FullName;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            scriptRuntimePanel.Save();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scriptRuntimePanel.Reset();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            ScriptRuntime r = scriptRuntimePanel.EditedExecRuntime;
            EngineUtils.ShowLoadFromSetDialog(r);
            scriptRuntimePanel.EditedExecRuntime = r;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EngineUtils.ShowSaveToSetDialog(scriptRuntimePanel.EditedExecRuntime);
        }

    }
}
