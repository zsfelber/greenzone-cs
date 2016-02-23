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
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{

    public class ServerEnvironmentRuntime : ServerEnvironmentRuntimeBase
    {

        [NonSerialized]
        EaTestManager testManager;

        [NonSerialized]
        // TODO load starting value
        int currentTicket;

        public ServerEnvironmentRuntime(GreenRmiManager rmiManager, string envId)
            : base(rmiManager)
        {
            EnvironmentId = envId;

            SymbolContexts = new Dictionary<symbol, ISymbolContext>();
            Scripts = new Dictionary<string, Mt4ExecutableInfo>();
            Experts = new Dictionary<string, Mt4ExecutableInfo>();
            Indicators = new Dictionary<string, Mt4ExecutableInfo>();
            Charts = new List<IChartGroupRuntime>();
            Orders = new ServerOrdersTable(rmiManager, this);

            Session = new ServerEnvironmentSession(rmiManager, envId);

            registerSystemExecutables();
        }

        protected ServerEnvironmentRuntime(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private void InitNonSerials()
        {
            SymbolContexts = new Dictionary<symbol, ISymbolContext>();
            Scripts = new Dictionary<string, Mt4ExecutableInfo>();
            Experts = new Dictionary<string, Mt4ExecutableInfo>();
            Indicators = new Dictionary<string, Mt4ExecutableInfo>();
            Charts = new List<IChartGroupRuntime>();

            Orders = new ServerOrdersTable(rmiManager, this);

            registerSystemExecutables();
        }

        private void registerSystemExecutables()
        {

            List<ReflType> indicators = GreenZoneUtilsBase.GetNamespaceClasses(rmiManager.Resolver, "GreenZoneFxEngine.Trading", typeof(IndicatorAttribute));
            foreach (var i in indicators)
            {
                RegisterIndicator(i);
            }

            List<ReflType> scripts = GreenZoneUtilsBase.GetNamespaceClasses(rmiManager.Resolver, "GreenZoneFxEngine.Trading", typeof(ScriptAttribute));
            foreach (var s in scripts)
            {
                RegisterScript(s);
            }

            List<ReflType> experts = GreenZoneUtilsBase.GetNamespaceClasses(rmiManager.Resolver, "GreenZoneFxEngine.Trading", typeof(ExpertAttribute));
            foreach (var e in experts)
            {
                RegisterEA(e);
            }
        }

        internal static ServerEnvironmentRuntime Load(GreenRmiManager rmiManager, string environmentId)
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

                envStr = File.Open(envDir + "\\environment.dat", FileMode.Open, FileAccess.Read);
                zipStr = new GZipStream(envStr, CompressionMode.Decompress);
                ServerEnvironmentRuntime env = (ServerEnvironmentRuntime)deserializer.Deserialize(zipStr);
                env.InitNonSerials();
                return env;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to load environment : " + environmentId, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public override void Save()
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

                envStr = File.Open(envDir + "\\environment.dat", FileMode.Create, FileAccess.Write);
                zipStr = new GZipStream(envStr, CompressionMode.Compress);
                serializer.Serialize(zipStr, this);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(ex.Message + "\n\nUnable to save environment : " + EnvironmentId, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (zipStr != null)
                    zipStr.Close();
                if (envStr != null)
                    envStr.Close();
            }
        }

        public override void LoadOrders()
        {
            foreach (ServerSymbolContext s in SymbolContexts.Values)
            {
                s.Runtime.LoadOrders();
            }
            Orders.GenerateFromChildren();
        }

        public override void SaveOrders()
        {
            foreach (ServerSymbolContext s in SymbolContexts.Values)
            {
                s.Runtime.SaveOrders();
            }
        }

        [Browsable(false)]
        public override IServerEnvironmentSession Session
        {
            get
            {
                return base.Session;
            }
            set
            {
                base.Session = value;

                Charts.Clear();
                for (int i = 0; i < Session.ChartSessions.Count; i++)
                {
                    var chart = new ServerChartGroupRuntime(rmiManager, this, Session.ChartSessions[i]);
                    Charts.Add(chart);
                }
                foreach (var e in Session.SymbolSessions)
                {
                    ISymbolContext symbolContext;
                    if (SymbolContexts.TryGetValue(e.Key, out symbolContext))
                    {
                        symbolContext.Runtime = new ServerSymbolRuntime(rmiManager, this, (IServerSymbolContext)symbolContext, e.Value);
                    }
                }
            }
        }

        public override void SetImportDir(string importDir, object tester)
        {
            MemberInfo info = tester.GetType();
            object[] attributes = info.GetCustomAttributes(true);

            foreach (object a in attributes)
            {
                if (a.GetType().FullName.Equals("Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute"))
                {
                    ImportedFromDirectory = importDir;
                    return;
                }
            }
        }

        [Browsable(false)]
        public EaTestManager TestManager
        {
            get
            {
                if (this.testManager == null)
                {
                    this.testManager = new EaTestManager(this);
                }
                return this.testManager;
            }
        }

        // TODO
        // [Browsable(false)]
        // public EaControllerStream ControllerStream
        // {
        //     get
        //     {
        //         if (this.controllerStream == null)
        //         {
        //             this.controllerStream = new EaControllerStream(this);
        //         }
        //         return this.controllerStream;
        //     }
        // }

        [Category("Symbol parameters")]
        [Browsable(false)]
        public override ISet<symbol> Symbols
        {
            get
            {
                SortedSet<symbol> result = new SortedSet<symbol>(SymbolContexts.Keys);
                return result;
            }
        }

        public override IServerSymbolContext AddSymbol(params string[] symbolRow)
        {
            symbol symbol = new symbol(symbolRow[0], SymbolContexts.Count);
            ServerSymbolContext symbolContext = new ServerSymbolContext(rmiManager, this, symbol);
            SymbolContexts[symbol] = symbolContext;
            Session.AddSymbol(symbolContext.Runtime.Session);

            symbolContext.Point = Convert.ToDouble(symbolRow[1]);
            symbolContext.Digits = Convert.ToInt32(symbolRow[2]);
            symbolContext.Spread = Convert.ToInt32(symbolRow[3]);
            symbolContext.StopLevel = Convert.ToInt32(symbolRow[4]);
            symbolContext.LotSize = Convert.ToDouble(symbolRow[5]);
            symbolContext.TickValue = Convert.ToDouble(symbolRow[6]);
            symbolContext.TickSize = Convert.ToDouble(symbolRow[7]);
            symbolContext.SwapLong = Convert.ToDouble(symbolRow[8]);
            symbolContext.SwapShort = Convert.ToDouble(symbolRow[9]);
            symbolContext.Starting = Convert.ToInt32(symbolRow[10]);
            symbolContext.Expiration = Convert.ToInt32(symbolRow[11]);
            symbolContext.TradeAllowed = Convert.ToDouble(symbolRow[12]) == 1;
            symbolContext.MinLot = Convert.ToDouble(symbolRow[13]);
            symbolContext.LotStep = Convert.ToDouble(symbolRow[14]);
            symbolContext.MaxLot = Convert.ToDouble(symbolRow[15]);
            symbolContext.SwapType = (SwapCalculationMethod)Convert.ToDouble(symbolRow[16]);
            symbolContext.ProfitCalcMode = (ProfitCalculationMode)Convert.ToDouble(symbolRow[17]);
            symbolContext.MarginCalcMode = (MarginCalculationMode)Convert.ToDouble(symbolRow[18]);
            symbolContext.MarginInit = Convert.ToDouble(symbolRow[19]);
            symbolContext.MarginMaintenance = Convert.ToDouble(symbolRow[10]);
            symbolContext.MarginHedged = Convert.ToDouble(symbolRow[21]);
            symbolContext.MarginRequired = Convert.ToDouble(symbolRow[22]);
            symbolContext.FreezeLevel = Convert.ToDouble(symbolRow[23]);

            return symbolContext;
        }

        public override Mt4ExecutableInfo RegisterScript(ReflType scriptType)
        {
            Mt4ExecutableInfo script = new Mt4ExecutableInfo(rmiManager, Mt4ExecutableType.SCRIPT, scriptType.TypeId, ExecutableLoadType.HARD_CODED);
            Scripts[script.SystemTypeId] = script;
            return script;
        }

        public override Mt4ExecutableInfo RegisterEA(ReflType eaType)
        {
            Mt4ExecutableInfo expert = new Mt4ExecutableInfo(rmiManager, Mt4ExecutableType.EA, eaType.TypeId, ExecutableLoadType.HARD_CODED);
            Experts[expert.SystemTypeId] = expert;
            return expert;
        }

        public override Mt4ExecutableInfo RegisterIndicator(ReflType indicatorType)
        {
            Mt4ExecutableInfo indicator = new Mt4ExecutableInfo(rmiManager, Mt4ExecutableType.INDICATOR, indicatorType.TypeId, ExecutableLoadType.HARD_CODED);
            Indicators[indicator.SystemTypeId] = indicator;
            return indicator;
        }

        public override void AddChart(IServerChartGroupRuntime chart)
        {
            Charts.Add(chart);
            Session.AddChart(chart.Session);
        }

        public override void RemoveChart(IServerChartGroupRuntime chart)
        {
            Charts.Remove(chart);
            Session.RemoveChart(chart.Session);
        }

    }
}
