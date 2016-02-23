//
// WizardPage.cs
//
// Copyright (C) 2002-2002 Steven M. Soloff (mailto:s_soloff@bellsouth.net)
// All rights reserved.
//

using System;
using System.Windows.Forms;

namespace SMS.Windows.Forms
{
	/// <summary>
	/// Represents a single page within a wizard dialog.
	/// </summary>
	public class AssistantPage : UserControl
	{
        // ==================================================================
        // Public Constructors
        // ==================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="SMS.Windows.Forms.AssistantPage">AssistantPage</see>
        /// class.
        /// </summary>
        public AssistantPage()
		{
            // Required for Windows Form Designer support
            InitializeComponent();
		}


        // ==================================================================
        // Public Properties
        // ==================================================================

        public AssistantPage SelectedNextPage
        {
            get;
            internal set;
        }

        public AssistantPage PreviousPage
        {
            get;
            internal set;
        }

        // ==================================================================
        // Protected Properties
        // ==================================================================
        
        /// <summary>
        /// Gets the <see cref="SMS.Windows.Forms.AssistantForm">AssistantForm</see>
        /// to which this <see cref="SMS.Windows.Forms.AssistantPage">AssistantPage</see>
        /// belongs.
        /// </summary>
        protected AssistantForm Assistant
        {
            get
            {
                // Return the parent WizardForm
                return (AssistantForm)Parent;
            }
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
            Name = "AssistantPage";
            Size = new System.Drawing.Size( 497, 313 );

        }
		#endregion


        // ==================================================================
        // Protected Internal Methods
        // ==================================================================
        
        /// <summary>
        /// Called when the page is no longer the active page.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the page was successfully deactivated; otherwise
        /// <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Override this method to perform special data validation tasks.
        /// </remarks>
        protected internal virtual bool OnKillActive()
        {
            // Deactivate if validation successful
            return Validate();
        }

        /// <summary>
        /// Called when the page becomes the active page.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the page was successfully set active; otherwise
        /// <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Override this method to performs tasks when a page is activated.
        /// Your override of this method should call the default version
        /// before any other processing is done.
        /// </remarks>
        protected internal virtual bool OnSetActive()
        {
            // Activate the page
            return true;
        }
        
        /// <summary>
        /// Called when the user clicks the Back button in a wizard.
        /// </summary>
        /// <returns>
        /// <c>AssistantForm.DefaultPage</c> to automatically advance to the
        /// next page; <c>AssistantForm.NoPageChange</c> to prevent the page
        /// changing.  To jump to a page other than the next one, return
        /// the <c>Name</c> of the page to be displayed.
        /// </returns>
        /// <remarks>
        /// Override this method to specify some action the user must take
        /// when the Back button is pressed.
        /// </remarks>
        protected internal virtual string OnAssistantBack()
        {
            // Move to the default previous page in the wizard
            return AssistantForm.NextPage;
        }
        
        /// <summary>
        /// Called when the user clicks the Finish button in a wizard.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the wizard finishes successfully; otherwise
        /// <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Override this method to specify some action the user must take
        /// when the Finish button is pressed.  Return <c>false</c> to
        /// prevent the wizard from finishing.
        /// </remarks>
        protected internal virtual bool OnAssistantFinish()
        {
            // Finish the wizard
            return true;
        }
        
        /// <summary>
        /// Called when the user clicks the Next button in a wizard.
        /// </summary>
        /// <returns>
        /// <c>AssistantForm.DefaultPage</c> to automatically advance to the
        /// next page; <c>AssistantForm.NoPageChange</c> to prevent the page
        /// changing.  To jump to a page other than the next one, return
        /// the <c>Name</c> of the page to be displayed.
        /// </returns>
        /// <remarks>
        /// Override this method to specify some action the user must take
        /// when the Next button is pressed.
        /// </remarks>
        protected internal virtual string OnAssistantNext()
        {
            // Move to the default next page in the wizard
            return AssistantForm.NextPage;
        }

        protected internal virtual bool OnAssistantCancel()
        {
            return true;
        }
    }
}
