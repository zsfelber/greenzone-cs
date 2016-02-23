using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ScriptInfoControllerProps
	{
		public static bool RmiGetProperty(IScriptInfoController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptInfoController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptInfoController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IScriptInfoController controller, bool goToParent)
		{
		}

		public static void SerializationRead(IScriptInfoController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

		public static void SerializationWrite(IScriptInfoController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			ScriptInfoControllerProps.AddDependencies(this, false);
		}

		public ScriptInfoControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptInfoControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ScriptInfoControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ScriptInfoControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptInfoControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ScriptInfoControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptInfoControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ScriptInfoControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ScriptInfoControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
