using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class SymbolContextProps
	{
		public const int PROPERTY_1_PARENT_ID = 1;
		public const int PROPERTY_2_SYMBOL_ID = 2;
		public const int PROPERTY_3_ORDERS_ID = 3;
		public const int PROPERTY_4_RUNTIME_ID = 4;
		public const int PROPERTY_5_LOW_ID = 5;
		public const int PROPERTY_6_HIGH_ID = 6;
		public const int PROPERTY_7_TIME_ID = 7;
		public const int PROPERTY_8_BID_ID = 8;
		public const int PROPERTY_9_ASK_ID = 9;
		public const int PROPERTY_10_POINT_ID = 10;
		public const int PROPERTY_11_TRADITIONALPIP_ID = 11;
		public const int PROPERTY_12_DIGITS_ID = 12;
		public const int PROPERTY_13_SPREAD_ID = 13;
		public const int PROPERTY_14_STOPLEVEL_ID = 14;
		public const int PROPERTY_15_LOTSIZE_ID = 15;
		public const int PROPERTY_16_TICKVALUE_ID = 16;
		public const int PROPERTY_17_TICKSIZE_ID = 17;
		public const int PROPERTY_18_SWAPLONG_ID = 18;
		public const int PROPERTY_19_SWAPSHORT_ID = 19;
		public const int PROPERTY_20_STARTING_ID = 20;
		public const int PROPERTY_21_EXPIRATION_ID = 21;
		public const int PROPERTY_22_TRADEALLOWED_ID = 22;
		public const int PROPERTY_23_MINLOT_ID = 23;
		public const int PROPERTY_24_LOTSTEP_ID = 24;
		public const int PROPERTY_25_MAXLOT_ID = 25;
		public const int PROPERTY_26_SWAPTYPE_ID = 26;
		public const int PROPERTY_27_PROFITCALCMODE_ID = 27;
		public const int PROPERTY_28_MARGINCALCMODE_ID = 28;
		public const int PROPERTY_29_MARGININIT_ID = 29;
		public const int PROPERTY_30_MARGINMAINTENANCE_ID = 30;
		public const int PROPERTY_31_MARGINHEDGED_ID = 31;
		public const int PROPERTY_32_MARGINREQUIRED_ID = 32;
		public const int PROPERTY_33_FREEZELEVEL_ID = 33;
		public static bool RmiGetProperty(ISymbolContext controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SymbolContextProps.PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case SymbolContextProps.PROPERTY_2_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case SymbolContextProps.PROPERTY_3_ORDERS_ID:
					value = controller.Orders;
					return true;
				case SymbolContextProps.PROPERTY_4_RUNTIME_ID:
					value = controller.Runtime;
					return true;
				case SymbolContextProps.PROPERTY_5_LOW_ID:
					value = controller.Low;
					return true;
				case SymbolContextProps.PROPERTY_6_HIGH_ID:
					value = controller.High;
					return true;
				case SymbolContextProps.PROPERTY_7_TIME_ID:
					value = controller.Time;
					return true;
				case SymbolContextProps.PROPERTY_8_BID_ID:
					value = controller.Bid;
					return true;
				case SymbolContextProps.PROPERTY_9_ASK_ID:
					value = controller.Ask;
					return true;
				case SymbolContextProps.PROPERTY_10_POINT_ID:
					value = controller.Point;
					return true;
				case SymbolContextProps.PROPERTY_11_TRADITIONALPIP_ID:
					value = controller.TraditionalPip;
					return true;
				case SymbolContextProps.PROPERTY_12_DIGITS_ID:
					value = controller.Digits;
					return true;
				case SymbolContextProps.PROPERTY_13_SPREAD_ID:
					value = controller.Spread;
					return true;
				case SymbolContextProps.PROPERTY_14_STOPLEVEL_ID:
					value = controller.StopLevel;
					return true;
				case SymbolContextProps.PROPERTY_15_LOTSIZE_ID:
					value = controller.LotSize;
					return true;
				case SymbolContextProps.PROPERTY_16_TICKVALUE_ID:
					value = controller.TickValue;
					return true;
				case SymbolContextProps.PROPERTY_17_TICKSIZE_ID:
					value = controller.TickSize;
					return true;
				case SymbolContextProps.PROPERTY_18_SWAPLONG_ID:
					value = controller.SwapLong;
					return true;
				case SymbolContextProps.PROPERTY_19_SWAPSHORT_ID:
					value = controller.SwapShort;
					return true;
				case SymbolContextProps.PROPERTY_20_STARTING_ID:
					value = controller.Starting;
					return true;
				case SymbolContextProps.PROPERTY_21_EXPIRATION_ID:
					value = controller.Expiration;
					return true;
				case SymbolContextProps.PROPERTY_22_TRADEALLOWED_ID:
					value = controller.TradeAllowed;
					return true;
				case SymbolContextProps.PROPERTY_23_MINLOT_ID:
					value = controller.MinLot;
					return true;
				case SymbolContextProps.PROPERTY_24_LOTSTEP_ID:
					value = controller.LotStep;
					return true;
				case SymbolContextProps.PROPERTY_25_MAXLOT_ID:
					value = controller.MaxLot;
					return true;
				case SymbolContextProps.PROPERTY_26_SWAPTYPE_ID:
					value = controller.SwapType;
					return true;
				case SymbolContextProps.PROPERTY_27_PROFITCALCMODE_ID:
					value = controller.ProfitCalcMode;
					return true;
				case SymbolContextProps.PROPERTY_28_MARGINCALCMODE_ID:
					value = controller.MarginCalcMode;
					return true;
				case SymbolContextProps.PROPERTY_29_MARGININIT_ID:
					value = controller.MarginInit;
					return true;
				case SymbolContextProps.PROPERTY_30_MARGINMAINTENANCE_ID:
					value = controller.MarginMaintenance;
					return true;
				case SymbolContextProps.PROPERTY_31_MARGINHEDGED_ID:
					value = controller.MarginHedged;
					return true;
				case SymbolContextProps.PROPERTY_32_MARGINREQUIRED_ID:
					value = controller.MarginRequired;
					return true;
				case SymbolContextProps.PROPERTY_33_FREEZELEVEL_ID:
					value = controller.FreezeLevel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolContext controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SymbolContextProps.PROPERTY_4_RUNTIME_ID:
					controller.Runtime = (ISymbolRuntime) value;
					return true;
				case SymbolContextProps.PROPERTY_5_LOW_ID:
					controller.Low = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_6_HIGH_ID:
					controller.High = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_7_TIME_ID:
					controller.Time = (datetime) value;
					return true;
				case SymbolContextProps.PROPERTY_8_BID_ID:
					controller.Bid = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_9_ASK_ID:
					controller.Ask = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_10_POINT_ID:
					controller.Point = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_11_TRADITIONALPIP_ID:
					controller.TraditionalPip = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_12_DIGITS_ID:
					controller.Digits = (Int32) value;
					return true;
				case SymbolContextProps.PROPERTY_13_SPREAD_ID:
					controller.Spread = (Int32) value;
					return true;
				case SymbolContextProps.PROPERTY_14_STOPLEVEL_ID:
					controller.StopLevel = (Int32) value;
					return true;
				case SymbolContextProps.PROPERTY_15_LOTSIZE_ID:
					controller.LotSize = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_16_TICKVALUE_ID:
					controller.TickValue = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_17_TICKSIZE_ID:
					controller.TickSize = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_18_SWAPLONG_ID:
					controller.SwapLong = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_19_SWAPSHORT_ID:
					controller.SwapShort = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_20_STARTING_ID:
					controller.Starting = (datetime) value;
					return true;
				case SymbolContextProps.PROPERTY_21_EXPIRATION_ID:
					controller.Expiration = (datetime) value;
					return true;
				case SymbolContextProps.PROPERTY_22_TRADEALLOWED_ID:
					controller.TradeAllowed = (Boolean) value;
					return true;
				case SymbolContextProps.PROPERTY_23_MINLOT_ID:
					controller.MinLot = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_24_LOTSTEP_ID:
					controller.LotStep = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_25_MAXLOT_ID:
					controller.MaxLot = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_26_SWAPTYPE_ID:
					controller.SwapType = (SwapCalculationMethod) value;
					return true;
				case SymbolContextProps.PROPERTY_27_PROFITCALCMODE_ID:
					controller.ProfitCalcMode = (ProfitCalculationMode) value;
					return true;
				case SymbolContextProps.PROPERTY_28_MARGINCALCMODE_ID:
					controller.MarginCalcMode = (MarginCalculationMode) value;
					return true;
				case SymbolContextProps.PROPERTY_29_MARGININIT_ID:
					controller.MarginInit = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_30_MARGINMAINTENANCE_ID:
					controller.MarginMaintenance = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_31_MARGINHEDGED_ID:
					controller.MarginHedged = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_32_MARGINREQUIRED_ID:
					controller.MarginRequired = (Double) value;
					return true;
				case SymbolContextProps.PROPERTY_33_FREEZELEVEL_ID:
					controller.FreezeLevel = (Double) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolContext controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Parent = (IEnvironmentRuntime) buffer.ChangedProps[SymbolContextProps.PROPERTY_1_PARENT_ID];
			controller.Symbol = (symbol) buffer.ChangedProps[SymbolContextProps.PROPERTY_2_SYMBOL_ID];
			controller.Orders = (IOrdersTable) buffer.ChangedProps[SymbolContextProps.PROPERTY_3_ORDERS_ID];
		}

		public static void AddDependencies(ISymbolContext controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.Orders);
		}

		public static void SerializationRead(ISymbolContext controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Parent = (IEnvironmentRuntime) info.GetValue("Parent", typeof(IEnvironmentRuntime));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Orders = (IOrdersTable) info.GetValue("Orders", typeof(IOrdersTable));
			controller.Runtime = (ISymbolRuntime) info.GetValue("Runtime", typeof(ISymbolRuntime));
			controller.Low = (Double) info.GetValue("Low", typeof(Double));
			controller.High = (Double) info.GetValue("High", typeof(Double));
			controller.Time = (datetime) info.GetValue("Time", typeof(datetime));
			controller.Bid = (Double) info.GetValue("Bid", typeof(Double));
			controller.Ask = (Double) info.GetValue("Ask", typeof(Double));
			controller.Point = (Double) info.GetValue("Point", typeof(Double));
			controller.TraditionalPip = (Double) info.GetValue("TraditionalPip", typeof(Double));
			controller.Digits = (Int32) info.GetValue("Digits", typeof(Int32));
			controller.Spread = (Int32) info.GetValue("Spread", typeof(Int32));
			controller.StopLevel = (Int32) info.GetValue("StopLevel", typeof(Int32));
			controller.LotSize = (Double) info.GetValue("LotSize", typeof(Double));
			controller.TickValue = (Double) info.GetValue("TickValue", typeof(Double));
			controller.TickSize = (Double) info.GetValue("TickSize", typeof(Double));
			controller.SwapLong = (Double) info.GetValue("SwapLong", typeof(Double));
			controller.SwapShort = (Double) info.GetValue("SwapShort", typeof(Double));
			controller.Starting = (datetime) info.GetValue("Starting", typeof(datetime));
			controller.Expiration = (datetime) info.GetValue("Expiration", typeof(datetime));
			controller.TradeAllowed = (Boolean) info.GetValue("TradeAllowed", typeof(Boolean));
			controller.MinLot = (Double) info.GetValue("MinLot", typeof(Double));
			controller.LotStep = (Double) info.GetValue("LotStep", typeof(Double));
			controller.MaxLot = (Double) info.GetValue("MaxLot", typeof(Double));
			controller.SwapType = (SwapCalculationMethod) info.GetValue("SwapType", typeof(SwapCalculationMethod));
			controller.ProfitCalcMode = (ProfitCalculationMode) info.GetValue("ProfitCalcMode", typeof(ProfitCalculationMode));
			controller.MarginCalcMode = (MarginCalculationMode) info.GetValue("MarginCalcMode", typeof(MarginCalculationMode));
			controller.MarginInit = (Double) info.GetValue("MarginInit", typeof(Double));
			controller.MarginMaintenance = (Double) info.GetValue("MarginMaintenance", typeof(Double));
			controller.MarginHedged = (Double) info.GetValue("MarginHedged", typeof(Double));
			controller.MarginRequired = (Double) info.GetValue("MarginRequired", typeof(Double));
			controller.FreezeLevel = (Double) info.GetValue("FreezeLevel", typeof(Double));
		}

		public static void SerializationWrite(ISymbolContext controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Orders", controller.Orders);
			info.AddValue("Runtime", controller.Runtime);
			info.AddValue("Low", controller.Low);
			info.AddValue("High", controller.High);
			info.AddValue("Time", controller.Time);
			info.AddValue("Bid", controller.Bid);
			info.AddValue("Ask", controller.Ask);
			info.AddValue("Point", controller.Point);
			info.AddValue("TraditionalPip", controller.TraditionalPip);
			info.AddValue("Digits", controller.Digits);
			info.AddValue("Spread", controller.Spread);
			info.AddValue("StopLevel", controller.StopLevel);
			info.AddValue("LotSize", controller.LotSize);
			info.AddValue("TickValue", controller.TickValue);
			info.AddValue("TickSize", controller.TickSize);
			info.AddValue("SwapLong", controller.SwapLong);
			info.AddValue("SwapShort", controller.SwapShort);
			info.AddValue("Starting", controller.Starting);
			info.AddValue("Expiration", controller.Expiration);
			info.AddValue("TradeAllowed", controller.TradeAllowed);
			info.AddValue("MinLot", controller.MinLot);
			info.AddValue("LotStep", controller.LotStep);
			info.AddValue("MaxLot", controller.MaxLot);
			info.AddValue("SwapType", controller.SwapType);
			info.AddValue("ProfitCalcMode", controller.ProfitCalcMode);
			info.AddValue("MarginCalcMode", controller.MarginCalcMode);
			info.AddValue("MarginInit", controller.MarginInit);
			info.AddValue("MarginMaintenance", controller.MarginMaintenance);
			info.AddValue("MarginHedged", controller.MarginHedged);
			info.AddValue("MarginRequired", controller.MarginRequired);
			info.AddValue("FreezeLevel", controller.FreezeLevel);
		}

	}
	public abstract class SymbolContextBase : TradingConst, ISymbolContext
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ISymbolContext_Runtime_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Low_Changed;
		public event PropertyChangedEventHandler ISymbolContext_High_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Time_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Bid_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Ask_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Point_Changed;
		public event PropertyChangedEventHandler ISymbolContext_TraditionalPip_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Digits_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Spread_Changed;
		public event PropertyChangedEventHandler ISymbolContext_StopLevel_Changed;
		public event PropertyChangedEventHandler ISymbolContext_LotSize_Changed;
		public event PropertyChangedEventHandler ISymbolContext_TickValue_Changed;
		public event PropertyChangedEventHandler ISymbolContext_TickSize_Changed;
		public event PropertyChangedEventHandler ISymbolContext_SwapLong_Changed;
		public event PropertyChangedEventHandler ISymbolContext_SwapShort_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Starting_Changed;
		public event PropertyChangedEventHandler ISymbolContext_Expiration_Changed;
		public event PropertyChangedEventHandler ISymbolContext_TradeAllowed_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MinLot_Changed;
		public event PropertyChangedEventHandler ISymbolContext_LotStep_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MaxLot_Changed;
		public event PropertyChangedEventHandler ISymbolContext_SwapType_Changed;
		public event PropertyChangedEventHandler ISymbolContext_ProfitCalcMode_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MarginCalcMode_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MarginInit_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MarginMaintenance_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MarginHedged_Changed;
		public event PropertyChangedEventHandler ISymbolContext_MarginRequired_Changed;
		public event PropertyChangedEventHandler ISymbolContext_FreezeLevel_Changed;

		public SymbolContextBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SymbolContextProps.AddDependencies(this, false);
		}

		public SymbolContextBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolContextProps.Initialize(this, buffer, false);
			___initialized = true;
			SymbolContextProps.AddDependencies(this, false);
		}

		protected SymbolContextBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolContextProps.SerializationRead(this, info, context, false);
			___initialized = true;
			SymbolContextProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolContextProps.SerializationWrite(this, info, context, false);
		}

		public abstract Double GetValue(MarketInfoConst field);


		IEnvironmentRuntime _ISymbolContext_Parent;
		public IEnvironmentRuntime Parent
		{
			get {
				return _ISymbolContext_Parent;
			}
			set {
				if (!___initialized) {
					_ISymbolContext_Parent= value;
					changed[SymbolContextProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol _ISymbolContext_Symbol;
		public symbol Symbol
		{
			get {
				return _ISymbolContext_Symbol;
			}
			set {
				if (!___initialized) {
					_ISymbolContext_Symbol= value;
					changed[SymbolContextProps.PROPERTY_2_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTable _ISymbolContext_Orders;
		public IOrdersTable Orders
		{
			get {
				return _ISymbolContext_Orders;
			}
			set {
				if (!___initialized) {
					_ISymbolContext_Orders= value;
					changed[SymbolContextProps.PROPERTY_3_ORDERS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime _ISymbolContext_Runtime;
		public virtual ISymbolRuntime Runtime
		{
			get {
				return _ISymbolContext_Runtime;
			}
			set {
				if (_ISymbolContext_Runtime != value) {
					_ISymbolContext_Runtime= value;
					changed[SymbolContextProps.PROPERTY_4_RUNTIME_ID] = true;
					if (ISymbolContext_Runtime_Changed != null)
						ISymbolContext_Runtime_Changed(this, new PropertyChangedEventArgs("Runtime", value));
				}
			}
		}

		Double _ISymbolContext_Low;
		public Double Low
		{
			get {
				return _ISymbolContext_Low;
			}
			set {
				if (_ISymbolContext_Low != value) {
					_ISymbolContext_Low= value;
					changed[SymbolContextProps.PROPERTY_5_LOW_ID] = true;
					if (ISymbolContext_Low_Changed != null)
						ISymbolContext_Low_Changed(this, new PropertyChangedEventArgs("Low", value));
				}
			}
		}

		Double _ISymbolContext_High;
		public Double High
		{
			get {
				return _ISymbolContext_High;
			}
			set {
				if (_ISymbolContext_High != value) {
					_ISymbolContext_High= value;
					changed[SymbolContextProps.PROPERTY_6_HIGH_ID] = true;
					if (ISymbolContext_High_Changed != null)
						ISymbolContext_High_Changed(this, new PropertyChangedEventArgs("High", value));
				}
			}
		}

		datetime _ISymbolContext_Time;
		public datetime Time
		{
			get {
				return _ISymbolContext_Time;
			}
			set {
				if (_ISymbolContext_Time != value) {
					_ISymbolContext_Time= value;
					changed[SymbolContextProps.PROPERTY_7_TIME_ID] = true;
					if (ISymbolContext_Time_Changed != null)
						ISymbolContext_Time_Changed(this, new PropertyChangedEventArgs("Time", value));
				}
			}
		}

		Double _ISymbolContext_Bid;
		public Double Bid
		{
			get {
				return _ISymbolContext_Bid;
			}
			set {
				if (_ISymbolContext_Bid != value) {
					_ISymbolContext_Bid= value;
					changed[SymbolContextProps.PROPERTY_8_BID_ID] = true;
					if (ISymbolContext_Bid_Changed != null)
						ISymbolContext_Bid_Changed(this, new PropertyChangedEventArgs("Bid", value));
				}
			}
		}

		Double _ISymbolContext_Ask;
		public Double Ask
		{
			get {
				return _ISymbolContext_Ask;
			}
			set {
				if (_ISymbolContext_Ask != value) {
					_ISymbolContext_Ask= value;
					changed[SymbolContextProps.PROPERTY_9_ASK_ID] = true;
					if (ISymbolContext_Ask_Changed != null)
						ISymbolContext_Ask_Changed(this, new PropertyChangedEventArgs("Ask", value));
				}
			}
		}

		Double _ISymbolContext_Point;
		public Double Point
		{
			get {
				return _ISymbolContext_Point;
			}
			set {
				if (_ISymbolContext_Point != value) {
					_ISymbolContext_Point= value;
					changed[SymbolContextProps.PROPERTY_10_POINT_ID] = true;
					if (ISymbolContext_Point_Changed != null)
						ISymbolContext_Point_Changed(this, new PropertyChangedEventArgs("Point", value));
				}
			}
		}

		Double _ISymbolContext_TraditionalPip;
		public Double TraditionalPip
		{
			get {
				return _ISymbolContext_TraditionalPip;
			}
			set {
				if (_ISymbolContext_TraditionalPip != value) {
					_ISymbolContext_TraditionalPip= value;
					changed[SymbolContextProps.PROPERTY_11_TRADITIONALPIP_ID] = true;
					if (ISymbolContext_TraditionalPip_Changed != null)
						ISymbolContext_TraditionalPip_Changed(this, new PropertyChangedEventArgs("TraditionalPip", value));
				}
			}
		}

		Int32 _ISymbolContext_Digits;
		public Int32 Digits
		{
			get {
				return _ISymbolContext_Digits;
			}
			set {
				if (_ISymbolContext_Digits != value) {
					_ISymbolContext_Digits= value;
					changed[SymbolContextProps.PROPERTY_12_DIGITS_ID] = true;
					if (ISymbolContext_Digits_Changed != null)
						ISymbolContext_Digits_Changed(this, new PropertyChangedEventArgs("Digits", value));
				}
			}
		}

		Int32 _ISymbolContext_Spread;
		public Int32 Spread
		{
			get {
				return _ISymbolContext_Spread;
			}
			set {
				if (_ISymbolContext_Spread != value) {
					_ISymbolContext_Spread= value;
					changed[SymbolContextProps.PROPERTY_13_SPREAD_ID] = true;
					if (ISymbolContext_Spread_Changed != null)
						ISymbolContext_Spread_Changed(this, new PropertyChangedEventArgs("Spread", value));
				}
			}
		}

		Int32 _ISymbolContext_StopLevel;
		public Int32 StopLevel
		{
			get {
				return _ISymbolContext_StopLevel;
			}
			set {
				if (_ISymbolContext_StopLevel != value) {
					_ISymbolContext_StopLevel= value;
					changed[SymbolContextProps.PROPERTY_14_STOPLEVEL_ID] = true;
					if (ISymbolContext_StopLevel_Changed != null)
						ISymbolContext_StopLevel_Changed(this, new PropertyChangedEventArgs("StopLevel", value));
				}
			}
		}

		Double _ISymbolContext_LotSize;
		public Double LotSize
		{
			get {
				return _ISymbolContext_LotSize;
			}
			set {
				if (_ISymbolContext_LotSize != value) {
					_ISymbolContext_LotSize= value;
					changed[SymbolContextProps.PROPERTY_15_LOTSIZE_ID] = true;
					if (ISymbolContext_LotSize_Changed != null)
						ISymbolContext_LotSize_Changed(this, new PropertyChangedEventArgs("LotSize", value));
				}
			}
		}

		Double _ISymbolContext_TickValue;
		public Double TickValue
		{
			get {
				return _ISymbolContext_TickValue;
			}
			set {
				if (_ISymbolContext_TickValue != value) {
					_ISymbolContext_TickValue= value;
					changed[SymbolContextProps.PROPERTY_16_TICKVALUE_ID] = true;
					if (ISymbolContext_TickValue_Changed != null)
						ISymbolContext_TickValue_Changed(this, new PropertyChangedEventArgs("TickValue", value));
				}
			}
		}

		Double _ISymbolContext_TickSize;
		public Double TickSize
		{
			get {
				return _ISymbolContext_TickSize;
			}
			set {
				if (_ISymbolContext_TickSize != value) {
					_ISymbolContext_TickSize= value;
					changed[SymbolContextProps.PROPERTY_17_TICKSIZE_ID] = true;
					if (ISymbolContext_TickSize_Changed != null)
						ISymbolContext_TickSize_Changed(this, new PropertyChangedEventArgs("TickSize", value));
				}
			}
		}

		Double _ISymbolContext_SwapLong;
		public Double SwapLong
		{
			get {
				return _ISymbolContext_SwapLong;
			}
			set {
				if (_ISymbolContext_SwapLong != value) {
					_ISymbolContext_SwapLong= value;
					changed[SymbolContextProps.PROPERTY_18_SWAPLONG_ID] = true;
					if (ISymbolContext_SwapLong_Changed != null)
						ISymbolContext_SwapLong_Changed(this, new PropertyChangedEventArgs("SwapLong", value));
				}
			}
		}

		Double _ISymbolContext_SwapShort;
		public Double SwapShort
		{
			get {
				return _ISymbolContext_SwapShort;
			}
			set {
				if (_ISymbolContext_SwapShort != value) {
					_ISymbolContext_SwapShort= value;
					changed[SymbolContextProps.PROPERTY_19_SWAPSHORT_ID] = true;
					if (ISymbolContext_SwapShort_Changed != null)
						ISymbolContext_SwapShort_Changed(this, new PropertyChangedEventArgs("SwapShort", value));
				}
			}
		}

		datetime _ISymbolContext_Starting;
		public datetime Starting
		{
			get {
				return _ISymbolContext_Starting;
			}
			set {
				if (_ISymbolContext_Starting != value) {
					_ISymbolContext_Starting= value;
					changed[SymbolContextProps.PROPERTY_20_STARTING_ID] = true;
					if (ISymbolContext_Starting_Changed != null)
						ISymbolContext_Starting_Changed(this, new PropertyChangedEventArgs("Starting", value));
				}
			}
		}

		datetime _ISymbolContext_Expiration;
		public datetime Expiration
		{
			get {
				return _ISymbolContext_Expiration;
			}
			set {
				if (_ISymbolContext_Expiration != value) {
					_ISymbolContext_Expiration= value;
					changed[SymbolContextProps.PROPERTY_21_EXPIRATION_ID] = true;
					if (ISymbolContext_Expiration_Changed != null)
						ISymbolContext_Expiration_Changed(this, new PropertyChangedEventArgs("Expiration", value));
				}
			}
		}

		Boolean _ISymbolContext_TradeAllowed;
		public Boolean TradeAllowed
		{
			get {
				return _ISymbolContext_TradeAllowed;
			}
			set {
				if (_ISymbolContext_TradeAllowed != value) {
					_ISymbolContext_TradeAllowed= value;
					changed[SymbolContextProps.PROPERTY_22_TRADEALLOWED_ID] = true;
					if (ISymbolContext_TradeAllowed_Changed != null)
						ISymbolContext_TradeAllowed_Changed(this, new PropertyChangedEventArgs("TradeAllowed", value));
				}
			}
		}

		Double _ISymbolContext_MinLot;
		public Double MinLot
		{
			get {
				return _ISymbolContext_MinLot;
			}
			set {
				if (_ISymbolContext_MinLot != value) {
					_ISymbolContext_MinLot= value;
					changed[SymbolContextProps.PROPERTY_23_MINLOT_ID] = true;
					if (ISymbolContext_MinLot_Changed != null)
						ISymbolContext_MinLot_Changed(this, new PropertyChangedEventArgs("MinLot", value));
				}
			}
		}

		Double _ISymbolContext_LotStep;
		public Double LotStep
		{
			get {
				return _ISymbolContext_LotStep;
			}
			set {
				if (_ISymbolContext_LotStep != value) {
					_ISymbolContext_LotStep= value;
					changed[SymbolContextProps.PROPERTY_24_LOTSTEP_ID] = true;
					if (ISymbolContext_LotStep_Changed != null)
						ISymbolContext_LotStep_Changed(this, new PropertyChangedEventArgs("LotStep", value));
				}
			}
		}

		Double _ISymbolContext_MaxLot;
		public Double MaxLot
		{
			get {
				return _ISymbolContext_MaxLot;
			}
			set {
				if (_ISymbolContext_MaxLot != value) {
					_ISymbolContext_MaxLot= value;
					changed[SymbolContextProps.PROPERTY_25_MAXLOT_ID] = true;
					if (ISymbolContext_MaxLot_Changed != null)
						ISymbolContext_MaxLot_Changed(this, new PropertyChangedEventArgs("MaxLot", value));
				}
			}
		}

		SwapCalculationMethod _ISymbolContext_SwapType;
		public SwapCalculationMethod SwapType
		{
			get {
				return _ISymbolContext_SwapType;
			}
			set {
				if (_ISymbolContext_SwapType != value) {
					_ISymbolContext_SwapType= value;
					changed[SymbolContextProps.PROPERTY_26_SWAPTYPE_ID] = true;
					if (ISymbolContext_SwapType_Changed != null)
						ISymbolContext_SwapType_Changed(this, new PropertyChangedEventArgs("SwapType", value));
				}
			}
		}

		ProfitCalculationMode _ISymbolContext_ProfitCalcMode;
		public ProfitCalculationMode ProfitCalcMode
		{
			get {
				return _ISymbolContext_ProfitCalcMode;
			}
			set {
				if (_ISymbolContext_ProfitCalcMode != value) {
					_ISymbolContext_ProfitCalcMode= value;
					changed[SymbolContextProps.PROPERTY_27_PROFITCALCMODE_ID] = true;
					if (ISymbolContext_ProfitCalcMode_Changed != null)
						ISymbolContext_ProfitCalcMode_Changed(this, new PropertyChangedEventArgs("ProfitCalcMode", value));
				}
			}
		}

		MarginCalculationMode _ISymbolContext_MarginCalcMode;
		public MarginCalculationMode MarginCalcMode
		{
			get {
				return _ISymbolContext_MarginCalcMode;
			}
			set {
				if (_ISymbolContext_MarginCalcMode != value) {
					_ISymbolContext_MarginCalcMode= value;
					changed[SymbolContextProps.PROPERTY_28_MARGINCALCMODE_ID] = true;
					if (ISymbolContext_MarginCalcMode_Changed != null)
						ISymbolContext_MarginCalcMode_Changed(this, new PropertyChangedEventArgs("MarginCalcMode", value));
				}
			}
		}

		Double _ISymbolContext_MarginInit;
		public Double MarginInit
		{
			get {
				return _ISymbolContext_MarginInit;
			}
			set {
				if (_ISymbolContext_MarginInit != value) {
					_ISymbolContext_MarginInit= value;
					changed[SymbolContextProps.PROPERTY_29_MARGININIT_ID] = true;
					if (ISymbolContext_MarginInit_Changed != null)
						ISymbolContext_MarginInit_Changed(this, new PropertyChangedEventArgs("MarginInit", value));
				}
			}
		}

		Double _ISymbolContext_MarginMaintenance;
		public Double MarginMaintenance
		{
			get {
				return _ISymbolContext_MarginMaintenance;
			}
			set {
				if (_ISymbolContext_MarginMaintenance != value) {
					_ISymbolContext_MarginMaintenance= value;
					changed[SymbolContextProps.PROPERTY_30_MARGINMAINTENANCE_ID] = true;
					if (ISymbolContext_MarginMaintenance_Changed != null)
						ISymbolContext_MarginMaintenance_Changed(this, new PropertyChangedEventArgs("MarginMaintenance", value));
				}
			}
		}

		Double _ISymbolContext_MarginHedged;
		public Double MarginHedged
		{
			get {
				return _ISymbolContext_MarginHedged;
			}
			set {
				if (_ISymbolContext_MarginHedged != value) {
					_ISymbolContext_MarginHedged= value;
					changed[SymbolContextProps.PROPERTY_31_MARGINHEDGED_ID] = true;
					if (ISymbolContext_MarginHedged_Changed != null)
						ISymbolContext_MarginHedged_Changed(this, new PropertyChangedEventArgs("MarginHedged", value));
				}
			}
		}

		Double _ISymbolContext_MarginRequired;
		public Double MarginRequired
		{
			get {
				return _ISymbolContext_MarginRequired;
			}
			set {
				if (_ISymbolContext_MarginRequired != value) {
					_ISymbolContext_MarginRequired= value;
					changed[SymbolContextProps.PROPERTY_32_MARGINREQUIRED_ID] = true;
					if (ISymbolContext_MarginRequired_Changed != null)
						ISymbolContext_MarginRequired_Changed(this, new PropertyChangedEventArgs("MarginRequired", value));
				}
			}
		}

		Double _ISymbolContext_FreezeLevel;
		public Double FreezeLevel
		{
			get {
				return _ISymbolContext_FreezeLevel;
			}
			set {
				if (_ISymbolContext_FreezeLevel != value) {
					_ISymbolContext_FreezeLevel= value;
					changed[SymbolContextProps.PROPERTY_33_FREEZELEVEL_ID] = true;
					if (ISymbolContext_FreezeLevel_Changed != null)
						ISymbolContext_FreezeLevel_Changed(this, new PropertyChangedEventArgs("FreezeLevel", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolContextProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (SymbolContextProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
