using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneFxEngine.ViewController.Properties;
using System.Windows.Forms;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;


namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IEATesterController : ITabPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IMainWindowController MainWindow
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridController DataGridView1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController SelectEAColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController TableTestEasEACol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController TableTestEasPeriodCol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        GridColumnController IconColumn
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController MethodCombo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController DataPeriodCombo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController ScrollAcrossTabsCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController SkipEmptyPeriodsCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController UpdateSpreadTickCb
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ProgressTrackController SpeedTrackBar
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ProgressTrackController ProgressTrackBar1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController StartStopButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController PropertiesButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController AddLinkLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController PauseButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController PauseAtButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController SnapButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController MethodInfLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController EaInfLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController SymbolInfLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController FromInfLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController ToInfLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController MethodLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController DataPeriodLabel
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

    }
}
