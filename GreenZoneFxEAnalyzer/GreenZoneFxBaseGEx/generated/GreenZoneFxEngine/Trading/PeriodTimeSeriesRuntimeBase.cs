using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class PeriodTimeSeriesRuntimeProps
	{
		public const int PROPERTY_17_OPEN_ID = 17;
		public const int PROPERTY_18_LOW_ID = 18;
		public const int PROPERTY_19_HIGH_ID = 19;
		public const int PROPERTY_20_CLOSE_ID = 20;
		public static bool RmiGetProperty(IPeriodTimeSeriesRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case PeriodTimeSeriesRuntimeProps.PROPERTY_17_OPEN_ID:
					value = controller.Open;
					return true;
				case PeriodTimeSeriesRuntimeProps.PROPERTY_18_LOW_ID:
					value = controller.Low;
					return true;
				case PeriodTimeSeriesRuntimeProps.PROPERTY_19_HIGH_ID:
					value = controller.High;
					return true;
				case PeriodTimeSeriesRuntimeProps.PROPERTY_20_CLOSE_ID:
					value = controller.Close;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IPeriodTimeSeriesRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case PeriodTimeSeriesRuntimeProps.PROPERTY_17_OPEN_ID:
					controller.Open = (DArr) value;
					return true;
				case PeriodTimeSeriesRuntimeProps.PROPERTY_18_LOW_ID:
					controller.Low = (DArr) value;
					return true;
				case PeriodTimeSeriesRuntimeProps.PROPERTY_19_HIGH_ID:
					controller.High = (DArr) value;
					return true;
				case PeriodTimeSeriesRuntimeProps.PROPERTY_20_CLOSE_ID:
					controller.Close = (DArr) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IPeriodTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IPeriodTimeSeriesRuntime controller, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IPeriodTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
			}
			controller.Open = (DArr) info.GetValue("Open", typeof(DArr));
			controller.Low = (DArr) info.GetValue("Low", typeof(DArr));
			controller.High = (DArr) info.GetValue("High", typeof(DArr));
			controller.Close = (DArr) info.GetValue("Close", typeof(DArr));
		}

		public static void SerializationWrite(IPeriodTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("Open", controller.Open);
			info.AddValue("Low", controller.Low);
			info.AddValue("High", controller.High);
			info.AddValue("Close", controller.Close);
		}

	}
	public abstract class PeriodTimeSeriesRuntimeBase : TimeSeriesRuntimeEx, IPeriodTimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_Open_Changed;
		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_Low_Changed;
		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_High_Changed;
		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_Close_Changed;

		public PeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			PeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public PeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			PeriodTimeSeriesRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			PeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected PeriodTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			PeriodTimeSeriesRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			PeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			PeriodTimeSeriesRuntimeProps.SerializationWrite(this, info, context, false);
		}


		DArr _IPeriodTimeSeriesRuntime_Open;
		public DArr Open
		{
			get {
				return _IPeriodTimeSeriesRuntime_Open;
			}
			set {
				if (_IPeriodTimeSeriesRuntime_Open != value) {
					_IPeriodTimeSeriesRuntime_Open= value;
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_17_OPEN_ID] = true;
					if (IPeriodTimeSeriesRuntime_Open_Changed != null)
						IPeriodTimeSeriesRuntime_Open_Changed(this, new PropertyChangedEventArgs("Open", value));
				}
			}
		}

		DArr _IPeriodTimeSeriesRuntime_Low;
		public DArr Low
		{
			get {
				return _IPeriodTimeSeriesRuntime_Low;
			}
			set {
				if (_IPeriodTimeSeriesRuntime_Low != value) {
					_IPeriodTimeSeriesRuntime_Low= value;
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_18_LOW_ID] = true;
					if (IPeriodTimeSeriesRuntime_Low_Changed != null)
						IPeriodTimeSeriesRuntime_Low_Changed(this, new PropertyChangedEventArgs("Low", value));
				}
			}
		}

		DArr _IPeriodTimeSeriesRuntime_High;
		public DArr High
		{
			get {
				return _IPeriodTimeSeriesRuntime_High;
			}
			set {
				if (_IPeriodTimeSeriesRuntime_High != value) {
					_IPeriodTimeSeriesRuntime_High= value;
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_19_HIGH_ID] = true;
					if (IPeriodTimeSeriesRuntime_High_Changed != null)
						IPeriodTimeSeriesRuntime_High_Changed(this, new PropertyChangedEventArgs("High", value));
				}
			}
		}

		DArr _IPeriodTimeSeriesRuntime_Close;
		public DArr Close
		{
			get {
				return _IPeriodTimeSeriesRuntime_Close;
			}
			set {
				if (_IPeriodTimeSeriesRuntime_Close != value) {
					_IPeriodTimeSeriesRuntime_Close= value;
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_20_CLOSE_ID] = true;
					if (IPeriodTimeSeriesRuntime_Close_Changed != null)
						IPeriodTimeSeriesRuntime_Close_Changed(this, new PropertyChangedEventArgs("Close", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (PeriodTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (PeriodTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
