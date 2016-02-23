using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ScriptPropertiesControllerProps
	{
		public static bool RmiGetProperty(IScriptPropertiesController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecPropertiesControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IScriptPropertiesController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecPropertiesControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptPropertiesController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IScriptPropertiesController controller, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IScriptPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IScriptPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ScriptPropertiesControllerBase : ExecPropertiesControllerBase, IScriptPropertiesController
	{

		bool ___initialized = false;


		public ScriptPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ScriptPropertiesControllerProps.AddDependencies(this, false);
		}

		public ScriptPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptPropertiesControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ScriptPropertiesControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptPropertiesControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ScriptPropertiesControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptPropertiesControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptPropertiesControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ScriptPropertiesControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
