using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class HistoryOrderProps
	{
		public static bool RmiGetProperty(IHistoryOrder controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (OrderProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IHistoryOrder controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (OrderProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IHistoryOrder controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				OrderProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IHistoryOrder controller, bool goToParent)
		{
			if (goToParent) {
				OrderProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IHistoryOrder controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrderProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IHistoryOrder controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrderProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class HistoryOrderBase : OrderEx, IHistoryOrder
	{

		bool ___initialized = false;


		public HistoryOrderBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			HistoryOrderProps.AddDependencies(this, false);
		}

		public HistoryOrderBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			HistoryOrderProps.Initialize(this, buffer, false);
			___initialized = true;
			HistoryOrderProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected HistoryOrderBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			HistoryOrderProps.SerializationRead(this, info, context, false);
			___initialized = true;
			HistoryOrderProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			HistoryOrderProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (HistoryOrderProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (HistoryOrderProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
