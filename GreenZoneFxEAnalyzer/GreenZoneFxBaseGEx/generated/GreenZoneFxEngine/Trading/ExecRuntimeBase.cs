using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExecRuntimeProps
	{
		public static bool RmiGetProperty(IExecRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (UserRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IExecRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (UserRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExecRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IExecRuntime controller, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IExecRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IExecRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ExecRuntimeBase : UserRuntimeEx, IExecRuntime
	{

		bool ___initialized = false;


		public ExecRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this, false);
		}

		public ExecRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this, false);
		}

		public ExecRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExecRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExecRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExecRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExecRuntimeProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExecRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExecRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
