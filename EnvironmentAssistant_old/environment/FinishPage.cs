using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace EnvironmentAssistant
{
	public class FinishPage : ExteriorAssistantPage
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components = null;

		public FinishPage()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

        new internal EnvironmentAssistantForm ParentForm
        {
            get
            {
                return (EnvironmentAssistantForm)base.ParentForm;
            }
        }
        
        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_watermarkPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.Text = "Almost Finished with the Sample Setup Assistant";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(170, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Back to Wizard 97 style.  The final page also extends ExteriorAssistantPage but w" +
                "ithout a watermark image.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(170, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Click the checkbox below to enable the Finish button.";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(170, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(292, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Click Finish to close this wizard.";
            // 
            // FinishPage
            // 
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FinishPage";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.m_titleLabel, 0);
            this.Controls.SetChildIndex(this.m_watermarkPicture, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_watermarkPicture)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        protected override bool OnSetActive()
        {
            if( !base.OnSetActive() )
                return false;
            
            // Enable both the Back and Finish (enabled/disabled) buttons on this page    
            Assistant.SetAssistantButtons( AssistantButton.Back | AssistantButton.Finish );

            return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

