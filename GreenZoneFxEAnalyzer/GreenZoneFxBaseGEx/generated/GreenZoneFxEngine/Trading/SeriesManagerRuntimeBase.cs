using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class SeriesManagerRuntimeProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_PARENT_ID = 2;
		public const int PROPERTY_3_SERIESCACHES_ID = 3;
		public const int PROPERTY_4_DEFAULTCACHE_ID = 4;
		public static bool RmiGetProperty(ISeriesManagerRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SeriesManagerRuntimeProps.PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case SeriesManagerRuntimeProps.PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case SeriesManagerRuntimeProps.PROPERTY_3_SERIESCACHES_ID:
					value = controller.SeriesCaches;
					return true;
				case SeriesManagerRuntimeProps.PROPERTY_4_DEFAULTCACHE_ID:
					value = controller.DefaultCache;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISeriesManagerRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SeriesManagerRuntimeProps.PROPERTY_3_SERIESCACHES_ID:
					controller.SeriesCaches = (Dictionary<SymbolPeriodId,INormalSeriesManagerCache>) value;
					return true;
				case SeriesManagerRuntimeProps.PROPERTY_4_DEFAULTCACHE_ID:
					controller.DefaultCache = (INormalSeriesManagerCache) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISeriesManagerRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[SeriesManagerRuntimeProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Parent = (IChartRuntime) buffer.ChangedProps[SeriesManagerRuntimeProps.PROPERTY_2_PARENT_ID];
		}

		public static void AddDependencies(ISeriesManagerRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Parent);
		}

		public static void SerializationRead(ISeriesManagerRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Parent = (IChartRuntime) info.GetValue("Parent", typeof(IChartRuntime));
			controller.SeriesCaches = (Dictionary<SymbolPeriodId,INormalSeriesManagerCache>) info.GetValue("SeriesCaches", typeof(Dictionary<SymbolPeriodId,INormalSeriesManagerCache>));
			controller.DefaultCache = (INormalSeriesManagerCache) info.GetValue("DefaultCache", typeof(INormalSeriesManagerCache));
		}

		public static void SerializationWrite(ISeriesManagerRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SeriesCaches", controller.SeriesCaches);
			info.AddValue("DefaultCache", controller.DefaultCache);
		}

	}
	public abstract class SeriesManagerRuntimeBase : TradingConst, ISeriesManagerRuntime
	{

		bool ___initialized = false;

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return SeriesCaches.Values.GetEnumerator(); }

		public event PropertyChangedEventHandler ISeriesManagerRuntime_SeriesCaches_Changed;
		public event PropertyChangedEventHandler ISeriesManagerRuntime_DefaultCache_Changed;

		public SeriesManagerRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SeriesManagerRuntimeProps.AddDependencies(this, false);
		}

		public SeriesManagerRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SeriesManagerRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			SeriesManagerRuntimeProps.AddDependencies(this, false);
		}

		protected SeriesManagerRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SeriesManagerRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			SeriesManagerRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SeriesManagerRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract INormalSeriesManagerCache CreateSeriesManagerCache(SymbolPeriodId sp);

		public abstract void UpdateCursorDefault();

		public IEnumerator<INormalSeriesManagerCache> GetEnumerator()
		{
			return SeriesCaches.Values.GetEnumerator();
		}


		IEnvironmentRuntime _ISeriesManagerRuntime_Environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return _ISeriesManagerRuntime_Environment;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerRuntime_Environment= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartRuntime _ISeriesManagerRuntime_Parent;
		public IChartRuntime Parent
		{
			get {
				return _ISeriesManagerRuntime_Parent;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerRuntime_Parent= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<SymbolPeriodId,INormalSeriesManagerCache> _ISeriesManagerRuntime_SeriesCaches;
		public Dictionary<SymbolPeriodId,INormalSeriesManagerCache> SeriesCaches
		{
			get {
				return _ISeriesManagerRuntime_SeriesCaches;
			}
			set {
				if (_ISeriesManagerRuntime_SeriesCaches != value) {
					_ISeriesManagerRuntime_SeriesCaches= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_3_SERIESCACHES_ID] = true;
					if (ISeriesManagerRuntime_SeriesCaches_Changed != null)
						ISeriesManagerRuntime_SeriesCaches_Changed(this, new PropertyChangedEventArgs("SeriesCaches", value));
				}
			}
		}

		INormalSeriesManagerCache _ISeriesManagerRuntime_DefaultCache;
		public INormalSeriesManagerCache DefaultCache
		{
			get {
				return _ISeriesManagerRuntime_DefaultCache;
			}
			set {
				if (_ISeriesManagerRuntime_DefaultCache != value) {
					_ISeriesManagerRuntime_DefaultCache= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_4_DEFAULTCACHE_ID] = true;
					if (ISeriesManagerRuntime_DefaultCache_Changed != null)
						ISeriesManagerRuntime_DefaultCache_Changed(this, new PropertyChangedEventArgs("DefaultCache", value));
				}
			}
		}

		public abstract datetime FocusedTime
		{
			get ;
			set ;
		}

		public abstract INormalSeriesManagerCache  this[symbol sym, TimePeriodConst period]
		{
			get;
		}

		public abstract IIndicatorRuntime  this[symbol sym, TimePeriodConst period, Mt4ExecutableInfo ind, params Object[] args]
		{
			get;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SeriesManagerRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (SeriesManagerRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
