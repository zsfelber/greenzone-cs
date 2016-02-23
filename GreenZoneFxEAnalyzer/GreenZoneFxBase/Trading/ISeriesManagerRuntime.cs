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
    [GreenRmiInsertBody("System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return SeriesCaches.Values.GetEnumerator(); }")]
    public interface ISeriesManagerRuntime : ITradingConst, IEnumerable<INormalSeriesManagerCache>
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEnvironmentRuntime Environment
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartRuntime Parent
        {
            get;
            set;
        }

        Dictionary<SymbolPeriodId, INormalSeriesManagerCache> SeriesCaches
        {
            get;
            set;
        }

        INormalSeriesManagerCache DefaultCache
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        datetime FocusedTime
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        INormalSeriesManagerCache this[symbol sym, TimePeriodConst period]
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        IIndicatorRuntime this[symbol sym, TimePeriodConst period, Mt4ExecutableInfo ind, params object[] args]
        {
            get;
        }


        INormalSeriesManagerCache CreateSeriesManagerCache(SymbolPeriodId sp);

        void UpdateCursorDefault();

        [GreenRmiMethod(GreenRmiMethodType.Simple, "return SeriesCaches.Values.GetEnumerator();")]
        new IEnumerator<INormalSeriesManagerCache> GetEnumerator();
    }


    [GreenRmi(BaseClass = "SeriesManagerRuntimeEx")]
    public interface IServerSeriesManagerRuntime : ISeriesManagerRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerNormalSeriesManagerCache DefaultCache
        {
            get;
            set;
        }

        void UpdateDefault();

        void AddExpert(IServerChartRuntime c);

        void UpdateDefault(symbol symbol, TimePeriodConst period, datetime focusedTime);

        void Tick(symbol symbol, double Bid, double Ask, double volume);

        void DeinitAll();
    }
}
