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
		public const int PROPERTY_18_BID_ID = 18;
		public const int PROPERTY_19_ASK_ID = 19;
		public static bool RmiGetProperty(ITickTimeSeriesRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_18_BID_ID:
					value = controller.Bid;
					return true;
				case PROPERTY_19_ASK_ID:
					value = controller.Ask;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITickTimeSeriesRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_18_BID_ID:
					controller.Bid = (IDArr) value;
					return true;
				case PROPERTY_19_ASK_ID:
					controller.Ask = (IDArr) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITickTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(ITickTimeSeriesRuntime controller)
		{
		}

		public static void SerializationRead(ITickTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Bid = (IDArr) info.GetValue("Bid", typeof(IDArr));
			controller.Ask = (IDArr) info.GetValue("Ask", typeof(IDArr));
		}

		public static void SerializationWrite(ITickTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Bid", controller.Bid);
			info.AddValue("Ask", controller.Ask);
		}

	}
	public abstract class TickTimeSeriesRuntimeBase : TimeSeriesRuntimeBase, ITickTimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler BidChanged;
		public event PropertyChangedEventHandler AskChanged;

		public TickTimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			TickTimeSeriesRuntimeProps.AddDependencies(this);
		}

		public TickTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TickTimeSeriesRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			TickTimeSeriesRuntimeProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected TickTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TickTimeSeriesRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			TickTimeSeriesRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TickTimeSeriesRuntimeProps.SerializationWrite(this, info, context);
		}

		IDArr bid;
		public IDArr Bid
		{
			get {
				return bid;
			}
			set {
				if (bid != value) {
					bid= value;
					changed[TickTimeSeriesRuntimeProps.PROPERTY_18_BID_ID] = true;
					if (BidChanged != null)
						BidChanged(this, new PropertyChangedEventArgs("Bid", value));
				}
			}
		}

		IDArr ask;
		public IDArr Ask
		{
			get {
				return ask;
			}
			set {
				if (ask != value) {
					ask= value;
					changed[TickTimeSeriesRuntimeProps.PROPERTY_19_ASK_ID] = true;
					if (AskChanged != null)
						AskChanged(this, new PropertyChangedEventArgs("Ask", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TickTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!TickTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
