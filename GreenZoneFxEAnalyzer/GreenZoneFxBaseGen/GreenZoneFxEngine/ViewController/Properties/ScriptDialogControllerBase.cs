using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class ScriptDialogControllerProps
	{
		public const int PROPERTY_22_SCRIPTRUNTIMEPANEL_ID = 22;
		public const int PROPERTY_23_OKBUTTON_ID = 23;
		public const int PROPERTY_24__CANCELBUTTON_ID = 24;
		public const int PROPERTY_25_RESETBUTTON_ID = 25;
		public const int PROPERTY_26_LOADBUTTON_ID = 26;
		public const int PROPERTY_27_SAVEBUTTON_ID = 27;
		public static bool RmiGetProperty(IScriptDialogController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_22_SCRIPTRUNTIMEPANEL_ID:
					value = controller.ScriptRuntimePanel;
					return true;
				case PROPERTY_23_OKBUTTON_ID:
					value = controller.OkButton;
					return true;
				case PROPERTY_24__CANCELBUTTON_ID:
					value = controller._CancelButton;
					return true;
				case PROPERTY_25_RESETBUTTON_ID:
					value = controller.ResetButton;
					return true;
				case PROPERTY_26_LOADBUTTON_ID:
					value = controller.LoadButton;
					return true;
				case PROPERTY_27_SAVEBUTTON_ID:
					value = controller.SaveButton;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptDialogController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptDialogController controller, GreenRmiObjectBuffer buffer)
		{
			controller.ScriptRuntimePanel = (IScriptPropertiesController) buffer.ChangedProps[ScriptDialogControllerProps.PROPERTY_22_SCRIPTRUNTIMEPANEL_ID];
			controller.OkButton = (ButtonController) buffer.ChangedProps[ScriptDialogControllerProps.PROPERTY_23_OKBUTTON_ID];
			controller._CancelButton = (ButtonController) buffer.ChangedProps[ScriptDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID];
			controller.ResetButton = (ButtonController) buffer.ChangedProps[ScriptDialogControllerProps.PROPERTY_25_RESETBUTTON_ID];
			controller.LoadButton = (ButtonController) buffer.ChangedProps[ScriptDialogControllerProps.PROPERTY_26_LOADBUTTON_ID];
			controller.SaveButton = (ButtonController) buffer.ChangedProps[ScriptDialogControllerProps.PROPERTY_27_SAVEBUTTON_ID];
		}

		public static void AddDependencies(IScriptDialogController controller)
		{
			controller.Dependencies.Add(controller.ScriptRuntimePanel);
			controller.Dependencies.Add(controller.OkButton);
			controller.Dependencies.Add(controller._CancelButton);
			controller.Dependencies.Add(controller.ResetButton);
			controller.Dependencies.Add(controller.LoadButton);
			controller.Dependencies.Add(controller.SaveButton);
		}

		public static void SerializationRead(IScriptDialogController controller, SerializationInfo info, StreamingContext context)
		{
			controller.ScriptRuntimePanel = (IScriptPropertiesController) info.GetValue("ScriptRuntimePanel", typeof(IScriptPropertiesController));
			controller.OkButton = (ButtonController) info.GetValue("OkButton", typeof(ButtonController));
			controller._CancelButton = (ButtonController) info.GetValue("_CancelButton", typeof(ButtonController));
			controller.ResetButton = (ButtonController) info.GetValue("ResetButton", typeof(ButtonController));
			controller.LoadButton = (ButtonController) info.GetValue("LoadButton", typeof(ButtonController));
			controller.SaveButton = (ButtonController) info.GetValue("SaveButton", typeof(ButtonController));
		}

		public static void SerializationWrite(IScriptDialogController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ScriptRuntimePanel", controller.ScriptRuntimePanel);
			info.AddValue("OkButton", controller.OkButton);
			info.AddValue("_CancelButton", controller._CancelButton);
			info.AddValue("ResetButton", controller.ResetButton);
			info.AddValue("LoadButton", controller.LoadButton);
			info.AddValue("SaveButton", controller.SaveButton);
		}

	}
	public abstract class ScriptDialogControllerBase : DialogController, IScriptDialogController
	{

		bool ___initialized = false;


		public ScriptDialogControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ScriptDialogControllerProps.AddDependencies(this);
		}

		public ScriptDialogControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			ScriptDialogControllerProps.AddDependencies(this);
		}

		public ScriptDialogControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			ScriptDialogControllerProps.AddDependencies(this);
		}

		public ScriptDialogControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptDialogControllerProps.Initialize(this, buffer);
			___initialized = true;
			ScriptDialogControllerProps.AddDependencies(this);
		}

		protected ScriptDialogControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptDialogControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ScriptDialogControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptDialogControllerProps.SerializationWrite(this, info, context);
		}

		IScriptPropertiesController scriptRuntimePanel;
		public IScriptPropertiesController ScriptRuntimePanel
		{
			get {
				return scriptRuntimePanel;
			}
			set {
				if (!___initialized) {
					scriptRuntimePanel= value;
					changed[ScriptDialogControllerProps.PROPERTY_22_SCRIPTRUNTIMEPANEL_ID] = true;
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
					changed[ScriptDialogControllerProps.PROPERTY_23_OKBUTTON_ID] = true;
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
					changed[ScriptDialogControllerProps.PROPERTY_24__CANCELBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController resetButton;
		public ButtonController ResetButton
		{
			get {
				return resetButton;
			}
			set {
				if (!___initialized) {
					resetButton= value;
					changed[ScriptDialogControllerProps.PROPERTY_25_RESETBUTTON_ID] = true;
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
					changed[ScriptDialogControllerProps.PROPERTY_26_LOADBUTTON_ID] = true;
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
					changed[ScriptDialogControllerProps.PROPERTY_27_SAVEBUTTON_ID] = true;
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
			if (ScriptDialogControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ScriptDialogControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
