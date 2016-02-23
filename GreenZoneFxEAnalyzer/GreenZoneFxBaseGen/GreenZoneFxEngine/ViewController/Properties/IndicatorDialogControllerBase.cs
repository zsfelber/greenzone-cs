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
		public static bool RmiGetProperty(IIndicatorDialogController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_22_INDICATORPANEL1_ID:
					value = controller.IndicatorPanel1;
					return true;
				case PROPERTY_23_OKBUTTON_ID:
					value = controller.OkButton;
					return true;
				case PROPERTY_24__CANCELBUTTON_ID:
					value = controller._CancelButton;
					return true;
				case PROPERTY_25_LOADBUTTON_ID:
					value = controller.LoadButton;
					return true;
				case PROPERTY_26_SAVEBUTTON_ID:
					value = controller.SaveButton;
					return true;
				case PROPERTY_27_REMOVEBUTTON_ID:
					value = controller.RemoveButton;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorDialogController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorDialogController controller, GreenRmiObjectBuffer buffer)
		{
			controller.IndicatorPanel1 = (IIndicatorPanelController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_22_INDICATORPANEL1_ID];
			controller.OkButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_23_OKBUTTON_ID];
			controller._CancelButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID];
			controller.LoadButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_25_LOADBUTTON_ID];
			controller.SaveButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_26_SAVEBUTTON_ID];
			controller.RemoveButton = (ButtonController) buffer.ChangedProps[IndicatorDialogControllerProps.PROPERTY_27_REMOVEBUTTON_ID];
		}

		public static void AddDependencies(IIndicatorDialogController controller)
		{
			controller.Dependencies.Add(controller.IndicatorPanel1);
			controller.Dependencies.Add(controller.OkButton);
			controller.Dependencies.Add(controller._CancelButton);
			controller.Dependencies.Add(controller.LoadButton);
			controller.Dependencies.Add(controller.SaveButton);
			controller.Dependencies.Add(controller.RemoveButton);
		}

		public static void SerializationRead(IIndicatorDialogController controller, SerializationInfo info, StreamingContext context)
		{
			controller.IndicatorPanel1 = (IIndicatorPanelController) info.GetValue("IndicatorPanel1", typeof(IIndicatorPanelController));
			controller.OkButton = (ButtonController) info.GetValue("OkButton", typeof(ButtonController));
			controller._CancelButton = (ButtonController) info.GetValue("_CancelButton", typeof(ButtonController));
			controller.LoadButton = (ButtonController) info.GetValue("LoadButton", typeof(ButtonController));
			controller.SaveButton = (ButtonController) info.GetValue("SaveButton", typeof(ButtonController));
			controller.RemoveButton = (ButtonController) info.GetValue("RemoveButton", typeof(ButtonController));
		}

		public static void SerializationWrite(IIndicatorDialogController controller, SerializationInfo info, StreamingContext context)
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
			IndicatorDialogControllerProps.AddDependencies(this);
		}

		public IndicatorDialogControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this);
		}

		public IndicatorDialogControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this);
		}

		public IndicatorDialogControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorDialogControllerProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this);
		}

		protected IndicatorDialogControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorDialogControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorDialogControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorDialogControllerProps.SerializationWrite(this, info, context);
		}

		IIndicatorPanelController indicatorPanel1;
		public IIndicatorPanelController IndicatorPanel1
		{
			get {
				return indicatorPanel1;
			}
			set {
				if (!___initialized) {
					indicatorPanel1= value;
					changed[IndicatorDialogControllerProps.PROPERTY_22_INDICATORPANEL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController okButton;
		public ButtonController OkButton
		{
			get {
				return okButton;
			}
			set {
				if (!___initialized) {
					okButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_23_OKBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController __CancelButton;
		public ButtonController _CancelButton
		{
			get {
				return __CancelButton;
			}
			set {
				if (!___initialized) {
					__CancelButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController loadButton;
		public ButtonController LoadButton
		{
			get {
				return loadButton;
			}
			set {
				if (!___initialized) {
					loadButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_25_LOADBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController saveButton;
		public ButtonController SaveButton
		{
			get {
				return saveButton;
			}
			set {
				if (!___initialized) {
					saveButton= value;
					changed[IndicatorDialogControllerProps.PROPERTY_26_SAVEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController removeButton;
		public ButtonController RemoveButton
		{
			get {
				return removeButton;
			}
			set {
				if (!___initialized) {
					removeButton= value;
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
			if (IndicatorDialogControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorDialogControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
