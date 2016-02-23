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
		public static bool RmiGetProperty(ISeriesManagerRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_3_SERIESCACHES_ID:
					value = controller.SeriesCaches;
					return true;
				case PROPERTY_4_DEFAULTCACHE_ID:
					value = controller.DefaultCache;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISeriesManagerRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_3_SERIESCACHES_ID:
					controller.SeriesCaches = (Dictionary<SymbolPeriodId,INormalSeriesManagerCache>) value;
					return true;
				case PROPERTY_4_DEFAULTCACHE_ID:
					controller.DefaultCache = (INormalSeriesManagerCache) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISeriesManagerRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[SeriesManagerRuntimeProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Parent = (IChartRuntime) buffer.ChangedProps[SeriesManagerRuntimeProps.PROPERTY_2_PARENT_ID];
		}

		public static void AddDependencies(ISeriesManagerRuntime controller)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Parent);
		}

		public static void SerializationRead(ISeriesManagerRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Parent = (IChartRuntime) info.GetValue("Parent", typeof(IChartRuntime));
			controller.SeriesCaches = (Dictionary<SymbolPeriodId,INormalSeriesManagerCache>) info.GetValue("SeriesCaches", typeof(Dictionary<SymbolPeriodId,INormalSeriesManagerCache>));
			controller.DefaultCache = (INormalSeriesManagerCache) info.GetValue("DefaultCache", typeof(INormalSeriesManagerCache));
		}

		public static void SerializationWrite(ISeriesManagerRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SeriesCaches", controller.SeriesCaches);
			info.AddValue("DefaultCache", controller.DefaultCache);
		}

	}
	public abstract class SeriesManagerRuntimeBase : RmiBase, ISeriesManagerRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SeriesCachesChanged;
		public event PropertyChangedEventHandler DefaultCacheChanged;

		public SeriesManagerRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SeriesManagerRuntimeProps.AddDependencies(this);
		}

		public SeriesManagerRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SeriesManagerRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			SeriesManagerRuntimeProps.AddDependencies(this);
		}

		protected SeriesManagerRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SeriesManagerRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			SeriesManagerRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SeriesManagerRuntimeProps.SerializationWrite(this, info, context);
		}

		public abstract void UpdateCursorDefault();

		IEnvironmentRuntime environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return environment;
			}
			set {
				if (!___initialized) {
					environment= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartRuntime parent;
		public IChartRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<SymbolPeriodId,INormalSeriesManagerCache> seriesCaches;
		public Dictionary<SymbolPeriodId,INormalSeriesManagerCache> SeriesCaches
		{
			get {
				return seriesCaches;
			}
			set {
				if (seriesCaches != value) {
					seriesCaches= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_3_SERIESCACHES_ID] = true;
					if (SeriesCachesChanged != null)
						SeriesCachesChanged(this, new PropertyChangedEventArgs("SeriesCaches", value));
				}
			}
		}

		INormalSeriesManagerCache defaultCache;
		public INormalSeriesManagerCache DefaultCache
		{
			get {
				return defaultCache;
			}
			set {
				if (defaultCache != value) {
					defaultCache= value;
					changed[SeriesManagerRuntimeProps.PROPERTY_4_DEFAULTCACHE_ID] = true;
					if (DefaultCacheChanged != null)
						DefaultCacheChanged(this, new PropertyChangedEventArgs("DefaultCache", value));
				}
			}
		}

		public abstract datetime FocusedTime
		{
			get ;
			set ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SeriesManagerRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!SeriesManagerRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
