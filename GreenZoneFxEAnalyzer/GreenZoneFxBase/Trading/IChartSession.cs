using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using System.Windows.Forms;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{
    
    [GreenRmi]
    public interface IChartSession : IRmiBase
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Dictionary<SymbolPeriodIndicatorId, IIndicatorSession> Indicators
        {
            get;
            set;
        }

        IScriptSession Script
        {
            get;
            set;
        }

        IExpertSession Expert
        {
            get;
            set;
        }

        symbol Symbol
        {
            get;
            set;
        }

        TimePeriodConst Period
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        ChartType ChartType
        {
            get;
            set;
        }

        ChartType DefaultChartType
        {
            get;
            set;
        }

        bool AppearsInTest
        {
            get;
            set;
        }

        bool IsCursorBarConnected
        {
            get;
            set;
        }

        datetime From
        {
            get;
            set;
        }

        datetime To
        {
            get;
            set;
        }

        bool TopBarVisible
        {
            get;
            set;
        }

        bool BottomBarVisible
        {
            get;
            set;
        }

        SeriesRange SeriesRange
        {
            get;
            set;
        }

        bool AutoSeriesRange
        {
            get;
            set;
        }

        datetime ScrolledBarTime
        {
            get;
            set;
        }

        StartStatus ScriptStartStatus
        {
            get;
            set;
        }

        datetime ScriptRunningTickTime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int ScriptRunningProgress
        {
            get;
        }
    }
}

