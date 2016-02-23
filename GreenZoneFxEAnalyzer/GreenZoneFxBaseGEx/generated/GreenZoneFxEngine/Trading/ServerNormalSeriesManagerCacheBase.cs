using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerNormalSeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IServerNormalSeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (NormalSeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ServerSeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerNormalSeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (NormalSeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ServerSeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerNormalSeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
			ServerSeriesManagerCacheProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerNormalSeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.AddDependencies(controller, true);
			}
			ServerSeriesManagerCacheProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerNormalSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
			ServerSeriesManagerCacheProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerNormalSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
			ServerSeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerNormalSeriesManagerCacheBase : NormalSeriesManagerCacheEx, IServerNormalSeriesManagerCache
	{

		bool ___initialized = false;



		public ServerNormalSeriesManagerCacheBase(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
			: base(rmiManager, parent, symbolPeriod)
		{
			___initialized = true;
			ServerNormalSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ServerNormalSeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerNormalSeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerNormalSeriesManagerCacheProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerNormalSeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerNormalSeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerNormalSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerNormalSeriesManagerCacheProps.SerializationWrite(this, info, context, false);
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
			if (ServerNormalSeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerNormalSeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
