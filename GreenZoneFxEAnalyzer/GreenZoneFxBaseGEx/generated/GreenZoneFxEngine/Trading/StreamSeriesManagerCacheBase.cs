using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class StreamSeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IStreamSeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (NormalSeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IStreamSeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (NormalSeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IStreamSeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IStreamSeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IStreamSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IStreamSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				NormalSeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class StreamSeriesManagerCacheBase : NormalSeriesManagerCacheEx, IStreamSeriesManagerCache
	{

		bool ___initialized = false;


		public StreamSeriesManagerCacheBase(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
			: base(rmiManager, parent, symbolPeriod)
		{
			___initialized = true;
			StreamSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public StreamSeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			StreamSeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			StreamSeriesManagerCacheProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected StreamSeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			StreamSeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			StreamSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			StreamSeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (StreamSeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (StreamSeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
