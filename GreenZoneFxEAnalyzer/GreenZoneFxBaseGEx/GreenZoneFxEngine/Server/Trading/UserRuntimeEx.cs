using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using GreenZoneFxEngine.Util;
using System.Drawing;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
    public class ServerUserRuntimeEx : ServerUserRuntimeBase
    {
        public ServerUserRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
            : base(rmiManager, parent, cache)
        {
            if (cache is INormalSeriesManagerCache && ((INormalSeriesManagerCache)cache).SeriesRuntime != null)
            {
                IndicatorLastOffset = ((INormalSeriesManagerCache)cache).SeriesRuntime.IndCount;
            }
            LastFileOffset = -1;
            LastBufferLength = -1;
        }
        
        public ServerUserRuntimeEx(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
            : base(rmiManager, parent, session, icache)
        {
        }

        public ServerUserRuntimeEx(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }


        protected ServerUserRuntimeEx(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override IOrder SelectedOrder
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.SelectedOrder;
                }
            }
            set
            {
                lock (Environment.Locker)
                {
                    base.SelectedOrder = value;
                }
            }
        }

        public override OrderType OrderType
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderType;
                }
            }
        }
        public override symbol OrderSymbol
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderSymbol;
                }
            }
        }
        public override int OrderTicket
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderTicket;
                }
            }
        }

        public override double OrderLots
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderLots;
                }
            }
        }

        public override double OrderStopLoss
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderStopLoss;
                }
            }
        }
        public override double OrderTakeProfit
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderTakeProfit;
                }
            }
        }

        public override datetime OrderOpenTime
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderOpenTime;
                }
            }
        }

        public override double OrderOpenPrice
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderOpenPrice;
                }
            }
        }

        public override datetime OrderCloseTime
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderCloseTime;
                }
            }
        }

        public override double OrderClosePrice
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderClosePrice;
                }
            }
        }

        public override string OrderComment
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderComment;
                }
            }
        }

        public override datetime OrderExpiration
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderExpiration;
                }
            }
        }

        public override int OrderMagicNumber
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrderMagicNumber;
                }
            }
        }

        public override double OrderCommission
        {
            get
            {
                lock (Environment.Locker)
                {
                    // TODO convert to account currency
                    return base.OrderCommission;
                }
            }
        }

        public override double OrderProfit
        {
            get
            {
                lock (Environment.Locker)
                {
                    // TODO convert to account currency
                    return base.OrderProfit;
                }
            }
        }

        public override double OrderSwap
        {
            get
            {
                lock (Environment.Locker)
                {
                    // TODO convert to account currency
                    return base.OrderSwap;
                }
            }
        }

        public override int OrdersTotal
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrdersTotal;
                }
            }
        }

        public override int OrdersHistoryTotal
        {
            get
            {
                lock (Environment.Locker)
                {
                    return base.OrdersHistoryTotal;
                }
            }
        }

        public override int IndicatorCounted
        {
            get
            {
                if (IndicatorLastOffset == 0)
                {
                    return Cache.Bars - 1;
                }
                else
                {
                    return Cache.Bars - IndicatorLastOffset;
                }
            }
        }


        public override double MarketInfo(symbol symbol, MarketInfoConst type)
        {
            return Environment.MarketInfo(symbol, type);
        }

        public override double MarketInfo(string _symbol, MarketInfoConst type)
        {
            symbol symbol = Environment.GetSymbol(_symbol);
            return Environment.MarketInfo(symbol, type);
        }

        public override int GetLastError()
        {
            return Environment.LastError;
        }

        public override string symbol()
        {
            return Cache.Symbol.strSymbol;
        }

        public override int iBars(string _symbol, TimePeriodConst timeframe)
        {
            symbol symbol;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    return Bars;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            int result = iBars(symbol, timeframe);
            return result;
        }

        public override int iBarShift(string _symbol, TimePeriodConst timeframe, datetime time, bool exact = false)
        {
            symbol symbol;
            if (_symbol == null)
            {
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            int result = iBarShift(symbol, timeframe, time, exact);
            return result;
        }

        public override double iClose(string _symbol, TimePeriodConst timeframe, int shift)
        {
            symbol symbol;
            double result;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    result = Close[shift];
                    return result;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            result = iClose(symbol, timeframe, shift);
            return result;
        }

        public override double iHigh(string _symbol, TimePeriodConst timeframe, int shift)
        {
            symbol symbol;
            double result;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    result = High[shift];
                    return result;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            result = iHigh(symbol, timeframe, shift);
            return result;
        }

        public override int Highest(string _symbol, TimePeriodConst timeframe, SeriesArrayPool type, int count = WHOLE_ARRAY, int start = 0)
        {
            int r = iHighest(_symbol, timeframe, type, count, start);
            return r;
        }

        public override int iHighest(string _symbol, TimePeriodConst timeframe, SeriesArrayPool type, int count = WHOLE_ARRAY, int start = 0)
        {
            symbol symbol;
            if (_symbol == null)
            {
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            int result = iHighest(symbol, timeframe, type, count, start);
            return result;
        }

        public override double iLow(string _symbol, TimePeriodConst timeframe, int shift)
        {
            symbol symbol;
            double result;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    result = Low[shift];
                    return result;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            result = iLow(symbol, timeframe, shift);
            return result;
        }

        public override int Lowest(string _symbol, TimePeriodConst timeframe, SeriesArrayPool type, int count = WHOLE_ARRAY, int start = 0)
        {
            int r = iLowest(_symbol, timeframe, type, count, start);
            return r;
        }

        public override int iLowest(string _symbol, TimePeriodConst timeframe, SeriesArrayPool type, int count = WHOLE_ARRAY, int start = 0)
        {
            symbol symbol;
            if (_symbol == null)
            {
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            int result = iLowest(symbol, timeframe, type, count, start);
            return result;
        }

        public override double iOpen(string _symbol, TimePeriodConst timeframe, int shift)
        {
            symbol symbol;
            double result;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    result = Open[shift];
                    return result;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            result = iOpen(symbol, timeframe, shift);
            return result;
        }

        public override datetime iTime(string _symbol, TimePeriodConst timeframe, int shift)
        {
            symbol symbol;
            datetime result;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    result = Time[shift];
                    return result;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            result = iTime(symbol, timeframe, shift);
            return result;
        }

        public override double iVolume(string _symbol, TimePeriodConst timeframe, int shift)
        {
            symbol symbol;
            double result;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    result = Volume[shift];
                    return result;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }
            result = iVolume(symbol, timeframe, shift);
            return result;
        }



        public override int iBars(symbol symbol, TimePeriodConst timeframe)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            return sch.Bars;
        }

        public override int iBarShift(symbol symbol, TimePeriodConst timeframe, datetime time, bool exact = false)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            int result = sch.Time.BinarySearch((int)time, -1);

            if (result >= 0)
            {
                return result;
            }
            else if (exact)
            {
                result = -1;
            }
            else
            {
                result = ~result;
            }

            return result;
        }

        public override double iClose(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            return sch.Close[shift];
        }

        public override double iHigh(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            return sch.High[shift];
        }

        public override int iHighest(symbol symbol, TimePeriodConst timeframe, SeriesArrayPool type, int count = WHOLE_ARRAY, int start = 0)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            ISeriesArr arr = sch.GetArray(type);
            int result = arr.SearchMaximum(start, count);
            return result;
        }

        public override double iLow(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            return sch.Low[shift];
        }

        public override int iLowest(symbol symbol, TimePeriodConst timeframe, SeriesArrayPool type, int count = WHOLE_ARRAY, int start = 0)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            ISeriesArr arr = sch.GetArray(type);
            int result = arr.SearchMinimum(start, count);
            return result;
        }

        public override double iOpen(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            return sch.Open[shift];
        }

        public override datetime iTime(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            datetime result = (datetime)sch.Time[shift];
            return result;
        }

        public override double iVolume(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            INormalSeriesManagerCache sch = SeriesManager[symbol, timeframe];
            double vol = sch.Volume[shift];
            return vol;
        }


        public override GeneralFile FileOpen(string filename, FileConstraint mode)
        {
            try
            {
                return FileOpen(filename, mode, ";");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return null;
            }
        }

        public override GeneralFile FileOpen(string filename, FileConstraint mode, params string[] delimiters)
        {
            return FileOpen(Environment.ImportedFromDirectory + "\\experts\\files", filename, mode, delimiters);
        }

        public override GeneralFile FileOpenHistory(string filename, FileConstraint mode)
        {
            return FileOpenHistory(filename, mode, ";");
        }

        public override GeneralFile FileOpenHistory(string filename, FileConstraint mode, params string[] delimiters)
        {
            string path;
            if ("history".Equals(Environment.HistoryDirectory) || string.IsNullOrEmpty(Environment.HistoryDirectory))
            {
                path = Environment.ImportedFromDirectory + "\\history\\";
            }
            else
            {
                path = Environment.ImportedFromDirectory + "\\history\\" + Environment.HistoryDirectory + "\\";
            }
            return FileOpen(path, filename, mode, delimiters);
        }

        private static GeneralFile FileOpen(string path, string filename, FileConstraint mode, params string[] delimiters)
        {
            try
            {
                switch (mode)
                {
                    case FILE_CSV | FILE_READ: return new CsvFileR(path + "\\" + filename, delimiters);
                    case FILE_CSV | FILE_WRITE: return new CsvFileW(path + "\\" + filename, false, delimiters);
                    case FILE_CSV | FILE_READ | FILE_WRITE: return new CsvFileW(path + "\\" + filename, true, delimiters);
                    case FILE_BIN | FILE_READ: return new BinaryFileR(path + "\\" + filename);
                    case FILE_BIN | FILE_WRITE: return new BinaryFileW(path + "\\" + filename, false);
                    case FILE_BIN | FILE_READ | FILE_WRITE: return new BinaryFileW(path + "\\" + filename, true);
                    default: throw new ArgumentException("mode : " + mode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return null;
            }
        }

        public override void FileClose(GeneralFile file)
        {
            try
            {
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
            }
        }
        public override void FileDelete(string filename)
        {
            try
            {
                System.IO.File.Delete(Environment.ImportedFromDirectory + "\\experts\\files\\" + filename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
            }
        }
        public override void FileFlush(GeneralFile file)
        {
            try
            {
                file.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
            }
        }
        public override bool FileIsEnding(GeneralFile file)
        {
            try
            {
                return file.IsEnding();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return false;
            }
        }
        public override bool FileIsLineEnding(GeneralFile file)
        {
            try
            {
                return file.IsLineEnding();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return false;
            }
        }
        public override int FileReadArray<T>(GeneralFile file, T[] array, int start, int count)
        {
            try
            {
                return file.ReadArray(array, start, count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override double FileReadDouble(GeneralFile file, FileDoubleType size = DOUBLE_VALUE)
        {
            try
            {
                return file.ReadDouble(size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override int FileReadInteger(GeneralFile file, FileIntegerType size = LONG_VALUE)
        {
            try
            {
                return file.ReadInteger(size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override double FileReadNumber(GeneralFile file)
        {
            try
            {
                return file.ReadNumber();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override string FileReadString(GeneralFile file, int length = 0)
        {
            try
            {
                return file.ReadString(length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return null;
            }
        }
        public override bool FileSeek(GeneralFile file, long offset, SeekOrigin origin)
        {
            try
            {
                file.Seek(offset, origin);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return false;
            }
        }
        public override long FileSize(GeneralFile file)
        {
            try
            {
                return file.Size();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override long FileTell(GeneralFile file)
        {
            try
            {
                return file.Tell();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override int FileWrite(GeneralFile file, params object[] args)
        {
            try
            {
                return file.Write(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override int FileWriteArray<T>(GeneralFile file, T[] args, int start, int count)
        {
            try
            {
                return file.WriteArray(args, start, count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override int FileWriteDouble(GeneralFile file, double value, FileDoubleType size = DOUBLE_VALUE)
        {
            try
            {
                file.WriteDouble(value, size);
                return (int)size;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override int FileWriteInteger(GeneralFile file, int value, FileIntegerType size = LONG_VALUE)
        {
            try
            {
                file.WriteInteger(value, size);
                return (int)size;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }
        public override int FileWriteString(GeneralFile file, string value, int length)
        {
            try
            {
                return file.WriteString(value, length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return -1;
            }
        }

        public override void Sleep(int millis)
        {
            Thread.Sleep(millis);
        }

        public override void Alert(string info)
        {
            MessageBox.Show(info, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public override double MathAbs(double d)
        {
            return Math.Abs(d);
        }

        public override int MathAbs(int d)
        {
            return Math.Abs(d);
        }

        public override int MathMax(int n1, int n2)
        {
            return Math.Max(n1, n2);
        }

        public override int MathMin(int n1, int n2)
        {
            return Math.Min(n1, n2);
        }

        public override double MathMax(double n1, double n2)
        {
            return Math.Max(n1, n2);
        }

        public override double MathMin(double n1, double n2)
        {
            return Math.Min(n1, n2);
        }

        public override int MathRand()
        {
            return random.Next(32767);
        }

        public override double NormalizeDouble(double d, int precision)
        {
            return Math.Round(d, precision);
        }

        public override string DoubleToStr(double d, int digits)
        {
            digits = Math.Min(digits, 8);
            string f = "#.00000000".Substring(0, digits + 2);
            string result = d.ToString(f);
            return result;
        }

        Random random = new Random(0);
        public override void MathSrand(int seed)
        {
            random = new Random(seed);
        }

        public override double MathRound(double d)
        {
            return Math.Round(d);
        }

        public override double MathPow(double d, double p)
        {
            return Math.Pow(d, p);
        }

        public override int StringLen(string str)
        {
            return str.Length;
        }

        public override string StringSubstr(string text, int start, int length = 0)
        {
            return text.Substring(start, length);
        }

        public override string StringConcatenate(params object[] args)
        {
            StringBuilder b = new StringBuilder();
            foreach (var s in args)
            {
                b.Append(s);
            }
            string result = b.ToString();
            return result;
        }

        public override int ArraySize(DArr array)
        {
            return array.Length;
        }
        public override int ArraySize(IArr array)
        {
            return array.Length;
        }
        public override int ArraySize<T>(T[] array)
        {
            return array.Length;
        }

        public override int ArrayInitialize(double[] array, double value)
        {
            for (int i = array.Length - 1; i >= 0; i--) array[i] = value;
            return array.Length;
        }

        public override int ArrayInitialize(double[,] array, double value)
        {
            for (int i = array.GetLength(0) - 1; i >= 0; i--)
                for (int j = array.GetLength(1) - 1; j >= 0; j--) array[i, j] = value;
            return array.Length;
        }

        public override int ArrayInitialize(DArr array, double value)
        {
            for (int i = array.Length - 1; i >= 0; i--) array[i] = value;
            return array.Length;
        }

        public override int ArrayResize<T>(ref T[] array, int new_size)
        {
            T[] a2 = new T[new_size];
            if (array != null)
            {
                array.CopyTo(a2, 0);
            }
            array = a2;
            return new_size;
        }



        public override datetime StrToTime(string datestr)
        {
            try
            {
                datetime date;
                try
                {
                    date = datetime.ParseExact(datestr, "yyyy.mm.dd hh:mi", null);
                }
                catch (FormatException)
                {
                    date = datetime.ParseExact(datestr, "yyyy.mm.dd", null);
                }
                return date;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name + ":" + e.Message);
                return datetime.MinValue;
            }
        }

        public override string TimeToStr(datetime date, DateTimeFormat mode = TIME_DATE|TIME_MINUTES)
        {
            StringBuilder b = new StringBuilder();
            if ((mode & TIME_DATE) == TIME_DATE)
            {
                b.Append(date.ToString("yyyy.mm.dd"));
            }
            if ((mode & TIME_DATE) == TIME_DATE)
            {
                if ((mode & TIME_DATE) == TIME_DATE)
                {
                    b.Append(" ");
                }
                b.Append(date.ToString("hh:mi"));
            }
            return b.ToString();
        }

        public override int TimeYear(datetime date)
        {
            return date.Year;
        }

        public override int TimeMonth(datetime date)
        {
            return date.Month;
        }

        public override int TimeDay(datetime date)
        {
            return date.Day;
        }

        public override DayOfWeek TimeDayOfWeek(datetime date)
        {
            return date.DayOfWeek;
        }

        public override int TimeDayOfYear(datetime date)
        {
            return date.DayOfYear;
        }

        public override int TimeHour(datetime date)
        {
            return date.Hour;
        }

        public override int TimeMinute(datetime date)
        {
            return date.Minute;
        }

        public override int TimeSecond(datetime date)
        {
            return date.Second;
        }

        public override int GetTickCount()
        {
            double tc = (DateTime.Now - datetime._EPOCH).TotalMilliseconds;
            return (int)tc;
        }

        public override void Print(string msg)
        {
            Console.WriteLine(msg);
        }

        public override void Print(params object[] msgs)
        {
            foreach (var v in msgs)
            {
                Console.Write(v);
            }
            Console.WriteLine();
        }





        public override string orderSymbol()
        {
            lock (Environment.Locker)
            {
                return SelectedOrder.Symbol.strSymbol;
            }
        }

        public override bool OrderSelect(int index, TradeSelectMode select = SELECT_BY_TICKET, TradePool pool = MODE_TRADES)
        {
            lock (Environment.Locker)
            {
                if (null != (SelectedOrder = Environment.Orders.Select(index, select, pool)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }

        public override IOrder OrderSend(string _symbol, OrderType cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expirationDatetime = default(datetime), Color arrow_color = default(Color))
        {
            // TODO default (..)
            lock (Environment.Locker)
            {
                symbol symbol = Environment.GetSymbol(_symbol);
                IOrder order = OrderSend(symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment, magic, expirationDatetime, arrow_color);
                return order;
            }
        }

        public override IOrder OrderSend(symbol symbol, OrderType cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = null, int magic = 0, datetime expirationDatetime = default(datetime), Color arrow_color = default(Color))
        {
            // TODO default (..)
            lock (Environment.Locker)
            {
                // TODO online
                IOrder order;
                if (symbol == Cache.Symbol)
                {
                    order = Cache.SymbolRuntime.Orders.Add(symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment, magic, expirationDatetime, arrow_color);
                }
                else
                {
                    order = Environment.SymbolContexts[symbol].Runtime.Orders.Add(symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment, magic, expirationDatetime, arrow_color);
                }
                return order;
            }
        }

        public override IOrder OrderClose(int ticket, double lots, double price, int slippage, Color color = default(Color))
        {
            lock (Environment.Locker)
            {
                IOrder order;
                // TODO this is market execution
                // TODO instant execution
                // TODO !!! !!! !!! lots
                if (Symbol == Cache.Symbol)
                {
                    order = Cache.SymbolRuntime.Orders.CloseOrDelete(ticket, OrderTypeKind.DIRECT_TRADE, color);
                }
                else
                {
                    order = Environment.SymbolContexts[Symbol].Runtime.Orders.CloseOrDelete(ticket, OrderTypeKind.DIRECT_TRADE, color);
                }
                return order;
            }
        }

        public override IOrder OrderDelete(int ticket, Color color = default(Color))
        {
            lock (Environment.Locker)
            {
                IOrder order;
                // TODO this is market execution
                // TODO instant execution
                if (Symbol == Cache.Symbol)
                {
                    order = Cache.SymbolRuntime.Orders.CloseOrDelete(ticket, OrderTypeKind.PENDING_TRADE, color);
                }
                else
                {
                    order = Environment.SymbolContexts[Symbol].Runtime.Orders.CloseOrDelete(ticket, OrderTypeKind.PENDING_TRADE, color);
                }
                return order;
            }
        }

        public override IOrder OrderModify(int ticket, double price, double stoploss, double takeprofit, datetime expiration, Color color = default(Color))
        {
            lock (Environment.Locker)
            {
                // TOOD visual
                if (color == default(Color))
                {
                    color = CLR_NONE;
                }
                ITradeOrder order = Environment.Orders.Select<ITradeOrder>(ticket);
                Environment.Orders.Modify(order, price, stoploss, takeprofit, expiration, color);

                return order;
            }
        }




        public override double GetIndicatorValue<T>(string _symbol, TimePeriodConst timeframe, IndicatorLine _mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(_symbol, timeframe, typeof(T).FullName, _mode, shift, args);
            return r;
        }

        public override double GetIndicatorValue<T>(string _symbol, TimePeriodConst timeframe, int mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(_symbol, timeframe, typeof(T).FullName, mode, shift, args);
            return r;
        }

        public override double GetIndicatorValue<T>(symbol symbol, TimePeriodConst timeframe, IndicatorLine _mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(symbol, timeframe, typeof(T).FullName, _mode, shift, args);
            return r;
        }

        public override double GetIndicatorValue<T>(symbol symbol, TimePeriodConst timeframe, int mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(symbol, timeframe, typeof(T).FullName, mode, shift, args);
            return r;
        }



        public override double GetIndicatorValue(string _symbol, TimePeriodConst timeframe, string name, IndicatorLine _mode, int shift, params object[] args)
        {
            int mode = (int)_mode;
            symbol symbol;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT)
                {
                    double result0 = Cache.GetIndicatorValue(name, mode, shift, args);
                    return result0;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }

            double result = GetIndicatorValue(symbol, timeframe, name, mode, shift, args);

            return result;
        }

        public override double GetIndicatorValue(string _symbol, TimePeriodConst timeframe, string name, int mode, int shift, params object[] args)
        {
            symbol symbol;
            if (_symbol == null)
            {
                if (timeframe == TimePeriodConst.PERIOD_CURRENT || timeframe == Cache.Period)
                {
                    double result0 = Cache.GetIndicatorValue(name, mode, shift, args);
                    return result0;
                }
                symbol = Cache.Symbol;
            }
            else
            {
                symbol = Environment.GetSymbol(_symbol);
            }

            double result = GetIndicatorValue(symbol, timeframe, name, mode, shift, args);

            return result;
        }

        public override double GetIndicatorValue(symbol symbol, TimePeriodConst timeframe, string name, IndicatorLine _mode, int shift, params object[] args)
        {
            int mode = (int)_mode;
            double result = GetIndicatorValue(symbol, timeframe, name, mode, shift, args);

            return result;
        }

        public override double GetIndicatorValue(symbol symbol, TimePeriodConst timeframe, string name, int mode, int shift, params object[] args)
        {
            if (timeframe == TimePeriodConst.PERIOD_CURRENT)
            {
                timeframe = Cache.Period;
            }
            if (symbol == Cache.Symbol && timeframe == Cache.Period)
            {
                double result0 = Cache.GetIndicatorValue(name, mode, shift, args);
                return result0;
            }

            INormalSeriesManagerCache cache2 = SeriesManager[symbol, timeframe];

            double result = cache2.GetIndicatorValue(name, mode, shift, args);

            return result;
        }


        public override IArraySeriesManagerCache GetIndicatorCacheOnArray(DArr priceData)
        {
            IArraySeriesManagerCache cache2;
            if (TmpArrayCaches.TryGetValue(priceData, out cache2))
            {
                return cache2;
            }
            else
            {
                cache2 = new ServerArraySeriesManagerCache(rmiManager, SeriesManager, new SymbolPeriodId(Cache.Symbol, Cache.Period), priceData);
                TmpArrayCaches[priceData] = cache2;

                return cache2;
            }
        }



        /* Calculates the specified custom indicator and returns its value. The custom indicator must be compiled (*.EX4 file) and be in the terminal_directory\experts\indicators directory. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * name   -   Custom indicator compiled program name. 
        * mode   -   Line index. Can be from 0 to 7 and must correspond with the index used by one of SetIndexBuffer functions. 
        * shift  -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        * args   -   Parameters set (if necessary). The passed parameters and their order must correspond with the desclaration order and the type of extern variables of the custom indicator. 
        */
        public override double iCustom(string _symbol, TimePeriodConst timeframe, string name, int mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(_symbol, timeframe, name, mode, shift, args);

            return r;
        }

        public override double iCustom(string _symbol, TimePeriodConst timeframe, string name, IndicatorLine mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(_symbol, timeframe, name, mode, shift, args);

            return r;
        }

        public override double iCustom(symbol symbol, TimePeriodConst timeframe, string name, IndicatorLine mode, int shift, params object[] args)
        {
            double r = GetIndicatorValue(symbol, timeframe, name, mode, shift, args);

            return r;
        }


        // standard indicators

        /*
         * Calculates the Bill Williams' Accelerator/Decelerator oscillator. 
         * Parameters:
         * symbol   -   Symbol name of the security on the data of which the indicator will be calculated. NULL means the current symbol. 
         * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
         * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
         */
        public override double iAC(string _symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<Accelerator>(_symbol, timeframe, 0, shift);

            return r;
        }
        public override double iAC(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<Accelerator>(symbol, timeframe, 0, shift);

            return r;
        }

        /*
         * Calculates the Accumulation/Distribution indicator and returns its value.
         * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
         * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
         * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
         */
        public override double iAD(string _symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<Accumulation>(_symbol, timeframe, 0, shift);

            return r;
        }
        public override double iAD(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<Accumulation>(symbol, timeframe, 0, shift);

            return r;
        }

        /*
         * Calculates the Bill Williams' Alligator and returns its value. 
         * Parameters:
         * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
         * timeframe   -   Timeframe. It can be one of Timeframe enumeration values. 0 means the current chart timeframe. 
         * jaw_period   -   Blue line averaging period (Alligator's Jaw). 
         * jaw_shift   -   Blue line shift relative to the chart. 
         * teeth_period   -   Red line averaging period (Alligator's Teeth). 
         * teeth_shift   -   Red line shift relative to the chart. 
         * lips_period   -   Green line averaging period (Alligator's Lips). 
         * lips_shift   -   Green line shift relative to the chart. 
         * ma_method   -   MA method. It can be any of Moving Average methods. 
         * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
         * mode   -   Data source, identifier of a line of the indicator. It can be any of the following values:
         * MODE_GATORJAW - Gator Jaw (blue) balance line,
         * MODE_GATORTEETH - Gator Teeth (red) balance line,
         * MODE_GATORLIPS - Gator Lips (green) balance line. 
         * shift   -   Shift relative to the current bar (number of periods back) where the data should be taken from. 
         */
        public override double iAlligator(string _symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Alligator>(_symbol, timeframe, mode, shift,
                jaw_period, jaw_shift, teeth_period, teeth_shift, lips_period, lips_shift, ma_method, applied_price);

            return r;
        }
        public override double iAlligator(symbol symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Alligator>(symbol, timeframe, mode, shift,
                jaw_period, jaw_shift, teeth_period, teeth_shift, lips_period, lips_shift, ma_method, applied_price);

            return r;
        }

        /*
         * Calculates the Movement directional index and returns its value. 
         * Parameters:symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
         * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
         * period   -   Averaging period for calculation. 
         * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
         * mode   -   Indicator line index. It can be any of the Indicators line identifiers enumeration value. 
         * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
         */
        public override double iADX(string _symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<ADX>(_symbol, timeframe, mode, shift, period, applied_price);

            return r;
        }
        public override double iADX(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<ADX>(symbol, timeframe, mode, shift, period, applied_price);

            return r;
        }

        /* Calculates the Indicator of the average true range and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iATR(string _symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<ATR>(_symbol, timeframe, 0, shift, period);

            return r;
        }
        public override double iATR(symbol symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<ATR>(symbol, timeframe, 0, shift, period);

            return r;
        }

        /* Calculates the Bill Williams' Awesome oscillator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iAO(string _symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<Awesome>(_symbol, timeframe, 0, shift);

            return r;
        }
        public override double iAO(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<Awesome>(symbol, timeframe, 0, shift);

            return r;
        }

        /* Calculates the Bears Power indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iBearsPower(string _symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<Bears>(_symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }
        public override double iBearsPower(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<Bears>(symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }



        /* Calculates the Bollinger bands indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate the indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period to calculate the main line. 
        * deviation   -   Deviation from the main line. 
        * bands_shift   -   The indicator shift relative to the chart. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * mode   -   Indicator line index. It can be any of the Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iBands(string _symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Bands>(_symbol, timeframe, mode, shift, period, bands_shift, deviation, applied_price);

            return r;
        }
        public override double iBands(symbol symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Bands>(symbol, timeframe, mode, shift, period, bands_shift, deviation, applied_price);

            return r;
        }

        public override double iBands(string _symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, PriceConstant applied_price, MovingAverageMethod ma_method, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Bands>(_symbol, timeframe, mode, shift, period, bands_shift, deviation, applied_price, ma_method);

            return r;
        }
        public override double iBands(symbol symbol, TimePeriodConst timeframe, int period, double deviation, int bands_shift, PriceConstant applied_price, MovingAverageMethod ma_method, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Bands>(symbol, timeframe, mode, shift, period, bands_shift, deviation, applied_price, ma_method);

            return r;
        }


        /* Calculation of the Bollinger Bands indicator on data stored in a numeric array. Unlike iBands(...), the iBandsOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 0 means the whole array. 
        * period   -   Averaging period for calculation of main line. 
        * deviation   -   Deviation from main line. 
        * bands_shift   -   The indicator shift relative to the chart. 
        * mode   -   Indicator line index. It can be any of the Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iBandsOnArray(DArr array, int total, int period, int deviation, int bands_shift, IndicatorLine mode, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<Bands>(mode, shift, period, bands_shift, deviation, PriceConstant.PRICE_CLOSE, MovingAverageMethod.MODE_SMA);

            return r;
        }

        public override double iBandsOnArray(DArr array, int total, int period, int deviation, int bands_shift, MovingAverageMethod ma_method, IndicatorLine mode, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<Bands>(mode, shift, period, bands_shift, deviation, PriceConstant.PRICE_CLOSE, ma_method);

            return r;
        }



        /* Calculates the Bulls Power indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iBullsPower(string _symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<Bulls>(_symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }
        public override double iBullsPower(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<Bulls>(symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }



        /* Calculates the Commodity channel index and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iCCI(string _symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<CCI>(_symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }
        public override double iCCI(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<CCI>(symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }


        /* Calculation of the Commodity Channel Index on data stored in a numeric array. Unlike iCCI(...), the iCCIOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 
        * period   -   Averaging period for calculation. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iCCIOnArray(DArr array, int total, int period, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<CCI>(0, shift, period);

            return r;
        }



        /* Calculates the DeMarker indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iDeMarker(string _symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<DeMarker>(_symbol, timeframe, 0, shift, period);

            return r;
        }
        public override double iDeMarker(symbol symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<DeMarker>(symbol, timeframe, 0, shift, period);

            return r;
        }



        /* Calculates the Envelopes indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * ma_period   -   Averaging period for calculation of the main line. 
        * ma_method   -   MA method. It can be any of Moving Average method enumeration value. 
        * ma_shift   -   MA shift. Indicator line offset relate to the chart by timeframe. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * deviation   -   Percent deviation from the main line. 
        * mode   -   Indicator line index. It can be any of Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iEnvelopes(string _symbol, TimePeriodConst timeframe, int ma_period, MovingAverageMethod ma_method, int ma_shift, PriceConstant applied_price, double deviation, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Envelopes>(_symbol, timeframe, mode, shift, ma_period, ma_shift, ma_method, applied_price, deviation);

            return r;
        }
        public override double iEnvelopes(symbol symbol, TimePeriodConst timeframe, int ma_period, MovingAverageMethod ma_method, int ma_shift, PriceConstant applied_price, double deviation, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Envelopes>(symbol, timeframe, mode, shift, ma_period, ma_shift, ma_method, applied_price, deviation);

            return r;
        }



        /* Calculation of the Envelopes indicator on data stored in a numeric array. Unlike iEnvelopes(...), the iEnvelopesOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 
        * ma_period   -   Averaging period for calculation of the main line. 
        * ma_method   -   MA method. It can be any of Moving Average method enumeration value. 
        * ma_shift   -   MA shift. Indicator line offset relate to the chart by timeframe. 
        * deviation   -   Percent deviation from the main line. 
        * mode   -   Indicator line index. It can be any of Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iEnvelopesOnArray(DArr array, int total, int ma_period, MovingAverageMethod ma_method, int ma_shift, double deviation, IndicatorLine mode, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<Envelopes>(0, shift, ma_period, ma_shift, ma_method, PriceConstant.PRICE_CLOSE, deviation);

            return r;
        }




        /* Calculates the Force index and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * ma_method   -   MA method. It can be any of Moving Average method enumeration value. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iForce(string _symbol, TimePeriodConst timeframe, int period, MovingAverageMethod ma_method, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<ForceIndex>(_symbol, timeframe, 0, shift, period, ma_method, applied_price);

            return r;
        }
        public override double iForce(symbol symbol, TimePeriodConst timeframe, int period, MovingAverageMethod ma_method, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<ForceIndex>(symbol, timeframe, 0, shift, period, ma_method, applied_price);

            return r;
        }




        /* Calculates the Fractals and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * mode   -   Indicator line index. It can be any of the Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iFractals(string _symbol, TimePeriodConst timeframe, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Fractals>(_symbol, timeframe, mode, shift);

            return r;
        }
        public override double iFractals(symbol symbol, TimePeriodConst timeframe, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Fractals>(symbol, timeframe, mode, shift);

            return r;
        }



        /* Gator oscillator calculation. The oscillator displays the difference between the Alligator red and blue lines (the upper histogram) and that between red and green lines (the lower histogram). 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * jaw_period   -   Blue line averaging period (Alligator's Jaw). 
        * jaw_shift   -   Blue line shift relative to the chart. 
        * teeth_period   -   Red line averaging period (Alligator's Teeth). 
        * teeth_shift   -   Red line shift relative to the chart. 
        * lips_period   -   Green line averaging period (Alligator's Lips). 
        * lips_shift   -   Green line shift relative to the chart. 
        * ma_method   -   MA method. It can be any of Moving Average method enumeration value. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * mode   -   Indicator line index. It can be any of Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iGator(string _symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Gator>(_symbol, timeframe, mode, shift, jaw_period, jaw_shift, teeth_period, teeth_shift, lips_period, lips_shift, ma_method, applied_price);

            return r;
        }
        public override double iGator(symbol symbol, TimePeriodConst timeframe, int jaw_period, int jaw_shift, int teeth_period, int teeth_shift, int lips_period, int lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Gator>(symbol, timeframe, mode, shift, jaw_period, jaw_shift, teeth_period, teeth_shift, lips_period, lips_shift, ma_method, applied_price);

            return r;
        }




        /* Calculates the Ichimoku Kinko Hyo and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * tenkan_sen   -   Tenkan Sen averaging period. 
        * kijun_sen   -   Kijun Sen averaging period. 
        * senkou_span_b   -   Senkou SpanB averaging period. 
        * mode   -   Source of data. It can be one of the Ichimoku Kinko Hyo mode enumeration. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iIchimoku(string _symbol, TimePeriodConst timeframe, int tenkan_sen, int kijun_sen, int senkou_span_b, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Ichimoku>(_symbol, timeframe, mode, shift, tenkan_sen, kijun_sen, senkou_span_b);

            return r;
        }
        public override double iIchimoku(symbol symbol, TimePeriodConst timeframe, int tenkan_sen, int kijun_sen, int senkou_span_b, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Ichimoku>(symbol, timeframe, mode, shift, tenkan_sen, kijun_sen, senkou_span_b);

            return r;
        }



        /* Calculates the Bill Williams Market Facilitation index and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iBWMFI(string _symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<MFI_BW>(_symbol, timeframe, 0, shift);

            return r;
        }
        public override double iBWMFI(symbol symbol, TimePeriodConst timeframe, int shift)
        {
            double r = GetIndicatorValue<MFI_BW>(symbol, timeframe, 0, shift);

            return r;
        }



        /* Calculates the Momentum indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator.NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Period (amount of bars) for calculation of price changes. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iMomentum(string _symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<Momentum>(_symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }
        public override double iMomentum(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<Momentum>(symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }



        /* Calculation of the Momentum indicator on data stored in a numeric array. Unlike iMomentum(...), the iMomentumOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 
        * period   -   Period (amount of bars) for calculation of price changes. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iMomentumOnArray(DArr array, int total, int period, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<Momentum>(0, shift, period);

            return r;
        }




        /* Calculates the Money flow index and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Period (amount of bars) for calculation of the indicator. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iMFI(string _symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<MFI>(_symbol, timeframe, 0, shift, period);

            return r;
        }
        public override double iMFI(symbol symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<MFI>(symbol, timeframe, 0, shift, period);

            return r;
        }



        /* Calculates the Moving average indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Averaging period for calculation. 
        * ma_shift   -   MA shift. Indicators line offset relate to the chart by timeframe. 
        * ma_method   -   MA method. It can be any of the Moving Average method enumeration value. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iMA(string _symbol, TimePeriodConst timeframe, int period, int ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<MovingAvg>(_symbol, timeframe, 0, shift, period, ma_shift, ma_method, applied_price);

            return r;
        }
        public override double iMA(symbol symbol, TimePeriodConst timeframe, int period, int ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<MovingAvg>(symbol, timeframe, 0, shift, period, ma_shift, ma_method, applied_price);

            return r;
        }

        public override double iPrice(string _symbol, TimePeriodConst timeframe, int period, PriceConstant type, int shift)
        {
            double r = GetIndicatorValue<MovingAvg>(_symbol, timeframe, 0, shift, period, type);

            return r;
        }
        public override double iPrice(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant type, int shift)
        {
            double r = GetIndicatorValue<MovingAvg>(symbol, timeframe, 0, shift, period, type);

            return r;
        }


        /* Calculation of the Moving Average on data stored in a numeric array. Unlike iMA(...), the iMAOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 0 means whole array. 
        * period   -   Averaging period for calculation. 
        * ma_shift   -   MA shift 
        * ma_method   -   MA method. It can be any of the Moving Average method enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iMAOnArray(DArr array, int total, int period, int ma_shift, MovingAverageMethod ma_method, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<MovingAvg>(0, shift, period, ma_shift, ma_method, PriceConstant.PRICE_CLOSE);

            return r;
        }



        /* Calculates the Moving Average of Oscillator and returns its value. Sometimes called MACD Histogram in some systems. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * fast_ema_period   -   Number of periods for fast moving average calculation. 
        * slow_ema_period   -   Number of periods for slow moving average calculation. 
        * signal_period   -   Number of periods for signal moving average calculation. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iOsMA(string _symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<OsMA>(_symbol, timeframe, 0, shift, fast_ema_period, slow_ema_period, signal_period, applied_price);

            return r;
        }
        public override double iOsMA(symbol symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<OsMA>(symbol, timeframe, 0, shift, fast_ema_period, slow_ema_period, signal_period, applied_price);

            return r;
        }



        /* Calculates the Moving averages convergence/divergence and returns its value. In the systems where OsMA is called MACD Histogram, this indicator is displayed as two lines. In the Client Terminal, the Moving Average Convergence/Divergence is drawn as a histogram. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * fast_ema_period   -   Number of periods for fast moving average calculation. 
        * slow_ema_period   -   Number of periods for slow moving average calculation. 
        * signal_period   -   Number of periods for signal moving average calculation. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * mode   -   Indicator line index. It can be any of the Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iMACD(string _symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<MACD>(_symbol, timeframe, mode, 0, shift, fast_ema_period, slow_ema_period, signal_period, applied_price);

            return r;
        }
        public override double iMACD(symbol symbol, TimePeriodConst timeframe, int fast_ema_period, int slow_ema_period, int signal_period, PriceConstant applied_price, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<MACD>(symbol, timeframe, mode, 0, shift, fast_ema_period, slow_ema_period, signal_period, applied_price);

            return r;
        }



        /* Calculates the On Balance Volume indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iOBV(string _symbol, TimePeriodConst timeframe, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<OBV>(_symbol, timeframe, 0, shift, applied_price);

            return r;
        }
        public override double iOBV(symbol symbol, TimePeriodConst timeframe, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<OBV>(symbol, timeframe, 0, shift, applied_price);

            return r;
        }



        /* Calculates the Parabolic Stop and Reverse system and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * step   -   Increment, usually 0.02. 
        * maximum   -   Maximum value, usually 0.2. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iSAR(string _symbol, TimePeriodConst timeframe, double step, double maximum, int shift)
        {
            double r = GetIndicatorValue<Parabolic>(_symbol, timeframe, 0, shift, step, maximum);

            return r;
        }
        public override double iSAR(symbol symbol, TimePeriodConst timeframe, double step, double maximum, int shift)
        {
            double r = GetIndicatorValue<Parabolic>(symbol, timeframe, 0, shift, step, maximum);

            return r;
        }




        /* Calculates the Relative strength index and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Number of periods for calculation. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iRSI(string _symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<RSI>(_symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }
        public override double iRSI(symbol symbol, TimePeriodConst timeframe, int period, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<RSI>(symbol, timeframe, 0, shift, period, applied_price);

            return r;
        }



        /* Calculation of the Relative Strength Index on data stored in a numeric array. Unlike iRSI(...), the iRSIOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 
        * period   -   Number of periods for calculation. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iRSIOnArray(DArr array, int total, int period, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<RSI>(0, shift, period);

            return r;
        }



        /* Calculates the Relative Vigor index and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Number of periods for calculation. 
        * mode   -   Indicator line index. It can be any of Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iRVI(string _symbol, TimePeriodConst timeframe, int period, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<RVI>(_symbol, timeframe, mode, shift, period);

            return r;
        }
        public override double iRVI(symbol symbol, TimePeriodConst timeframe, int period, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<RVI>(symbol, timeframe, mode, shift, period);

            return r;
        }



        /* Calculates the Standard Deviation indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * ma_period   -   MA period. 
        * ma_shift   -   MA shift. 
        * ma_method   -   MA method. It can be any of Moving Average method enumeration value. 
        * applied_price   -   Applied price. It can be any of Applied price enumeration values. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iStdDev(string _symbol, TimePeriodConst timeframe, int ma_period, int ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<StDev>(_symbol, timeframe, 0, shift, ma_period, ma_method, applied_price, ma_shift);

            return r;
        }
        public override double iStdDev(symbol symbol, TimePeriodConst timeframe, int ma_period, int ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, int shift)
        {
            double r = GetIndicatorValue<StDev>(symbol, timeframe, 0, shift, ma_period, ma_method, applied_price, ma_shift);

            return r;
        }



        /* Calculation of the Standard Deviation indicator on data stored in a numeric array. Unlike iStdDev(...), the iStdDevOnArray function does not take data by symbol name, timeframe, the applied price. The price data must be previously prepared. The indicator is calculated from left to right. To access to the array elements as to a series array (i.e., from right to left), one has to use the ArraySetAsSeries function. 
        * Parameters:
        * array[]   -   Array with data. 
        * total   -   The number of items to be counted. 
        * ma_period   -   MA period. 
        * ma_shift   -   MA shift. 
        * ma_method   -   MA method. It can be any of Moving Average method enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iStdDevOnArray(DArr array, int total, int ma_period, int ma_shift, MovingAverageMethod ma_method, int shift)
        {
            // TODO total
            ISeriesManagerCache cache2 = GetIndicatorCacheOnArray(array);
            double r = cache2.GetIndicatorValue<StDev>(0, shift, ma_period, ma_method, PriceConstant.PRICE_CLOSE, ma_shift);

            return r;
        }



        /* Calculates the Stochastic oscillator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * %Kperiod   -   %K line period. 
        * %Dperiod   -   %D line period. 
        * slowing   -   Slowing value. 
        * method   -   MA method. It can be any ofMoving Average method enumeration value. 
        * price_field   -   Price field parameter. Can be one of this values: 0 - Low/High or 1 - Close/Close. 
        * mode   -   Indicator line index. It can be any of the Indicators line identifiers enumeration value. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iStochastic(string _symbol, TimePeriodConst timeframe, int _Kperiod, int _Dperiod, int slowing, int method, int price_field, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Stochastic>(_symbol, timeframe, mode, shift, _Kperiod, _Dperiod, slowing);

            return r;
        }
        public override double iStochastic(symbol symbol, TimePeriodConst timeframe, int _Kperiod, int _Dperiod, int slowing, int method, int price_field, IndicatorLine mode, int shift)
        {
            double r = GetIndicatorValue<Stochastic>(symbol, timeframe, mode, shift, _Kperiod, _Dperiod, slowing);

            return r;
        }



        /* Calculates the Larry William's percent range indicator and returns its value. 
        * Parameters:
        * symbol   -   Symbol the data of which should be used to calculate indicator. NULL means the current symbol. 
        * timeframe   -   Timeframe. It can be any of Timeframe enumeration values. 0 means the current chart timeframe. 
        * period   -   Number of periods for calculation. 
        * shift   -   Index of the value taken from the indicator buffer (shift relative to the current bar the given amount of periods ago). 
        */
        public override double iWPR(string _symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<WPR>(_symbol, timeframe, 0, shift, period);

            return r;
        }
        public override double iWPR(symbol symbol, TimePeriodConst timeframe, int period, int shift)
        {
            double r = GetIndicatorValue<WPR>(symbol, timeframe, 0, shift, period);

            return r;
        }


        // TODO impl ...

        public override int WindowFind(string name)
        {
            foreach (var ind in Parent.GuiSeriesManager.DefaultCache)
            {
            }
            return -1;
        }
        public override void WindowRedraw()
        {
        }

        public override bool ObjectCreate(string name, GraphObjectType type, int window, datetime time1, double price1, int time2 = 0, double price2 = 0, int time3 = 0, double price3 = 0)
        {
            return false;
        }

        public override GraphObjectType ObjectType(string name)
        {
            return GraphObjectType.OBJ_NONE;
        }

        public override int ObjectFind(string name)
        {
            return -1;
        }

        public override string ObjectName(GraphObjectProperty index)
        {
            return null;
        }

        public override int ObjectsTotal(int type = EMPTY)
        {
            return 0;
        }

        public override bool ObjectMove(string name, int point, datetime time1, double price1)
        {
            return false;
        }

        public override double ObjectGet(string name, GraphObjectProperty index)
        {
            return 0;
        }

        public override bool ObjectSet(string name, GraphObjectProperty index, double value)
        {
            return false;
        }
        public override bool ObjectSet(string name, GraphObjectProperty index, int value)
        {
            return false;
        }
        public override bool ObjectSet(string name, GraphObjectProperty index, DrawingStylesWidth1 value)
        {
            return false;
        }
        public override bool ObjectSet(string name, GraphObjectProperty index, Color value)
        {
            return false;
        }

        public override bool ObjectSetText(string name, string text, int font_size = 0, string font = null, Color text_color = default(Color))
        {
            return false;
        }

        public override int ObjectsDeleteAll(int window = EMPTY, GraphObjectType type = GraphObjectType.OBJ_NONE)
        {
            return 0;
        }
        public override bool ObjectDelete(string name)
        {
            return false;
        }
        public override void ObjectsRedraw()
        {
            WindowRedraw();
        }







        public const int SEV_ERROR = 0;
        public const int SEV_WARNING = 1;
        public const int SEV_INFO = 2;
        public const int SEV_DEBUG_1 = 3;
        public const int SEV_DEBUG_2 = 4;
        public const int SEV_DEBUG_3 = 5;
        public const int SEV_DEBUG_4 = 6;
        public const int SEV_DEBUG_5 = 7;
        public const bool Use_File_Log = false;
        public const int Max_Severity = SEV_INFO;
        public readonly string[] LogSeverity_Str = new string[] { "ERROR", "WARNING", "INFO", "DEBUG(1)", "DEBUG(2)", "DEBUG(3)", "DEBUG(4)", "DEBUG(5)" };


        public override bool CompareDouble(double A, double B)
        {
            bool Compare = NormalizeDouble(A - B, 8) == 0;
            return (Compare);
        }

        public override int i(bool b)
        {
            return b ? 1 : 0;
        }

        public override double nd(double d)
        {
            return Math.Round(d, Digits);
        }

        public override string ErrorDescription(int e)
        {
            return Errors.ErrorDescription(e);
        }

        public override void printError(string err)
        {
            int e = GetLastError();
            printLog(err + "  error #" + e + " : " + ErrorDescription(e), SEV_ERROR);
        }

        public override void printLog(string msg, int severity)
        {
            Print(LogSeverity_Str[severity] + " " + msg);
        }
    }
}
