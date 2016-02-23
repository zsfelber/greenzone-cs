using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ChartSessionProps
	{
		public const int PROPERTY_1_INDICATORS_ID = 1;
		public const int PROPERTY_2_SCRIPT_ID = 2;
		public const int PROPERTY_3_EXPERT_ID = 3;
		public const int PROPERTY_4_SYMBOL_ID = 4;
		public const int PROPERTY_5_PERIOD_ID = 5;
		public const int PROPERTY_6_CHARTTYPE_ID = 6;
		public const int PROPERTY_7_DEFAULTCHARTTYPE_ID = 7;
		public const int PROPERTY_8_APPEARSINTEST_ID = 8;
		public const int PROPERTY_9_ISCURSORBARCONNECTED_ID = 9;
		public const int PROPERTY_10_FROM_ID = 10;
		public const int PROPERTY_11_TO_ID = 11;
		public const int PROPERTY_12_TOPBARVISIBLE_ID = 12;
		public const int PROPERTY_13_BOTTOMBARVISIBLE_ID = 13;
		public const int PROPERTY_14_SERIESRANGE_ID = 14;
		public const int PROPERTY_15_AUTOSERIESRANGE_ID = 15;
		public const int PROPERTY_16_SCROLLEDBARTIME_ID = 16;
		public const int PROPERTY_17_SCRIPTSTARTSTATUS_ID = 17;
		public const int PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID = 18;
		public static bool RmiGetProperty(IChartSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_INDICATORS_ID:
					value = controller.Indicators;
					return true;
				case PROPERTY_2_SCRIPT_ID:
					value = controller.Script;
					return true;
				case PROPERTY_3_EXPERT_ID:
					value = controller.Expert;
					return true;
				case PROPERTY_4_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_5_PERIOD_ID:
					value = controller.Period;
					return true;
				case PROPERTY_6_CHARTTYPE_ID:
					value = controller.ChartType;
					return true;
				case PROPERTY_7_DEFAULTCHARTTYPE_ID:
					value = controller.DefaultChartType;
					return true;
				case PROPERTY_8_APPEARSINTEST_ID:
					value = controller.AppearsInTest;
					return true;
				case PROPERTY_9_ISCURSORBARCONNECTED_ID:
					value = controller.IsCursorBarConnected;
					return true;
				case PROPERTY_10_FROM_ID:
					value = controller.From;
					return true;
				case PROPERTY_11_TO_ID:
					value = controller.To;
					return true;
				case PROPERTY_12_TOPBARVISIBLE_ID:
					value = controller.TopBarVisible;
					return true;
				case PROPERTY_13_BOTTOMBARVISIBLE_ID:
					value = controller.BottomBarVisible;
					return true;
				case PROPERTY_14_SERIESRANGE_ID:
					value = controller.SeriesRange;
					return true;
				case PROPERTY_15_AUTOSERIESRANGE_ID:
					value = controller.AutoSeriesRange;
					return true;
				case PROPERTY_16_SCROLLEDBARTIME_ID:
					value = controller.ScrolledBarTime;
					return true;
				case PROPERTY_17_SCRIPTSTARTSTATUS_ID:
					value = controller.ScriptStartStatus;
					return true;
				case PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID:
					value = controller.ScriptRunningTickTime;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_2_SCRIPT_ID:
					controller.Script = (IScriptSession) value;
					return true;
				case PROPERTY_3_EXPERT_ID:
					controller.Expert = (IExpertSession) value;
					return true;
				case PROPERTY_4_SYMBOL_ID:
					controller.Symbol = (symbol) value;
					return true;
				case PROPERTY_5_PERIOD_ID:
					controller.Period = (TimePeriodConst) value;
					return true;
				case PROPERTY_6_CHARTTYPE_ID:
					controller.ChartType = (ChartType) value;
					return true;
				case PROPERTY_7_DEFAULTCHARTTYPE_ID:
					controller.DefaultChartType = (ChartType) value;
					return true;
				case PROPERTY_8_APPEARSINTEST_ID:
					controller.AppearsInTest = (Boolean) value;
					return true;
				case PROPERTY_9_ISCURSORBARCONNECTED_ID:
					controller.IsCursorBarConnected = (Boolean) value;
					return true;
				case PROPERTY_10_FROM_ID:
					controller.From = (datetime) value;
					return true;
				case PROPERTY_11_TO_ID:
					controller.To = (datetime) value;
					return true;
				case PROPERTY_12_TOPBARVISIBLE_ID:
					controller.TopBarVisible = (Boolean) value;
					return true;
				case PROPERTY_13_BOTTOMBARVISIBLE_ID:
					controller.BottomBarVisible = (Boolean) value;
					return true;
				case PROPERTY_14_SERIESRANGE_ID:
					controller.SeriesRange = (SeriesRange) value;
					return true;
				case PROPERTY_15_AUTOSERIESRANGE_ID:
					controller.AutoSeriesRange = (Boolean) value;
					return true;
				case PROPERTY_16_SCROLLEDBARTIME_ID:
					controller.ScrolledBarTime = (datetime) value;
					return true;
				case PROPERTY_17_SCRIPTSTARTSTATUS_ID:
					controller.ScriptStartStatus = (StartStatus) value;
					return true;
				case PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID:
					controller.ScriptRunningTickTime = (datetime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartSession controller, GreenRmiObjectBuffer buffer)
		{
			controller.Indicators = (Dictionary<SymbolPeriodIndicatorId,IIndicatorSession>) buffer.ChangedProps[ChartSessionProps.PROPERTY_1_INDICATORS_ID];
		}

		public static void AddDependencies(IChartSession controller)
		{
			controller.Dependencies.AddRange(controller.Indicators.Values);
		}

		public static void SerializationRead(IChartSession controller, SerializationInfo info, StreamingContext context)
		{
			controller.Indicators = (Dictionary<SymbolPeriodIndicatorId,IIndicatorSession>) info.GetValue("Indicators", typeof(Dictionary<SymbolPeriodIndicatorId,IIndicatorSession>));
			controller.Script = (IScriptSession) info.GetValue("Script", typeof(IScriptSession));
			controller.Expert = (IExpertSession) info.GetValue("Expert", typeof(IExpertSession));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Period = (TimePeriodConst) info.GetValue("Period", typeof(TimePeriodConst));
			controller.ChartType = (ChartType) info.GetValue("ChartType", typeof(ChartType));
			controller.DefaultChartType = (ChartType) info.GetValue("DefaultChartType", typeof(ChartType));
			controller.AppearsInTest = (Boolean) info.GetValue("AppearsInTest", typeof(Boolean));
			controller.IsCursorBarConnected = (Boolean) info.GetValue("IsCursorBarConnected", typeof(Boolean));
			controller.From = (datetime) info.GetValue("From", typeof(datetime));
			controller.To = (datetime) info.GetValue("To", typeof(datetime));
			controller.TopBarVisible = (Boolean) info.GetValue("TopBarVisible", typeof(Boolean));
			controller.BottomBarVisible = (Boolean) info.GetValue("BottomBarVisible", typeof(Boolean));
			controller.SeriesRange = (SeriesRange) info.GetValue("SeriesRange", typeof(SeriesRange));
			controller.AutoSeriesRange = (Boolean) info.GetValue("AutoSeriesRange", typeof(Boolean));
			controller.ScrolledBarTime = (datetime) info.GetValue("ScrolledBarTime", typeof(datetime));
			controller.ScriptStartStatus = (StartStatus) info.GetValue("ScriptStartStatus", typeof(StartStatus));
			controller.ScriptRunningTickTime = (datetime) info.GetValue("ScriptRunningTickTime", typeof(datetime));
		}

		public static void SerializationWrite(IChartSession controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Indicators", controller.Indicators);
			info.AddValue("Script", controller.Script);
			info.AddValue("Expert", controller.Expert);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Period", controller.Period);
			info.AddValue("ChartType", controller.ChartType);
			info.AddValue("DefaultChartType", controller.DefaultChartType);
			info.AddValue("AppearsInTest", controller.AppearsInTest);
			info.AddValue("IsCursorBarConnected", controller.IsCursorBarConnected);
			info.AddValue("From", controller.From);
			info.AddValue("To", controller.To);
			info.AddValue("TopBarVisible", controller.TopBarVisible);
			info.AddValue("BottomBarVisible", controller.BottomBarVisible);
			info.AddValue("SeriesRange", controller.SeriesRange);
			info.AddValue("AutoSeriesRange", controller.AutoSeriesRange);
			info.AddValue("ScrolledBarTime", controller.ScrolledBarTime);
			info.AddValue("ScriptStartStatus", controller.ScriptStartStatus);
			info.AddValue("ScriptRunningTickTime", controller.ScriptRunningTickTime);
		}

	}
	public abstract class ChartSessionBase : RmiBase, IChartSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ScriptChanged;
		public event PropertyChangedEventHandler ExpertChanged;
		public event PropertyChangedEventHandler SymbolChanged;
		public event PropertyChangedEventHandler PeriodChanged;
		public event PropertyChangedEventHandler ChartTypeChanged;
		public event PropertyChangedEventHandler DefaultChartTypeChanged;
		public event PropertyChangedEventHandler AppearsInTestChanged;
		public event PropertyChangedEventHandler IsCursorBarConnectedChanged;
		public event PropertyChangedEventHandler FromChanged;
		public event PropertyChangedEventHandler ToChanged;
		public event PropertyChangedEventHandler TopBarVisibleChanged;
		public event PropertyChangedEventHandler BottomBarVisibleChanged;
		public event PropertyChangedEventHandler SeriesRangeChanged;
		public event PropertyChangedEventHandler AutoSeriesRangeChanged;
		public event PropertyChangedEventHandler ScrolledBarTimeChanged;
		public event PropertyChangedEventHandler ScriptStartStatusChanged;
		public event PropertyChangedEventHandler ScriptRunningTickTimeChanged;

		public ChartSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartSessionProps.AddDependencies(this);
		}

		public ChartSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartSessionProps.Initialize(this, buffer);
			___initialized = true;
			ChartSessionProps.AddDependencies(this);
		}

		protected ChartSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartSessionProps.SerializationWrite(this, info, context);
		}

		Dictionary<SymbolPeriodIndicatorId,IIndicatorSession> indicators;
		public Dictionary<SymbolPeriodIndicatorId,IIndicatorSession> Indicators
		{
			get {
				return indicators;
			}
			set {
				if (!___initialized) {
					indicators= value;
					changed[ChartSessionProps.PROPERTY_1_INDICATORS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IScriptSession script;
		public IScriptSession Script
		{
			get {
				return script;
			}
			set {
				if (script != value) {
					script= value;
					changed[ChartSessionProps.PROPERTY_2_SCRIPT_ID] = true;
					if (ScriptChanged != null)
						ScriptChanged(this, new PropertyChangedEventArgs("Script", value));
				}
			}
		}

		IExpertSession expert;
		public IExpertSession Expert
		{
			get {
				return expert;
			}
			set {
				if (expert != value) {
					expert= value;
					changed[ChartSessionProps.PROPERTY_3_EXPERT_ID] = true;
					if (ExpertChanged != null)
						ExpertChanged(this, new PropertyChangedEventArgs("Expert", value));
				}
			}
		}

		symbol symbol;
		public symbol Symbol
		{
			get {
				return symbol;
			}
			set {
				if (symbol != value) {
					symbol= value;
					changed[ChartSessionProps.PROPERTY_4_SYMBOL_ID] = true;
					if (SymbolChanged != null)
						SymbolChanged(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		TimePeriodConst period;
		public TimePeriodConst Period
		{
			get {
				return period;
			}
			set {
				if (period != value) {
					period= value;
					changed[ChartSessionProps.PROPERTY_5_PERIOD_ID] = true;
					if (PeriodChanged != null)
						PeriodChanged(this, new PropertyChangedEventArgs("Period", value));
				}
			}
		}

		ChartType chartType;
		public virtual ChartType ChartType
		{
			get {
				return chartType;
			}
			set {
				if (chartType != value) {
					chartType= value;
					changed[ChartSessionProps.PROPERTY_6_CHARTTYPE_ID] = true;
					if (ChartTypeChanged != null)
						ChartTypeChanged(this, new PropertyChangedEventArgs("ChartType", value));
				}
			}
		}

		ChartType defaultChartType;
		public ChartType DefaultChartType
		{
			get {
				return defaultChartType;
			}
			set {
				if (defaultChartType != value) {
					defaultChartType= value;
					changed[ChartSessionProps.PROPERTY_7_DEFAULTCHARTTYPE_ID] = true;
					if (DefaultChartTypeChanged != null)
						DefaultChartTypeChanged(this, new PropertyChangedEventArgs("DefaultChartType", value));
				}
			}
		}

		Boolean appearsInTest;
		public Boolean AppearsInTest
		{
			get {
				return appearsInTest;
			}
			set {
				if (appearsInTest != value) {
					appearsInTest= value;
					changed[ChartSessionProps.PROPERTY_8_APPEARSINTEST_ID] = true;
					if (AppearsInTestChanged != null)
						AppearsInTestChanged(this, new PropertyChangedEventArgs("AppearsInTest", value));
				}
			}
		}

		Boolean isCursorBarConnected;
		public Boolean IsCursorBarConnected
		{
			get {
				return isCursorBarConnected;
			}
			set {
				if (isCursorBarConnected != value) {
					isCursorBarConnected= value;
					changed[ChartSessionProps.PROPERTY_9_ISCURSORBARCONNECTED_ID] = true;
					if (IsCursorBarConnectedChanged != null)
						IsCursorBarConnectedChanged(this, new PropertyChangedEventArgs("IsCursorBarConnected", value));
				}
			}
		}

		datetime from;
		public datetime From
		{
			get {
				return from;
			}
			set {
				if (from != value) {
					from= value;
					changed[ChartSessionProps.PROPERTY_10_FROM_ID] = true;
					if (FromChanged != null)
						FromChanged(this, new PropertyChangedEventArgs("From", value));
				}
			}
		}

		datetime to;
		public datetime To
		{
			get {
				return to;
			}
			set {
				if (to != value) {
					to= value;
					changed[ChartSessionProps.PROPERTY_11_TO_ID] = true;
					if (ToChanged != null)
						ToChanged(this, new PropertyChangedEventArgs("To", value));
				}
			}
		}

		Boolean topBarVisible;
		public Boolean TopBarVisible
		{
			get {
				return topBarVisible;
			}
			set {
				if (topBarVisible != value) {
					topBarVisible= value;
					changed[ChartSessionProps.PROPERTY_12_TOPBARVISIBLE_ID] = true;
					if (TopBarVisibleChanged != null)
						TopBarVisibleChanged(this, new PropertyChangedEventArgs("TopBarVisible", value));
				}
			}
		}

		Boolean bottomBarVisible;
		public Boolean BottomBarVisible
		{
			get {
				return bottomBarVisible;
			}
			set {
				if (bottomBarVisible != value) {
					bottomBarVisible= value;
					changed[ChartSessionProps.PROPERTY_13_BOTTOMBARVISIBLE_ID] = true;
					if (BottomBarVisibleChanged != null)
						BottomBarVisibleChanged(this, new PropertyChangedEventArgs("BottomBarVisible", value));
				}
			}
		}

		SeriesRange seriesRange;
		public SeriesRange SeriesRange
		{
			get {
				return seriesRange;
			}
			set {
				if (seriesRange != value) {
					seriesRange= value;
					changed[ChartSessionProps.PROPERTY_14_SERIESRANGE_ID] = true;
					if (SeriesRangeChanged != null)
						SeriesRangeChanged(this, new PropertyChangedEventArgs("SeriesRange", value));
				}
			}
		}

		Boolean autoSeriesRange;
		public Boolean AutoSeriesRange
		{
			get {
				return autoSeriesRange;
			}
			set {
				if (autoSeriesRange != value) {
					autoSeriesRange= value;
					changed[ChartSessionProps.PROPERTY_15_AUTOSERIESRANGE_ID] = true;
					if (AutoSeriesRangeChanged != null)
						AutoSeriesRangeChanged(this, new PropertyChangedEventArgs("AutoSeriesRange", value));
				}
			}
		}

		datetime scrolledBarTime;
		public datetime ScrolledBarTime
		{
			get {
				return scrolledBarTime;
			}
			set {
				if (scrolledBarTime != value) {
					scrolledBarTime= value;
					changed[ChartSessionProps.PROPERTY_16_SCROLLEDBARTIME_ID] = true;
					if (ScrolledBarTimeChanged != null)
						ScrolledBarTimeChanged(this, new PropertyChangedEventArgs("ScrolledBarTime", value));
				}
			}
		}

		StartStatus scriptStartStatus;
		public StartStatus ScriptStartStatus
		{
			get {
				return scriptStartStatus;
			}
			set {
				if (scriptStartStatus != value) {
					scriptStartStatus= value;
					changed[ChartSessionProps.PROPERTY_17_SCRIPTSTARTSTATUS_ID] = true;
					if (ScriptStartStatusChanged != null)
						ScriptStartStatusChanged(this, new PropertyChangedEventArgs("ScriptStartStatus", value));
				}
			}
		}

		datetime scriptRunningTickTime;
		public datetime ScriptRunningTickTime
		{
			get {
				return scriptRunningTickTime;
			}
			set {
				if (scriptRunningTickTime != value) {
					scriptRunningTickTime= value;
					changed[ChartSessionProps.PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID] = true;
					if (ScriptRunningTickTimeChanged != null)
						ScriptRunningTickTimeChanged(this, new PropertyChangedEventArgs("ScriptRunningTickTime", value));
				}
			}
		}

		public abstract Int32 ScriptRunningProgress
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
