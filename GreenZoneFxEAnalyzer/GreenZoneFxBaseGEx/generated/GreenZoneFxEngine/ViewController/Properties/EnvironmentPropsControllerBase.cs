using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class EnvironmentPropsControllerProps
	{
		public static bool RmiGetProperty(IEnvironmentPropsController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentPropsController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentPropsController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IEnvironmentPropsController controller, bool goToParent)
		{
		}

		public static void SerializationRead(IEnvironmentPropsController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

		public static void SerializationWrite(IEnvironmentPropsController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

	}
	public abstract class EnvironmentPropsControllerBase : PropertyPanelController, IEnvironmentPropsController
	{

		bool ___initialized = false;


		public EnvironmentPropsControllerBase(GreenRmiManager rmiManager, Controller parent, Object selectedObject)
			: base(rmiManager, parent, selectedObject)
		{
			___initialized = true;
			EnvironmentPropsControllerProps.AddDependencies(this, false);
		}

		public EnvironmentPropsControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentPropsControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			EnvironmentPropsControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected EnvironmentPropsControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentPropsControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EnvironmentPropsControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentPropsControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EnvironmentPropsControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EnvironmentPropsControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
