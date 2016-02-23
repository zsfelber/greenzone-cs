using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExpertPropertiesControllerProps
	{
		public static bool RmiGetProperty(IExpertPropertiesController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertPropertiesController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertPropertiesController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IExpertPropertiesController controller)
		{
		}

		public static void SerializationRead(IExpertPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IExpertPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ExpertPropertiesControllerBase : ExecPropertiesControllerBase, IExpertPropertiesController
	{

		bool ___initialized = false;


		public ExpertPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ExpertPropertiesControllerProps.AddDependencies(this);
		}

		public ExpertPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertPropertiesControllerProps.Initialize(this, buffer);
			___initialized = true;
			ExpertPropertiesControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertPropertiesControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ExpertPropertiesControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertPropertiesControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertPropertiesControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExpertPropertiesControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
