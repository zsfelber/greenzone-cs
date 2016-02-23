using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class OrdersHistoryViewProps
	{
		public const int PROPERTY_1_FILTEREDORDERS_ID = 1;
		public const int PROPERTY_2_PARENT_ID = 2;
		public const int PROPERTY_3_FILTER_ID = 3;
		public const int PROPERTY_4_BALANCEHISTASDARR_ID = 4;
		public const int PROPERTY_5_BALANCETIMEHISTASLARR_ID = 5;
		public const int PROPERTY_6_ISCURSORBARCONNECTED_ID = 6;
		public const int PROPERTY_7_SERIESRANGE_ID = 7;
		public const int PROPERTY_8_AUTOSERIESRANGE_ID = 8;
		public static bool RmiGetProperty(IOrdersHistoryView controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_FILTEREDORDERS_ID:
					value = controller.FilteredOrders;
					return true;
				case PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_3_FILTER_ID:
					value = controller.Filter;
					return true;
				case PROPERTY_4_BALANCEHISTASDARR_ID:
					value = controller.BalanceHistAsDArr;
					return true;
				case PROPERTY_5_BALANCETIMEHISTASLARR_ID:
					value = controller.BalanceTimeHistAsLArr;
					return true;
				case PROPERTY_6_ISCURSORBARCONNECTED_ID:
					value = controller.IsCursorBarConnected;
					return true;
				case PROPERTY_7_SERIESRANGE_ID:
					value = controller.SeriesRange;
					return true;
				case PROPERTY_8_AUTOSERIESRANGE_ID:
					value = controller.AutoSeriesRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersHistoryView controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_FILTEREDORDERS_ID:
					controller.FilteredOrders = (IList<IHistoryOrderEtc>) value;
					return true;
				case PROPERTY_4_BALANCEHISTASDARR_ID:
					controller.BalanceHistAsDArr = (IDArr) value;
					return true;
				case PROPERTY_5_BALANCETIMEHISTASLARR_ID:
					controller.BalanceTimeHistAsLArr = (ILArr) value;
					return true;
				case PROPERTY_6_ISCURSORBARCONNECTED_ID:
					controller.IsCursorBarConnected = (Boolean) value;
					return true;
				case PROPERTY_7_SERIESRANGE_ID:
					controller.SeriesRange = (SeriesRange) value;
					return true;
				case PROPERTY_8_AUTOSERIESRANGE_ID:
					controller.AutoSeriesRange = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersHistoryView controller, GreenRmiObjectBuffer buffer)
		{
			controller.Parent = (IOrdersTable) buffer.ChangedProps[OrdersHistoryViewProps.PROPERTY_2_PARENT_ID];
			controller.Filter = (IOrderFilter) buffer.ChangedProps[OrdersHistoryViewProps.PROPERTY_3_FILTER_ID];
		}

		public static void AddDependencies(IOrdersHistoryView controller)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.Filter);
		}

		public static void SerializationRead(IOrdersHistoryView controller, SerializationInfo info, StreamingContext context)
		{
			controller.FilteredOrders = (IList<IHistoryOrderEtc>) info.GetValue("FilteredOrders", typeof(IList<IHistoryOrderEtc>));
			controller.Parent = (IOrdersTable) info.GetValue("Parent", typeof(IOrdersTable));
			controller.Filter = (IOrderFilter) info.GetValue("Filter", typeof(IOrderFilter));
			controller.BalanceHistAsDArr = (IDArr) info.GetValue("BalanceHistAsDArr", typeof(IDArr));
			controller.BalanceTimeHistAsLArr = (ILArr) info.GetValue("BalanceTimeHistAsLArr", typeof(ILArr));
			controller.IsCursorBarConnected = (Boolean) info.GetValue("IsCursorBarConnected", typeof(Boolean));
			controller.SeriesRange = (SeriesRange) info.GetValue("SeriesRange", typeof(SeriesRange));
			controller.AutoSeriesRange = (Boolean) info.GetValue("AutoSeriesRange", typeof(Boolean));
		}

		public static void SerializationWrite(IOrdersHistoryView controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("FilteredOrders", controller.FilteredOrders);
			info.AddValue("Parent", controller.Parent);
			info.AddValue("Filter", controller.Filter);
			info.AddValue("BalanceHistAsDArr", controller.BalanceHistAsDArr);
			info.AddValue("BalanceTimeHistAsLArr", controller.BalanceTimeHistAsLArr);
			info.AddValue("IsCursorBarConnected", controller.IsCursorBarConnected);
			info.AddValue("SeriesRange", controller.SeriesRange);
			info.AddValue("AutoSeriesRange", controller.AutoSeriesRange);
		}

	}
	public abstract class OrdersHistoryViewBase : RmiBase, IOrdersHistoryView
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler FilteredOrdersChanged;
		public event PropertyChangedEventHandler BalanceHistAsDArrChanged;
		public event PropertyChangedEventHandler BalanceTimeHistAsLArrChanged;
		public event PropertyChangedEventHandler IsCursorBarConnectedChanged;
		public event PropertyChangedEventHandler SeriesRangeChanged;
		public event PropertyChangedEventHandler AutoSeriesRangeChanged;

		public OrdersHistoryViewBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrdersHistoryViewProps.AddDependencies(this);
		}

		public OrdersHistoryViewBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersHistoryViewProps.Initialize(this, buffer);
			___initialized = true;
			OrdersHistoryViewProps.AddDependencies(this);
		}

		protected OrdersHistoryViewBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersHistoryViewProps.SerializationRead(this, info, context);
			___initialized = true;
			OrdersHistoryViewProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersHistoryViewProps.SerializationWrite(this, info, context);
		}

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		IList<IHistoryOrderEtc> filteredOrders;
		public IList<IHistoryOrderEtc> FilteredOrders
		{
			get {
				return filteredOrders;
			}
			set {
				if (filteredOrders != value) {
					filteredOrders= value;
					changed[OrdersHistoryViewProps.PROPERTY_1_FILTEREDORDERS_ID] = true;
					if (FilteredOrdersChanged != null)
						FilteredOrdersChanged(this, new PropertyChangedEventArgs("FilteredOrders", value));
				}
			}
		}

		IOrdersTable parent;
		public IOrdersTable Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[OrdersHistoryViewProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrderFilter filter;
		public IOrderFilter Filter
		{
			get {
				return filter;
			}
			set {
				if (!___initialized) {
					filter= value;
					changed[OrdersHistoryViewProps.PROPERTY_3_FILTER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IDArr balanceHistAsDArr;
		public IDArr BalanceHistAsDArr
		{
			get {
				return balanceHistAsDArr;
			}
			set {
				if (balanceHistAsDArr != value) {
					balanceHistAsDArr= value;
					changed[OrdersHistoryViewProps.PROPERTY_4_BALANCEHISTASDARR_ID] = true;
					if (BalanceHistAsDArrChanged != null)
						BalanceHistAsDArrChanged(this, new PropertyChangedEventArgs("BalanceHistAsDArr", value));
				}
			}
		}

		ILArr balanceTimeHistAsLArr;
		public ILArr BalanceTimeHistAsLArr
		{
			get {
				return balanceTimeHistAsLArr;
			}
			set {
				if (balanceTimeHistAsLArr != value) {
					balanceTimeHistAsLArr= value;
					changed[OrdersHistoryViewProps.PROPERTY_5_BALANCETIMEHISTASLARR_ID] = true;
					if (BalanceTimeHistAsLArrChanged != null)
						BalanceTimeHistAsLArrChanged(this, new PropertyChangedEventArgs("BalanceTimeHistAsLArr", value));
				}
			}
		}

		public abstract Double Point
		{
			get ;
		}

		public abstract Int32 Digits
		{
			get ;
		}

		public abstract String SymbolFormat
		{
			get ;
		}

		public abstract datetime ScrolledBarTime
		{
			get ;
			set ;
		}

		public abstract datetime ParentScrolledBarTime
		{
			get ;
			set ;
		}

		Boolean isCursorBarConnected;
		public Boolean IsCursorBarConnected
		{
			get {
				return isCursorBarConnected;
			}
			set {
				if (isCursorBarConnected != value) {
					isCursorBarConnected= value;
					changed[OrdersHistoryViewProps.PROPERTY_6_ISCURSORBARCONNECTED_ID] = true;
					if (IsCursorBarConnectedChanged != null)
						IsCursorBarConnectedChanged(this, new PropertyChangedEventArgs("IsCursorBarConnected", value));
				}
			}
		}

		public abstract Int32 ParentCursorPosition
		{
			get ;
			set ;
		}

		public abstract Int32 CursorPosition
		{
			get ;
			set ;
		}

		SeriesRange seriesRange;
		public SeriesRange SeriesRange
		{
			get {
				return seriesRange;
			}
			set {
				if (seriesRange != value) {
					seriesRange= value;
					changed[OrdersHistoryViewProps.PROPERTY_7_SERIESRANGE_ID] = true;
					if (SeriesRangeChanged != null)
						SeriesRangeChanged(this, new PropertyChangedEventArgs("SeriesRange", value));
				}
			}
		}

		Boolean autoSeriesRange;
		public Boolean AutoSeriesRange
		{
			get {
				return autoSeriesRange;
			}
			set {
				if (autoSeriesRange != value) {
					autoSeriesRange= value;
					changed[OrdersHistoryViewProps.PROPERTY_8_AUTOSERIESRANGE_ID] = true;
					if (AutoSeriesRangeChanged != null)
						AutoSeriesRangeChanged(this, new PropertyChangedEventArgs("AutoSeriesRange", value));
				}
			}
		}

		public abstract Int64 RecordCount
		{
			get ;
		}

		public abstract Int64 TotalFileOffset
		{
			get ;
		}

		public abstract datetime From
		{
			get ;
		}

		public abstract datetime To
		{
			get ;
		}

		public abstract ILArr sLTime
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrdersHistoryViewProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrdersHistoryViewProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
