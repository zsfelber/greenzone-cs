using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneUtil.ViewController;
using System.Reflection;
using GreenZoneUtil.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Properties
{
    
    [GreenRmi]
    public interface IIndicatorDialogController : IDialogController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IIndicatorPanelController IndicatorPanel1
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

		[GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController RemoveButton
        {
            get;
            set;
        }

    }
}
