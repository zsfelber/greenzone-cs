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
		public static bool RmiGetProperty(IOrderFilter controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrderFilterProps.PROPERTY_1_GROUPBY_ID:
					value = controller.GroupBy;
					return true;
				case OrderFilterProps.PROPERTY_2_BUY_ID:
					value = controller.Buy;
					return true;
				case OrderFilterProps.PROPERTY_3_SELL_ID:
					value = controller.Sell;
					return true;
				case OrderFilterProps.PROPERTY_4_LIMIT_ID:
					value = controller.Limit;
					return true;
				case OrderFilterProps.PROPERTY_5_STOP_ID:
					value = controller.Stop;
					return true;
				case OrderFilterProps.PROPERTY_6_FROM_ID:
					value = controller.From;
					return true;
				case OrderFilterProps.PROPERTY_7_TO_ID:
					value = controller.To;
					return true;
				case OrderFilterProps.PROPERTY_8_MORE_ID:
					value = controller.More;
					return true;
				case OrderFilterProps.PROPERTY_9_TICKET_ID:
					value = controller.Ticket;
					return true;
				case OrderFilterProps.PROPERTY_10_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case OrderFilterProps.PROPERTY_11_OPERATION_ID:
					value = controller.Operation;
					return true;
				case OrderFilterProps.PROPERTY_12_EXPERT_ID:
					value = controller.Expert;
					return true;
				case OrderFilterProps.PROPERTY_13_MAGIC_ID:
					value = controller.Magic;
					return true;
				case OrderFilterProps.PROPERTY_14_COMMENT_ID:
					value = controller.Comment;
					return true;
				case OrderFilterProps.PROPERTY_15_HIDDENCOLUMNS_ID:
					value = controller.HiddenColumns;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderFilter controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrderFilterProps.PROPERTY_1_GROUPBY_ID:
					controller.GroupBy = (String) value;
					return true;
				case OrderFilterProps.PROPERTY_2_BUY_ID:
					controller.Buy = (Boolean) value;
					return true;
				case OrderFilterProps.PROPERTY_3_SELL_ID:
					controller.Sell = (Boolean) value;
					return true;
				case OrderFilterProps.PROPERTY_4_LIMIT_ID:
					controller.Limit = (Boolean) value;
					return true;
				case OrderFilterProps.PROPERTY_5_STOP_ID:
					controller.Stop = (Boolean) value;
					return true;
				case OrderFilterProps.PROPERTY_6_FROM_ID:
					controller.From = (datetime) value;
					return true;
				case OrderFilterProps.PROPERTY_7_TO_ID:
					controller.To = (datetime) value;
					return true;
				case OrderFilterProps.PROPERTY_8_MORE_ID:
					controller.More = (Boolean) value;
					return true;
				case OrderFilterProps.PROPERTY_9_TICKET_ID:
					controller.Ticket = (Int32) value;
					return true;
				case OrderFilterProps.PROPERTY_10_SYMBOL_ID:
					controller.Symbol = (String) value;
					return true;
				case OrderFilterProps.PROPERTY_11_OPERATION_ID:
					controller.Operation = (TradeOperation) value;
					return true;
				case OrderFilterProps.PROPERTY_12_EXPERT_ID:
					controller.Expert = (String) value;
					return true;
				case OrderFilterProps.PROPERTY_13_MAGIC_ID:
					controller.Magic = (Int32) value;
					return true;
				case OrderFilterProps.PROPERTY_14_COMMENT_ID:
					controller.Comment = (String) value;
					return true;
				case OrderFilterProps.PROPERTY_15_HIDDENCOLUMNS_ID:
					controller.HiddenColumns = (SortedSet<String>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrderFilter controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IOrderFilter controller, bool goToParent)
		{
		}

		public static void SerializationRead(IOrderFilter controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IOrderFilter controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IOrderFilter_GroupBy_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Buy_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Sell_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Limit_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Stop_Changed;
		public event PropertyChangedEventHandler IOrderFilter_From_Changed;
		public event PropertyChangedEventHandler IOrderFilter_To_Changed;
		public event PropertyChangedEventHandler IOrderFilter_More_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Ticket_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Symbol_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Operation_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Expert_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Magic_Changed;
		public event PropertyChangedEventHandler IOrderFilter_Comment_Changed;
		public event PropertyChangedEventHandler IOrderFilter_HiddenColumns_Changed;

		public OrderFilterBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrderFilterProps.AddDependencies(this, false);
		}

		public OrderFilterBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderFilterProps.Initialize(this, buffer, false);
			___initialized = true;
			OrderFilterProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected OrderFilterBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderFilterProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrderFilterProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderFilterProps.SerializationWrite(this, info, context, false);
		}


		String _IOrderFilter_GroupBy = "";
		public String GroupBy
		{
			get {
				return _IOrderFilter_GroupBy;
			}
			set {
				if (_IOrderFilter_GroupBy != value) {
					_IOrderFilter_GroupBy= value;
					changed[OrderFilterProps.PROPERTY_1_GROUPBY_ID] = true;
					if (IOrderFilter_GroupBy_Changed != null)
						IOrderFilter_GroupBy_Changed(this, new PropertyChangedEventArgs("GroupBy", value));
				}
			}
		}

		Boolean _IOrderFilter_Buy = true;
		public Boolean Buy
		{
			get {
				return _IOrderFilter_Buy;
			}
			set {
				if (_IOrderFilter_Buy != value) {
					_IOrderFilter_Buy= value;
					changed[OrderFilterProps.PROPERTY_2_BUY_ID] = true;
					if (IOrderFilter_Buy_Changed != null)
						IOrderFilter_Buy_Changed(this, new PropertyChangedEventArgs("Buy", value));
				}
			}
		}

		Boolean _IOrderFilter_Sell = true;
		public Boolean Sell
		{
			get {
				return _IOrderFilter_Sell;
			}
			set {
				if (_IOrderFilter_Sell != value) {
					_IOrderFilter_Sell= value;
					changed[OrderFilterProps.PROPERTY_3_SELL_ID] = true;
					if (IOrderFilter_Sell_Changed != null)
						IOrderFilter_Sell_Changed(this, new PropertyChangedEventArgs("Sell", value));
				}
			}
		}

		Boolean _IOrderFilter_Limit = true;
		public Boolean Limit
		{
			get {
				return _IOrderFilter_Limit;
			}
			set {
				if (_IOrderFilter_Limit != value) {
					_IOrderFilter_Limit= value;
					changed[OrderFilterProps.PROPERTY_4_LIMIT_ID] = true;
					if (IOrderFilter_Limit_Changed != null)
						IOrderFilter_Limit_Changed(this, new PropertyChangedEventArgs("Limit", value));
				}
			}
		}

		Boolean _IOrderFilter_Stop = true;
		public Boolean Stop
		{
			get {
				return _IOrderFilter_Stop;
			}
			set {
				if (_IOrderFilter_Stop != value) {
					_IOrderFilter_Stop= value;
					changed[OrderFilterProps.PROPERTY_5_STOP_ID] = true;
					if (IOrderFilter_Stop_Changed != null)
						IOrderFilter_Stop_Changed(this, new PropertyChangedEventArgs("Stop", value));
				}
			}
		}

		datetime _IOrderFilter_From = datetime.MinValue;
		public datetime From
		{
			get {
				return _IOrderFilter_From;
			}
			set {
				if (_IOrderFilter_From != value) {
					_IOrderFilter_From= value;
					changed[OrderFilterProps.PROPERTY_6_FROM_ID] = true;
					if (IOrderFilter_From_Changed != null)
						IOrderFilter_From_Changed(this, new PropertyChangedEventArgs("From", value));
				}
			}
		}

		datetime _IOrderFilter_To = datetime.MaxValue;
		public datetime To
		{
			get {
				return _IOrderFilter_To;
			}
			set {
				if (_IOrderFilter_To != value) {
					_IOrderFilter_To= value;
					changed[OrderFilterProps.PROPERTY_7_TO_ID] = true;
					if (IOrderFilter_To_Changed != null)
						IOrderFilter_To_Changed(this, new PropertyChangedEventArgs("To", value));
				}
			}
		}

		Boolean _IOrderFilter_More;
		public Boolean More
		{
			get {
				return _IOrderFilter_More;
			}
			set {
				if (_IOrderFilter_More != value) {
					_IOrderFilter_More= value;
					changed[OrderFilterProps.PROPERTY_8_MORE_ID] = true;
					if (IOrderFilter_More_Changed != null)
						IOrderFilter_More_Changed(this, new PropertyChangedEventArgs("More", value));
				}
			}
		}

		Int32 _IOrderFilter_Ticket;
		public Int32 Ticket
		{
			get {
				return _IOrderFilter_Ticket;
			}
			set {
				if (_IOrderFilter_Ticket != value) {
					_IOrderFilter_Ticket= value;
					changed[OrderFilterProps.PROPERTY_9_TICKET_ID] = true;
					if (IOrderFilter_Ticket_Changed != null)
						IOrderFilter_Ticket_Changed(this, new PropertyChangedEventArgs("Ticket", value));
				}
			}
		}

		String _IOrderFilter_Symbol;
		public String Symbol
		{
			get {
				return _IOrderFilter_Symbol;
			}
			set {
				if (_IOrderFilter_Symbol != value) {
					_IOrderFilter_Symbol= value;
					changed[OrderFilterProps.PROPERTY_10_SYMBOL_ID] = true;
					if (IOrderFilter_Symbol_Changed != null)
						IOrderFilter_Symbol_Changed(this, new PropertyChangedEventArgs("Symbol", value));
				}
			}
		}

		TradeOperation _IOrderFilter_Operation;
		public TradeOperation Operation
		{
			get {
				return _IOrderFilter_Operation;
			}
			set {
				if (_IOrderFilter_Operation != value) {
					_IOrderFilter_Operation= value;
					changed[OrderFilterProps.PROPERTY_11_OPERATION_ID] = true;
					if (IOrderFilter_Operation_Changed != null)
						IOrderFilter_Operation_Changed(this, new PropertyChangedEventArgs("Operation", value));
				}
			}
		}

		String _IOrderFilter_Expert;
		public String Expert
		{
			get {
				return _IOrderFilter_Expert;
			}
			set {
				if (_IOrderFilter_Expert != value) {
					_IOrderFilter_Expert= value;
					changed[OrderFilterProps.PROPERTY_12_EXPERT_ID] = true;
					if (IOrderFilter_Expert_Changed != null)
						IOrderFilter_Expert_Changed(this, new PropertyChangedEventArgs("Expert", value));
				}
			}
		}

		Int32 _IOrderFilter_Magic = -1;
		public Int32 Magic
		{
			get {
				return _IOrderFilter_Magic;
			}
			set {
				if (_IOrderFilter_Magic != value) {
					_IOrderFilter_Magic= value;
					changed[OrderFilterProps.PROPERTY_13_MAGIC_ID] = true;
					if (IOrderFilter_Magic_Changed != null)
						IOrderFilter_Magic_Changed(this, new PropertyChangedEventArgs("Magic", value));
				}
			}
		}

		String _IOrderFilter_Comment;
		public String Comment
		{
			get {
				return _IOrderFilter_Comment;
			}
			set {
				if (_IOrderFilter_Comment != value) {
					_IOrderFilter_Comment= value;
					changed[OrderFilterProps.PROPERTY_14_COMMENT_ID] = true;
					if (IOrderFilter_Comment_Changed != null)
						IOrderFilter_Comment_Changed(this, new PropertyChangedEventArgs("Comment", value));
				}
			}
		}

		SortedSet<String> _IOrderFilter_HiddenColumns = new SortedSet<string>();
		public SortedSet<String> HiddenColumns
		{
			get {
				return _IOrderFilter_HiddenColumns;
			}
			set {
				if (_IOrderFilter_HiddenColumns != value) {
					_IOrderFilter_HiddenColumns= value;
					changed[OrderFilterProps.PROPERTY_15_HIDDENCOLUMNS_ID] = true;
					if (IOrderFilter_HiddenColumns_Changed != null)
						IOrderFilter_HiddenColumns_Changed(this, new PropertyChangedEventArgs("HiddenColumns", value));
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
			if (OrderFilterProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrderFilterProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
