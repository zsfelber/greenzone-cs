using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ArraySeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IArraySeriesManagerCache controller, int propertyId, out object value, bool goToParent)
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
		public static bool RmiSetProperty(IArraySeriesManagerCache controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IArraySeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IArraySeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IArraySeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IArraySeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ArraySeriesManagerCacheBase : SeriesManagerCacheEx, IArraySeriesManagerCache
	{

		bool ___initialized = false;


		public ArraySeriesManagerCacheBase(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
			: base(rmiManager, parent, symbolPeriod)
		{
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ArraySeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this, false);
		}

		public ArraySeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ArraySeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ArraySeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ArraySeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ArraySeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ArraySeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ArraySeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
