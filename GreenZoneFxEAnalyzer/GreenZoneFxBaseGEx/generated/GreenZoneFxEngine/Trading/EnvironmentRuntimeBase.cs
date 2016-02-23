using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class EnvironmentRuntimeProps
	{
		public const int PROPERTY_1_PROGRAMOPTIONS_ID = 1;
		public const int PROPERTY_2_SESSION_ID = 2;
		public const int PROPERTY_3_EXPERTS_ID = 3;
		public const int PROPERTY_4_INDICATORS_ID = 4;
		public const int PROPERTY_5_SCRIPTS_ID = 5;
		public const int PROPERTY_6_CHARTS_ID = 6;
		public const int PROPERTY_7_SYMBOLCONTEXTS_ID = 7;
		public const int PROPERTY_8_ORDERS_ID = 8;
		public const int PROPERTY_9_ENVIRONMENTID_ID = 9;
		public const int PROPERTY_10_ENVIRONMENTTYPE_ID = 10;
		public const int PROPERTY_11_ACCOUNTSERVER_ID = 11;
		public const int PROPERTY_12_ACCOUNTCOMPANY_ID = 12;
		public const int PROPERTY_13_ACCOUNTNAME_ID = 13;
		public const int PROPERTY_14_ACCOUNTNUMBER_ID = 14;
		public const int PROPERTY_15_ACCOUNTCURRENCY_ID = 15;
		public const int PROPERTY_16_ACCOUNTLEVERAGE_ID = 16;
		public const int PROPERTY_17_ACCOUNTFREEMARGINMODE_ID = 17;
		public const int PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID = 18;
		public const int PROPERTY_19_ACCOUNTSTOPOUTMODE_ID = 19;
		public const int PROPERTY_20_ISCONNECTED_ID = 20;
		public const int PROPERTY_21_ISSTOPPED_ID = 21;
		public const int PROPERTY_22_LASTERROR_ID = 22;
		public const int PROPERTY_23_SLIPPAGEMODE_ID = 23;
		public const int PROPERTY_24_RANDOMSEED_ID = 24;
		public const int PROPERTY_25_STARTINGBALANCE_ID = 25;
		public const int PROPERTY_26_IMPORTEDFROMDIRECTORY_ID = 26;
		public const int PROPERTY_27_HISTORYDIRECTORY_ID = 27;
		public static bool RmiGetProperty(IEnvironmentRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentRuntimeProps.PROPERTY_1_PROGRAMOPTIONS_ID:
					value = controller.ProgramOptions;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_2_SESSION_ID:
					value = controller.Session;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_3_EXPERTS_ID:
					value = controller.Experts;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_4_INDICATORS_ID:
					value = controller.Indicators;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_5_SCRIPTS_ID:
					value = controller.Scripts;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_6_CHARTS_ID:
					value = controller.Charts;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_7_SYMBOLCONTEXTS_ID:
					value = controller.SymbolContexts;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_8_ORDERS_ID:
					value = controller.Orders;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_9_ENVIRONMENTID_ID:
					value = controller.EnvironmentId;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_10_ENVIRONMENTTYPE_ID:
					value = controller.EnvironmentType;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_11_ACCOUNTSERVER_ID:
					value = controller.AccountServer;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_12_ACCOUNTCOMPANY_ID:
					value = controller.AccountCompany;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_13_ACCOUNTNAME_ID:
					value = controller.AccountName;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_14_ACCOUNTNUMBER_ID:
					value = controller.AccountNumber;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_15_ACCOUNTCURRENCY_ID:
					value = controller.AccountCurrency;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_16_ACCOUNTLEVERAGE_ID:
					value = controller.AccountLeverage;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_17_ACCOUNTFREEMARGINMODE_ID:
					value = controller.AccountFreeMarginMode;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID:
					value = controller.AccountStopoutLevel;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_19_ACCOUNTSTOPOUTMODE_ID:
					value = controller.AccountStopoutMode;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_20_ISCONNECTED_ID:
					value = controller.IsConnected;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_21_ISSTOPPED_ID:
					value = controller.IsStopped;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_22_LASTERROR_ID:
					value = controller.LastError;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_23_SLIPPAGEMODE_ID:
					value = controller.SlippageMode;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_24_RANDOMSEED_ID:
					value = controller.RandomSeed;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_25_STARTINGBALANCE_ID:
					value = controller.StartingBalance;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_26_IMPORTEDFROMDIRECTORY_ID:
					value = controller.ImportedFromDirectory;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_27_HISTORYDIRECTORY_ID:
					value = controller.HistoryDirectory;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentRuntimeProps.PROPERTY_2_SESSION_ID:
					controller.Session = (IEnvironmentSession) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_10_ENVIRONMENTTYPE_ID:
					controller.EnvironmentType = (EnvironmentType) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_11_ACCOUNTSERVER_ID:
					controller.AccountServer = (String) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_12_ACCOUNTCOMPANY_ID:
					controller.AccountCompany = (String) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_13_ACCOUNTNAME_ID:
					controller.AccountName = (String) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_14_ACCOUNTNUMBER_ID:
					controller.AccountNumber = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_15_ACCOUNTCURRENCY_ID:
					controller.AccountCurrency = (String) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_16_ACCOUNTLEVERAGE_ID:
					controller.AccountLeverage = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_17_ACCOUNTFREEMARGINMODE_ID:
					controller.AccountFreeMarginMode = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID:
					controller.AccountStopoutLevel = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_19_ACCOUNTSTOPOUTMODE_ID:
					controller.AccountStopoutMode = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_20_ISCONNECTED_ID:
					controller.IsConnected = (Boolean) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_21_ISSTOPPED_ID:
					controller.IsStopped = (Boolean) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_22_LASTERROR_ID:
					controller.LastError = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_23_SLIPPAGEMODE_ID:
					controller.SlippageMode = (SlippageMode) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_24_RANDOMSEED_ID:
					controller.RandomSeed = (Int32) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_25_STARTINGBALANCE_ID:
					controller.StartingBalance = (Double) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_26_IMPORTEDFROMDIRECTORY_ID:
					controller.ImportedFromDirectory = (String) value;
					return true;
				case EnvironmentRuntimeProps.PROPERTY_27_HISTORYDIRECTORY_ID:
					controller.HistoryDirectory = (String) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.ProgramOptions = (IEAnalyzerOptions) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_1_PROGRAMOPTIONS_ID];
			controller.Experts = (Dictionary<String,Mt4ExecutableInfo>) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_3_EXPERTS_ID];
			controller.Indicators = (Dictionary<String,Mt4ExecutableInfo>) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_4_INDICATORS_ID];
			controller.Scripts = (Dictionary<String,Mt4ExecutableInfo>) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_5_SCRIPTS_ID];
			controller.Charts = (IList<IChartGroupRuntime>) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_6_CHARTS_ID];
			controller.SymbolContexts = (Dictionary<symbol,ISymbolContext>) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_7_SYMBOLCONTEXTS_ID];
			controller.Orders = (IOrdersTable) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_8_ORDERS_ID];
			controller.EnvironmentId = (String) buffer.ChangedProps[EnvironmentRuntimeProps.PROPERTY_9_ENVIRONMENTID_ID];
		}

		public static void AddDependencies(IEnvironmentRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.ProgramOptions);
			controller.Dependencies.AddRange(controller.Experts.Values);
			controller.Dependencies.AddRange(controller.Indicators.Values);
			controller.Dependencies.AddRange(controller.Scripts.Values);
			controller.Dependencies.AddRange(controller.Charts);
			controller.Dependencies.AddRange(controller.SymbolContexts.Values);
			controller.Dependencies.Add(controller.Orders);
		}

		public static void SerializationRead(IEnvironmentRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.ProgramOptions = (IEAnalyzerOptions) info.GetValue("ProgramOptions", typeof(IEAnalyzerOptions));
			controller.SymbolContexts = (Dictionary<symbol,ISymbolContext>) info.GetValue("SymbolContexts", typeof(Dictionary<symbol,ISymbolContext>));
			controller.EnvironmentId = (String) info.GetValue("EnvironmentId", typeof(String));
			controller.EnvironmentType = (EnvironmentType) info.GetValue("EnvironmentType", typeof(EnvironmentType));
			controller.AccountServer = (String) info.GetValue("AccountServer", typeof(String));
			controller.AccountCompany = (String) info.GetValue("AccountCompany", typeof(String));
			controller.AccountName = (String) info.GetValue("AccountName", typeof(String));
			controller.AccountNumber = (Int32) info.GetValue("AccountNumber", typeof(Int32));
			controller.AccountCurrency = (String) info.GetValue("AccountCurrency", typeof(String));
			controller.AccountLeverage = (Int32) info.GetValue("AccountLeverage", typeof(Int32));
			controller.AccountFreeMarginMode = (Int32) info.GetValue("AccountFreeMarginMode", typeof(Int32));
			controller.AccountStopoutLevel = (Int32) info.GetValue("AccountStopoutLevel", typeof(Int32));
			controller.AccountStopoutMode = (Int32) info.GetValue("AccountStopoutMode", typeof(Int32));
			controller.IsConnected = (Boolean) info.GetValue("IsConnected", typeof(Boolean));
			controller.IsStopped = (Boolean) info.GetValue("IsStopped", typeof(Boolean));
			controller.LastError = (Int32) info.GetValue("LastError", typeof(Int32));
			controller.SlippageMode = (SlippageMode) info.GetValue("SlippageMode", typeof(SlippageMode));
			controller.RandomSeed = (Int32) info.GetValue("RandomSeed", typeof(Int32));
			controller.StartingBalance = (Double) info.GetValue("StartingBalance", typeof(Double));
			controller.ImportedFromDirectory = (String) info.GetValue("ImportedFromDirectory", typeof(String));
			controller.HistoryDirectory = (String) info.GetValue("HistoryDirectory", typeof(String));
		}

		public static void SerializationWrite(IEnvironmentRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("ProgramOptions", controller.ProgramOptions);
			info.AddValue("SymbolContexts", controller.SymbolContexts);
			info.AddValue("EnvironmentId", controller.EnvironmentId);
			info.AddValue("EnvironmentType", controller.EnvironmentType);
			info.AddValue("AccountServer", controller.AccountServer);
			info.AddValue("AccountCompany", controller.AccountCompany);
			info.AddValue("AccountName", controller.AccountName);
			info.AddValue("AccountNumber", controller.AccountNumber);
			info.AddValue("AccountCurrency", controller.AccountCurrency);
			info.AddValue("AccountLeverage", controller.AccountLeverage);
			info.AddValue("AccountFreeMarginMode", controller.AccountFreeMarginMode);
			info.AddValue("AccountStopoutLevel", controller.AccountStopoutLevel);
			info.AddValue("AccountStopoutMode", controller.AccountStopoutMode);
			info.AddValue("IsConnected", controller.IsConnected);
			info.AddValue("IsStopped", controller.IsStopped);
			info.AddValue("LastError", controller.LastError);
			info.AddValue("SlippageMode", controller.SlippageMode);
			info.AddValue("RandomSeed", controller.RandomSeed);
			info.AddValue("StartingBalance", controller.StartingBalance);
			info.AddValue("ImportedFromDirectory", controller.ImportedFromDirectory);
			info.AddValue("HistoryDirectory", controller.HistoryDirectory);
		}

	}
	public abstract class EnvironmentRuntimeBase : RmiBase, IEnvironmentRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IEnvironmentRuntime_Session_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_EnvironmentType_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountServer_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountCompany_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountName_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountNumber_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountCurrency_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountLeverage_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountFreeMarginMode_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountStopoutLevel_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_AccountStopoutMode_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_IsConnected_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_IsStopped_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_LastError_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_SlippageMode_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_RandomSeed_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_StartingBalance_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_ImportedFromDirectory_Changed;
		public event PropertyChangedEventHandler IEnvironmentRuntime_HistoryDirectory_Changed;

		public EnvironmentRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EnvironmentRuntimeProps.AddDependencies(this, false);
		}

		public EnvironmentRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			EnvironmentRuntimeProps.AddDependencies(this, false);
		}

		protected EnvironmentRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EnvironmentRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract symbol GetSymbol(String _symbol);

		public abstract Double MarketInfo(symbol symbol, MarketInfoConst type);

		public abstract ISymbolContext GetSymbolContext(symbol symbol);

		public abstract ISymbolContext GetSymbolContext(String symbol);

		public abstract Mt4ExecutableInfo GetScriptInfo(String systemTypeId);

		public abstract Mt4ExecutableInfo GetExpertInfo(String systemTypeId);

		public abstract Mt4ExecutableInfo GetIndicatorInfo(String systemTypeId);

		public abstract Mt4ExecutableInfo GetExecutableInfo(String systemTypeId);

		public abstract Int32 GenerateTicket();


		IEAnalyzerOptions _IEnvironmentRuntime_ProgramOptions;
		public IEAnalyzerOptions ProgramOptions
		{
			get {
				return _IEnvironmentRuntime_ProgramOptions;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_ProgramOptions= value;
					changed[EnvironmentRuntimeProps.PROPERTY_1_PROGRAMOPTIONS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IEnvironmentSession _IEnvironmentRuntime_Session;
		public virtual IEnvironmentSession Session
		{
			get {
				return _IEnvironmentRuntime_Session;
			}
			set {
				if (_IEnvironmentRuntime_Session != value) {
					_IEnvironmentRuntime_Session= value;
					changed[EnvironmentRuntimeProps.PROPERTY_2_SESSION_ID] = true;
					if (IEnvironmentRuntime_Session_Changed != null)
						IEnvironmentRuntime_Session_Changed(this, new PropertyChangedEventArgs("Session", value));
				}
			}
		}

		Dictionary<String,Mt4ExecutableInfo> _IEnvironmentRuntime_Experts;
		public Dictionary<String,Mt4ExecutableInfo> Experts
		{
			get {
				return _IEnvironmentRuntime_Experts;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_Experts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_3_EXPERTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<String,Mt4ExecutableInfo> _IEnvironmentRuntime_Indicators;
		public Dictionary<String,Mt4ExecutableInfo> Indicators
		{
			get {
				return _IEnvironmentRuntime_Indicators;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_Indicators= value;
					changed[EnvironmentRuntimeProps.PROPERTY_4_INDICATORS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<String,Mt4ExecutableInfo> _IEnvironmentRuntime_Scripts;
		public Dictionary<String,Mt4ExecutableInfo> Scripts
		{
			get {
				return _IEnvironmentRuntime_Scripts;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_Scripts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_5_SCRIPTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IList<IChartGroupRuntime> _IEnvironmentRuntime_Charts;
		public IList<IChartGroupRuntime> Charts
		{
			get {
				return _IEnvironmentRuntime_Charts;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_Charts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_6_CHARTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<symbol,ISymbolContext> _IEnvironmentRuntime_SymbolContexts;
		public Dictionary<symbol,ISymbolContext> SymbolContexts
		{
			get {
				return _IEnvironmentRuntime_SymbolContexts;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_SymbolContexts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_7_SYMBOLCONTEXTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTable _IEnvironmentRuntime_Orders;
		public IOrdersTable Orders
		{
			get {
				return _IEnvironmentRuntime_Orders;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_Orders= value;
					changed[EnvironmentRuntimeProps.PROPERTY_8_ORDERS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IList<IHistoryOrderEtc> HistoryOrders
		{
			get {
				return Orders.OrdersHistoryEtc;
			}
		}

		String _IEnvironmentRuntime_EnvironmentId;
		public String EnvironmentId
		{
			get {
				return _IEnvironmentRuntime_EnvironmentId;
			}
			set {
				if (!___initialized) {
					_IEnvironmentRuntime_EnvironmentId= value;
					changed[EnvironmentRuntimeProps.PROPERTY_9_ENVIRONMENTID_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		EnvironmentType _IEnvironmentRuntime_EnvironmentType;
		public EnvironmentType EnvironmentType
		{
			get {
				return _IEnvironmentRuntime_EnvironmentType;
			}
			set {
				if (_IEnvironmentRuntime_EnvironmentType != value) {
					_IEnvironmentRuntime_EnvironmentType= value;
					changed[EnvironmentRuntimeProps.PROPERTY_10_ENVIRONMENTTYPE_ID] = true;
					if (IEnvironmentRuntime_EnvironmentType_Changed != null)
						IEnvironmentRuntime_EnvironmentType_Changed(this, new PropertyChangedEventArgs("EnvironmentType", value));
				}
			}
		}

		String _IEnvironmentRuntime_AccountServer;
		public String AccountServer
		{
			get {
				return _IEnvironmentRuntime_AccountServer;
			}
			set {
				if (_IEnvironmentRuntime_AccountServer != value) {
					_IEnvironmentRuntime_AccountServer= value;
					changed[EnvironmentRuntimeProps.PROPERTY_11_ACCOUNTSERVER_ID] = true;
					if (IEnvironmentRuntime_AccountServer_Changed != null)
						IEnvironmentRuntime_AccountServer_Changed(this, new PropertyChangedEventArgs("AccountServer", value));
				}
			}
		}

		String _IEnvironmentRuntime_AccountCompany;
		public String AccountCompany
		{
			get {
				return _IEnvironmentRuntime_AccountCompany;
			}
			set {
				if (_IEnvironmentRuntime_AccountCompany != value) {
					_IEnvironmentRuntime_AccountCompany= value;
					changed[EnvironmentRuntimeProps.PROPERTY_12_ACCOUNTCOMPANY_ID] = true;
					if (IEnvironmentRuntime_AccountCompany_Changed != null)
						IEnvironmentRuntime_AccountCompany_Changed(this, new PropertyChangedEventArgs("AccountCompany", value));
				}
			}
		}

		String _IEnvironmentRuntime_AccountName;
		public String AccountName
		{
			get {
				return _IEnvironmentRuntime_AccountName;
			}
			set {
				if (_IEnvironmentRuntime_AccountName != value) {
					_IEnvironmentRuntime_AccountName= value;
					changed[EnvironmentRuntimeProps.PROPERTY_13_ACCOUNTNAME_ID] = true;
					if (IEnvironmentRuntime_AccountName_Changed != null)
						IEnvironmentRuntime_AccountName_Changed(this, new PropertyChangedEventArgs("AccountName", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_AccountNumber;
		public Int32 AccountNumber
		{
			get {
				return _IEnvironmentRuntime_AccountNumber;
			}
			set {
				if (_IEnvironmentRuntime_AccountNumber != value) {
					_IEnvironmentRuntime_AccountNumber= value;
					changed[EnvironmentRuntimeProps.PROPERTY_14_ACCOUNTNUMBER_ID] = true;
					if (IEnvironmentRuntime_AccountNumber_Changed != null)
						IEnvironmentRuntime_AccountNumber_Changed(this, new PropertyChangedEventArgs("AccountNumber", value));
				}
			}
		}

		String _IEnvironmentRuntime_AccountCurrency;
		public String AccountCurrency
		{
			get {
				return _IEnvironmentRuntime_AccountCurrency;
			}
			set {
				if (_IEnvironmentRuntime_AccountCurrency != value) {
					_IEnvironmentRuntime_AccountCurrency= value;
					changed[EnvironmentRuntimeProps.PROPERTY_15_ACCOUNTCURRENCY_ID] = true;
					if (IEnvironmentRuntime_AccountCurrency_Changed != null)
						IEnvironmentRuntime_AccountCurrency_Changed(this, new PropertyChangedEventArgs("AccountCurrency", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_AccountLeverage;
		public Int32 AccountLeverage
		{
			get {
				return _IEnvironmentRuntime_AccountLeverage;
			}
			set {
				if (_IEnvironmentRuntime_AccountLeverage != value) {
					_IEnvironmentRuntime_AccountLeverage= value;
					changed[EnvironmentRuntimeProps.PROPERTY_16_ACCOUNTLEVERAGE_ID] = true;
					if (IEnvironmentRuntime_AccountLeverage_Changed != null)
						IEnvironmentRuntime_AccountLeverage_Changed(this, new PropertyChangedEventArgs("AccountLeverage", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_AccountFreeMarginMode;
		public Int32 AccountFreeMarginMode
		{
			get {
				return _IEnvironmentRuntime_AccountFreeMarginMode;
			}
			set {
				if (_IEnvironmentRuntime_AccountFreeMarginMode != value) {
					_IEnvironmentRuntime_AccountFreeMarginMode= value;
					changed[EnvironmentRuntimeProps.PROPERTY_17_ACCOUNTFREEMARGINMODE_ID] = true;
					if (IEnvironmentRuntime_AccountFreeMarginMode_Changed != null)
						IEnvironmentRuntime_AccountFreeMarginMode_Changed(this, new PropertyChangedEventArgs("AccountFreeMarginMode", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_AccountStopoutLevel;
		public Int32 AccountStopoutLevel
		{
			get {
				return _IEnvironmentRuntime_AccountStopoutLevel;
			}
			set {
				if (_IEnvironmentRuntime_AccountStopoutLevel != value) {
					_IEnvironmentRuntime_AccountStopoutLevel= value;
					changed[EnvironmentRuntimeProps.PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID] = true;
					if (IEnvironmentRuntime_AccountStopoutLevel_Changed != null)
						IEnvironmentRuntime_AccountStopoutLevel_Changed(this, new PropertyChangedEventArgs("AccountStopoutLevel", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_AccountStopoutMode;
		public Int32 AccountStopoutMode
		{
			get {
				return _IEnvironmentRuntime_AccountStopoutMode;
			}
			set {
				if (_IEnvironmentRuntime_AccountStopoutMode != value) {
					_IEnvironmentRuntime_AccountStopoutMode= value;
					changed[EnvironmentRuntimeProps.PROPERTY_19_ACCOUNTSTOPOUTMODE_ID] = true;
					if (IEnvironmentRuntime_AccountStopoutMode_Changed != null)
						IEnvironmentRuntime_AccountStopoutMode_Changed(this, new PropertyChangedEventArgs("AccountStopoutMode", value));
				}
			}
		}

		Boolean _IEnvironmentRuntime_IsConnected;
		public Boolean IsConnected
		{
			get {
				return _IEnvironmentRuntime_IsConnected;
			}
			set {
				if (_IEnvironmentRuntime_IsConnected != value) {
					_IEnvironmentRuntime_IsConnected= value;
					changed[EnvironmentRuntimeProps.PROPERTY_20_ISCONNECTED_ID] = true;
					if (IEnvironmentRuntime_IsConnected_Changed != null)
						IEnvironmentRuntime_IsConnected_Changed(this, new PropertyChangedEventArgs("IsConnected", value));
				}
			}
		}

		Boolean _IEnvironmentRuntime_IsStopped;
		public Boolean IsStopped
		{
			get {
				return _IEnvironmentRuntime_IsStopped;
			}
			set {
				if (_IEnvironmentRuntime_IsStopped != value) {
					_IEnvironmentRuntime_IsStopped= value;
					changed[EnvironmentRuntimeProps.PROPERTY_21_ISSTOPPED_ID] = true;
					if (IEnvironmentRuntime_IsStopped_Changed != null)
						IEnvironmentRuntime_IsStopped_Changed(this, new PropertyChangedEventArgs("IsStopped", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_LastError;
		public Int32 LastError
		{
			get {
				return _IEnvironmentRuntime_LastError;
			}
			set {
				if (_IEnvironmentRuntime_LastError != value) {
					_IEnvironmentRuntime_LastError= value;
					changed[EnvironmentRuntimeProps.PROPERTY_22_LASTERROR_ID] = true;
					if (IEnvironmentRuntime_LastError_Changed != null)
						IEnvironmentRuntime_LastError_Changed(this, new PropertyChangedEventArgs("LastError", value));
				}
			}
		}

		SlippageMode _IEnvironmentRuntime_SlippageMode;
		public SlippageMode SlippageMode
		{
			get {
				return _IEnvironmentRuntime_SlippageMode;
			}
			set {
				if (_IEnvironmentRuntime_SlippageMode != value) {
					_IEnvironmentRuntime_SlippageMode= value;
					changed[EnvironmentRuntimeProps.PROPERTY_23_SLIPPAGEMODE_ID] = true;
					if (IEnvironmentRuntime_SlippageMode_Changed != null)
						IEnvironmentRuntime_SlippageMode_Changed(this, new PropertyChangedEventArgs("SlippageMode", value));
				}
			}
		}

		Int32 _IEnvironmentRuntime_RandomSeed;
		public Int32 RandomSeed
		{
			get {
				return _IEnvironmentRuntime_RandomSeed;
			}
			set {
				if (_IEnvironmentRuntime_RandomSeed != value) {
					_IEnvironmentRuntime_RandomSeed= value;
					changed[EnvironmentRuntimeProps.PROPERTY_24_RANDOMSEED_ID] = true;
					if (IEnvironmentRuntime_RandomSeed_Changed != null)
						IEnvironmentRuntime_RandomSeed_Changed(this, new PropertyChangedEventArgs("RandomSeed", value));
				}
			}
		}

		Double _IEnvironmentRuntime_StartingBalance;
		public Double StartingBalance
		{
			get {
				return _IEnvironmentRuntime_StartingBalance;
			}
			set {
				if (_IEnvironmentRuntime_StartingBalance != value) {
					_IEnvironmentRuntime_StartingBalance= value;
					changed[EnvironmentRuntimeProps.PROPERTY_25_STARTINGBALANCE_ID] = true;
					if (IEnvironmentRuntime_StartingBalance_Changed != null)
						IEnvironmentRuntime_StartingBalance_Changed(this, new PropertyChangedEventArgs("StartingBalance", value));
				}
			}
		}

		String _IEnvironmentRuntime_ImportedFromDirectory;
		public String ImportedFromDirectory
		{
			get {
				return _IEnvironmentRuntime_ImportedFromDirectory;
			}
			set {
				if (_IEnvironmentRuntime_ImportedFromDirectory != value) {
					_IEnvironmentRuntime_ImportedFromDirectory= value;
					changed[EnvironmentRuntimeProps.PROPERTY_26_IMPORTEDFROMDIRECTORY_ID] = true;
					if (IEnvironmentRuntime_ImportedFromDirectory_Changed != null)
						IEnvironmentRuntime_ImportedFromDirectory_Changed(this, new PropertyChangedEventArgs("ImportedFromDirectory", value));
				}
			}
		}

		String _IEnvironmentRuntime_HistoryDirectory;
		public String HistoryDirectory
		{
			get {
				return _IEnvironmentRuntime_HistoryDirectory;
			}
			set {
				if (_IEnvironmentRuntime_HistoryDirectory != value) {
					_IEnvironmentRuntime_HistoryDirectory= value;
					changed[EnvironmentRuntimeProps.PROPERTY_27_HISTORYDIRECTORY_ID] = true;
					if (IEnvironmentRuntime_HistoryDirectory_Changed != null)
						IEnvironmentRuntime_HistoryDirectory_Changed(this, new PropertyChangedEventArgs("HistoryDirectory", value));
				}
			}
		}

		public abstract Object Locker
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EnvironmentRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EnvironmentRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
