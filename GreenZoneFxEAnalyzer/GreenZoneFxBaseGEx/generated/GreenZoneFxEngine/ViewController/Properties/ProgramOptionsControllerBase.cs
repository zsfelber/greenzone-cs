using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ProgramOptionsControllerProps
	{
		public static bool RmiGetProperty(IProgramOptionsController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IProgramOptionsController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IProgramOptionsController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IProgramOptionsController controller, bool goToParent)
		{
		}

		public static void SerializationRead(IProgramOptionsController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

		public static void SerializationWrite(IProgramOptionsController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
		}

	}
	public abstract class ProgramOptionsControllerBase : PropertyPanelController, IProgramOptionsController
	{

		bool ___initialized = false;


		public ProgramOptionsControllerBase(GreenRmiManager rmiManager, Controller parent, Object selectedObject)
			: base(rmiManager, parent, selectedObject)
		{
			___initialized = true;
			ProgramOptionsControllerProps.AddDependencies(this, false);
		}

		public ProgramOptionsControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ProgramOptionsControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ProgramOptionsControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ProgramOptionsControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ProgramOptionsControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ProgramOptionsControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ProgramOptionsControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ProgramOptionsControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ProgramOptionsControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
