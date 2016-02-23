using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class TradeOrderProps
	{
		public const int PROPERTY_19_SLIPPAGE_ID = 19;
		public static bool RmiGetProperty(ITradeOrder controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (OrderProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case TradeOrderProps.PROPERTY_19_SLIPPAGE_ID:
					value = controller.Slippage;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITradeOrder controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (OrderProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case TradeOrderProps.PROPERTY_19_SLIPPAGE_ID:
					controller.Slippage = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITradeOrder controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				OrderProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(ITradeOrder controller, bool goToParent)
		{
			if (goToParent) {
				OrderProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(ITradeOrder controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrderProps.SerializationRead(controller, info, context, true);
			}
			controller.Slippage = (Int32) info.GetValue("Slippage", typeof(Int32));
		}

		public static void SerializationWrite(ITradeOrder controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrderProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("Slippage", controller.Slippage);
		}

	}
	public abstract class TradeOrderBase : OrderEx, ITradeOrder
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ITradeOrder_Slippage_Changed;

		public TradeOrderBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			TradeOrderProps.AddDependencies(this, false);
		}

		public TradeOrderBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TradeOrderProps.Initialize(this, buffer, false);
			___initialized = true;
			TradeOrderProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected TradeOrderBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TradeOrderProps.SerializationRead(this, info, context, false);
			___initialized = true;
			TradeOrderProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TradeOrderProps.SerializationWrite(this, info, context, false);
		}


		Int32 _ITradeOrder_Slippage;
		public Int32 Slippage
		{
			get {
				return _ITradeOrder_Slippage;
			}
			set {
				if (_ITradeOrder_Slippage != value) {
					_ITradeOrder_Slippage= value;
					changed[TradeOrderProps.PROPERTY_19_SLIPPAGE_ID] = true;
					if (ITradeOrder_Slippage_Changed != null)
						ITradeOrder_Slippage_Changed(this, new PropertyChangedEventArgs("Slippage", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TradeOrderProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (TradeOrderProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
