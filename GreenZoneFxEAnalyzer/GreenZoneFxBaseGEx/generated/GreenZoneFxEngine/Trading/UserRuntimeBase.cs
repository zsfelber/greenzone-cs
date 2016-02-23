using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class UserRuntimeProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_PARENT_ID = 2;
		public const int PROPERTY_3_SERIESMANAGER_ID = 3;
		public const int PROPERTY_4_CACHE_ID = 4;
		public const int PROPERTY_5_SESSION_ID = 5;
		public const int PROPERTY_6_TMPARRAYCACHES_ID = 6;
		public const int PROPERTY_7_INDICATORLASTOFFSET_ID = 7;
		public const int PROPERTY_8_LASTFILEOFFSET_ID = 8;
		public const int PROPERTY_9_LASTBUFFERLENGTH_ID = 9;
		public const int PROPERTY_10_UPDATESTATE_ID = 10;
		public static bool RmiGetProperty(IUserRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case UserRuntimeProps.PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case UserRuntimeProps.PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case UserRuntimeProps.PROPERTY_3_SERIESMANAGER_ID:
					value = controller.SeriesManager;
					return true;
				case UserRuntimeProps.PROPERTY_4_CACHE_ID:
					value = controller.Cache;
					return true;
				case UserRuntimeProps.PROPERTY_5_SESSION_ID:
					value = controller.Session;
					return true;
				case UserRuntimeProps.PROPERTY_6_TMPARRAYCACHES_ID:
					value = controller.TmpArrayCaches;
					return true;
				case UserRuntimeProps.PROPERTY_7_INDICATORLASTOFFSET_ID:
					value = controller.IndicatorLastOffset;
					return true;
				case UserRuntimeProps.PROPERTY_8_LASTFILEOFFSET_ID:
					value = controller.LastFileOffset;
					return true;
				case UserRuntimeProps.PROPERTY_9_LASTBUFFERLENGTH_ID:
					value = controller.LastBufferLength;
					return true;
				case UserRuntimeProps.PROPERTY_10_UPDATESTATE_ID:
					value = controller.UpdateState;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IUserRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case UserRuntimeProps.PROPERTY_7_INDICATORLASTOFFSET_ID:
					controller.IndicatorLastOffset = (Int32) value;
					return true;
				case UserRuntimeProps.PROPERTY_8_LASTFILEOFFSET_ID:
					controller.LastFileOffset = (Int64) value;
					return true;
				case UserRuntimeProps.PROPERTY_9_LASTBUFFERLENGTH_ID:
					controller.LastBufferLength = (Int32) value;
					return true;
				case UserRuntimeProps.PROPERTY_10_UPDATESTATE_ID:
					controller.UpdateState = (IndicatorUpdateState) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IUserRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[UserRuntimeProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Parent = (IChartRuntime) buffer.ChangedProps[UserRuntimeProps.PROPERTY_2_PARENT_ID];
			controller.SeriesManager = (ISeriesManagerRuntime) buffer.ChangedProps[UserRuntimeProps.PROPERTY_3_SERIESMANAGER_ID];
			controller.Cache = (ISeriesManagerCache) buffer.ChangedProps[UserRuntimeProps.PROPERTY_4_CACHE_ID];
			controller.Session = (IExecSession) buffer.ChangedProps[UserRuntimeProps.PROPERTY_5_SESSION_ID];
			controller.TmpArrayCaches = (Dictionary<DArr,IArraySeriesManagerCache>) buffer.ChangedProps[UserRuntimeProps.PROPERTY_6_TMPARRAYCACHES_ID];
		}

		public static void AddDependencies(IUserRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.SeriesManager);
			controller.Dependencies.Add(controller.Cache);
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.AddRange(controller.TmpArrayCaches.Values);
		}

		public static void SerializationRead(IUserRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Parent = (IChartRuntime) info.GetValue("Parent", typeof(IChartRuntime));
			controller.SeriesManager = (ISeriesManagerRuntime) info.GetValue("SeriesManager", typeof(ISeriesManagerRuntime));
			controller.Cache = (ISeriesManagerCache) info.GetValue("Cache", typeof(ISeriesManagerCache));
			controller.Session = (IExecSession) info.GetValue("Session", typeof(IExecSession));
			controller.TmpArrayCaches = (Dictionary<DArr,IArraySeriesManagerCache>) info.GetValue("TmpArrayCaches", typeof(Dictionary<DArr,IArraySeriesManagerCache>));
			controller.IndicatorLastOffset = (Int32) info.GetValue("IndicatorLastOffset", typeof(Int32));
			controller.LastFileOffset = (Int64) info.GetValue("LastFileOffset", typeof(Int64));
			controller.LastBufferLength = (Int32) info.GetValue("LastBufferLength", typeof(Int32));
			controller.UpdateState = (IndicatorUpdateState) info.GetValue("UpdateState", typeof(IndicatorUpdateState));
		}

		public static void SerializationWrite(IUserRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SeriesManager", controller.SeriesManager);
			info.AddValue("Cache", controller.Cache);
			info.AddValue("Session", controller.Session);
			info.AddValue("TmpArrayCaches", controller.TmpArrayCaches);
			info.AddValue("IndicatorLastOffset", controller.IndicatorLastOffset);
			info.AddValue("LastFileOffset", controller.LastFileOffset);
			info.AddValue("LastBufferLength", controller.LastBufferLength);
			info.AddValue("UpdateState", controller.UpdateState);
		}

	}
	public abstract class UserRuntimeBase : TradingConst, IUserRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IUserRuntime_IndicatorLastOffset_Changed;
		public event PropertyChangedEventHandler IUserRuntime_LastFileOffset_Changed;
		public event PropertyChangedEventHandler IUserRuntime_LastBufferLength_Changed;
		public event PropertyChangedEventHandler IUserRuntime_UpdateState_Changed;

		public UserRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			UserRuntimeProps.AddDependencies(this, false);
		}

		public UserRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			UserRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			UserRuntimeProps.AddDependencies(this, false);
		}

		protected UserRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			UserRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			UserRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			UserRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Object[] GenerateParamArray();

		public abstract void CopyParamsTo(IExecRuntime other);

		public abstract void CopyTopLevelParamsToSession();

		public abstract void LoadFromSet(String file);

		public abstract void SaveToSet(String file);

		public abstract IExecRuntime Copy();


		IEnvironmentRuntime _IUserRuntime_Environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return _IUserRuntime_Environment;
			}
			set {
				if (!___initialized) {
					_IUserRuntime_Environment= value;
					changed[UserRuntimeProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartRuntime _IUserRuntime_Parent;
		public IChartRuntime Parent
		{
			get {
				return _IUserRuntime_Parent;
			}
			set {
				if (!___initialized) {
					_IUserRuntime_Parent= value;
					changed[UserRuntimeProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISeriesManagerRuntime _IUserRuntime_SeriesManager;
		public ISeriesManagerRuntime SeriesManager
		{
			get {
				return _IUserRuntime_SeriesManager;
			}
			set {
				if (!___initialized) {
					_IUserRuntime_SeriesManager= value;
					changed[UserRuntimeProps.PROPERTY_3_SERIESMANAGER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISeriesManagerCache _IUserRuntime_Cache;
		public ISeriesManagerCache Cache
		{
			get {
				return _IUserRuntime_Cache;
			}
			set {
				if (!___initialized) {
					_IUserRuntime_Cache= value;
					changed[UserRuntimeProps.PROPERTY_4_CACHE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IExecSession _IUserRuntime_Session;
		public IExecSession Session
		{
			get {
				return _IUserRuntime_Session;
			}
			set {
				if (!___initialized) {
					_IUserRuntime_Session= value;
					changed[UserRuntimeProps.PROPERTY_5_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual Mt4ExecutableInfo ExecutableInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}

		Dictionary<DArr,IArraySeriesManagerCache> _IUserRuntime_TmpArrayCaches;
		public Dictionary<DArr,IArraySeriesManagerCache> TmpArrayCaches
		{
			get {
				return _IUserRuntime_TmpArrayCaches;
			}
			set {
				if (!___initialized) {
					_IUserRuntime_TmpArrayCaches= value;
					changed[UserRuntimeProps.PROPERTY_6_TMPARRAYCACHES_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Int32 _IUserRuntime_IndicatorLastOffset;
		public Int32 IndicatorLastOffset
		{
			get {
				return _IUserRuntime_IndicatorLastOffset;
			}
			set {
				if (_IUserRuntime_IndicatorLastOffset != value) {
					_IUserRuntime_IndicatorLastOffset= value;
					changed[UserRuntimeProps.PROPERTY_7_INDICATORLASTOFFSET_ID] = true;
					if (IUserRuntime_IndicatorLastOffset_Changed != null)
						IUserRuntime_IndicatorLastOffset_Changed(this, new PropertyChangedEventArgs("IndicatorLastOffset", value));
				}
			}
		}

		Int64 _IUserRuntime_LastFileOffset;
		public Int64 LastFileOffset
		{
			get {
				return _IUserRuntime_LastFileOffset;
			}
			set {
				if (_IUserRuntime_LastFileOffset != value) {
					_IUserRuntime_LastFileOffset= value;
					changed[UserRuntimeProps.PROPERTY_8_LASTFILEOFFSET_ID] = true;
					if (IUserRuntime_LastFileOffset_Changed != null)
						IUserRuntime_LastFileOffset_Changed(this, new PropertyChangedEventArgs("LastFileOffset", value));
				}
			}
		}

		Int32 _IUserRuntime_LastBufferLength;
		public Int32 LastBufferLength
		{
			get {
				return _IUserRuntime_LastBufferLength;
			}
			set {
				if (_IUserRuntime_LastBufferLength != value) {
					_IUserRuntime_LastBufferLength= value;
					changed[UserRuntimeProps.PROPERTY_9_LASTBUFFERLENGTH_ID] = true;
					if (IUserRuntime_LastBufferLength_Changed != null)
						IUserRuntime_LastBufferLength_Changed(this, new PropertyChangedEventArgs("LastBufferLength", value));
				}
			}
		}

		IndicatorUpdateState _IUserRuntime_UpdateState;
		public IndicatorUpdateState UpdateState
		{
			get {
				return _IUserRuntime_UpdateState;
			}
			set {
				if (_IUserRuntime_UpdateState != value) {
					_IUserRuntime_UpdateState= value;
					changed[UserRuntimeProps.PROPERTY_10_UPDATESTATE_ID] = true;
					if (IUserRuntime_UpdateState_Changed != null)
						IUserRuntime_UpdateState_Changed(this, new PropertyChangedEventArgs("UpdateState", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (UserRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (UserRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
