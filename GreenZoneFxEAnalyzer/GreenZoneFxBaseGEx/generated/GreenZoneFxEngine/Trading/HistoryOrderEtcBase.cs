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
		public const int PROPERTY_19_OPERATION_ID = 19;
		public const int PROPERTY_20_COLOR_ID = 20;
		public const int PROPERTY_21_BALANCE_ID = 21;
		public static bool RmiGetProperty(IHistoryOrderEtc controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (HistoryOrderProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case HistoryOrderEtcProps.PROPERTY_19_OPERATION_ID:
					value = controller.Operation;
					return true;
				case HistoryOrderEtcProps.PROPERTY_20_COLOR_ID:
					value = controller.Color;
					return true;
				case HistoryOrderEtcProps.PROPERTY_21_BALANCE_ID:
					value = controller.Balance;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IHistoryOrderEtc controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (HistoryOrderProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case HistoryOrderEtcProps.PROPERTY_19_OPERATION_ID:
					controller.Operation = (TradeOperation) value;
					return true;
				case HistoryOrderEtcProps.PROPERTY_20_COLOR_ID:
					controller.Color = (Color) value;
					return true;
				case HistoryOrderEtcProps.PROPERTY_21_BALANCE_ID:
					controller.Balance = (Double) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IHistoryOrderEtc controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				HistoryOrderProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IHistoryOrderEtc controller, bool goToParent)
		{
			if (goToParent) {
				HistoryOrderProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IHistoryOrderEtc controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				HistoryOrderProps.SerializationRead(controller, info, context, true);
			}
			controller.Operation = (TradeOperation) info.GetValue("Operation", typeof(TradeOperation));
			controller.Color = (Color) info.GetValue("Color", typeof(Color));
			controller.Balance = (Double) info.GetValue("Balance", typeof(Double));
		}

		public static void SerializationWrite(IHistoryOrderEtc controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				HistoryOrderProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("Operation", controller.Operation);
			info.AddValue("Color", controller.Color);
			info.AddValue("Balance", controller.Balance);
		}

	}
	public abstract class HistoryOrderEtcBase : HistoryOrderEx, IHistoryOrderEtc
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IHistoryOrderEtc_Operation_Changed;
		public event PropertyChangedEventHandler IHistoryOrderEtc_Color_Changed;
		public event PropertyChangedEventHandler IHistoryOrderEtc_Balance_Changed;

		public HistoryOrderEtcBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this, false);
		}

		public HistoryOrderEtcBase(GreenRmiManager rmiManager, ITradeOrder order)
			: base(rmiManager, order)
		{
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this, false);
		}

		public HistoryOrderEtcBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			HistoryOrderEtcProps.Initialize(this, buffer, false);
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected HistoryOrderEtcBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			HistoryOrderEtcProps.SerializationRead(this, info, context, false);
			___initialized = true;
			HistoryOrderEtcProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			HistoryOrderEtcProps.SerializationWrite(this, info, context, false);
		}


		TradeOperation _IHistoryOrderEtc_Operation;
		public TradeOperation Operation
		{
			get {
				return _IHistoryOrderEtc_Operation;
			}
			set {
				if (_IHistoryOrderEtc_Operation != value) {
					_IHistoryOrderEtc_Operation= value;
					changed[HistoryOrderEtcProps.PROPERTY_19_OPERATION_ID] = true;
					if (IHistoryOrderEtc_Operation_Changed != null)
						IHistoryOrderEtc_Operation_Changed(this, new PropertyChangedEventArgs("Operation", value));
				}
			}
		}

		Color _IHistoryOrderEtc_Color;
		public Color Color
		{
			get {
				return _IHistoryOrderEtc_Color;
			}
			set {
				if (_IHistoryOrderEtc_Color != value) {
					_IHistoryOrderEtc_Color= value;
					changed[HistoryOrderEtcProps.PROPERTY_20_COLOR_ID] = true;
					if (IHistoryOrderEtc_Color_Changed != null)
						IHistoryOrderEtc_Color_Changed(this, new PropertyChangedEventArgs("Color", value));
				}
			}
		}

		Double _IHistoryOrderEtc_Balance;
		public Double Balance
		{
			get {
				return _IHistoryOrderEtc_Balance;
			}
			set {
				if (_IHistoryOrderEtc_Balance != value) {
					_IHistoryOrderEtc_Balance= value;
					changed[HistoryOrderEtcProps.PROPERTY_21_BALANCE_ID] = true;
					if (IHistoryOrderEtc_Balance_Changed != null)
						IHistoryOrderEtc_Balance_Changed(this, new PropertyChangedEventArgs("Balance", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (HistoryOrderEtcProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (HistoryOrderEtcProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
