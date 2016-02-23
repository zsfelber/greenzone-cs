using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ScriptRuntimeProps
	{
		public const int PROPERTY_11_SESSION_ID = 11;
		public static bool RmiGetProperty(IScriptRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_11_SESSION_ID:
					value = controller.Session;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Session = (IScriptSession) buffer.ChangedProps[ScriptRuntimeProps.PROPERTY_11_SESSION_ID];
		}

		public static void AddDependencies(IScriptRuntime controller)
		{
			controller.Dependencies.Add(controller.Session);
		}

		public static void SerializationRead(IScriptRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Session = (IScriptSession) info.GetValue("Session", typeof(IScriptSession));
		}

		public static void SerializationWrite(IScriptRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Session", controller.Session);
		}

	}
	public abstract class ScriptRuntimeBase : ExecRuntimeBase, IScriptRuntime
	{

		bool ___initialized = false;


		public ScriptRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this);
		}

		public ScriptRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this);
		}

		protected ScriptRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ScriptRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptRuntimeProps.SerializationWrite(this, info, context);
		}

		IScriptSession session;
		public IScriptSession Session
		{
			get {
				return session;
			}
			set {
				if (!___initialized) {
					session= value;
					changed[ScriptRuntimeProps.PROPERTY_11_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual Mt4ExecutableInfo ScriptInfo
		{
			get {
				return Session.ScriptInfo;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ScriptRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
