using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerStreamSeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IServerStreamSeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerNormalSeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (StreamSeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerStreamSeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerNormalSeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (StreamSeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerStreamSeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerNormalSeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
			StreamSeriesManagerCacheProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerStreamSeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				ServerNormalSeriesManagerCacheProps.AddDependencies(controller, true);
			}
			StreamSeriesManagerCacheProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerStreamSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerNormalSeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
			StreamSeriesManagerCacheProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerStreamSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerNormalSeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
			StreamSeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerStreamSeriesManagerCacheBase : ServerNormalSeriesManagerCacheEx, IServerStreamSeriesManagerCache
	{

		bool ___initialized = false;



		internal ServerStreamSeriesManagerCacheBase(GreenRmiManager rmiManager, ServerSeriesManagerRuntimeEx parent, SymbolPeriodId symbolPeriod, datetime focusedTime)
			: base(rmiManager, parent, symbolPeriod, focusedTime)
		{
			___initialized = true;
			ServerStreamSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ServerStreamSeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerStreamSeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerStreamSeriesManagerCacheProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerStreamSeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerStreamSeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerStreamSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerStreamSeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}

		public abstract void AddExpert(IServerChartRuntime c);

		public abstract void Bar(TimePeriodConst period, Double open, Double low, Double high, Double close, Double volume, Int32 offset);

		public abstract void Tick(Double Bid, Double Ask, Double volume);




		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerStreamSeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerStreamSeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
