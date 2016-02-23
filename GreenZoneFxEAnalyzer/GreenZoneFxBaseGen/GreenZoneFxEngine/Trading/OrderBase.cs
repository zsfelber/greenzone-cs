using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class OrderProps
	{
		public const int PROPERTY_1_SYMBOL_ID = 1;
		public const int PROPERTY_2_TYPE_ID = 2;
		public const int PROPERTY_3_TICKET_ID = 3;
		public const int PROPERTY_4_LOTS_ID = 4;
		public const int PROPERTY_5_STOPLOSS_ID = 5;
		public const int PROPERTY_6_TAKEPROFIT_ID = 6;
		public const int PROPERTY_7_OPENTIME_ID = 7;
		public const int PROPERTY_8_OPENPRICE_ID = 8;
		public const int PROPERTY_9_CLOSETIME_ID = 9;
		public const int PROPERTY_10_CLOSEPRICE_ID = 10;
		public const int PROPERTY_11_COMMENT_ID = 11;
		public const int PROPERTY_12_EXPIRATION_ID = 12;
		public const int PROPERTY_13_MAGICNUMBER_ID = 13;
		public const int PROPERTY_14_COMMISSION_ID = 14;
		public const int PROPERTY_15_PROFIT_ID = 15;
		public const int PROPERTY_16_SWAP_ID = 16;
		public static bool RmiGetProperty(IOrder controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_2_TYPE_ID:
					value = controller.Type;
					return true;
				case PROPERTY_3_TICKET_ID:
					value = controller.Ticket;
					return true;
				case PROPERTY_4_LOTS_ID:
					value = controller.Lots;
					return true;
				case PROPERTY_5_STOPLOSS_ID:
					value = controller.StopLoss;
					return true;
				case PROPERTY_6_TAKEPROFIT_ID:
					value = controller.TakeProfit;
					return true;
				case PROPERTY_7_OPENTIME_ID:
					value = controller.OpenTime;
					return true;
				case PROPERTY_8_OPENPRICE_ID:
					value = controller.OpenPrice;
					return true;
				case PROPERTY_9_CLOSETIME_ID:
					value = controller.CloseTime;
					return true;
				case PROPERTY_10_CLOSEPRICE_ID:
					value = controller.ClosePrice;
					return true;
				case PROPERTY_11_COMMENT_ID:
					value = controller.Comment;
					return true;
				case PROPERTY_12_EXPIRATION_ID:
					value = controller.Expiration;
					return true;
				case PROPERTY_13_MAGICNUMBER_ID:
					value = controller.MagicNumber;
					return true;
				case PROPERTY_14_COMMISSION_ID:
					value = controller.Commission;
					return true;
				case PROPERTY_15_PROFIT_ID:
					value = controller.Profit;
					return true;
				case PROPERTY_16_SWAP_ID:
					value = controller.Swap;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrder controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_SYMBOL_ID:
					controller.Symbol = (symbol) value;
					return true;
				case PROPERTY_2_TYPE_ID:
					controller.Type = (OrderType) value;
					return true;
				case PROPERTY_3_TICKET_ID:
					controller.Ticket = (Int32) value;
					return true;
				case PROPERTY_4_LOTS_ID:
					controller.Lots = (Double) value;
					return true;
				case PROPERTY_5_STOPLOSS_ID:
					controller.StopLoss = (Double) value;
					return true;
				case PROPERTY_6_TAKEPROFIT_ID:
					controller.TakeProfit = (Double) value;
					return true;
				case PROPERTY_7_OPENTIME_ID:
					controller.OpenTime = (datetime) value;
					return true;
				case PROPERTY_8_OPENPRICE_ID:
					controller.OpenPrice = (Double) value;
					return true;
				case PROPERTY_9_CLOSETIME_ID:
					controller.CloseTime = (datetime) value;
					return true;
				case PROPERTY_10_CLOSEPRICE_ID:
					controller.ClosePrice = (Double) value;
					return true;
				case PROPERTY_11_COMMENT_ID:
					controller.Comment = (String) value;
					return true;
				case PROPERTY_12_EXPIRATION_ID:
					controller.Expiration = (datetime) value;
					return true;
				case PROPERTY_13_MAGICNUMBER_ID:
					controller.MagicNumber = (Int32) value;
					return true;
				case PROPERTY_14_COMMISSION_ID:
					controller.Commission = (Double) value;
					return true;
				case PROPERTY_15_PROFIT_ID:
					controller.Profit = (Double) value;
					return true;
				case PROPERTY_16_SWAP_ID:
					controller.Swap = (Double) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrder controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IOrder controller)
		{
		}

		public static void SerializationRead(IOrder controller, SerializationInfo info, StreamingContext context)
		{
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Type = (OrderType) info.GetValue("Type", typeof(OrderType));
			controller.Ticket = (Int32) info.GetValue("Ticket", typeof(Int32));
			controller.Lots = (Double) info.GetValue("Lots", typeof(Double));
			controller.StopLoss = (Double) info.GetValue("StopLoss", typeof(Double));
			controller.TakeProfit = (Double) info.GetValue("TakeProfit", typeof(Double));
			controller.OpenTime = (datetime) info.GetValue("OpenTime", typeof(datetime));
			controller.OpenPrice = (Double) info.GetValue("OpenPrice", typeof(Double));
			controller.CloseTime = (datetime) info.GetValue("CloseTime", typeof(datetime));
			controller.ClosePrice = (Double) info.GetValue("ClosePrice", typeof(Double));
			controller.Comment = (String) info.GetValue("Comment", typeof(String));
			controller.Expiration = (datetime) info.GetValue("Expiration", typeof(datetime));
			controller.MagicNumber = (Int32) info.GetValue("MagicNumber", typeof(Int32));
			controller.Commission = (Double) info.GetValue("Commission", typeof(Double));
			controller.Profit = (Double) info.GetValue("Profit", typeof(Double));
			controller.Swap = (Double) info.GetValue("Swap", typeof(Double));
		}

		public static void SerializationWrite(IOrder controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Type", controller.Type);
			info.AddValue("Ticket", controller.Ticket);
			info.AddValue("Lots", controller.Lots);
			info.AddValue("StopLoss", controller.StopLoss);
			info.AddValue("TakeProfit", controller.TakeProfit);
			info.AddValue("OpenTime", controller.OpenTime);
			info.AddValue("OpenPrice", controller.OpenPrice);
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

		public event PropertyChangedEventHandler SymbolChanged;
		public event PropertyChangedEventHandler TypeChanged;
		public event PropertyChangedEventHandler TicketChanged;
		public event PropertyChangedEventHandler LotsChanged;
		public event PropertyChangedEventHandler StopLossChanged;
		public event PropertyChangedEventHandler TakeProfitChanged;
		public event PropertyChangedEventHandler OpenTimeChanged;
		public event PropertyChangedEventHandler OpenPriceChanged;
		public event PropertyChangedEventHandler CloseTimeChanged;
		public event PropertyChangedEventHandler ClosePriceChanged;
		public event PropertyChangedEventHandler CommentChanged;
		public event PropertyChangedEventHandler ExpirationChanged;
		public event PropertyChangedEventHandler MagicNumberChanged;
		public event PropertyChangedEventHandler CommissionChanged;
		public event PropertyChangedEventHandler ProfitChanged;
		public event PropertyChangedEventHandler SwapChanged;

		public OrderBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrderProps.AddDependencies(this);
		}

		public OrderBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderProps.Initialize(this, buffer);
			___initialized = true;
			OrderProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected OrderBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderProps.SerializationRead(this, info, context);
			___initialized = true;
			OrderProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderProps.SerializationWrite(this, info, context);
		}

		symbol symbol;
		public symbol Symbol
		{
			get {
				return symbol;
			}
			set {
				if (symbol != value) {
					symbol= value;
					changed[OrderProps.PROPERTY_1_SYMBOL_ID] = true;
					if (SymbolChanged != null)
						SymbolChanged(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		OrderType type;
		public OrderType Type
		{
			get {
				return type;
			}
			set {
				if (type != value) {
					type= value;
					changed[OrderProps.PROPERTY_2_TYPE_ID] = true;
					if (TypeChanged != null)
						TypeChanged(this, new PropertyChangedEventArgs("Type", value));
				}
			}
		}

		Int32 ticket;
		public Int32 Ticket
		{
			get {
				return ticket;
			}
			set {
				if (ticket != value) {
					ticket= value;
					changed[OrderProps.PROPERTY_3_TICKET_ID] = true;
					if (TicketChanged != null)
						TicketChanged(this, new PropertyChangedEventArgs("Ticket", value));
				}
			}
		}

		Double lots;
		public Double Lots
		{
			get {
				return lots;
			}
			set {
				if (lots != value) {
					lots= value;
					changed[OrderProps.PROPERTY_4_LOTS_ID] = true;
					if (LotsChanged != null)
						LotsChanged(this, new PropertyChangedEventArgs("Lots", value));
				}
			}
		}

		Double stopLoss;
		public Double StopLoss
		{
			get {
				return stopLoss;
			}
			set {
				if (stopLoss != value) {
					stopLoss= value;
					changed[OrderProps.PROPERTY_5_STOPLOSS_ID] = true;
					if (StopLossChanged != null)
						StopLossChanged(this, new PropertyChangedEventArgs("StopLoss", value));
				}
			}
		}

		Double takeProfit;
		public Double TakeProfit
		{
			get {
				return takeProfit;
			}
			set {
				if (takeProfit != value) {
					takeProfit= value;
					changed[OrderProps.PROPERTY_6_TAKEPROFIT_ID] = true;
					if (TakeProfitChanged != null)
						TakeProfitChanged(this, new PropertyChangedEventArgs("TakeProfit", value));
				}
			}
		}

		datetime openTime;
		public datetime OpenTime
		{
			get {
				return openTime;
			}
			set {
				if (openTime != value) {
					openTime= value;
					changed[OrderProps.PROPERTY_7_OPENTIME_ID] = true;
					if (OpenTimeChanged != null)
						OpenTimeChanged(this, new PropertyChangedEventArgs("OpenTime", value));
				}
			}
		}

		Double openPrice;
		public Double OpenPrice
		{
			get {
				return openPrice;
			}
			set {
				if (openPrice != value) {
					openPrice= value;
					changed[OrderProps.PROPERTY_8_OPENPRICE_ID] = true;
					if (OpenPriceChanged != null)
						OpenPriceChanged(this, new PropertyChangedEventArgs("OpenPrice", value));
				}
			}
		}

		datetime closeTime;
		public datetime CloseTime
		{
			get {
				return closeTime;
			}
			set {
				if (closeTime != value) {
					closeTime= value;
					changed[OrderProps.PROPERTY_9_CLOSETIME_ID] = true;
					if (CloseTimeChanged != null)
						CloseTimeChanged(this, new PropertyChangedEventArgs("CloseTime", value));
				}
			}
		}

		Double closePrice;
		public Double ClosePrice
		{
			get {
				return closePrice;
			}
			set {
				if (closePrice != value) {
					closePrice= value;
					changed[OrderProps.PROPERTY_10_CLOSEPRICE_ID] = true;
					if (ClosePriceChanged != null)
						ClosePriceChanged(this, new PropertyChangedEventArgs("ClosePrice", value));
				}
			}
		}

		String comment;
		public String Comment
		{
			get {
				return comment;
			}
			set {
				if (comment != value) {
					comment= value;
					changed[OrderProps.PROPERTY_11_COMMENT_ID] = true;
					if (CommentChanged != null)
						CommentChanged(this, new PropertyChangedEventArgs("Comment", value));
				}
			}
		}

		datetime expiration;
		public datetime Expiration
		{
			get {
				return expiration;
			}
			set {
				if (expiration != value) {
					expiration= value;
					changed[OrderProps.PROPERTY_12_EXPIRATION_ID] = true;
					if (ExpirationChanged != null)
						ExpirationChanged(this, new PropertyChangedEventArgs("Expiration", value));
				}
			}
		}

		Int32 magicNumber;
		public Int32 MagicNumber
		{
			get {
				return magicNumber;
			}
			set {
				if (magicNumber != value) {
					magicNumber= value;
					changed[OrderProps.PROPERTY_13_MAGICNUMBER_ID] = true;
					if (MagicNumberChanged != null)
						MagicNumberChanged(this, new PropertyChangedEventArgs("MagicNumber", value));
				}
			}
		}

		Double commission;
		public Double Commission
		{
			get {
				return commission;
			}
			set {
				if (commission != value) {
					commission= value;
					changed[OrderProps.PROPERTY_14_COMMISSION_ID] = true;
					if (CommissionChanged != null)
						CommissionChanged(this, new PropertyChangedEventArgs("Commission", value));
				}
			}
		}

		Double profit;
		public Double Profit
		{
			get {
				return profit;
			}
			set {
				if (profit != value) {
					profit= value;
					changed[OrderProps.PROPERTY_15_PROFIT_ID] = true;
					if (ProfitChanged != null)
						ProfitChanged(this, new PropertyChangedEventArgs("Profit", value));
				}
			}
		}

		Double swap;
		public Double Swap
		{
			get {
				return swap;
			}
			set {
				if (swap != value) {
					swap= value;
					changed[OrderProps.PROPERTY_16_SWAP_ID] = true;
					if (SwapChanged != null)
						SwapChanged(this, new PropertyChangedEventArgs("Swap", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrderProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
