using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    [GreenRmi]
    public interface ISelectEnvTypePageController : IAssistantPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController RadioButton1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController RadioButton3
        {
            get;
            set;
        }

    }
}
