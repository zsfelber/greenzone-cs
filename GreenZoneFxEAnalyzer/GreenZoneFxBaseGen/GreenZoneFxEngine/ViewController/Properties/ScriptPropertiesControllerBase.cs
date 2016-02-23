using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ScriptPropertiesControllerProps
	{
		public static bool RmiGetProperty(IScriptPropertiesController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptPropertiesController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptPropertiesController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IScriptPropertiesController controller)
		{
		}

		public static void SerializationRead(IScriptPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IScriptPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ScriptPropertiesControllerBase : ExecPropertiesControllerBase, IScriptPropertiesController
	{

		bool ___initialized = false;


		public ScriptPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ScriptPropertiesControllerProps.AddDependencies(this);
		}

		public ScriptPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptPropertiesControllerProps.Initialize(this, buffer);
			___initialized = true;
			ScriptPropertiesControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptPropertiesControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ScriptPropertiesControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptPropertiesControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptPropertiesControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ScriptPropertiesControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
