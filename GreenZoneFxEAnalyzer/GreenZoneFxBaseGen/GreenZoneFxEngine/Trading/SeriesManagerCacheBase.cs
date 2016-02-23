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
		public static bool RmiGetProperty(ISeriesManagerCache controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_2_SYMBOLRUNTIME_ID:
					value = controller.SymbolRuntime;
					return true;
				case PROPERTY_3_INDICATORS_ID:
					value = controller.Indicators;
					return true;
				case PROPERTY_4_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_5_PERIOD_ID:
					value = controller.Period;
					return true;
				case PROPERTY_6_BID_ID:
					value = controller.Bid;
					return true;
				case PROPERTY_7_ASK_ID:
					value = controller.Ask;
					return true;
				case PROPERTY_8_POINT_ID:
					value = controller.Point;
					return true;
				case PROPERTY_9_DIGITS_ID:
					value = controller.Digits;
					return true;
				case PROPERTY_10_SOPEN_ID:
					value = controller.sOpen;
					return true;
				case PROPERTY_11_SLOW_ID:
					value = controller.sLow;
					return true;
				case PROPERTY_12_SHIGH_ID:
					value = controller.sHigh;
					return true;
				case PROPERTY_13_SCLOSE_ID:
					value = controller.sClose;
					return true;
				case PROPERTY_14_SBIDS_ID:
					value = controller.sBids;
					return true;
				case PROPERTY_15_SASKS_ID:
					value = controller.sAsks;
					return true;
				case PROPERTY_16_SLTIME_ID:
					value = controller.sLTime;
					return true;
				case PROPERTY_17_SVOLUME_ID:
					value = controller.sVolume;
					return true;
				case PROPERTY_18_OPEN_ID:
					value = controller.Open;
					return true;
				case PROPERTY_19_LOW_ID:
					value = controller.Low;
					return true;
				case PROPERTY_20_HIGH_ID:
					value = controller.High;
					return true;
				case PROPERTY_21_CLOSE_ID:
					value = controller.Close;
					return true;
				case PROPERTY_22_BIDS_ID:
					value = controller.Bids;
					return true;
				case PROPERTY_23_ASKS_ID:
					value = controller.Asks;
					return true;
				case PROPERTY_24_LTIME_ID:
					value = controller.LTime;
					return true;
				case PROPERTY_25_TIME_ID:
					value = controller.Time;
					return true;
				case PROPERTY_26_VOLUME_ID:
					value = controller.Volume;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISeriesManagerCache controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_10_SOPEN_ID:
					controller.sOpen = (IDArr) value;
					return true;
				case PROPERTY_11_SLOW_ID:
					controller.sLow = (IDArr) value;
					return true;
				case PROPERTY_12_SHIGH_ID:
					controller.sHigh = (IDArr) value;
					return true;
				case PROPERTY_13_SCLOSE_ID:
					controller.sClose = (IDArr) value;
					return true;
				case PROPERTY_14_SBIDS_ID:
					controller.sBids = (IDArr) value;
					return true;
				case PROPERTY_15_SASKS_ID:
					controller.sAsks = (IDArr) value;
					return true;
				case PROPERTY_16_SLTIME_ID:
					controller.sLTime = (ILArr) value;
					return true;
				case PROPERTY_17_SVOLUME_ID:
					controller.sVolume = (IDArr) value;
					return true;
				case PROPERTY_18_OPEN_ID:
					controller.Open = (IDArr) value;
					return true;
				case PROPERTY_19_LOW_ID:
					controller.Low = (IDArr) value;
					return true;
				case PROPERTY_20_HIGH_ID:
					controller.High = (IDArr) value;
					return true;
				case PROPERTY_21_CLOSE_ID:
					controller.Close = (IDArr) value;
					return true;
				case PROPERTY_22_BIDS_ID:
					controller.Bids = (IDArr) value;
					return true;
				case PROPERTY_23_ASKS_ID:
					controller.Asks = (IDArr) value;
					return true;
				case PROPERTY_24_LTIME_ID:
					controller.LTime = (ILArr) value;
					return true;
				case PROPERTY_25_TIME_ID:
					controller.Time = (ILArrAsIArr) value;
					return true;
				case PROPERTY_26_VOLUME_ID:
					controller.Volume = (IDArr) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISeriesManagerCache controller, GreenRmiObjectBuffer buffer)
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

		public static void AddDependencies(ISeriesManagerCache controller)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.SymbolRuntime);
			controller.Dependencies.AddRange(controller.Indicators.Values);
		}

		public static void SerializationRead(ISeriesManagerCache controller, SerializationInfo info, StreamingContext context)
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
			controller.sOpen = (IDArr) info.GetValue("sOpen", typeof(IDArr));
			controller.sLow = (IDArr) info.GetValue("sLow", typeof(IDArr));
			controller.sHigh = (IDArr) info.GetValue("sHigh", typeof(IDArr));
			controller.sClose = (IDArr) info.GetValue("sClose", typeof(IDArr));
			controller.sBids = (IDArr) info.GetValue("sBids", typeof(IDArr));
			controller.sAsks = (IDArr) info.GetValue("sAsks", typeof(IDArr));
			controller.sLTime = (ILArr) info.GetValue("sLTime", typeof(ILArr));
			controller.sVolume = (IDArr) info.GetValue("sVolume", typeof(IDArr));
			controller.Open = (IDArr) info.GetValue("Open", typeof(IDArr));
			controller.Low = (IDArr) info.GetValue("Low", typeof(IDArr));
			controller.High = (IDArr) info.GetValue("High", typeof(IDArr));
			controller.Close = (IDArr) info.GetValue("Close", typeof(IDArr));
			controller.Bids = (IDArr) info.GetValue("Bids", typeof(IDArr));
			controller.Asks = (IDArr) info.GetValue("Asks", typeof(IDArr));
			controller.LTime = (ILArr) info.GetValue("LTime", typeof(ILArr));
			controller.Time = (ILArrAsIArr) info.GetValue("Time", typeof(ILArrAsIArr));
			controller.Volume = (IDArr) info.GetValue("Volume", typeof(IDArr));
		}

		public static void SerializationWrite(ISeriesManagerCache controller, SerializationInfo info, StreamingContext context)
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
	public abstract class SeriesManagerCacheBase : RmiBase, ISeriesManagerCache
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler sOpenChanged;
		public event PropertyChangedEventHandler sLowChanged;
		public event PropertyChangedEventHandler sHighChanged;
		public event PropertyChangedEventHandler sCloseChanged;
		public event PropertyChangedEventHandler sBidsChanged;
		public event PropertyChangedEventHandler sAsksChanged;
		public event PropertyChangedEventHandler sLTimeChanged;
		public event PropertyChangedEventHandler sVolumeChanged;
		public event PropertyChangedEventHandler OpenChanged;
		public event PropertyChangedEventHandler LowChanged;
		public event PropertyChangedEventHandler HighChanged;
		public event PropertyChangedEventHandler CloseChanged;
		public event PropertyChangedEventHandler BidsChanged;
		public event PropertyChangedEventHandler AsksChanged;
		public event PropertyChangedEventHandler LTimeChanged;
		public event PropertyChangedEventHandler TimeChanged;
		public event PropertyChangedEventHandler VolumeChanged;

		public SeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SeriesManagerCacheProps.AddDependencies(this);
		}

		public SeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SeriesManagerCacheProps.Initialize(this, buffer);
			___initialized = true;
			SeriesManagerCacheProps.AddDependencies(this);
		}

		protected SeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SeriesManagerCacheProps.SerializationRead(this, info, context);
			___initialized = true;
			SeriesManagerCacheProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SeriesManagerCacheProps.SerializationWrite(this, info, context);
		}

		ISeriesManagerRuntime parent;
		public ISeriesManagerRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[SeriesManagerCacheProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolRuntime symbolRuntime;
		public ISymbolRuntime SymbolRuntime
		{
			get {
				return symbolRuntime;
			}
			set {
				if (!___initialized) {
					symbolRuntime= value;
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

		Dictionary<IndicatorId,IIndicatorRuntime> indicators;
		public Dictionary<IndicatorId,IIndicatorRuntime> Indicators
		{
			get {
				return indicators;
			}
			set {
				if (!___initialized) {
					indicators= value;
					changed[SeriesManagerCacheProps.PROPERTY_3_INDICATORS_ID] = true;
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
					changed[SeriesManagerCacheProps.PROPERTY_4_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TimePeriodConst period;
		public TimePeriodConst Period
		{
			get {
				return period;
			}
			set {
				if (!___initialized) {
					period= value;
					changed[SeriesManagerCacheProps.PROPERTY_5_PERIOD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
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
				if (!___initialized) {
					bid= value;
					changed[SeriesManagerCacheProps.PROPERTY_6_BID_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
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
				if (!___initialized) {
					ask= value;
					changed[SeriesManagerCacheProps.PROPERTY_7_ASK_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
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
				if (!___initialized) {
					point= value;
					changed[SeriesManagerCacheProps.PROPERTY_8_POINT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
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
				if (!___initialized) {
					digits= value;
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

		IDArr _sOpen;
		public IDArr sOpen
		{
			get {
				return _sOpen;
			}
			set {
				if (_sOpen != value) {
					_sOpen= value;
					changed[SeriesManagerCacheProps.PROPERTY_10_SOPEN_ID] = true;
					if (sOpenChanged != null)
						sOpenChanged(this, new PropertyChangedEventArgs("sOpen", value));
				}
			}
		}

		IDArr _sLow;
		public IDArr sLow
		{
			get {
				return _sLow;
			}
			set {
				if (_sLow != value) {
					_sLow= value;
					changed[SeriesManagerCacheProps.PROPERTY_11_SLOW_ID] = true;
					if (sLowChanged != null)
						sLowChanged(this, new PropertyChangedEventArgs("sLow", value));
				}
			}
		}

		IDArr _sHigh;
		public IDArr sHigh
		{
			get {
				return _sHigh;
			}
			set {
				if (_sHigh != value) {
					_sHigh= value;
					changed[SeriesManagerCacheProps.PROPERTY_12_SHIGH_ID] = true;
					if (sHighChanged != null)
						sHighChanged(this, new PropertyChangedEventArgs("sHigh", value));
				}
			}
		}

		IDArr _sClose;
		public IDArr sClose
		{
			get {
				return _sClose;
			}
			set {
				if (_sClose != value) {
					_sClose= value;
					changed[SeriesManagerCacheProps.PROPERTY_13_SCLOSE_ID] = true;
					if (sCloseChanged != null)
						sCloseChanged(this, new PropertyChangedEventArgs("sClose", value));
				}
			}
		}

		IDArr _sBids;
		public IDArr sBids
		{
			get {
				return _sBids;
			}
			set {
				if (_sBids != value) {
					_sBids= value;
					changed[SeriesManagerCacheProps.PROPERTY_14_SBIDS_ID] = true;
					if (sBidsChanged != null)
						sBidsChanged(this, new PropertyChangedEventArgs("sBids", value));
				}
			}
		}

		IDArr _sAsks;
		public IDArr sAsks
		{
			get {
				return _sAsks;
			}
			set {
				if (_sAsks != value) {
					_sAsks= value;
					changed[SeriesManagerCacheProps.PROPERTY_15_SASKS_ID] = true;
					if (sAsksChanged != null)
						sAsksChanged(this, new PropertyChangedEventArgs("sAsks", value));
				}
			}
		}

		ILArr _sLTime;
		public ILArr sLTime
		{
			get {
				return _sLTime;
			}
			set {
				if (_sLTime != value) {
					_sLTime= value;
					changed[SeriesManagerCacheProps.PROPERTY_16_SLTIME_ID] = true;
					if (sLTimeChanged != null)
						sLTimeChanged(this, new PropertyChangedEventArgs("sLTime", value));
				}
			}
		}

		IDArr _sVolume;
		public IDArr sVolume
		{
			get {
				return _sVolume;
			}
			set {
				if (_sVolume != value) {
					_sVolume= value;
					changed[SeriesManagerCacheProps.PROPERTY_17_SVOLUME_ID] = true;
					if (sVolumeChanged != null)
						sVolumeChanged(this, new PropertyChangedEventArgs("sVolume", value));
				}
			}
		}

		IDArr open;
		public IDArr Open
		{
			get {
				return open;
			}
			set {
				if (open != value) {
					open= value;
					changed[SeriesManagerCacheProps.PROPERTY_18_OPEN_ID] = true;
					if (OpenChanged != null)
						OpenChanged(this, new PropertyChangedEventArgs("Open", value));
				}
			}
		}

		IDArr low;
		public IDArr Low
		{
			get {
				return low;
			}
			set {
				if (low != value) {
					low= value;
					changed[SeriesManagerCacheProps.PROPERTY_19_LOW_ID] = true;
					if (LowChanged != null)
						LowChanged(this, new PropertyChangedEventArgs("Low", value));
				}
			}
		}

		IDArr high;
		public IDArr High
		{
			get {
				return high;
			}
			set {
				if (high != value) {
					high= value;
					changed[SeriesManagerCacheProps.PROPERTY_20_HIGH_ID] = true;
					if (HighChanged != null)
						HighChanged(this, new PropertyChangedEventArgs("High", value));
				}
			}
		}

		IDArr close;
		public IDArr Close
		{
			get {
				return close;
			}
			set {
				if (close != value) {
					close= value;
					changed[SeriesManagerCacheProps.PROPERTY_21_CLOSE_ID] = true;
					if (CloseChanged != null)
						CloseChanged(this, new PropertyChangedEventArgs("Close", value));
				}
			}
		}

		IDArr bids;
		public IDArr Bids
		{
			get {
				return bids;
			}
			set {
				if (bids != value) {
					bids= value;
					changed[SeriesManagerCacheProps.PROPERTY_22_BIDS_ID] = true;
					if (BidsChanged != null)
						BidsChanged(this, new PropertyChangedEventArgs("Bids", value));
				}
			}
		}

		IDArr asks;
		public IDArr Asks
		{
			get {
				return asks;
			}
			set {
				if (asks != value) {
					asks= value;
					changed[SeriesManagerCacheProps.PROPERTY_23_ASKS_ID] = true;
					if (AsksChanged != null)
						AsksChanged(this, new PropertyChangedEventArgs("Asks", value));
				}
			}
		}

		ILArr lTime;
		public ILArr LTime
		{
			get {
				return lTime;
			}
			set {
				if (lTime != value) {
					lTime= value;
					changed[SeriesManagerCacheProps.PROPERTY_24_LTIME_ID] = true;
					if (LTimeChanged != null)
						LTimeChanged(this, new PropertyChangedEventArgs("LTime", value));
				}
			}
		}

		ILArrAsIArr time;
		public ILArrAsIArr Time
		{
			get {
				return time;
			}
			set {
				if (time != value) {
					time= value;
					changed[SeriesManagerCacheProps.PROPERTY_25_TIME_ID] = true;
					if (TimeChanged != null)
						TimeChanged(this, new PropertyChangedEventArgs("Time", value));
				}
			}
		}

		IDArr volume;
		public IDArr Volume
		{
			get {
				return volume;
			}
			set {
				if (volume != value) {
					volume= value;
					changed[SeriesManagerCacheProps.PROPERTY_26_VOLUME_ID] = true;
					if (VolumeChanged != null)
						VolumeChanged(this, new PropertyChangedEventArgs("Volume", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!SeriesManagerCacheProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
