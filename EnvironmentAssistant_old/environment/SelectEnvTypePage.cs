using SMS.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace EnvironmentAssistant
{
	public class SelectEnvTypePage : InteriorAssistantPage
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private RadioButton radioButton3;
        private GroupBox groupBox1;
		private System.ComponentModel.IContainer components = null;

		public SelectEnvTypePage()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectEnvTypePage));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_titleLabel
            // 
            this.m_titleLabel.Text = "First Interior Page";
            // 
            // m_subtitleLabel
            // 
            this.m_subtitleLabel.Text = "The first interior page subtitle will help a user complete a certain task in the " +
                "Sample wizard by clarifying the task in some way.";
            // 
            // m_headerPicture
            // 
            this.m_headerPicture.Image = ((System.Drawing.Image)(resources.GetObject("m_headerPicture.Image")));
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(41, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lorem ipsum  Lorem ipsum  Lorem ipsum  Lorem ipsum  Lorem ipsum  Lorem ipsum  Lor" +
                "em ipsum  Lorem ipsum  ";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(41, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(412, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select which page you would like to go to next.";
            // 
            // radioButton3
            // 
            this.radioButton3.Location = new System.Drawing.Point(6, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(374, 24);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.Text = "&Dukascopy Tick Data Environment";
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(374, 24);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Import &Metatrader 4 or 5 Environment";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(44, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 82);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // SelectEnvTypePage
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectEnvTypePage";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.m_headerPanel, 0);
            this.Controls.SetChildIndex(this.m_headerSeparator, 0);
            this.Controls.SetChildIndex(this.m_titleLabel, 0);
            this.Controls.SetChildIndex(this.m_subtitleLabel, 0);
            this.Controls.SetChildIndex(this.m_headerPicture, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_headerPicture)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        new internal EnvironmentAssistantForm ParentForm
        {
            get
            {
                return (EnvironmentAssistantForm)base.ParentForm;
            }
        }

        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;
            bool meta = ParentForm.UpdatedEnvironment != null && ParentForm.UpdatedEnvironmentType.StartsWith("Metatrader");
            bool duka = ParentForm.UpdatedEnvironment != null && ParentForm.UpdatedEnvironmentType.Equals("Dukascopy");
            radioButton1.Enabled = string.IsNullOrEmpty(ParentForm.UpdatedEnvironment) || meta;
            radioButton3.Enabled = string.IsNullOrEmpty(ParentForm.UpdatedEnvironment) || duka;
            radioButton3.Checked = duka;

            // Enable both the Next and Back buttons on this page    
            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);
            return true;
        }
 
        protected override string OnAssistantNext()
        {
            if (radioButton1.Checked)
                return typeof(ImportMetatraderPage1).Name;
            else if (radioButton3.Checked)
                return typeof(DukascopyPage0).Name;
            else
                throw new NotSupportedException();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

