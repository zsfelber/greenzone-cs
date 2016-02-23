using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerSeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IServerSeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (SeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerSeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (SeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerSeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerSeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerSeriesManagerCacheBase : SeriesManagerCacheEx, IServerSeriesManagerCache
	{

		bool ___initialized = false;


		public ServerSeriesManagerCacheBase(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
			: base(rmiManager, parent, symbolPeriod)
		{
			___initialized = true;
			ServerSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ServerSeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ServerSeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerSeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerSeriesManagerCacheProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerSeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerSeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerSeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}

		public abstract void DeinitAll();


		public new virtual IServerSymbolRuntime SymbolRuntime
		{
			get {
				return (IServerSymbolRuntime) ((ISeriesManagerCache)this).SymbolRuntime;
			}
			set {
				((ISeriesManagerCache)this).SymbolRuntime = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerSeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerSeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
