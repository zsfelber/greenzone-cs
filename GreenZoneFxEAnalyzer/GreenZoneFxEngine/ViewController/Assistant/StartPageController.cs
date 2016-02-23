using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class StartPageController : StartPageControllerBase
    {

        public StartPageController(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
        {
            TextBox1 = new LabelledController(rmiManager, this);
            RadioButton1 = new RadioButtonController(rmiManager, this);
            RadioButton2 = new RadioButtonController(rmiManager, this);
        }

        new internal EnvironmentAssistantController Assistant
        {
            get
            {
                return (EnvironmentAssistantController)base.Assistant;
            }
        }
        protected override bool OnSetActive()
        {
            if (!base.OnSetActive())
                return false;

            // Enable only the Next button on the this page    
            Assistant.SetAssistantButtons(AssistantButton.Next);

            RadioButton1.Enabled = false;
            RadioButton2.Enabled = false;

            string env = Assistant.UpdatedEnvironment;

            if (env == null)
            {
                RadioButton1.Checked = true;
            }
            else
            {
                RadioButton2.Checked = true;
                TextBox1.Text = env;
            }
            return true;
        }

        protected override string OnAssistantNext()
        {
            return typeof(SelectEnvTypePageController).Name;
        }
    }
}
