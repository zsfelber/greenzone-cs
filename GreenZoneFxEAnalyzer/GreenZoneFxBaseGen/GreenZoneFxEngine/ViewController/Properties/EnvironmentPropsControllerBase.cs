using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class EnvironmentPropsControllerProps
	{
		public static bool RmiGetProperty(IEnvironmentPropsController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentPropsController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentPropsController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IEnvironmentPropsController controller)
		{
		}

		public static void SerializationRead(IEnvironmentPropsController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IEnvironmentPropsController controller, SerializationInfo info, StreamingContext context)
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
			EnvironmentPropsControllerProps.AddDependencies(this);
		}

		public EnvironmentPropsControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentPropsControllerProps.Initialize(this, buffer);
			___initialized = true;
			EnvironmentPropsControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected EnvironmentPropsControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentPropsControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			EnvironmentPropsControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentPropsControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EnvironmentPropsControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!EnvironmentPropsControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
