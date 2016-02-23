using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class IndicatorDialogControllerProps
	{
		public const int PROPERTY_22_INDICATORPANEL1_ID = 22;
		public const int PROPERTY_23_OKBUTTON_ID = 23;
		public const int PROPERTY_24__CANCELBUTTON_ID = 24;
		public const int PROPERTY_25_LOADBUTTON_ID = 25;
		public const int PROPERTY_26_SAVEBUTTON_ID = 26;
		public const int PROPERTY_27_REMOVEBUTTON_ID = 27;
		public static bool RmiGetProperty(IIndicatorDialogController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case IndicatorDialogControllerProps.PROPERTY_22_INDICATORPANEL1_ID:
					value = controller.IndicatorPanel1;
					return true;
				case IndicatorDialogControllerProps.PROPERTY_23_OKBUTTON_ID:
					value = controller.OkButton;
					return true;
				case IndicatorDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID:
					value = controller._CancelButton;
					return true;
				case IndicatorDialogControllerProps.PROPERTY_25_LOADBUTTON_ID:
					value = controller.LoadButton;
					return true;
				case IndicatorDialogControllerProps.PROPERTY_26_SAVEBUTTON_ID:
					value = controller.SaveButton;
					return true;
				case IndicatorDialogControllerProps.PROPERTY_27_REMOVEBUTTON_ID:
					value = controller.RemoveButton;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorDialogController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorDialogController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.IndicatorPanel1 = (IIndicatorPanelController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_22_INDICATORPANEL1_ID];
			controller.OkButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_23_OKBUTTON_ID];
			controller._CancelButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID];
			controller.LoadButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_25_LOADBUTTON_ID];
			controller.SaveButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_26_SAVEBUTTON_ID];
			controller.RemoveButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_27_REMOVEBUTTON_ID];
		}

		public static void AddDependencies(IIndicatorDialogController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.IndicatorPanel1);
			controller.Dependencies.Add(controller.OkButton);
			controller.Dependencies.Add(controller._CancelButton);
			controller.Dependencies.Add(controller.LoadButton);
			controller.Dependencies.Add(controller.SaveButton);
			controller.Dependencies.Add(controller.RemoveButton);
		}

		public static void SerializationRead(IIndicatorDialogController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.IndicatorPanel1 = (IIndicatorPanelController) info.GetValue("IndicatorPanel1", typeof(IIndicatorPanelController));
			controller.OkButton = (ButtonController) info.GetValue("OkButton", typeof(ButtonController));
			controller._CancelButton = (ButtonController) info.GetValue("_CancelButton", typeof(ButtonController));
			controller.LoadButton = (ButtonController) info.GetValue("LoadButton", typeof(ButtonController));
			controller.SaveButton = (ButtonController) info.GetValue("SaveButton", typeof(ButtonController));
			controller.RemoveButton = (ButtonController) info.GetValue("RemoveButton", typeof(ButtonController));
		}

		public static void SerializationWrite(IIndicatorDialogController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("IndicatorPanel1", controller.IndicatorPanel1);
			info.AddValue("OkButton", controller.OkButton);
			info.AddValue("_CancelButton", controller._CancelButton);
			info.AddValue("LoadButton", controller.LoadButton);
			info.AddValue("SaveButton", controller.SaveButton);
			info.AddValue("RemoveButton", controller.RemoveButton);
		}

	}
	public abstract class IndicatorDialogControllerBase : DialogController, IIndicatorDialogController
	{

		bool ___initialized = false;


		public IndicatorDialogControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this, false);
		}

		public IndicatorDialogControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this, false);
		}

		public IndicatorDialogControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this, false);
		}

		public IndicatorDialogControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorDialogControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this, false);
		}

		protected IndicatorDialogControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorDialogControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorDialogControllerProps.SerializationWrite(this, info, context, false);
		}


		IIndicatorPanelController _IIndicatorDialogController_IndicatorPanel1;
		public IIndicatorPanelController IndicatorPanel1
		{
			get {
				return _IIndicatorDialogController_IndicatorPanel1;
			}
			set {
				if (!___initialized) {
					_IIndicatorDialogController_IndicatorPanel1= value;
					changed[IndicatorDialogControllerProps.PROPERTY_22_INDICATORPANEL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorDialogController_OkButton;
		public ButtonController OkButton
		{
			get {
				return _IIndicatorDialogController_OkButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorDialogController_OkButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_23_OKBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorDialogController__CancelButton;
		public ButtonController _CancelButton
		{
			get {
				return _IIndicatorDialogController__CancelButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorDialogController__CancelButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorDialogController_LoadButton;
		public ButtonController LoadButton
		{
			get {
				return _IIndicatorDialogController_LoadButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorDialogController_LoadButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_25_LOADBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorDialogController_SaveButton;
		public ButtonController SaveButton
		{
			get {
				return _IIndicatorDialogController_SaveButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorDialogController_SaveButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_26_SAVEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorDialogController_RemoveButton;
		public ButtonController RemoveButton
		{
			get {
				return _IIndicatorDialogController_RemoveButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorDialogController_RemoveButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_27_REMOVEBUTTON_ID] = true;
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
			if (IndicatorDialogControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorDialogControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
