//
// WizardPage.cs
//
// Copyright (C) 2002-2002 Steven M. Soloff (mailto:s_soloff@bellsouth.net)
// All rights reserved.
//

using System;
using System.Windows.Forms;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.Gui.ViewController;

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

        public void Bind(WinFormsMVContext context, AssistantPageController controller)
        {
            new SimpleControlVCBinder(context, this, controller);
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

    }
}
