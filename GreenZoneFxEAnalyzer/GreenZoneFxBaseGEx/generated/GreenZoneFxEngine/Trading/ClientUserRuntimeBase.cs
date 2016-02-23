using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientUserRuntimeProps
	{
		public static bool RmiGetProperty(IClientUserRuntime controller, int propertyId, out object value, bool goToParent)
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
		public static bool RmiSetProperty(IClientUserRuntime controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IClientUserRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientUserRuntime controller, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientUserRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientUserRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientUserRuntimeBase : UserRuntimeEx, IClientUserRuntime
	{

		bool ___initialized = false;


		public ClientUserRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ClientUserRuntimeProps.AddDependencies(this, false);
		}

		public ClientUserRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ClientUserRuntimeProps.AddDependencies(this, false);
		}

		public ClientUserRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientUserRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientUserRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientUserRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientUserRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientUserRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientUserRuntimeProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IClientChartRuntime Parent
		{
			get {
				return (IClientChartRuntime) ((IUserRuntime)this).Parent;
			}
			set {
				((IUserRuntime)this).Parent = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientUserRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientUserRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
