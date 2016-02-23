using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class HistoryOrderProps
	{
		public static bool RmiGetProperty(IHistoryOrder controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IHistoryOrder controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IHistoryOrder controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IHistoryOrder controller)
		{
		}

		public static void SerializationRead(IHistoryOrder controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IHistoryOrder controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class HistoryOrderBase : OrderBase, IHistoryOrder
	{

		bool ___initialized = false;


		public HistoryOrderBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			HistoryOrderProps.AddDependencies(this);
		}

		public HistoryOrderBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			HistoryOrderProps.Initialize(this, buffer);
			___initialized = true;
			HistoryOrderProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected HistoryOrderBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			HistoryOrderProps.SerializationRead(this, info, context);
			___initialized = true;
			HistoryOrderProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			HistoryOrderProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (HistoryOrderProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!HistoryOrderProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
