using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExpertInfoControllerProps
	{
		public static bool RmiGetProperty(IExpertInfoController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertInfoController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertInfoController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IExpertInfoController controller)
		{
		}

		public static void SerializationRead(IExpertInfoController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IExpertInfoController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ExpertInfoControllerBase : PropertyPanelController, IExpertInfoController
	{

		bool ___initialized = false;


		public ExpertInfoControllerBase(GreenRmiManager rmiManager, Controller parent, Object selectedObject)
			: base(rmiManager, parent, selectedObject)
		{
			___initialized = true;
			ExpertInfoControllerProps.AddDependencies(this);
		}

		public ExpertInfoControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertInfoControllerProps.Initialize(this, buffer);
			___initialized = true;
			ExpertInfoControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertInfoControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertInfoControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ExpertInfoControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertInfoControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertInfoControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExpertInfoControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
