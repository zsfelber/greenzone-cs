using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{

    [GreenRmi]
	public interface IDukascopyPage0Controller : IAssistantPageController
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
        FieldController<DateTime> FromDateP
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<DateTime> ToDateP
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

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<int> AccountNumberNupd
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
        ListController SymbolsChl
        {
            get;
            set;
        }


        string[] ImportedParameters
        {
            get;
            set;
        }


        string EnvironmentName
        {
            get;
            set;
        }

        List<string> SelectedSymbols
        {
            get;
            set;
        }

    }
}
