using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class HistoryOrderEtcProps
	{
		public const int PROPERTY_17_OPERATION_ID = 17;
		public const int PROPERTY_18_COLOR_ID = 18;
		public const int PROPERTY_19_BALANCE_ID = 19;
		public static bool RmiGetProperty(IHistoryOrderEtc controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_OPERATION_ID:
					value = controller.Operation;
					return true;
				case PROPERTY_18_COLOR_ID:
					value = controller.Color;
					return true;
				case PROPERTY_19_BALANCE_ID:
					value = controller.Balance;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IHistoryOrderEtc controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_OPERATION_ID:
					controller.Operation = (TradeOperation) value;
					return true;
				case PROPERTY_18_COLOR_ID:
					controller.Color = (Color) value;
					return true;
				case PROPERTY_19_BALANCE_ID:
					controller.Balance = (Double) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IHistoryOrderEtc controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IHistoryOrderEtc controller)
		{
		}

		public static void SerializationRead(IHistoryOrderEtc controller, SerializationInfo info, StreamingContext context)
		{
			controller.Operation = (TradeOperation) info.GetValue("Operation", typeof(TradeOperation));
			controller.Color = (Color) info.GetValue("Color", typeof(Color));
			controller.Balance = (Double) info.GetValue("Balance", typeof(Double));
		}

		public static void SerializationWrite(IHistoryOrderEtc controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Operation", controller.Operation);
			info.AddValue("Color", controller.Color);
			info.AddValue("Balance", controller.Balance);
		}

	}
	public abstract class HistoryOrderEtcBase : HistoryOrderBase, IHistoryOrderEtc
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler OperationChanged;
		public event PropertyChangedEventHandler ColorChanged;
		public event PropertyChangedEventHandler BalanceChanged;

		public HistoryOrderEtcBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this);
		}

		public HistoryOrderEtcBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			HistoryOrderEtcProps.Initialize(this, buffer);
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected HistoryOrderEtcBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			HistoryOrderEtcProps.SerializationRead(this, info, context);
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			HistoryOrderEtcProps.SerializationWrite(this, info, context);
		}

		TradeOperation operation;
		public TradeOperation Operation
		{
			get {
				return operation;
			}
			set {
				if (operation != value) {
					operation= value;
					changed[HistoryOrderEtcProps.PROPERTY_17_OPERATION_ID] = true;
					if (OperationChanged != null)
						OperationChanged(this, new PropertyChangedEventArgs("Operation", value));
				}
			}
		}

		Color color;
		public Color Color
		{
			get {
				return color;
			}
			set {
				if (color != value) {
					color= value;
					changed[HistoryOrderEtcProps.PROPERTY_18_COLOR_ID] = true;
					if (ColorChanged != null)
						ColorChanged(this, new PropertyChangedEventArgs("Color", value));
				}
			}
		}

		Double balance;
		public Double Balance
		{
			get {
				return balance;
			}
			set {
				if (balance != value) {
					balance= value;
					changed[HistoryOrderEtcProps.PROPERTY_19_BALANCE_ID] = true;
					if (BalanceChanged != null)
						BalanceChanged(this, new PropertyChangedEventArgs("Balance", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (HistoryOrderEtcProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!HistoryOrderEtcProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
