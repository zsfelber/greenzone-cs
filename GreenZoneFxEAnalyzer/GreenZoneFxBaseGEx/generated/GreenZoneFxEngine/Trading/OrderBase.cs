using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class OrderProps
	{
		public const int PROPERTY_1_EXPERT_ID = 1;
		public const int PROPERTY_2_SYMBOL_ID = 2;
		public const int PROPERTY_3_TYPE_ID = 3;
		public const int PROPERTY_4_TICKET_ID = 4;
		public const int PROPERTY_5_LOTS_ID = 5;
		public const int PROPERTY_6_STOPLOSS_ID = 6;
		public const int PROPERTY_7_TAKEPROFIT_ID = 7;
		public const int PROPERTY_8_OPENTIME_ID = 8;
		public const int PROPERTY_9_OPENPRICE_ID = 9;
		public const int PROPERTY_10_PENDINGOPENPRICE_ID = 10;
		public const int PROPERTY_11_CLOSETIME_ID = 11;
		public const int PROPERTY_12_CLOSEPRICE_ID = 12;
		public const int PROPERTY_13_COMMENT_ID = 13;
		public const int PROPERTY_14_EXPIRATION_ID = 14;
		public const int PROPERTY_15_MAGICNUMBER_ID = 15;
		public const int PROPERTY_16_COMMISSION_ID = 16;
		public const int PROPERTY_17_PROFIT_ID = 17;
		public const int PROPERTY_18_SWAP_ID = 18;
		public static bool RmiGetProperty(IOrder controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrderProps.PROPERTY_1_EXPERT_ID:
					value = controller.Expert;
					return true;
				case OrderProps.PROPERTY_2_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case OrderProps.PROPERTY_3_TYPE_ID:
					value = controller.Type;
					return true;
				case OrderProps.PROPERTY_4_TICKET_ID:
					value = controller.Ticket;
					return true;
				case OrderProps.PROPERTY_5_LOTS_ID:
					value = controller.Lots;
					return true;
				case OrderProps.PROPERTY_6_STOPLOSS_ID:
					value = controller.StopLoss;
					return true;
				case OrderProps.PROPERTY_7_TAKEPROFIT_ID:
					value = controller.TakeProfit;
					return true;
				case OrderProps.PROPERTY_8_OPENTIME_ID:
					value = controller.OpenTime;
					return true;
				case OrderProps.PROPERTY_9_OPENPRICE_ID:
					value = controller.OpenPrice;
					return true;
				case OrderProps.PROPERTY_10_PENDINGOPENPRICE_ID:
					value = controller.PendingOpenPrice;
					return true;
				case OrderProps.PROPERTY_11_CLOSETIME_ID:
					value = controller.CloseTime;
					return true;
				case OrderProps.PROPERTY_12_CLOSEPRICE_ID:
					value = controller.ClosePrice;
					return true;
				case OrderProps.PROPERTY_13_COMMENT_ID:
					value = controller.Comment;
					return true;
				case OrderProps.PROPERTY_14_EXPIRATION_ID:
					value = controller.Expiration;
					return true;
				case OrderProps.PROPERTY_15_MAGICNUMBER_ID:
					value = controller.MagicNumber;
					return true;
				case OrderProps.PROPERTY_16_COMMISSION_ID:
					value = controller.Commission;
					return true;
				case OrderProps.PROPERTY_17_PROFIT_ID:
					value = controller.Profit;
					return true;
				case OrderProps.PROPERTY_18_SWAP_ID:
					value = controller.Swap;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrder controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrderProps.PROPERTY_1_EXPERT_ID:
					controller.Expert = (Mt4ExecutableInfo) value;
					return true;
				case OrderProps.PROPERTY_2_SYMBOL_ID:
					controller.Symbol = (symbol) value;
					return true;
				case OrderProps.PROPERTY_3_TYPE_ID:
					controller.Type = (OrderType) value;
					return true;
				case OrderProps.PROPERTY_4_TICKET_ID:
					controller.Ticket = (Int32) value;
					return true;
				case OrderProps.PROPERTY_5_LOTS_ID:
					controller.Lots = (Double) value;
					return true;
				case OrderProps.PROPERTY_6_STOPLOSS_ID:
					controller.StopLoss = (Double) value;
					return true;
				case OrderProps.PROPERTY_7_TAKEPROFIT_ID:
					controller.TakeProfit = (Double) value;
					return true;
				case OrderProps.PROPERTY_8_OPENTIME_ID:
					controller.OpenTime = (datetime) value;
					return true;
				case OrderProps.PROPERTY_9_OPENPRICE_ID:
					controller.OpenPrice = (Double) value;
					return true;
				case OrderProps.PROPERTY_10_PENDINGOPENPRICE_ID:
					controller.PendingOpenPrice = (Double) value;
					return true;
				case OrderProps.PROPERTY_11_CLOSETIME_ID:
					controller.CloseTime = (datetime) value;
					return true;
				case OrderProps.PROPERTY_12_CLOSEPRICE_ID:
					controller.ClosePrice = (Double) value;
					return true;
				case OrderProps.PROPERTY_13_COMMENT_ID:
					controller.Comment = (String) value;
					return true;
				case OrderProps.PROPERTY_14_EXPIRATION_ID:
					controller.Expiration = (datetime) value;
					return true;
				case OrderProps.PROPERTY_15_MAGICNUMBER_ID:
					controller.MagicNumber = (Int32) value;
					return true;
				case OrderProps.PROPERTY_16_COMMISSION_ID:
					controller.Commission = (Double) value;
					return true;
				case OrderProps.PROPERTY_17_PROFIT_ID:
					controller.Profit = (Double) value;
					return true;
				case OrderProps.PROPERTY_18_SWAP_ID:
					controller.Swap = (Double) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrder controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IOrder controller, bool goToParent)
		{
		}

		public static void SerializationRead(IOrder controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Expert = (Mt4ExecutableInfo) info.GetValue("Expert", typeof(Mt4ExecutableInfo));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Type = (OrderType) info.GetValue("Type", typeof(OrderType));
			controller.Ticket = (Int32) info.GetValue("Ticket", typeof(Int32));
			controller.Lots = (Double) info.GetValue("Lots", typeof(Double));
			controller.StopLoss = (Double) info.GetValue("StopLoss", typeof(Double));
			controller.TakeProfit = (Double) info.GetValue("TakeProfit", typeof(Double));
			controller.OpenTime = (datetime) info.GetValue("OpenTime", typeof(datetime));
			controller.OpenPrice = (Double) info.GetValue("OpenPrice", typeof(Double));
			controller.PendingOpenPrice = (Double) info.GetValue("PendingOpenPrice", typeof(Double));
			controller.CloseTime = (datetime) info.GetValue("CloseTime", typeof(datetime));
			controller.ClosePrice = (Double) info.GetValue("ClosePrice", typeof(Double));
			controller.Comment = (String) info.GetValue("Comment", typeof(String));
			controller.Expiration = (datetime) info.GetValue("Expiration", typeof(datetime));
			controller.MagicNumber = (Int32) info.GetValue("MagicNumber", typeof(Int32));
			controller.Commission = (Double) info.GetValue("Commission", typeof(Double));
			controller.Profit = (Double) info.GetValue("Profit", typeof(Double));
			controller.Swap = (Double) info.GetValue("Swap", typeof(Double));
		}

		public static void SerializationWrite(IOrder controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Expert", controller.Expert);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Type", controller.Type);
			info.AddValue("Ticket", controller.Ticket);
			info.AddValue("Lots", controller.Lots);
			info.AddValue("StopLoss", controller.StopLoss);
			info.AddValue("TakeProfit", controller.TakeProfit);
			info.AddValue("OpenTime", controller.OpenTime);
			info.AddValue("OpenPrice", controller.OpenPrice);
			info.AddValue("PendingOpenPrice", controller.PendingOpenPrice);
			info.AddValue("CloseTime", controller.CloseTime);
			info.AddValue("ClosePrice", controller.ClosePrice);
			info.AddValue("Comment", controller.Comment);
			info.AddValue("Expiration", controller.Expiration);
			info.AddValue("MagicNumber", controller.MagicNumber);
			info.AddValue("Commission", controller.Commission);
			info.AddValue("Profit", controller.Profit);
			info.AddValue("Swap", controller.Swap);
		}

	}
	public abstract class OrderBase : RmiBase, IOrder
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrder_Expert_Changed;
		public event PropertyChangedEventHandler IOrder_Symbol_Changed;
		public event PropertyChangedEventHandler IOrder_Type_Changed;
		public event PropertyChangedEventHandler IOrder_Ticket_Changed;
		public event PropertyChangedEventHandler IOrder_Lots_Changed;
		public event PropertyChangedEventHandler IOrder_StopLoss_Changed;
		public event PropertyChangedEventHandler IOrder_TakeProfit_Changed;
		public event PropertyChangedEventHandler IOrder_OpenTime_Changed;
		public event PropertyChangedEventHandler IOrder_OpenPrice_Changed;
		public event PropertyChangedEventHandler IOrder_PendingOpenPrice_Changed;
		public event PropertyChangedEventHandler IOrder_CloseTime_Changed;
		public event PropertyChangedEventHandler IOrder_ClosePrice_Changed;
		public event PropertyChangedEventHandler IOrder_Comment_Changed;
		public event PropertyChangedEventHandler IOrder_Expiration_Changed;
		public event PropertyChangedEventHandler IOrder_MagicNumber_Changed;
		public event PropertyChangedEventHandler IOrder_Commission_Changed;
		public event PropertyChangedEventHandler IOrder_Profit_Changed;
		public event PropertyChangedEventHandler IOrder_Swap_Changed;

		public OrderBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrderProps.AddDependencies(this, false);
		}

		public OrderBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderProps.Initialize(this, buffer, false);
			___initialized = true;
			OrderProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected OrderBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrderProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderProps.SerializationWrite(this, info, context, false);
		}

		public abstract void Load(IEnvironmentRuntime env, BinaryReader r);

		public abstract void Save(BinaryWriter w);


		Mt4ExecutableInfo _IOrder_Expert;
		public Mt4ExecutableInfo Expert
		{
			get {
				return _IOrder_Expert;
			}
			set {
				if (_IOrder_Expert != value) {
					_IOrder_Expert= value;
					changed[OrderProps.PROPERTY_1_EXPERT_ID] = true;
					if (IOrder_Expert_Changed != null)
						IOrder_Expert_Changed(this, new PropertyChangedEventArgs("Expert", value));
				}
			}
		}

		symbol _IOrder_Symbol;
		public symbol Symbol
		{
			get {
				return _IOrder_Symbol;
			}
			set {
				if (_IOrder_Symbol != value) {
					_IOrder_Symbol= value;
					changed[OrderProps.PROPERTY_2_SYMBOL_ID] = true;
					if (IOrder_Symbol_Changed != null)
						IOrder_Symbol_Changed(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		OrderType _IOrder_Type;
		public OrderType Type
		{
			get {
				return _IOrder_Type;
			}
			set {
				if (_IOrder_Type != value) {
					_IOrder_Type= value;
					changed[OrderProps.PROPERTY_3_TYPE_ID] = true;
					if (IOrder_Type_Changed != null)
						IOrder_Type_Changed(this, new PropertyChangedEventArgs("Type", value));
				}
			}
		}

		Int32 _IOrder_Ticket;
		public Int32 Ticket
		{
			get {
				return _IOrder_Ticket;
			}
			set {
				if (_IOrder_Ticket != value) {
					_IOrder_Ticket= value;
					changed[OrderProps.PROPERTY_4_TICKET_ID] = true;
					if (IOrder_Ticket_Changed != null)
						IOrder_Ticket_Changed(this, new PropertyChangedEventArgs("Ticket", value));
				}
			}
		}

		Double _IOrder_Lots;
		public Double Lots
		{
			get {
				return _IOrder_Lots;
			}
			set {
				if (_IOrder_Lots != value) {
					_IOrder_Lots= value;
					changed[OrderProps.PROPERTY_5_LOTS_ID] = true;
					if (IOrder_Lots_Changed != null)
						IOrder_Lots_Changed(this, new PropertyChangedEventArgs("Lots", value));
				}
			}
		}

		Double _IOrder_StopLoss;
		public Double StopLoss
		{
			get {
				return _IOrder_StopLoss;
			}
			set {
				if (_IOrder_StopLoss != value) {
					_IOrder_StopLoss= value;
					changed[OrderProps.PROPERTY_6_STOPLOSS_ID] = true;
					if (IOrder_StopLoss_Changed != null)
						IOrder_StopLoss_Changed(this, new PropertyChangedEventArgs("StopLoss", value));
				}
			}
		}

		Double _IOrder_TakeProfit;
		public Double TakeProfit
		{
			get {
				return _IOrder_TakeProfit;
			}
			set {
				if (_IOrder_TakeProfit != value) {
					_IOrder_TakeProfit= value;
					changed[OrderProps.PROPERTY_7_TAKEPROFIT_ID] = true;
					if (IOrder_TakeProfit_Changed != null)
						IOrder_TakeProfit_Changed(this, new PropertyChangedEventArgs("TakeProfit", value));
				}
			}
		}

		datetime _IOrder_OpenTime;
		public datetime OpenTime
		{
			get {
				return _IOrder_OpenTime;
			}
			set {
				if (_IOrder_OpenTime != value) {
					_IOrder_OpenTime= value;
					changed[OrderProps.PROPERTY_8_OPENTIME_ID] = true;
					if (IOrder_OpenTime_Changed != null)
						IOrder_OpenTime_Changed(this, new PropertyChangedEventArgs("OpenTime", value));
				}
			}
		}

		Double _IOrder_OpenPrice;
		public Double OpenPrice
		{
			get {
				return _IOrder_OpenPrice;
			}
			set {
				if (_IOrder_OpenPrice != value) {
					_IOrder_OpenPrice= value;
					changed[OrderProps.PROPERTY_9_OPENPRICE_ID] = true;
					if (IOrder_OpenPrice_Changed != null)
						IOrder_OpenPrice_Changed(this, new PropertyChangedEventArgs("OpenPrice", value));
				}
			}
		}

		Double _IOrder_PendingOpenPrice;
		public Double PendingOpenPrice
		{
			get {
				return _IOrder_PendingOpenPrice;
			}
			set {
				if (_IOrder_PendingOpenPrice != value) {
					_IOrder_PendingOpenPrice= value;
					changed[OrderProps.PROPERTY_10_PENDINGOPENPRICE_ID] = true;
					if (IOrder_PendingOpenPrice_Changed != null)
						IOrder_PendingOpenPrice_Changed(this, new PropertyChangedEventArgs("PendingOpenPrice", value));
				}
			}
		}

		datetime _IOrder_CloseTime;
		public datetime CloseTime
		{
			get {
				return _IOrder_CloseTime;
			}
			set {
				if (_IOrder_CloseTime != value) {
					_IOrder_CloseTime= value;
					changed[OrderProps.PROPERTY_11_CLOSETIME_ID] = true;
					if (IOrder_CloseTime_Changed != null)
						IOrder_CloseTime_Changed(this, new PropertyChangedEventArgs("CloseTime", value));
				}
			}
		}

		Double _IOrder_ClosePrice;
		public Double ClosePrice
		{
			get {
				return _IOrder_ClosePrice;
			}
			set {
				if (_IOrder_ClosePrice != value) {
					_IOrder_ClosePrice= value;
					changed[OrderProps.PROPERTY_12_CLOSEPRICE_ID] = true;
					if (IOrder_ClosePrice_Changed != null)
						IOrder_ClosePrice_Changed(this, new PropertyChangedEventArgs("ClosePrice", value));
				}
			}
		}

		String _IOrder_Comment;
		public String Comment
		{
			get {
				return _IOrder_Comment;
			}
			set {
				if (_IOrder_Comment != value) {
					_IOrder_Comment= value;
					changed[OrderProps.PROPERTY_13_COMMENT_ID] = true;
					if (IOrder_Comment_Changed != null)
						IOrder_Comment_Changed(this, new PropertyChangedEventArgs("Comment", value));
				}
			}
		}

		datetime _IOrder_Expiration;
		public datetime Expiration
		{
			get {
				return _IOrder_Expiration;
			}
			set {
				if (_IOrder_Expiration != value) {
					_IOrder_Expiration= value;
					changed[OrderProps.PROPERTY_14_EXPIRATION_ID] = true;
					if (IOrder_Expiration_Changed != null)
						IOrder_Expiration_Changed(this, new PropertyChangedEventArgs("Expiration", value));
				}
			}
		}

		Int32 _IOrder_MagicNumber;
		public Int32 MagicNumber
		{
			get {
				return _IOrder_MagicNumber;
			}
			set {
				if (_IOrder_MagicNumber != value) {
					_IOrder_MagicNumber= value;
					changed[OrderProps.PROPERTY_15_MAGICNUMBER_ID] = true;
					if (IOrder_MagicNumber_Changed != null)
						IOrder_MagicNumber_Changed(this, new PropertyChangedEventArgs("MagicNumber", value));
				}
			}
		}

		Double _IOrder_Commission;
		public Double Commission
		{
			get {
				return _IOrder_Commission;
			}
			set {
				if (_IOrder_Commission != value) {
					_IOrder_Commission= value;
					changed[OrderProps.PROPERTY_16_COMMISSION_ID] = true;
					if (IOrder_Commission_Changed != null)
						IOrder_Commission_Changed(this, new PropertyChangedEventArgs("Commission", value));
				}
			}
		}

		Double _IOrder_Profit;
		public Double Profit
		{
			get {
				return _IOrder_Profit;
			}
			set {
				if (_IOrder_Profit != value) {
					_IOrder_Profit= value;
					changed[OrderProps.PROPERTY_17_PROFIT_ID] = true;
					if (IOrder_Profit_Changed != null)
						IOrder_Profit_Changed(this, new PropertyChangedEventArgs("Profit", value));
				}
			}
		}

		Double _IOrder_Swap;
		public Double Swap
		{
			get {
				return _IOrder_Swap;
			}
			set {
				if (_IOrder_Swap != value) {
					_IOrder_Swap= value;
					changed[OrderProps.PROPERTY_18_SWAP_ID] = true;
					if (IOrder_Swap_Changed != null)
						IOrder_Swap_Changed(this, new PropertyChangedEventArgs("Swap", value));
				}
			}
		}

		public abstract Dictionary<String,Object> Params
		{
			get ;
			set ;
		}

		public abstract Object  this[String field]
		{
			get;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrderProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
