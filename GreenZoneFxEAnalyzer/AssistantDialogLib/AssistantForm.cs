//
// WizardForm.cs
//
// Copyright (C) 2002-2002 Steven M. Soloff (mailto:s_soloff@bellsouth.net)
// All rights reserved.
//

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Gui.ViewController;

namespace SMS.Windows.Forms
{
    /// <summary>
    /// Represents a wizard dialog.
    /// </summary>
    public class AssistantForm : Form
	{
        /// <summary>
        /// The Back button.
        /// </summary>
        protected Button m_backButton;

        /// <summary>
        /// The Next button.
        /// </summary>
        protected Button m_nextButton;

        /// <summary>
        /// The Cancel button.
        /// </summary>
        protected Button m_cancelButton;

        /// <summary>
        /// The Finish button.
        /// </summary>
        protected Button m_finishButton;

        /// <summary>
        /// The separator between the buttons and the content.
        /// </summary>
        protected GroupBox m_separator;


        // ==================================================================
        // Public Constructors
        // ==================================================================
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SMS.Windows.Forms.AssistantForm">AssistantForm</see>
        /// class.
        /// </summary>
        public AssistantForm()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

            // Ensure Finish and Next buttons are positioned similarly
			m_finishButton.Location = m_nextButton.Location;
		}

        // ==================================================================
        // whatsit
        // ==================================================================

        public void Bind(WinFormsMVContext context, AssistantFormController controller)
        {
            new FormVCBinder(context, this, controller);
            new ButtonVCBinder(context, m_backButton, controller.M_backButton);
            new ButtonVCBinder(context, m_nextButton, controller.M_nextButton);
            new ButtonVCBinder(context, m_cancelButton, controller.M_cancelButton);
            new ButtonVCBinder(context, m_finishButton, controller.M_finishButton);
        }

        // ==================================================================
        // Private Methods
        // ==================================================================
        
        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_backButton = new System.Windows.Forms.Button();
            this.m_nextButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.m_finishButton = new System.Windows.Forms.Button();
            this.m_separator = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // m_backButton
            // 
            this.m_backButton.Location = new System.Drawing.Point(252, 327);
            this.m_backButton.Name = "m_backButton";
            this.m_backButton.Size = new System.Drawing.Size(75, 23);
            this.m_backButton.TabIndex = 8;
            this.m_backButton.Text = "< &Back";
            // 
            // m_nextButton
            // 
            this.m_nextButton.Location = new System.Drawing.Point(327, 327);
            this.m_nextButton.Name = "m_nextButton";
            this.m_nextButton.Size = new System.Drawing.Size(75, 23);
            this.m_nextButton.TabIndex = 9;
            this.m_nextButton.Text = "&Next >";
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(412, 327);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 11;
            this.m_cancelButton.Text = "Cancel";
            // 
            // m_finishButton
            // 
            this.m_finishButton.Location = new System.Drawing.Point(10, 327);
            this.m_finishButton.Name = "m_finishButton";
            this.m_finishButton.Size = new System.Drawing.Size(75, 23);
            this.m_finishButton.TabIndex = 10;
            this.m_finishButton.Text = "&Finish";
            this.m_finishButton.Visible = false;
            // 
            // m_separator
            // 
            this.m_separator.Location = new System.Drawing.Point(0, 313);
            this.m_separator.Name = "m_separator";
            this.m_separator.Size = new System.Drawing.Size(499, 2);
            this.m_separator.TabIndex = 7;
            this.m_separator.TabStop = false;
            // 
            // AssistantForm
            // 
            this.AcceptButton = this.m_nextButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(497, 360);
            this.Controls.Add(this.m_backButton);
            this.Controls.Add(this.m_nextButton);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_finishButton);
            this.Controls.Add(this.m_separator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssistantForm";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
		#endregion

    }
}
