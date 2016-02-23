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
    public interface IChartCursorRuntime : IRmiBase
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IChartRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        ISeriesManagerRuntime SeriesManager
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesManager.DefaultCache;")]
        INormalSeriesManagerCache SeriesCache
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        datetime ScrolledBarTime
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Parent.ScrolledBarTime;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "Parent.ScrolledBarTime = value;")]
            set;
        }

        SeriesRange SeriesRange
        {
            get;
            set;
        }

        void UpdateSeriesManager();
        void UpdateSeriesRange();
    }

    [GreenRmi(BaseClass = "ChartCursorRuntimeEx")]
    public interface IServerChartCursorRuntime : IChartCursorRuntime
    {
    }


    [GreenRmi(BaseClass = "ChartCursorRuntimeEx")]
    public interface IClientChartCursorRuntime : IChartCursorRuntime, IChartOwner
    {

        [GreenRmiField(GreenRmiFieldType.New)]
        new IClientChartRuntime Parent
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        new datetime ParentScrolledBarTime
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return Parent.ParentScrolledBarTime;")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "Parent.ParentScrolledBarTime = value;")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new SeriesRange SeriesRange
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.AutoSeriesRange;")]
        new bool AutoSeriesRange
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return true;")]
        new bool IsCursorBarConnected
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        new int ParentCursorPosition
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return 0; //dummy property")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "//dummy property")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple)]
        new int CursorPosition
        {
            [GreenRmiMethod(GreenRmiMethodType.Simple, "return 0; //dummy property")]
            get;
            [GreenRmiMethod(GreenRmiMethodType.Simple, "//dummy property")]
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesCache.RecordCount;")]
        new long RecordCount
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesCache.TotalFileOffset;")]
        new long TotalFileOffset
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.From;")]
        new datetime From
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.To;")]
        new datetime To
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.Point;")]
        new double Point
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.Digits;")]
        new int Digits
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Parent.SymbolFormat;")]
        new string SymbolFormat
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return SeriesCache.sLTime;")]
        new LArr sLTime
        {
            get;
        }


        new void LoadForward(int offset);
        new void LoadAtTotal(long total_ind);
    }


}
