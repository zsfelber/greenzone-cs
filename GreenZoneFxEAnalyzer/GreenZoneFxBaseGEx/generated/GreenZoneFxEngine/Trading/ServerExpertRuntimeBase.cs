using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerExpertRuntimeProps
	{
		public static bool RmiGetProperty(IServerExpertRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ExpertRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerExpertRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ExpertRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerExpertRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.Initialize(controller, buffer, true);
			}
			ExpertRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerExpertRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.AddDependencies(controller, true);
			}
			ExpertRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerExpertRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
			ExpertRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerExpertRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			ExpertRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerExpertRuntimeBase : ServerExecRuntimeEx, IServerExpertRuntime
	{

		bool ___initialized = false;



		public ServerExpertRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ServerExpertRuntimeProps.AddDependencies(this, false);
		}

		public ServerExpertRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ServerExpertRuntimeProps.AddDependencies(this, false);
		}

		public ServerExpertRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerExpertRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerExpertRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerExpertRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerExpertRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerExpertRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerExpertRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Int32 Init();

		public abstract Int32 Deinit();

		public abstract Int32 OnTick();



		public virtual Mt4ExecutableInfo ExpertInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}

		public new virtual IExpertSession Session
		{
			get {
				return (IExpertSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerExpertRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerExpertRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
