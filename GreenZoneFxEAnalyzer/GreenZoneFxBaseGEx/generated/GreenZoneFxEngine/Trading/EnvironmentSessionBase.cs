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
		public static bool RmiGetProperty(IEnvironmentSession controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentSessionProps.PROPERTY_1_SYMBOLSESSIONS_ID:
					value = controller.SymbolSessions;
					return true;
				case EnvironmentSessionProps.PROPERTY_2_CHARTSESSIONS_ID:
					value = controller.ChartSessions;
					return true;
				case EnvironmentSessionProps.PROPERTY_3_ENVIRONMENTID_ID:
					value = controller.EnvironmentId;
					return true;
				case EnvironmentSessionProps.PROPERTY_4_SCROLLACROSSCHARTS_ID:
					value = controller.ScrollAcrossCharts;
					return true;
				case EnvironmentSessionProps.PROPERTY_5_SKIPEMPTYPERIODS_ID:
					value = controller.SkipEmptyPeriods;
					return true;
				case EnvironmentSessionProps.PROPERTY_6_EATESTINGSPEED_ID:
					value = controller.EATestingSpeed;
					return true;
				case EnvironmentSessionProps.PROPERTY_7_EASTARTSTATUS_ID:
					value = controller.EAStartStatus;
					return true;
				case EnvironmentSessionProps.PROPERTY_8_EATESTINGGLOBALFROM_ID:
					value = controller.EATestingGlobalFrom;
					return true;
				case EnvironmentSessionProps.PROPERTY_9_EATESTINGGLOBALTO_ID:
					value = controller.EATestingGlobalTo;
					return true;
				case EnvironmentSessionProps.PROPERTY_10_EATESTINGTRACKBARTICK_ID:
					value = controller.EATestingTrackBarTick;
					return true;
				case EnvironmentSessionProps.PROPERTY_11_EATESTINGSNAPMODE_ID:
					value = controller.EATestingSnapMode;
					return true;
				case EnvironmentSessionProps.PROPERTY_12_TIME_ID:
					value = controller.Time;
					return true;
				case EnvironmentSessionProps.PROPERTY_13_UPDATESPREADTICK_ID:
					value = controller.UpdateSpreadTick;
					return true;
				case EnvironmentSessionProps.PROPERTY_14_WINDOWSPLITPOINT_ID:
					value = controller.WindowSplitPoint;
					return true;
				case EnvironmentSessionProps.PROPERTY_15_ISNAVIGATORVISIBLE_ID:
					value = controller.IsNavigatorVisible;
					return true;
				case EnvironmentSessionProps.PROPERTY_16_ISORDERSTABLEVISIBLE_ID:
					value = controller.IsOrdersTableVisible;
					return true;
				case EnvironmentSessionProps.PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID:
					value = controller.IsOrdersOverviewVisible;
					return true;
				case EnvironmentSessionProps.PROPERTY_18_ORDERFILTER_ID:
					value = controller.OrderFilter;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentSession controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentSessionProps.PROPERTY_1_SYMBOLSESSIONS_ID:
					controller.SymbolSessions = (Dictionary<symbol,ISymbolSession>) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_2_CHARTSESSIONS_ID:
					controller.ChartSessions = (List<IChartGroupSession>) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_3_ENVIRONMENTID_ID:
					controller.EnvironmentId = (String) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_4_SCROLLACROSSCHARTS_ID:
					controller.ScrollAcrossCharts = (Boolean) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_5_SKIPEMPTYPERIODS_ID:
					controller.SkipEmptyPeriods = (Boolean) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_6_EATESTINGSPEED_ID:
					controller.EATestingSpeed = (Int32) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_7_EASTARTSTATUS_ID:
					controller.EAStartStatus = (StartStatus) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_8_EATESTINGGLOBALFROM_ID:
					controller.EATestingGlobalFrom = (datetime) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_9_EATESTINGGLOBALTO_ID:
					controller.EATestingGlobalTo = (datetime) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_10_EATESTINGTRACKBARTICK_ID:
					controller.EATestingTrackBarTick = (Int32) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_11_EATESTINGSNAPMODE_ID:
					controller.EATestingSnapMode = (PorgressSnapMode) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_12_TIME_ID:
					controller.Time = (datetime) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_13_UPDATESPREADTICK_ID:
					controller.UpdateSpreadTick = (Boolean) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_14_WINDOWSPLITPOINT_ID:
					controller.WindowSplitPoint = (Point) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_15_ISNAVIGATORVISIBLE_ID:
					controller.IsNavigatorVisible = (Boolean) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_16_ISORDERSTABLEVISIBLE_ID:
					controller.IsOrdersTableVisible = (Boolean) value;
					return true;
				case EnvironmentSessionProps.PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID:
					controller.IsOrdersOverviewVisible = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.OrderFilter = (IOrderFilter) buffer.ChangedProps[EnvironmentSessionProps.PROPERTY_18_ORDERFILTER_ID];
		}

		public static void AddDependencies(IEnvironmentSession controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.OrderFilter);
		}

		public static void SerializationRead(IEnvironmentSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IEnvironmentSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IEnvironmentSession_SymbolSessions_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_ChartSessions_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EnvironmentId_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_ScrollAcrossCharts_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_SkipEmptyPeriods_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EATestingSpeed_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EAStartStatus_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EATestingGlobalFrom_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EATestingGlobalTo_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EATestingTrackBarTick_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_EATestingSnapMode_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_Time_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_UpdateSpreadTick_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_WindowSplitPoint_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_IsNavigatorVisible_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_IsOrdersTableVisible_Changed;
		public event PropertyChangedEventHandler IEnvironmentSession_IsOrdersOverviewVisible_Changed;

		public EnvironmentSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EnvironmentSessionProps.AddDependencies(this, false);
		}

		public EnvironmentSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			EnvironmentSessionProps.AddDependencies(this, false);
		}

		protected EnvironmentSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EnvironmentSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentSessionProps.SerializationWrite(this, info, context, false);
		}


		Dictionary<symbol,ISymbolSession> _IEnvironmentSession_SymbolSessions;
		public Dictionary<symbol,ISymbolSession> SymbolSessions
		{
			get {
				return _IEnvironmentSession_SymbolSessions;
			}
			set {
				if (_IEnvironmentSession_SymbolSessions != value) {
					_IEnvironmentSession_SymbolSessions= value;
					changed[EnvironmentSessionProps.PROPERTY_1_SYMBOLSESSIONS_ID] = true;
					if (IEnvironmentSession_SymbolSessions_Changed != null)
						IEnvironmentSession_SymbolSessions_Changed(this, new PropertyChangedEventArgs("SymbolSessions", value));
				}
			}
		}

		List<IChartGroupSession> _IEnvironmentSession_ChartSessions;
		public List<IChartGroupSession> ChartSessions
		{
			get {
				return _IEnvironmentSession_ChartSessions;
			}
			set {
				if (_IEnvironmentSession_ChartSessions != value) {
					_IEnvironmentSession_ChartSessions= value;
					changed[EnvironmentSessionProps.PROPERTY_2_CHARTSESSIONS_ID] = true;
					if (IEnvironmentSession_ChartSessions_Changed != null)
						IEnvironmentSession_ChartSessions_Changed(this, new PropertyChangedEventArgs("ChartSessions", value));
				}
			}
		}

		String _IEnvironmentSession_EnvironmentId;
		public String EnvironmentId
		{
			get {
				return _IEnvironmentSession_EnvironmentId;
			}
			set {
				if (_IEnvironmentSession_EnvironmentId != value) {
					_IEnvironmentSession_EnvironmentId= value;
					changed[EnvironmentSessionProps.PROPERTY_3_ENVIRONMENTID_ID] = true;
					if (IEnvironmentSession_EnvironmentId_Changed != null)
						IEnvironmentSession_EnvironmentId_Changed(this, new PropertyChangedEventArgs("EnvironmentId", value));
				}
			}
		}

		Boolean _IEnvironmentSession_ScrollAcrossCharts;
		public Boolean ScrollAcrossCharts
		{
			get {
				return _IEnvironmentSession_ScrollAcrossCharts;
			}
			set {
				if (_IEnvironmentSession_ScrollAcrossCharts != value) {
					_IEnvironmentSession_ScrollAcrossCharts= value;
					changed[EnvironmentSessionProps.PROPERTY_4_SCROLLACROSSCHARTS_ID] = true;
					if (IEnvironmentSession_ScrollAcrossCharts_Changed != null)
						IEnvironmentSession_ScrollAcrossCharts_Changed(this, new PropertyChangedEventArgs("ScrollAcrossCharts", value));
				}
			}
		}

		Boolean _IEnvironmentSession_SkipEmptyPeriods;
		public Boolean SkipEmptyPeriods
		{
			get {
				return _IEnvironmentSession_SkipEmptyPeriods;
			}
			set {
				if (_IEnvironmentSession_SkipEmptyPeriods != value) {
					_IEnvironmentSession_SkipEmptyPeriods= value;
					changed[EnvironmentSessionProps.PROPERTY_5_SKIPEMPTYPERIODS_ID] = true;
					if (IEnvironmentSession_SkipEmptyPeriods_Changed != null)
						IEnvironmentSession_SkipEmptyPeriods_Changed(this, new PropertyChangedEventArgs("SkipEmptyPeriods", value));
				}
			}
		}

		volatile Int32 _IEnvironmentSession_EATestingSpeed;
		public Int32 EATestingSpeed
		{
			get {
				return _IEnvironmentSession_EATestingSpeed;
			}
			set {
				if (_IEnvironmentSession_EATestingSpeed != value) {
					_IEnvironmentSession_EATestingSpeed= value;
					changed[EnvironmentSessionProps.PROPERTY_6_EATESTINGSPEED_ID] = true;
					if (IEnvironmentSession_EATestingSpeed_Changed != null)
						IEnvironmentSession_EATestingSpeed_Changed(this, new PropertyChangedEventArgs("EATestingSpeed", value));
				}
			}
		}

		StartStatus _IEnvironmentSession_EAStartStatus;
		public StartStatus EAStartStatus
		{
			get {
				return _IEnvironmentSession_EAStartStatus;
			}
			set {
				if (_IEnvironmentSession_EAStartStatus != value) {
					_IEnvironmentSession_EAStartStatus= value;
					changed[EnvironmentSessionProps.PROPERTY_7_EASTARTSTATUS_ID] = true;
					if (IEnvironmentSession_EAStartStatus_Changed != null)
						IEnvironmentSession_EAStartStatus_Changed(this, new PropertyChangedEventArgs("EAStartStatus", value));
				}
			}
		}

		datetime _IEnvironmentSession_EATestingGlobalFrom;
		public datetime EATestingGlobalFrom
		{
			get {
				return _IEnvironmentSession_EATestingGlobalFrom;
			}
			set {
				if (_IEnvironmentSession_EATestingGlobalFrom != value) {
					_IEnvironmentSession_EATestingGlobalFrom= value;
					changed[EnvironmentSessionProps.PROPERTY_8_EATESTINGGLOBALFROM_ID] = true;
					if (IEnvironmentSession_EATestingGlobalFrom_Changed != null)
						IEnvironmentSession_EATestingGlobalFrom_Changed(this, new PropertyChangedEventArgs("EATestingGlobalFrom", value));
				}
			}
		}

		datetime _IEnvironmentSession_EATestingGlobalTo;
		public datetime EATestingGlobalTo
		{
			get {
				return _IEnvironmentSession_EATestingGlobalTo;
			}
			set {
				if (_IEnvironmentSession_EATestingGlobalTo != value) {
					_IEnvironmentSession_EATestingGlobalTo= value;
					changed[EnvironmentSessionProps.PROPERTY_9_EATESTINGGLOBALTO_ID] = true;
					if (IEnvironmentSession_EATestingGlobalTo_Changed != null)
						IEnvironmentSession_EATestingGlobalTo_Changed(this, new PropertyChangedEventArgs("EATestingGlobalTo", value));
				}
			}
		}

		public abstract Int32 EATestingProgress
		{
			get ;
		}

		volatile Int32 _IEnvironmentSession_EATestingTrackBarTick;
		public Int32 EATestingTrackBarTick
		{
			get {
				return _IEnvironmentSession_EATestingTrackBarTick;
			}
			set {
				if (_IEnvironmentSession_EATestingTrackBarTick != value) {
					_IEnvironmentSession_EATestingTrackBarTick= value;
					changed[EnvironmentSessionProps.PROPERTY_10_EATESTINGTRACKBARTICK_ID] = true;
					if (IEnvironmentSession_EATestingTrackBarTick_Changed != null)
						IEnvironmentSession_EATestingTrackBarTick_Changed(this, new PropertyChangedEventArgs("EATestingTrackBarTick", value));
				}
			}
		}

		PorgressSnapMode _IEnvironmentSession_EATestingSnapMode;
		public PorgressSnapMode EATestingSnapMode
		{
			get {
				return _IEnvironmentSession_EATestingSnapMode;
			}
			set {
				if (_IEnvironmentSession_EATestingSnapMode != value) {
					_IEnvironmentSession_EATestingSnapMode= value;
					changed[EnvironmentSessionProps.PROPERTY_11_EATESTINGSNAPMODE_ID] = true;
					if (IEnvironmentSession_EATestingSnapMode_Changed != null)
						IEnvironmentSession_EATestingSnapMode_Changed(this, new PropertyChangedEventArgs("EATestingSnapMode", value));
				}
			}
		}

		datetime _IEnvironmentSession_Time;
		public virtual datetime Time
		{
			get {
				return _IEnvironmentSession_Time;
			}
			set {
				if (_IEnvironmentSession_Time != value) {
					_IEnvironmentSession_Time= value;
					changed[EnvironmentSessionProps.PROPERTY_12_TIME_ID] = true;
					if (IEnvironmentSession_Time_Changed != null)
						IEnvironmentSession_Time_Changed(this, new PropertyChangedEventArgs("Time", value));
				}
			}
		}

		volatile Boolean _IEnvironmentSession_UpdateSpreadTick;
		public Boolean UpdateSpreadTick
		{
			get {
				return _IEnvironmentSession_UpdateSpreadTick;
			}
			set {
				if (_IEnvironmentSession_UpdateSpreadTick != value) {
					_IEnvironmentSession_UpdateSpreadTick= value;
					changed[EnvironmentSessionProps.PROPERTY_13_UPDATESPREADTICK_ID] = true;
					if (IEnvironmentSession_UpdateSpreadTick_Changed != null)
						IEnvironmentSession_UpdateSpreadTick_Changed(this, new PropertyChangedEventArgs("UpdateSpreadTick", value));
				}
			}
		}

		Point _IEnvironmentSession_WindowSplitPoint;
		public Point WindowSplitPoint
		{
			get {
				return _IEnvironmentSession_WindowSplitPoint;
			}
			set {
				if (_IEnvironmentSession_WindowSplitPoint != value) {
					_IEnvironmentSession_WindowSplitPoint= value;
					changed[EnvironmentSessionProps.PROPERTY_14_WINDOWSPLITPOINT_ID] = true;
					if (IEnvironmentSession_WindowSplitPoint_Changed != null)
						IEnvironmentSession_WindowSplitPoint_Changed(this, new PropertyChangedEventArgs("WindowSplitPoint", value));
				}
			}
		}

		Boolean _IEnvironmentSession_IsNavigatorVisible;
		public Boolean IsNavigatorVisible
		{
			get {
				return _IEnvironmentSession_IsNavigatorVisible;
			}
			set {
				if (_IEnvironmentSession_IsNavigatorVisible != value) {
					_IEnvironmentSession_IsNavigatorVisible= value;
					changed[EnvironmentSessionProps.PROPERTY_15_ISNAVIGATORVISIBLE_ID] = true;
					if (IEnvironmentSession_IsNavigatorVisible_Changed != null)
						IEnvironmentSession_IsNavigatorVisible_Changed(this, new PropertyChangedEventArgs("IsNavigatorVisible", value));
				}
			}
		}

		Boolean _IEnvironmentSession_IsOrdersTableVisible;
		public Boolean IsOrdersTableVisible
		{
			get {
				return _IEnvironmentSession_IsOrdersTableVisible;
			}
			set {
				if (_IEnvironmentSession_IsOrdersTableVisible != value) {
					_IEnvironmentSession_IsOrdersTableVisible= value;
					changed[EnvironmentSessionProps.PROPERTY_16_ISORDERSTABLEVISIBLE_ID] = true;
					if (IEnvironmentSession_IsOrdersTableVisible_Changed != null)
						IEnvironmentSession_IsOrdersTableVisible_Changed(this, new PropertyChangedEventArgs("IsOrdersTableVisible", value));
				}
			}
		}

		Boolean _IEnvironmentSession_IsOrdersOverviewVisible;
		public Boolean IsOrdersOverviewVisible
		{
			get {
				return _IEnvironmentSession_IsOrdersOverviewVisible;
			}
			set {
				if (_IEnvironmentSession_IsOrdersOverviewVisible != value) {
					_IEnvironmentSession_IsOrdersOverviewVisible= value;
					changed[EnvironmentSessionProps.PROPERTY_17_ISORDERSOVERVIEWVISIBLE_ID] = true;
					if (IEnvironmentSession_IsOrdersOverviewVisible_Changed != null)
						IEnvironmentSession_IsOrdersOverviewVisible_Changed(this, new PropertyChangedEventArgs("IsOrdersOverviewVisible", value));
				}
			}
		}

		IOrderFilter _IEnvironmentSession_OrderFilter;
		public IOrderFilter OrderFilter
		{
			get {
				return _IEnvironmentSession_OrderFilter;
			}
			set {
				if (!___initialized) {
					_IEnvironmentSession_OrderFilter= value;
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
			if (EnvironmentSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EnvironmentSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
