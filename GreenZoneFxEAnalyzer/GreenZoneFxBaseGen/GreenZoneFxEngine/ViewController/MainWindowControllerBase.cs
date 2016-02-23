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
		public static bool RmiGetProperty(IMainWindowController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_22_EATESTER_ID:
					value = controller.EaTester;
					return true;
				case PROPERTY_23_SCRIPTRUNNER_ID:
					value = controller.ScriptRunner;
					return true;
				case PROPERTY_24_TABPAGES_ID:
					value = controller.TabPages;
					return true;
				case PROPERTY_25_NAVIGATORCONTROLLER_ID:
					value = controller.NavigatorController;
					return true;
				case PROPERTY_26_ORDERSTABLE_ID:
					value = controller.OrdersTable;
					return true;
				case PROPERTY_27_ORDERSOVERVIEW_ID:
					value = controller.OrdersOverview;
					return true;
				case PROPERTY_28_TABCONTROL1_ID:
					value = controller.TabControl1;
					return true;
				case PROPERTY_29_LAUNCHERTABCONTROL_ID:
					value = controller.LauncherTabControl;
					return true;
				case PROPERTY_30_SPLITCONTAINER1_ID:
					value = controller.SplitContainer1;
					return true;
				case PROPERTY_31_SPLITCONTAINER2_ID:
					value = controller.SplitContainer2;
					return true;
				case PROPERTY_32_ENVIRONMENTCOMBO_ID:
					value = controller.EnvironmentCombo;
					return true;
				case PROPERTY_33_TOOLSTRIPBUTTON1_ID:
					value = controller.ToolStripButton1;
					return true;
				case PROPERTY_34_TOOLSTRIPBUTTON2_ID:
					value = controller.ToolStripButton2;
					return true;
				case PROPERTY_35_TOOLSTRIPBUTTON3_ID:
					value = controller.ToolStripButton3;
					return true;
				case PROPERTY_36_ORDERSOVERVIEWTOOLSTRIPMENUITEM_ID:
					value = controller.OrdersOverviewToolStripMenuItem;
					return true;
				case PROPERTY_37_ORDERSTABLETOOLSTRIPMENUITEM_ID:
					value = controller.OrdersTableToolStripMenuItem;
					return true;
				case PROPERTY_38_NAVIGATORTOOLSTRIPMENUITEM_ID:
					value = controller.NavigatorToolStripMenuItem;
					return true;
				case PROPERTY_39_EDITENVORINMENTSTOOLSTRIPMENUITEM_ID:
					value = controller.EditEnvorinmentsToolStripMenuItem;
					return true;
				case PROPERTY_40_OPTIONSTOOLSTRIPMENUITEM_ID:
					value = controller.OptionsToolStripMenuItem;
					return true;
				case PROPERTY_41_NEWENVIRONMENTTOOLSTRIPMENUITEM_ID:
					value = controller.NewEnvironmentToolStripMenuItem;
					return true;
				case PROPERTY_42_UPDATEENVIRONMENTMENUITEM_ID:
					value = controller.UpdateEnvironmentMenuItem;
					return true;
				case PROPERTY_43_TIMELABEL_ID:
					value = controller.TimeLabel;
					return true;
				case PROPERTY_44_OPENLABEL_ID:
					value = controller.OpenLabel;
					return true;
				case PROPERTY_45_LOWLABEL_ID:
					value = controller.LowLabel;
					return true;
				case PROPERTY_46_HIGHLABEL_ID:
					value = controller.HighLabel;
					return true;
				case PROPERTY_47_CLOSELABEL_ID:
					value = controller.CloseLabel;
					return true;
				case PROPERTY_48_VALUELABEL_ID:
					value = controller.ValueLabel;
					return true;
				case PROPERTY_49_STATUSLABEL_ID:
					value = controller.StatusLabel;
					return true;
				case PROPERTY_50_TOOLSTRIPSTATUSLABEL2_ID:
					value = controller.ToolStripStatusLabel2;
					return true;
				case PROPERTY_51_OLABEL_ID:
					value = controller.OLabel;
					return true;
				case PROPERTY_52_LLABEL_ID:
					value = controller.LLabel;
					return true;
				case PROPERTY_53_HLABEL_ID:
					value = controller.HLabel;
					return true;
				case PROPERTY_54_CLABEL_ID:
					value = controller.CLabel;
					return true;
				case PROPERTY_55_VLABEL_ID:
					value = controller.VLabel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IMainWindowController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_24_TABPAGES_ID:
					controller.TabPages = (IList<IMainWinTabPageController>) value;
					return true;
				case PROPERTY_26_ORDERSTABLE_ID:
					controller.OrdersTable = (IOrdersTableController) value;
					return true;
				case PROPERTY_27_ORDERSOVERVIEW_ID:
					controller.OrdersOverview = (IOrdersOverviewController) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IMainWindowController controller, GreenRmiObjectBuffer buffer)
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

		public static void AddDependencies(IMainWindowController controller)
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

		public static void SerializationRead(IMainWindowController controller, SerializationInfo info, StreamingContext context)
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

		public static void SerializationWrite(IMainWindowController controller, SerializationInfo info, StreamingContext context)
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

		public event PropertyChangedEventHandler TabPagesChanged;
		public event PropertyChangedEventHandler OrdersTableChanged;
		public event PropertyChangedEventHandler OrdersOverviewChanged;

		public MainWindowControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this);
		}

		public MainWindowControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this);
		}

		public MainWindowControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this);
		}

		public MainWindowControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			MainWindowControllerProps.Initialize(this, buffer);
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this);
		}

		protected MainWindowControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			MainWindowControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			MainWindowControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			MainWindowControllerProps.SerializationWrite(this, info, context);
		}

		IEATesterController eaTester;
		public IEATesterController EaTester
		{
			get {
				return eaTester;
			}
			set {
				if (!___initialized) {
					eaTester= value;
					changed[MainWindowControllerProps.PROPERTY_22_EATESTER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IScriptRunnerController scriptRunner;
		public IScriptRunnerController ScriptRunner
		{
			get {
				return scriptRunner;
			}
			set {
				if (!___initialized) {
					scriptRunner= value;
					changed[MainWindowControllerProps.PROPERTY_23_SCRIPTRUNNER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IList<IMainWinTabPageController> tabPages;
		public IList<IMainWinTabPageController> TabPages
		{
			get {
				return tabPages;
			}
			set {
				if (tabPages != value) {
					tabPages= value;
					changed[MainWindowControllerProps.PROPERTY_24_TABPAGES_ID] = true;
					if (TabPagesChanged != null)
						TabPagesChanged(this, new PropertyChangedEventArgs("TabPages", value));
				}
			}
		}

		INavigatorController navigatorController;
		public INavigatorController NavigatorController
		{
			get {
				return navigatorController;
			}
			set {
				if (!___initialized) {
					navigatorController= value;
					changed[MainWindowControllerProps.PROPERTY_25_NAVIGATORCONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersTableController ordersTable;
		public IOrdersTableController OrdersTable
		{
			get {
				return ordersTable;
			}
			set {
				if (ordersTable != value) {
					ordersTable= value;
					changed[MainWindowControllerProps.PROPERTY_26_ORDERSTABLE_ID] = true;
					if (OrdersTableChanged != null)
						OrdersTableChanged(this, new PropertyChangedEventArgs("OrdersTable", value));
				}
			}
		}

		IOrdersOverviewController ordersOverview;
		public IOrdersOverviewController OrdersOverview
		{
			get {
				return ordersOverview;
			}
			set {
				if (ordersOverview != value) {
					ordersOverview= value;
					changed[MainWindowControllerProps.PROPERTY_27_ORDERSOVERVIEW_ID] = true;
					if (OrdersOverviewChanged != null)
						OrdersOverviewChanged(this, new PropertyChangedEventArgs("OrdersOverview", value));
				}
			}
		}

		TabController tabControl1;
		public TabController TabControl1
		{
			get {
				return tabControl1;
			}
			set {
				if (!___initialized) {
					tabControl1= value;
					changed[MainWindowControllerProps.PROPERTY_28_TABCONTROL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TabController launcherTabControl;
		public TabController LauncherTabControl
		{
			get {
				return launcherTabControl;
			}
			set {
				if (!___initialized) {
					launcherTabControl= value;
					changed[MainWindowControllerProps.PROPERTY_29_LAUNCHERTABCONTROL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SplitController splitContainer1;
		public SplitController SplitContainer1
		{
			get {
				return splitContainer1;
			}
			set {
				if (!___initialized) {
					splitContainer1= value;
					changed[MainWindowControllerProps.PROPERTY_30_SPLITCONTAINER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SplitController splitContainer2;
		public SplitController SplitContainer2
		{
			get {
				return splitContainer2;
			}
			set {
				if (!___initialized) {
					splitContainer2= value;
					changed[MainWindowControllerProps.PROPERTY_31_SPLITCONTAINER2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController environmentCombo;
		public ComboController EnvironmentCombo
		{
			get {
				return environmentCombo;
			}
			set {
				if (!___initialized) {
					environmentCombo= value;
					changed[MainWindowControllerProps.PROPERTY_32_ENVIRONMENTCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toolStripButton1;
		public ButtonController ToolStripButton1
		{
			get {
				return toolStripButton1;
			}
			set {
				if (!___initialized) {
					toolStripButton1= value;
					changed[MainWindowControllerProps.PROPERTY_33_TOOLSTRIPBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toolStripButton2;
		public ButtonController ToolStripButton2
		{
			get {
				return toolStripButton2;
			}
			set {
				if (!___initialized) {
					toolStripButton2= value;
					changed[MainWindowControllerProps.PROPERTY_34_TOOLSTRIPBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toolStripButton3;
		public ButtonController ToolStripButton3
		{
			get {
				return toolStripButton3;
			}
			set {
				if (!___initialized) {
					toolStripButton3= value;
					changed[MainWindowControllerProps.PROPERTY_35_TOOLSTRIPBUTTON3_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController ordersOverviewToolStripMenuItem;
		public ToggleButtonController OrdersOverviewToolStripMenuItem
		{
			get {
				return ordersOverviewToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					ordersOverviewToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_36_ORDERSOVERVIEWTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController ordersTableToolStripMenuItem;
		public ToggleButtonController OrdersTableToolStripMenuItem
		{
			get {
				return ordersTableToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					ordersTableToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_37_ORDERSTABLETOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController navigatorToolStripMenuItem;
		public ToggleButtonController NavigatorToolStripMenuItem
		{
			get {
				return navigatorToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					navigatorToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_38_NAVIGATORTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController editEnvorinmentsToolStripMenuItem;
		public ToggleButtonController EditEnvorinmentsToolStripMenuItem
		{
			get {
				return editEnvorinmentsToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					editEnvorinmentsToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_39_EDITENVORINMENTSTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController optionsToolStripMenuItem;
		public ToggleButtonController OptionsToolStripMenuItem
		{
			get {
				return optionsToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					optionsToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_40_OPTIONSTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController newEnvironmentToolStripMenuItem;
		public ToggleButtonController NewEnvironmentToolStripMenuItem
		{
			get {
				return newEnvironmentToolStripMenuItem;
			}
			set {
				if (!___initialized) {
					newEnvironmentToolStripMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_41_NEWENVIRONMENTTOOLSTRIPMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController updateEnvironmentMenuItem;
		public ComboController UpdateEnvironmentMenuItem
		{
			get {
				return updateEnvironmentMenuItem;
			}
			set {
				if (!___initialized) {
					updateEnvironmentMenuItem= value;
					changed[MainWindowControllerProps.PROPERTY_42_UPDATEENVIRONMENTMENUITEM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController timeLabel;
		public LabelledController TimeLabel
		{
			get {
				return timeLabel;
			}
			set {
				if (!___initialized) {
					timeLabel= value;
					changed[MainWindowControllerProps.PROPERTY_43_TIMELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController openLabel;
		public LabelledController OpenLabel
		{
			get {
				return openLabel;
			}
			set {
				if (!___initialized) {
					openLabel= value;
					changed[MainWindowControllerProps.PROPERTY_44_OPENLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController lowLabel;
		public LabelledController LowLabel
		{
			get {
				return lowLabel;
			}
			set {
				if (!___initialized) {
					lowLabel= value;
					changed[MainWindowControllerProps.PROPERTY_45_LOWLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController highLabel;
		public LabelledController HighLabel
		{
			get {
				return highLabel;
			}
			set {
				if (!___initialized) {
					highLabel= value;
					changed[MainWindowControllerProps.PROPERTY_46_HIGHLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController closeLabel;
		public LabelledController CloseLabel
		{
			get {
				return closeLabel;
			}
			set {
				if (!___initialized) {
					closeLabel= value;
					changed[MainWindowControllerProps.PROPERTY_47_CLOSELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController valueLabel;
		public LabelledController ValueLabel
		{
			get {
				return valueLabel;
			}
			set {
				if (!___initialized) {
					valueLabel= value;
					changed[MainWindowControllerProps.PROPERTY_48_VALUELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController statusLabel;
		public LabelledController StatusLabel
		{
			get {
				return statusLabel;
			}
			set {
				if (!___initialized) {
					statusLabel= value;
					changed[MainWindowControllerProps.PROPERTY_49_STATUSLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController toolStripStatusLabel2;
		public LabelledController ToolStripStatusLabel2
		{
			get {
				return toolStripStatusLabel2;
			}
			set {
				if (!___initialized) {
					toolStripStatusLabel2= value;
					changed[MainWindowControllerProps.PROPERTY_50_TOOLSTRIPSTATUSLABEL2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController oLabel;
		public LabelledController OLabel
		{
			get {
				return oLabel;
			}
			set {
				if (!___initialized) {
					oLabel= value;
					changed[MainWindowControllerProps.PROPERTY_51_OLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController lLabel;
		public LabelledController LLabel
		{
			get {
				return lLabel;
			}
			set {
				if (!___initialized) {
					lLabel= value;
					changed[MainWindowControllerProps.PROPERTY_52_LLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController hLabel;
		public LabelledController HLabel
		{
			get {
				return hLabel;
			}
			set {
				if (!___initialized) {
					hLabel= value;
					changed[MainWindowControllerProps.PROPERTY_53_HLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController cLabel;
		public LabelledController CLabel
		{
			get {
				return cLabel;
			}
			set {
				if (!___initialized) {
					cLabel= value;
					changed[MainWindowControllerProps.PROPERTY_54_CLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController vLabel;
		public LabelledController VLabel
		{
			get {
				return vLabel;
			}
			set {
				if (!___initialized) {
					vLabel= value;
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
			if (MainWindowControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!MainWindowControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
