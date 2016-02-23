using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    [GreenRmi]
    public interface IExpertDialogController : IDialogController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IExpertPropertiesController ExpertRuntimePanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController OkButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController _CancelButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ResetButton
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController LoadButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController SaveButton
        {
            get;
            set;
        }

    }
}
