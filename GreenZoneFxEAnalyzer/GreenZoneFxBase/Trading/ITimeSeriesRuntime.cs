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
    public interface ITimeSeriesRuntime : IRmiBase
    {

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        ISymbolRuntime Parent
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
        symbol Symbol
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        bool Online
        {
            get;
            set;
        }

        bool InputFileIsRead
        {
            get;
            set;
        }

        DArr Volume
        {
            get;
            set;
        }

        LArr Begin_time
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        int HeaderLen
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        int RecordLen
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        datetime From
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        datetime To
        {
            get;
            set;
        }

        long FileOffset
        {
            get;
            set;
        }

        long FileOffsetTo
        {
            get;
            set;
        }

        int IndOffset
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return FileOffsetTo - IndOffset;")]
        long TotalFileOffset
        {
            get;
        }

        int IndCount
        {
            get;
            set;
        }

        long RecordCount
        {
            get;
            set;
        }

        void Load(datetime focusedTime);

        void LoadForward(datetime focusedTime);

        void LoadForward(int offset);

        void LoadAtTotal(long total_ind);

    }

    [GreenRmi(BaseClass = "TimeSeriesRuntimeEx")]
    public interface IServerTimeSeriesRuntime : ITimeSeriesRuntime
    {
        datetime GetTime(int i);

        double GetVolume(int i);
    }

    [GreenRmi(BaseClass = "TimeSeriesRuntimeEx")]
    public interface IPeriodTimeSeriesRuntime : ITimeSeriesRuntime
    {
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
    }

    [GreenRmi(BaseClass = "TimeSeriesRuntimeEx")]
    public interface ITickTimeSeriesRuntime : ITimeSeriesRuntime
    {
        DArr Bid
        {
            get;
            set;
        }

        DArr Ask
        {
            get;
            set;
        }
    }


    [GreenRmi(BaseClass = "ServerTimeSeriesRuntimeEx")]
    public interface IServerPeriodTimeSeriesRuntime : IServerTimeSeriesRuntime, IPeriodTimeSeriesRuntime
    {
        double GetOpen(int i);

        double GetLow(int i);

        double GetHigh(int i);

        double GetClose(int i);
    }

    [GreenRmi(BaseClass = "ServerTimeSeriesRuntimeEx")]
    public interface IServerTickTimeSeriesRuntime : IServerTimeSeriesRuntime, ITickTimeSeriesRuntime
    {
        double GetBid(int i);

        double GetAsk(int i);
    }
}
