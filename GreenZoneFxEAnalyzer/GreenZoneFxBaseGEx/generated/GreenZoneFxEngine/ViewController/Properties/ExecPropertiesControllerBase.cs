using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExecPropertiesControllerProps
	{
		public const int PROPERTY_14_PROPERTYGRID1_ID = 14;
		public static bool RmiGetProperty(IExecPropertiesController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ExecPropertiesControllerProps.PROPERTY_14_PROPERTYGRID1_ID:
					value = controller.PropertyGrid1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExecPropertiesController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExecPropertiesController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.PropertyGrid1 = (BufferedPropertyGridController) buffer.ChangedProps[ExecPropertiesControllerProps.PROPERTY_14_PROPERTYGRID1_ID];
		}

		public static void AddDependencies(IExecPropertiesController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.PropertyGrid1);
		}

		public static void SerializationRead(IExecPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.PropertyGrid1 = (BufferedPropertyGridController) info.GetValue("PropertyGrid1", typeof(BufferedPropertyGridController));
		}

		public static void SerializationWrite(IExecPropertiesController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			ExecPropertiesControllerProps.AddDependencies(this, false);
		}

		public ExecPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExecPropertiesControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ExecPropertiesControllerProps.AddDependencies(this, false);
		}

		protected ExecPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExecPropertiesControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExecPropertiesControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExecPropertiesControllerProps.SerializationWrite(this, info, context, false);
		}


		BufferedPropertyGridController _IExecPropertiesController_PropertyGrid1;
		public BufferedPropertyGridController PropertyGrid1
		{
			get {
				return _IExecPropertiesController_PropertyGrid1;
			}
			set {
				if (!___initialized) {
					_IExecPropertiesController_PropertyGrid1= value;
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
			if (ExecPropertiesControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExecPropertiesControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
