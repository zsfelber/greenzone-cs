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
using System.ComponentModel;
using GreenZoneUtil.Util;
using System.Drawing;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{
    
    [GreenRmi]
    public interface IEnvironmentRuntime : IRmiBase
    {
        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IEAnalyzerOptions ProgramOptions
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        [GreenRmiNonSerial]
        IEnvironmentSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        [GreenRmiNonSerial]
        Dictionary<string, Mt4ExecutableInfo> Experts
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        [GreenRmiNonSerial]
        Dictionary<string, Mt4ExecutableInfo> Indicators
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        [GreenRmiNonSerial]
        Dictionary<string, Mt4ExecutableInfo> Scripts
        {
            get;
            set;
        }


        [GreenRmiField(GreenRmiFieldType.Readonly)]
        [GreenRmiNonSerial]
        IList<IChartGroupRuntime> Charts
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        Dictionary<symbol, ISymbolContext> SymbolContexts
        {
            get;
            set;
        }



        [GreenRmiField(GreenRmiFieldType.Readonly)]
        [GreenRmiNonSerial]
        IOrdersTable Orders
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Simple, "return Orders.OrdersHistoryEtc;")]
        IList<IHistoryOrderEtc> HistoryOrders
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        string EnvironmentId
        {
            get;
            set;
        }

        [Category("File information")]
        EnvironmentType EnvironmentType
        {
            get;
            set;
        }

        [Category("Account information")]
        string AccountServer
        {
            get;
            set;
        }

        [Category("Account information")]
        string AccountCompany
        {
            get;
            set;
        }

        [Category("Account information")]
        string AccountName
        {
            get;
            set;
        }

        [Category("Account information")]
        int AccountNumber
        {
            get;
            set;
        }

        [Category("Account information")]
        string AccountCurrency
        {
            get;
            set;
        }

        [Category("Account information")]
        int AccountLeverage
        {
            get;
            set;
        }

        [Category("Account information")]
        int AccountFreeMarginMode
        {
            get;
            set;
        }

        [Category("Account information")]
        int AccountStopoutLevel
        {
            get;
            set;
        }

        [Category("Account information")]
        int AccountStopoutMode
        {
            get;
            set;
        }

        [Category("Runtime information")]
        bool IsConnected
        {
            get;
            set;
        }

        [Category("Runtime information")]
        bool IsStopped
        {
            get;
            set;
        }

        [Category("Runtime information")]
        int LastError
        {
            get;
            set;
        }

        [Category("Testing behavior")]
        SlippageMode SlippageMode
        {
            get;
            set;
        }

        [Category("Testing behavior")]
        [Description("Constant used to alter random seed (affects slippage emulation and tick data generation).")]
        int RandomSeed
        {
            get;
            set;
        }

        [Category("Testing behavior")]
        [Description("Starting balance in base currency.")]
        double StartingBalance
        {
            get;
            set;
        }

        [Category("File information")]
        string ImportedFromDirectory
        {
            get;
            set;
        }

        [Category("File information")]
        string HistoryDirectory
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        object Locker
        {
            get;
        }

        symbol GetSymbol(string _symbol);

        double MarketInfo(symbol symbol, MarketInfoConst type);

        ISymbolContext GetSymbolContext(symbol symbol);

        ISymbolContext GetSymbolContext(string symbol);

        Mt4ExecutableInfo GetScriptInfo(string systemTypeId);

        Mt4ExecutableInfo GetExpertInfo(string systemTypeId);

        Mt4ExecutableInfo GetIndicatorInfo(string systemTypeId);

        Mt4ExecutableInfo GetExecutableInfo(string systemTypeId);

        int GenerateTicket();
    }

    [GreenRmi(BaseClass = "EnvironmentRuntimeEx")]
    public interface IServerEnvironmentRuntime : IEnvironmentRuntime
    {
        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerEnvironmentSession Session
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.New)]
        new IServerOrdersTable Orders
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        ISet<symbol> Symbols
        {
            get;
        }

        void Save();
        void LoadOrders();
        void SaveOrders();
        void SetImportDir(string importDir, object tester);
        IServerSymbolContext AddSymbol(params string[] symbolRow);
        Mt4ExecutableInfo RegisterScript(ReflType scriptType);
        Mt4ExecutableInfo RegisterEA(ReflType eaType);
        Mt4ExecutableInfo RegisterIndicator(ReflType indicatorType);
        void AddChart(IServerChartGroupRuntime chart);
        void RemoveChart(IServerChartGroupRuntime chart);
    }

    [GreenRmi]
    public interface IEAnalyzerOptions : IRmiBase
    {
        SelectableDir EAnalyzerDirectory
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        string DefaultEnvironment
        {
            get;
            set;
        }

        int BufferSize
        {
            get;
            set;
        }
    }

    [GreenRmi]
    public interface IEnvironmentSession : IRmiBase
    {
        Dictionary<symbol, ISymbolSession> SymbolSessions
        {
            get;
            set;
        }

        List<IChartGroupSession> ChartSessions
        {
            get;
            set;
        }


        string EnvironmentId
        {
            get;
            set;
        }

        [Category("EA testing")]
        bool ScrollAcrossCharts
        {
            get;
            set;
        }

        [Category("EA testing")]
        bool SkipEmptyPeriods
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldModifiers = "volatile")]
        [Category("EA testing")]
        int EATestingSpeed
        {
            get;
            set;
        }

        [Category("EA testing")]
        StartStatus EAStartStatus
        {
            get;
            set;
        }

        [Category("EA testing")]
        datetime EATestingGlobalFrom
        {
            get;
            set;
        }

        [Category("EA testing")]
        datetime EATestingGlobalTo
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Abstract)]
        [Category("EA testing")]
        int EATestingProgress
        {
            get;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldModifiers = "volatile")]
        [Category("EA testing")]
        int EATestingTrackBarTick
        {
            get;
            set;
        }

        [Category("EA testing")]
        PorgressSnapMode EATestingSnapMode
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Virtual)]
        [Category("EA testing")]
        datetime Time
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Normal, FieldModifiers = "volatile")]
        bool UpdateSpreadTick
        {
            get;
            set;
        }

        Point WindowSplitPoint
        {
            get;
            set;
        }

        bool IsNavigatorVisible
        {
            get;
            set;
        }

        bool IsOrdersTableVisible
        {
            get;
            set;
        }

        bool IsOrdersOverviewVisible
        {
            get;
            set;
        }

        [GreenRmiField(GreenRmiFieldType.Readonly)]
        IOrderFilter OrderFilter
        {
            get;
            set;
        }
    }

    [GreenRmi]
    public interface IServerEnvironmentSession : IEnvironmentSession
    {
        bool Save();

        void AddSymbol(ISymbolSession symbolSession);
        
        void AddChart(IChartGroupSession chartSession);
        
        void RemoveChart(IChartGroupSession chart);

    }
}
