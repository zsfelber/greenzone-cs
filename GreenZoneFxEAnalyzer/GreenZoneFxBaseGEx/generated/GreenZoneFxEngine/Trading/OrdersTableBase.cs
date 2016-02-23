using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class OrdersTableProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_PARENT_ID = 2;
		public const int PROPERTY_3_SYMBOLRUNTIME_ID = 3;
		public const int PROPERTY_4_ORDERS_ID = 4;
		public const int PROPERTY_5_ORDERSHISTORY_ID = 5;
		public const int PROPERTY_6_ORDERSHISTORYETC_ID = 6;
		public const int PROPERTY_7_CHILDREN_ID = 7;
		public const int PROPERTY_8_CHILDRENVIEW_ID = 8;
		public const int PROPERTY_9_CURSORPOSITION_ID = 9;
		public static bool RmiGetProperty(IOrdersTable controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrdersTableProps.PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case OrdersTableProps.PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case OrdersTableProps.PROPERTY_3_SYMBOLRUNTIME_ID:
					value = controller.SymbolRuntime;
					return true;
				case OrdersTableProps.PROPERTY_4_ORDERS_ID:
					value = controller.Orders;
					return true;
				case OrdersTableProps.PROPERTY_5_ORDERSHISTORY_ID:
					value = controller.OrdersHistory;
					return true;
				case OrdersTableProps.PROPERTY_6_ORDERSHISTORYETC_ID:
					value = controller.OrdersHistoryEtc;
					return true;
				case OrdersTableProps.PROPERTY_7_CHILDREN_ID:
					value = controller.Children;
					return true;
				case OrdersTableProps.PROPERTY_8_CHILDRENVIEW_ID:
					value = controller.ChildrenView;
					return true;
				case OrdersTableProps.PROPERTY_9_CURSORPOSITION_ID:
					value = controller.CursorPosition;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersTable controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrdersTableProps.PROPERTY_4_ORDERS_ID:
					controller.Orders = (List<ITradeOrder>) value;
					return true;
				case OrdersTableProps.PROPERTY_5_ORDERSHISTORY_ID:
					controller.OrdersHistory = (List<IHistoryOrder>) value;
					return true;
				case OrdersTableProps.PROPERTY_6_ORDERSHISTORYETC_ID:
					controller.OrdersHistoryEtc = (List<IHistoryOrderEtc>) value;
					return true;
				case OrdersTableProps.PROPERTY_7_CHILDREN_ID:
					controller.Children = (List<IOrdersTable>) value;
					return true;
				case OrdersTableProps.PROPERTY_8_CHILDRENVIEW_ID:
					controller.ChildrenView = (List<IOrdersHistoryView>) value;
					return true;
				case OrdersTableProps.PROPERTY_9_CURSORPOSITION_ID:
					controller.CursorPosition = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersTable controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[OrdersTableProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Parent = (IOrdersTable) buffer.ChangedProps[OrdersTableProps.PROPERTY_2_PARENT_ID];
			controller.SymbolRuntime = (ISymbolRuntime) buffer.ChangedProps[OrdersTableProps.PROPERTY_3_SYMBOLRUNTIME_ID];
		}

		public static void AddDependencies(IOrdersTable controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.SymbolRuntime);
		}

		public static void SerializationRead(IOrdersTable controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Parent = (IOrdersTable) info.GetValue("Parent", typeof(IOrdersTable));
			controller.SymbolRuntime = (ISymbolRuntime) info.GetValue("SymbolRuntime", typeof(ISymbolRuntime));
			controller.Orders = (List<ITradeOrder>) info.GetValue("Orders", typeof(List<ITradeOrder>));
			controller.OrdersHistory = (List<IHistoryOrder>) info.GetValue("OrdersHistory", typeof(List<IHistoryOrder>));
			controller.OrdersHistoryEtc = (List<IHistoryOrderEtc>) info.GetValue("OrdersHistoryEtc", typeof(List<IHistoryOrderEtc>));
			controller.Children = (List<IOrdersTable>) info.GetValue("Children", typeof(List<IOrdersTable>));
			controller.ChildrenView = (List<IOrdersHistoryView>) info.GetValue("ChildrenView", typeof(List<IOrdersHistoryView>));
			controller.CursorPosition = (Int32) info.GetValue("CursorPosition", typeof(Int32));
		}

		public static void SerializationWrite(IOrdersTable controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SymbolRuntime", controller.SymbolRuntime);
			info.AddValue("Orders", controller.Orders);
			info.AddValue("OrdersHistory", controller.OrdersHistory);
			info.AddValue("OrdersHistoryEtc", controller.OrdersHistoryEtc);
			info.AddValue("Children", controller.Children);
			info.AddValue("ChildrenView", controller.ChildrenView);
			info.AddValue("CursorPosition", controller.CursorPosition);
		}

	}
	public abstract class OrdersTableBase : TradingConst, IOrdersTable
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrdersTable_Orders_Changed;
		public event PropertyChangedEventHandler IOrdersTable_OrdersHistory_Changed;
		public event PropertyChangedEventHandler IOrdersTable_OrdersHistoryEtc_Changed;
		public event PropertyChangedEventHandler IOrdersTable_Children_Changed;
		public event PropertyChangedEventHandler IOrdersTable_ChildrenView_Changed;
		public event PropertyChangedEventHandler IOrdersTable_CursorPosition_Changed;

		public OrdersTableBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrdersTableProps.AddDependencies(this, false);
		}

		public OrdersTableBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersTableProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersTableProps.AddDependencies(this, false);
		}

		protected OrdersTableBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersTableProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersTableProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersTableProps.SerializationWrite(this, info, context, false);
		}

		public abstract IOrder Select(Int32 index, TradeSelectMode select, TradePool pool);

		public abstract O Select<O>(Int32 index, TradeSelectMode select, TradePool pool)
			where O : IOrder;

		public abstract IOrder Add(symbol symbol, OrderType cmd, Double volume, Double price, Int32 slippage, Double stoploss, Double takeprofit, String comment, Int32 magic, datetime expirationDatetime, Color arrow_color);

		public abstract IOrder CloseOrDelete(Int32 ticket, OrderTypeKind preferredKind, Color arrow_color);

		public abstract Boolean Modify(ITradeOrder _order, Double price, Double stoploss, Double takeprofit, datetime expiration, Color color);


		IEnvironmentRuntime _IOrdersTable_Environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return _IOrdersTable_Environment;
			}
			set {
				if (!___initialized) {
					_IOrdersTable_Environment= value;
					changed[OrdersTableProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTable _IOrdersTable_Parent;
		public IOrdersTable Parent
		{
			get {
				return _IOrdersTable_Parent;
			}
			set {
				if (!___initialized) {
					_IOrdersTable_Parent= value;
					changed[OrdersTableProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime _IOrdersTable_SymbolRuntime;
		public ISymbolRuntime SymbolRuntime
		{
			get {
				return _IOrdersTable_SymbolRuntime;
			}
			set {
				if (!___initialized) {
					_IOrdersTable_SymbolRuntime= value;
					changed[OrdersTableProps.PROPERTY_3_SYMBOLRUNTIME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual ISymbolContext SymbolContext
		{
			get {
				return SymbolRuntime.Context;
			}
		}

		List<ITradeOrder> _IOrdersTable_Orders;
		public List<ITradeOrder> Orders
		{
			get {
				return _IOrdersTable_Orders;
			}
			set {
				if (_IOrdersTable_Orders != value) {
					_IOrdersTable_Orders= value;
					changed[OrdersTableProps.PROPERTY_4_ORDERS_ID] = true;
					if (IOrdersTable_Orders_Changed != null)
						IOrdersTable_Orders_Changed(this, new PropertyChangedEventArgs("Orders", value));
				}
			}
		}

		List<IHistoryOrder> _IOrdersTable_OrdersHistory;
		public List<IHistoryOrder> OrdersHistory
		{
			get {
				return _IOrdersTable_OrdersHistory;
			}
			set {
				if (_IOrdersTable_OrdersHistory != value) {
					_IOrdersTable_OrdersHistory= value;
					changed[OrdersTableProps.PROPERTY_5_ORDERSHISTORY_ID] = true;
					if (IOrdersTable_OrdersHistory_Changed != null)
						IOrdersTable_OrdersHistory_Changed(this, new PropertyChangedEventArgs("OrdersHistory", value));
				}
			}
		}

		List<IHistoryOrderEtc> _IOrdersTable_OrdersHistoryEtc;
		public List<IHistoryOrderEtc> OrdersHistoryEtc
		{
			get {
				return _IOrdersTable_OrdersHistoryEtc;
			}
			set {
				if (_IOrdersTable_OrdersHistoryEtc != value) {
					_IOrdersTable_OrdersHistoryEtc= value;
					changed[OrdersTableProps.PROPERTY_6_ORDERSHISTORYETC_ID] = true;
					if (IOrdersTable_OrdersHistoryEtc_Changed != null)
						IOrdersTable_OrdersHistoryEtc_Changed(this, new PropertyChangedEventArgs("OrdersHistoryEtc", value));
				}
			}
		}

		List<IOrdersTable> _IOrdersTable_Children;
		public List<IOrdersTable> Children
		{
			get {
				return _IOrdersTable_Children;
			}
			set {
				if (_IOrdersTable_Children != value) {
					_IOrdersTable_Children= value;
					changed[OrdersTableProps.PROPERTY_7_CHILDREN_ID] = true;
					if (IOrdersTable_Children_Changed != null)
						IOrdersTable_Children_Changed(this, new PropertyChangedEventArgs("Children", value));
				}
			}
		}

		List<IOrdersHistoryView> _IOrdersTable_ChildrenView;
		public List<IOrdersHistoryView> ChildrenView
		{
			get {
				return _IOrdersTable_ChildrenView;
			}
			set {
				if (_IOrdersTable_ChildrenView != value) {
					_IOrdersTable_ChildrenView= value;
					changed[OrdersTableProps.PROPERTY_8_CHILDRENVIEW_ID] = true;
					if (IOrdersTable_ChildrenView_Changed != null)
						IOrdersTable_ChildrenView_Changed(this, new PropertyChangedEventArgs("ChildrenView", value));
				}
			}
		}

		public abstract datetime ScrolledBarTime
		{
			get ;
			set ;
		}

		Int32 _IOrdersTable_CursorPosition;
		public Int32 CursorPosition
		{
			get {
				return _IOrdersTable_CursorPosition;
			}
			set {
				if (_IOrdersTable_CursorPosition != value) {
					_IOrdersTable_CursorPosition= value;
					changed[OrdersTableProps.PROPERTY_9_CURSORPOSITION_ID] = true;
					if (IOrdersTable_CursorPosition_Changed != null)
						IOrdersTable_CursorPosition_Changed(this, new PropertyChangedEventArgs("CursorPosition", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrdersTableProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersTableProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
