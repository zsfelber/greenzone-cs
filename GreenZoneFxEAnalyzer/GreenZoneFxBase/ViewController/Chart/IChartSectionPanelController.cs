using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using System.Drawing;
using GreenZoneUtil.GreenRmi;


namespace GreenZoneFxEngine.ViewController.Chart
{
    
    [GreenRmi]
    public interface IChartSectionPanelController : IClientController
    {

        IChartPaneController ChartPane
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IChartController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.Environment;")]
        IEnvironmentRuntime Environment
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IPriceLabelPaneController PriceLabelPane1
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        IndicatorWindowType WindowType
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        string PriceFormat
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int Scale
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        SeriesRange SectionRange
        {
            get;
            set;
        }
    }


    [GreenRmi]
    public interface IClientChartSectionPanelController : IChartSectionPanelController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartPaneController ChartPane
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartController Parent
        {
            get;
        }

        IList<PriceLabelY> PriceLabelYs
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.Owner;")]
        IChartOwner Owner
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        new SeriesRange SectionRange
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Owner.SeriesRange;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "throw new NotSupportedException();")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        SeriesRange DragYRange
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Owner.SeriesRange;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "Owner.SeriesRange = value;")]
            set;
        }

        void CalculateSeriesRangeToFit();

        void UpdateCursor();

        void UpdateChartOnScreen(bool layout = true);

        void CalculatePriceLabelYs();

        void CalculatePriceLabelYs(int levelPix);
    }

    [GreenRmi]
    public interface IServerChartSectionPanelController : IChartSectionPanelController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartPaneController ChartPane
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartController Parent
        {
            get;
        }


        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.Owner;")]
        IServerChartOwner Owner
        {
            get;
        }

    }
}
