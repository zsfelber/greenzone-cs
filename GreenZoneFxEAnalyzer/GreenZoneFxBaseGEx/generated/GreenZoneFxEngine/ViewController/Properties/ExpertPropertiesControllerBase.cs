using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExpertPropertiesControllerProps
	{
		public static bool RmiGetProperty(IExpertPropertiesController controller, int propertyId, out object value, bool goToParent)
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
		public static bool RmiSetProperty(IExpertPropertiesController controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IExpertPropertiesController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IExpertPropertiesController controller, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IExpertPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IExpertPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ExpertPropertiesControllerBase : ExecPropertiesControllerBase, IExpertPropertiesController
	{

		bool ___initialized = false;


		public ExpertPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ExpertPropertiesControllerProps.AddDependencies(this, false);
		}

		public ExpertPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertPropertiesControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ExpertPropertiesControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertPropertiesControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExpertPropertiesControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertPropertiesControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertPropertiesControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExpertPropertiesControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
