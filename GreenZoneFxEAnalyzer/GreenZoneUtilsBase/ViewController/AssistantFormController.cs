using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.Util;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    /// <summary>
    /// Used to identify the various buttons that may appear within a wizard
    /// dialog.  
    /// </summary>
    [Flags]
    
    public enum AssistantButton
    {
        /// <summary>
        /// Identifies the <b>Back</b> button.
        /// </summary>
        Back = 0x00000001,

        /// <summary>
        /// Identifies the <b>Next</b> button.
        /// </summary>
        Next = 0x00000002,

        /// <summary>
        /// Identifies the <b>Finish</b> button.
        /// </summary>
        Finish = 0x00000004,

        /// <summary>
        /// Identifies the disabled <b>Finish</b> button.
        /// </summary>
        DisabledFinish = 0x00000008,
    }

    public interface IAssistantFormController : IDialogController
    {
    }
    
    public class AssistantFormController : DialogController
    {
        // ==================================================================
        // Public Constants
        // ==================================================================
        /// <summary>
        /// Used by a page to indicate to this wizard that the next page
        /// should be activated when either the Back or Next buttons are
        /// pressed.
        /// </summary>
        public const string NextPage = "";

        /// <summary>
        /// Used by a page to indicate to this wizard that the selected page
        /// should remain active when either the Back or Next buttons are
        /// pressed.
        /// </summary>
        public const string NoPageChange = null;

        // ==================================================================
        // Private Fields
        // ==================================================================

        /// <summary>
        /// Index of the selected page; -1 if no page selected.
        /// </summary>
        private int m_selectedIndex = -1;



        // ==================================================================
        // Public Constructors
        // ==================================================================

        /// <summary>
        /// Initializes a new instance of the <see cref="SMS.Windows.Forms.AssistantForm">AssistantForm</see>
        /// class.
        /// </summary>
        public AssistantFormController(GreenRmiManager rmiManager)
            : this(rmiManager, null)
        {
        }

        public AssistantFormController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            this.m_backButton = new ButtonController(rmiManager, this);
            this.m_nextButton = new ButtonController(rmiManager, this);
            this.m_cancelButton = new ButtonController(rmiManager, this);
            this.m_finishButton = new ButtonController(rmiManager, this);
            // 
            // m_backButton
            // 
            this.m_backButton.Name = "m_backButton";
            this.m_backButton.Text = "< &Back";
            this.m_backButton.Pressed += new ControllerEventHandler(this.OnClickBack);
            // 
            // m_nextButton
            // 
            this.m_nextButton.Name = "m_nextButton";
            this.m_nextButton.Text = "&Next >";
            this.m_nextButton.Pressed += new ControllerEventHandler(this.OnClickNext);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.Pressed += new ControllerEventHandler(this.OnClickCancel);
            // 
            // m_finishButton
            // 
            this.m_finishButton.Name = "m_finishButton";
            this.m_finishButton.Text = "&Finish";
            this.m_finishButton.Visible = false;
            this.m_finishButton.Pressed += new ControllerEventHandler(this.OnClickFinish);
            // 
            // AssistantForm
            // 
            this.AcceptButton = this.m_nextButton;
            this.CancelButton = this.m_cancelButton;
            this.Add(this.m_backButton);
            this.Add(this.m_nextButton);
            this.Add(this.m_cancelButton);
            this.Add(this.m_finishButton);
            this.AllowMaximize = false;
            this.AllowMinimize = false;
            this.ShowInTaskbar = false;
            this.Name = "AssistantForm";
            this.FormClosing += new ControllerEventHandler(this.AssistantForm_FormClosing);

            _addDeps();
        }

        protected AssistantFormController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Pages = (List<AssistantPageController>) info.GetValue("Pages", typeof(List<AssistantPageController>));
            m_backButton = (ButtonController) info.GetValue("M_backButton", typeof(ButtonController));
            m_nextButton = (ButtonController) info.GetValue("M_nextButton", typeof(ButtonController));
            m_cancelButton = (ButtonController) info.GetValue("M_cancelButton", typeof(ButtonController));
            m_finishButton = (ButtonController) info.GetValue("M_finishButton", typeof(ButtonController));

            _addDeps();
        }

        void _addDeps()
        {
            dependencies.AddRange(Pages);
            dependencies.Add(M_backButton);
            dependencies.Add(M_nextButton);
            dependencies.Add(M_cancelButton);
            dependencies.Add(M_finishButton);
        }

        public event PropertyChangedEventHandler PagesChanged;

        List<AssistantPageController> pages;
        IList<AssistantPageController> pagesUm;
        const int PROPERTY_22_PAGES_ID = 22;
        public IList<AssistantPageController> Pages
        {
            get
            {
                return pagesUm;
            }
            set
            {
                if (pages != value)
                {
                    pages = (List<AssistantPageController>)value;
                    pagesUm = pages.AsReadOnly();
                    changed[PROPERTY_22_PAGES_ID] = true;
                    somethingChanged = true;
                    if (PagesChanged != null)
                    {
                        PagesChanged(this, new PropertyChangedEventArgs("Pages", value));
                    }
                }
            }
        }

        /// <summary>
        /// The Back button.
        /// </summary>
        readonly ButtonController m_backButton;
        const int PROPERTY_23_BACKBUTTON_ID = 23;
        public ButtonController M_backButton
        {
            get
            {
                return m_backButton;
            }
        }

        /// <summary>
        /// The Next button.
        /// </summary>
        readonly ButtonController m_nextButton;
        const int PROPERTY_24_NEXTBUTTON_ID = 24;
        public ButtonController M_nextButton
        {
            get
            {
                return m_nextButton;
            }
        }

        /// <summary>
        /// The Cancel button.
        /// </summary>
        readonly ButtonController m_cancelButton;
        const int PROPERTY_25_CANCELBUTTON_ID = 25;
        public ButtonController M_cancelButton
        {
            get
            {
                return m_cancelButton;
            }
        }

        /// <summary>
        /// The Finish button.
        /// </summary>
        readonly ButtonController m_finishButton;
        const int PROPERTY_26_FINISHBUTTON_ID = 26;
        public ButtonController M_finishButton
        {
            get
            {
                return m_finishButton;
            }
        }

        /// <summary>
        /// Activates the page at the specified index in the page array.
        /// </summary>
        /// <param name="newIndex">
        /// Index of new page to be selected.
        /// </param>
        private void ActivatePage(int newIndex)
        {
            // Ensure the index is valid
            if (newIndex < 0 || newIndex >= pages.Count)
                throw new ArgumentOutOfRangeException();

            // Deactivate the current page if applicable
            AssistantPageController currentPage = null;
            if (m_selectedIndex != -1)
            {
                currentPage = (AssistantPageController)pages[m_selectedIndex];
                if (!currentPage.OnKillActive())
                    return;
            }

            // Activate the new page
            AssistantPageController newPage = (AssistantPageController)pages[newIndex];
            if (!newPage.OnSetActive())
                return;

            // Update state
            m_selectedIndex = newIndex;
            if (currentPage != null)
                currentPage.Visible = false;
            newPage.Visible = true;
        }

        /// <summary>
        /// Handles the Click event for the Back button.
        /// </summary>
        private void OnClickBack(object sender, ControllerEventArgs e)
        {
            // Ensure a page is currently selected
            if (m_selectedIndex != -1)
            {
                AssistantPageController cur = (AssistantPageController)pages[
                    m_selectedIndex];
                // Inform selected page that the Back button was clicked
                string pageName = cur.OnAssistantBack();
                switch (pageName)
                {
                    // Do nothing
                    case NoPageChange:
                        break;

                    // Activate the next appropriate page
                    case NextPage:
                        if (m_selectedIndex - 1 >= 0)
                        {
                            cur.SelectedNextPage = null;
                            ActivatePage(pages.IndexOf(cur.PreviousPage));
                        }
                        break;

                    // Activate the specified page if it exists
                    default:
                        foreach (AssistantPageController page in pages)
                        {
                            if (page.Name == pageName)
                            {
                                cur.SelectedNextPage = null;
                                ActivatePage(pages.IndexOf(page));
                                break;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the Click event for the Cancel button.
        /// </summary>
        private void OnClickCancel(object sender, ControllerEventArgs e)
        {
            // Close wizard
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the Click event for the Finish button.
        /// </summary>
        private void OnClickFinish(object sender, ControllerEventArgs e)
        {
            // Ensure a page is currently selected
            if (m_selectedIndex != -1)
            {
                // Inform selected page that the Finish button was clicked
                AssistantPageController page = (AssistantPageController)pages[m_selectedIndex];
                if (page.OnAssistantFinish())
                {
                    // Deactivate page and close wizard
                    if (page.OnKillActive())
                        DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Handles the Click event for the Next button.
        /// </summary>
        private void OnClickNext(object sender, ControllerEventArgs e)
        {
            // Ensure a page is currently selected
            if (m_selectedIndex != -1)
            {
                AssistantPageController next;
                AssistantPageController cur = (AssistantPageController)pages[
                    m_selectedIndex];
                // Inform selected page that the Next button was clicked
                string pageName = cur.OnAssistantNext();
                switch (pageName)
                {
                    // Do nothing
                    case NoPageChange:
                        break;

                    // Activate the next appropriate page
                    case NextPage:
                        if (m_selectedIndex + 1 < pages.Count)
                        {
                            next = (AssistantPageController)pages[
                                m_selectedIndex + 1];
                            cur.SelectedNextPage = next;
                            next.PreviousPage = cur;
                            ActivatePage(m_selectedIndex + 1);
                        }
                        break;

                    // Activate the specified page if it exists
                    default:
                        foreach (AssistantPageController page in pages)
                        {
                            if (page.Name == pageName)
                            {
                                cur.SelectedNextPage = page;
                                page.PreviousPage = cur;
                                ActivatePage(pages.IndexOf(page));
                                break;
                            }
                        }
                        break;
                }
            }
        }


        // ==================================================================
        // Protected Methods
        // ==================================================================

        /// <seealso cref="System.Windows.Forms.Control.OnControlAdded">
        /// System.Windows.Forms.Control.OnControlAdded
        /// </seealso>
        public override void Add(Controller child)
        {
            // Invoke base class implementation
            base.Add(child);

            // Set default properties for all WizardPage instances added to
            // this form
            AssistantPageController page = child as AssistantPageController;
            if (page != null)
            {
                page.Visible = false;
                pages.Add(page);
                if (m_selectedIndex == -1)
                    m_selectedIndex = 0;
            }
        }

        /// <seealso cref="System.Windows.Forms.Form.OnLoad">
        /// System.Windows.Forms.Form.OnLoad
        /// </seealso>
        protected override void OnLoad(FormControllerEventArgs e)
        {
            // Invoke base class implementation
            base.OnLoad(e);

            // Activate the first page in the wizard
            if (pages.Count > 0)
                ActivatePage(0);
        }


        // ==================================================================
        // Public Methods
        // ==================================================================

        /// <summary>
        /// Sets the text in the Finish button.
        /// </summary>
        /// <param name="text">
        /// Text to be displayed on the Finish button.
        /// </param>
        public void SetFinishText(string text)
        {
            // Set the Finish button text
            m_finishButton.Text = text;
        }

        /// <summary>
        /// Enables or disables the Back, Next, or Finish buttons in the
        /// wizard.
        /// </summary>
        /// <param name="flags">
        /// A set of flags that customize the function and appearance of the
        /// wizard buttons.  This parameter can be a combination of any
        /// value in the <c>AssistantButton</c> enumeration.
        /// </param>
        /// <remarks>
        /// Typically, you should call <c>SetAssistantButtons</c> from
        /// <c>AssistantPageController.OnSetActive</c>.  You can display a Finish or a
        /// Next button at one time, but not both.
        /// </remarks>
        public void SetAssistantButtons(AssistantButton flags)
        {
            // Enable/disable and show/hide buttons appropriately
            m_backButton.Enabled =
                (flags & AssistantButton.Back) == AssistantButton.Back;
            m_nextButton.Enabled =
                (flags & AssistantButton.Next) == AssistantButton.Next;
            m_nextButton.Visible =
                (flags & AssistantButton.Finish) == 0 &&
                (flags & AssistantButton.DisabledFinish) == 0;
            m_finishButton.Enabled =
                (flags & AssistantButton.DisabledFinish) == 0;
            m_finishButton.Visible =
                (flags & AssistantButton.Finish) == AssistantButton.Finish ||
                (flags & AssistantButton.DisabledFinish) == AssistantButton.DisabledFinish;

            // Set the AcceptButton depending on whether or not the Finish
            // button is visible or not
            AcceptButton = m_finishButton.Visible ? m_finishButton :
                m_nextButton;
        }

        private void AssistantForm_FormClosing(object sender, ControllerEventArgs _e)
        {
            FormControllerEventArgs e = (FormControllerEventArgs)_e;
            // Ensure a page is currently selected
            if (m_selectedIndex != -1 && DialogResult != DialogResult.OK)
            {
                // Inform selected page that the Finish button was clicked
                AssistantPageController page = (AssistantPageController)pages[m_selectedIndex];
                if (!page.OnAssistantCancel())
                {
                    e.Cancel = true;
                }
            }
        }

        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_22_PAGES_ID:
                    return Pages;
                case PROPERTY_23_BACKBUTTON_ID:
                    return M_backButton;
                case PROPERTY_24_NEXTBUTTON_ID:
                    return M_nextButton;
                case PROPERTY_25_CANCELBUTTON_ID:
                    return M_cancelButton;
                case PROPERTY_26_FINISHBUTTON_ID:
                    return M_finishButton;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_22_PAGES_ID:
                    Pages = new BridgeCollection<AssistantPageController>((ArrayList)value);
                    break;
                case PROPERTY_23_BACKBUTTON_ID:
                    break;
                case PROPERTY_24_NEXTBUTTON_ID:
                    break;
                case PROPERTY_25_CANCELBUTTON_ID:
                    break;
                case PROPERTY_26_FINISHBUTTON_ID:
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Pages", Pages);
            info.AddValue("M_backButton", M_backButton);
            info.AddValue("M_nextButton", M_nextButton);
            info.AddValue("M_cancelButton", M_cancelButton);
            info.AddValue("M_finishButton", M_finishButton);
        }
    }
}
