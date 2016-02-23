using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;

using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController
{
    
    [GreenRmi]
    public interface IChartViewController : ILabelledController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IMainWindowController MainWindow
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IChartGroupController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartGroupController ChartGroupController
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        INormalChartController Chart1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ICursorChartController CursorChart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<DateTime> FromTimePicker
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        FieldController<DateTime> ToTimePicker
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController CloseChartButton1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController CloseChartButton2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController ConnectCursorButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ToggleButtonController AutoSeriesRangeButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToggleTopBarButton1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ToggleTopBarButton2
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController AddChartButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController SymbolDdButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController PeriodDdButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController ChartTypeDdButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ComboController IndicatorsDdButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController SymPerMiniLabel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Controller TopToolStrip
        {
            get;
            set;
        }
    }

    [GreenRmi]
    public interface IClientChartViewController : IChartViewController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientNormalChartController Chart1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientCursorChartController CursorChart
        {
            get;
            set;
        }

        void UpdateChartAndCursor();

        void UpdateCursor();

        void UpdateSeries();
    }
}