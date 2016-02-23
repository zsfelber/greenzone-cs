using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerTimeSeriesRuntimeProps
	{
		public static bool RmiGetProperty(IServerTimeSeriesRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeSeriesRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerTimeSeriesRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeSeriesRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerTimeSeriesRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerTimeSeriesRuntime controller, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerTimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeSeriesRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerTimeSeriesRuntimeBase : TimeSeriesRuntimeEx, IServerTimeSeriesRuntime
	{

		bool ___initialized = false;


		public ServerTimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public ServerTimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerTimeSeriesRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerTimeSeriesRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerTimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerTimeSeriesRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerTimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerTimeSeriesRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract datetime GetTime(Int32 i);

		public abstract Double GetVolume(Int32 i);



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerTimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerTimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
