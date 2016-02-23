using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class SelectEnvTypePageControllerProps
	{
		public const int PROPERTY_17_RADIOBUTTON1_ID = 17;
		public const int PROPERTY_18_RADIOBUTTON3_ID = 18;
		public static bool RmiGetProperty(ISelectEnvTypePageController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SelectEnvTypePageControllerProps.PROPERTY_17_RADIOBUTTON1_ID:
					value = controller.RadioButton1;
					return true;
				case SelectEnvTypePageControllerProps.PROPERTY_18_RADIOBUTTON3_ID:
					value = controller.RadioButton3;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISelectEnvTypePageController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(ISelectEnvTypePageController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.RadioButton1 = (RadioButtonController) buffer.ChangedProps[SelectEnvTypePageControllerProps.PROPERTY_17_RADIOBUTTON1_ID];
			controller.RadioButton3 = (RadioButtonController) buffer.ChangedProps[SelectEnvTypePageControllerProps.PROPERTY_18_RADIOBUTTON3_ID];
		}

		public static void AddDependencies(ISelectEnvTypePageController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.RadioButton1);
			controller.Dependencies.Add(controller.RadioButton3);
		}

		public static void SerializationRead(ISelectEnvTypePageController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.RadioButton1 = (RadioButtonController) info.GetValue("RadioButton1", typeof(RadioButtonController));
			controller.RadioButton3 = (RadioButtonController) info.GetValue("RadioButton3", typeof(RadioButtonController));
		}

		public static void SerializationWrite(ISelectEnvTypePageController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("RadioButton1", controller.RadioButton1);
			info.AddValue("RadioButton3", controller.RadioButton3);
		}

	}
	public abstract class SelectEnvTypePageControllerBase : AssistantPageController, ISelectEnvTypePageController
	{

		bool ___initialized = false;


		public SelectEnvTypePageControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			SelectEnvTypePageControllerProps.AddDependencies(this, false);
		}

		public SelectEnvTypePageControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SelectEnvTypePageControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			SelectEnvTypePageControllerProps.AddDependencies(this, false);
		}

		protected SelectEnvTypePageControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SelectEnvTypePageControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			SelectEnvTypePageControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SelectEnvTypePageControllerProps.SerializationWrite(this, info, context, false);
		}


		RadioButtonController _ISelectEnvTypePageController_RadioButton1;
		public RadioButtonController RadioButton1
		{
			get {
				return _ISelectEnvTypePageController_RadioButton1;
			}
			set {
				if (!___initialized) {
					_ISelectEnvTypePageController_RadioButton1= value;
					changed[SelectEnvTypePageControllerProps.PROPERTY_17_RADIOBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController _ISelectEnvTypePageController_RadioButton3;
		public RadioButtonController RadioButton3
		{
			get {
				return _ISelectEnvTypePageController_RadioButton3;
			}
			set {
				if (!___initialized) {
					_ISelectEnvTypePageController_RadioButton3= value;
					changed[SelectEnvTypePageControllerProps.PROPERTY_18_RADIOBUTTON3_ID] = true;
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
			if (SelectEnvTypePageControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (SelectEnvTypePageControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
