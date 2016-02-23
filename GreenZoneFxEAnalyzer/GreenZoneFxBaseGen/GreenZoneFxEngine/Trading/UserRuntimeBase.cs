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
	public static class UserRuntimeProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_PARENT_ID = 2;
		public const int PROPERTY_3_SERIESMANAGER_ID = 3;
		public const int PROPERTY_4_CACHE_ID = 4;
		public const int PROPERTY_5_TMPARRAYCACHES_ID = 5;
		public const int PROPERTY_6_INDICATORLASTOFFSET_ID = 6;
		public const int PROPERTY_7_LASTFILEOFFSET_ID = 7;
		public const int PROPERTY_8_LASTBUFFERLENGTH_ID = 8;
		public const int PROPERTY_9_UPDATESTATE_ID = 9;
		public const int PROPERTY_10_SELECTEDORDER_ID = 10;
		public static bool RmiGetProperty(IUserRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case PROPERTY_2_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_3_SERIESMANAGER_ID:
					value = controller.SeriesManager;
					return true;
				case PROPERTY_4_CACHE_ID:
					value = controller.Cache;
					return true;
				case PROPERTY_5_TMPARRAYCACHES_ID:
					value = controller.TmpArrayCaches;
					return true;
				case PROPERTY_6_INDICATORLASTOFFSET_ID:
					value = controller.IndicatorLastOffset;
					return true;
				case PROPERTY_7_LASTFILEOFFSET_ID:
					value = controller.LastFileOffset;
					return true;
				case PROPERTY_8_LASTBUFFERLENGTH_ID:
					value = controller.LastBufferLength;
					return true;
				case PROPERTY_9_UPDATESTATE_ID:
					value = controller.UpdateState;
					return true;
				case PROPERTY_10_SELECTEDORDER_ID:
					value = controller.SelectedOrder;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IUserRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_6_INDICATORLASTOFFSET_ID:
					controller.IndicatorLastOffset = (Int32) value;
					return true;
				case PROPERTY_7_LASTFILEOFFSET_ID:
					controller.LastFileOffset = (Int64) value;
					return true;
				case PROPERTY_8_LASTBUFFERLENGTH_ID:
					controller.LastBufferLength = (Int32) value;
					return true;
				case PROPERTY_9_UPDATESTATE_ID:
					controller.UpdateState = (IndicatorUpdateState) value;
					return true;
				case PROPERTY_10_SELECTEDORDER_ID:
					controller.SelectedOrder = (IOrder) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IUserRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[UserRuntimeProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Parent = (IChartRuntime) buffer.ChangedProps[UserRuntimeProps.PROPERTY_2_PARENT_ID];
			controller.SeriesManager = (ISeriesManagerRuntime) buffer.ChangedProps[UserRuntimeProps.PROPERTY_3_SERIESMANAGER_ID];
			controller.Cache = (ISeriesManagerCache) buffer.ChangedProps[UserRuntimeProps.PROPERTY_4_CACHE_ID];
			controller.TmpArrayCaches = (Dictionary<IDArr,IArraySeriesManagerCache>) buffer.ChangedProps[UserRuntimeProps.PROPERTY_5_TMPARRAYCACHES_ID];
		}

		public static void AddDependencies(IUserRuntime controller)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.SeriesManager);
			controller.Dependencies.Add(controller.Cache);
			controller.Dependencies.AddRange(controller.TmpArrayCaches.Values);
		}

		public static void SerializationRead(IUserRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Parent = (IChartRuntime) info.GetValue("Parent", typeof(IChartRuntime));
			controller.SeriesManager = (ISeriesManagerRuntime) info.GetValue("SeriesManager", typeof(ISeriesManagerRuntime));
			controller.Cache = (ISeriesManagerCache) info.GetValue("Cache", typeof(ISeriesManagerCache));
			controller.TmpArrayCaches = (Dictionary<IDArr,IArraySeriesManagerCache>) info.GetValue("TmpArrayCaches", typeof(Dictionary<IDArr,IArraySeriesManagerCache>));
			controller.IndicatorLastOffset = (Int32) info.GetValue("IndicatorLastOffset", typeof(Int32));
			controller.LastFileOffset = (Int64) info.GetValue("LastFileOffset", typeof(Int64));
			controller.LastBufferLength = (Int32) info.GetValue("LastBufferLength", typeof(Int32));
			controller.UpdateState = (IndicatorUpdateState) info.GetValue("UpdateState", typeof(IndicatorUpdateState));
			controller.SelectedOrder = (IOrder) info.GetValue("SelectedOrder", typeof(IOrder));
		}

		public static void SerializationWrite(IUserRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SeriesManager", controller.SeriesManager);
			info.AddValue("Cache", controller.Cache);
			info.AddValue("TmpArrayCaches", controller.TmpArrayCaches);
			info.AddValue("IndicatorLastOffset", controller.IndicatorLastOffset);
			info.AddValue("LastFileOffset", controller.LastFileOffset);
			info.AddValue("LastBufferLength", controller.LastBufferLength);
			info.AddValue("UpdateState", controller.UpdateState);
			info.AddValue("SelectedOrder", controller.SelectedOrder);
		}

	}
	public abstract class UserRuntimeBase : TradingConst, IUserRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IndicatorLastOffsetChanged;
		public event PropertyChangedEventHandler LastFileOffsetChanged;
		public event PropertyChangedEventHandler LastBufferLengthChanged;
		public event PropertyChangedEventHandler UpdateStateChanged;
		public event PropertyChangedEventHandler SelectedOrderChanged;

		public UserRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			UserRuntimeProps.AddDependencies(this);
		}

		public UserRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			UserRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			UserRuntimeProps.AddDependencies(this);
		}

		protected UserRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			UserRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			UserRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			UserRuntimeProps.SerializationWrite(this, info, context);
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
					changed[UserRuntimeProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartRuntime parent;
		public IChartRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[UserRuntimeProps.PROPERTY_2_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISeriesManagerRuntime seriesManager;
		public ISeriesManagerRuntime SeriesManager
		{
			get {
				return seriesManager;
			}
			set {
				if (!___initialized) {
					seriesManager= value;
					changed[UserRuntimeProps.PROPERTY_3_SERIESMANAGER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISeriesManagerCache cache;
		public ISeriesManagerCache Cache
		{
			get {
				return cache;
			}
			set {
				if (!___initialized) {
					cache= value;
					changed[UserRuntimeProps.PROPERTY_4_CACHE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Dictionary<IDArr,IArraySeriesManagerCache> tmpArrayCaches;
		public Dictionary<IDArr,IArraySeriesManagerCache> TmpArrayCaches
		{
			get {
				return tmpArrayCaches;
			}
			set {
				if (!___initialized) {
					tmpArrayCaches= value;
					changed[UserRuntimeProps.PROPERTY_5_TMPARRAYCACHES_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual Int32 Bars
		{
			get {
				return Cache.Bars;
			}
		}

		public virtual Double Bid
		{
			get {
				return Cache.Bid;
			}
		}

		public virtual Double Ask
		{
			get {
				return Cache.Ask;
			}
		}

		public virtual Double Point
		{
			get {
				return Cache.Point;
			}
		}

		public virtual Int32 Digits
		{
			get {
				return Cache.Digits;
			}
		}

		public virtual IDArr Open
		{
			get {
				return Cache.Open;
			}
		}

		public virtual IDArr Low
		{
			get {
				return Cache.Low;
			}
		}

		public virtual IDArr High
		{
			get {
				return Cache.High;
			}
		}

		public virtual IDArr Close
		{
			get {
				return Cache.Close;
			}
		}

		public virtual ILArr LTime
		{
			get {
				return Cache.LTime;
			}
		}

		public virtual ILArrAsIArr Time
		{
			get {
				return Cache.Time;
			}
		}

		public virtual IDArr Volume
		{
			get {
				return Cache.Volume;
			}
		}

		public virtual symbol Symbol
		{
			get {
				return Cache.Symbol;
			}
		}

		public virtual TimePeriodConst Period
		{
			get {
				return Cache.Period;
			}
		}

		Int32 indicatorLastOffset;
		public Int32 IndicatorLastOffset
		{
			get {
				return indicatorLastOffset;
			}
			set {
				if (indicatorLastOffset != value) {
					indicatorLastOffset= value;
					changed[UserRuntimeProps.PROPERTY_6_INDICATORLASTOFFSET_ID] = true;
					if (IndicatorLastOffsetChanged != null)
						IndicatorLastOffsetChanged(this, new PropertyChangedEventArgs("IndicatorLastOffset", value));
				}
			}
		}

		Int64 lastFileOffset;
		public Int64 LastFileOffset
		{
			get {
				return lastFileOffset;
			}
			set {
				if (lastFileOffset != value) {
					lastFileOffset= value;
					changed[UserRuntimeProps.PROPERTY_7_LASTFILEOFFSET_ID] = true;
					if (LastFileOffsetChanged != null)
						LastFileOffsetChanged(this, new PropertyChangedEventArgs("LastFileOffset", value));
				}
			}
		}

		Int32 lastBufferLength;
		public Int32 LastBufferLength
		{
			get {
				return lastBufferLength;
			}
			set {
				if (lastBufferLength != value) {
					lastBufferLength= value;
					changed[UserRuntimeProps.PROPERTY_8_LASTBUFFERLENGTH_ID] = true;
					if (LastBufferLengthChanged != null)
						LastBufferLengthChanged(this, new PropertyChangedEventArgs("LastBufferLength", value));
				}
			}
		}

		IndicatorUpdateState updateState;
		public IndicatorUpdateState UpdateState
		{
			get {
				return updateState;
			}
			set {
				if (updateState != value) {
					updateState= value;
					changed[UserRuntimeProps.PROPERTY_9_UPDATESTATE_ID] = true;
					if (UpdateStateChanged != null)
						UpdateStateChanged(this, new PropertyChangedEventArgs("UpdateState", value));
				}
			}
		}

		public virtual String AccountServer
		{
			get {
				return Environment.AccountServer;
			}
		}

		public virtual String AccountCurrency
		{
			get {
				return Environment.AccountCurrency;
			}
		}

		public virtual Int32 AccountNumber
		{
			get {
				return Environment.AccountNumber;
			}
		}

		public virtual Int32 AccountFreeMarginMode
		{
			get {
				return Environment.AccountFreeMarginMode;
			}
		}

		public virtual Int32 AccountStopoutLevel
		{
			get {
				return Environment.AccountStopoutLevel;
			}
		}

		public virtual Int32 AccountStopoutMode
		{
			get {
				return Environment.AccountStopoutMode;
			}
		}

		public virtual Boolean IsConnected
		{
			get {
				return Environment.IsConnected;
			}
		}

		public virtual Boolean IsStopped
		{
			get {
				return Environment.IsStopped;
			}
		}

		public virtual Boolean IsTesting
		{
			get {
				return !Environment.EnvironmentType.IsOnline();
			}
		}

		public virtual Int32 LastError
		{
			get {
				return Environment.LastError;
			}
		}

		public virtual datetime TimeCurrent
		{
			get {
				return Environment.Session.Time;
			}
		}

		IOrder selectedOrder;
		public virtual IOrder SelectedOrder
		{
			get {
				return selectedOrder;
			}
			set {
				if (selectedOrder != value) {
					selectedOrder= value;
					changed[UserRuntimeProps.PROPERTY_10_SELECTEDORDER_ID] = true;
					if (SelectedOrderChanged != null)
						SelectedOrderChanged(this, new PropertyChangedEventArgs("SelectedOrder", value));
				}
			}
		}

		public virtual OrderType OrderType
		{
			get {
				return SelectedOrder.Type;
			}
		}

		public virtual symbol OrderSymbol
		{
			get {
				return SelectedOrder.Symbol;
			}
		}

		public virtual Int32 OrderTicket
		{
			get {
				return SelectedOrder.Ticket;
			}
		}

		public virtual Double OrderLots
		{
			get {
				return SelectedOrder.Lots;
			}
		}

		public virtual Double OrderStopLoss
		{
			get {
				return SelectedOrder.StopLoss;
			}
		}

		public virtual Double OrderTakeProfit
		{
			get {
				return SelectedOrder.TakeProfit;
			}
		}

		public virtual datetime OrderOpenTime
		{
			get {
				return SelectedOrder.OpenTime;
			}
		}

		public virtual Double OrderOpenPrice
		{
			get {
				return SelectedOrder.OpenPrice;
			}
		}

		public virtual datetime OrderCloseTime
		{
			get {
				return SelectedOrder.CloseTime;
			}
		}

		public virtual Double OrderClosePrice
		{
			get {
				return SelectedOrder.ClosePrice;
			}
		}

		public virtual String OrderComment
		{
			get {
				return SelectedOrder.Comment;
			}
		}

		public virtual datetime OrderExpiration
		{
			get {
				return SelectedOrder.Expiration;
			}
		}

		public virtual Int32 OrderMagicNumber
		{
			get {
				return SelectedOrder.MagicNumber;
			}
		}

		public virtual Double OrderCommission
		{
			get {
				return SelectedOrder.Commission;
			}
		}

		public virtual Double OrderProfit
		{
			get {
				return SelectedOrder.Profit;
			}
		}

		public virtual Double OrderSwap
		{
			get {
				return SelectedOrder.Swap;
			}
		}

		public virtual Int32 OrdersTotal
		{
			get {
				return Environment.Orders.Orders.Count;
			}
		}

		public virtual Int32 OrdersHistoryTotal
		{
			get {
				return Environment.Orders.OrdersHistory.Count;
			}
		}

		public abstract Int32 IndicatorCounted
		{
			get ;
		}

		public virtual Int32 WindowBarsPerChart
		{
			get {
				return Parent.WindowBarsPerChart;
			}
		}

		public virtual Int32 WindowFirstVisibleBar
		{
			get {
				return Parent.WindowFirstVisibleBar;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (UserRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!UserRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
