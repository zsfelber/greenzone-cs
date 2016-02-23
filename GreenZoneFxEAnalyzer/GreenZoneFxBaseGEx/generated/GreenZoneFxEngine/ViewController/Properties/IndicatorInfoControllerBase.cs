using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class IndicatorInfoControllerProps
	{
		public static bool RmiGetProperty(IIndicatorInfoController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorInfoController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorInfoController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IIndicatorInfoController controller, bool goToParent)
		{
		}

		public static void SerializationRead(IIndicatorInfoController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

		public static void SerializationWrite(IIndicatorInfoController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			IndicatorInfoControllerProps.AddDependencies(this, false);
		}

		public IndicatorInfoControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorInfoControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorInfoControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorInfoControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorInfoControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorInfoControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorInfoControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorInfoControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorInfoControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
