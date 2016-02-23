using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientChartGroupRuntimeProps
	{
		public static bool RmiGetProperty(IClientChartGroupRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartGroupRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientChartGroupRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartGroupRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartGroupRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartGroupRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartGroupRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientChartGroupRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientChartGroupRuntimeBase : ChartGroupRuntimeEx, IClientChartGroupRuntime
	{

		bool ___initialized = false;


		public ClientChartGroupRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ClientChartGroupRuntimeProps.AddDependencies(this, false);
		}

		public ClientChartGroupRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartGroupRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartGroupRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartGroupRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartGroupRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartGroupRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartGroupRuntimeProps.SerializationWrite(this, info, context, false);
		}


		public abstract Int32 CursorPosition
		{
			get ;
			set ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartGroupRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartGroupRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
