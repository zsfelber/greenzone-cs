using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Windows.Forms;
using GreenZoneUtil.Util;

using System.Drawing;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{
    
    [GreenRmi]
    public interface IChartController : IClientController
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IServerChartOwner Owner
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        SeriesRange SeriesRange
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Owner.SeriesRange;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "Owner.SeriesRange = value;")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IMainWindowController MainWindow
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IMainWinTabPageController TabPanel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color SliderBarColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IList<IChartSectionPanelController> ChartSectionPanels
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartSectionPanelController MasterChartSectionPanel
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
        ITimeLabelPaneController TimeLabelPane1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ZoomOutVButton
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ZoomOutHButton
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ZoomInVButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ZoomInHButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ZoomToFitButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController ZoomToScrollPriceButton
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return TabPanel.Environment;")]
        IEnvironmentRuntime Environment
        {
            get;
        }
    }

    [GreenRmi]
    public interface IClientChartController : IChartController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IChartOwner Owner
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartSectionPanelController MasterChartSectionPanel
        {
            get;
            set;
        }

        List<TimeLabelX> TimeLabelXsUpper
        {
            get;
            set;
        }
        
        List<TimeLabelX> TimeLabelXsLower
        {
            get;
            set;
        }

        void UpdateAllChartAndCursor();

        void ParentUpdateAllChartAndCursor();

        void PrintStatus(SeriesBar bar, IndicatorBar ibar, string f);

        void UpdateCursor();

        void UpdateChartAndCursor();

        void UpdateSeries(bool enableAutoFit = true);

        void UpdateChartOnScreen();

        void CalculateSeriesRangeToFit(bool includeMainChart);

        void ScrollYToPrice();

        void CalculateTimeLabelXs();
    }

    [GreenRmi]
    public interface IServerChartController : IChartController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartSectionPanelController MasterChartSectionPanel
        {
            get;
            set;
        }
    }
}