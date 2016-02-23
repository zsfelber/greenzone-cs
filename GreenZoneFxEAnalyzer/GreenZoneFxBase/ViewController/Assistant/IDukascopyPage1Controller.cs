using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.IO;
using GreenZoneFxEngine.Types;
using System.Collections;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Assistant
{
    public enum UpdateMode
    {
        NONE,
        UPDATE_ALL,
        UPDATE_TICKS_GEN_PERIODS,
        DOWNLOAD_TICKS_GEN_PERIODS
    }


    [GreenRmi]
    public interface IDukascopyPage1Controller : IAssistantPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ChildControlMap<string> ErrorProvider1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ChildControlMap<string> ToolTip1
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
        ListController GenerateChl
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController GenerateDefTb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController UpdateNoneRb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController DownloadTickGenPeriodsRb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController UpdateTicksGenPeriodsRb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        RadioButtonController UpdateAllRb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController DeleteCorruptPeriodsCb
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
        FieldController<DateTime> FromDateP
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

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        UpdateMode UpdateMode
        {
            get;
            set;
        }

        List<TimePeriodConst> CustomPeriods
        {
            get;
            set;
        }

        List<TimePeriodConst> AllSelectedPeriods
        {
            get;
            set;
        }

        DateTime From
        {
            get;
            set;
        }

        DateTime To
        {
            get;
            set;
        }

        bool DeleteCorruptPeriods
        {
            get;
            set;
        }





    }

}
