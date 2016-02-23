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
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_GROUP_ID = 2;
		public const int PROPERTY_3_CURSORRUNTIME_ID = 3;
		public const int PROPERTY_4_SESSION_ID = 4;
		public const int PROPERTY_5_SYMBOLRUNTIME_ID = 5;
		public const int PROPERTY_6_GUISERIESMANAGER_ID = 6;
		public const int PROPERTY_7_SCRIPT_ID = 7;
		public const int PROPERTY_8_EXPERT_ID = 8;
		public const int PROPERTY_9_SYMBOL_ID = 9;
		public const int PROPERTY_10_PERIOD_ID = 10;
		public const int PROPERTY_11_ISMASTER_ID = 11;
		public static bool RmiGetProperty(IChartRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case PROPERTY_2_GROUP_ID:
					value = controller.Group;
					return true;
				case PROPERTY_3_CURSORRUNTIME_ID:
					value = controller.CursorRuntime;
					return true;
				case PROPERTY_4_SESSION_ID:
					value = controller.Session;
					return true;
				case PROPERTY_5_SYMBOLRUNTIME_ID:
					value = controller.SymbolRuntime;
					return true;
				case PROPERTY_6_GUISERIESMANAGER_ID:
					value = controller.GuiSeriesManager;
					return true;
				case PROPERTY_7_SCRIPT_ID:
					value = controller.Script;
					return true;
				case PROPERTY_8_EXPERT_ID:
					value = controller.Expert;
					return true;
				case PROPERTY_9_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_10_PERIOD_ID:
					value = controller.Period;
					return true;
				case PROPERTY_11_ISMASTER_ID:
					value = controller.IsMaster;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_5_SYMBOLRUNTIME_ID:
					controller.SymbolRuntime = (ISymbolRuntime) value;
					return true;
				case PROPERTY_7_SCRIPT_ID:
					controller.Script = (IScriptRuntime) value;
					return true;
				case PROPERTY_8_EXPERT_ID:
					controller.Expert = (IExpertRuntime) value;
					return true;
				case PROPERTY_9_SYMBOL_ID:
					controller.Symbol = (symbol) value;
					return true;
				case PROPERTY_10_PERIOD_ID:
					controller.Period = (TimePeriodConst) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Group = (IChartGroupRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_2_GROUP_ID];
			controller.CursorRuntime = (IChartCursorRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_3_CURSORRUNTIME_ID];
			controller.Session = (IChartSession) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_4_SESSION_ID];
			controller.GuiSeriesManager = (ISeriesManagerRuntime) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_6_GUISERIESMANAGER_ID];
			controller.IsMaster = (Boolean) buffer.ChangedProps[ChartRuntimeProps.PROPERTY_11_ISMASTER_ID];
		}

		public static void AddDependencies(IChartRuntime controller)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Group);
			controller.Dependencies.Add(controller.CursorRuntime);
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.Add(controller.GuiSeriesManager);
		}

		public static void SerializationRead(IChartRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
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

		public static void SerializationWrite(IChartRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Environment", controller.Environment);
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

		public event PropertyChangedEventHandler SymbolRuntimeChanged;
		public event PropertyChangedEventHandler ScriptChanged;
		public event PropertyChangedEventHandler ExpertChanged;
		public event PropertyChangedEventHandler SymbolChanged;
		public event PropertyChangedEventHandler PeriodChanged;

		public ChartRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartRuntimeProps.AddDependencies(this);
		}

		public ChartRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ChartRuntimeProps.AddDependencies(this);
		}

		protected ChartRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartRuntimeProps.SerializationWrite(this, info, context);
		}

		IEnvironmentRuntime environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return environment;
			}
			set {
				if (!___initialized) {
					environment= value;
					changed[ChartRuntimeProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartGroupRuntime group;
		public IChartGroupRuntime Group
		{
			get {
				return group;
			}
			set {
				if (!___initialized) {
					group= value;
					changed[ChartRuntimeProps.PROPERTY_2_GROUP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartCursorRuntime cursorRuntime;
		public IChartCursorRuntime CursorRuntime
		{
			get {
				return cursorRuntime;
			}
			set {
				if (!___initialized) {
					cursorRuntime= value;
					changed[ChartRuntimeProps.PROPERTY_3_CURSORRUNTIME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartSession session;
		public IChartSession Session
		{
			get {
				return session;
			}
			set {
				if (!___initialized) {
					session= value;
					changed[ChartRuntimeProps.PROPERTY_4_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime symbolRuntime;
		public ISymbolRuntime SymbolRuntime
		{
			get {
				return symbolRuntime;
			}
			set {
				if (symbolRuntime != value) {
					symbolRuntime= value;
					changed[ChartRuntimeProps.PROPERTY_5_SYMBOLRUNTIME_ID] = true;
					if (SymbolRuntimeChanged != null)
						SymbolRuntimeChanged(this, new PropertyChangedEventArgs("SymbolRuntime", value));
				}
			}
		}

		public virtual ISymbolContext SymbolContext
		{
			get {
				return SymbolRuntime.Context;
			}
		}

		ISeriesManagerRuntime guiSeriesManager;
		public ISeriesManagerRuntime GuiSeriesManager
		{
			get {
				return guiSeriesManager;
			}
			set {
				if (!___initialized) {
					guiSeriesManager= value;
					changed[ChartRuntimeProps.PROPERTY_6_GUISERIESMANAGER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IScriptRuntime script;
		public virtual IScriptRuntime Script
		{
			get {
				return script;
			}
			set {
				if (script != value) {
					script= value;
					changed[ChartRuntimeProps.PROPERTY_7_SCRIPT_ID] = true;
					if (ScriptChanged != null)
						ScriptChanged(this, new PropertyChangedEventArgs("Script", value));
				}
			}
		}

		IExpertRuntime expert;
		public virtual IExpertRuntime Expert
		{
			get {
				return expert;
			}
			set {
				if (expert != value) {
					expert= value;
					changed[ChartRuntimeProps.PROPERTY_8_EXPERT_ID] = true;
					if (ExpertChanged != null)
						ExpertChanged(this, new PropertyChangedEventArgs("Expert", value));
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

		symbol symbol;
		public virtual symbol Symbol
		{
			get {
				return symbol;
			}
			set {
				if (symbol != value) {
					symbol= value;
					changed[ChartRuntimeProps.PROPERTY_9_SYMBOL_ID] = true;
					if (SymbolChanged != null)
						SymbolChanged(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		TimePeriodConst period;
		public virtual TimePeriodConst Period
		{
			get {
				return period;
			}
			set {
				if (period != value) {
					period= value;
					changed[ChartRuntimeProps.PROPERTY_10_PERIOD_ID] = true;
					if (PeriodChanged != null)
						PeriodChanged(this, new PropertyChangedEventArgs("Period", value));
				}
			}
		}

		Boolean isMaster;
		public Boolean IsMaster
		{
			get {
				return isMaster;
			}
			set {
				if (!___initialized) {
					isMaster= value;
					changed[ChartRuntimeProps.PROPERTY_11_ISMASTER_ID] = true;
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
