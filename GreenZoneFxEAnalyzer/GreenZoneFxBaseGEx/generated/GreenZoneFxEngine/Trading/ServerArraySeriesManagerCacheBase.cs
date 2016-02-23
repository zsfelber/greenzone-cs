using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerArraySeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IServerArraySeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerSeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ArraySeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerArraySeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerSeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ArraySeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerArraySeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerSeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
			ArraySeriesManagerCacheProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerArraySeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				ServerSeriesManagerCacheProps.AddDependencies(controller, true);
			}
			ArraySeriesManagerCacheProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerArraySeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerSeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
			ArraySeriesManagerCacheProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerArraySeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerSeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
			ArraySeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerArraySeriesManagerCacheBase : ServerSeriesManagerCacheBase, IServerArraySeriesManagerCache
	{

		bool ___initialized = false;



		public ServerArraySeriesManagerCacheBase(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
			: base(rmiManager, parent, symbolPeriod)
		{
			___initialized = true;
			ServerArraySeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ServerArraySeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerArraySeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ServerArraySeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerArraySeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerArraySeriesManagerCacheProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerArraySeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerArraySeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerArraySeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerArraySeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}




		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerArraySeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerArraySeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
