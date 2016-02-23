using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class EnvironmentSessionProps
	{
		public const int PROPERTY_1_SYMBOLSESSIONS_ID = 1;
		public const int PROPERTY_2_CHARTSESSIONS_ID = 2;
		public const int PROPERTY_3_ENVIRONMENTID_ID = 3;
		public const int PROPERTY_4_SCROLLACROSSCHARTS_ID = 4;
		public const int PROPERTY_5_SKIPEMPTYPERIODS_ID = 5;
		public const int PROPERTY_6_EATESTINGSPEED_ID = 6;
		public const int PROPERTY_7_EASTARTSTATUS_ID = 7;
		public const int PROPERTY_8_EATESTINGGLOBALFROM_ID = 8;
		public const int PROPERTY_9_EATESTINGGLOBALTO_ID = 9;
		public const int PROPERTY_10_EATESTINGTRACKBARTICK_ID = 10;
		public const int PROPERTY_11_EATESTINGSNAPMODE_ID = 11;
		public const int PROPERTY_12_TIME_ID = 12;
		public const int PROPERTY_13_UPDATESPREADTICK_ID = 13;
		public const int PROPERTY_14_WINDOWSPLITPOINT_ID = 14;
		public const int PROPERTY_15_ISNAVIGATORVISIBLE_ID = 15;
		public const int PROPERTY_16_ISORDERSTABLEVISIBLE_ID = 16;
		public const int PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID = 17;
		public const int PROPERTY_18_ORDERFILTER_ID = 18;
		public static bool RmiGetProperty(IEnvironmentSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_SYMBOLSESSIONS_ID:
					value = controller.SymbolSessions;
					return true;
				case PROPERTY_2_CHARTSESSIONS_ID:
					value = controller.ChartSessions;
					return true;
				case PROPERTY_3_ENVIRONMENTID_ID:
					value = controller.EnvironmentId;
					return true;
				case PROPERTY_4_SCROLLACROSSCHARTS_ID:
					value = controller.ScrollAcrossCharts;
					return true;
				case PROPERTY_5_SKIPEMPTYPERIODS_ID:
					value = controller.SkipEmptyPeriods;
					return true;
				case PROPERTY_6_EATESTINGSPEED_ID:
					value = controller.EATestingSpeed;
					return true;
				case PROPERTY_7_EASTARTSTATUS_ID:
					value = controller.EAStartStatus;
					return true;
				case PROPERTY_8_EATESTINGGLOBALFROM_ID:
					value = controller.EATestingGlobalFrom;
					return true;
				case PROPERTY_9_EATESTINGGLOBALTO_ID:
					value = controller.EATestingGlobalTo;
					return true;
				case PROPERTY_10_EATESTINGTRACKBARTICK_ID:
					value = controller.EATestingTrackBarTick;
					return true;
				case PROPERTY_11_EATESTINGSNAPMODE_ID:
					value = controller.EATestingSnapMode;
					return true;
				case PROPERTY_12_TIME_ID:
					value = controller.Time;
					return true;
				case PROPERTY_13_UPDATESPREADTICK_ID:
					value = controller.UpdateSpreadTick;
					return true;
				case PROPERTY_14_WINDOWSPLITPOINT_ID:
					value = controller.WindowSplitPoint;
					return true;
				case PROPERTY_15_ISNAVIGATORVISIBLE_ID:
					value = controller.IsNavigatorVisible;
					return true;
				case PROPERTY_16_ISORDERSTABLEVISIBLE_ID:
					value = controller.IsOrdersTableVisible;
					return true;
				case PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID:
					value = controller.IsOrdersOverviewVisible;
					return true;
				case PROPERTY_18_ORDERFILTER_ID:
					value = controller.OrderFilter;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_SYMBOLSESSIONS_ID:
					controller.SymbolSessions = (Dictionary<symbol,ISymbolSession>) value;
					return true;
				case PROPERTY_2_CHARTSESSIONS_ID:
					controller.ChartSessions = (List<IChartGroupSession>) value;
					return true;
				case PROPERTY_3_ENVIRONMENTID_ID:
					controller.EnvironmentId = (String) value;
					return true;
				case PROPERTY_4_SCROLLACROSSCHARTS_ID:
					controller.ScrollAcrossCharts = (Boolean) value;
					return true;
				case PROPERTY_5_SKIPEMPTYPERIODS_ID:
					controller.SkipEmptyPeriods = (Boolean) value;
					return true;
				case PROPERTY_6_EATESTINGSPEED_ID:
					controller.EATestingSpeed = (Int32) value;
					return true;
				case PROPERTY_7_EASTARTSTATUS_ID:
					controller.EAStartStatus = (StartStatus) value;
					return true;
				case PROPERTY_8_EATESTINGGLOBALFROM_ID:
					controller.EATestingGlobalFrom = (datetime) value;
					return true;
				case PROPERTY_9_EATESTINGGLOBALTO_ID:
					controller.EATestingGlobalTo = (datetime) value;
					return true;
				case PROPERTY_10_EATESTINGTRACKBARTICK_ID:
					controller.EATestingTrackBarTick = (Int32) value;
					return true;
				case PROPERTY_11_EATESTINGSNAPMODE_ID:
					controller.EATestingSnapMode = (PorgressSnapMode) value;
					return true;
				case PROPERTY_12_TIME_ID:
					controller.Time = (datetime) value;
					return true;
				case PROPERTY_13_UPDATESPREADTICK_ID:
					controller.UpdateSpreadTick = (Boolean) value;
					return true;
				case PROPERTY_14_WINDOWSPLITPOINT_ID:
					controller.WindowSplitPoint = (Point) value;
					return true;
				case PROPERTY_15_ISNAVIGATORVISIBLE_ID:
					controller.IsNavigatorVisible = (Boolean) value;
					return true;
				case PROPERTY_16_ISORDERSTABLEVISIBLE_ID:
					controller.IsOrdersTableVisible = (Boolean) value;
					return true;
				case PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID:
					controller.IsOrdersOverviewVisible = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentSession controller, GreenRmiObjectBuffer buffer)
		{
			controller.OrderFilter = (IOrderFilter) buffer.ChangedProps[EnvironmentSessionProps.PROPERTY_18_ORDERFILTER_ID];
		}

		public static void AddDependencies(IEnvironmentSession controller)
		{
			controller.Dependencies.Add(controller.OrderFilter);
		}

		public static void SerializationRead(IEnvironmentSession controller, SerializationInfo info, StreamingContext context)
		{
			controller.SymbolSessions = (Dictionary<symbol,ISymbolSession>) info.GetValue("SymbolSessions", typeof(Dictionary<symbol,ISymbolSession>));
			controller.ChartSessions = (List<IChartGroupSession>) info.GetValue("ChartSessions", typeof(List<IChartGroupSession>));
			controller.EnvironmentId = (String) info.GetValue("EnvironmentId", typeof(String));
			controller.ScrollAcrossCharts = (Boolean) info.GetValue("ScrollAcrossCharts", typeof(Boolean));
			controller.SkipEmptyPeriods = (Boolean) info.GetValue("SkipEmptyPeriods", typeof(Boolean));
			controller.EATestingSpeed = (Int32) info.GetValue("EATestingSpeed", typeof(Int32));
			controller.EAStartStatus = (StartStatus) info.GetValue("EAStartStatus", typeof(StartStatus));
			controller.EATestingGlobalFrom = (datetime) info.GetValue("EATestingGlobalFrom", typeof(datetime));
			controller.EATestingGlobalTo = (datetime) info.GetValue("EATestingGlobalTo", typeof(datetime));
			controller.EATestingTrackBarTick = (Int32) info.GetValue("EATestingTrackBarTick", typeof(Int32));
			controller.EATestingSnapMode = (PorgressSnapMode) info.GetValue("EATestingSnapMode", typeof(PorgressSnapMode));
			controller.Time = (datetime) info.GetValue("Time", typeof(datetime));
			controller.UpdateSpreadTick = (Boolean) info.GetValue("UpdateSpreadTick", typeof(Boolean));
			controller.WindowSplitPoint = (Point) info.GetValue("WindowSplitPoint", typeof(Point));
			controller.IsNavigatorVisible = (Boolean) info.GetValue("IsNavigatorVisible", typeof(Boolean));
			controller.IsOrdersTableVisible = (Boolean) info.GetValue("IsOrdersTableVisible", typeof(Boolean));
			controller.IsOrdersOverviewVisible = (Boolean) info.GetValue("IsOrdersOverviewVisible", typeof(Boolean));
			controller.OrderFilter = (IOrderFilter) info.GetValue("OrderFilter", typeof(IOrderFilter));
		}

		public static void SerializationWrite(IEnvironmentSession controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SymbolSessions", controller.SymbolSessions);
			info.AddValue("ChartSessions", controller.ChartSessions);
			info.AddValue("EnvironmentId", controller.EnvironmentId);
			info.AddValue("ScrollAcrossCharts", controller.ScrollAcrossCharts);
			info.AddValue("SkipEmptyPeriods", controller.SkipEmptyPeriods);
			info.AddValue("EATestingSpeed", controller.EATestingSpeed);
			info.AddValue("EAStartStatus", controller.EAStartStatus);
			info.AddValue("EATestingGlobalFrom", controller.EATestingGlobalFrom);
			info.AddValue("EATestingGlobalTo", controller.EATestingGlobalTo);
			info.AddValue("EATestingTrackBarTick", controller.EATestingTrackBarTick);
			info.AddValue("EATestingSnapMode", controller.EATestingSnapMode);
			info.AddValue("Time", controller.Time);
			info.AddValue("UpdateSpreadTick", controller.UpdateSpreadTick);
			info.AddValue("WindowSplitPoint", controller.WindowSplitPoint);
			info.AddValue("IsNavigatorVisible", controller.IsNavigatorVisible);
			info.AddValue("IsOrdersTableVisible", controller.IsOrdersTableVisible);
			info.AddValue("IsOrdersOverviewVisible", controller.IsOrdersOverviewVisible);
			info.AddValue("OrderFilter", controller.OrderFilter);
		}

	}
	public abstract class EnvironmentSessionBase : RmiBase, IEnvironmentSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SymbolSessionsChanged;
		public event PropertyChangedEventHandler ChartSessionsChanged;
		public event PropertyChangedEventHandler EnvironmentIdChanged;
		public event PropertyChangedEventHandler ScrollAcrossChartsChanged;
		public event PropertyChangedEventHandler SkipEmptyPeriodsChanged;
		public event PropertyChangedEventHandler EATestingSpeedChanged;
		public event PropertyChangedEventHandler EAStartStatusChanged;
		public event PropertyChangedEventHandler EATestingGlobalFromChanged;
		public event PropertyChangedEventHandler EATestingGlobalToChanged;
		public event PropertyChangedEventHandler EATestingTrackBarTickChanged;
		public event PropertyChangedEventHandler EATestingSnapModeChanged;
		public event PropertyChangedEventHandler TimeChanged;
		public event PropertyChangedEventHandler UpdateSpreadTickChanged;
		public event PropertyChangedEventHandler WindowSplitPointChanged;
		public event PropertyChangedEventHandler IsNavigatorVisibleChanged;
		public event PropertyChangedEventHandler IsOrdersTableVisibleChanged;
		public event PropertyChangedEventHandler IsOrdersOverviewVisibleChanged;

		public EnvironmentSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EnvironmentSessionProps.AddDependencies(this);
		}

		public EnvironmentSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentSessionProps.Initialize(this, buffer);
			___initialized = true;
			EnvironmentSessionProps.AddDependencies(this);
		}

		protected EnvironmentSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			EnvironmentSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentSessionProps.SerializationWrite(this, info, context);
		}

		Dictionary<symbol,ISymbolSession> symbolSessions;
		public Dictionary<symbol,ISymbolSession> SymbolSessions
		{
			get {
				return symbolSessions;
			}
			set {
				if (symbolSessions != value) {
					symbolSessions= value;
					changed[EnvironmentSessionProps.PROPERTY_1_SYMBOLSESSIONS_ID] = true;
					if (SymbolSessionsChanged != null)
						SymbolSessionsChanged(this, new PropertyChangedEventArgs("SymbolSessions", value));
				}
			}
		}

		List<IChartGroupSession> chartSessions;
		public List<IChartGroupSession> ChartSessions
		{
			get {
				return chartSessions;
			}
			set {
				if (chartSessions != value) {
					chartSessions= value;
					changed[EnvironmentSessionProps.PROPERTY_2_CHARTSESSIONS_ID] = true;
					if (ChartSessionsChanged != null)
						ChartSessionsChanged(this, new PropertyChangedEventArgs("ChartSessions", value));
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
				if (environmentId != value) {
					environmentId= value;
					changed[EnvironmentSessionProps.PROPERTY_3_ENVIRONMENTID_ID] = true;
					if (EnvironmentIdChanged != null)
						EnvironmentIdChanged(this, new PropertyChangedEventArgs("EnvironmentId", value));
				}
			}
		}

		Boolean scrollAcrossCharts;
		public Boolean ScrollAcrossCharts
		{
			get {
				return scrollAcrossCharts;
			}
			set {
				if (scrollAcrossCharts != value) {
					scrollAcrossCharts= value;
					changed[EnvironmentSessionProps.PROPERTY_4_SCROLLACROSSCHARTS_ID] = true;
					if (ScrollAcrossChartsChanged != null)
						ScrollAcrossChartsChanged(this, new PropertyChangedEventArgs("ScrollAcrossCharts", value));
				}
			}
		}

		Boolean skipEmptyPeriods;
		public Boolean SkipEmptyPeriods
		{
			get {
				return skipEmptyPeriods;
			}
			set {
				if (skipEmptyPeriods != value) {
					skipEmptyPeriods= value;
					changed[EnvironmentSessionProps.PROPERTY_5_SKIPEMPTYPERIODS_ID] = true;
					if (SkipEmptyPeriodsChanged != null)
						SkipEmptyPeriodsChanged(this, new PropertyChangedEventArgs("SkipEmptyPeriods", value));
				}
			}
		}

		volatile Int32 eATestingSpeed;
		public Int32 EATestingSpeed
		{
			get {
				return eATestingSpeed;
			}
			set {
				if (eATestingSpeed != value) {
					eATestingSpeed= value;
					changed[EnvironmentSessionProps.PROPERTY_6_EATESTINGSPEED_ID] = true;
					if (EATestingSpeedChanged != null)
						EATestingSpeedChanged(this, new PropertyChangedEventArgs("EATestingSpeed", value));
				}
			}
		}

		StartStatus eAStartStatus;
		public StartStatus EAStartStatus
		{
			get {
				return eAStartStatus;
			}
			set {
				if (eAStartStatus != value) {
					eAStartStatus= value;
					changed[EnvironmentSessionProps.PROPERTY_7_EASTARTSTATUS_ID] = true;
					if (EAStartStatusChanged != null)
						EAStartStatusChanged(this, new PropertyChangedEventArgs("EAStartStatus", value));
				}
			}
		}

		datetime eATestingGlobalFrom;
		public datetime EATestingGlobalFrom
		{
			get {
				return eATestingGlobalFrom;
			}
			set {
				if (eATestingGlobalFrom != value) {
					eATestingGlobalFrom= value;
					changed[EnvironmentSessionProps.PROPERTY_8_EATESTINGGLOBALFROM_ID] = true;
					if (EATestingGlobalFromChanged != null)
						EATestingGlobalFromChanged(this, new PropertyChangedEventArgs("EATestingGlobalFrom", value));
				}
			}
		}

		datetime eATestingGlobalTo;
		public datetime EATestingGlobalTo
		{
			get {
				return eATestingGlobalTo;
			}
			set {
				if (eATestingGlobalTo != value) {
					eATestingGlobalTo= value;
					changed[EnvironmentSessionProps.PROPERTY_9_EATESTINGGLOBALTO_ID] = true;
					if (EATestingGlobalToChanged != null)
						EATestingGlobalToChanged(this, new PropertyChangedEventArgs("EATestingGlobalTo", value));
				}
			}
		}

		public abstract Int32 EATestingProgress
		{
			get ;
		}

		volatile Int32 eATestingTrackBarTick;
		public Int32 EATestingTrackBarTick
		{
			get {
				return eATestingTrackBarTick;
			}
			set {
				if (eATestingTrackBarTick != value) {
					eATestingTrackBarTick= value;
					changed[EnvironmentSessionProps.PROPERTY_10_EATESTINGTRACKBARTICK_ID] = true;
					if (EATestingTrackBarTickChanged != null)
						EATestingTrackBarTickChanged(this, new PropertyChangedEventArgs("EATestingTrackBarTick", value));
				}
			}
		}

		PorgressSnapMode eATestingSnapMode;
		public PorgressSnapMode EATestingSnapMode
		{
			get {
				return eATestingSnapMode;
			}
			set {
				if (eATestingSnapMode != value) {
					eATestingSnapMode= value;
					changed[EnvironmentSessionProps.PROPERTY_11_EATESTINGSNAPMODE_ID] = true;
					if (EATestingSnapModeChanged != null)
						EATestingSnapModeChanged(this, new PropertyChangedEventArgs("EATestingSnapMode", value));
				}
			}
		}

		datetime time;
		public datetime Time
		{
			get {
				return time;
			}
			set {
				if (time != value) {
					time= value;
					changed[EnvironmentSessionProps.PROPERTY_12_TIME_ID] = true;
					if (TimeChanged != null)
						TimeChanged(this, new PropertyChangedEventArgs("Time", value));
				}
			}
		}

		volatile Boolean updateSpreadTick;
		public Boolean UpdateSpreadTick
		{
			get {
				return updateSpreadTick;
			}
			set {
				if (updateSpreadTick != value) {
					updateSpreadTick= value;
					changed[EnvironmentSessionProps.PROPERTY_13_UPDATESPREADTICK_ID] = true;
					if (UpdateSpreadTickChanged != null)
						UpdateSpreadTickChanged(this, new PropertyChangedEventArgs("UpdateSpreadTick", value));
				}
			}
		}

		Point windowSplitPoint;
		public Point WindowSplitPoint
		{
			get {
				return windowSplitPoint;
			}
			set {
				if (windowSplitPoint != value) {
					windowSplitPoint= value;
					changed[EnvironmentSessionProps.PROPERTY_14_WINDOWSPLITPOINT_ID] = true;
					if (WindowSplitPointChanged != null)
						WindowSplitPointChanged(this, new PropertyChangedEventArgs("WindowSplitPoint", value));
				}
			}
		}

		Boolean isNavigatorVisible;
		public Boolean IsNavigatorVisible
		{
			get {
				return isNavigatorVisible;
			}
			set {
				if (isNavigatorVisible != value) {
					isNavigatorVisible= value;
					changed[EnvironmentSessionProps.PROPERTY_15_ISNAVIGATORVISIBLE_ID] = true;
					if (IsNavigatorVisibleChanged != null)
						IsNavigatorVisibleChanged(this, new PropertyChangedEventArgs("IsNavigatorVisible", value));
				}
			}
		}

		Boolean isOrdersTableVisible;
		public Boolean IsOrdersTableVisible
		{
			get {
				return isOrdersTableVisible;
			}
			set {
				if (isOrdersTableVisible != value) {
					isOrdersTableVisible= value;
					changed[EnvironmentSessionProps.PROPERTY_16_ISORDERSTABLEVISIBLE_ID] = true;
					if (IsOrdersTableVisibleChanged != null)
						IsOrdersTableVisibleChanged(this, new PropertyChangedEventArgs("IsOrdersTableVisible", value));
				}
			}
		}

		Boolean isOrdersOverviewVisible;
		public Boolean IsOrdersOverviewVisible
		{
			get {
				return isOrdersOverviewVisible;
			}
			set {
				if (isOrdersOverviewVisible != value) {
					isOrdersOverviewVisible= value;
					changed[EnvironmentSessionProps.PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID] = true;
					if (IsOrdersOverviewVisibleChanged != null)
						IsOrdersOverviewVisibleChanged(this, new PropertyChangedEventArgs("IsOrdersOverviewVisible", value));
				}
			}
		}

		IOrderFilter orderFilter;
		public IOrderFilter OrderFilter
		{
			get {
				return orderFilter;
			}
			set {
				if (!___initialized) {
					orderFilter= value;
					changed[EnvironmentSessionProps.PROPERTY_18_ORDERFILTER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EnvironmentSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!EnvironmentSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
