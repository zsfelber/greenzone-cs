using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ScriptInfoControllerProps
	{
		public static bool RmiGetProperty(IScriptInfoController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptInfoController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptInfoController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IScriptInfoController controller)
		{
		}

		public static void SerializationRead(IScriptInfoController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IScriptInfoController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ScriptInfoControllerBase : PropertyPanelController, IScriptInfoController
	{

		bool ___initialized = false;


		public ScriptInfoControllerBase(GreenRmiManager rmiManager, Controller parent, Object selectedObject)
			: base(rmiManager, parent, selectedObject)
		{
			___initialized = true;
			ScriptInfoControllerProps.AddDependencies(this);
		}

		public ScriptInfoControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptInfoControllerProps.Initialize(this, buffer);
			___initialized = true;
			ScriptInfoControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptInfoControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptInfoControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ScriptInfoControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptInfoControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptInfoControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ScriptInfoControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
