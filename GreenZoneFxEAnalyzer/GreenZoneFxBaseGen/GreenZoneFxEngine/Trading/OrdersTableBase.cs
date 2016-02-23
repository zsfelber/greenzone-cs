using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class OrdersTableProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_ORDERS_ID = 2;
		public const int PROPERTY_3_ORDERSHISTORY_ID = 3;
		public const int PROPERTY_4_ORDERSHISTORYETC_ID = 4;
		public const int PROPERTY_5_CHILDREN_ID = 5;
		public const int PROPERTY_6_CHILDRENVIEW_ID = 6;
		public const int PROPERTY_7_SCROLLEDBARTIME_ID = 7;
		public const int PROPERTY_8_CURSORPOSITION_ID = 8;
		public static bool RmiGetProperty(IOrdersTable controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case PROPERTY_2_ORDERS_ID:
					value = controller.Orders;
					return true;
				case PROPERTY_3_ORDERSHISTORY_ID:
					value = controller.OrdersHistory;
					return true;
				case PROPERTY_4_ORDERSHISTORYETC_ID:
					value = controller.OrdersHistoryEtc;
					return true;
				case PROPERTY_5_CHILDREN_ID:
					value = controller.Children;
					return true;
				case PROPERTY_6_CHILDRENVIEW_ID:
					value = controller.ChildrenView;
					return true;
				case PROPERTY_7_SCROLLEDBARTIME_ID:
					value = controller.ScrolledBarTime;
					return true;
				case PROPERTY_8_CURSORPOSITION_ID:
					value = controller.CursorPosition;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersTable controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_2_ORDERS_ID:
					controller.Orders = (List<ITradeOrder>) value;
					return true;
				case PROPERTY_3_ORDERSHISTORY_ID:
					controller.OrdersHistory = (List<IHistoryOrder>) value;
					return true;
				case PROPERTY_4_ORDERSHISTORYETC_ID:
					controller.OrdersHistoryEtc = (List<IHistoryOrderEtc>) value;
					return true;
				case PROPERTY_5_CHILDREN_ID:
					controller.Children = (List<IOrdersTable>) value;
					return true;
				case PROPERTY_6_CHILDRENVIEW_ID:
					controller.ChildrenView = (List<IOrdersHistoryView>) value;
					return true;
				case PROPERTY_7_SCROLLEDBARTIME_ID:
					controller.ScrolledBarTime = (datetime) value;
					return true;
				case PROPERTY_8_CURSORPOSITION_ID:
					controller.CursorPosition = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersTable controller, GreenRmiObjectBuffer buffer)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[OrdersTableProps.PROPERTY_1_ENVIRONMENT_ID];
		}

		public static void AddDependencies(IOrdersTable controller)
		{
			controller.Dependencies.Add(controller.Environment);
		}

		public static void SerializationRead(IOrdersTable controller, SerializationInfo info, StreamingContext context)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Orders = (List<ITradeOrder>) info.GetValue("Orders", typeof(List<ITradeOrder>));
			controller.OrdersHistory = (List<IHistoryOrder>) info.GetValue("OrdersHistory", typeof(List<IHistoryOrder>));
			controller.OrdersHistoryEtc = (List<IHistoryOrderEtc>) info.GetValue("OrdersHistoryEtc", typeof(List<IHistoryOrderEtc>));
			controller.Children = (List<IOrdersTable>) info.GetValue("Children", typeof(List<IOrdersTable>));
			controller.ChildrenView = (List<IOrdersHistoryView>) info.GetValue("ChildrenView", typeof(List<IOrdersHistoryView>));
			controller.ScrolledBarTime = (datetime) info.GetValue("ScrolledBarTime", typeof(datetime));
			controller.CursorPosition = (Int32) info.GetValue("CursorPosition", typeof(Int32));
		}

		public static void SerializationWrite(IOrdersTable controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Orders", controller.Orders);
			info.AddValue("OrdersHistory", controller.OrdersHistory);
			info.AddValue("OrdersHistoryEtc", controller.OrdersHistoryEtc);
			info.AddValue("Children", controller.Children);
			info.AddValue("ChildrenView", controller.ChildrenView);
			info.AddValue("ScrolledBarTime", controller.ScrolledBarTime);
			info.AddValue("CursorPosition", controller.CursorPosition);
		}

	}
	public abstract class OrdersTableBase : TradingConst, IOrdersTable
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler OrdersChanged;
		public event PropertyChangedEventHandler OrdersHistoryChanged;
		public event PropertyChangedEventHandler OrdersHistoryEtcChanged;
		public event PropertyChangedEventHandler ChildrenChanged;
		public event PropertyChangedEventHandler ChildrenViewChanged;
		public event PropertyChangedEventHandler ScrolledBarTimeChanged;
		public event PropertyChangedEventHandler CursorPositionChanged;

		public OrdersTableBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrdersTableProps.AddDependencies(this);
		}

		public OrdersTableBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersTableProps.Initialize(this, buffer);
			___initialized = true;
			OrdersTableProps.AddDependencies(this);
		}

		protected OrdersTableBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersTableProps.SerializationRead(this, info, context);
			___initialized = true;
			OrdersTableProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersTableProps.SerializationWrite(this, info, context);
		}

		IEnvironmentRuntime environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return environment;
			}
			set {
				if (!___initialized) {
					environment= value;
					changed[OrdersTableProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		List<ITradeOrder> orders;
		public List<ITradeOrder> Orders
		{
			get {
				return orders;
			}
			set {
				if (orders != value) {
					orders= value;
					changed[OrdersTableProps.PROPERTY_2_ORDERS_ID] = true;
					if (OrdersChanged != null)
						OrdersChanged(this, new PropertyChangedEventArgs("Orders", value));
				}
			}
		}

		List<IHistoryOrder> ordersHistory;
		public List<IHistoryOrder> OrdersHistory
		{
			get {
				return ordersHistory;
			}
			set {
				if (ordersHistory != value) {
					ordersHistory= value;
					changed[OrdersTableProps.PROPERTY_3_ORDERSHISTORY_ID] = true;
					if (OrdersHistoryChanged != null)
						OrdersHistoryChanged(this, new PropertyChangedEventArgs("OrdersHistory", value));
				}
			}
		}

		List<IHistoryOrderEtc> ordersHistoryEtc;
		public List<IHistoryOrderEtc> OrdersHistoryEtc
		{
			get {
				return ordersHistoryEtc;
			}
			set {
				if (ordersHistoryEtc != value) {
					ordersHistoryEtc= value;
					changed[OrdersTableProps.PROPERTY_4_ORDERSHISTORYETC_ID] = true;
					if (OrdersHistoryEtcChanged != null)
						OrdersHistoryEtcChanged(this, new PropertyChangedEventArgs("OrdersHistoryEtc", value));
				}
			}
		}

		List<IOrdersTable> children;
		public List<IOrdersTable> Children
		{
			get {
				return children;
			}
			set {
				if (children != value) {
					children= value;
					changed[OrdersTableProps.PROPERTY_5_CHILDREN_ID] = true;
					if (ChildrenChanged != null)
						ChildrenChanged(this, new PropertyChangedEventArgs("Children", value));
				}
			}
		}

		List<IOrdersHistoryView> childrenView;
		public List<IOrdersHistoryView> ChildrenView
		{
			get {
				return childrenView;
			}
			set {
				if (childrenView != value) {
					childrenView= value;
					changed[OrdersTableProps.PROPERTY_6_CHILDRENVIEW_ID] = true;
					if (ChildrenViewChanged != null)
						ChildrenViewChanged(this, new PropertyChangedEventArgs("ChildrenView", value));
				}
			}
		}

		datetime scrolledBarTime;
		public datetime ScrolledBarTime
		{
			get {
				return scrolledBarTime;
			}
			set {
				if (scrolledBarTime != value) {
					scrolledBarTime= value;
					changed[OrdersTableProps.PROPERTY_7_SCROLLEDBARTIME_ID] = true;
					if (ScrolledBarTimeChanged != null)
						ScrolledBarTimeChanged(this, new PropertyChangedEventArgs("ScrolledBarTime", value));
				}
			}
		}

		Int32 cursorPosition;
		public Int32 CursorPosition
		{
			get {
				return cursorPosition;
			}
			set {
				if (cursorPosition != value) {
					cursorPosition= value;
					changed[OrdersTableProps.PROPERTY_8_CURSORPOSITION_ID] = true;
					if (CursorPositionChanged != null)
						CursorPositionChanged(this, new PropertyChangedEventArgs("CursorPosition", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrdersTableProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrdersTableProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
