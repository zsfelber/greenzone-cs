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
		public static bool RmiGetProperty(IEnvironmentRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_PROGRAMOPTIONS_ID:
					value = controller.ProgramOptions;
					return true;
				case PROPERTY_2_SESSION_ID:
					value = controller.Session;
					return true;
				case PROPERTY_3_EXPERTS_ID:
					value = controller.Experts;
					return true;
				case PROPERTY_4_INDICATORS_ID:
					value = controller.Indicators;
					return true;
				case PROPERTY_5_SCRIPTS_ID:
					value = controller.Scripts;
					return true;
				case PROPERTY_6_CHARTS_ID:
					value = controller.Charts;
					return true;
				case PROPERTY_7_SYMBOLCONTEXTS_ID:
					value = controller.SymbolContexts;
					return true;
				case PROPERTY_8_ORDERS_ID:
					value = controller.Orders;
					return true;
				case PROPERTY_9_ENVIRONMENTID_ID:
					value = controller.EnvironmentId;
					return true;
				case PROPERTY_10_ENVIRONMENTTYPE_ID:
					value = controller.EnvironmentType;
					return true;
				case PROPERTY_11_ACCOUNTSERVER_ID:
					value = controller.AccountServer;
					return true;
				case PROPERTY_12_ACCOUNTCOMPANY_ID:
					value = controller.AccountCompany;
					return true;
				case PROPERTY_13_ACCOUNTNAME_ID:
					value = controller.AccountName;
					return true;
				case PROPERTY_14_ACCOUNTNUMBER_ID:
					value = controller.AccountNumber;
					return true;
				case PROPERTY_15_ACCOUNTCURRENCY_ID:
					value = controller.AccountCurrency;
					return true;
				case PROPERTY_16_ACCOUNTLEVERAGE_ID:
					value = controller.AccountLeverage;
					return true;
				case PROPERTY_17_ACCOUNTFREEMARGINMODE_ID:
					value = controller.AccountFreeMarginMode;
					return true;
				case PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID:
					value = controller.AccountStopoutLevel;
					return true;
				case PROPERTY_19_ACCOUNTSTOPOUTMODE_ID:
					value = controller.AccountStopoutMode;
					return true;
				case PROPERTY_20_ISCONNECTED_ID:
					value = controller.IsConnected;
					return true;
				case PROPERTY_21_ISSTOPPED_ID:
					value = controller.IsStopped;
					return true;
				case PROPERTY_22_LASTERROR_ID:
					value = controller.LastError;
					return true;
				case PROPERTY_23_SLIPPAGEMODE_ID:
					value = controller.SlippageMode;
					return true;
				case PROPERTY_24_RANDOMSEED_ID:
					value = controller.RandomSeed;
					return true;
				case PROPERTY_25_STARTINGBALANCE_ID:
					value = controller.StartingBalance;
					return true;
				case PROPERTY_26_IMPORTEDFROMDIRECTORY_ID:
					value = controller.ImportedFromDirectory;
					return true;
				case PROPERTY_27_HISTORYDIRECTORY_ID:
					value = controller.HistoryDirectory;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_2_SESSION_ID:
					controller.Session = (IEnvironmentSession) value;
					return true;
				case PROPERTY_10_ENVIRONMENTTYPE_ID:
					controller.EnvironmentType = (EnvironmentType) value;
					return true;
				case PROPERTY_11_ACCOUNTSERVER_ID:
					controller.AccountServer = (String) value;
					return true;
				case PROPERTY_12_ACCOUNTCOMPANY_ID:
					controller.AccountCompany = (String) value;
					return true;
				case PROPERTY_13_ACCOUNTNAME_ID:
					controller.AccountName = (String) value;
					return true;
				case PROPERTY_14_ACCOUNTNUMBER_ID:
					controller.AccountNumber = (Int32) value;
					return true;
				case PROPERTY_15_ACCOUNTCURRENCY_ID:
					controller.AccountCurrency = (String) value;
					return true;
				case PROPERTY_16_ACCOUNTLEVERAGE_ID:
					controller.AccountLeverage = (Int32) value;
					return true;
				case PROPERTY_17_ACCOUNTFREEMARGINMODE_ID:
					controller.AccountFreeMarginMode = (Int32) value;
					return true;
				case PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID:
					controller.AccountStopoutLevel = (Int32) value;
					return true;
				case PROPERTY_19_ACCOUNTSTOPOUTMODE_ID:
					controller.AccountStopoutMode = (Int32) value;
					return true;
				case PROPERTY_20_ISCONNECTED_ID:
					controller.IsConnected = (Boolean) value;
					return true;
				case PROPERTY_21_ISSTOPPED_ID:
					controller.IsStopped = (Boolean) value;
					return true;
				case PROPERTY_22_LASTERROR_ID:
					controller.LastError = (Int32) value;
					return true;
				case PROPERTY_23_SLIPPAGEMODE_ID:
					controller.SlippageMode = (SlippageMode) value;
					return true;
				case PROPERTY_24_RANDOMSEED_ID:
					controller.RandomSeed = (Int32) value;
					return true;
				case PROPERTY_25_STARTINGBALANCE_ID:
					controller.StartingBalance = (Double) value;
					return true;
				case PROPERTY_26_IMPORTEDFROMDIRECTORY_ID:
					controller.ImportedFromDirectory = (String) value;
					return true;
				case PROPERTY_27_HISTORYDIRECTORY_ID:
					controller.HistoryDirectory = (String) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentRuntime controller, GreenRmiObjectBuffer buffer)
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

		public static void AddDependencies(IEnvironmentRuntime controller)
		{
			controller.Dependencies.Add(controller.ProgramOptions);
			controller.Dependencies.AddRange(controller.Experts.Values);
			controller.Dependencies.AddRange(controller.Indicators.Values);
			controller.Dependencies.AddRange(controller.Scripts.Values);
			controller.Dependencies.AddRange(controller.Charts);
			controller.Dependencies.AddRange(controller.SymbolContexts.Values);
			controller.Dependencies.Add(controller.Orders);
		}

		public static void SerializationRead(IEnvironmentRuntime controller, SerializationInfo info, StreamingContext context)
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

		public static void SerializationWrite(IEnvironmentRuntime controller, SerializationInfo info, StreamingContext context)
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

		public event PropertyChangedEventHandler SessionChanged;
		public event PropertyChangedEventHandler EnvironmentTypeChanged;
		public event PropertyChangedEventHandler AccountServerChanged;
		public event PropertyChangedEventHandler AccountCompanyChanged;
		public event PropertyChangedEventHandler AccountNameChanged;
		public event PropertyChangedEventHandler AccountNumberChanged;
		public event PropertyChangedEventHandler AccountCurrencyChanged;
		public event PropertyChangedEventHandler AccountLeverageChanged;
		public event PropertyChangedEventHandler AccountFreeMarginModeChanged;
		public event PropertyChangedEventHandler AccountStopoutLevelChanged;
		public event PropertyChangedEventHandler AccountStopoutModeChanged;
		public event PropertyChangedEventHandler IsConnectedChanged;
		public event PropertyChangedEventHandler IsStoppedChanged;
		public event PropertyChangedEventHandler LastErrorChanged;
		public event PropertyChangedEventHandler SlippageModeChanged;
		public event PropertyChangedEventHandler RandomSeedChanged;
		public event PropertyChangedEventHandler StartingBalanceChanged;
		public event PropertyChangedEventHandler ImportedFromDirectoryChanged;
		public event PropertyChangedEventHandler HistoryDirectoryChanged;

		public EnvironmentRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EnvironmentRuntimeProps.AddDependencies(this);
		}

		public EnvironmentRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			EnvironmentRuntimeProps.AddDependencies(this);
		}

		protected EnvironmentRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			EnvironmentRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentRuntimeProps.SerializationWrite(this, info, context);
		}

		public abstract Mt4ExecutableInfo GetScriptInfo0(String systemTypeId);

		public abstract Mt4ExecutableInfo GetExpertInfo0(String systemTypeId);

		public abstract Mt4ExecutableInfo GetIndicatorInfo0(String systemTypeId);

		IEAnalyzerOptions programOptions;
		public IEAnalyzerOptions ProgramOptions
		{
			get {
				return programOptions;
			}
			set {
				if (!___initialized) {
					programOptions= value;
					changed[EnvironmentRuntimeProps.PROPERTY_1_PROGRAMOPTIONS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IEnvironmentSession session;
		public virtual IEnvironmentSession Session
		{
			get {
				return session;
			}
			set {
				if (session != value) {
					session= value;
					changed[EnvironmentRuntimeProps.PROPERTY_2_SESSION_ID] = true;
					if (SessionChanged != null)
						SessionChanged(this, new PropertyChangedEventArgs("Session", value));
				}
			}
		}

		Dictionary<String,Mt4ExecutableInfo> experts;
		public Dictionary<String,Mt4ExecutableInfo> Experts
		{
			get {
				return experts;
			}
			set {
				if (!___initialized) {
					experts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_3_EXPERTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<String,Mt4ExecutableInfo> indicators;
		public Dictionary<String,Mt4ExecutableInfo> Indicators
		{
			get {
				return indicators;
			}
			set {
				if (!___initialized) {
					indicators= value;
					changed[EnvironmentRuntimeProps.PROPERTY_4_INDICATORS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<String,Mt4ExecutableInfo> scripts;
		public Dictionary<String,Mt4ExecutableInfo> Scripts
		{
			get {
				return scripts;
			}
			set {
				if (!___initialized) {
					scripts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_5_SCRIPTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IList<IChartGroupRuntime> charts;
		public IList<IChartGroupRuntime> Charts
		{
			get {
				return charts;
			}
			set {
				if (!___initialized) {
					charts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_6_CHARTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<symbol,ISymbolContext> symbolContexts;
		public Dictionary<symbol,ISymbolContext> SymbolContexts
		{
			get {
				return symbolContexts;
			}
			set {
				if (!___initialized) {
					symbolContexts= value;
					changed[EnvironmentRuntimeProps.PROPERTY_7_SYMBOLCONTEXTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTable orders;
		public IOrdersTable Orders
		{
			get {
				return orders;
			}
			set {
				if (!___initialized) {
					orders= value;
					changed[EnvironmentRuntimeProps.PROPERTY_8_ORDERS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String environmentId;
		public String EnvironmentId
		{
			get {
				return environmentId;
			}
			set {
				if (!___initialized) {
					environmentId= value;
					changed[EnvironmentRuntimeProps.PROPERTY_9_ENVIRONMENTID_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		EnvironmentType environmentType;
		public EnvironmentType EnvironmentType
		{
			get {
				return environmentType;
			}
			set {
				if (environmentType != value) {
					environmentType= value;
					changed[EnvironmentRuntimeProps.PROPERTY_10_ENVIRONMENTTYPE_ID] = true;
					if (EnvironmentTypeChanged != null)
						EnvironmentTypeChanged(this, new PropertyChangedEventArgs("EnvironmentType", value));
				}
			}
		}

		String accountServer;
		public String AccountServer
		{
			get {
				return accountServer;
			}
			set {
				if (accountServer != value) {
					accountServer= value;
					changed[EnvironmentRuntimeProps.PROPERTY_11_ACCOUNTSERVER_ID] = true;
					if (AccountServerChanged != null)
						AccountServerChanged(this, new PropertyChangedEventArgs("AccountServer", value));
				}
			}
		}

		String accountCompany;
		public String AccountCompany
		{
			get {
				return accountCompany;
			}
			set {
				if (accountCompany != value) {
					accountCompany= value;
					changed[EnvironmentRuntimeProps.PROPERTY_12_ACCOUNTCOMPANY_ID] = true;
					if (AccountCompanyChanged != null)
						AccountCompanyChanged(this, new PropertyChangedEventArgs("AccountCompany", value));
				}
			}
		}

		String accountName;
		public String AccountName
		{
			get {
				return accountName;
			}
			set {
				if (accountName != value) {
					accountName= value;
					changed[EnvironmentRuntimeProps.PROPERTY_13_ACCOUNTNAME_ID] = true;
					if (AccountNameChanged != null)
						AccountNameChanged(this, new PropertyChangedEventArgs("AccountName", value));
				}
			}
		}

		Int32 accountNumber;
		public Int32 AccountNumber
		{
			get {
				return accountNumber;
			}
			set {
				if (accountNumber != value) {
					accountNumber= value;
					changed[EnvironmentRuntimeProps.PROPERTY_14_ACCOUNTNUMBER_ID] = true;
					if (AccountNumberChanged != null)
						AccountNumberChanged(this, new PropertyChangedEventArgs("AccountNumber", value));
				}
			}
		}

		String accountCurrency;
		public String AccountCurrency
		{
			get {
				return accountCurrency;
			}
			set {
				if (accountCurrency != value) {
					accountCurrency= value;
					changed[EnvironmentRuntimeProps.PROPERTY_15_ACCOUNTCURRENCY_ID] = true;
					if (AccountCurrencyChanged != null)
						AccountCurrencyChanged(this, new PropertyChangedEventArgs("AccountCurrency", value));
				}
			}
		}

		Int32 accountLeverage;
		public Int32 AccountLeverage
		{
			get {
				return accountLeverage;
			}
			set {
				if (accountLeverage != value) {
					accountLeverage= value;
					changed[EnvironmentRuntimeProps.PROPERTY_16_ACCOUNTLEVERAGE_ID] = true;
					if (AccountLeverageChanged != null)
						AccountLeverageChanged(this, new PropertyChangedEventArgs("AccountLeverage", value));
				}
			}
		}

		Int32 accountFreeMarginMode;
		public Int32 AccountFreeMarginMode
		{
			get {
				return accountFreeMarginMode;
			}
			set {
				if (accountFreeMarginMode != value) {
					accountFreeMarginMode= value;
					changed[EnvironmentRuntimeProps.PROPERTY_17_ACCOUNTFREEMARGINMODE_ID] = true;
					if (AccountFreeMarginModeChanged != null)
						AccountFreeMarginModeChanged(this, new PropertyChangedEventArgs("AccountFreeMarginMode", value));
				}
			}
		}

		Int32 accountStopoutLevel;
		public Int32 AccountStopoutLevel
		{
			get {
				return accountStopoutLevel;
			}
			set {
				if (accountStopoutLevel != value) {
					accountStopoutLevel= value;
					changed[EnvironmentRuntimeProps.PROPERTY_18_ACCOUNTSTOPOUTLEVEL_ID] = true;
					if (AccountStopoutLevelChanged != null)
						AccountStopoutLevelChanged(this, new PropertyChangedEventArgs("AccountStopoutLevel", value));
				}
			}
		}

		Int32 accountStopoutMode;
		public Int32 AccountStopoutMode
		{
			get {
				return accountStopoutMode;
			}
			set {
				if (accountStopoutMode != value) {
					accountStopoutMode= value;
					changed[EnvironmentRuntimeProps.PROPERTY_19_ACCOUNTSTOPOUTMODE_ID] = true;
					if (AccountStopoutModeChanged != null)
						AccountStopoutModeChanged(this, new PropertyChangedEventArgs("AccountStopoutMode", value));
				}
			}
		}

		Boolean isConnected;
		public Boolean IsConnected
		{
			get {
				return isConnected;
			}
			set {
				if (isConnected != value) {
					isConnected= value;
					changed[EnvironmentRuntimeProps.PROPERTY_20_ISCONNECTED_ID] = true;
					if (IsConnectedChanged != null)
						IsConnectedChanged(this, new PropertyChangedEventArgs("IsConnected", value));
				}
			}
		}

		Boolean isStopped;
		public Boolean IsStopped
		{
			get {
				return isStopped;
			}
			set {
				if (isStopped != value) {
					isStopped= value;
					changed[EnvironmentRuntimeProps.PROPERTY_21_ISSTOPPED_ID] = true;
					if (IsStoppedChanged != null)
						IsStoppedChanged(this, new PropertyChangedEventArgs("IsStopped", value));
				}
			}
		}

		Int32 lastError;
		public Int32 LastError
		{
			get {
				return lastError;
			}
			set {
				if (lastError != value) {
					lastError= value;
					changed[EnvironmentRuntimeProps.PROPERTY_22_LASTERROR_ID] = true;
					if (LastErrorChanged != null)
						LastErrorChanged(this, new PropertyChangedEventArgs("LastError", value));
				}
			}
		}

		SlippageMode slippageMode;
		public SlippageMode SlippageMode
		{
			get {
				return slippageMode;
			}
			set {
				if (slippageMode != value) {
					slippageMode= value;
					changed[EnvironmentRuntimeProps.PROPERTY_23_SLIPPAGEMODE_ID] = true;
					if (SlippageModeChanged != null)
						SlippageModeChanged(this, new PropertyChangedEventArgs("SlippageMode", value));
				}
			}
		}

		Int32 randomSeed;
		public Int32 RandomSeed
		{
			get {
				return randomSeed;
			}
			set {
				if (randomSeed != value) {
					randomSeed= value;
					changed[EnvironmentRuntimeProps.PROPERTY_24_RANDOMSEED_ID] = true;
					if (RandomSeedChanged != null)
						RandomSeedChanged(this, new PropertyChangedEventArgs("RandomSeed", value));
				}
			}
		}

		Double startingBalance;
		public Double StartingBalance
		{
			get {
				return startingBalance;
			}
			set {
				if (startingBalance != value) {
					startingBalance= value;
					changed[EnvironmentRuntimeProps.PROPERTY_25_STARTINGBALANCE_ID] = true;
					if (StartingBalanceChanged != null)
						StartingBalanceChanged(this, new PropertyChangedEventArgs("StartingBalance", value));
				}
			}
		}

		String importedFromDirectory;
		public String ImportedFromDirectory
		{
			get {
				return importedFromDirectory;
			}
			set {
				if (importedFromDirectory != value) {
					importedFromDirectory= value;
					changed[EnvironmentRuntimeProps.PROPERTY_26_IMPORTEDFROMDIRECTORY_ID] = true;
					if (ImportedFromDirectoryChanged != null)
						ImportedFromDirectoryChanged(this, new PropertyChangedEventArgs("ImportedFromDirectory", value));
				}
			}
		}

		String historyDirectory;
		public String HistoryDirectory
		{
			get {
				return historyDirectory;
			}
			set {
				if (historyDirectory != value) {
					historyDirectory= value;
					changed[EnvironmentRuntimeProps.PROPERTY_27_HISTORYDIRECTORY_ID] = true;
					if (HistoryDirectoryChanged != null)
						HistoryDirectoryChanged(this, new PropertyChangedEventArgs("HistoryDirectory", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EnvironmentRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!EnvironmentRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
