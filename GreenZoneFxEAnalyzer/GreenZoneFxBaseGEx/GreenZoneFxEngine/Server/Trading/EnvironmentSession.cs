using System;
using System.Collections.Generic;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Reflection;
using System.Drawing;
using System.Linq;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.Util;
using GreenZoneUtil.GreenRmi;

namespace GreenZoneFxEngine.Trading
{

    public class ServerEnvironmentSession : ServerEnvironmentSessionBase
    {
        internal ServerEnvironmentSession(GreenRmiManager rmiManager, string environmentId)
            : base(rmiManager)
        {
            EnvironmentId = environmentId;
            InitNonSerials();
        }

        protected ServerEnvironmentSession(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private void InitNonSerials()
        {
            if (SymbolSessions == null)
            {
                SymbolSessions = new Dictionary<symbol, ISymbolSession>();
            }
            if (ChartSessions == null)
            {
                ChartSessions = new List<IChartGroupSession>();
            }
            if (OrderFilter == null)
            {
                OrderFilter = new OrderFilter(rmiManager);
            }
        }

        [Category("EA testing")]
        public override int EATestingProgress
        {
            get
            {
                int d = EATestingGlobalTo - EATestingGlobalFrom;
                int ttd = Time - EATestingGlobalFrom;
                int progress = 100 * ttd / d;
                return progress;
            }
        }

        object tmlck = new object();
        [Category("EA testing")]
        public override datetime Time
        {
            get
            {
                lock (tmlck)
                {
                    return base.Time;
                }
            }
            set
            {
                lock (tmlck)
                {
                    base.Time = value;
                }
            }
        }

        internal static ServerEnvironmentSession Load(GreenRmiManager rmiManager, string environmentId)
        {
            FileStream envStr = null;
            GZipStream zipStr = null;
            try
            {
                string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                Directory.CreateDirectory(eaRootDir);
                StreamingContext sc = new StreamingContext(StreamingContextStates.Persistence, rmiManager);
                BinaryFormatter deserializer = new BinaryFormatter();
                deserializer.Context = sc;

                string envDir = eaRootDir + "\\" + environmentId;
                Directory.CreateDirectory(envDir);

                envStr = File.Open(envDir + "\\session.dat", FileMode.Open, FileAccess.Read);
                zipStr = new GZipStream(envStr, CompressionMode.Decompress);
                ServerEnvironmentSession env = (ServerEnvironmentSession)deserializer.Deserialize(zipStr);
                env.InitNonSerials();
                return env;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to load environment session : " + environmentId, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            finally
            {
                if (zipStr != null)
                    zipStr.Close();
                if (envStr != null)
                    envStr.Close();
            }
        }

        public override bool Save()
        {
            FileStream envStr = null;
            GZipStream zipStr = null;
            try
            {
                string eaRootDir = EAnalyzerOptions.Singleton.EAnalyzerDirectory.ToString();
                Directory.CreateDirectory(eaRootDir);
                BinaryFormatter serializer = new BinaryFormatter();

                string envDir = eaRootDir + "\\" + EnvironmentId;
                Directory.CreateDirectory(envDir);

                envStr = File.Open(envDir + "\\session.dat", FileMode.Create, FileAccess.Write);
                zipStr = new GZipStream(envStr, CompressionMode.Compress);
                serializer.Serialize(zipStr, this);
                return true;
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to save environment session : " + EnvironmentId, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                if (zipStr != null)
                    zipStr.Close();
                if (envStr != null)
                    envStr.Close();
            }
        }

        public override void AddSymbol(ISymbolSession symbolSession)
        {
            SymbolSessions[symbolSession.Symbol] = symbolSession;
        }

        public override void AddChart(IChartGroupSession chartSession)
        {
            ChartSessions.Add(chartSession);
        }

        public override void RemoveChart(IChartGroupSession chart)
        {
            ChartSessions.Remove(chart);
        }
    }

    public class OrderFilter : OrderFilterBase, ICloneable
    {
		public OrderFilter(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
		}

		public OrderFilter(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
		}

		protected OrderFilter(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
        }

        public override object GroupField
        {
            get
            {
                switch (GroupBy)
                {
                    case "Expert": return Expert;
                    case "Symbol": return Symbol;
                    case "Ticket": return Ticket;

                    default: return null;
                }
            }
            set
            {
                switch (GroupBy)
                {
                    case "Expert": Expert = (string)value; break;
                    case "Symbol": Symbol = (string)value; break;
                    case "Ticket": Ticket = (int)value; break;

                    default: break;
                }
            }
        }

        public void Clear()
        {
            OrderFilter o = new OrderFilter(rmiManager);
            o.CopyTo(this);
        }

        public OrderFilter Clone()
        {
            OrderFilter o = new OrderFilter(rmiManager);
            CopyTo(o);
            return o;
        }

        object ICloneable.Clone()
        {
            OrderFilter o = Clone();
            return o;
        }

        public void CopyTo(OrderFilter o)
        {
            o.GroupBy = GroupBy;
            o.Buy = Buy;
            o.Sell = Sell;
            o.Limit = Limit;
            o.Stop = Stop;
            o.From = From;
            o.To = To;
            o.More = More;
            o.Ticket = Ticket;
            o.Symbol = Symbol;
            o.Operation = Operation;
            o.Expert = Expert;
            o.Magic = Magic;
            o.Comment = Comment;
            o.HiddenColumns.Clear();
            foreach (var c in HiddenColumns)
            {
                o.HiddenColumns.Add(c);
            }
        }
    }

}
