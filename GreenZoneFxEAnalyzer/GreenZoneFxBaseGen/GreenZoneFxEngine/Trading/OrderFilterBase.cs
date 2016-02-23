using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class OrderFilterProps
	{
		public const int PROPERTY_1_GROUPBY_ID = 1;
		public const int PROPERTY_2_BUY_ID = 2;
		public const int PROPERTY_3_SELL_ID = 3;
		public const int PROPERTY_4_LIMIT_ID = 4;
		public const int PROPERTY_5_STOP_ID = 5;
		public const int PROPERTY_6_FROM_ID = 6;
		public const int PROPERTY_7_TO_ID = 7;
		public const int PROPERTY_8_MORE_ID = 8;
		public const int PROPERTY_9_TICKET_ID = 9;
		public const int PROPERTY_10_SYMBOL_ID = 10;
		public const int PROPERTY_11_OPERATION_ID = 11;
		public const int PROPERTY_12_EXPERT_ID = 12;
		public const int PROPERTY_13_MAGIC_ID = 13;
		public const int PROPERTY_14_COMMENT_ID = 14;
		public const int PROPERTY_15_HIDDENCOLUMNS_ID = 15;
		public static bool RmiGetProperty(IOrderFilter controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_GROUPBY_ID:
					value = controller.GroupBy;
					return true;
				case PROPERTY_2_BUY_ID:
					value = controller.Buy;
					return true;
				case PROPERTY_3_SELL_ID:
					value = controller.Sell;
					return true;
				case PROPERTY_4_LIMIT_ID:
					value = controller.Limit;
					return true;
				case PROPERTY_5_STOP_ID:
					value = controller.Stop;
					return true;
				case PROPERTY_6_FROM_ID:
					value = controller.From;
					return true;
				case PROPERTY_7_TO_ID:
					value = controller.To;
					return true;
				case PROPERTY_8_MORE_ID:
					value = controller.More;
					return true;
				case PROPERTY_9_TICKET_ID:
					value = controller.Ticket;
					return true;
				case PROPERTY_10_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_11_OPERATION_ID:
					value = controller.Operation;
					return true;
				case PROPERTY_12_EXPERT_ID:
					value = controller.Expert;
					return true;
				case PROPERTY_13_MAGIC_ID:
					value = controller.Magic;
					return true;
				case PROPERTY_14_COMMENT_ID:
					value = controller.Comment;
					return true;
				case PROPERTY_15_HIDDENCOLUMNS_ID:
					value = controller.HiddenColumns;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderFilter controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_GROUPBY_ID:
					controller.GroupBy = (String) value;
					return true;
				case PROPERTY_2_BUY_ID:
					controller.Buy = (Boolean) value;
					return true;
				case PROPERTY_3_SELL_ID:
					controller.Sell = (Boolean) value;
					return true;
				case PROPERTY_4_LIMIT_ID:
					controller.Limit = (Boolean) value;
					return true;
				case PROPERTY_5_STOP_ID:
					controller.Stop = (Boolean) value;
					return true;
				case PROPERTY_6_FROM_ID:
					controller.From = (datetime) value;
					return true;
				case PROPERTY_7_TO_ID:
					controller.To = (datetime) value;
					return true;
				case PROPERTY_8_MORE_ID:
					controller.More = (Boolean) value;
					return true;
				case PROPERTY_9_TICKET_ID:
					controller.Ticket = (Int32) value;
					return true;
				case PROPERTY_10_SYMBOL_ID:
					controller.Symbol = (String) value;
					return true;
				case PROPERTY_11_OPERATION_ID:
					controller.Operation = (TradeOperation) value;
					return true;
				case PROPERTY_12_EXPERT_ID:
					controller.Expert = (String) value;
					return true;
				case PROPERTY_13_MAGIC_ID:
					controller.Magic = (Int32) value;
					return true;
				case PROPERTY_14_COMMENT_ID:
					controller.Comment = (String) value;
					return true;
				case PROPERTY_15_HIDDENCOLUMNS_ID:
					controller.HiddenColumns = (SortedSet<String>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrderFilter controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IOrderFilter controller)
		{
		}

		public static void SerializationRead(IOrderFilter controller, SerializationInfo info, StreamingContext context)
		{
			controller.GroupBy = (String) info.GetValue("GroupBy", typeof(String));
			controller.Buy = (Boolean) info.GetValue("Buy", typeof(Boolean));
			controller.Sell = (Boolean) info.GetValue("Sell", typeof(Boolean));
			controller.Limit = (Boolean) info.GetValue("Limit", typeof(Boolean));
			controller.Stop = (Boolean) info.GetValue("Stop", typeof(Boolean));
			controller.From = (datetime) info.GetValue("From", typeof(datetime));
			controller.To = (datetime) info.GetValue("To", typeof(datetime));
			controller.More = (Boolean) info.GetValue("More", typeof(Boolean));
			controller.Ticket = (Int32) info.GetValue("Ticket", typeof(Int32));
			controller.Symbol = (String) info.GetValue("Symbol", typeof(String));
			controller.Operation = (TradeOperation) info.GetValue("Operation", typeof(TradeOperation));
			controller.Expert = (String) info.GetValue("Expert", typeof(String));
			controller.Magic = (Int32) info.GetValue("Magic", typeof(Int32));
			controller.Comment = (String) info.GetValue("Comment", typeof(String));
			controller.HiddenColumns = (SortedSet<String>) info.GetValue("HiddenColumns", typeof(SortedSet<String>));
		}

		public static void SerializationWrite(IOrderFilter controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("GroupBy", controller.GroupBy);
			info.AddValue("Buy", controller.Buy);
			info.AddValue("Sell", controller.Sell);
			info.AddValue("Limit", controller.Limit);
			info.AddValue("Stop", controller.Stop);
			info.AddValue("From", controller.From);
			info.AddValue("To", controller.To);
			info.AddValue("More", controller.More);
			info.AddValue("Ticket", controller.Ticket);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Operation", controller.Operation);
			info.AddValue("Expert", controller.Expert);
			info.AddValue("Magic", controller.Magic);
			info.AddValue("Comment", controller.Comment);
			info.AddValue("HiddenColumns", controller.HiddenColumns);
		}

	}
	public abstract class OrderFilterBase : RmiBase, IOrderFilter
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler GroupByChanged;
		public event PropertyChangedEventHandler BuyChanged;
		public event PropertyChangedEventHandler SellChanged;
		public event PropertyChangedEventHandler LimitChanged;
		public event PropertyChangedEventHandler StopChanged;
		public event PropertyChangedEventHandler FromChanged;
		public event PropertyChangedEventHandler ToChanged;
		public event PropertyChangedEventHandler MoreChanged;
		public event PropertyChangedEventHandler TicketChanged;
		public event PropertyChangedEventHandler SymbolChanged;
		public event PropertyChangedEventHandler OperationChanged;
		public event PropertyChangedEventHandler ExpertChanged;
		public event PropertyChangedEventHandler MagicChanged;
		public event PropertyChangedEventHandler CommentChanged;
		public event PropertyChangedEventHandler HiddenColumnsChanged;

		public OrderFilterBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrderFilterProps.AddDependencies(this);
		}

		public OrderFilterBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderFilterProps.Initialize(this, buffer);
			___initialized = true;
			OrderFilterProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected OrderFilterBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderFilterProps.SerializationRead(this, info, context);
			___initialized = true;
			OrderFilterProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderFilterProps.SerializationWrite(this, info, context);
		}

		String groupBy = "";
		public String GroupBy
		{
			get {
				return groupBy;
			}
			set {
				if (groupBy != value) {
					groupBy= value;
					changed[OrderFilterProps.PROPERTY_1_GROUPBY_ID] = true;
					if (GroupByChanged != null)
						GroupByChanged(this, new PropertyChangedEventArgs("GroupBy", value));
				}
			}
		}

		Boolean buy = true;
		public Boolean Buy
		{
			get {
				return buy;
			}
			set {
				if (buy != value) {
					buy= value;
					changed[OrderFilterProps.PROPERTY_2_BUY_ID] = true;
					if (BuyChanged != null)
						BuyChanged(this, new PropertyChangedEventArgs("Buy", value));
				}
			}
		}

		Boolean sell = true;
		public Boolean Sell
		{
			get {
				return sell;
			}
			set {
				if (sell != value) {
					sell= value;
					changed[OrderFilterProps.PROPERTY_3_SELL_ID] = true;
					if (SellChanged != null)
						SellChanged(this, new PropertyChangedEventArgs("Sell", value));
				}
			}
		}

		Boolean limit = true;
		public Boolean Limit
		{
			get {
				return limit;
			}
			set {
				if (limit != value) {
					limit= value;
					changed[OrderFilterProps.PROPERTY_4_LIMIT_ID] = true;
					if (LimitChanged != null)
						LimitChanged(this, new PropertyChangedEventArgs("Limit", value));
				}
			}
		}

		Boolean stop = true;
		public Boolean Stop
		{
			get {
				return stop;
			}
			set {
				if (stop != value) {
					stop= value;
					changed[OrderFilterProps.PROPERTY_5_STOP_ID] = true;
					if (StopChanged != null)
						StopChanged(this, new PropertyChangedEventArgs("Stop", value));
				}
			}
		}

		datetime from = datetime.MinValue;
		public datetime From
		{
			get {
				return from;
			}
			set {
				if (from != value) {
					from= value;
					changed[OrderFilterProps.PROPERTY_6_FROM_ID] = true;
					if (FromChanged != null)
						FromChanged(this, new PropertyChangedEventArgs("From", value));
				}
			}
		}

		datetime to = datetime.MaxValue;
		public datetime To
		{
			get {
				return to;
			}
			set {
				if (to != value) {
					to= value;
					changed[OrderFilterProps.PROPERTY_7_TO_ID] = true;
					if (ToChanged != null)
						ToChanged(this, new PropertyChangedEventArgs("To", value));
				}
			}
		}

		Boolean more;
		public Boolean More
		{
			get {
				return more;
			}
			set {
				if (more != value) {
					more= value;
					changed[OrderFilterProps.PROPERTY_8_MORE_ID] = true;
					if (MoreChanged != null)
						MoreChanged(this, new PropertyChangedEventArgs("More", value));
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
					changed[OrderFilterProps.PROPERTY_9_TICKET_ID] = true;
					if (TicketChanged != null)
						TicketChanged(this, new PropertyChangedEventArgs("Ticket", value));
				}
			}
		}

		String symbol;
		public String Symbol
		{
			get {
				return symbol;
			}
			set {
				if (symbol != value) {
					symbol= value;
					changed[OrderFilterProps.PROPERTY_10_SYMBOL_ID] = true;
					if (SymbolChanged != null)
						SymbolChanged(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
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
					changed[OrderFilterProps.PROPERTY_11_OPERATION_ID] = true;
					if (OperationChanged != null)
						OperationChanged(this, new PropertyChangedEventArgs("Operation", value));
				}
			}
		}

		String expert;
		public String Expert
		{
			get {
				return expert;
			}
			set {
				if (expert != value) {
					expert= value;
					changed[OrderFilterProps.PROPERTY_12_EXPERT_ID] = true;
					if (ExpertChanged != null)
						ExpertChanged(this, new PropertyChangedEventArgs("Expert", value));
				}
			}
		}

		Int32 magic = -1;
		public Int32 Magic
		{
			get {
				return magic;
			}
			set {
				if (magic != value) {
					magic= value;
					changed[OrderFilterProps.PROPERTY_13_MAGIC_ID] = true;
					if (MagicChanged != null)
						MagicChanged(this, new PropertyChangedEventArgs("Magic", value));
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
					changed[OrderFilterProps.PROPERTY_14_COMMENT_ID] = true;
					if (CommentChanged != null)
						CommentChanged(this, new PropertyChangedEventArgs("Comment", value));
				}
			}
		}

		SortedSet<String> hiddenColumns = new SortedSet<string>();
		public SortedSet<String> HiddenColumns
		{
			get {
				return hiddenColumns;
			}
			set {
				if (hiddenColumns != value) {
					hiddenColumns= value;
					changed[OrderFilterProps.PROPERTY_15_HIDDENCOLUMNS_ID] = true;
					if (HiddenColumnsChanged != null)
						HiddenColumnsChanged(this, new PropertyChangedEventArgs("HiddenColumns", value));
				}
			}
		}

		public abstract Object GroupField
		{
			get ;
			set ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderFilterProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrderFilterProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
