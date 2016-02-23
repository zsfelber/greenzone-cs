using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerSeriesManagerRuntimeProps
	{
		public static bool RmiGetProperty(IServerSeriesManagerRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (SeriesManagerRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerSeriesManagerRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (SeriesManagerRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerSeriesManagerRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerSeriesManagerRuntime controller, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerSeriesManagerRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerSeriesManagerRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerSeriesManagerRuntimeBase : SeriesManagerRuntimeEx, IServerSeriesManagerRuntime
	{

		bool ___initialized = false;


		public ServerSeriesManagerRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerSeriesManagerRuntimeProps.AddDependencies(this, false);
		}

		public ServerSeriesManagerRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerSeriesManagerRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerSeriesManagerRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerSeriesManagerRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerSeriesManagerRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerSeriesManagerRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerSeriesManagerRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void UpdateDefault();

		public abstract void AddExpert(IServerChartRuntime c);

		public abstract void UpdateDefault(symbol symbol, TimePeriodConst period, datetime focusedTime);

		public abstract void Tick(symbol symbol, Double Bid, Double Ask, Double volume);

		public abstract void DeinitAll();


		public new virtual IServerNormalSeriesManagerCache DefaultCache
		{
			get {
				return (IServerNormalSeriesManagerCache) ((ISeriesManagerRuntime)this).DefaultCache;
			}
			set {
				((ISeriesManagerRuntime)this).DefaultCache = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerSeriesManagerRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerSeriesManagerRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
