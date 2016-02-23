using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class IndicatorPropertiesControllerProps
	{
		public static bool RmiGetProperty(IIndicatorPropertiesController controller, int propertyId, out object value, bool goToParent)
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
		public static bool RmiSetProperty(IIndicatorPropertiesController controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IIndicatorPropertiesController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IIndicatorPropertiesController controller, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IIndicatorPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IIndicatorPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecPropertiesControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class IndicatorPropertiesControllerBase : ExecPropertiesControllerBase, IIndicatorPropertiesController
	{

		bool ___initialized = false;


		public IndicatorPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorPropertiesControllerProps.AddDependencies(this, false);
		}

		public IndicatorPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorPropertiesControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorPropertiesControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorPropertiesControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorPropertiesControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorPropertiesControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorPropertiesControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorPropertiesControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
