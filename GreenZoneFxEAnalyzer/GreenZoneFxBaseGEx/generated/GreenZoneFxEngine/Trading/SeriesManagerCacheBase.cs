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
	public static class SeriesManagerCacheProps
	{
		public const int PROPERTY_1_PARENT_ID = 1;
		public const int PROPERTY_2_SYMBOLRUNTIME_ID = 2;
		public const int PROPERTY_3_INDICATORS_ID = 3;
		public const int PROPERTY_4_SYMBOL_ID = 4;
		public const int PROPERTY_5_PERIOD_ID = 5;
		public const int PROPERTY_6_BID_ID = 6;
		public const int PROPERTY_7_ASK_ID = 7;
		public const int PROPERTY_8_POINT_ID = 8;
		public const int PROPERTY_9_DIGITS_ID = 9;
		public const int PROPERTY_10_SOPEN_ID = 10;
		public const int PROPERTY_11_SLOW_ID = 11;
		public const int PROPERTY_12_SHIGH_ID = 12;
		public const int PROPERTY_13_SCLOSE_ID = 13;
		public const int PROPERTY_14_SBIDS_ID = 14;
		public const int PROPERTY_15_SASKS_ID = 15;
		public const int PROPERTY_16_SLTIME_ID = 16;
		public const int PROPERTY_17_SVOLUME_ID = 17;
		public const int PROPERTY_18_OPEN_ID = 18;
		public const int PROPERTY_19_LOW_ID = 19;
		public const int PROPERTY_20_HIGH_ID = 20;
		public const int PROPERTY_21_CLOSE_ID = 21;
		public const int PROPERTY_22_BIDS_ID = 22;
		public const int PROPERTY_23_ASKS_ID = 23;
		public const int PROPERTY_24_LTIME_ID = 24;
		public const int PROPERTY_25_TIME_ID = 25;
		public const int PROPERTY_26_VOLUME_ID = 26;
		public static bool RmiGetProperty(ISeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SeriesManagerCacheProps.PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case SeriesManagerCacheProps.PROPERTY_2_SYMBOLRUNTIME_ID:
					value = controller.SymbolRuntime;
					return true;
				case SeriesManagerCacheProps.PROPERTY_3_INDICATORS_ID:
					value = controller.Indicators;
					return true;
				case SeriesManagerCacheProps.PROPERTY_4_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case SeriesManagerCacheProps.PROPERTY_5_PERIOD_ID:
					value = controller.Period;
					return true;
				case SeriesManagerCacheProps.PROPERTY_6_BID_ID:
					value = controller.Bid;
					return true;
				case SeriesManagerCacheProps.PROPERTY_7_ASK_ID:
					value = controller.Ask;
					return true;
				case SeriesManagerCacheProps.PROPERTY_8_POINT_ID:
					value = controller.Point;
					return true;
				case SeriesManagerCacheProps.PROPERTY_9_DIGITS_ID:
					value = controller.Digits;
					return true;
				case SeriesManagerCacheProps.PROPERTY_10_SOPEN_ID:
					value = controller.sOpen;
					return true;
				case SeriesManagerCacheProps.PROPERTY_11_SLOW_ID:
					value = controller.sLow;
					return true;
				case SeriesManagerCacheProps.PROPERTY_12_SHIGH_ID:
					value = controller.sHigh;
					return true;
				case SeriesManagerCacheProps.PROPERTY_13_SCLOSE_ID:
					value = controller.sClose;
					return true;
				case SeriesManagerCacheProps.PROPERTY_14_SBIDS_ID:
					value = controller.sBids;
					return true;
				case SeriesManagerCacheProps.PROPERTY_15_SASKS_ID:
					value = controller.sAsks;
					return true;
				case SeriesManagerCacheProps.PROPERTY_16_SLTIME_ID:
					value = controller.sLTime;
					return true;
				case SeriesManagerCacheProps.PROPERTY_17_SVOLUME_ID:
					value = controller.sVolume;
					return true;
				case SeriesManagerCacheProps.PROPERTY_18_OPEN_ID:
					value = controller.Open;
					return true;
				case SeriesManagerCacheProps.PROPERTY_19_LOW_ID:
					value = controller.Low;
					return true;
				case SeriesManagerCacheProps.PROPERTY_20_HIGH_ID:
					value = controller.High;
					return true;
				case SeriesManagerCacheProps.PROPERTY_21_CLOSE_ID:
					value = controller.Close;
					return true;
				case SeriesManagerCacheProps.PROPERTY_22_BIDS_ID:
					value = controller.Bids;
					return true;
				case SeriesManagerCacheProps.PROPERTY_23_ASKS_ID:
					value = controller.Asks;
					return true;
				case SeriesManagerCacheProps.PROPERTY_24_LTIME_ID:
					value = controller.LTime;
					return true;
				case SeriesManagerCacheProps.PROPERTY_25_TIME_ID:
					value = controller.Time;
					return true;
				case SeriesManagerCacheProps.PROPERTY_26_VOLUME_ID:
					value = controller.Volume;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SeriesManagerCacheProps.PROPERTY_10_SOPEN_ID:
					controller.sOpen = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_11_SLOW_ID:
					controller.sLow = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_12_SHIGH_ID:
					controller.sHigh = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_13_SCLOSE_ID:
					controller.sClose = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_14_SBIDS_ID:
					controller.sBids = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_15_SASKS_ID:
					controller.sAsks = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_16_SLTIME_ID:
					controller.sLTime = (LArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_17_SVOLUME_ID:
					controller.sVolume = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_18_OPEN_ID:
					controller.Open = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_19_LOW_ID:
					controller.Low = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_20_HIGH_ID:
					controller.High = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_21_CLOSE_ID:
					controller.Close = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_22_BIDS_ID:
					controller.Bids = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_23_ASKS_ID:
					controller.Asks = (DArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_24_LTIME_ID:
					controller.LTime = (LArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_25_TIME_ID:
					controller.Time = (LArrAsIArr) value;
					return true;
				case SeriesManagerCacheProps.PROPERTY_26_VOLUME_ID:
					controller.Volume = (DArr) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Parent = (ISeriesManagerRuntime) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_1_PARENT_ID];
			controller.SymbolRuntime = (ISymbolRuntime) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_2_SYMBOLRUNTIME_ID];
			controller.Indicators = (Dictionary<IndicatorId,IIndicatorRuntime>) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_3_INDICATORS_ID];
			controller.Symbol = (symbol) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_4_SYMBOL_ID];
			controller.Period = (TimePeriodConst) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_5_PERIOD_ID];
			controller.Bid = (Double) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_6_BID_ID];
			controller.Ask = (Double) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_7_ASK_ID];
			controller.Point = (Double) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_8_POINT_ID];
			controller.Digits = (Int32) buffer.ChangedProps[SeriesManagerCacheProps.PROPERTY_9_DIGITS_ID];
		}

		public static void AddDependencies(ISeriesManagerCache controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.SymbolRuntime);
			controller.Dependencies.AddRange(controller.Indicators.Values);
		}

		public static void SerializationRead(ISeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Parent = (ISeriesManagerRuntime) info.GetValue("Parent", typeof(ISeriesManagerRuntime));
			controller.SymbolRuntime = (ISymbolRuntime) info.GetValue("SymbolRuntime", typeof(ISymbolRuntime));
			controller.Indicators = (Dictionary<IndicatorId,IIndicatorRuntime>) info.GetValue("Indicators", typeof(Dictionary<IndicatorId,IIndicatorRuntime>));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Period = (TimePeriodConst) info.GetValue("Period", typeof(TimePeriodConst));
			controller.Bid = (Double) info.GetValue("Bid", typeof(Double));
			controller.Ask = (Double) info.GetValue("Ask", typeof(Double));
			controller.Point = (Double) info.GetValue("Point", typeof(Double));
			controller.Digits = (Int32) info.GetValue("Digits", typeof(Int32));
			controller.sOpen = (DArr) info.GetValue("sOpen", typeof(DArr));
			controller.sLow = (DArr) info.GetValue("sLow", typeof(DArr));
			controller.sHigh = (DArr) info.GetValue("sHigh", typeof(DArr));
			controller.sClose = (DArr) info.GetValue("sClose", typeof(DArr));
			controller.sBids = (DArr) info.GetValue("sBids", typeof(DArr));
			controller.sAsks = (DArr) info.GetValue("sAsks", typeof(DArr));
			controller.sLTime = (LArr) info.GetValue("sLTime", typeof(LArr));
			controller.sVolume = (DArr) info.GetValue("sVolume", typeof(DArr));
			controller.Open = (DArr) info.GetValue("Open", typeof(DArr));
			controller.Low = (DArr) info.GetValue("Low", typeof(DArr));
			controller.High = (DArr) info.GetValue("High", typeof(DArr));
			controller.Close = (DArr) info.GetValue("Close", typeof(DArr));
			controller.Bids = (DArr) info.GetValue("Bids", typeof(DArr));
			controller.Asks = (DArr) info.GetValue("Asks", typeof(DArr));
			controller.LTime = (LArr) info.GetValue("LTime", typeof(LArr));
			controller.Time = (LArrAsIArr) info.GetValue("Time", typeof(LArrAsIArr));
			controller.Volume = (DArr) info.GetValue("Volume", typeof(DArr));
		}

		public static void SerializationWrite(ISeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SymbolRuntime", controller.SymbolRuntime);
			info.AddValue("Indicators", controller.Indicators);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Period", controller.Period);
			info.AddValue("Bid", controller.Bid);
			info.AddValue("Ask", controller.Ask);
			info.AddValue("Point", controller.Point);
			info.AddValue("Digits", controller.Digits);
			info.AddValue("sOpen", controller.sOpen);
			info.AddValue("sLow", controller.sLow);
			info.AddValue("sHigh", controller.sHigh);
			info.AddValue("sClose", controller.sClose);
			info.AddValue("sBids", controller.sBids);
			info.AddValue("sAsks", controller.sAsks);
			info.AddValue("sLTime", controller.sLTime);
			info.AddValue("sVolume", controller.sVolume);
			info.AddValue("Open", controller.Open);
			info.AddValue("Low", controller.Low);
			info.AddValue("High", controller.High);
			info.AddValue("Close", controller.Close);
			info.AddValue("Bids", controller.Bids);
			info.AddValue("Asks", controller.Asks);
			info.AddValue("LTime", controller.LTime);
			info.AddValue("Time", controller.Time);
			info.AddValue("Volume", controller.Volume);
		}

	}
	public abstract class SeriesManagerCacheBase : TradingConst, ISeriesManagerCache
	{

		bool ___initialized = false;

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return Indicators.Values.GetEnumerator(); }

		public event PropertyChangedEventHandler ISeriesManagerCache_sOpen_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sLow_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sHigh_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sClose_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sBids_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sAsks_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sLTime_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_sVolume_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Open_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Low_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_High_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Close_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Bids_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Asks_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_LTime_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Time_Changed;
		public event PropertyChangedEventHandler ISeriesManagerCache_Volume_Changed;

		public SeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SeriesManagerCacheProps.AddDependencies(this, false);
		}

		public SeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			SeriesManagerCacheProps.AddDependencies(this, false);
		}

		protected SeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			SeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}

		public abstract ISeriesArr GetArray(SeriesArrayPool type);

		public abstract IIndicatorRuntime Get(Mt4ExecutableInfo ind, Object[] args, Boolean autoCreate);

		public abstract IIndicatorRuntime Get(Mt4ExecutableInfo ind, String argKey, Object[] args, Boolean autoCreate);

		public abstract IIndicatorRuntime AddIndicator(Mt4ExecutableInfo ind, Object[] args);

		public abstract IndicatorId AddIndicator(IIndicatorRuntime indicator, Boolean forceUpdate);

		public abstract IndicatorId ReplaceIndicator(IndicatorId sp0, IIndicatorRuntime indicator, Boolean forceUpdate);

		public abstract IIndicatorRuntime RemoveIndicator(Mt4ExecutableInfo ind, Object[] args);

		public abstract IIndicatorRuntime RemoveIndicator(IndicatorId sp);

		public abstract Double GetIndicatorValue(String name, IndicatorLine _mode, Int32 shift, params Object[] args);

		public abstract Double GetIndicatorValue(String name, Int32 mode, Int32 shift, params Object[] args);

		public abstract Double GetIndicatorValue<T>(IndicatorLine _mode, Int32 shift, params Object[] args)
			where T : IIndicatorRuntime;

		public abstract Double GetIndicatorValue<T>(Int32 mode, Int32 shift, params Object[] args)
			where T : IIndicatorRuntime;

		public IEnumerator<IIndicatorRuntime> GetEnumerator()
		{
			return Indicators.Values.GetEnumerator();
		}


		public virtual IEnvironmentRuntime Environment
		{
			get {
				return Parent.Environment;
			}
		}

		ISeriesManagerRuntime _ISeriesManagerCache_Parent;
		public ISeriesManagerRuntime Parent
		{
			get {
				return _ISeriesManagerCache_Parent;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Parent= value;
					changed[SeriesManagerCacheProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime _ISeriesManagerCache_SymbolRuntime;
		public ISymbolRuntime SymbolRuntime
		{
			get {
				return _ISeriesManagerCache_SymbolRuntime;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_SymbolRuntime= value;
					changed[SeriesManagerCacheProps.PROPERTY_2_SYMBOLRUNTIME_ID] = true;
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

		Dictionary<IndicatorId,IIndicatorRuntime> _ISeriesManagerCache_Indicators;
		public Dictionary<IndicatorId,IIndicatorRuntime> Indicators
		{
			get {
				return _ISeriesManagerCache_Indicators;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Indicators= value;
					changed[SeriesManagerCacheProps.PROPERTY_3_INDICATORS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol _ISeriesManagerCache_Symbol;
		public symbol Symbol
		{
			get {
				return _ISeriesManagerCache_Symbol;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Symbol= value;
					changed[SeriesManagerCacheProps.PROPERTY_4_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TimePeriodConst _ISeriesManagerCache_Period;
		public TimePeriodConst Period
		{
			get {
				return _ISeriesManagerCache_Period;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Period= value;
					changed[SeriesManagerCacheProps.PROPERTY_5_PERIOD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Double _ISeriesManagerCache_Bid;
		public Double Bid
		{
			get {
				return _ISeriesManagerCache_Bid;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Bid= value;
					changed[SeriesManagerCacheProps.PROPERTY_6_BID_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Double _ISeriesManagerCache_Ask;
		public Double Ask
		{
			get {
				return _ISeriesManagerCache_Ask;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Ask= value;
					changed[SeriesManagerCacheProps.PROPERTY_7_ASK_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Double _ISeriesManagerCache_Point;
		public Double Point
		{
			get {
				return _ISeriesManagerCache_Point;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Point= value;
					changed[SeriesManagerCacheProps.PROPERTY_8_POINT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Int32 _ISeriesManagerCache_Digits;
		public Int32 Digits
		{
			get {
				return _ISeriesManagerCache_Digits;
			}
			set {
				if (!___initialized) {
					_ISeriesManagerCache_Digits= value;
					changed[SeriesManagerCacheProps.PROPERTY_9_DIGITS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual datetime FocusedTime
		{
			get {
				return (datetime)sLTime[0];
			}
		}

		public abstract Int32 Bars
		{
			get ;
		}

		DArr _ISeriesManagerCache_sOpen;
		public DArr sOpen
		{
			get {
				return _ISeriesManagerCache_sOpen;
			}
			set {
				if (_ISeriesManagerCache_sOpen != value) {
					_ISeriesManagerCache_sOpen= value;
					changed[SeriesManagerCacheProps.PROPERTY_10_SOPEN_ID] = true;
					if (ISeriesManagerCache_sOpen_Changed != null)
						ISeriesManagerCache_sOpen_Changed(this, new PropertyChangedEventArgs("sOpen", value));
				}
			}
		}

		DArr _ISeriesManagerCache_sLow;
		public DArr sLow
		{
			get {
				return _ISeriesManagerCache_sLow;
			}
			set {
				if (_ISeriesManagerCache_sLow != value) {
					_ISeriesManagerCache_sLow= value;
					changed[SeriesManagerCacheProps.PROPERTY_11_SLOW_ID] = true;
					if (ISeriesManagerCache_sLow_Changed != null)
						ISeriesManagerCache_sLow_Changed(this, new PropertyChangedEventArgs("sLow", value));
				}
			}
		}

		DArr _ISeriesManagerCache_sHigh;
		public DArr sHigh
		{
			get {
				return _ISeriesManagerCache_sHigh;
			}
			set {
				if (_ISeriesManagerCache_sHigh != value) {
					_ISeriesManagerCache_sHigh= value;
					changed[SeriesManagerCacheProps.PROPERTY_12_SHIGH_ID] = true;
					if (ISeriesManagerCache_sHigh_Changed != null)
						ISeriesManagerCache_sHigh_Changed(this, new PropertyChangedEventArgs("sHigh", value));
				}
			}
		}

		DArr _ISeriesManagerCache_sClose;
		public DArr sClose
		{
			get {
				return _ISeriesManagerCache_sClose;
			}
			set {
				if (_ISeriesManagerCache_sClose != value) {
					_ISeriesManagerCache_sClose= value;
					changed[SeriesManagerCacheProps.PROPERTY_13_SCLOSE_ID] = true;
					if (ISeriesManagerCache_sClose_Changed != null)
						ISeriesManagerCache_sClose_Changed(this, new PropertyChangedEventArgs("sClose", value));
				}
			}
		}

		DArr _ISeriesManagerCache_sBids;
		public DArr sBids
		{
			get {
				return _ISeriesManagerCache_sBids;
			}
			set {
				if (_ISeriesManagerCache_sBids != value) {
					_ISeriesManagerCache_sBids= value;
					changed[SeriesManagerCacheProps.PROPERTY_14_SBIDS_ID] = true;
					if (ISeriesManagerCache_sBids_Changed != null)
						ISeriesManagerCache_sBids_Changed(this, new PropertyChangedEventArgs("sBids", value));
				}
			}
		}

		DArr _ISeriesManagerCache_sAsks;
		public DArr sAsks
		{
			get {
				return _ISeriesManagerCache_sAsks;
			}
			set {
				if (_ISeriesManagerCache_sAsks != value) {
					_ISeriesManagerCache_sAsks= value;
					changed[SeriesManagerCacheProps.PROPERTY_15_SASKS_ID] = true;
					if (ISeriesManagerCache_sAsks_Changed != null)
						ISeriesManagerCache_sAsks_Changed(this, new PropertyChangedEventArgs("sAsks", value));
				}
			}
		}

		LArr _ISeriesManagerCache_sLTime;
		public LArr sLTime
		{
			get {
				return _ISeriesManagerCache_sLTime;
			}
			set {
				if (_ISeriesManagerCache_sLTime != value) {
					_ISeriesManagerCache_sLTime= value;
					changed[SeriesManagerCacheProps.PROPERTY_16_SLTIME_ID] = true;
					if (ISeriesManagerCache_sLTime_Changed != null)
						ISeriesManagerCache_sLTime_Changed(this, new PropertyChangedEventArgs("sLTime", value));
				}
			}
		}

		DArr _ISeriesManagerCache_sVolume;
		public DArr sVolume
		{
			get {
				return _ISeriesManagerCache_sVolume;
			}
			set {
				if (_ISeriesManagerCache_sVolume != value) {
					_ISeriesManagerCache_sVolume= value;
					changed[SeriesManagerCacheProps.PROPERTY_17_SVOLUME_ID] = true;
					if (ISeriesManagerCache_sVolume_Changed != null)
						ISeriesManagerCache_sVolume_Changed(this, new PropertyChangedEventArgs("sVolume", value));
				}
			}
		}

		DArr _ISeriesManagerCache_Open;
		public DArr Open
		{
			get {
				return _ISeriesManagerCache_Open;
			}
			set {
				if (_ISeriesManagerCache_Open != value) {
					_ISeriesManagerCache_Open= value;
					changed[SeriesManagerCacheProps.PROPERTY_18_OPEN_ID] = true;
					if (ISeriesManagerCache_Open_Changed != null)
						ISeriesManagerCache_Open_Changed(this, new PropertyChangedEventArgs("Open", value));
				}
			}
		}

		DArr _ISeriesManagerCache_Low;
		public DArr Low
		{
			get {
				return _ISeriesManagerCache_Low;
			}
			set {
				if (_ISeriesManagerCache_Low != value) {
					_ISeriesManagerCache_Low= value;
					changed[SeriesManagerCacheProps.PROPERTY_19_LOW_ID] = true;
					if (ISeriesManagerCache_Low_Changed != null)
						ISeriesManagerCache_Low_Changed(this, new PropertyChangedEventArgs("Low", value));
				}
			}
		}

		DArr _ISeriesManagerCache_High;
		public DArr High
		{
			get {
				return _ISeriesManagerCache_High;
			}
			set {
				if (_ISeriesManagerCache_High != value) {
					_ISeriesManagerCache_High= value;
					changed[SeriesManagerCacheProps.PROPERTY_20_HIGH_ID] = true;
					if (ISeriesManagerCache_High_Changed != null)
						ISeriesManagerCache_High_Changed(this, new PropertyChangedEventArgs("High", value));
				}
			}
		}

		DArr _ISeriesManagerCache_Close;
		public DArr Close
		{
			get {
				return _ISeriesManagerCache_Close;
			}
			set {
				if (_ISeriesManagerCache_Close != value) {
					_ISeriesManagerCache_Close= value;
					changed[SeriesManagerCacheProps.PROPERTY_21_CLOSE_ID] = true;
					if (ISeriesManagerCache_Close_Changed != null)
						ISeriesManagerCache_Close_Changed(this, new PropertyChangedEventArgs("Close", value));
				}
			}
		}

		DArr _ISeriesManagerCache_Bids;
		public DArr Bids
		{
			get {
				return _ISeriesManagerCache_Bids;
			}
			set {
				if (_ISeriesManagerCache_Bids != value) {
					_ISeriesManagerCache_Bids= value;
					changed[SeriesManagerCacheProps.PROPERTY_22_BIDS_ID] = true;
					if (ISeriesManagerCache_Bids_Changed != null)
						ISeriesManagerCache_Bids_Changed(this, new PropertyChangedEventArgs("Bids", value));
				}
			}
		}

		DArr _ISeriesManagerCache_Asks;
		public DArr Asks
		{
			get {
				return _ISeriesManagerCache_Asks;
			}
			set {
				if (_ISeriesManagerCache_Asks != value) {
					_ISeriesManagerCache_Asks= value;
					changed[SeriesManagerCacheProps.PROPERTY_23_ASKS_ID] = true;
					if (ISeriesManagerCache_Asks_Changed != null)
						ISeriesManagerCache_Asks_Changed(this, new PropertyChangedEventArgs("Asks", value));
				}
			}
		}

		LArr _ISeriesManagerCache_LTime;
		public LArr LTime
		{
			get {
				return _ISeriesManagerCache_LTime;
			}
			set {
				if (_ISeriesManagerCache_LTime != value) {
					_ISeriesManagerCache_LTime= value;
					changed[SeriesManagerCacheProps.PROPERTY_24_LTIME_ID] = true;
					if (ISeriesManagerCache_LTime_Changed != null)
						ISeriesManagerCache_LTime_Changed(this, new PropertyChangedEventArgs("LTime", value));
				}
			}
		}

		LArrAsIArr _ISeriesManagerCache_Time;
		public LArrAsIArr Time
		{
			get {
				return _ISeriesManagerCache_Time;
			}
			set {
				if (_ISeriesManagerCache_Time != value) {
					_ISeriesManagerCache_Time= value;
					changed[SeriesManagerCacheProps.PROPERTY_25_TIME_ID] = true;
					if (ISeriesManagerCache_Time_Changed != null)
						ISeriesManagerCache_Time_Changed(this, new PropertyChangedEventArgs("Time", value));
				}
			}
		}

		DArr _ISeriesManagerCache_Volume;
		public DArr Volume
		{
			get {
				return _ISeriesManagerCache_Volume;
			}
			set {
				if (_ISeriesManagerCache_Volume != value) {
					_ISeriesManagerCache_Volume= value;
					changed[SeriesManagerCacheProps.PROPERTY_26_VOLUME_ID] = true;
					if (ISeriesManagerCache_Volume_Changed != null)
						ISeriesManagerCache_Volume_Changed(this, new PropertyChangedEventArgs("Volume", value));
				}
			}
		}

		public virtual IIndicatorRuntime  this[Mt4ExecutableInfo ind, Object[] args]
		{
			get {
				return Get(ind, args, false);
			}
		}

		public virtual IIndicatorRuntime  this[Mt4ExecutableInfo ind, String argKey, Object[] args]
		{
			get {
				return Get(ind, argKey, args, false);
			}
		}

		public virtual IIndicatorRuntime  this[IndicatorId id]
		{
			get {
				return Indicators[id];
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (SeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
