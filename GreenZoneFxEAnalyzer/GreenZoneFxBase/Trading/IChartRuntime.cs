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
    public interface IChartRuntime : IRmiBase, IServerChartOwner
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return Group.Environment;")]
        IEnvironmentRuntime Environment
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartGroupRuntime Group
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartCursorRuntime CursorRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        ISymbolRuntime SymbolRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SymbolRuntime.Context;")]
        ISymbolContext SymbolContext
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISeriesManagerRuntime GuiSeriesManager
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        IScriptRuntime Script
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        IExpertRuntime Expert
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        new SeriesRange SeriesRange
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Session.SeriesRange;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "Session.SeriesRange = value;")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        datetime ScrolledBarTime
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Session.ScrolledBarTime;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "Session.ScrolledBarTime = value;")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        symbol Symbol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        TimePeriodConst Period
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        bool IsMaster
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.SeriesRange.NumBars;")]
        int WindowBarsPerChart
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return GuiSeriesManager.DefaultCache.IndOffset + Session.SeriesRange.OffsetTo;")]
        int WindowFirstVisibleBar
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SymbolRuntime.SymbolFormat;")]
        new string SymbolFormat
        {
            get;
        }
    }

    [GreenRmi]
    public interface IServerChartRuntime : IChartRuntime
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSymbolRuntime SymbolRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSymbolContext SymbolContext
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSeriesManagerRuntime GuiSeriesManager
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerChartCursorRuntime CursorRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerScriptRuntime Script
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerExpertRuntime Expert
        {
            get;
            set;
        }

        void AddIndicator(IServerIndicatorRuntime indicatorRuntime);

        void ReplaceIndicator(IndicatorId id0, IServerIndicatorRuntime indicatorRuntime);

        IIndicatorRuntime RemoveIndicator(IndicatorId id0);

        void Update(symbol _symbol, TimePeriodConst _period);
    }

    [GreenRmi]
    public interface IClientChartRuntime : IChartRuntime, IChartOwner
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartGroupRuntime Group
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartCursorRuntime CursorRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientScriptRuntime Script
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientExpertRuntime Expert
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new datetime ParentScrolledBarTime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.IsCursorBarConnected;")]
        new bool IsCursorBarConnected
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new int ParentCursorPosition
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        new int CursorPosition
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return GuiSeriesManager.DefaultCache.RecordCount;")]
        new long RecordCount
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return GuiSeriesManager.DefaultCache.TotalFileOffset;")]
        new long TotalFileOffset
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.AutoSeriesRange;")]
        new bool AutoSeriesRange
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.From;")]
        new datetime From
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Session.To;")]
        new datetime To
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return GuiSeriesManager.DefaultCache;")]
        ISeriesManagerCache SeriesCache
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SymbolContext.Point;")]
        new double Point
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SymbolContext.Digits;")]
        new int Digits
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return GuiSeriesManager.DefaultCache.sLTime;")]
        new LArr sLTime
        {
            get;
        }

        new void LoadForward(int offset);
        new void LoadAtTotal(long total_ind);
    }

}
