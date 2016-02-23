using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class TickTimeSeriesRuntimeProps
	{
		public const int PROPERTY_17_BID_ID = 17;
		public const int PROPERTY_18_ASK_ID = 18;
		public static bool RmiGetProperty(ITickTimeSeriesRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case TickTimeSeriesRuntimeProps.PROPERTY_17_BID_ID:
					value = controller.Bid;
					return true;
				case TickTimeSeriesRuntimeProps.PROPERTY_18_ASK_ID:
					value = controller.Ask;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITickTimeSeriesRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case TickTimeSeriesRuntimeProps.PROPERTY_17_BID_ID:
					controller.Bid = (DArr) value;
					return true;
				case TickTimeSeriesRuntimeProps.PROPERTY_18_ASK_ID:
					controller.Ask = (DArr) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITickTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(ITickTimeSeriesRuntime controller, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(ITickTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
			}
			controller.Bid = (DArr) info.GetValue("Bid", typeof(DArr));
			controller.Ask = (DArr) info.GetValue("Ask", typeof(DArr));
		}

		public static void SerializationWrite(ITickTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("Bid", controller.Bid);
			info.AddValue("Ask", controller.Ask);
		}

	}
	public abstract class TickTimeSeriesRuntimeBase : TimeSeriesRuntimeEx, ITickTimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ITickTimeSeriesRuntime_Bid_Changed;
		public event PropertyChangedEventHandler ITickTimeSeriesRuntime_Ask_Changed;

		public TickTimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			TickTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public TickTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TickTimeSeriesRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			TickTimeSeriesRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected TickTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TickTimeSeriesRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			TickTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TickTimeSeriesRuntimeProps.SerializationWrite(this, info, context, false);
		}


		DArr _ITickTimeSeriesRuntime_Bid;
		public DArr Bid
		{
			get {
				return _ITickTimeSeriesRuntime_Bid;
			}
			set {
				if (_ITickTimeSeriesRuntime_Bid != value) {
					_ITickTimeSeriesRuntime_Bid= value;
					changed[TickTimeSeriesRuntimeProps.PROPERTY_17_BID_ID] = true;
					if (ITickTimeSeriesRuntime_Bid_Changed != null)
						ITickTimeSeriesRuntime_Bid_Changed(this, new PropertyChangedEventArgs("Bid", value));
				}
			}
		}

		DArr _ITickTimeSeriesRuntime_Ask;
		public DArr Ask
		{
			get {
				return _ITickTimeSeriesRuntime_Ask;
			}
			set {
				if (_ITickTimeSeriesRuntime_Ask != value) {
					_ITickTimeSeriesRuntime_Ask= value;
					changed[TickTimeSeriesRuntimeProps.PROPERTY_18_ASK_ID] = true;
					if (ITickTimeSeriesRuntime_Ask_Changed != null)
						ITickTimeSeriesRuntime_Ask_Changed(this, new PropertyChangedEventArgs("Ask", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TickTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (TickTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
