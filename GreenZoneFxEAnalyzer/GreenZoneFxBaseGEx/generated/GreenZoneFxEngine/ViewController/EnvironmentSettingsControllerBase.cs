using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class EnvironmentSettingsControllerProps
	{
		public const int PROPERTY_22_TREEVIEW1_ID = 22;
		public const int PROPERTY_23_SPLITCONTAINER1_ID = 23;
		public const int PROPERTY_24_CURRENT_ID = 24;
		public const int PROPERTY_25_CURRENTPANEL_ID = 25;
		public const int PROPERTY_26_SAVEBUTTON_ID = 26;
		public const int PROPERTY_27__CANCELBUTTON_ID = 27;
		public const int PROPERTY_28_RESETBUTTON_ID = 28;
		public static bool RmiGetProperty(IEnvironmentSettingsController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentSettingsControllerProps.PROPERTY_22_TREEVIEW1_ID:
					value = controller.TreeView1;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_23_SPLITCONTAINER1_ID:
					value = controller.SplitContainer1;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_24_CURRENT_ID:
					value = controller.Current;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_25_CURRENTPANEL_ID:
					value = controller.CurrentPanel;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_26_SAVEBUTTON_ID:
					value = controller.SaveButton;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_27__CANCELBUTTON_ID:
					value = controller._CancelButton;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_28_RESETBUTTON_ID:
					value = controller.ResetButton;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentSettingsController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentSettingsControllerProps.PROPERTY_24_CURRENT_ID:
					controller.Current = (TreeNodeController) value;
					return true;
				case EnvironmentSettingsControllerProps.PROPERTY_25_CURRENTPANEL_ID:
					controller.CurrentPanel = (BufferedPropertyGridController) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentSettingsController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.TreeView1 = (TreeController) buffer.ChangedProps[EnvironmentSettingsControllerProps.PROPERTY_22_TREEVIEW1_ID];
			controller.SplitContainer1 = (SplitController) buffer.ChangedProps[EnvironmentSettingsControllerProps.PROPERTY_23_SPLITCONTAINER1_ID];
			controller.SaveButton = (ButtonController) buffer.ChangedProps[EnvironmentSettingsControllerProps.PROPERTY_26_SAVEBUTTON_ID];
			controller._CancelButton = (ButtonController) buffer.ChangedProps[EnvironmentSettingsControllerProps.PROPERTY_27__CANCELBUTTON_ID];
			controller.ResetButton = (ButtonController) buffer.ChangedProps[EnvironmentSettingsControllerProps.PROPERTY_28_RESETBUTTON_ID];
		}

		public static void AddDependencies(IEnvironmentSettingsController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.TreeView1);
			controller.Dependencies.Add(controller.SplitContainer1);
			controller.Dependencies.Add(controller.SaveButton);
			controller.Dependencies.Add(controller._CancelButton);
			controller.Dependencies.Add(controller.ResetButton);
		}

		public static void SerializationRead(IEnvironmentSettingsController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.TreeView1 = (TreeController) info.GetValue("TreeView1", typeof(TreeController));
			controller.SplitContainer1 = (SplitController) info.GetValue("SplitContainer1", typeof(SplitController));
			controller.Current = (TreeNodeController) info.GetValue("Current", typeof(TreeNodeController));
			controller.CurrentPanel = (BufferedPropertyGridController) info.GetValue("CurrentPanel", typeof(BufferedPropertyGridController));
			controller.SaveButton = (ButtonController) info.GetValue("SaveButton", typeof(ButtonController));
			controller._CancelButton = (ButtonController) info.GetValue("_CancelButton", typeof(ButtonController));
			controller.ResetButton = (ButtonController) info.GetValue("ResetButton", typeof(ButtonController));
		}

		public static void SerializationWrite(IEnvironmentSettingsController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("TreeView1", controller.TreeView1);
			info.AddValue("SplitContainer1", controller.SplitContainer1);
			info.AddValue("Current", controller.Current);
			info.AddValue("CurrentPanel", controller.CurrentPanel);
			info.AddValue("SaveButton", controller.SaveButton);
			info.AddValue("_CancelButton", controller._CancelButton);
			info.AddValue("ResetButton", controller.ResetButton);
		}

	}
	public abstract class EnvironmentSettingsControllerBase : DialogController, IEnvironmentSettingsController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IEnvironmentSettingsController_Current_Changed;
		public event PropertyChangedEventHandler IEnvironmentSettingsController_CurrentPanel_Changed;

		public EnvironmentSettingsControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EnvironmentSettingsControllerProps.AddDependencies(this, false);
		}

		public EnvironmentSettingsControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			EnvironmentSettingsControllerProps.AddDependencies(this, false);
		}

		public EnvironmentSettingsControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			EnvironmentSettingsControllerProps.AddDependencies(this, false);
		}

		public EnvironmentSettingsControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentSettingsControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			EnvironmentSettingsControllerProps.AddDependencies(this, false);
		}

		protected EnvironmentSettingsControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentSettingsControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EnvironmentSettingsControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentSettingsControllerProps.SerializationWrite(this, info, context, false);
		}


		TreeController _IEnvironmentSettingsController_TreeView1;
		public TreeController TreeView1
		{
			get {
				return _IEnvironmentSettingsController_TreeView1;
			}
			set {
				if (!___initialized) {
					_IEnvironmentSettingsController_TreeView1= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_22_TREEVIEW1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SplitController _IEnvironmentSettingsController_SplitContainer1;
		public SplitController SplitContainer1
		{
			get {
				return _IEnvironmentSettingsController_SplitContainer1;
			}
			set {
				if (!___initialized) {
					_IEnvironmentSettingsController_SplitContainer1= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_23_SPLITCONTAINER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TreeNodeController _IEnvironmentSettingsController_Current;
		public TreeNodeController Current
		{
			get {
				return _IEnvironmentSettingsController_Current;
			}
			set {
				if (_IEnvironmentSettingsController_Current != value) {
					_IEnvironmentSettingsController_Current= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_24_CURRENT_ID] = true;
					if (IEnvironmentSettingsController_Current_Changed != null)
						IEnvironmentSettingsController_Current_Changed(this, new PropertyChangedEventArgs("Current", value));
				}
			}
		}

		BufferedPropertyGridController _IEnvironmentSettingsController_CurrentPanel;
		public BufferedPropertyGridController CurrentPanel
		{
			get {
				return _IEnvironmentSettingsController_CurrentPanel;
			}
			set {
				if (_IEnvironmentSettingsController_CurrentPanel != value) {
					_IEnvironmentSettingsController_CurrentPanel= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_25_CURRENTPANEL_ID] = true;
					if (IEnvironmentSettingsController_CurrentPanel_Changed != null)
						IEnvironmentSettingsController_CurrentPanel_Changed(this, new PropertyChangedEventArgs("CurrentPanel", value));
				}
			}
		}

		ButtonController _IEnvironmentSettingsController_SaveButton;
		public ButtonController SaveButton
		{
			get {
				return _IEnvironmentSettingsController_SaveButton;
			}
			set {
				if (!___initialized) {
					_IEnvironmentSettingsController_SaveButton= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_26_SAVEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEnvironmentSettingsController__CancelButton;
		public ButtonController _CancelButton
		{
			get {
				return _IEnvironmentSettingsController__CancelButton;
			}
			set {
				if (!___initialized) {
					_IEnvironmentSettingsController__CancelButton= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_27__CANCELBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEnvironmentSettingsController_ResetButton;
		public ButtonController ResetButton
		{
			get {
				return _IEnvironmentSettingsController_ResetButton;
			}
			set {
				if (!___initialized) {
					_IEnvironmentSettingsController_ResetButton= value;
					changed[EnvironmentSettingsControllerProps.PROPERTY_28_RESETBUTTON_ID] = true;
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
			if (EnvironmentSettingsControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EnvironmentSettingsControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
