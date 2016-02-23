using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerScriptRuntimeProps
	{
		public static bool RmiGetProperty(IServerScriptRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ScriptRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerScriptRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ScriptRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerScriptRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.Initialize(controller, buffer, true);
			}
			ScriptRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerScriptRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.AddDependencies(controller, true);
			}
			ScriptRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerScriptRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
			ScriptRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerScriptRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			ScriptRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerScriptRuntimeBase : ServerExecRuntimeEx, IServerScriptRuntime
	{

		bool ___initialized = false;



		public ServerScriptRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ServerScriptRuntimeProps.AddDependencies(this, false);
		}

		public ServerScriptRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ServerScriptRuntimeProps.AddDependencies(this, false);
		}

		public ServerScriptRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerScriptRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerScriptRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerScriptRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerScriptRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerScriptRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerScriptRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Int32 Start();



		public virtual Mt4ExecutableInfo ScriptInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}

		public new virtual IScriptSession Session
		{
			get {
				return (IScriptSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerScriptRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerScriptRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}