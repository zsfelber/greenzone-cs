using GreenZoneFxEngine.ViewController.Properties;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class IndicatorPanelControllerProps
	{
		public const int PROPERTY_15_TABPAGE1_ID = 15;
		public const int PROPERTY_16_TABPAGE2_ID = 16;
		public const int PROPERTY_17_TABPAGE3_ID = 17;
		public const int PROPERTY_18_INDICATORRUNTIMEPANEL_ID = 18;
		public const int PROPERTY_19_INDEXESPRGRD_ID = 19;
		public const int PROPERTY_20_LEVELSPRGRD_ID = 20;
		public const int PROPERTY_21_RESET1BUTTON_ID = 21;
		public const int PROPERTY_22_RESET2BUTTON_ID = 22;
		public const int PROPERTY_23_RESET3BUTTON_ID = 23;
		public static bool RmiGetProperty(IIndicatorPanelController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case IndicatorPanelControllerProps.PROPERTY_15_TABPAGE1_ID:
					value = controller.TabPage1;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_16_TABPAGE2_ID:
					value = controller.TabPage2;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_17_TABPAGE3_ID:
					value = controller.TabPage3;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_18_INDICATORRUNTIMEPANEL_ID:
					value = controller.IndicatorRuntimePanel;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_19_INDEXESPRGRD_ID:
					value = controller.IndexesPrgrd;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_20_LEVELSPRGRD_ID:
					value = controller.LevelsPrgrd;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_21_RESET1BUTTON_ID:
					value = controller.Reset1Button;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_22_RESET2BUTTON_ID:
					value = controller.Reset2Button;
					return true;
				case IndicatorPanelControllerProps.PROPERTY_23_RESET3BUTTON_ID:
					value = controller.Reset3Button;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorPanelController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.TabPage1 = (TabPageController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_15_TABPAGE1_ID];
			controller.TabPage2 = (TabPageController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_16_TABPAGE2_ID];
			controller.TabPage3 = (TabPageController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_17_TABPAGE3_ID];
			controller.IndicatorRuntimePanel = (IIndicatorPropertiesController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_18_INDICATORRUNTIMEPANEL_ID];
			controller.IndexesPrgrd = (BufferedPropertyGridController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_19_INDEXESPRGRD_ID];
			controller.LevelsPrgrd = (BufferedPropertyGridController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_20_LEVELSPRGRD_ID];
			controller.Reset1Button = (ButtonController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_21_RESET1BUTTON_ID];
			controller.Reset2Button = (ButtonController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_22_RESET2BUTTON_ID];
			controller.Reset3Button = (ButtonController) buffer.ChangedProps[IndicatorPanelControllerProps.PROPERTY_23_RESET3BUTTON_ID];
		}

		public static void AddDependencies(IIndicatorPanelController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.TabPage1);
			controller.Dependencies.Add(controller.TabPage2);
			controller.Dependencies.Add(controller.TabPage3);
			controller.Dependencies.Add(controller.IndicatorRuntimePanel);
			controller.Dependencies.Add(controller.IndexesPrgrd);
			controller.Dependencies.Add(controller.LevelsPrgrd);
			controller.Dependencies.Add(controller.Reset1Button);
			controller.Dependencies.Add(controller.Reset2Button);
			controller.Dependencies.Add(controller.Reset3Button);
		}

		public static void SerializationRead(IIndicatorPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.TabPage1 = (TabPageController) info.GetValue("TabPage1", typeof(TabPageController));
			controller.TabPage2 = (TabPageController) info.GetValue("TabPage2", typeof(TabPageController));
			controller.TabPage3 = (TabPageController) info.GetValue("TabPage3", typeof(TabPageController));
			controller.IndicatorRuntimePanel = (IIndicatorPropertiesController) info.GetValue("IndicatorRuntimePanel", typeof(IIndicatorPropertiesController));
			controller.IndexesPrgrd = (BufferedPropertyGridController) info.GetValue("IndexesPrgrd", typeof(BufferedPropertyGridController));
			controller.LevelsPrgrd = (BufferedPropertyGridController) info.GetValue("LevelsPrgrd", typeof(BufferedPropertyGridController));
			controller.Reset1Button = (ButtonController) info.GetValue("Reset1Button", typeof(ButtonController));
			controller.Reset2Button = (ButtonController) info.GetValue("Reset2Button", typeof(ButtonController));
			controller.Reset3Button = (ButtonController) info.GetValue("Reset3Button", typeof(ButtonController));
		}

		public static void SerializationWrite(IIndicatorPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("TabPage1", controller.TabPage1);
			info.AddValue("TabPage2", controller.TabPage2);
			info.AddValue("TabPage3", controller.TabPage3);
			info.AddValue("IndicatorRuntimePanel", controller.IndicatorRuntimePanel);
			info.AddValue("IndexesPrgrd", controller.IndexesPrgrd);
			info.AddValue("LevelsPrgrd", controller.LevelsPrgrd);
			info.AddValue("Reset1Button", controller.Reset1Button);
			info.AddValue("Reset2Button", controller.Reset2Button);
			info.AddValue("Reset3Button", controller.Reset3Button);
		}

	}
	public abstract class IndicatorPanelControllerBase : TabController, IIndicatorPanelController
	{

		bool ___initialized = false;


		public IndicatorPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorPanelControllerProps.AddDependencies(this, false);
		}

		public IndicatorPanelControllerBase(GreenRmiManager rmiManager, Controller parent, params TabPageController[] pages)
			: base(rmiManager, parent, pages)
		{
			___initialized = true;
			IndicatorPanelControllerProps.AddDependencies(this, false);
		}

		public IndicatorPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorPanelControllerProps.AddDependencies(this, false);
		}

		protected IndicatorPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorPanelControllerProps.SerializationWrite(this, info, context, false);
		}


		TabPageController _IIndicatorPanelController_TabPage1;
		public TabPageController TabPage1
		{
			get {
				return _IIndicatorPanelController_TabPage1;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_TabPage1= value;
					changed[IndicatorPanelControllerProps.PROPERTY_15_TABPAGE1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TabPageController _IIndicatorPanelController_TabPage2;
		public TabPageController TabPage2
		{
			get {
				return _IIndicatorPanelController_TabPage2;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_TabPage2= value;
					changed[IndicatorPanelControllerProps.PROPERTY_16_TABPAGE2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TabPageController _IIndicatorPanelController_TabPage3;
		public TabPageController TabPage3
		{
			get {
				return _IIndicatorPanelController_TabPage3;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_TabPage3= value;
					changed[IndicatorPanelControllerProps.PROPERTY_17_TABPAGE3_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IIndicatorPropertiesController _IIndicatorPanelController_IndicatorRuntimePanel;
		public IIndicatorPropertiesController IndicatorRuntimePanel
		{
			get {
				return _IIndicatorPanelController_IndicatorRuntimePanel;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_IndicatorRuntimePanel= value;
					changed[IndicatorPanelControllerProps.PROPERTY_18_INDICATORRUNTIMEPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		BufferedPropertyGridController _IIndicatorPanelController_IndexesPrgrd;
		public BufferedPropertyGridController IndexesPrgrd
		{
			get {
				return _IIndicatorPanelController_IndexesPrgrd;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_IndexesPrgrd= value;
					changed[IndicatorPanelControllerProps.PROPERTY_19_INDEXESPRGRD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		BufferedPropertyGridController _IIndicatorPanelController_LevelsPrgrd;
		public BufferedPropertyGridController LevelsPrgrd
		{
			get {
				return _IIndicatorPanelController_LevelsPrgrd;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_LevelsPrgrd= value;
					changed[IndicatorPanelControllerProps.PROPERTY_20_LEVELSPRGRD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorPanelController_Reset1Button;
		public ButtonController Reset1Button
		{
			get {
				return _IIndicatorPanelController_Reset1Button;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_Reset1Button= value;
					changed[IndicatorPanelControllerProps.PROPERTY_21_RESET1BUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorPanelController_Reset2Button;
		public ButtonController Reset2Button
		{
			get {
				return _IIndicatorPanelController_Reset2Button;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_Reset2Button= value;
					changed[IndicatorPanelControllerProps.PROPERTY_22_RESET2BUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorPanelController_Reset3Button;
		public ButtonController Reset3Button
		{
			get {
				return _IIndicatorPanelController_Reset3Button;
			}
			set {
				if (!___initialized) {
					_IIndicatorPanelController_Reset3Button= value;
					changed[IndicatorPanelControllerProps.PROPERTY_23_RESET3BUTTON_ID] = true;
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
			if (IndicatorPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
