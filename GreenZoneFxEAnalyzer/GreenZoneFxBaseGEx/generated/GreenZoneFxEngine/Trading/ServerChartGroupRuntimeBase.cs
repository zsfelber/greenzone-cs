using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerChartGroupRuntimeProps
	{
		public static bool RmiGetProperty(IServerChartGroupRuntime controller, int propertyId, out object value, bool goToParent)
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
		public static bool RmiSetProperty(IServerChartGroupRuntime controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IServerChartGroupRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerChartGroupRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerChartGroupRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerChartGroupRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartGroupRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerChartGroupRuntimeBase : ChartGroupRuntimeEx, IServerChartGroupRuntime
	{

		bool ___initialized = false;


		public ServerChartGroupRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerChartGroupRuntimeProps.AddDependencies(this, false);
		}

		public ServerChartGroupRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartGroupRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartGroupRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartGroupRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartGroupRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartGroupRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartGroupRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void AddChart(IServerChartRuntime chart);

		public abstract void RemoveChart(IServerChartRuntime chart);


		public new virtual IServerChartRuntime MasterChart
		{
			get {
				return (IServerChartRuntime) ((IChartGroupRuntime)this).MasterChart;
			}
		}

		public new virtual IServerChartRuntime FirstConnectedChart
		{
			get {
				return (IServerChartRuntime) ((IChartGroupRuntime)this).FirstConnectedChart;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartGroupRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartGroupRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
