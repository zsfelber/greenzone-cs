using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerPeriodTimeSeriesRuntimeProps
	{
		public static bool RmiGetProperty(IServerPeriodTimeSeriesRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerTimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (PeriodTimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerPeriodTimeSeriesRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerTimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (PeriodTimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerPeriodTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.Initialize(controller, buffer, true);
			}
			PeriodTimeSeriesRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerPeriodTimeSeriesRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.AddDependencies(controller, true);
			}
			PeriodTimeSeriesRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerPeriodTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
			}
			PeriodTimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerPeriodTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerTimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			PeriodTimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerPeriodTimeSeriesRuntimeBase : ServerTimeSeriesRuntimeEx, IServerPeriodTimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_Open_Changed;
		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_Low_Changed;
		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_High_Changed;
		public event PropertyChangedEventHandler IPeriodTimeSeriesRuntime_Close_Changed;


		internal ServerPeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager, ServerSymbolRuntime parent, TimePeriodConst period, bool online, datetime focusedTime, int headerLen, int recordLen, string path)
			: base(rmiManager, parent, period, online, focusedTime, headerLen, recordLen, path)
		{
			___initialized = true;
			ServerPeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		internal ServerPeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager, string envHistoryDir, TimePeriodConst period, bool online, datetime focusedTime, int headerLen, int recordLen, string path)
			: base(rmiManager, envHistoryDir, period, online, focusedTime, headerLen, recordLen, path)
		{
			___initialized = true;
			ServerPeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		internal ServerPeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager, string path, bool online, datetime focusedTime, int headerLen, int recordLen)
			: base(rmiManager, path, online, focusedTime, headerLen, recordLen)
		{
			___initialized = true;
			ServerPeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public ServerPeriodTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerPeriodTimeSeriesRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerPeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerPeriodTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerPeriodTimeSeriesRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerPeriodTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerPeriodTimeSeriesRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Double GetOpen(Int32 i);

		public abstract Double GetLow(Int32 i);

		public abstract Double GetHigh(Int32 i);

		public abstract Double GetClose(Int32 i);



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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerPeriodTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerPeriodTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
