using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ChartRuntimeProps
	{
		public const int PROPERTY_1_GROUP_ID = 1;
		public const int PROPERTY_2_CURSORRUNTIME_ID = 2;
		public const int PROPERTY_3_SESSION_ID = 3;
		public const int PROPERTY_4_SYMBOLRUNTIME_ID = 4;
		public const int PROPERTY_5_GUISERIESMANAGER_ID = 5;
		public const int PROPERTY_6_SCRIPT_ID = 6;
		public const int PROPERTY_7_EXPERT_ID = 7;
		public const int PROPERTY_8_SYMBOL_ID = 8;
		public const int PROPERTY_9_PERIOD_ID = 9;
		public const int PROPERTY_10_ISMASTER_ID = 10;
		public static bool RmiGetProperty(IChartRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartRuntimeProps.PROPERTY_1_GROUP_ID:
					value = controller.Group;
					return true;
				case ChartRuntimeProps.PROPERTY_2_CURSORRUNTIME_ID:
					value = controller.CursorRuntime;
					return true;
				case ChartRuntimeProps.PROPERTY_3_SESSION_ID:
					value = controller.Session;
					return true;
				case ChartRuntimeProps.PROPERTY_4_SYMBOLRUNTIME_ID:
					value = controller.SymbolRuntime;
					return true;
				case ChartRuntimeProps.PROPERTY_5_GUISERIESMANAGER_ID:
					value = controller.GuiSeriesManager;
					return true;
				case ChartRuntimeProps.PROPERTY_6_SCRIPT_ID:
					value = controller.Script;
					return true;
				case ChartRuntimeProps.PROPERTY_7_EXPERT_ID:
					value = controller.Expert;
					return true;
				case ChartRuntimeProps.PROPERTY_8_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case ChartRuntimeProps.PROPERTY_9_PERIOD_ID:
					value = controller.Period;
					return true;
				case ChartRuntimeProps.PROPERTY_10_ISMASTER_ID:
					value = controller.IsMaster;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartRuntimeProps.PROPERTY_4_SYMBOLRUNTIME_ID:
					controller.SymbolRuntime = (ISymbolRuntime) value;
					return true;
				case ChartRuntimeProps.PROPERTY_6_SCRIPT_ID:
					controller.Script = (IScriptRuntime) value;
					return true;
				case ChartRuntimeProps.PROPERTY_7_EXPERT_ID:
					controller.Expert = (IExpertRuntime) value;
					return true;
				case ChartRuntimeProps.PROPERTY_8_SYMBOL_ID:
					controller.Symbol = (symbol) value;
					return true;
				case ChartRuntimeProps.PROPERTY_9_PERIOD_ID:
					controller.Period = (TimePeriodConst) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Group = (IChartGroupRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_1_GROUP_ID];
			controller.CursorRuntime = (IChartCursorRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_2_CURSORRUNTIME_ID];
			controller.Session = (IChartSession) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_3_SESSION_ID];
			controller.GuiSeriesManager = (ISeriesManagerRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_5_GUISERIESMANAGER_ID];
			controller.IsMaster = (Boolean) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_10_ISMASTER_ID];
		}

		public static void AddDependencies(IChartRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Group);
			controller.Dependencies.Add(controller.CursorRuntime);
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.Add(controller.GuiSeriesManager);
		}

		public static void SerializationRead(IChartRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Group = (IChartGroupRuntime) info.GetValue("Group", typeof(IChartGroupRuntime));
			controller.CursorRuntime = (IChartCursorRuntime) info.GetValue("CursorRuntime", typeof(IChartCursorRuntime));
			controller.Session = (IChartSession) info.GetValue("Session", typeof(IChartSession));
			controller.SymbolRuntime = (ISymbolRuntime) info.GetValue("SymbolRuntime", typeof(ISymbolRuntime));
			controller.GuiSeriesManager = (ISeriesManagerRuntime) info.GetValue("GuiSeriesManager", typeof(ISeriesManagerRuntime));
			controller.Script = (IScriptRuntime) info.GetValue("Script", typeof(IScriptRuntime));
			controller.Expert = (IExpertRuntime) info.GetValue("Expert", typeof(IExpertRuntime));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Period = (TimePeriodConst) info.GetValue("Period", typeof(TimePeriodConst));
			controller.IsMaster = (Boolean) info.GetValue("IsMaster", typeof(Boolean));
		}

		public static void SerializationWrite(IChartRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Group", controller.Group);
			info.AddValue("CursorRuntime", controller.CursorRuntime);
			info.AddValue("Session", controller.Session);
			info.AddValue("SymbolRuntime", controller.SymbolRuntime);
			info.AddValue("GuiSeriesManager", controller.GuiSeriesManager);
			info.AddValue("Script", controller.Script);
			info.AddValue("Expert", controller.Expert);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Period", controller.Period);
			info.AddValue("IsMaster", controller.IsMaster);
		}

	}
	public abstract class ChartRuntimeBase : RmiBase, IChartRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IChartRuntime_SymbolRuntime_Changed;
		public event PropertyChangedEventHandler IChartRuntime_Script_Changed;
		public event PropertyChangedEventHandler IChartRuntime_Expert_Changed;
		public event PropertyChangedEventHandler IChartRuntime_Symbol_Changed;
		public event PropertyChangedEventHandler IChartRuntime_Period_Changed;

		public ChartRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartRuntimeProps.AddDependencies(this, false);
		}

		public ChartRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartRuntimeProps.AddDependencies(this, false);
		}

		protected ChartRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartRuntimeProps.SerializationWrite(this, info, context, false);
		}


		public virtual IEnvironmentRuntime Environment
		{
			get {
				return Group.Environment;
			}
		}

		IChartGroupRuntime _IChartRuntime_Group;
		public IChartGroupRuntime Group
		{
			get {
				return _IChartRuntime_Group;
			}
			set {
				if (!___initialized) {
					_IChartRuntime_Group= value;
					changed[ChartRuntimeProps.PROPERTY_1_GROUP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartCursorRuntime _IChartRuntime_CursorRuntime;
		public IChartCursorRuntime CursorRuntime
		{
			get {
				return _IChartRuntime_CursorRuntime;
			}
			set {
				if (!___initialized) {
					_IChartRuntime_CursorRuntime= value;
					changed[ChartRuntimeProps.PROPERTY_2_CURSORRUNTIME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartSession _IChartRuntime_Session;
		public IChartSession Session
		{
			get {
				return _IChartRuntime_Session;
			}
			set {
				if (!___initialized) {
					_IChartRuntime_Session= value;
					changed[ChartRuntimeProps.PROPERTY_3_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime _IChartRuntime_SymbolRuntime;
		public virtual ISymbolRuntime SymbolRuntime
		{
			get {
				return _IChartRuntime_SymbolRuntime;
			}
			set {
				if (_IChartRuntime_SymbolRuntime != value) {
					_IChartRuntime_SymbolRuntime= value;
					changed[ChartRuntimeProps.PROPERTY_4_SYMBOLRUNTIME_ID] = true;
					if (IChartRuntime_SymbolRuntime_Changed != null)
						IChartRuntime_SymbolRuntime_Changed(this, new PropertyChangedEventArgs("SymbolRuntime", value));
				}
			}
		}

		public virtual ISymbolContext SymbolContext
		{
			get {
				return SymbolRuntime.Context;
			}
		}

		ISeriesManagerRuntime _IChartRuntime_GuiSeriesManager;
		public ISeriesManagerRuntime GuiSeriesManager
		{
			get {
				return _IChartRuntime_GuiSeriesManager;
			}
			set {
				if (!___initialized) {
					_IChartRuntime_GuiSeriesManager= value;
					changed[ChartRuntimeProps.PROPERTY_5_GUISERIESMANAGER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IScriptRuntime _IChartRuntime_Script;
		public virtual IScriptRuntime Script
		{
			get {
				return _IChartRuntime_Script;
			}
			set {
				if (_IChartRuntime_Script != value) {
					_IChartRuntime_Script= value;
					changed[ChartRuntimeProps.PROPERTY_6_SCRIPT_ID] = true;
					if (IChartRuntime_Script_Changed != null)
						IChartRuntime_Script_Changed(this, new PropertyChangedEventArgs("Script", value));
				}
			}
		}

		IExpertRuntime _IChartRuntime_Expert;
		public virtual IExpertRuntime Expert
		{
			get {
				return _IChartRuntime_Expert;
			}
			set {
				if (_IChartRuntime_Expert != value) {
					_IChartRuntime_Expert= value;
					changed[ChartRuntimeProps.PROPERTY_7_EXPERT_ID] = true;
					if (IChartRuntime_Expert_Changed != null)
						IChartRuntime_Expert_Changed(this, new PropertyChangedEventArgs("Expert", value));
				}
			}
		}

		public virtual SeriesRange SeriesRange
		{
			get {
				return Session.SeriesRange;
			}
			set {
				Session.SeriesRange = value;
			}
		}

		public virtual datetime ScrolledBarTime
		{
			get {
				return Session.ScrolledBarTime;
			}
			set {
				Session.ScrolledBarTime = value;
			}
		}

		symbol _IChartRuntime_Symbol;
		public virtual symbol Symbol
		{
			get {
				return _IChartRuntime_Symbol;
			}
			set {
				if (_IChartRuntime_Symbol != value) {
					_IChartRuntime_Symbol= value;
					changed[ChartRuntimeProps.PROPERTY_8_SYMBOL_ID] = true;
					if (IChartRuntime_Symbol_Changed != null)
						IChartRuntime_Symbol_Changed(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		TimePeriodConst _IChartRuntime_Period;
		public virtual TimePeriodConst Period
		{
			get {
				return _IChartRuntime_Period;
			}
			set {
				if (_IChartRuntime_Period != value) {
					_IChartRuntime_Period= value;
					changed[ChartRuntimeProps.PROPERTY_9_PERIOD_ID] = true;
					if (IChartRuntime_Period_Changed != null)
						IChartRuntime_Period_Changed(this, new PropertyChangedEventArgs("Period", value));
				}
			}
		}

		Boolean _IChartRuntime_IsMaster;
		public Boolean IsMaster
		{
			get {
				return _IChartRuntime_IsMaster;
			}
			set {
				if (!___initialized) {
					_IChartRuntime_IsMaster= value;
					changed[ChartRuntimeProps.PROPERTY_10_ISMASTER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual Int32 WindowBarsPerChart
		{
			get {
				return Session.SeriesRange.NumBars;
			}
		}

		public virtual Int32 WindowFirstVisibleBar
		{
			get {
				return GuiSeriesManager.DefaultCache.IndOffset + Session.SeriesRange.OffsetTo;
			}
		}

		public virtual String SymbolFormat
		{
			get {
				return SymbolRuntime.SymbolFormat;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
