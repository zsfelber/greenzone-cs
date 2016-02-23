using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ScriptSessionProps
	{
		public static bool RmiGetProperty(IScriptSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptSession controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IScriptSession controller)
		{
		}

		public static void SerializationRead(IScriptSession controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IScriptSession controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ScriptSessionBase : ExecSessionBase, IScriptSession
	{

		bool ___initialized = false;


		public ScriptSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ScriptSessionProps.AddDependencies(this);
		}

		public ScriptSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptSessionProps.Initialize(this, buffer);
			___initialized = true;
			ScriptSessionProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			ScriptSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptSessionProps.SerializationWrite(this, info, context);
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
			if (ScriptSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ScriptSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
