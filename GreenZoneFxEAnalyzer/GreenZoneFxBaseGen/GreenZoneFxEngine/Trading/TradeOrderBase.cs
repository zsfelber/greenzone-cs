using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class TradeOrderProps
	{
		public static bool RmiGetProperty(ITradeOrder controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITradeOrder controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(ITradeOrder controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(ITradeOrder controller)
		{
		}

		public static void SerializationRead(ITradeOrder controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(ITradeOrder controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class TradeOrderBase : OrderBase, ITradeOrder
	{

		bool ___initialized = false;


		public TradeOrderBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			TradeOrderProps.AddDependencies(this);
		}

		public TradeOrderBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TradeOrderProps.Initialize(this, buffer);
			___initialized = true;
			TradeOrderProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected TradeOrderBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TradeOrderProps.SerializationRead(this, info, context);
			___initialized = true;
			TradeOrderProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TradeOrderProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TradeOrderProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!TradeOrderProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
