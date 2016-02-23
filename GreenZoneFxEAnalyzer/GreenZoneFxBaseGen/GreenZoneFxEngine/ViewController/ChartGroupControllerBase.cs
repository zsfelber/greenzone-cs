using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class ChartGroupControllerProps
	{
		public const int PROPERTY_17_CHARTVIEWS_ID = 17;
		public const int PROPERTY_18_TABLELAYOUTPANEL1_ID = 18;
		public const int PROPERTY_19_SCRIPTCOMBO_ID = 19;
		public const int PROPERTY_20_EACOMBO_ID = 20;
		public const int PROPERTY_21_OPENEABUTTON_ID = 21;
		public const int PROPERTY_22_OPENSCRIPTBUTTON_ID = 22;
		public const int PROPERTY_23_INTESTBUTTON_ID = 23;
		public const int PROPERTY_24_TOGGLEBOTTOMBARBUTTON1_ID = 24;
		public const int PROPERTY_25_TOGGLEBOTTOMBARBUTTON2_ID = 25;
		public const int PROPERTY_26_BOTTOMTOOLSTRIP_ID = 26;
		public static bool RmiGetProperty(IChartGroupController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_CHARTVIEWS_ID:
					value = controller.ChartViews;
					return true;
				case PROPERTY_18_TABLELAYOUTPANEL1_ID:
					value = controller.TableLayoutPanel1;
					return true;
				case PROPERTY_19_SCRIPTCOMBO_ID:
					value = controller.ScriptCombo;
					return true;
				case PROPERTY_20_EACOMBO_ID:
					value = controller.EaCombo;
					return true;
				case PROPERTY_21_OPENEABUTTON_ID:
					value = controller.OpenEaButton;
					return true;
				case PROPERTY_22_OPENSCRIPTBUTTON_ID:
					value = controller.OpenScriptButton;
					return true;
				case PROPERTY_23_INTESTBUTTON_ID:
					value = controller.InTestButton;
					return true;
				case PROPERTY_24_TOGGLEBOTTOMBARBUTTON1_ID:
					value = controller.ToggleBottomBarButton1;
					return true;
				case PROPERTY_25_TOGGLEBOTTOMBARBUTTON2_ID:
					value = controller.ToggleBottomBarButton2;
					return true;
				case PROPERTY_26_BOTTOMTOOLSTRIP_ID:
					value = controller.BottomToolStrip;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartGroupController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartGroupController controller, GreenRmiObjectBuffer buffer)
		{
			controller.ChartViews = (IList<IChartViewController>) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_17_CHARTVIEWS_ID];
			controller.TableLayoutPanel1 = (MultiSplitController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_18_TABLELAYOUTPANEL1_ID];
			controller.ScriptCombo = (ComboController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_19_SCRIPTCOMBO_ID];
			controller.EaCombo = (ComboController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_20_EACOMBO_ID];
			controller.OpenEaButton = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_21_OPENEABUTTON_ID];
			controller.OpenScriptButton = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_22_OPENSCRIPTBUTTON_ID];
			controller.InTestButton = (ToggleButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_23_INTESTBUTTON_ID];
			controller.ToggleBottomBarButton1 = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_24_TOGGLEBOTTOMBARBUTTON1_ID];
			controller.ToggleBottomBarButton2 = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_25_TOGGLEBOTTOMBARBUTTON2_ID];
			controller.BottomToolStrip = (Controller) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_26_BOTTOMTOOLSTRIP_ID];
		}

		public static void AddDependencies(IChartGroupController controller)
		{
			controller.Dependencies.AddRange(controller.ChartViews);
			controller.Dependencies.Add(controller.TableLayoutPanel1);
			controller.Dependencies.Add(controller.ScriptCombo);
			controller.Dependencies.Add(controller.EaCombo);
			controller.Dependencies.Add(controller.OpenEaButton);
			controller.Dependencies.Add(controller.OpenScriptButton);
			controller.Dependencies.Add(controller.InTestButton);
			controller.Dependencies.Add(controller.ToggleBottomBarButton1);
			controller.Dependencies.Add(controller.ToggleBottomBarButton2);
			controller.Dependencies.Add(controller.BottomToolStrip);
		}

		public static void SerializationRead(IChartGroupController controller, SerializationInfo info, StreamingContext context)
		{
			controller.ChartViews = (IList<IChartViewController>) info.GetValue("ChartViews", typeof(IList<IChartViewController>));
			controller.TableLayoutPanel1 = (MultiSplitController) info.GetValue("TableLayoutPanel1", typeof(MultiSplitController));
			controller.ScriptCombo = (ComboController) info.GetValue("ScriptCombo", typeof(ComboController));
			controller.EaCombo = (ComboController) info.GetValue("EaCombo", typeof(ComboController));
			controller.OpenEaButton = (ButtonController) info.GetValue("OpenEaButton", typeof(ButtonController));
			controller.OpenScriptButton = (ButtonController) info.GetValue("OpenScriptButton", typeof(ButtonController));
			controller.InTestButton = (ToggleButtonController) info.GetValue("InTestButton", typeof(ToggleButtonController));
			controller.ToggleBottomBarButton1 = (ButtonController) info.GetValue("ToggleBottomBarButton1", typeof(ButtonController));
			controller.ToggleBottomBarButton2 = (ButtonController) info.GetValue("ToggleBottomBarButton2", typeof(ButtonController));
			controller.BottomToolStrip = (Controller) info.GetValue("BottomToolStrip", typeof(Controller));
		}

		public static void SerializationWrite(IChartGroupController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ChartViews", controller.ChartViews);
			info.AddValue("TableLayoutPanel1", controller.TableLayoutPanel1);
			info.AddValue("ScriptCombo", controller.ScriptCombo);
			info.AddValue("EaCombo", controller.EaCombo);
			info.AddValue("OpenEaButton", controller.OpenEaButton);
			info.AddValue("OpenScriptButton", controller.OpenScriptButton);
			info.AddValue("InTestButton", controller.InTestButton);
			info.AddValue("ToggleBottomBarButton1", controller.ToggleBottomBarButton1);
			info.AddValue("ToggleBottomBarButton2", controller.ToggleBottomBarButton2);
			info.AddValue("BottomToolStrip", controller.BottomToolStrip);
		}

	}
	public abstract class ChartGroupControllerBase : MainWinTabPageControllerBase, IChartGroupController
	{

		bool ___initialized = false;


		public ChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this);
		}

		public ChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this);
		}

		public ChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this);
		}

		public ChartGroupControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartGroupControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this);
		}

		protected ChartGroupControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartGroupControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartGroupControllerProps.SerializationWrite(this, info, context);
		}

		IList<IChartViewController> chartViews;
		public IList<IChartViewController> ChartViews
		{
			get {
				return chartViews;
			}
			set {
				if (!___initialized) {
					chartViews= value;
					changed[ChartGroupControllerProps.PROPERTY_17_CHARTVIEWS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		MultiSplitController tableLayoutPanel1;
		public MultiSplitController TableLayoutPanel1
		{
			get {
				return tableLayoutPanel1;
			}
			set {
				if (!___initialized) {
					tableLayoutPanel1= value;
					changed[ChartGroupControllerProps.PROPERTY_18_TABLELAYOUTPANEL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController scriptCombo;
		public ComboController ScriptCombo
		{
			get {
				return scriptCombo;
			}
			set {
				if (!___initialized) {
					scriptCombo= value;
					changed[ChartGroupControllerProps.PROPERTY_19_SCRIPTCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController eaCombo;
		public ComboController EaCombo
		{
			get {
				return eaCombo;
			}
			set {
				if (!___initialized) {
					eaCombo= value;
					changed[ChartGroupControllerProps.PROPERTY_20_EACOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController openEaButton;
		public ButtonController OpenEaButton
		{
			get {
				return openEaButton;
			}
			set {
				if (!___initialized) {
					openEaButton= value;
					changed[ChartGroupControllerProps.PROPERTY_21_OPENEABUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController openScriptButton;
		public ButtonController OpenScriptButton
		{
			get {
				return openScriptButton;
			}
			set {
				if (!___initialized) {
					openScriptButton= value;
					changed[ChartGroupControllerProps.PROPERTY_22_OPENSCRIPTBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController inTestButton;
		public ToggleButtonController InTestButton
		{
			get {
				return inTestButton;
			}
			set {
				if (!___initialized) {
					inTestButton= value;
					changed[ChartGroupControllerProps.PROPERTY_23_INTESTBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toggleBottomBarButton1;
		public ButtonController ToggleBottomBarButton1
		{
			get {
				return toggleBottomBarButton1;
			}
			set {
				if (!___initialized) {
					toggleBottomBarButton1= value;
					changed[ChartGroupControllerProps.PROPERTY_24_TOGGLEBOTTOMBARBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toggleBottomBarButton2;
		public ButtonController ToggleBottomBarButton2
		{
			get {
				return toggleBottomBarButton2;
			}
			set {
				if (!___initialized) {
					toggleBottomBarButton2= value;
					changed[ChartGroupControllerProps.PROPERTY_25_TOGGLEBOTTOMBARBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Controller bottomToolStrip;
		public Controller BottomToolStrip
		{
			get {
				return bottomToolStrip;
			}
			set {
				if (!___initialized) {
					bottomToolStrip= value;
					changed[ChartGroupControllerProps.PROPERTY_26_BOTTOMTOOLSTRIP_ID] = true;
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
			if (ChartGroupControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartGroupControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
