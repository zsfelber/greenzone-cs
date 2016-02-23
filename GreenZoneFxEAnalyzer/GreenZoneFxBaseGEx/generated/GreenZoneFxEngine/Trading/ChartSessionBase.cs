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
		public static bool RmiGetProperty(IChartSession controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartSessionProps.PROPERTY_1_INDICATORS_ID:
					value = controller.Indicators;
					return true;
				case ChartSessionProps.PROPERTY_2_SCRIPT_ID:
					value = controller.Script;
					return true;
				case ChartSessionProps.PROPERTY_3_EXPERT_ID:
					value = controller.Expert;
					return true;
				case ChartSessionProps.PROPERTY_4_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case ChartSessionProps.PROPERTY_5_PERIOD_ID:
					value = controller.Period;
					return true;
				case ChartSessionProps.PROPERTY_6_CHARTTYPE_ID:
					value = controller.ChartType;
					return true;
				case ChartSessionProps.PROPERTY_7_DEFAULTCHARTTYPE_ID:
					value = controller.DefaultChartType;
					return true;
				case ChartSessionProps.PROPERTY_8_APPEARSINTEST_ID:
					value = controller.AppearsInTest;
					return true;
				case ChartSessionProps.PROPERTY_9_ISCURSORBARCONNECTED_ID:
					value = controller.IsCursorBarConnected;
					return true;
				case ChartSessionProps.PROPERTY_10_FROM_ID:
					value = controller.From;
					return true;
				case ChartSessionProps.PROPERTY_11_TO_ID:
					value = controller.To;
					return true;
				case ChartSessionProps.PROPERTY_12_TOPBARVISIBLE_ID:
					value = controller.TopBarVisible;
					return true;
				case ChartSessionProps.PROPERTY_13_BOTTOMBARVISIBLE_ID:
					value = controller.BottomBarVisible;
					return true;
				case ChartSessionProps.PROPERTY_14_SERIESRANGE_ID:
					value = controller.SeriesRange;
					return true;
				case ChartSessionProps.PROPERTY_15_AUTOSERIESRANGE_ID:
					value = controller.AutoSeriesRange;
					return true;
				case ChartSessionProps.PROPERTY_16_SCROLLEDBARTIME_ID:
					value = controller.ScrolledBarTime;
					return true;
				case ChartSessionProps.PROPERTY_17_SCRIPTSTARTSTATUS_ID:
					value = controller.ScriptStartStatus;
					return true;
				case ChartSessionProps.PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID:
					value = controller.ScriptRunningTickTime;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartSession controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartSessionProps.PROPERTY_2_SCRIPT_ID:
					controller.Script = (IScriptSession) value;
					return true;
				case ChartSessionProps.PROPERTY_3_EXPERT_ID:
					controller.Expert = (IExpertSession) value;
					return true;
				case ChartSessionProps.PROPERTY_4_SYMBOL_ID:
					controller.Symbol = (symbol) value;
					return true;
				case ChartSessionProps.PROPERTY_5_PERIOD_ID:
					controller.Period = (TimePeriodConst) value;
					return true;
				case ChartSessionProps.PROPERTY_6_CHARTTYPE_ID:
					controller.ChartType = (ChartType) value;
					return true;
				case ChartSessionProps.PROPERTY_7_DEFAULTCHARTTYPE_ID:
					controller.DefaultChartType = (ChartType) value;
					return true;
				case ChartSessionProps.PROPERTY_8_APPEARSINTEST_ID:
					controller.AppearsInTest = (Boolean) value;
					return true;
				case ChartSessionProps.PROPERTY_9_ISCURSORBARCONNECTED_ID:
					controller.IsCursorBarConnected = (Boolean) value;
					return true;
				case ChartSessionProps.PROPERTY_10_FROM_ID:
					controller.From = (datetime) value;
					return true;
				case ChartSessionProps.PROPERTY_11_TO_ID:
					controller.To = (datetime) value;
					return true;
				case ChartSessionProps.PROPERTY_12_TOPBARVISIBLE_ID:
					controller.TopBarVisible = (Boolean) value;
					return true;
				case ChartSessionProps.PROPERTY_13_BOTTOMBARVISIBLE_ID:
					controller.BottomBarVisible = (Boolean) value;
					return true;
				case ChartSessionProps.PROPERTY_14_SERIESRANGE_ID:
					controller.SeriesRange = (SeriesRange) value;
					return true;
				case ChartSessionProps.PROPERTY_15_AUTOSERIESRANGE_ID:
					controller.AutoSeriesRange = (Boolean) value;
					return true;
				case ChartSessionProps.PROPERTY_16_SCROLLEDBARTIME_ID:
					controller.ScrolledBarTime = (datetime) value;
					return true;
				case ChartSessionProps.PROPERTY_17_SCRIPTSTARTSTATUS_ID:
					controller.ScriptStartStatus = (StartStatus) value;
					return true;
				case ChartSessionProps.PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID:
					controller.ScriptRunningTickTime = (datetime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Indicators = (Dictionary<SymbolPeriodIndicatorId,IIndicatorSession>) buffer.ChangedProps[ChartSessionProps.PROPERTY_1_INDICATORS_ID];
		}

		public static void AddDependencies(IChartSession controller, bool goToParent)
		{
			controller.Dependencies.AddRange(controller.Indicators.Values);
		}

		public static void SerializationRead(IChartSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IChartSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IChartSession_Script_Changed;
		public event PropertyChangedEventHandler IChartSession_Expert_Changed;
		public event PropertyChangedEventHandler IChartSession_Symbol_Changed;
		public event PropertyChangedEventHandler IChartSession_Period_Changed;
		public event PropertyChangedEventHandler IChartSession_ChartType_Changed;
		public event PropertyChangedEventHandler IChartSession_DefaultChartType_Changed;
		public event PropertyChangedEventHandler IChartSession_AppearsInTest_Changed;
		public event PropertyChangedEventHandler IChartSession_IsCursorBarConnected_Changed;
		public event PropertyChangedEventHandler IChartSession_From_Changed;
		public event PropertyChangedEventHandler IChartSession_To_Changed;
		public event PropertyChangedEventHandler IChartSession_TopBarVisible_Changed;
		public event PropertyChangedEventHandler IChartSession_BottomBarVisible_Changed;
		public event PropertyChangedEventHandler IChartSession_SeriesRange_Changed;
		public event PropertyChangedEventHandler IChartSession_AutoSeriesRange_Changed;
		public event PropertyChangedEventHandler IChartSession_ScrolledBarTime_Changed;
		public event PropertyChangedEventHandler IChartSession_ScriptStartStatus_Changed;
		public event PropertyChangedEventHandler IChartSession_ScriptRunningTickTime_Changed;

		public ChartSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartSessionProps.AddDependencies(this, false);
		}

		public ChartSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartSessionProps.AddDependencies(this, false);
		}

		protected ChartSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartSessionProps.SerializationWrite(this, info, context, false);
		}


		Dictionary<SymbolPeriodIndicatorId,IIndicatorSession> _IChartSession_Indicators;
		public Dictionary<SymbolPeriodIndicatorId,IIndicatorSession> Indicators
		{
			get {
				return _IChartSession_Indicators;
			}
			set {
				if (!___initialized) {
					_IChartSession_Indicators= value;
					changed[ChartSessionProps.PROPERTY_1_INDICATORS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IScriptSession _IChartSession_Script;
		public IScriptSession Script
		{
			get {
				return _IChartSession_Script;
			}
			set {
				if (_IChartSession_Script != value) {
					_IChartSession_Script= value;
					changed[ChartSessionProps.PROPERTY_2_SCRIPT_ID] = true;
					if (IChartSession_Script_Changed != null)
						IChartSession_Script_Changed(this, new PropertyChangedEventArgs("Script", value));
				}
			}
		}

		IExpertSession _IChartSession_Expert;
		public IExpertSession Expert
		{
			get {
				return _IChartSession_Expert;
			}
			set {
				if (_IChartSession_Expert != value) {
					_IChartSession_Expert= value;
					changed[ChartSessionProps.PROPERTY_3_EXPERT_ID] = true;
					if (IChartSession_Expert_Changed != null)
						IChartSession_Expert_Changed(this, new PropertyChangedEventArgs("Expert", value));
				}
			}
		}

		symbol _IChartSession_Symbol;
		public symbol Symbol
		{
			get {
				return _IChartSession_Symbol;
			}
			set {
				if (_IChartSession_Symbol != value) {
					_IChartSession_Symbol= value;
					changed[ChartSessionProps.PROPERTY_4_SYMBOL_ID] = true;
					if (IChartSession_Symbol_Changed != null)
						IChartSession_Symbol_Changed(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		TimePeriodConst _IChartSession_Period;
		public TimePeriodConst Period
		{
			get {
				return _IChartSession_Period;
			}
			set {
				if (_IChartSession_Period != value) {
					_IChartSession_Period= value;
					changed[ChartSessionProps.PROPERTY_5_PERIOD_ID] = true;
					if (IChartSession_Period_Changed != null)
						IChartSession_Period_Changed(this, new PropertyChangedEventArgs("Period", value));
				}
			}
		}

		ChartType _IChartSession_ChartType;
		public virtual ChartType ChartType
		{
			get {
				return _IChartSession_ChartType;
			}
			set {
				if (_IChartSession_ChartType != value) {
					_IChartSession_ChartType= value;
					changed[ChartSessionProps.PROPERTY_6_CHARTTYPE_ID] = true;
					if (IChartSession_ChartType_Changed != null)
						IChartSession_ChartType_Changed(this, new PropertyChangedEventArgs("ChartType", value));
				}
			}
		}

		ChartType _IChartSession_DefaultChartType;
		public ChartType DefaultChartType
		{
			get {
				return _IChartSession_DefaultChartType;
			}
			set {
				if (_IChartSession_DefaultChartType != value) {
					_IChartSession_DefaultChartType= value;
					changed[ChartSessionProps.PROPERTY_7_DEFAULTCHARTTYPE_ID] = true;
					if (IChartSession_DefaultChartType_Changed != null)
						IChartSession_DefaultChartType_Changed(this, new PropertyChangedEventArgs("DefaultChartType", value));
				}
			}
		}

		Boolean _IChartSession_AppearsInTest;
		public Boolean AppearsInTest
		{
			get {
				return _IChartSession_AppearsInTest;
			}
			set {
				if (_IChartSession_AppearsInTest != value) {
					_IChartSession_AppearsInTest= value;
					changed[ChartSessionProps.PROPERTY_8_APPEARSINTEST_ID] = true;
					if (IChartSession_AppearsInTest_Changed != null)
						IChartSession_AppearsInTest_Changed(this, new PropertyChangedEventArgs("AppearsInTest", value));
				}
			}
		}

		Boolean _IChartSession_IsCursorBarConnected;
		public Boolean IsCursorBarConnected
		{
			get {
				return _IChartSession_IsCursorBarConnected;
			}
			set {
				if (_IChartSession_IsCursorBarConnected != value) {
					_IChartSession_IsCursorBarConnected= value;
					changed[ChartSessionProps.PROPERTY_9_ISCURSORBARCONNECTED_ID] = true;
					if (IChartSession_IsCursorBarConnected_Changed != null)
						IChartSession_IsCursorBarConnected_Changed(this, new PropertyChangedEventArgs("IsCursorBarConnected", value));
				}
			}
		}

		datetime _IChartSession_From;
		public datetime From
		{
			get {
				return _IChartSession_From;
			}
			set {
				if (_IChartSession_From != value) {
					_IChartSession_From= value;
					changed[ChartSessionProps.PROPERTY_10_FROM_ID] = true;
					if (IChartSession_From_Changed != null)
						IChartSession_From_Changed(this, new PropertyChangedEventArgs("From", value));
				}
			}
		}

		datetime _IChartSession_To;
		public datetime To
		{
			get {
				return _IChartSession_To;
			}
			set {
				if (_IChartSession_To != value) {
					_IChartSession_To= value;
					changed[ChartSessionProps.PROPERTY_11_TO_ID] = true;
					if (IChartSession_To_Changed != null)
						IChartSession_To_Changed(this, new PropertyChangedEventArgs("To", value));
				}
			}
		}

		Boolean _IChartSession_TopBarVisible;
		public Boolean TopBarVisible
		{
			get {
				return _IChartSession_TopBarVisible;
			}
			set {
				if (_IChartSession_TopBarVisible != value) {
					_IChartSession_TopBarVisible= value;
					changed[ChartSessionProps.PROPERTY_12_TOPBARVISIBLE_ID] = true;
					if (IChartSession_TopBarVisible_Changed != null)
						IChartSession_TopBarVisible_Changed(this, new PropertyChangedEventArgs("TopBarVisible", value));
				}
			}
		}

		Boolean _IChartSession_BottomBarVisible;
		public Boolean BottomBarVisible
		{
			get {
				return _IChartSession_BottomBarVisible;
			}
			set {
				if (_IChartSession_BottomBarVisible != value) {
					_IChartSession_BottomBarVisible= value;
					changed[ChartSessionProps.PROPERTY_13_BOTTOMBARVISIBLE_ID] = true;
					if (IChartSession_BottomBarVisible_Changed != null)
						IChartSession_BottomBarVisible_Changed(this, new PropertyChangedEventArgs("BottomBarVisible", value));
				}
			}
		}

		SeriesRange _IChartSession_SeriesRange;
		public SeriesRange SeriesRange
		{
			get {
				return _IChartSession_SeriesRange;
			}
			set {
				if (_IChartSession_SeriesRange != value) {
					_IChartSession_SeriesRange= value;
					changed[ChartSessionProps.PROPERTY_14_SERIESRANGE_ID] = true;
					if (IChartSession_SeriesRange_Changed != null)
						IChartSession_SeriesRange_Changed(this, new PropertyChangedEventArgs("SeriesRange", value));
				}
			}
		}

		Boolean _IChartSession_AutoSeriesRange;
		public Boolean AutoSeriesRange
		{
			get {
				return _IChartSession_AutoSeriesRange;
			}
			set {
				if (_IChartSession_AutoSeriesRange != value) {
					_IChartSession_AutoSeriesRange= value;
					changed[ChartSessionProps.PROPERTY_15_AUTOSERIESRANGE_ID] = true;
					if (IChartSession_AutoSeriesRange_Changed != null)
						IChartSession_AutoSeriesRange_Changed(this, new PropertyChangedEventArgs("AutoSeriesRange", value));
				}
			}
		}

		datetime _IChartSession_ScrolledBarTime;
		public datetime ScrolledBarTime
		{
			get {
				return _IChartSession_ScrolledBarTime;
			}
			set {
				if (_IChartSession_ScrolledBarTime != value) {
					_IChartSession_ScrolledBarTime= value;
					changed[ChartSessionProps.PROPERTY_16_SCROLLEDBARTIME_ID] = true;
					if (IChartSession_ScrolledBarTime_Changed != null)
						IChartSession_ScrolledBarTime_Changed(this, new PropertyChangedEventArgs("ScrolledBarTime", value));
				}
			}
		}

		StartStatus _IChartSession_ScriptStartStatus;
		public StartStatus ScriptStartStatus
		{
			get {
				return _IChartSession_ScriptStartStatus;
			}
			set {
				if (_IChartSession_ScriptStartStatus != value) {
					_IChartSession_ScriptStartStatus= value;
					changed[ChartSessionProps.PROPERTY_17_SCRIPTSTARTSTATUS_ID] = true;
					if (IChartSession_ScriptStartStatus_Changed != null)
						IChartSession_ScriptStartStatus_Changed(this, new PropertyChangedEventArgs("ScriptStartStatus", value));
				}
			}
		}

		datetime _IChartSession_ScriptRunningTickTime;
		public datetime ScriptRunningTickTime
		{
			get {
				return _IChartSession_ScriptRunningTickTime;
			}
			set {
				if (_IChartSession_ScriptRunningTickTime != value) {
					_IChartSession_ScriptRunningTickTime= value;
					changed[ChartSessionProps.PROPERTY_18_SCRIPTRUNNINGTICKTIME_ID] = true;
					if (IChartSession_ScriptRunningTickTime_Changed != null)
						IChartSession_ScriptRunningTickTime_Changed(this, new PropertyChangedEventArgs("ScriptRunningTickTime", value));
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
			if (ChartSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
