using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    
    public class SelectEnvTypePageController : SelectEnvTypePageControllerBase
    {
        public SelectEnvTypePageController(GreenRmiManager rmiManager, EnvironmentAssistantController assistant)
            : base(rmiManager, assistant)
        {
            RadioButton1 = new RadioButtonController(rmiManager, this);
            RadioButton3 = new RadioButtonController(rmiManager, this);
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
            bool meta = Assistant.UpdatedEnvironment != null && Assistant.UpdatedEnvironmentType.StartsWith("Metatrader");
            bool duka = Assistant.UpdatedEnvironment != null && Assistant.UpdatedEnvironmentType.Equals("Dukascopy");
            RadioButton1.Enabled = string.IsNullOrEmpty(Assistant.UpdatedEnvironment) || meta;
            RadioButton3.Enabled = string.IsNullOrEmpty(Assistant.UpdatedEnvironment) || duka;
            RadioButton3.Checked = duka;

            // Enable both the Next and Back buttons on this page    
            Assistant.SetAssistantButtons(AssistantButton.Back | AssistantButton.Next);
            return true;
        }

        protected override string OnAssistantNext()
        {
            if (RadioButton1.Checked)
                return typeof(ImportMetatraderPage1Controller).Name;
            else if (RadioButton3.Checked)
                return typeof(DukascopyPage0Controller).Name;
            else
                throw new NotSupportedException();
        }

        private void radioButton3_CheckedChanged(object sender, ControllerEventArgs e)
        {

        }
    }
}
