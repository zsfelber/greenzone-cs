using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExecPropertiesControllerProps
	{
		public const int PROPERTY_14_PROPERTYGRID1_ID = 14;
		public static bool RmiGetProperty(IExecPropertiesController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_14_PROPERTYGRID1_ID:
					value = controller.PropertyGrid1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExecPropertiesController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExecPropertiesController controller, GreenRmiObjectBuffer buffer)
		{
			controller.PropertyGrid1 = (BufferedPropertyGridController) buffer.ChangedProps[ExecPropertiesControllerProps.PROPERTY_14_PROPERTYGRID1_ID];
		}

		public static void AddDependencies(IExecPropertiesController controller)
		{
			controller.Dependencies.Add(controller.PropertyGrid1);
		}

		public static void SerializationRead(IExecPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
			controller.PropertyGrid1 = (BufferedPropertyGridController) info.GetValue("PropertyGrid1", typeof(BufferedPropertyGridController));
		}

		public static void SerializationWrite(IExecPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("PropertyGrid1", controller.PropertyGrid1);
		}

	}
	public abstract class ExecPropertiesControllerBase : Controller, IExecPropertiesController
	{

		bool ___initialized = false;


		public ExecPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ExecPropertiesControllerProps.AddDependencies(this);
		}

		public ExecPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExecPropertiesControllerProps.Initialize(this, buffer);
			___initialized = true;
			ExecPropertiesControllerProps.AddDependencies(this);
		}

		protected ExecPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExecPropertiesControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ExecPropertiesControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExecPropertiesControllerProps.SerializationWrite(this, info, context);
		}

		BufferedPropertyGridController propertyGrid1;
		public BufferedPropertyGridController PropertyGrid1
		{
			get {
				return propertyGrid1;
			}
			set {
				if (!___initialized) {
					propertyGrid1= value;
					changed[ExecPropertiesControllerProps.PROPERTY_14_PROPERTYGRID1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExecPropertiesControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExecPropertiesControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
