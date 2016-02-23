using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class IndicatorPropertiesControllerProps
	{
		public static bool RmiGetProperty(IIndicatorPropertiesController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorPropertiesController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorPropertiesController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IIndicatorPropertiesController controller)
		{
		}

		public static void SerializationRead(IIndicatorPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IIndicatorPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class IndicatorPropertiesControllerBase : ExecPropertiesControllerBase, IIndicatorPropertiesController
	{

		bool ___initialized = false;


		public IndicatorPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorPropertiesControllerProps.AddDependencies(this);
		}

		public IndicatorPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorPropertiesControllerProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorPropertiesControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorPropertiesControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorPropertiesControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorPropertiesControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorPropertiesControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorPropertiesControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
