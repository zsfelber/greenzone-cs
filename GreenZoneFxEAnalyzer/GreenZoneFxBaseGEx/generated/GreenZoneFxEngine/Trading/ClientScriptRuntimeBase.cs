using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientScriptRuntimeProps
	{
		public static bool RmiGetProperty(IClientScriptRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientScriptRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientScriptRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.Initialize(controller, buffer, true);
			}
			ScriptRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientScriptRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.AddDependencies(controller, true);
			}
			ScriptRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientScriptRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
			ScriptRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientScriptRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			ScriptRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientScriptRuntimeBase : ClientExecRuntimeBase, IClientScriptRuntime
	{

		bool ___initialized = false;



		public ClientScriptRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ClientScriptRuntimeProps.AddDependencies(this, false);
		}

		public ClientScriptRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ClientScriptRuntimeProps.AddDependencies(this, false);
		}

		public ClientScriptRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientScriptRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientScriptRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientScriptRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientScriptRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientScriptRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientScriptRuntimeProps.SerializationWrite(this, info, context, false);
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
			if (ClientScriptRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientScriptRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
