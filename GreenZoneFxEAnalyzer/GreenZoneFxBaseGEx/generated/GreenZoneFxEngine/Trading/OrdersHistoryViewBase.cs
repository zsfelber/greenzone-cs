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
		public static bool RmiGetProperty(IOrdersHistoryView controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrdersHistoryViewProps.PROPERTY_1_FILTEREDORDERS_ID:
					value = controller.FilteredOrders;
					return true;
				case OrdersHistoryViewProps.PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case OrdersHistoryViewProps.PROPERTY_3_FILTER_ID:
					value = controller.Filter;
					return true;
				case OrdersHistoryViewProps.PROPERTY_4_BALANCEHISTASDARR_ID:
					value = controller.BalanceHistAsDArr;
					return true;
				case OrdersHistoryViewProps.PROPERTY_5_BALANCETIMEHISTASLARR_ID:
					value = controller.BalanceTimeHistAsLArr;
					return true;
				case OrdersHistoryViewProps.PROPERTY_6_ISCURSORBARCONNECTED_ID:
					value = controller.IsCursorBarConnected;
					return true;
				case OrdersHistoryViewProps.PROPERTY_7_SERIESRANGE_ID:
					value = controller.SeriesRange;
					return true;
				case OrdersHistoryViewProps.PROPERTY_8_AUTOSERIESRANGE_ID:
					value = controller.AutoSeriesRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersHistoryView controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrdersHistoryViewProps.PROPERTY_1_FILTEREDORDERS_ID:
					controller.FilteredOrders = (IList<IHistoryOrderEtc>) value;
					return true;
				case OrdersHistoryViewProps.PROPERTY_4_BALANCEHISTASDARR_ID:
					controller.BalanceHistAsDArr = (DArr) value;
					return true;
				case OrdersHistoryViewProps.PROPERTY_5_BALANCETIMEHISTASLARR_ID:
					controller.BalanceTimeHistAsLArr = (LArr) value;
					return true;
				case OrdersHistoryViewProps.PROPERTY_6_ISCURSORBARCONNECTED_ID:
					controller.IsCursorBarConnected = (Boolean) value;
					return true;
				case OrdersHistoryViewProps.PROPERTY_7_SERIESRANGE_ID:
					controller.SeriesRange = (SeriesRange) value;
					return true;
				case OrdersHistoryViewProps.PROPERTY_8_AUTOSERIESRANGE_ID:
					controller.AutoSeriesRange = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersHistoryView controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Parent = (IOrdersTable) buffer.ChangedProps[OrdersHistoryViewProps.PROPERTY_2_PARENT_ID];
			controller.Filter = (IOrderFilter) buffer.ChangedProps[OrdersHistoryViewProps.PROPERTY_3_FILTER_ID];
		}

		public static void AddDependencies(IOrdersHistoryView controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.Filter);
		}

		public static void SerializationRead(IOrdersHistoryView controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.FilteredOrders = (IList<IHistoryOrderEtc>) info.GetValue("FilteredOrders", typeof(IList<IHistoryOrderEtc>));
			controller.Parent = (IOrdersTable) info.GetValue("Parent", typeof(IOrdersTable));
			controller.Filter = (IOrderFilter) info.GetValue("Filter", typeof(IOrderFilter));
			controller.BalanceHistAsDArr = (DArr) info.GetValue("BalanceHistAsDArr", typeof(DArr));
			controller.BalanceTimeHistAsLArr = (LArr) info.GetValue("BalanceTimeHistAsLArr", typeof(LArr));
			controller.IsCursorBarConnected = (Boolean) info.GetValue("IsCursorBarConnected", typeof(Boolean));
			controller.SeriesRange = (SeriesRange) info.GetValue("SeriesRange", typeof(SeriesRange));
			controller.AutoSeriesRange = (Boolean) info.GetValue("AutoSeriesRange", typeof(Boolean));
		}

		public static void SerializationWrite(IOrdersHistoryView controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IOrdersHistoryView_FilteredOrders_Changed;
		public event PropertyChangedEventHandler IOrdersHistoryView_BalanceHistAsDArr_Changed;
		public event PropertyChangedEventHandler IOrdersHistoryView_BalanceTimeHistAsLArr_Changed;
		public event PropertyChangedEventHandler IOrdersHistoryView_IsCursorBarConnected_Changed;
		public event PropertyChangedEventHandler IOrdersHistoryView_SeriesRange_Changed;
		public event PropertyChangedEventHandler IOrdersHistoryView_AutoSeriesRange_Changed;

		public OrdersHistoryViewBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			OrdersHistoryViewProps.AddDependencies(this, false);
		}

		public OrdersHistoryViewBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersHistoryViewProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersHistoryViewProps.AddDependencies(this, false);
		}

		protected OrdersHistoryViewBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersHistoryViewProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersHistoryViewProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersHistoryViewProps.SerializationWrite(this, info, context, false);
		}

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		public abstract void ApplyFilters(IHistoryOrderEtc newOrder);


		IList<IHistoryOrderEtc> _IOrdersHistoryView_FilteredOrders;
		public IList<IHistoryOrderEtc> FilteredOrders
		{
			get {
				return _IOrdersHistoryView_FilteredOrders;
			}
			set {
				if (_IOrdersHistoryView_FilteredOrders != value) {
					_IOrdersHistoryView_FilteredOrders= value;
					changed[OrdersHistoryViewProps.PROPERTY_1_FILTEREDORDERS_ID] = true;
					if (IOrdersHistoryView_FilteredOrders_Changed != null)
						IOrdersHistoryView_FilteredOrders_Changed(this, new PropertyChangedEventArgs("FilteredOrders", value));
				}
			}
		}

		IOrdersTable _IOrdersHistoryView_Parent;
		public IOrdersTable Parent
		{
			get {
				return _IOrdersHistoryView_Parent;
			}
			set {
				if (!___initialized) {
					_IOrdersHistoryView_Parent= value;
					changed[OrdersHistoryViewProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrderFilter _IOrdersHistoryView_Filter;
		public IOrderFilter Filter
		{
			get {
				return _IOrdersHistoryView_Filter;
			}
			set {
				if (!___initialized) {
					_IOrdersHistoryView_Filter= value;
					changed[OrdersHistoryViewProps.PROPERTY_3_FILTER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		DArr _IOrdersHistoryView_BalanceHistAsDArr;
		public DArr BalanceHistAsDArr
		{
			get {
				return _IOrdersHistoryView_BalanceHistAsDArr;
			}
			set {
				if (_IOrdersHistoryView_BalanceHistAsDArr != value) {
					_IOrdersHistoryView_BalanceHistAsDArr= value;
					changed[OrdersHistoryViewProps.PROPERTY_4_BALANCEHISTASDARR_ID] = true;
					if (IOrdersHistoryView_BalanceHistAsDArr_Changed != null)
						IOrdersHistoryView_BalanceHistAsDArr_Changed(this, new PropertyChangedEventArgs("BalanceHistAsDArr", value));
				}
			}
		}

		LArr _IOrdersHistoryView_BalanceTimeHistAsLArr;
		public LArr BalanceTimeHistAsLArr
		{
			get {
				return _IOrdersHistoryView_BalanceTimeHistAsLArr;
			}
			set {
				if (_IOrdersHistoryView_BalanceTimeHistAsLArr != value) {
					_IOrdersHistoryView_BalanceTimeHistAsLArr= value;
					changed[OrdersHistoryViewProps.PROPERTY_5_BALANCETIMEHISTASLARR_ID] = true;
					if (IOrdersHistoryView_BalanceTimeHistAsLArr_Changed != null)
						IOrdersHistoryView_BalanceTimeHistAsLArr_Changed(this, new PropertyChangedEventArgs("BalanceTimeHistAsLArr", value));
				}
			}
		}

		public abstract IEnumerable<Object> Groups
		{
			get ;
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

		Boolean _IOrdersHistoryView_IsCursorBarConnected;
		public Boolean IsCursorBarConnected
		{
			get {
				return _IOrdersHistoryView_IsCursorBarConnected;
			}
			set {
				if (_IOrdersHistoryView_IsCursorBarConnected != value) {
					_IOrdersHistoryView_IsCursorBarConnected= value;
					changed[OrdersHistoryViewProps.PROPERTY_6_ISCURSORBARCONNECTED_ID] = true;
					if (IOrdersHistoryView_IsCursorBarConnected_Changed != null)
						IOrdersHistoryView_IsCursorBarConnected_Changed(this, new PropertyChangedEventArgs("IsCursorBarConnected", value));
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

		SeriesRange _IOrdersHistoryView_SeriesRange;
		public SeriesRange SeriesRange
		{
			get {
				return _IOrdersHistoryView_SeriesRange;
			}
			set {
				if (_IOrdersHistoryView_SeriesRange != value) {
					_IOrdersHistoryView_SeriesRange= value;
					changed[OrdersHistoryViewProps.PROPERTY_7_SERIESRANGE_ID] = true;
					if (IOrdersHistoryView_SeriesRange_Changed != null)
						IOrdersHistoryView_SeriesRange_Changed(this, new PropertyChangedEventArgs("SeriesRange", value));
				}
			}
		}

		Boolean _IOrdersHistoryView_AutoSeriesRange;
		public Boolean AutoSeriesRange
		{
			get {
				return _IOrdersHistoryView_AutoSeriesRange;
			}
			set {
				if (_IOrdersHistoryView_AutoSeriesRange != value) {
					_IOrdersHistoryView_AutoSeriesRange= value;
					changed[OrdersHistoryViewProps.PROPERTY_8_AUTOSERIESRANGE_ID] = true;
					if (IOrdersHistoryView_AutoSeriesRange_Changed != null)
						IOrdersHistoryView_AutoSeriesRange_Changed(this, new PropertyChangedEventArgs("AutoSeriesRange", value));
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

		public abstract LArr sLTime
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrdersHistoryViewProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersHistoryViewProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
