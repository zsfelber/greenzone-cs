using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;

namespace GreenZoneFxEngine.Trading
{
    public interface IServerChartOwner
    {
        SeriesRange SeriesRange
        {
            get;
            set;
        }
        string SymbolFormat
        {
            get;
        }
    }

    public interface IChartOwner : IServerChartOwner
    {
        datetime ScrolledBarTime
        {
            get;
            set;
        }
        datetime ParentScrolledBarTime
        {
            get;
            set;
        }
        bool AutoSeriesRange
        {
            get;
        }
        bool IsCursorBarConnected
        {
            get;
        }
        int ParentCursorPosition
        {
            get;
            set;
        }
        int CursorPosition
        {
            get;
            set;
        }
        long RecordCount
        {
            get;
        }
        long TotalFileOffset
        {
            get;
        }
        datetime From
        {
            get;
        }
        datetime To
        {
            get;
        }
        double Point
        {
            get;
        }
        int Digits
        {
            get;
        }
        LArr sLTime
        {
            get;
        }

        void LoadForward(int offset);
        void LoadAtTotal(long total_ind);
    }
}
