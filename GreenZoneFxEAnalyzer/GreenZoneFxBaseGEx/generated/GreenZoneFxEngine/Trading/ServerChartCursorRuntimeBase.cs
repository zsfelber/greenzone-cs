using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerChartCursorRuntimeProps
	{
		public static bool RmiGetProperty(IServerChartCursorRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartCursorRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerChartCursorRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartCursorRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerChartCursorRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerChartCursorRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerChartCursorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerChartCursorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerChartCursorRuntimeBase : ChartCursorRuntimeEx, IServerChartCursorRuntime
	{

		bool ___initialized = false;


		public ServerChartCursorRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerChartCursorRuntimeProps.AddDependencies(this, false);
		}

		public ServerChartCursorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartCursorRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartCursorRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartCursorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartCursorRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartCursorRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartCursorRuntimeProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartCursorRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartCursorRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
