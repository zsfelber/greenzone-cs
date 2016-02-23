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
    public partial class ExpertRuntimeDialog : Form
    {
        public ExpertRuntimeDialog(ExpertRuntime expertRuntime)
        {
            InitializeComponent();
            expertRuntimePanel.Set(expertRuntime);
            Text = expertRuntime.ExpertInfo.FullName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            expertRuntimePanel.Save();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            expertRuntimePanel.Reset();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            ExpertRuntime r = expertRuntimePanel.EditedExecRuntime;
            EngineUtils.ShowLoadFromSetDialog(r);
            expertRuntimePanel.EditedExecRuntime = r;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EngineUtils.ShowSaveToSetDialog(expertRuntimePanel.EditedExecRuntime);
        }

    }
}
