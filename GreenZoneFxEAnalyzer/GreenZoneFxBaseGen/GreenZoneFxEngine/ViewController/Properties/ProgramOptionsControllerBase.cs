using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ProgramOptionsControllerProps
	{
		public static bool RmiGetProperty(IProgramOptionsController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IProgramOptionsController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IProgramOptionsController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IProgramOptionsController controller)
		{
		}

		public static void SerializationRead(IProgramOptionsController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IProgramOptionsController controller, SerializationInfo info, StreamingContext context)
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
			ProgramOptionsControllerProps.AddDependencies(this);
		}

		public ProgramOptionsControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ProgramOptionsControllerProps.Initialize(this, buffer);
			___initialized = true;
			ProgramOptionsControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ProgramOptionsControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ProgramOptionsControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ProgramOptionsControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ProgramOptionsControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ProgramOptionsControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ProgramOptionsControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
