using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ArraySeriesManagerCacheProps
	{
		public static bool RmiGetProperty(IArraySeriesManagerCache controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IArraySeriesManagerCache controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IArraySeriesManagerCache controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IArraySeriesManagerCache controller)
		{
		}

		public static void SerializationRead(IArraySeriesManagerCache controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IArraySeriesManagerCache controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ArraySeriesManagerCacheBase : SeriesManagerCacheBase, IArraySeriesManagerCache
	{

		bool ___initialized = false;


		public ArraySeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this);
		}

		public ArraySeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ArraySeriesManagerCacheProps.Initialize(this, buffer);
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ArraySeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ArraySeriesManagerCacheProps.SerializationRead(this, info, context);
			___initialized = true;
			ArraySeriesManagerCacheProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ArraySeriesManagerCacheProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ArraySeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ArraySeriesManagerCacheProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
