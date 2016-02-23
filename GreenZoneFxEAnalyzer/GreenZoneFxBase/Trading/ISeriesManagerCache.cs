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
    [GreenRmiInsertBody("System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return Indicators.Values.GetEnumerator(); }")]
    public interface ISeriesManagerCache : ITradingConst, IEnumerable<IIndicatorRuntime>
    {
        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.Environment;")]
        IEnvironmentRuntime Environment
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISeriesManagerRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
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
        Dictionary<IndicatorId, IIndicatorRuntime> Indicators
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        symbol Symbol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        TimePeriodConst Period
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        double Bid
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        double Ask
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        double Point
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        int Digits
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return (datetime)sLTime[0];")]
        datetime FocusedTime
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        int Bars
        {
            get;
        }

        DArr sOpen
        {
            get;
            set;
        }

        DArr sLow
        {
            get;
            set;
        }

        DArr sHigh
        {
            get;
            set;
        }

        DArr sClose
        {
            get;
            set;
        }

        DArr sBids
        {
            get;
            set;
        }

        DArr sAsks
        {
            get;
            set;
        }

        LArr sLTime
        {
            get;
            set;
        }

        DArr sVolume
        {
            get;
            set;
        }

        DArr Open
        {
            get;
            set;
        }

        DArr Low
        {
            get;
            set;
        }

        DArr High
        {
            get;
            set;
        }

        DArr Close
        {
            get;
            set;
        }

        DArr Bids
        {
            get;
            set;
        }

        DArr Asks
        {
            get;
            set;
        }

        LArr LTime
        {
            get;
            set;
        }

        LArrAsIArr Time
        {
            get;
            set;
        }

        DArr Volume
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Get(ind, args, false);")]
        IIndicatorRuntime this[Mt4ExecutableInfo ind, object[] args]
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Get(ind, argKey, args, false);")]
        IIndicatorRuntime this[Mt4ExecutableInfo ind, string argKey, object[] args]
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Indicators[id];")]
        IIndicatorRuntime this[IndicatorId id]
        {
            get;
        }

        ISeriesArr GetArray(GreenZoneFxEngine.Trading.TradingConst.SeriesArrayPool type);

        IIndicatorRuntime Get(Mt4ExecutableInfo ind, object[] args, bool autoCreate = false);

        IIndicatorRuntime Get(Mt4ExecutableInfo ind, string argKey, object[] args, bool autoCreate = false);

        IIndicatorRuntime AddIndicator(Mt4ExecutableInfo ind, object[] args);

        IndicatorId AddIndicator(IIndicatorRuntime indicator, bool forceUpdate = true);

        IndicatorId ReplaceIndicator(IndicatorId sp0, IIndicatorRuntime indicator, bool forceUpdate = true);

        IIndicatorRuntime RemoveIndicator(Mt4ExecutableInfo ind, object[] args);

        IIndicatorRuntime RemoveIndicator(IndicatorId sp);

        double GetIndicatorValue(string name, GreenZoneFxEngine.Trading.TradingConst.IndicatorLine _mode, int shift, params object[] args);

        double GetIndicatorValue(string name, int mode, int shift, params object[] args);

        double GetIndicatorValue<T>(GreenZoneFxEngine.Trading.TradingConst.IndicatorLine _mode, int shift, params object[] args) where T : IIndicatorRuntime;

        double GetIndicatorValue<T>(int mode, int shift, params object[] args) where T : IIndicatorRuntime;

        [GreenRmiMethod(GreenRmiMethodType.Simple, "return Indicators.Values.GetEnumerator();")]
        new IEnumerator<IIndicatorRuntime> GetEnumerator();

    }

    [GreenRmi(BaseClass = "SeriesManagerCacheEx")]
    public interface IServerSeriesManagerCache : ISeriesManagerCache
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerSymbolRuntime SymbolRuntime
        {
            get;
            set;
        }

        void DeinitAll();
    }

    [GreenRmi(BaseClass = "SeriesManagerCacheEx")]
    public interface INormalSeriesManagerCache : ISeriesManagerCache
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ITimeSeriesRuntime SeriesRuntime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.IndCount;")]
        new int Bars
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.IndOffset;")]
        int IndOffset
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.IndCount;")]
        int IndCount
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.FileOffset;")]
        long FileOffset
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.TotalFileOffset;")]
        long TotalFileOffset
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return 0;")]
        int BufferFromIndex
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.RecordCount;")]
        long RecordCount
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.From;")]
        datetime From
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesRuntime.To;")]
        datetime To
        {
            get;
        }

        long LastUpdatedFileOffset
        {
            get;
            set;
        }

        int LastUpdatedBufferLength
        {
            get;
            set;
        }

        void Load(datetime focusedTime);

        void LoadForward(int offset);

        void LoadAtTotal(long total_ind);

        void fixSeriesRange(ref SeriesRange seriesRange);
    }

    [GreenRmi(BaseClass = "NormalSeriesManagerCacheEx")]
    public interface IServerNormalSeriesManagerCache : INormalSeriesManagerCache, IServerSeriesManagerCache 
    {
    }

    [GreenRmi(BaseClass = "NormalSeriesManagerCacheEx")]
    public interface IStreamSeriesManagerCache : INormalSeriesManagerCache
    {
    }

    [GreenRmi(BaseClass = "ServerNormalSeriesManagerCacheEx")]
    public interface IServerStreamSeriesManagerCache : IServerNormalSeriesManagerCache, IStreamSeriesManagerCache
    {
        void AddExpert(IServerChartRuntime c);

        void Bar(TimePeriodConst period, double open, double low, double high, double close, double volume, int offset);

        void Tick(double Bid, double Ask, double volume);

    }

    [GreenRmi(BaseClass = "SeriesManagerCacheEx")]
    public interface IArraySeriesManagerCache : ISeriesManagerCache
    {
    }

    [GreenRmi]
    public interface IServerArraySeriesManagerCache : IServerSeriesManagerCache, IArraySeriesManagerCache
    {
    }
}
