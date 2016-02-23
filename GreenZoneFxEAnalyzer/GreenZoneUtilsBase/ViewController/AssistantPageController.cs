using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface IAssistantPageController : IController
    {
    }

    public class AssistantPageController : Controller, IAssistantPageController
    {
        public event PropertyChangedEventHandler SelectedNextPageChanged;
        public event PropertyChangedEventHandler PreviousPageChanged;

        public AssistantPageController(GreenRmiManager rmiManager, AssistantFormController assistant)
            : base(rmiManager, assistant)
        {
            this.assistant = assistant;
            dependencies.Add(assistant);
        }

        public AssistantPageController(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
            AssistantFormController assistant = (AssistantFormController)buffer.ChangedProps[PROPERTY_14_ASSISTANT_ID];
            this.assistant = assistant;
            dependencies.Add(assistant);
        }

        protected AssistantPageController(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            assistant = (AssistantFormController) info.GetValue("Assistant", typeof(AssistantFormController));
            SelectedNextPage = (AssistantPageController) info.GetValue("SelectedNextPage", typeof(AssistantPageController));
            PreviousPage = (AssistantPageController) info.GetValue("PreviousPage", typeof(AssistantPageController));
            dependencies.Add(assistant);
        }

        readonly AssistantFormController assistant;
        const int PROPERTY_14_ASSISTANT_ID = 14;
        public AssistantFormController Assistant
        {
            get
            {
                return assistant;
            }
        }

        AssistantPageController selectedNextPage;
        const int PROPERTY_15_SELECTEDNEXTPAGE_ID = 15;
        public AssistantPageController SelectedNextPage
        {
            get
            {
                return selectedNextPage;
            }
            set
            {
                if (selectedNextPage != value)
                {
                    selectedNextPage = value;
                    changed[PROPERTY_15_SELECTEDNEXTPAGE_ID] = true;
                    somethingChanged = true;
                    if (SelectedNextPageChanged != null)
                    {
                        SelectedNextPageChanged(this, new PropertyChangedEventArgs("SelectedNextPage", value));
                    }
                }
            }
        }


        AssistantPageController previousPage;
        const int PROPERTY_16_PREVIOUSPAGE_ID = 16;
        public AssistantPageController PreviousPage
        {
            get
            {
                return previousPage;
            }
            set
            {
                if (previousPage != value)
                {
                    previousPage = value;
                    changed[PROPERTY_16_PREVIOUSPAGE_ID] = true;
                    somethingChanged = true;
                    if (PreviousPageChanged != null)
                    {
                        PreviousPageChanged(this, new PropertyChangedEventArgs("PreviousPage", value));
                    }
                }
            }
        }

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
            return AssistantFormController.NextPage;
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
            return AssistantFormController.NextPage;
        }

        protected internal virtual bool OnAssistantCancel()
        {
            return true;
        }


        public override object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                case PROPERTY_14_ASSISTANT_ID:
                    return Assistant;
                case PROPERTY_15_SELECTEDNEXTPAGE_ID:
                    return SelectedNextPage;
                case PROPERTY_16_PREVIOUSPAGE_ID:
                    return PreviousPage;
                default:
                    return base.RmiGetProperty(propertyId);
            }
        }

        public override void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                case PROPERTY_14_ASSISTANT_ID:
                    //RO Assistant = (AssistantFormController)value;
                    break;
                case PROPERTY_15_SELECTEDNEXTPAGE_ID:
                    SelectedNextPage = (AssistantPageController)value;
                    break;
                case PROPERTY_16_PREVIOUSPAGE_ID:
                    PreviousPage = (AssistantPageController)value;
                    break;
                default:
                    base.RmiSetProperty(propertyId, value);
                    break;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Assistant", Assistant);
            info.AddValue("SelectedNextPage", SelectedNextPage);
            info.AddValue("PreviousPage", PreviousPage);
        }
    }
}
