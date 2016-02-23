using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class MainWindowControllerProps
	{
		public const int PROPERTY_22_EATESTER_ID = 22;
		public const int PROPERTY_23_SCRIPTRUNNER_ID = 23;
		public const int PROPERTY_24_TABPAGES_ID = 24;
		public const int PROPERTY_25_NAVIGATORCONTROLLER_ID = 25;
		public const int PROPERTY_26_ORDERSTABLE_ID = 26;
		public const int PROPERTY_27_ORDERSOVERVIEW_ID = 27;
		public const int PROPERTY_28_TABCONTROL1_ID = 28;
		public const int PROPERTY_29_LAUNCHERTABCONTROL_ID = 29;
		public const int PROPERTY_30_SPLITCONTAINER1_ID = 30;
		public const int PROPERTY_31_SPLITCONTAINER2_ID = 31;
		public const int PROPERTY_32_ENVIRONMENTCOMBO_ID = 32;
		public const int PROPERTY_33_TOOLSTRIPBUTTON1_ID = 33;
		public const int PROPERTY_34_TOOLSTRIPBUTTON2_ID = 34;
		public const int PROPERTY_35_TOOLSTRIPBUTTON3_ID = 35;
		public const int PROPERTY_36_ORDERSOVERVIEWTOOLSTRIPMENUITEM_ID = 36;
		public const int PROPERTY_37_ORDERSTABLETOOLSTRIPMENUITEM_ID = 37;
		public const int PROPERTY_38_NAVIGATORTOOLSTRIPMENUITEM_ID = 38;
		public const int PROPERTY_39_EDITENVORINMENTSTOOLSTRIPMENUITEM_ID = 39;
		public const int PROPERTY_40_OPTIONSTOOLSTRIPMENUITEM_ID = 40;
		public const int PROPERTY_41_NEWENVIRONMENTTOOLSTRIPMENUITEM_ID = 41;
		public const int PROPERTY_42_UPDATEENVIRONMENTMENUITEM_ID = 42;
		public const int PROPERTY_43_TIMELABEL_ID = 43;
		public const int PROPERTY_44_OPENLABEL_ID = 44;
		public const int PROPERTY_45_LOWLABEL_ID = 45;
		public const int PROPERTY_46_HIGHLABEL_ID = 46;
		public const int PROPERTY_47_CLOSELABEL_ID = 47;
		public const int PROPERTY_48_VALUELABEL_ID = 48;
		public const int PROPERTY_49_STATUSLABEL_ID = 49;
		public const int PROPERTY_50_TOOLSTRIPSTATUSLABEL2_ID = 50;
		public const int PROPERTY_51_OLABEL_ID = 51;
		public const int PROPERTY_52_LLABEL_ID = 52;
		public const int PROPERTY_53_HLABEL_ID = 53;
		public const int PROPERTY_54_CLABEL_ID = 54;
		public const int PROPERTY_55_VLABEL_ID = 55;
		public static bool RmiGetProperty(IMainWindowController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case MainWindowControllerProps.PROPERTY_22_EATESTER_ID:
					value = controller.EaTester;
					return true;
				case MainWindowControllerProps.PROPERTY_23_SCRIPTRUNNER_ID:
					value = controller.ScriptRunner;
					return true;
				case MainWindowControllerProps.PROPERTY_24_TABPAGES_ID:
					value = controller.TabPages;
					return true;
				case MainWindowControllerProps.PROPERTY_25_NAVIGATORCONTROLLER_ID:
					value = controller.NavigatorController;
					return true;
				case MainWindowControllerProps.PROPERTY_26_ORDERSTABLE_ID:
					value = controller.OrdersTable;
					return true;
				case MainWindowControllerProps.PROPERTY_27_ORDERSOVERVIEW_ID:
					value = controller.OrdersOverview;
					return true;
				case MainWindowControllerProps.PROPERTY_28_TABCONTROL1_ID:
					value = controller.TabControl1;
					return true;
				case MainWindowControllerProps.PROPERTY_29_LAUNCHERTABCONTROL_ID:
					value = controller.LauncherTabControl;
					return true;
				case MainWindowControllerProps.PROPERTY_30_SPLITCONTAINER1_ID:
					value = controller.SplitContainer1;
					return true;
				case MainWindowControllerProps.PROPERTY_31_SPLITCONTAINER2_ID:
					value = controller.SplitContainer2;
					return true;
				case MainWindowControllerProps.PROPERTY_32_ENVIRONMENTCOMBO_ID:
					value = controller.EnvironmentCombo;
					return true;
				case MainWindowControllerProps.PROPERTY_33_TOOLSTRIPBUTTON1_ID:
					value = controller.ToolStripButton1;
					return true;
				case MainWindowControllerProps.PROPERTY_34_TOOLSTRIPBUTTON2_ID:
					value = controller.ToolStripButton2;
					return true;
				case MainWindowControllerProps.PROPERTY_35_TOOLSTRIPBUTTON3_ID:
					value = controller.ToolStripButton3;
					return true;
				case MainWindowControllerProps.PROPERTY_36_ORDERSOVERVIEWTOOLSTRIPMENUITEM_ID:
					value = controller.OrdersOverviewToolStripMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_37_ORDERSTABLETOOLSTRIPMENUITEM_ID:
					value = controller.OrdersTableToolStripMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_38_NAVIGATORTOOLSTRIPMENUITEM_ID:
					value = controller.NavigatorToolStripMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_39_EDITENVORINMENTSTOOLSTRIPMENUITEM_ID:
					value = controller.EditEnvorinmentsToolStripMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_40_OPTIONSTOOLSTRIPMENUITEM_ID:
					value = controller.OptionsToolStripMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_41_NEWENVIRONMENTTOOLSTRIPMENUITEM_ID:
					value = controller.NewEnvironmentToolStripMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_42_UPDATEENVIRONMENTMENUITEM_ID:
					value = controller.UpdateEnvironmentMenuItem;
					return true;
				case MainWindowControllerProps.PROPERTY_43_TIMELABEL_ID:
					value = controller.TimeLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_44_OPENLABEL_ID:
					value = controller.OpenLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_45_LOWLABEL_ID:
					value = controller.LowLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_46_HIGHLABEL_ID:
					value = controller.HighLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_47_CLOSELABEL_ID:
					value = controller.CloseLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_48_VALUELABEL_ID:
					value = controller.ValueLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_49_STATUSLABEL_ID:
					value = controller.StatusLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_50_TOOLSTRIPSTATUSLABEL2_ID:
					value = controller.ToolStripStatusLabel2;
					return true;
				case MainWindowControllerProps.PROPERTY_51_OLABEL_ID:
					value = controller.OLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_52_LLABEL_ID:
					value = controller.LLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_53_HLABEL_ID:
					value = controller.HLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_54_CLABEL_ID:
					value = controller.CLabel;
					return true;
				case MainWindowControllerProps.PROPERTY_55_VLABEL_ID:
					value = controller.VLabel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IMainWindowController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case MainWindowControllerProps.PROPERTY_24_TABPAGES_ID:
					controller.TabPages = (IList<IMainWinTabPageController>) value;
					return true;
				case MainWindowControllerProps.PROPERTY_26_ORDERSTABLE_ID:
					controller.OrdersTable = (IOrdersTableController) value;
					return true;
				case MainWindowControllerProps.PROPERTY_27_ORDERSOVERVIEW_ID:
					controller.OrdersOverview = (IOrdersOverviewController) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IMainWindowController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.EaTester = (IEATesterController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_22_EATESTER_ID];
			controller.ScriptRunner = (IScriptRunnerController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_23_SCRIPTRUNNER_ID];
			controller.NavigatorController = (INavigatorController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_25_NAVIGATORCONTROLLER_ID];
			controller.TabControl1 = (TabController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_28_TABCONTROL1_ID];
			controller.LauncherTabControl = (TabController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_29_LAUNCHERTABCONTROL_ID];
			controller.SplitContainer1 = (SplitController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_30_SPLITCONTAINER1_ID];
			controller.SplitContainer2 = (SplitController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_31_SPLITCONTAINER2_ID];
			controller.EnvironmentCombo = (ComboController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_32_ENVIRONMENTCOMBO_ID];
			controller.ToolStripButton1 = (ButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_33_TOOLSTRIPBUTTON1_ID];
			controller.ToolStripButton2 = (ButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_34_TOOLSTRIPBUTTON2_ID];
			controller.ToolStripButton3 = (ButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_35_TOOLSTRIPBUTTON3_ID];
			controller.OrdersOverviewToolStripMenuItem = (ToggleButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_36_ORDERSOVERVIEWTOOLSTRIPMENUITEM_ID];
			controller.OrdersTableToolStripMenuItem = (ToggleButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_37_ORDERSTABLETOOLSTRIPMENUITEM_ID];
			controller.NavigatorToolStripMenuItem = (ToggleButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_38_NAVIGATORTOOLSTRIPMENUITEM_ID];
			controller.EditEnvorinmentsToolStripMenuItem = (ToggleButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_39_EDITENVORINMENTSTOOLSTRIPMENUITEM_ID];
			controller.OptionsToolStripMenuItem = (ToggleButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_40_OPTIONSTOOLSTRIPMENUITEM_ID];
			controller.NewEnvironmentToolStripMenuItem = (ToggleButtonController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_41_NEWENVIRONMENTTOOLSTRIPMENUITEM_ID];
			controller.UpdateEnvironmentMenuItem = (ComboController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_42_UPDATEENVIRONMENTMENUITEM_ID];
			controller.TimeLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_43_TIMELABEL_ID];
			controller.OpenLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_44_OPENLABEL_ID];
			controller.LowLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_45_LOWLABEL_ID];
			controller.HighLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_46_HIGHLABEL_ID];
			controller.CloseLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_47_CLOSELABEL_ID];
			controller.ValueLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_48_VALUELABEL_ID];
			controller.StatusLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_49_STATUSLABEL_ID];
			controller.ToolStripStatusLabel2 = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_50_TOOLSTRIPSTATUSLABEL2_ID];
			controller.OLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_51_OLABEL_ID];
			controller.LLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_52_LLABEL_ID];
			controller.HLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_53_HLABEL_ID];
			controller.CLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_54_CLABEL_ID];
			controller.VLabel = (LabelledController) buffer.ChangedProps[MainWindowControllerProps.PROPERTY_55_VLABEL_ID];
		}

		public static void AddDependencies(IMainWindowController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.EaTester);
			controller.Dependencies.Add(controller.ScriptRunner);
			controller.Dependencies.Add(controller.NavigatorController);
			controller.Dependencies.Add(controller.TabControl1);
			controller.Dependencies.Add(controller.LauncherTabControl);
			controller.Dependencies.Add(controller.SplitContainer1);
			controller.Dependencies.Add(controller.SplitContainer2);
			controller.Dependencies.Add(controller.EnvironmentCombo);
			controller.Dependencies.Add(controller.ToolStripButton1);
			controller.Dependencies.Add(controller.ToolStripButton2);
			controller.Dependencies.Add(controller.ToolStripButton3);
			controller.Dependencies.Add(controller.OrdersOverviewToolStripMenuItem);
			controller.Dependencies.Add(controller.OrdersTableToolStripMenuItem);
			controller.Dependencies.Add(controller.NavigatorToolStripMenuItem);
			controller.Dependencies.Add(controller.EditEnvorinmentsToolStripMenuItem);
			controller.Dependencies.Add(controller.OptionsToolStripMenuItem);
			controller.Dependencies.Add(controller.NewEnvironmentToolStripMenuItem);
			controller.Dependencies.Add(controller.UpdateEnvironmentMenuItem);
			controller.Dependencies.Add(controller.TimeLabel);
			controller.Dependencies.Add(controller.OpenLabel);
			controller.Dependencies.Add(controller.LowLabel);
			controller.Dependencies.Add(controller.HighLabel);
			controller.Dependencies.Add(controller.CloseLabel);
			controller.Dependencies.Add(controller.ValueLabel);
			controller.Dependencies.Add(controller.StatusLabel);
			controller.Dependencies.Add(controller.ToolStripStatusLabel2);
			controller.Dependencies.Add(controller.OLabel);
			controller.Dependencies.Add(controller.LLabel);
			controller.Dependencies.Add(controller.HLabel);
			controller.Dependencies.Add(controller.CLabel);
			controller.Dependencies.Add(controller.VLabel);
		}

		public static void SerializationRead(IMainWindowController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.EaTester = (IEATesterController) info.GetValue("EaTester", typeof(IEATesterController));
			controller.ScriptRunner = (IScriptRunnerController) info.GetValue("ScriptRunner", typeof(IScriptRunnerController));
			controller.TabPages = (IList<IMainWinTabPageController>) info.GetValue("TabPages", typeof(IList<IMainWinTabPageController>));
			controller.NavigatorController = (INavigatorController) info.GetValue("NavigatorController", typeof(INavigatorController));
			controller.OrdersTable = (IOrdersTableController) info.GetValue("OrdersTable", typeof(IOrdersTableController));
			controller.OrdersOverview = (IOrdersOverviewController) info.GetValue("OrdersOverview", typeof(IOrdersOverviewController));
			controller.TabControl1 = (TabController) info.GetValue("TabControl1", typeof(TabController));
			controller.LauncherTabControl = (TabController) info.GetValue("LauncherTabControl", typeof(TabController));
			controller.SplitContainer1 = (SplitController) info.GetValue("SplitContainer1", typeof(SplitController));
			controller.SplitContainer2 = (SplitController) info.GetValue("SplitContainer2", typeof(SplitController));
			controller.EnvironmentCombo = (ComboController) info.GetValue("EnvironmentCombo", typeof(ComboController));
			controller.ToolStripButton1 = (ButtonController) info.GetValue("ToolStripButton1", typeof(ButtonController));
			controller.ToolStripButton2 = (ButtonController) info.GetValue("ToolStripButton2", typeof(ButtonController));
			controller.ToolStripButton3 = (ButtonController) info.GetValue("ToolStripButton3", typeof(ButtonController));
			controller.OrdersOverviewToolStripMenuItem = (ToggleButtonController) info.GetValue("OrdersOverviewToolStripMenuItem", typeof(ToggleButtonController));
			controller.OrdersTableToolStripMenuItem = (ToggleButtonController) info.GetValue("OrdersTableToolStripMenuItem", typeof(ToggleButtonController));
			controller.NavigatorToolStripMenuItem = (ToggleButtonController) info.GetValue("NavigatorToolStripMenuItem", typeof(ToggleButtonController));
			controller.EditEnvorinmentsToolStripMenuItem = (ToggleButtonController) info.GetValue("EditEnvorinmentsToolStripMenuItem", typeof(ToggleButtonController));
			controller.OptionsToolStripMenuItem = (ToggleButtonController) info.GetValue("OptionsToolStripMenuItem", typeof(ToggleButtonController));
			controller.NewEnvironmentToolStripMenuItem = (ToggleButtonController) info.GetValue("NewEnvironmentToolStripMenuItem", typeof(ToggleButtonController));
			controller.UpdateEnvironmentMenuItem = (ComboController) info.GetValue("UpdateEnvironmentMenuItem", typeof(ComboController));
			controller.TimeLabel = (LabelledController) info.GetValue("TimeLabel", typeof(LabelledController));
			controller.OpenLabel = (LabelledController) info.GetValue("OpenLabel", typeof(LabelledController));
			controller.LowLabel = (LabelledController) info.GetValue("LowLabel", typeof(LabelledController));
			controller.HighLabel = (LabelledController) info.GetValue("HighLabel", typeof(LabelledController));
			controller.CloseLabel = (LabelledController) info.GetValue("CloseLabel", typeof(LabelledController));
			controller.ValueLabel = (LabelledController) info.GetValue("ValueLabel", typeof(LabelledController));
			controller.StatusLabel = (LabelledController) info.GetValue("StatusLabel", typeof(LabelledController));
			controller.ToolStripStatusLabel2 = (LabelledController) info.GetValue("ToolStripStatusLabel2", typeof(LabelledController));
			controller.OLabel = (LabelledController) info.GetValue("OLabel", typeof(LabelledController));
			controller.LLabel = (LabelledController) info.GetValue("LLabel", typeof(LabelledController));
			controller.HLabel = (LabelledController) info.GetValue("HLabel", typeof(LabelledController));
			controller.CLabel = (LabelledController) info.GetValue("CLabel", typeof(LabelledController));
			controller.VLabel = (LabelledController) info.GetValue("VLabel", typeof(LabelledController));
		}

		public static void SerializationWrite(IMainWindowController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("EaTester", controller.EaTester);
			info.AddValue("ScriptRunner", controller.ScriptRunner);
			info.AddValue("TabPages", controller.TabPages);
			info.AddValue("NavigatorController", controller.NavigatorController);
			info.AddValue("OrdersTable", controller.OrdersTable);
			info.AddValue("OrdersOverview", controller.OrdersOverview);
			info.AddValue("TabControl1", controller.TabControl1);
			info.AddValue("LauncherTabControl", controller.LauncherTabControl);
			info.AddValue("SplitContainer1", controller.SplitContainer1);
			info.AddValue("SplitContainer2", controller.SplitContainer2);
			info.AddValue("EnvironmentCombo", controller.EnvironmentCombo);
			info.AddValue("ToolStripButton1", controller.ToolStripButton1);
			info.AddValue("ToolStripButton2", controller.ToolStripButton2);
			info.AddValue("ToolStripButton3", controller.ToolStripButton3);
			info.AddValue("OrdersOverviewToolStripMenuItem", controller.OrdersOverviewToolStripMenuItem);
			info.AddValue("OrdersTableToolStripMenuItem", controller.OrdersTableToolStripMenuItem);
			info.AddValue("NavigatorToolStripMenuItem", controller.NavigatorToolStripMenuItem);
			info.AddValue("EditEnvorinmentsToolStripMenuItem", controller.EditEnvorinmentsToolStripMenuItem);
			info.AddValue("OptionsToolStripMenuItem", controller.OptionsToolStripMenuItem);
			info.AddValue("NewEnvironmentToolStripMenuItem", controller.NewEnvironmentToolStripMenuItem);
			info.AddValue("UpdateEnvironmentMenuItem", controller.UpdateEnvironmentMenuItem);
			info.AddValue("TimeLabel", controller.TimeLabel);
			info.AddValue("OpenLabel", controller.OpenLabel);
			info.AddValue("LowLabel", controller.LowLabel);
			info.AddValue("HighLabel", controller.HighLabel);
			info.AddValue("CloseLabel", controller.CloseLabel);
			info.AddValue("ValueLabel", controller.ValueLabel);
			info.AddValue("StatusLabel", controller.StatusLabel);
			info.AddValue("ToolStripStatusLabel2", controller.ToolStripStatusLabel2);
			info.AddValue("OLabel", controller.OLabel);
			info.AddValue("LLabel", controller.LLabel);
			info.AddValue("HLabel", controller.HLabel);
			info.AddValue("CLabel", controller.CLabel);
			info.AddValue("VLabel", controller.VLabel);
		}

	}
	public abstract class MainWindowControllerBase : FormController, IMainWindowController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IMainWindowController_TabPages_Changed;
		public event PropertyChangedEventHandler IMainWindowController_OrdersTable_Changed;
		public event PropertyChangedEventHandler IMainWindowController_OrdersOverview_Changed;

		public MainWindowControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this, false);
		}

		public MainWindowControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this, false);
		}

		public MainWindowControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this, false);
		}

		public MainWindowControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			MainWindowControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this, false);
		}

		protected MainWindowControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			MainWindowControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			MainWindowControllerProps.SerializationWrite(this, info, context, false);
		}


		IEATesterController _IMainWindowController_EaTester;
		public IEATesterController EaTester
		{
			get {
				return _IMainWindowController_EaTester;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_EaTester= value;
					changed[MainWindowControllerProps.PROPERTY_22_EATESTER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IScriptRunnerController _IMainWindowController_ScriptRunner;
		public IScriptRunnerController ScriptRunner
		{
			get {
				return _IMainWindowController_ScriptRunner;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_ScriptRunner= value;
					changed[MainWindowControllerProps.PROPERTY_23_SCRIPTRUNNER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IEnvironmentRuntime Environment
		{
			get {
				return (IEnvironmentRuntime)EnvironmentCombo.SelectedItem;
			}
		}

		IList<IMainWinTabPageController> _IMainWindowController_TabPages;
		public IList<IMainWinTabPageController> TabPages
		{
			get {
				return _IMainWindowController_TabPages;
			}
			set {
				if (_IMainWindowController_TabPages != value) {
					_IMainWindowController_TabPages= value;
					changed[MainWindowControllerProps.PROPERTY_24_TABPAGES_ID] = true;
					if (IMainWindowController_TabPages_Changed != null)
						IMainWindowController_TabPages_Changed(this, new PropertyChangedEventArgs("TabPages", value));
				}
			}
		}

		INavigatorController _IMainWindowController_NavigatorController;
		public INavigatorController NavigatorController
		{
			get {
				return _IMainWindowController_NavigatorController;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_NavigatorController= value;
					changed[MainWindowControllerProps.PROPERTY_25_NAVIGATORCONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTableController _IMainWindowController_OrdersTable;
		public IOrdersTableController OrdersTable
		{
			get {
				return _IMainWindowController_OrdersTable;
			}
			set {
				if (_IMainWindowController_OrdersTable != value) {
					_IMainWindowController_OrdersTable= value;
					changed[MainWindowControllerProps.PROPERTY_26_ORDERSTABLE_ID] = true;
					if (IMainWindowController_OrdersTable_Changed != null)
						IMainWindowController_OrdersTable_Changed(this, new PropertyChangedEventArgs("OrdersTable", value));
				}
			}
		}

		IOrdersOverviewController _IMainWindowController_OrdersOverview;
		public IOrdersOverviewController OrdersOverview
		{
			get {
				return _IMainWindowController_OrdersOverview;
			}
			set {
				if (_IMainWindowController_OrdersOverview != value) {
					_IMainWindowController_OrdersOverview= value;
					changed[MainWindowControllerProps.PROPERTY_27_ORDERSOVERVIEW_ID] = true;
					if (IMainWindowController_OrdersOverview_Changed != null)
						IMainWindowController_OrdersOverview_Changed(this, new PropertyChangedEventArgs("OrdersOverview", value));
				}
			}
		}

		TabController _IMainWindowController_TabControl1;
		public TabController TabControl1
		{
			get {
				return _IMainWindowController_TabControl1;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_TabControl1= value;
					changed[MainWindowControllerProps.PROPERTY_28_TABCONTROL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TabController _IMainWindowController_LauncherTabControl;
		public TabController LauncherTabControl
		{
			get {
				return _IMainWindowController_LauncherTabControl;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_LauncherTabControl= value;
					changed[MainWindowControllerProps.PROPERTY_29_LAUNCHERTABCONTROL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SplitController _IMainWindowController_SplitContainer1;
		public SplitController SplitContainer1
		{
			get {
				return _IMainWindowController_SplitContainer1;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_SplitContainer1= value;
					changed[MainWindowControllerProps.PROPERTY_30_SPLITCONTAINER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SplitController _IMainWindowController_SplitContainer2;
		public SplitController SplitContainer2
		{
			get {
				return _IMainWindowController_SplitContainer2;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_SplitContainer2= value;
					changed[MainWindowControllerProps.PROPERTY_31_SPLITCONTAINER2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IMainWindowController_EnvironmentCombo;
		public ComboController EnvironmentCombo
		{
			get {
				return _IMainWindowController_EnvironmentCombo;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_EnvironmentCombo= value;
					changed[MainWindowControllerProps.PROPERTY_32_ENVIRONMENTCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IMainWindowController_ToolStripButton1;
		public ButtonController ToolStripButton1
		{
			get {
				return _IMainWindowController_ToolStripButton1;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_ToolStripButton1= value;
					changed[MainWindowControllerProps.PROPERTY_33_TOOLSTRIPBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IMainWindowController_ToolStripButton2;
		public ButtonController ToolStripButton2
		{
			get {
				return _IMainWindowController_ToolStripButton2;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_ToolStripButton2= value;
					changed[MainWindowControllerProps.PROPERTY_34_TOOLSTRIPBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IMainWindowController_ToolStripButton3;
		public ButtonController ToolStripButton3
		{
			get {
				return _IMainWindowController_ToolStripButton3;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_ToolStripButton3= value;
					changed[MainWindowControllerProps.PROPERTY_35_TOOLSTRIPBUTTON3_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IMainWindowController_OrdersOverviewToolStripMenuItem;
		public ToggleButtonController OrdersOverviewToolStripMenuItem
		{
			get {
				return _IMainWindowController_OrdersOverviewToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_OrdersOverviewToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_36_ORDERSOVERVIEWTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IMainWindowController_OrdersTableToolStripMenuItem;
		public ToggleButtonController OrdersTableToolStripMenuItem
		{
			get {
				return _IMainWindowController_OrdersTableToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_OrdersTableToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_37_ORDERSTABLETOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IMainWindowController_NavigatorToolStripMenuItem;
		public ToggleButtonController NavigatorToolStripMenuItem
		{
			get {
				return _IMainWindowController_NavigatorToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_NavigatorToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_38_NAVIGATORTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IMainWindowController_EditEnvorinmentsToolStripMenuItem;
		public ToggleButtonController EditEnvorinmentsToolStripMenuItem
		{
			get {
				return _IMainWindowController_EditEnvorinmentsToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_EditEnvorinmentsToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_39_EDITENVORINMENTSTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IMainWindowController_OptionsToolStripMenuItem;
		public ToggleButtonController OptionsToolStripMenuItem
		{
			get {
				return _IMainWindowController_OptionsToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_OptionsToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_40_OPTIONSTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IMainWindowController_NewEnvironmentToolStripMenuItem;
		public ToggleButtonController NewEnvironmentToolStripMenuItem
		{
			get {
				return _IMainWindowController_NewEnvironmentToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_NewEnvironmentToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_41_NEWENVIRONMENTTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IMainWindowController_UpdateEnvironmentMenuItem;
		public ComboController UpdateEnvironmentMenuItem
		{
			get {
				return _IMainWindowController_UpdateEnvironmentMenuItem;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_UpdateEnvironmentMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_42_UPDATEENVIRONMENTMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_TimeLabel;
		public LabelledController TimeLabel
		{
			get {
				return _IMainWindowController_TimeLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_TimeLabel= value;
					changed[MainWindowControllerProps.PROPERTY_43_TIMELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_OpenLabel;
		public LabelledController OpenLabel
		{
			get {
				return _IMainWindowController_OpenLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_OpenLabel= value;
					changed[MainWindowControllerProps.PROPERTY_44_OPENLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_LowLabel;
		public LabelledController LowLabel
		{
			get {
				return _IMainWindowController_LowLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_LowLabel= value;
					changed[MainWindowControllerProps.PROPERTY_45_LOWLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_HighLabel;
		public LabelledController HighLabel
		{
			get {
				return _IMainWindowController_HighLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_HighLabel= value;
					changed[MainWindowControllerProps.PROPERTY_46_HIGHLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_CloseLabel;
		public LabelledController CloseLabel
		{
			get {
				return _IMainWindowController_CloseLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_CloseLabel= value;
					changed[MainWindowControllerProps.PROPERTY_47_CLOSELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_ValueLabel;
		public LabelledController ValueLabel
		{
			get {
				return _IMainWindowController_ValueLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_ValueLabel= value;
					changed[MainWindowControllerProps.PROPERTY_48_VALUELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_StatusLabel;
		public LabelledController StatusLabel
		{
			get {
				return _IMainWindowController_StatusLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_StatusLabel= value;
					changed[MainWindowControllerProps.PROPERTY_49_STATUSLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_ToolStripStatusLabel2;
		public LabelledController ToolStripStatusLabel2
		{
			get {
				return _IMainWindowController_ToolStripStatusLabel2;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_ToolStripStatusLabel2= value;
					changed[MainWindowControllerProps.PROPERTY_50_TOOLSTRIPSTATUSLABEL2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_OLabel;
		public LabelledController OLabel
		{
			get {
				return _IMainWindowController_OLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_OLabel= value;
					changed[MainWindowControllerProps.PROPERTY_51_OLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_LLabel;
		public LabelledController LLabel
		{
			get {
				return _IMainWindowController_LLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_LLabel= value;
					changed[MainWindowControllerProps.PROPERTY_52_LLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_HLabel;
		public LabelledController HLabel
		{
			get {
				return _IMainWindowController_HLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_HLabel= value;
					changed[MainWindowControllerProps.PROPERTY_53_HLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_CLabel;
		public LabelledController CLabel
		{
			get {
				return _IMainWindowController_CLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_CLabel= value;
					changed[MainWindowControllerProps.PROPERTY_54_CLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IMainWindowController_VLabel;
		public LabelledController VLabel
		{
			get {
				return _IMainWindowController_VLabel;
			}
			set {
				if (!___initialized) {
					_IMainWindowController_VLabel= value;
					changed[MainWindowControllerProps.PROPERTY_55_VLABEL_ID] = true;
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
			if (MainWindowControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (MainWindowControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
