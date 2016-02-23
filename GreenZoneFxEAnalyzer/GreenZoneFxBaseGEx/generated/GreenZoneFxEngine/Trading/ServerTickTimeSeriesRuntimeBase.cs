using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerTickTimeSeriesRuntimeProps
	{
		public static bool RmiGetProperty(IServerTickTimeSeriesRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerTimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (TickTimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerTickTimeSeriesRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerTimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (TickTimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerTickTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.Initialize(controller, buffer, true);
			}
			TickTimeSeriesRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerTickTimeSeriesRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.AddDependencies(controller, true);
			}
			TickTimeSeriesRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerTickTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
			}
			TickTimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerTickTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			TickTimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerTickTimeSeriesRuntimeBase : ServerTimeSeriesRuntimeEx, IServerTickTimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ITickTimeSeriesRuntime_Bid_Changed;
		public event PropertyChangedEventHandler ITickTimeSeriesRuntime_Ask_Changed;


		internal ServerTickTimeSeriesRuntimeBase(GreenRmiManager rmiManager, ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime, int headerLen, int recordLen, string path)
			: base(rmiManager, parent, period, online, focusedTime, headerLen, recordLen, path)
		{
			___initialized = true;
			ServerTickTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		internal ServerTickTimeSeriesRuntimeBase(GreenRmiManager rmiManager, string envHistoryDir, TimePeriodConst period, bool online, datetime focusedTime, int headerLen, int recordLen, string path)
			: base(rmiManager, envHistoryDir, period, online, focusedTime, headerLen, recordLen, path)
		{
			___initialized = true;
			ServerTickTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		internal ServerTickTimeSeriesRuntimeBase(GreenRmiManager rmiManager, string path, bool online, datetime focusedTime, int headerLen, int recordLen)
			: base(rmiManager, path, online, focusedTime, headerLen, recordLen)
		{
			___initialized = true;
			ServerTickTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public ServerTickTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerTickTimeSeriesRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerTickTimeSeriesRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerTickTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerTickTimeSeriesRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerTickTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerTickTimeSeriesRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Double GetBid(Int32 i);

		public abstract Double GetAsk(Int32 i);



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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerTickTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerTickTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
