using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{

    public abstract class ServerTimeSeriesRuntimeEx : ServerTimeSeriesRuntimeBase
    {

        protected readonly string path;

        protected bool inputFileIsRead;

        protected readonly int bufSz;


        internal ServerTimeSeriesRuntimeEx(ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime, int headerLen, int recordLen, string path)
            : base(rmiManager)
        {
            IEnvironmentRuntime environment = parent.Parent;
            bufSz = environment.ProgramOptions.BufferSize;
            if ("history".Equals(environment.HistoryDirectory) || string.IsNullOrEmpty(environment.HistoryDirectory))
            {
                this.path = environment.ImportedFromDirectory + "\\history\\" + path;
            }
            else
            {
                this.path = environment.ImportedFromDirectory + "\\history\\" + environment.HistoryDirectory + "\\" + path;
            }

            HeaderLen = headerLen;
            RecordLen = recordLen;

            Init(parent, period, online, focusedTime);
        }

        internal ServerTimeSeriesRuntimeEx(string envHistoryDir, TimePeriodConst period, bool online, datetime focusedTime, int headerLen, int recordLen, string path)
            : base(rmiManager)
        {
            bufSz = EAnalyzerOptions.Singleton.BufferSize;

            if (!envHistoryDir.EndsWith("\\") && !envHistoryDir.EndsWith("/"))
            {
                this.path = envHistoryDir + "\\" + path;
            }
            else
            {
                this.path = envHistoryDir + path;
            }

            HeaderLen = headerLen;
            RecordLen = recordLen;
            Init(null, period, online, focusedTime);
        }

        internal ServerTimeSeriesRuntimeEx(string path, bool online, datetime focusedTime, int headerLen, int recordLen)
            : base(rmiManager)
        {
            bufSz = EAnalyzerOptions.Singleton.BufferSize;

            this.path = path;

            HeaderLen = headerLen;
            RecordLen = recordLen;
            Init(null, TimePeriodConst.PERIOD_CURRENT, online, focusedTime);
        }

		public ServerTimeSeriesRuntimeEx(GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

        protected ServerTimeSeriesRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
        }

        void Init(ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime)
        {
            Volume = null;
            Begin_time = null;

            Parent = parent;
            Period = period;

            if (parent != null)
            {
                Symbol = parent.Symbol;
            }
            Period = period;
            Online = online;

            RecordCount = 0;
            FileOffset = 0;
            FileOffsetTo = 0;
            IndOffset = 0;
            IndCount = 0;

            inputFileIsRead = false;
            Load(focusedTime);
        }

        public override datetime GetTime(int i)
        {
            // NOTE reverse indexing...
            return (datetime)Begin_time[IndOffset + i];
        }

        public override double GetVolume(int i)
        {
            // NOTE reverse indexing...
            return Volume[IndOffset + i];
        }


        protected override void LoadFile()
        {
            if (!inputFileIsRead && !File.Exists(path))
            {
                throw new TimeSeriesException("File doesn't exist:" + path);
            }

            FileStream hstFile = null;
            BinaryReader hstReader;

            try
            {

                hstFile = File.Open(path, FileMode.Open, FileAccess.Read);
                hstReader = new BinaryReader(hstFile);

                long len = hstFile.Length;
                if ((len - HeaderLen) % RecordLen != 0)
                {
                    throw new TimeSeriesException("Wrong hst : " + path + " (len - HeaderLen) % RecordLen != 0   len:" + len + " HeaderLen:" + HeaderLen + " RecordLen:" + RecordLen + " %:" + ((len - HeaderLen) % RecordLen));
                }
                RecordCount = (len - HeaderLen) / RecordLen;

                IRecord cur = CreateRecord();

                hstFile.Seek(HeaderLen, SeekOrigin.Begin);
                cur.Load(hstReader);
                From = cur.Time;

                hstFile.Seek(-RecordLen, SeekOrigin.End);
                cur.Load(hstReader);
                To = cur.Time;

                inputFileIsRead = true;
            }
            finally
            {
                CloseFile(hstFile);
            }
        }

        long findDateInFile(datetime focusedTime)
        {

            FileStream hstFile = null;
            BinaryReader hstReader;

            try
            {
                hstFile = File.Open(path, FileMode.Open, FileAccess.Read);
                hstReader = new BinaryReader(hstFile);

                IRecord cur = CreateRecord();

                long a = 0;
                long b = RecordCount - 1;

                while (a <= b)
                {
                    long mid = (a + b) / 2;
                    hstFile.Seek(HeaderLen + mid * RecordLen, SeekOrigin.Begin);
                    cur.Load(hstReader);

                    switch (Math.Sign(datetime.Subtract(cur.Time, focusedTime)))
                    {
                        case 0:
                            a = mid + 1;
                            b = mid;
                            break;
                        case -1:
                            a = mid + 1;
                            break;
                        default:
                            b = mid - 1;
                            break;
                    }
                }
                b = Math.Max(b, 0);
                //b = Math.Min(b, RecordCount - 1);  (always true by default)
                return b;
            }
            finally
            {
                CloseFile(hstFile);
            }
        }

        protected override void FillFromFile(long tot_ind)
        {
            FileStream hstFile = null;
            BinaryReader hstReader;

            try
            {
                hstFile = File.Open(path, FileMode.Open, FileAccess.Read);
                hstReader = new BinaryReader(hstFile);
                IRecord cur = CreateRecord();

                FileOffset = Math.Max(0, tot_ind - bufSz);
                FileOffsetTo = Math.Min(RecordCount - 1, tot_ind + bufSz);
                // NOTE reverse indexing
                IndOffset = (int)(FileOffsetTo - tot_ind);
                IndCount = (int)(FileOffsetTo - FileOffset + 1);

                InitArrays();
                Volume = new DArr(IndCount);
                Begin_time = new LArr(IndCount);

                hstFile.Seek(HeaderLen + FileOffset * (long)RecordLen, SeekOrigin.Begin);

                long i = FileOffset;
                for (int j = IndCount - 1; i <= FileOffsetTo; i++, j--)
                {
                    cur.Load(hstReader);
                    LoadArrayItem(cur, j);
                    Volume[j] = cur.Volume;
                    Begin_time[j] = (long)cur.Time;
                }
            }
            finally
            {
                CloseFile(hstFile);
            }
        }

        void CloseFile(FileStream hstFile)
        {
            try
            {
                if (hstFile != null)
                {
                    hstFile.Close();
                }
            }
            catch (IOException ex)
            {
                throw new TimeSeriesException("IO error:" + path, ex);
            }
        }


        protected abstract IRecord CreateRecord();

        protected abstract void InitArrays();

        protected abstract void LoadArrayItem(IRecord cur, int index);


        public static ITimeSeriesRuntime Create(ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime)
        {
            ITimeSeriesRuntime result;
            if (period.GetCategory() == TimePeriodCategory.TICKS)
            {
                result = new ServerTickTimeSeriesRuntime(rmiManager, parent, period, online, focusedTime);
            }
            else
            {
                result = new ServerPeriodTimeSeriesRuntime(rmiManager, parent, period, online, focusedTime);
            }
            return result;
        }

        public static ITimeSeriesRuntime Create(string envHistoryDir, string symbol, TimePeriodConst period, bool online, datetime focusedTime)
        {
            ITimeSeriesRuntime result;
            if (period.GetCategory() == TimePeriodCategory.TICKS)
            {
                result = new ServerTickTimeSeriesRuntime(rmiManager, envHistoryDir, symbol, period, online, focusedTime);
            }
            else
            {
                result = new ServerPeriodTimeSeriesRuntime(rmiManager, envHistoryDir, symbol, period, online, focusedTime);
            }
            return result;
        }

        public static bool IsSeriesAvailable(IEnvironmentRuntime environment, symbol symbol, TimePeriodConst period)
        {
            string envHistDir;
            if ("history".Equals(environment.HistoryDirectory) || string.IsNullOrEmpty(environment.HistoryDirectory))
            {
                envHistDir = environment.ImportedFromDirectory + "\\history\\";
            }
            else
            {
                envHistDir = environment.ImportedFromDirectory + "\\history\\" + environment.HistoryDirectory + "\\";
            }
            bool result = IsSeriesAvailable(envHistDir, symbol.ToString(), period);
            return result;
        }

        public static bool IsSeriesAvailable(string envHistoryDir, string symbol, TimePeriodConst period)
        {
            string path;
            if (!envHistoryDir.EndsWith("\\") && !envHistoryDir.EndsWith("/"))
            {
                path = envHistoryDir + "\\";
            }
            else
            {
                path = envHistoryDir;
            }

            string file;
            if (period.GetCategory() == TimePeriodCategory.TICKS)
            {
                file = "" + symbol + ".ticks";
            }
            else
            {
                file = "" + symbol + (int)period + ".hst";
            }
            bool result = File.Exists(path + file);
            return result;
        }


        public static long GetRecordCount(IEnvironmentRuntime environment, symbol symbol, TimePeriodConst period)
        {
            string envHistDir;
            if ("history".Equals(environment.HistoryDirectory) || string.IsNullOrEmpty(environment.HistoryDirectory))
            {
                envHistDir = environment.ImportedFromDirectory + "\\history\\";
            }
            else
            {
                envHistDir = environment.ImportedFromDirectory + "\\history\\" + environment.HistoryDirectory + "\\";
            }
            long result = GetRecordCount(envHistDir, symbol.ToString(), period);
            return result;
        }

        public static long GetRecordCount(string envHistoryDir, string symbol, TimePeriodConst period)
        {
            string path;
            if (!envHistoryDir.EndsWith("\\") && !envHistoryDir.EndsWith("/"))
            {
                path = envHistoryDir + "\\";
            }
            else
            {
                path = envHistoryDir;
            }

            string file;
            int headerLen, recordLen;
            if (period.GetCategory() == TimePeriodCategory.TICKS)
            {
                file = "" + symbol + ".ticks";
                headerLen = 0;
                recordLen = TickSeriesRecord.RecordLen;
            }
            else
            {
                file = "" + symbol + (int)period + ".hst";
                headerLen = 4 + 64 + 12 + 4 + 4 + 15 * 4;
                recordLen = PeriodSeriesRecord.RecordLen;
            }

            long resultLen;

            if (File.Exists(path + file))
            {
                resultLen = (new FileInfo(path + file).Length - headerLen) / recordLen;
            }
            else
            {
                resultLen = 0;
            }
            return resultLen;
        }


        protected override void LoadFile(datetime focusedTime)
        {
            LoadFile();

            FillFromFile(focusedTime);
        }

        protected override void FillFromFile(datetime focusedTime)
        {
            long tot_ind = findDateInFile(focusedTime);
            FillFromFile(tot_ind);
        }
    }

    public class ServerPeriodTimeSeriesRuntime : ServerPeriodTimeSeriesRuntimeBase
    {
        const int headerLen = 4 + 64 + 12 + 4 + 4 + 15 * 4;

        internal ServerPeriodTimeSeriesRuntime(ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime)
            : base(rmiManager, parent, period, online, focusedTime, headerLen, PeriodSeriesRecord.RecordLen, "" + parent.Symbol + (int)period + ".hst")
        {
        }
        internal ServerPeriodTimeSeriesRuntime(string envHistoryDir, string symbol, TimePeriodConst period, bool online, datetime focusedTime)
            : base(rmiManager, envHistoryDir, period, online, focusedTime, headerLen, PeriodSeriesRecord.RecordLen, "" + symbol + (int)period + ".hst")
        {
        }
        public ServerPeriodTimeSeriesRuntime(string path, bool online, datetime focusedTime)
            : base(rmiManager, path, online, focusedTime, headerLen, PeriodSeriesRecord.RecordLen)
        {
        }

        public override double GetOpen(int i)
        {
            // NOTE reverse indexing...
            return Open[IndOffset + i];
        }

        public override double GetLow(int i)
        {
            // NOTE reverse indexing...
            return Low[IndOffset + i];
        }

        public override double GetHigh(int i)
        {
            // NOTE reverse indexing...
            return High[IndOffset + i];
        }

        public override double GetClose(int i)
        {
            // NOTE reverse indexing...
            return Close[IndOffset + i];
        }
        
        protected override IRecord CreateRecord()
        {
            return new PeriodSeriesRecord();
        }

        protected override void InitArrays()
        {
            Open = new DArr(IndCount);
            Low = new DArr(IndCount);
            High = new DArr(IndCount);
            Close = new DArr(IndCount);
        }

        protected override void LoadArrayItem(IRecord _cur, int index)
        {
            PeriodSeriesRecord cur = (PeriodSeriesRecord)_cur;
            Open[index] = cur.open;
            Low[index] = cur.low;
            High[index] = cur.high;
            Close[index] = cur.close;
        }
    }

    public class ServerTickTimeSeriesRuntime : ServerTickTimeSeriesRuntimeBase
    {
        const int headerLen = 0;

        internal ServerTickTimeSeriesRuntime(ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime)
            : base(rmiManager, parent, period, online, focusedTime, headerLen, TickSeriesRecord.RecordLen, "" + parent.Symbol + ".ticks")
        {
            if (period.GetCategory() != TimePeriodCategory.TICKS)
            {
                throw new NotSupportedException();
            }
        }
        internal ServerTickTimeSeriesRuntime(string envHistoryDir, string symbol, TimePeriodConst period, bool online, datetime focusedTime)
            : base(rmiManager, envHistoryDir, period, online, focusedTime, headerLen, TickSeriesRecord.RecordLen, "" + symbol + ".ticks")
        {
            if (period.GetCategory() != TimePeriodCategory.TICKS)
            {
                throw new NotSupportedException();
            }
        }
        public ServerTickTimeSeriesRuntime(string path, bool online, datetime focusedTime)
            : base(rmiManager, path, online, focusedTime, headerLen, TickSeriesRecord.RecordLen)
        {
        }

        public override double GetBid(int i)
        {
            // NOTE reverse indexing...
            return Bid[IndOffset + i];
        }

        public override double GetAsk(int i)
        {
            // NOTE reverse indexing...
            return Ask[IndOffset + i];
        }

        protected override IRecord CreateRecord()
        {
            return new TickSeriesRecord();
        }

        protected override void InitArrays()
        {
            Bid = new DArr(IndCount);
            Ask = new DArr(IndCount);
        }

        protected override void LoadArrayItem(IRecord _cur, int index)
        {
            TickSeriesRecord cur = (TickSeriesRecord)_cur;
            Bid[index] = cur.bid;
            Ask[index] = cur.ask;
        }
    }

    public interface IRecord
    {
        void Load(BinaryReader r);

        double Volume
        {
            get;
        }
        datetime Time
        {
            get;
        }
    }

    internal struct PeriodSeriesRecord : IRecord
    {
        internal const int RecordLen = 4 + 8 + 8 + 8 + 8 + 8;

        internal int time;
        internal double open;
        internal double low;
        internal double high;
        internal double close;
        internal double volume;
        internal datetime dttime;

        public void Load(BinaryReader r)
        {
            dttime = time = r.ReadInt32();
            open = r.ReadDouble();
            low = r.ReadDouble();
            high = r.ReadDouble();
            close = r.ReadDouble();
            volume = r.ReadDouble();
        }

        public datetime Time
        {
            get
            {
                return dttime;
            }
        }
        public double Volume
        {
            get
            {
                return volume;
            }
        }
    }

    internal struct TickSeriesRecord : IRecord
    {
        internal const int RecordLen = 20;
        internal long time;
        internal double volume;
        internal double bid;
        internal double ask;
        internal datetime dttime;

        public void Load(BinaryReader r)
        {
            time = r.ReadInt64();
            dttime = (datetime)time;
            volume = r.ReadSingle();
            bid = r.ReadSingle();
            ask = r.ReadSingle();
        }

        public datetime Time
        {
            get
            {
                return dttime;
            }
        }
        public double Volume
        {
            get
            {
                return volume;
            }
        }
    }

}