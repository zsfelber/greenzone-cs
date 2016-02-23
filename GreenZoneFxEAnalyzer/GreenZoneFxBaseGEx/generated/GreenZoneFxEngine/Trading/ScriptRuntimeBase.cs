using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ScriptRuntimeProps
	{
		public static bool RmiGetProperty(IScriptRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IScriptRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IScriptRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IScriptRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IScriptRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ScriptRuntimeBase : ExecRuntimeBase, IScriptRuntime
	{

		bool ___initialized = false;


		public ScriptRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this, false);
		}

		public ScriptRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this, false);
		}

		public ScriptRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Int32 Start();


		public new virtual IScriptSession Session
		{
			get {
				return (IScriptSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}

		public virtual Mt4ExecutableInfo ScriptInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ScriptRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
