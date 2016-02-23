using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IAddScriptController : IDialogController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IMainWindowController MainWindow
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly, Modifiers = "virtual")]
        symbol Symbol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly, Modifiers = "virtual")]
        TimePeriodConst Period
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController ScriptCombo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController SymbolCombo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController PeriodCombo
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
        ChildControlMap<string> ErrorProvider1
        {
            get;
            set;
        }

    }
}
