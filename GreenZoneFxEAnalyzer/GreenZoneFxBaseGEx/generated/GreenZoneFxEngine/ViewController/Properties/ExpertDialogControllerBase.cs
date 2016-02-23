using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ExpertDialogControllerProps
	{
		public const int PROPERTY_22_EXPERTRUNTIMEPANEL_ID = 22;
		public const int PROPERTY_23_OKBUTTON_ID = 23;
		public const int PROPERTY_24__CANCELBUTTON_ID = 24;
		public const int PROPERTY_25_RESETBUTTON_ID = 25;
		public const int PROPERTY_26_LOADBUTTON_ID = 26;
		public const int PROPERTY_27_SAVEBUTTON_ID = 27;
		public static bool RmiGetProperty(IExpertDialogController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ExpertDialogControllerProps.PROPERTY_22_EXPERTRUNTIMEPANEL_ID:
					value = controller.ExpertRuntimePanel;
					return true;
				case ExpertDialogControllerProps.PROPERTY_23_OKBUTTON_ID:
					value = controller.OkButton;
					return true;
				case ExpertDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID:
					value = controller._CancelButton;
					return true;
				case ExpertDialogControllerProps.PROPERTY_25_RESETBUTTON_ID:
					value = controller.ResetButton;
					return true;
				case ExpertDialogControllerProps.PROPERTY_26_LOADBUTTON_ID:
					value = controller.LoadButton;
					return true;
				case ExpertDialogControllerProps.PROPERTY_27_SAVEBUTTON_ID:
					value = controller.SaveButton;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertDialogController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertDialogController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.ExpertRuntimePanel = (IExpertPropertiesController) buffer.ChangedProps[ExpertDialogControllerProps.PROPERTY_22_EXPERTRUNTIMEPANEL_ID];
			controller.OkButton = (ButtonController) buffer.ChangedProps[ExpertDialogControllerProps.PROPERTY_23_OKBUTTON_ID];
			controller._CancelButton = (ButtonController) buffer.ChangedProps[ExpertDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID];
			controller.ResetButton = (ButtonController) buffer.ChangedProps[ExpertDialogControllerProps.PROPERTY_25_RESETBUTTON_ID];
			controller.LoadButton = (ButtonController) buffer.ChangedProps[ExpertDialogControllerProps.PROPERTY_26_LOADBUTTON_ID];
			controller.SaveButton = (ButtonController) buffer.ChangedProps[ExpertDialogControllerProps.PROPERTY_27_SAVEBUTTON_ID];
		}

		public static void AddDependencies(IExpertDialogController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.ExpertRuntimePanel);
			controller.Dependencies.Add(controller.OkButton);
			controller.Dependencies.Add(controller._CancelButton);
			controller.Dependencies.Add(controller.ResetButton);
			controller.Dependencies.Add(controller.LoadButton);
			controller.Dependencies.Add(controller.SaveButton);
		}

		public static void SerializationRead(IExpertDialogController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.ExpertRuntimePanel = (IExpertPropertiesController) info.GetValue("ExpertRuntimePanel", typeof(IExpertPropertiesController));
			controller.OkButton = (ButtonController) info.GetValue("OkButton", typeof(ButtonController));
			controller._CancelButton = (ButtonController) info.GetValue("_CancelButton", typeof(ButtonController));
			controller.ResetButton = (ButtonController) info.GetValue("ResetButton", typeof(ButtonController));
			controller.LoadButton = (ButtonController) info.GetValue("LoadButton", typeof(ButtonController));
			controller.SaveButton = (ButtonController) info.GetValue("SaveButton", typeof(ButtonController));
		}

		public static void SerializationWrite(IExpertDialogController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("ExpertRuntimePanel", controller.ExpertRuntimePanel);
			info.AddValue("OkButton", controller.OkButton);
			info.AddValue("_CancelButton", controller._CancelButton);
			info.AddValue("ResetButton", controller.ResetButton);
			info.AddValue("LoadButton", controller.LoadButton);
			info.AddValue("SaveButton", controller.SaveButton);
		}

	}
	public abstract class ExpertDialogControllerBase : DialogController, IExpertDialogController
	{

		bool ___initialized = false;


		public ExpertDialogControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExpertDialogControllerProps.AddDependencies(this, false);
		}

		public ExpertDialogControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			ExpertDialogControllerProps.AddDependencies(this, false);
		}

		public ExpertDialogControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			ExpertDialogControllerProps.AddDependencies(this, false);
		}

		public ExpertDialogControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertDialogControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ExpertDialogControllerProps.AddDependencies(this, false);
		}

		protected ExpertDialogControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertDialogControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExpertDialogControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertDialogControllerProps.SerializationWrite(this, info, context, false);
		}


		IExpertPropertiesController _IExpertDialogController_ExpertRuntimePanel;
		public IExpertPropertiesController ExpertRuntimePanel
		{
			get {
				return _IExpertDialogController_ExpertRuntimePanel;
			}
			set {
				if (!___initialized) {
					_IExpertDialogController_ExpertRuntimePanel= value;
					changed[ExpertDialogControllerProps.PROPERTY_22_EXPERTRUNTIMEPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IExpertDialogController_OkButton;
		public ButtonController OkButton
		{
			get {
				return _IExpertDialogController_OkButton;
			}
			set {
				if (!___initialized) {
					_IExpertDialogController_OkButton= value;
					changed[ExpertDialogControllerProps.PROPERTY_23_OKBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IExpertDialogController__CancelButton;
		public ButtonController _CancelButton
		{
			get {
				return _IExpertDialogController__CancelButton;
			}
			set {
				if (!___initialized) {
					_IExpertDialogController__CancelButton= value;
					changed[ExpertDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IExpertDialogController_ResetButton;
		public ButtonController ResetButton
		{
			get {
				return _IExpertDialogController_ResetButton;
			}
			set {
				if (!___initialized) {
					_IExpertDialogController_ResetButton= value;
					changed[ExpertDialogControllerProps.PROPERTY_25_RESETBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IExpertDialogController_LoadButton;
		public ButtonController LoadButton
		{
			get {
				return _IExpertDialogController_LoadButton;
			}
			set {
				if (!___initialized) {
					_IExpertDialogController_LoadButton= value;
					changed[ExpertDialogControllerProps.PROPERTY_26_LOADBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IExpertDialogController_SaveButton;
		public ButtonController SaveButton
		{
			get {
				return _IExpertDialogController_SaveButton;
			}
			set {
				if (!___initialized) {
					_IExpertDialogController_SaveButton= value;
					changed[ExpertDialogControllerProps.PROPERTY_27_SAVEBUTTON_ID] = true;
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
			if (ExpertDialogControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExpertDialogControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
