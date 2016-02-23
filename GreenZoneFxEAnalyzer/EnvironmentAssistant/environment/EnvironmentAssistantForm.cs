using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.Gui.ViewController;

namespace EnvironmentAssistant
{
	public class EnvironmentAssistantForm : AssistantForm
	{
        private DukascopyPage0 dukascopyPage01;
        private DukascopyPage1 dukascopyPage11;
        private DukascopyPage3 dukascopyPage31;
        private ImportMetatraderPage1 importMetatraderPage11;
        private ImportMetatraderPage2 importMetatraderPage21;
        private SelectEnvTypePage selectEnvTypePage1;
        private StartPage startPage1;
        private DukascopyPage2 dukascopyPage21;
    
        public EnvironmentAssistantForm()
		{
		    InitializeComponent();
        }

        public void Bind(WinFormsMVContext context, EnvironmentAssistantController controller)
        {
            base.Bind(context, controller);
            this.dukascopyPage01.Bind(context, controller.DukascopyPage0Controller);
            this.dukascopyPage11.Bind(context, controller.DukascopyPage1Controller);
            this.dukascopyPage21.Bind(context, controller.DukascopyPage2Controller);
            this.dukascopyPage31.Bind(context, controller.DukascopyPage3Controller);
            this.importMetatraderPage11.Bind(context, controller.ImportMetatraderPage1Controller);
            this.importMetatraderPage21.Bind(context, controller.ImportMetatraderPage2Controller);
            this.selectEnvTypePage1.Bind(context, controller.SelectEnvTypePageController);
            this.startPage1.Bind(context, controller.StartPageController);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startPage1 = new EnvironmentAssistant.StartPage();
            this.selectEnvTypePage1 = new EnvironmentAssistant.SelectEnvTypePage();
            this.importMetatraderPage21 = new EnvironmentAssistant.ImportMetatraderPage2();
            this.importMetatraderPage11 = new EnvironmentAssistant.ImportMetatraderPage1();
            this.dukascopyPage31 = new EnvironmentAssistant.DukascopyPage3();
            this.dukascopyPage21 = new EnvironmentAssistant.DukascopyPage2();
            this.dukascopyPage11 = new EnvironmentAssistant.DukascopyPage1();
            this.dukascopyPage01 = new EnvironmentAssistant.DukascopyPage0();
            this.SuspendLayout();
            // 
            // m_backButton
            // 
            this.m_backButton.Location = new System.Drawing.Point(252, 363);
            // 
            // m_nextButton
            // 
            this.m_nextButton.Location = new System.Drawing.Point(327, 363);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Location = new System.Drawing.Point(412, 363);
            // 
            // m_finishButton
            // 
            this.m_finishButton.Location = new System.Drawing.Point(327, 363);
            // 
            // m_separator
            // 
            this.m_separator.Location = new System.Drawing.Point(0, 353);
            // 
            // startPage1
            // 
            this.startPage1.BackColor = System.Drawing.Color.White;
            this.startPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPage1.Location = new System.Drawing.Point(0, 0);
            this.startPage1.Name = "startPage1";
            this.startPage1.Size = new System.Drawing.Size(508, 393);
            this.startPage1.TabIndex = 19;
            // 
            // selectEnvTypePage1
            // 
            this.selectEnvTypePage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectEnvTypePage1.Location = new System.Drawing.Point(0, 0);
            this.selectEnvTypePage1.Name = "selectEnvTypePage1";
            this.selectEnvTypePage1.Size = new System.Drawing.Size(508, 393);
            this.selectEnvTypePage1.TabIndex = 18;
            // 
            // importMetatraderPage21
            // 
            this.importMetatraderPage21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importMetatraderPage21.Location = new System.Drawing.Point(0, 0);
            this.importMetatraderPage21.Name = "importMetatraderPage21";
            this.importMetatraderPage21.Size = new System.Drawing.Size(508, 393);
            this.importMetatraderPage21.TabIndex = 17;
            // 
            // importMetatraderPage11
            // 
            this.importMetatraderPage11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importMetatraderPage11.Location = new System.Drawing.Point(0, 0);
            this.importMetatraderPage11.Name = "importMetatraderPage11";
            this.importMetatraderPage11.Size = new System.Drawing.Size(508, 393);
            this.importMetatraderPage11.TabIndex = 16;
            // 
            // dukascopyPage31
            // 
            this.dukascopyPage31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dukascopyPage31.Location = new System.Drawing.Point(0, 0);
            this.dukascopyPage31.Name = "dukascopyPage31";
            this.dukascopyPage31.Size = new System.Drawing.Size(508, 393);
            this.dukascopyPage31.TabIndex = 15;
            // 
            // dukascopyPage21
            // 
            this.dukascopyPage21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dukascopyPage21.Location = new System.Drawing.Point(0, 0);
            this.dukascopyPage21.Name = "dukascopyPage21";
            this.dukascopyPage21.Size = new System.Drawing.Size(508, 393);
            this.dukascopyPage21.TabIndex = 14;
            // 
            // dukascopyPage11
            // 
            this.dukascopyPage11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dukascopyPage11.Location = new System.Drawing.Point(0, 0);
            this.dukascopyPage11.Name = "dukascopyPage11";
            this.dukascopyPage11.Size = new System.Drawing.Size(508, 393);
            this.dukascopyPage11.TabIndex = 13;
            // 
            // dukascopyPage01
            // 
            this.dukascopyPage01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dukascopyPage01.Location = new System.Drawing.Point(0, 0);
            this.dukascopyPage01.Name = "dukascopyPage01";
            this.dukascopyPage01.Size = new System.Drawing.Size(508, 393);
            this.dukascopyPage01.TabIndex = 12;
            // 
            // EnvironmentAssistantForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 393);
            this.Controls.Add(this.startPage1);
            this.Controls.Add(this.selectEnvTypePage1);
            this.Controls.Add(this.importMetatraderPage21);
            this.Controls.Add(this.importMetatraderPage11);
            this.Controls.Add(this.dukascopyPage31);
            this.Controls.Add(this.dukascopyPage21);
            this.Controls.Add(this.dukascopyPage11);
            this.Controls.Add(this.dukascopyPage01);
            this.Name = "EnvironmentAssistantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environment Assistant";
            this.Controls.SetChildIndex(this.dukascopyPage01, 0);
            this.Controls.SetChildIndex(this.dukascopyPage11, 0);
            this.Controls.SetChildIndex(this.dukascopyPage21, 0);
            this.Controls.SetChildIndex(this.dukascopyPage31, 0);
            this.Controls.SetChildIndex(this.importMetatraderPage11, 0);
            this.Controls.SetChildIndex(this.importMetatraderPage21, 0);
            this.Controls.SetChildIndex(this.selectEnvTypePage1, 0);
            this.Controls.SetChildIndex(this.startPage1, 0);
            this.Controls.SetChildIndex(this.m_separator, 0);
            this.Controls.SetChildIndex(this.m_finishButton, 0);
            this.Controls.SetChildIndex(this.m_cancelButton, 0);
            this.Controls.SetChildIndex(this.m_nextButton, 0);
            this.Controls.SetChildIndex(this.m_backButton, 0);
            this.ResumeLayout(false);

        }
		#endregion

    }
}

