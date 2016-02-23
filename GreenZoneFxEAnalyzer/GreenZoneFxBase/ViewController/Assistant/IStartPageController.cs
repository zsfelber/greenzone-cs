using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    [GreenRmi]
    public interface IStartPageController : IAssistantPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController TextBox1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController RadioButton2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController RadioButton1
        {
            get;
            set;
        }

    }
}
