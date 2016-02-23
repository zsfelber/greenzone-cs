using GreenZoneFxEngine.Trading;
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
		public const int PROPERTY_18_CHARTGROUPRUNTIME_ID = 18;
		public const int PROPERTY_19_TABLELAYOUTPANEL1_ID = 19;
		public const int PROPERTY_20_SCRIPTCOMBO_ID = 20;
		public const int PROPERTY_21_EACOMBO_ID = 21;
		public const int PROPERTY_22_OPENEABUTTON_ID = 22;
		public const int PROPERTY_23_OPENSCRIPTBUTTON_ID = 23;
		public const int PROPERTY_24_INTESTBUTTON_ID = 24;
		public const int PROPERTY_25_TOGGLEBOTTOMBARBUTTON1_ID = 25;
		public const int PROPERTY_26_TOGGLEBOTTOMBARBUTTON2_ID = 26;
		public const int PROPERTY_27_BOTTOMTOOLSTRIP_ID = 27;
		public static bool RmiGetProperty(IChartGroupController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (MainWinTabPageControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ChartGroupControllerProps.PROPERTY_17_CHARTVIEWS_ID:
					value = controller.ChartViews;
					return true;
				case ChartGroupControllerProps.PROPERTY_18_CHARTGROUPRUNTIME_ID:
					value = controller.ChartGroupRuntime;
					return true;
				case ChartGroupControllerProps.PROPERTY_19_TABLELAYOUTPANEL1_ID:
					value = controller.TableLayoutPanel1;
					return true;
				case ChartGroupControllerProps.PROPERTY_20_SCRIPTCOMBO_ID:
					value = controller.ScriptCombo;
					return true;
				case ChartGroupControllerProps.PROPERTY_21_EACOMBO_ID:
					value = controller.EaCombo;
					return true;
				case ChartGroupControllerProps.PROPERTY_22_OPENEABUTTON_ID:
					value = controller.OpenEaButton;
					return true;
				case ChartGroupControllerProps.PROPERTY_23_OPENSCRIPTBUTTON_ID:
					value = controller.OpenScriptButton;
					return true;
				case ChartGroupControllerProps.PROPERTY_24_INTESTBUTTON_ID:
					value = controller.InTestButton;
					return true;
				case ChartGroupControllerProps.PROPERTY_25_TOGGLEBOTTOMBARBUTTON1_ID:
					value = controller.ToggleBottomBarButton1;
					return true;
				case ChartGroupControllerProps.PROPERTY_26_TOGGLEBOTTOMBARBUTTON2_ID:
					value = controller.ToggleBottomBarButton2;
					return true;
				case ChartGroupControllerProps.PROPERTY_27_BOTTOMTOOLSTRIP_ID:
					value = controller.BottomToolStrip;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartGroupController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (MainWinTabPageControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartGroupController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.Initialize(controller, buffer, true);
			}
			controller.ChartViews = (IList<IChartViewController>) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_17_CHARTVIEWS_ID];
			controller.ChartGroupRuntime = (IServerChartGroupRuntime) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_18_CHARTGROUPRUNTIME_ID];
			controller.TableLayoutPanel1 = (MultiSplitController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_19_TABLELAYOUTPANEL1_ID];
			controller.ScriptCombo = (ComboController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_20_SCRIPTCOMBO_ID];
			controller.EaCombo = (ComboController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_21_EACOMBO_ID];
			controller.OpenEaButton = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_22_OPENEABUTTON_ID];
			controller.OpenScriptButton = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_23_OPENSCRIPTBUTTON_ID];
			controller.InTestButton = (ToggleButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_24_INTESTBUTTON_ID];
			controller.ToggleBottomBarButton1 = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_25_TOGGLEBOTTOMBARBUTTON1_ID];
			controller.ToggleBottomBarButton2 = (ButtonController) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_26_TOGGLEBOTTOMBARBUTTON2_ID];
			controller.BottomToolStrip = (Controller) buffer.ChangedProps[ChartGroupControllerProps.PROPERTY_27_BOTTOMTOOLSTRIP_ID];
		}

		public static void AddDependencies(IChartGroupController controller, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.AddRange(controller.ChartViews);
			controller.Dependencies.Add(controller.ChartGroupRuntime);
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

		public static void SerializationRead(IChartGroupController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.ChartViews = (IList<IChartViewController>) info.GetValue("ChartViews", typeof(IList<IChartViewController>));
			controller.ChartGroupRuntime = (IServerChartGroupRuntime) info.GetValue("ChartGroupRuntime", typeof(IServerChartGroupRuntime));
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

		public static void SerializationWrite(IChartGroupController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("ChartViews", controller.ChartViews);
			info.AddValue("ChartGroupRuntime", controller.ChartGroupRuntime);
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
			ChartGroupControllerProps.AddDependencies(this, false);
		}

		public ChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this, false);
		}

		public ChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this, false);
		}

		public ChartGroupControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartGroupControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this, false);
		}

		protected ChartGroupControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartGroupControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartGroupControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartGroupControllerProps.SerializationWrite(this, info, context, false);
		}


		IList<IChartViewController> _IChartGroupController_ChartViews;
		public IList<IChartViewController> ChartViews
		{
			get {
				return _IChartGroupController_ChartViews;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_ChartViews= value;
					changed[ChartGroupControllerProps.PROPERTY_17_CHARTVIEWS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IServerChartGroupRuntime _IChartGroupController_ChartGroupRuntime;
		public IServerChartGroupRuntime ChartGroupRuntime
		{
			get {
				return _IChartGroupController_ChartGroupRuntime;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_ChartGroupRuntime= value;
					changed[ChartGroupControllerProps.PROPERTY_18_CHARTGROUPRUNTIME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		MultiSplitController _IChartGroupController_TableLayoutPanel1;
		public MultiSplitController TableLayoutPanel1
		{
			get {
				return _IChartGroupController_TableLayoutPanel1;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_TableLayoutPanel1= value;
					changed[ChartGroupControllerProps.PROPERTY_19_TABLELAYOUTPANEL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IChartGroupController_ScriptCombo;
		public ComboController ScriptCombo
		{
			get {
				return _IChartGroupController_ScriptCombo;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_ScriptCombo= value;
					changed[ChartGroupControllerProps.PROPERTY_20_SCRIPTCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IChartGroupController_EaCombo;
		public ComboController EaCombo
		{
			get {
				return _IChartGroupController_EaCombo;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_EaCombo= value;
					changed[ChartGroupControllerProps.PROPERTY_21_EACOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartGroupController_OpenEaButton;
		public ButtonController OpenEaButton
		{
			get {
				return _IChartGroupController_OpenEaButton;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_OpenEaButton= value;
					changed[ChartGroupControllerProps.PROPERTY_22_OPENEABUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartGroupController_OpenScriptButton;
		public ButtonController OpenScriptButton
		{
			get {
				return _IChartGroupController_OpenScriptButton;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_OpenScriptButton= value;
					changed[ChartGroupControllerProps.PROPERTY_23_OPENSCRIPTBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IChartGroupController_InTestButton;
		public ToggleButtonController InTestButton
		{
			get {
				return _IChartGroupController_InTestButton;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_InTestButton= value;
					changed[ChartGroupControllerProps.PROPERTY_24_INTESTBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartGroupController_ToggleBottomBarButton1;
		public ButtonController ToggleBottomBarButton1
		{
			get {
				return _IChartGroupController_ToggleBottomBarButton1;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_ToggleBottomBarButton1= value;
					changed[ChartGroupControllerProps.PROPERTY_25_TOGGLEBOTTOMBARBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartGroupController_ToggleBottomBarButton2;
		public ButtonController ToggleBottomBarButton2
		{
			get {
				return _IChartGroupController_ToggleBottomBarButton2;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_ToggleBottomBarButton2= value;
					changed[ChartGroupControllerProps.PROPERTY_26_TOGGLEBOTTOMBARBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Controller _IChartGroupController_BottomToolStrip;
		public Controller BottomToolStrip
		{
			get {
				return _IChartGroupController_BottomToolStrip;
			}
			set {
				if (!___initialized) {
					_IChartGroupController_BottomToolStrip= value;
					changed[ChartGroupControllerProps.PROPERTY_27_BOTTOMTOOLSTRIP_ID] = true;
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
			if (ChartGroupControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartGroupControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
