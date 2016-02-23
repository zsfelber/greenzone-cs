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
		public const int PROPERTY_18_OPEN_ID = 18;
		public const int PROPERTY_19_LOW_ID = 19;
		public const int PROPERTY_20_HIGH_ID = 20;
		public const int PROPERTY_21_CLOSE_ID = 21;
		public static bool RmiGetProperty(IPeriodTimeSeriesRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
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
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IPeriodTimeSeriesRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
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
				default:
					return false;
			}
		}
		public static void Initialize(IPeriodTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IPeriodTimeSeriesRuntime controller)
		{
		}

		public static void SerializationRead(IPeriodTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Open = (IDArr) info.GetValue("Open", typeof(IDArr));
			controller.Low = (IDArr) info.GetValue("Low", typeof(IDArr));
			controller.High = (IDArr) info.GetValue("High", typeof(IDArr));
			controller.Close = (IDArr) info.GetValue("Close", typeof(IDArr));
		}

		public static void SerializationWrite(IPeriodTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Open", controller.Open);
			info.AddValue("Low", controller.Low);
			info.AddValue("High", controller.High);
			info.AddValue("Close", controller.Close);
		}

	}
	public abstract class PeriodTimeSeriesRuntimeBase : TimeSeriesRuntimeBase, IPeriodTimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler OpenChanged;
		public event PropertyChangedEventHandler LowChanged;
		public event PropertyChangedEventHandler HighChanged;
		public event PropertyChangedEventHandler CloseChanged;

		public PeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			PeriodTimeSeriesRuntimeProps.AddDependencies(this);
		}

		public PeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			PeriodTimeSeriesRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			PeriodTimeSeriesRuntimeProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected PeriodTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			PeriodTimeSeriesRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			PeriodTimeSeriesRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			PeriodTimeSeriesRuntimeProps.SerializationWrite(this, info, context);
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
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_18_OPEN_ID] = true;
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
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_19_LOW_ID] = true;
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
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_20_HIGH_ID] = true;
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
					changed[PeriodTimeSeriesRuntimeProps.PROPERTY_21_CLOSE_ID] = true;
					if (CloseChanged != null)
						CloseChanged(this, new PropertyChangedEventArgs("Close", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (PeriodTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!PeriodTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
