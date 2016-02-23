using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ScriptSessionProps
	{
		public static bool RmiGetProperty(IScriptSession controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecSessionProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IScriptSession controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecSessionProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IScriptSession controller, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IScriptSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IScriptSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ScriptSessionBase : ExecSessionBase, IScriptSession
	{

		bool ___initialized = false;


		public ScriptSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ScriptSessionProps.AddDependencies(this, false);
		}

		public ScriptSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			ScriptSessionProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ScriptSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptSessionProps.SerializationWrite(this, info, context, false);
		}


		public virtual Mt4ExecutableInfo ScriptInfo
		{
			get {
				return ExecutableInfo;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ScriptSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
