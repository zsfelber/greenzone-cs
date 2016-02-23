using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientExecRuntimeProps
	{
		public static bool RmiGetProperty(IClientExecRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientUserRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientExecRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientUserRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientExecRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientUserRuntimeProps.Initialize(controller, buffer, true);
			}
			ExecRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientExecRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ClientUserRuntimeProps.AddDependencies(controller, true);
			}
			ExecRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientExecRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientUserRuntimeProps.SerializationRead(controller, info, context, true);
			}
			ExecRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientExecRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientUserRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			ExecRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientExecRuntimeBase : ClientUserRuntimeBase, IClientExecRuntime
	{

		bool ___initialized = false;



		public ClientExecRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ClientExecRuntimeProps.AddDependencies(this, false);
		}

		public ClientExecRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ClientExecRuntimeProps.AddDependencies(this, false);
		}

		public ClientExecRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientExecRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientExecRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientExecRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientExecRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientExecRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientExecRuntimeProps.SerializationWrite(this, info, context, false);
		}




		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientExecRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientExecRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
