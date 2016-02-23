using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Timers;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    [GreenRmi]
    public interface IImportMetatraderPage2Controller : IAssistantPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ChildControlMap<string> ErrorProvider1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FileDialogController OpenFileDialog1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController CheckBox1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController AccountNameTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController AccountCompanyTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController HistoryDirectoryCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<string> NameTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<int> AccountNumberNupd
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController AccountCurrencyTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<int> LeverageNupd
        {
            get;
            set;
        }

        string[] ImportedParameters
        {
            get;
            set;
        }

        string[] HistoryDirectories
        {
            get;
            set;
        }

        string ImportedMetatarderDir
        {
            get;
            set;
        }

        string ImportedMetatarderVersion
        {
            get;
            set;
        }

        string HistoryDirectory
        {
            get;
            set;
        }

        string EnvironmentName
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly, Modifiers = "virtual")]
        bool CloseMetatrader
        {
            get;
            set;
        }

    
    }
}
