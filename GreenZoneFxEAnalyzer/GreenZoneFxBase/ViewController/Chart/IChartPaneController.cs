using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;

using System.Windows.Forms.VisualStyles;
using System.Drawing;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.ViewController.Chart
{

    [GreenRmi]
    public interface IChartPaneController : IClientController
    {

        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.Environment;")]
        IEnvironmentRuntime Environment
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartController Chart
        {
            get;
            set;
        }
        
        [GreenRmiField(GreenRmiFieldType.Simple, "return Chart.Owner;")]
        IServerChartOwner Owner
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        bool DragTimerUsed
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color AskColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color InactiveColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color GridColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Rectangle ThumbRectangle
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Rectangle BarRectangle
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Rectangle CpBarRectangle
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Virtual)]
        bool ChartCalcAutoGap
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int ChartLeftGap
        {
            get ;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int ChartRightGap
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int ChartMinimumX
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int ChartMaximumX
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int ChartEffectiveWidth
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Color SliderBarColor
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int SliderMinimum
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int SliderMaximum
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        Range SliderValueRange
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int SliderValue
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int SliderPosition
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        int CpBarValue
        {
            get ;
            set ;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int CpBarPosition
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        bool CpBarVisible
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        bool ThumbRectBarVisible
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        bool SliderThumbVisible
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int ChartAutoGap
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int SliderDefaultAutoGap
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        Size ThumbRectangleSize
        {
            get;
            set;
        }

        bool ThumbInitiliazed
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController SelectedIndDel
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ButtonController SelectedProps
        {
            get;
            set;
        }
    }


    [GreenRmi(BaseClass = "ChartPaneControllerEx")]
    public interface IClientChartPaneController : IChartPaneController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartController Chart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartSectionPanelController Parent
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IChartOwner Owner
        {
            get;
        }

        [System.ComponentModel.Category("ChartController pane")]
        List<SeriesBar> SeriesBars
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int SeriesBarWidth
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        double ChartWindowTopGapValue
        {
            get;
        }

        ReadOnlyCollection<Pen> GridPens
        {
            get;
            set;
        }
    }

    [GreenRmi(BaseClass = "ChartPaneControllerEx")]
    public interface IServerChartPaneController : IChartPaneController
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartController Chart
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartSectionPanelController Parent
        {
            get;
        }
    }
}
