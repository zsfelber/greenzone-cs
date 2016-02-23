using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Util;

using GreenZoneFxEngine.ViewController.Properties;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IChartGroupController : IMainWinTabPageController
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IList<IChartViewController> ChartViews
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IServerChartGroupRuntime ChartGroupRuntime
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        MultiSplitController TableLayoutPanel1
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
        ComboController EaCombo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController OpenEaButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController OpenScriptButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController InTestButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToggleBottomBarButton1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToggleBottomBarButton2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Controller BottomToolStrip
        {
            get;
            set;
        }
    }

    [GreenRmi]
    public interface IClientChartGroupController : IChartGroupController
    {
        void UpdateAllCursor(IClientChartViewController invoker = null);

        void UpdateAllChartAndCursor(IClientChartViewController invoker = null);

    }

}
