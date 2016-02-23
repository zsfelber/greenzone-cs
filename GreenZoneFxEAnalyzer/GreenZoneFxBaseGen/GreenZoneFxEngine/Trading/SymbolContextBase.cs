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
		public static bool RmiGetProperty(ISymbolContext controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_2_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_3_ORDERS_ID:
					value = controller.Orders;
					return true;
				case PROPERTY_4_RUNTIME_ID:
					value = controller.Runtime;
					return true;
				case PROPERTY_5_LOW_ID:
					value = controller.Low;
					return true;
				case PROPERTY_6_HIGH_ID:
					value = controller.High;
					return true;
				case PROPERTY_7_TIME_ID:
					value = controller.Time;
					return true;
				case PROPERTY_8_BID_ID:
					value = controller.Bid;
					return true;
				case PROPERTY_9_ASK_ID:
					value = controller.Ask;
					return true;
				case PROPERTY_10_POINT_ID:
					value = controller.Point;
					return true;
				case PROPERTY_11_TRADITIONALPIP_ID:
					value = controller.TraditionalPip;
					return true;
				case PROPERTY_12_DIGITS_ID:
					value = controller.Digits;
					return true;
				case PROPERTY_13_SPREAD_ID:
					value = controller.Spread;
					return true;
				case PROPERTY_14_STOPLEVEL_ID:
					value = controller.StopLevel;
					return true;
				case PROPERTY_15_LOTSIZE_ID:
					value = controller.LotSize;
					return true;
				case PROPERTY_16_TICKVALUE_ID:
					value = controller.TickValue;
					return true;
				case PROPERTY_17_TICKSIZE_ID:
					value = controller.TickSize;
					return true;
				case PROPERTY_18_SWAPLONG_ID:
					value = controller.SwapLong;
					return true;
				case PROPERTY_19_SWAPSHORT_ID:
					value = controller.SwapShort;
					return true;
				case PROPERTY_20_STARTING_ID:
					value = controller.Starting;
					return true;
				case PROPERTY_21_EXPIRATION_ID:
					value = controller.Expiration;
					return true;
				case PROPERTY_22_TRADEALLOWED_ID:
					value = controller.TradeAllowed;
					return true;
				case PROPERTY_23_MINLOT_ID:
					value = controller.MinLot;
					return true;
				case PROPERTY_24_LOTSTEP_ID:
					value = controller.LotStep;
					return true;
				case PROPERTY_25_MAXLOT_ID:
					value = controller.MaxLot;
					return true;
				case PROPERTY_26_SWAPTYPE_ID:
					value = controller.SwapType;
					return true;
				case PROPERTY_27_PROFITCALCMODE_ID:
					value = controller.ProfitCalcMode;
					return true;
				case PROPERTY_28_MARGINCALCMODE_ID:
					value = controller.MarginCalcMode;
					return true;
				case PROPERTY_29_MARGININIT_ID:
					value = controller.MarginInit;
					return true;
				case PROPERTY_30_MARGINMAINTENANCE_ID:
					value = controller.MarginMaintenance;
					return true;
				case PROPERTY_31_MARGINHEDGED_ID:
					value = controller.MarginHedged;
					return true;
				case PROPERTY_32_MARGINREQUIRED_ID:
					value = controller.MarginRequired;
					return true;
				case PROPERTY_33_FREEZELEVEL_ID:
					value = controller.FreezeLevel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolContext controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_4_RUNTIME_ID:
					controller.Runtime = (ISymbolRuntime) value;
					return true;
				case PROPERTY_5_LOW_ID:
					controller.Low = (Double) value;
					return true;
				case PROPERTY_6_HIGH_ID:
					controller.High = (Double) value;
					return true;
				case PROPERTY_7_TIME_ID:
					controller.Time = (datetime) value;
					return true;
				case PROPERTY_8_BID_ID:
					controller.Bid = (Double) value;
					return true;
				case PROPERTY_9_ASK_ID:
					controller.Ask = (Double) value;
					return true;
				case PROPERTY_10_POINT_ID:
					controller.Point = (Double) value;
					return true;
				case PROPERTY_11_TRADITIONALPIP_ID:
					controller.TraditionalPip = (Double) value;
					return true;
				case PROPERTY_12_DIGITS_ID:
					controller.Digits = (Int32) value;
					return true;
				case PROPERTY_13_SPREAD_ID:
					controller.Spread = (Int32) value;
					return true;
				case PROPERTY_14_STOPLEVEL_ID:
					controller.StopLevel = (Int32) value;
					return true;
				case PROPERTY_15_LOTSIZE_ID:
					controller.LotSize = (Double) value;
					return true;
				case PROPERTY_16_TICKVALUE_ID:
					controller.TickValue = (Double) value;
					return true;
				case PROPERTY_17_TICKSIZE_ID:
					controller.TickSize = (Double) value;
					return true;
				case PROPERTY_18_SWAPLONG_ID:
					controller.SwapLong = (Double) value;
					return true;
				case PROPERTY_19_SWAPSHORT_ID:
					controller.SwapShort = (Double) value;
					return true;
				case PROPERTY_20_STARTING_ID:
					controller.Starting = (datetime) value;
					return true;
				case PROPERTY_21_EXPIRATION_ID:
					controller.Expiration = (datetime) value;
					return true;
				case PROPERTY_22_TRADEALLOWED_ID:
					controller.TradeAllowed = (Boolean) value;
					return true;
				case PROPERTY_23_MINLOT_ID:
					controller.MinLot = (Double) value;
					return true;
				case PROPERTY_24_LOTSTEP_ID:
					controller.LotStep = (Double) value;
					return true;
				case PROPERTY_25_MAXLOT_ID:
					controller.MaxLot = (Double) value;
					return true;
				case PROPERTY_26_SWAPTYPE_ID:
					controller.SwapType = (SwapCalculationMethod) value;
					return true;
				case PROPERTY_27_PROFITCALCMODE_ID:
					controller.ProfitCalcMode = (ProfitCalculationMode) value;
					return true;
				case PROPERTY_28_MARGINCALCMODE_ID:
					controller.MarginCalcMode = (MarginCalculationMode) value;
					return true;
				case PROPERTY_29_MARGININIT_ID:
					controller.MarginInit = (Double) value;
					return true;
				case PROPERTY_30_MARGINMAINTENANCE_ID:
					controller.MarginMaintenance = (Double) value;
					return true;
				case PROPERTY_31_MARGINHEDGED_ID:
					controller.MarginHedged = (Double) value;
					return true;
				case PROPERTY_32_MARGINREQUIRED_ID:
					controller.MarginRequired = (Double) value;
					return true;
				case PROPERTY_33_FREEZELEVEL_ID:
					controller.FreezeLevel = (Double) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolContext controller, GreenRmiObjectBuffer buffer)
		{
			controller.Parent = (IEnvironmentRuntime) buffer.ChangedProps[SymbolContextProps.PROPERTY_1_PARENT_ID];
			controller.Symbol = (symbol) buffer.ChangedProps[SymbolContextProps.PROPERTY_2_SYMBOL_ID];
			controller.Orders = (IOrdersTable) buffer.ChangedProps[SymbolContextProps.PROPERTY_3_ORDERS_ID];
		}

		public static void AddDependencies(ISymbolContext controller)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.Orders);
		}

		public static void SerializationRead(ISymbolContext controller, SerializationInfo info, StreamingContext context)
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

		public static void SerializationWrite(ISymbolContext controller, SerializationInfo info, StreamingContext context)
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

		public event PropertyChangedEventHandler RuntimeChanged;
		public event PropertyChangedEventHandler LowChanged;
		public event PropertyChangedEventHandler HighChanged;
		public event PropertyChangedEventHandler TimeChanged;
		public event PropertyChangedEventHandler BidChanged;
		public event PropertyChangedEventHandler AskChanged;
		public event PropertyChangedEventHandler PointChanged;
		public event PropertyChangedEventHandler TraditionalPipChanged;
		public event PropertyChangedEventHandler DigitsChanged;
		public event PropertyChangedEventHandler SpreadChanged;
		public event PropertyChangedEventHandler StopLevelChanged;
		public event PropertyChangedEventHandler LotSizeChanged;
		public event PropertyChangedEventHandler TickValueChanged;
		public event PropertyChangedEventHandler TickSizeChanged;
		public event PropertyChangedEventHandler SwapLongChanged;
		public event PropertyChangedEventHandler SwapShortChanged;
		public event PropertyChangedEventHandler StartingChanged;
		public event PropertyChangedEventHandler ExpirationChanged;
		public event PropertyChangedEventHandler TradeAllowedChanged;
		public event PropertyChangedEventHandler MinLotChanged;
		public event PropertyChangedEventHandler LotStepChanged;
		public event PropertyChangedEventHandler MaxLotChanged;
		public event PropertyChangedEventHandler SwapTypeChanged;
		public event PropertyChangedEventHandler ProfitCalcModeChanged;
		public event PropertyChangedEventHandler MarginCalcModeChanged;
		public event PropertyChangedEventHandler MarginInitChanged;
		public event PropertyChangedEventHandler MarginMaintenanceChanged;
		public event PropertyChangedEventHandler MarginHedgedChanged;
		public event PropertyChangedEventHandler MarginRequiredChanged;
		public event PropertyChangedEventHandler FreezeLevelChanged;

		public SymbolContextBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SymbolContextProps.AddDependencies(this);
		}

		public SymbolContextBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolContextProps.Initialize(this, buffer);
			___initialized = true;
			SymbolContextProps.AddDependencies(this);
		}

		protected SymbolContextBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolContextProps.SerializationRead(this, info, context);
			___initialized = true;
			SymbolContextProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolContextProps.SerializationWrite(this, info, context);
		}

		public abstract Double GetValue(MarketInfoConst field);

		IEnvironmentRuntime parent;
		public IEnvironmentRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[SymbolContextProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol symbol;
		public symbol Symbol
		{
			get {
				return symbol;
			}
			set {
				if (!___initialized) {
					symbol= value;
					changed[SymbolContextProps.PROPERTY_2_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTable orders;
		public IOrdersTable Orders
		{
			get {
				return orders;
			}
			set {
				if (!___initialized) {
					orders= value;
					changed[SymbolContextProps.PROPERTY_3_ORDERS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime runtime;
		public virtual ISymbolRuntime Runtime
		{
			get {
				return runtime;
			}
			set {
				if (runtime != value) {
					runtime= value;
					changed[SymbolContextProps.PROPERTY_4_RUNTIME_ID] = true;
					if (RuntimeChanged != null)
						RuntimeChanged(this, new PropertyChangedEventArgs("Runtime", value));
				}
			}
		}

		Double low;
		public Double Low
		{
			get {
				return low;
			}
			set {
				if (low != value) {
					low= value;
					changed[SymbolContextProps.PROPERTY_5_LOW_ID] = true;
					if (LowChanged != null)
						LowChanged(this, new PropertyChangedEventArgs("Low", value));
				}
			}
		}

		Double high;
		public Double High
		{
			get {
				return high;
			}
			set {
				if (high != value) {
					high= value;
					changed[SymbolContextProps.PROPERTY_6_HIGH_ID] = true;
					if (HighChanged != null)
						HighChanged(this, new PropertyChangedEventArgs("High", value));
				}
			}
		}

		datetime time;
		public datetime Time
		{
			get {
				return time;
			}
			set {
				if (time != value) {
					time= value;
					changed[SymbolContextProps.PROPERTY_7_TIME_ID] = true;
					if (TimeChanged != null)
						TimeChanged(this, new PropertyChangedEventArgs("Time", value));
				}
			}
		}

		Double bid;
		public Double Bid
		{
			get {
				return bid;
			}
			set {
				if (bid != value) {
					bid= value;
					changed[SymbolContextProps.PROPERTY_8_BID_ID] = true;
					if (BidChanged != null)
						BidChanged(this, new PropertyChangedEventArgs("Bid", value));
				}
			}
		}

		Double ask;
		public Double Ask
		{
			get {
				return ask;
			}
			set {
				if (ask != value) {
					ask= value;
					changed[SymbolContextProps.PROPERTY_9_ASK_ID] = true;
					if (AskChanged != null)
						AskChanged(this, new PropertyChangedEventArgs("Ask", value));
				}
			}
		}

		Double point;
		public Double Point
		{
			get {
				return point;
			}
			set {
				if (point != value) {
					point= value;
					changed[SymbolContextProps.PROPERTY_10_POINT_ID] = true;
					if (PointChanged != null)
						PointChanged(this, new PropertyChangedEventArgs("Point", value));
				}
			}
		}

		Double traditionalPip;
		public Double TraditionalPip
		{
			get {
				return traditionalPip;
			}
			set {
				if (traditionalPip != value) {
					traditionalPip= value;
					changed[SymbolContextProps.PROPERTY_11_TRADITIONALPIP_ID] = true;
					if (TraditionalPipChanged != null)
						TraditionalPipChanged(this, new PropertyChangedEventArgs("TraditionalPip", value));
				}
			}
		}

		Int32 digits;
		public Int32 Digits
		{
			get {
				return digits;
			}
			set {
				if (digits != value) {
					digits= value;
					changed[SymbolContextProps.PROPERTY_12_DIGITS_ID] = true;
					if (DigitsChanged != null)
						DigitsChanged(this, new PropertyChangedEventArgs("Digits", value));
				}
			}
		}

		Int32 spread;
		public Int32 Spread
		{
			get {
				return spread;
			}
			set {
				if (spread != value) {
					spread= value;
					changed[SymbolContextProps.PROPERTY_13_SPREAD_ID] = true;
					if (SpreadChanged != null)
						SpreadChanged(this, new PropertyChangedEventArgs("Spread", value));
				}
			}
		}

		Int32 stopLevel;
		public Int32 StopLevel
		{
			get {
				return stopLevel;
			}
			set {
				if (stopLevel != value) {
					stopLevel= value;
					changed[SymbolContextProps.PROPERTY_14_STOPLEVEL_ID] = true;
					if (StopLevelChanged != null)
						StopLevelChanged(this, new PropertyChangedEventArgs("StopLevel", value));
				}
			}
		}

		Double lotSize;
		public Double LotSize
		{
			get {
				return lotSize;
			}
			set {
				if (lotSize != value) {
					lotSize= value;
					changed[SymbolContextProps.PROPERTY_15_LOTSIZE_ID] = true;
					if (LotSizeChanged != null)
						LotSizeChanged(this, new PropertyChangedEventArgs("LotSize", value));
				}
			}
		}

		Double tickValue;
		public Double TickValue
		{
			get {
				return tickValue;
			}
			set {
				if (tickValue != value) {
					tickValue= value;
					changed[SymbolContextProps.PROPERTY_16_TICKVALUE_ID] = true;
					if (TickValueChanged != null)
						TickValueChanged(this, new PropertyChangedEventArgs("TickValue", value));
				}
			}
		}

		Double tickSize;
		public Double TickSize
		{
			get {
				return tickSize;
			}
			set {
				if (tickSize != value) {
					tickSize= value;
					changed[SymbolContextProps.PROPERTY_17_TICKSIZE_ID] = true;
					if (TickSizeChanged != null)
						TickSizeChanged(this, new PropertyChangedEventArgs("TickSize", value));
				}
			}
		}

		Double swapLong;
		public Double SwapLong
		{
			get {
				return swapLong;
			}
			set {
				if (swapLong != value) {
					swapLong= value;
					changed[SymbolContextProps.PROPERTY_18_SWAPLONG_ID] = true;
					if (SwapLongChanged != null)
						SwapLongChanged(this, new PropertyChangedEventArgs("SwapLong", value));
				}
			}
		}

		Double swapShort;
		public Double SwapShort
		{
			get {
				return swapShort;
			}
			set {
				if (swapShort != value) {
					swapShort= value;
					changed[SymbolContextProps.PROPERTY_19_SWAPSHORT_ID] = true;
					if (SwapShortChanged != null)
						SwapShortChanged(this, new PropertyChangedEventArgs("SwapShort", value));
				}
			}
		}

		datetime starting;
		public datetime Starting
		{
			get {
				return starting;
			}
			set {
				if (starting != value) {
					starting= value;
					changed[SymbolContextProps.PROPERTY_20_STARTING_ID] = true;
					if (StartingChanged != null)
						StartingChanged(this, new PropertyChangedEventArgs("Starting", value));
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
					changed[SymbolContextProps.PROPERTY_21_EXPIRATION_ID] = true;
					if (ExpirationChanged != null)
						ExpirationChanged(this, new PropertyChangedEventArgs("Expiration", value));
				}
			}
		}

		Boolean tradeAllowed;
		public Boolean TradeAllowed
		{
			get {
				return tradeAllowed;
			}
			set {
				if (tradeAllowed != value) {
					tradeAllowed= value;
					changed[SymbolContextProps.PROPERTY_22_TRADEALLOWED_ID] = true;
					if (TradeAllowedChanged != null)
						TradeAllowedChanged(this, new PropertyChangedEventArgs("TradeAllowed", value));
				}
			}
		}

		Double minLot;
		public Double MinLot
		{
			get {
				return minLot;
			}
			set {
				if (minLot != value) {
					minLot= value;
					changed[SymbolContextProps.PROPERTY_23_MINLOT_ID] = true;
					if (MinLotChanged != null)
						MinLotChanged(this, new PropertyChangedEventArgs("MinLot", value));
				}
			}
		}

		Double lotStep;
		public Double LotStep
		{
			get {
				return lotStep;
			}
			set {
				if (lotStep != value) {
					lotStep= value;
					changed[SymbolContextProps.PROPERTY_24_LOTSTEP_ID] = true;
					if (LotStepChanged != null)
						LotStepChanged(this, new PropertyChangedEventArgs("LotStep", value));
				}
			}
		}

		Double maxLot;
		public Double MaxLot
		{
			get {
				return maxLot;
			}
			set {
				if (maxLot != value) {
					maxLot= value;
					changed[SymbolContextProps.PROPERTY_25_MAXLOT_ID] = true;
					if (MaxLotChanged != null)
						MaxLotChanged(this, new PropertyChangedEventArgs("MaxLot", value));
				}
			}
		}

		SwapCalculationMethod swapType;
		public SwapCalculationMethod SwapType
		{
			get {
				return swapType;
			}
			set {
				if (swapType != value) {
					swapType= value;
					changed[SymbolContextProps.PROPERTY_26_SWAPTYPE_ID] = true;
					if (SwapTypeChanged != null)
						SwapTypeChanged(this, new PropertyChangedEventArgs("SwapType", value));
				}
			}
		}

		ProfitCalculationMode profitCalcMode;
		public ProfitCalculationMode ProfitCalcMode
		{
			get {
				return profitCalcMode;
			}
			set {
				if (profitCalcMode != value) {
					profitCalcMode= value;
					changed[SymbolContextProps.PROPERTY_27_PROFITCALCMODE_ID] = true;
					if (ProfitCalcModeChanged != null)
						ProfitCalcModeChanged(this, new PropertyChangedEventArgs("ProfitCalcMode", value));
				}
			}
		}

		MarginCalculationMode marginCalcMode;
		public MarginCalculationMode MarginCalcMode
		{
			get {
				return marginCalcMode;
			}
			set {
				if (marginCalcMode != value) {
					marginCalcMode= value;
					changed[SymbolContextProps.PROPERTY_28_MARGINCALCMODE_ID] = true;
					if (MarginCalcModeChanged != null)
						MarginCalcModeChanged(this, new PropertyChangedEventArgs("MarginCalcMode", value));
				}
			}
		}

		Double marginInit;
		public Double MarginInit
		{
			get {
				return marginInit;
			}
			set {
				if (marginInit != value) {
					marginInit= value;
					changed[SymbolContextProps.PROPERTY_29_MARGININIT_ID] = true;
					if (MarginInitChanged != null)
						MarginInitChanged(this, new PropertyChangedEventArgs("MarginInit", value));
				}
			}
		}

		Double marginMaintenance;
		public Double MarginMaintenance
		{
			get {
				return marginMaintenance;
			}
			set {
				if (marginMaintenance != value) {
					marginMaintenance= value;
					changed[SymbolContextProps.PROPERTY_30_MARGINMAINTENANCE_ID] = true;
					if (MarginMaintenanceChanged != null)
						MarginMaintenanceChanged(this, new PropertyChangedEventArgs("MarginMaintenance", value));
				}
			}
		}

		Double marginHedged;
		public Double MarginHedged
		{
			get {
				return marginHedged;
			}
			set {
				if (marginHedged != value) {
					marginHedged= value;
					changed[SymbolContextProps.PROPERTY_31_MARGINHEDGED_ID] = true;
					if (MarginHedgedChanged != null)
						MarginHedgedChanged(this, new PropertyChangedEventArgs("MarginHedged", value));
				}
			}
		}

		Double marginRequired;
		public Double MarginRequired
		{
			get {
				return marginRequired;
			}
			set {
				if (marginRequired != value) {
					marginRequired= value;
					changed[SymbolContextProps.PROPERTY_32_MARGINREQUIRED_ID] = true;
					if (MarginRequiredChanged != null)
						MarginRequiredChanged(this, new PropertyChangedEventArgs("MarginRequired", value));
				}
			}
		}

		Double freezeLevel;
		public Double FreezeLevel
		{
			get {
				return freezeLevel;
			}
			set {
				if (freezeLevel != value) {
					freezeLevel= value;
					changed[SymbolContextProps.PROPERTY_33_FREEZELEVEL_ID] = true;
					if (FreezeLevelChanged != null)
						FreezeLevelChanged(this, new PropertyChangedEventArgs("FreezeLevel", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolContextProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!SymbolContextProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
