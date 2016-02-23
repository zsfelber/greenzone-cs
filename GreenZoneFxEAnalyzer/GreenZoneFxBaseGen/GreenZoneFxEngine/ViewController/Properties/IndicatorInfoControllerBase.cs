using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class IndicatorInfoControllerProps
	{
		public static bool RmiGetProperty(IIndicatorInfoController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorInfoController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorInfoController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IIndicatorInfoController controller)
		{
		}

		public static void SerializationRead(IIndicatorInfoController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IIndicatorInfoController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class IndicatorInfoControllerBase : PropertyPanelController, IIndicatorInfoController
	{

		bool ___initialized = false;


		public IndicatorInfoControllerBase(GreenRmiManager rmiManager, Controller parent, Object selectedObject)
			: base(rmiManager, parent, selectedObject)
		{
			___initialized = true;
			IndicatorInfoControllerProps.AddDependencies(this);
		}

		public IndicatorInfoControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorInfoControllerProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorInfoControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorInfoControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorInfoControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorInfoControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorInfoControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorInfoControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorInfoControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
