using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerExecRuntimeProps
	{
		public static bool RmiGetProperty(IServerExecRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerUserRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerExecRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerUserRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerExecRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerUserRuntimeProps.Initialize(controller, buffer, true);
			}
			ExecRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerExecRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ServerUserRuntimeProps.AddDependencies(controller, true);
			}
			ExecRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerExecRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerUserRuntimeProps.SerializationRead(controller, info, context, true);
			}
			ExecRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerExecRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerUserRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			ExecRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerExecRuntimeBase : ServerUserRuntimeEx, IServerExecRuntime
	{

		bool ___initialized = false;



		public ServerExecRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ServerExecRuntimeProps.AddDependencies(this, false);
		}

		public ServerExecRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ServerExecRuntimeProps.AddDependencies(this, false);
		}

		public ServerExecRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerExecRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerExecRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerExecRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerExecRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerExecRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerExecRuntimeProps.SerializationWrite(this, info, context, false);
		}




		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerExecRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerExecRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
