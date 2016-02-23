using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExpertInfoControllerProps
	{
		public static bool RmiGetProperty(IExpertInfoController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertInfoController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertInfoController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IExpertInfoController controller, bool goToParent)
		{
		}

		public static void SerializationRead(IExpertInfoController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

		public static void SerializationWrite(IExpertInfoController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			ExpertInfoControllerProps.AddDependencies(this, false);
		}

		public ExpertInfoControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertInfoControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ExpertInfoControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertInfoControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertInfoControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExpertInfoControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertInfoControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertInfoControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExpertInfoControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
