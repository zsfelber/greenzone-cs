using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EnvironmentAssistant
{
	public class EnvironmentAssistantForm : AssistantForm
	{

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new EnvironmentAssistantForm());
        }
        
        public EnvironmentAssistantForm()
		{
		    InitializeComponent();
		    Controls.AddRange( new Control[] {
		        new StartPage(),
		        new SelectEnvTypePage(),
		        new ImportMetatraderPage1(),
		        new ImportMetatraderPage2(),
		        new DukascopyPage0(),
		        new DukascopyPage1(),
		        new DukascopyPage2(),
		        new DukascopyPage3(),
		        new FinishPage()
		        } );
        }

        public EnvironmentAssistantForm(string updatedEnvironment, string updatedEnvironmentDir, string updatedEnvironmentType, string updatedEnvironmentHistoryDir, string[] updatedEnvironmentData, ISet<string> environments)
            : this()
		{
            UpdatedEnvironment = updatedEnvironment;
            UpdatedEnvironmentDir = updatedEnvironmentDir;
            UpdatedEnvironmentType = updatedEnvironmentType;
            UpdatedEnvironmentData = updatedEnvironmentData;
            UpdatedEnvironmentHistoryDir = updatedEnvironmentHistoryDir;
            Environments = environments;
        }

        public string UpdatedEnvironment
        {
            get;
            private set;
        }

        public string UpdatedEnvironmentDir
        {
            get;
            private set;
        }

        public string UpdatedEnvironmentType
        {
            get;
            private set;
        }

        public string UpdatedEnvironmentHistoryDir
        {
            get;
            private set;
        }

        public ISet<string> Environments
        {
            get;
            private set;
        }

        public string[] UpdatedEnvironmentData
        {
            get;
            private set;
        }


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            // EnvironmentAssistantForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(499, 393);
            this.Name = "EnvironmentAssistantForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environment Assistant";
            this.ResumeLayout(false);

        }
		#endregion

    }
}

