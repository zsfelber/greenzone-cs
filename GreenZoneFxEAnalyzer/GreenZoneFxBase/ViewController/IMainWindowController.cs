using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;

using GreenZoneUtil.ViewController;
using System.Timers;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IMainWindowController : IFormController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEATesterController EaTester
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IScriptRunnerController ScriptRunner
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (IEnvironmentRuntime)EnvironmentCombo.SelectedItem;")]
        IEnvironmentRuntime Environment
        {
            get;
        }

        IList<IMainWinTabPageController> TabPages
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        INavigatorController NavigatorController
        {
            get;
            set;
        }

        IOrdersTableController OrdersTable
        {
            get;
            set;
        }

        IOrdersOverviewController OrdersOverview
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TabController TabControl1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TabController LauncherTabControl
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        SplitController SplitContainer1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        SplitController SplitContainer2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController EnvironmentCombo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToolStripButton1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToolStripButton2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToolStripButton3
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController OrdersOverviewToolStripMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController OrdersTableToolStripMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController NavigatorToolStripMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController EditEnvorinmentsToolStripMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController OptionsToolStripMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController NewEnvironmentToolStripMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController UpdateEnvironmentMenuItem
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController TimeLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController OpenLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController LowLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController HighLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController CloseLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController ValueLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController StatusLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController ToolStripStatusLabel2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController OLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController LLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController HLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController CLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        LabelledController VLabel
        {
            get;
            set;
        }


    }

}
