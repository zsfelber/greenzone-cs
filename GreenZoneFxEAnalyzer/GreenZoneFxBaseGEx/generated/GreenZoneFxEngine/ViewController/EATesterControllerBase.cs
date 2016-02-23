using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class EATesterControllerProps
	{
		public const int PROPERTY_16_MAINWINDOW_ID = 16;
		public const int PROPERTY_17_DATAGRIDVIEW1_ID = 17;
		public const int PROPERTY_18_SELECTEACOLUMN_ID = 18;
		public const int PROPERTY_19_TABLETESTEASEACOL_ID = 19;
		public const int PROPERTY_20_TABLETESTEASPERIODCOL_ID = 20;
		public const int PROPERTY_21_ICONCOLUMN_ID = 21;
		public const int PROPERTY_22_METHODCOMBO_ID = 22;
		public const int PROPERTY_23_DATAPERIODCOMBO_ID = 23;
		public const int PROPERTY_24_SCROLLACROSSTABSCB_ID = 24;
		public const int PROPERTY_25_SKIPEMPTYPERIODSCB_ID = 25;
		public const int PROPERTY_26_UPDATESPREADTICKCB_ID = 26;
		public const int PROPERTY_27_SPEEDTRACKBAR_ID = 27;
		public const int PROPERTY_28_PROGRESSTRACKBAR1_ID = 28;
		public const int PROPERTY_29_STARTSTOPBUTTON_ID = 29;
		public const int PROPERTY_30_PROPERTIESBUTTON_ID = 30;
		public const int PROPERTY_31_ADDLINKLABEL_ID = 31;
		public const int PROPERTY_32_PAUSEBUTTON_ID = 32;
		public const int PROPERTY_33_PAUSEATBUTTON_ID = 33;
		public const int PROPERTY_34_SNAPBUTTON_ID = 34;
		public const int PROPERTY_35_METHODINFLABEL_ID = 35;
		public const int PROPERTY_36_EAINFLABEL_ID = 36;
		public const int PROPERTY_37_SYMBOLINFLABEL_ID = 37;
		public const int PROPERTY_38_FROMINFLABEL_ID = 38;
		public const int PROPERTY_39_TOINFLABEL_ID = 39;
		public const int PROPERTY_40_METHODLABEL_ID = 40;
		public const int PROPERTY_41_DATAPERIODLABEL_ID = 41;
		public const int PROPERTY_42_TOOLTIP1_ID = 42;
		public static bool RmiGetProperty(IEATesterController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EATesterControllerProps.PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case EATesterControllerProps.PROPERTY_17_DATAGRIDVIEW1_ID:
					value = controller.DataGridView1;
					return true;
				case EATesterControllerProps.PROPERTY_18_SELECTEACOLUMN_ID:
					value = controller.SelectEAColumn;
					return true;
				case EATesterControllerProps.PROPERTY_19_TABLETESTEASEACOL_ID:
					value = controller.TableTestEasEACol;
					return true;
				case EATesterControllerProps.PROPERTY_20_TABLETESTEASPERIODCOL_ID:
					value = controller.TableTestEasPeriodCol;
					return true;
				case EATesterControllerProps.PROPERTY_21_ICONCOLUMN_ID:
					value = controller.IconColumn;
					return true;
				case EATesterControllerProps.PROPERTY_22_METHODCOMBO_ID:
					value = controller.MethodCombo;
					return true;
				case EATesterControllerProps.PROPERTY_23_DATAPERIODCOMBO_ID:
					value = controller.DataPeriodCombo;
					return true;
				case EATesterControllerProps.PROPERTY_24_SCROLLACROSSTABSCB_ID:
					value = controller.ScrollAcrossTabsCb;
					return true;
				case EATesterControllerProps.PROPERTY_25_SKIPEMPTYPERIODSCB_ID:
					value = controller.SkipEmptyPeriodsCb;
					return true;
				case EATesterControllerProps.PROPERTY_26_UPDATESPREADTICKCB_ID:
					value = controller.UpdateSpreadTickCb;
					return true;
				case EATesterControllerProps.PROPERTY_27_SPEEDTRACKBAR_ID:
					value = controller.SpeedTrackBar;
					return true;
				case EATesterControllerProps.PROPERTY_28_PROGRESSTRACKBAR1_ID:
					value = controller.ProgressTrackBar1;
					return true;
				case EATesterControllerProps.PROPERTY_29_STARTSTOPBUTTON_ID:
					value = controller.StartStopButton;
					return true;
				case EATesterControllerProps.PROPERTY_30_PROPERTIESBUTTON_ID:
					value = controller.PropertiesButton;
					return true;
				case EATesterControllerProps.PROPERTY_31_ADDLINKLABEL_ID:
					value = controller.AddLinkLabel;
					return true;
				case EATesterControllerProps.PROPERTY_32_PAUSEBUTTON_ID:
					value = controller.PauseButton;
					return true;
				case EATesterControllerProps.PROPERTY_33_PAUSEATBUTTON_ID:
					value = controller.PauseAtButton;
					return true;
				case EATesterControllerProps.PROPERTY_34_SNAPBUTTON_ID:
					value = controller.SnapButton;
					return true;
				case EATesterControllerProps.PROPERTY_35_METHODINFLABEL_ID:
					value = controller.MethodInfLabel;
					return true;
				case EATesterControllerProps.PROPERTY_36_EAINFLABEL_ID:
					value = controller.EaInfLabel;
					return true;
				case EATesterControllerProps.PROPERTY_37_SYMBOLINFLABEL_ID:
					value = controller.SymbolInfLabel;
					return true;
				case EATesterControllerProps.PROPERTY_38_FROMINFLABEL_ID:
					value = controller.FromInfLabel;
					return true;
				case EATesterControllerProps.PROPERTY_39_TOINFLABEL_ID:
					value = controller.ToInfLabel;
					return true;
				case EATesterControllerProps.PROPERTY_40_METHODLABEL_ID:
					value = controller.MethodLabel;
					return true;
				case EATesterControllerProps.PROPERTY_41_DATAPERIODLABEL_ID:
					value = controller.DataPeriodLabel;
					return true;
				case EATesterControllerProps.PROPERTY_42_TOOLTIP1_ID:
					value = controller.ToolTip1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEATesterController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IEATesterController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_16_MAINWINDOW_ID];
			controller.DataGridView1 = (GridController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_17_DATAGRIDVIEW1_ID];
			controller.SelectEAColumn = (GridColumnController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_18_SELECTEACOLUMN_ID];
			controller.TableTestEasEACol = (GridColumnController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_19_TABLETESTEASEACOL_ID];
			controller.TableTestEasPeriodCol = (GridColumnController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_20_TABLETESTEASPERIODCOL_ID];
			controller.IconColumn = (GridColumnController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_21_ICONCOLUMN_ID];
			controller.MethodCombo = (ComboController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_22_METHODCOMBO_ID];
			controller.DataPeriodCombo = (ComboController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_23_DATAPERIODCOMBO_ID];
			controller.ScrollAcrossTabsCb = (ToggleButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_24_SCROLLACROSSTABSCB_ID];
			controller.SkipEmptyPeriodsCb = (ToggleButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_25_SKIPEMPTYPERIODSCB_ID];
			controller.UpdateSpreadTickCb = (ToggleButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_26_UPDATESPREADTICKCB_ID];
			controller.SpeedTrackBar = (ProgressTrackController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_27_SPEEDTRACKBAR_ID];
			controller.ProgressTrackBar1 = (ProgressTrackController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_28_PROGRESSTRACKBAR1_ID];
			controller.StartStopButton = (ButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_29_STARTSTOPBUTTON_ID];
			controller.PropertiesButton = (ButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_30_PROPERTIESBUTTON_ID];
			controller.AddLinkLabel = (ButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_31_ADDLINKLABEL_ID];
			controller.PauseButton = (ButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_32_PAUSEBUTTON_ID];
			controller.PauseAtButton = (ButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_33_PAUSEATBUTTON_ID];
			controller.SnapButton = (ButtonController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_34_SNAPBUTTON_ID];
			controller.MethodInfLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_35_METHODINFLABEL_ID];
			controller.EaInfLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_36_EAINFLABEL_ID];
			controller.SymbolInfLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_37_SYMBOLINFLABEL_ID];
			controller.FromInfLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_38_FROMINFLABEL_ID];
			controller.ToInfLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_39_TOINFLABEL_ID];
			controller.MethodLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_40_METHODLABEL_ID];
			controller.DataPeriodLabel = (LabelledController) buffer.ChangedProps[EATesterControllerProps.PROPERTY_41_DATAPERIODLABEL_ID];
			controller.ToolTip1 = (ChildControlMap<String>) buffer.ChangedProps[EATesterControllerProps.PROPERTY_42_TOOLTIP1_ID];
		}

		public static void AddDependencies(IEATesterController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.MainWindow);
			controller.Dependencies.Add(controller.DataGridView1);
			controller.Dependencies.Add(controller.SelectEAColumn);
			controller.Dependencies.Add(controller.TableTestEasEACol);
			controller.Dependencies.Add(controller.TableTestEasPeriodCol);
			controller.Dependencies.Add(controller.IconColumn);
			controller.Dependencies.Add(controller.MethodCombo);
			controller.Dependencies.Add(controller.DataPeriodCombo);
			controller.Dependencies.Add(controller.ScrollAcrossTabsCb);
			controller.Dependencies.Add(controller.SkipEmptyPeriodsCb);
			controller.Dependencies.Add(controller.UpdateSpreadTickCb);
			controller.Dependencies.Add(controller.SpeedTrackBar);
			controller.Dependencies.Add(controller.ProgressTrackBar1);
			controller.Dependencies.Add(controller.StartStopButton);
			controller.Dependencies.Add(controller.PropertiesButton);
			controller.Dependencies.Add(controller.AddLinkLabel);
			controller.Dependencies.Add(controller.PauseButton);
			controller.Dependencies.Add(controller.PauseAtButton);
			controller.Dependencies.Add(controller.SnapButton);
			controller.Dependencies.Add(controller.MethodInfLabel);
			controller.Dependencies.Add(controller.EaInfLabel);
			controller.Dependencies.Add(controller.SymbolInfLabel);
			controller.Dependencies.Add(controller.FromInfLabel);
			controller.Dependencies.Add(controller.ToInfLabel);
			controller.Dependencies.Add(controller.MethodLabel);
			controller.Dependencies.Add(controller.DataPeriodLabel);
			controller.Dependencies.Add(controller.ToolTip1);
		}

		public static void SerializationRead(IEATesterController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
			controller.DataGridView1 = (GridController) info.GetValue("DataGridView1", typeof(GridController));
			controller.SelectEAColumn = (GridColumnController) info.GetValue("SelectEAColumn", typeof(GridColumnController));
			controller.TableTestEasEACol = (GridColumnController) info.GetValue("TableTestEasEACol", typeof(GridColumnController));
			controller.TableTestEasPeriodCol = (GridColumnController) info.GetValue("TableTestEasPeriodCol", typeof(GridColumnController));
			controller.IconColumn = (GridColumnController) info.GetValue("IconColumn", typeof(GridColumnController));
			controller.MethodCombo = (ComboController) info.GetValue("MethodCombo", typeof(ComboController));
			controller.DataPeriodCombo = (ComboController) info.GetValue("DataPeriodCombo", typeof(ComboController));
			controller.ScrollAcrossTabsCb = (ToggleButtonController) info.GetValue("ScrollAcrossTabsCb", typeof(ToggleButtonController));
			controller.SkipEmptyPeriodsCb = (ToggleButtonController) info.GetValue("SkipEmptyPeriodsCb", typeof(ToggleButtonController));
			controller.UpdateSpreadTickCb = (ToggleButtonController) info.GetValue("UpdateSpreadTickCb", typeof(ToggleButtonController));
			controller.SpeedTrackBar = (ProgressTrackController) info.GetValue("SpeedTrackBar", typeof(ProgressTrackController));
			controller.ProgressTrackBar1 = (ProgressTrackController) info.GetValue("ProgressTrackBar1", typeof(ProgressTrackController));
			controller.StartStopButton = (ButtonController) info.GetValue("StartStopButton", typeof(ButtonController));
			controller.PropertiesButton = (ButtonController) info.GetValue("PropertiesButton", typeof(ButtonController));
			controller.AddLinkLabel = (ButtonController) info.GetValue("AddLinkLabel", typeof(ButtonController));
			controller.PauseButton = (ButtonController) info.GetValue("PauseButton", typeof(ButtonController));
			controller.PauseAtButton = (ButtonController) info.GetValue("PauseAtButton", typeof(ButtonController));
			controller.SnapButton = (ButtonController) info.GetValue("SnapButton", typeof(ButtonController));
			controller.MethodInfLabel = (LabelledController) info.GetValue("MethodInfLabel", typeof(LabelledController));
			controller.EaInfLabel = (LabelledController) info.GetValue("EaInfLabel", typeof(LabelledController));
			controller.SymbolInfLabel = (LabelledController) info.GetValue("SymbolInfLabel", typeof(LabelledController));
			controller.FromInfLabel = (LabelledController) info.GetValue("FromInfLabel", typeof(LabelledController));
			controller.ToInfLabel = (LabelledController) info.GetValue("ToInfLabel", typeof(LabelledController));
			controller.MethodLabel = (LabelledController) info.GetValue("MethodLabel", typeof(LabelledController));
			controller.DataPeriodLabel = (LabelledController) info.GetValue("DataPeriodLabel", typeof(LabelledController));
			controller.ToolTip1 = (ChildControlMap<String>) info.GetValue("ToolTip1", typeof(ChildControlMap<String>));
		}

		public static void SerializationWrite(IEATesterController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("MainWindow", controller.MainWindow);
			info.AddValue("DataGridView1", controller.DataGridView1);
			info.AddValue("SelectEAColumn", controller.SelectEAColumn);
			info.AddValue("TableTestEasEACol", controller.TableTestEasEACol);
			info.AddValue("TableTestEasPeriodCol", controller.TableTestEasPeriodCol);
			info.AddValue("IconColumn", controller.IconColumn);
			info.AddValue("MethodCombo", controller.MethodCombo);
			info.AddValue("DataPeriodCombo", controller.DataPeriodCombo);
			info.AddValue("ScrollAcrossTabsCb", controller.ScrollAcrossTabsCb);
			info.AddValue("SkipEmptyPeriodsCb", controller.SkipEmptyPeriodsCb);
			info.AddValue("UpdateSpreadTickCb", controller.UpdateSpreadTickCb);
			info.AddValue("SpeedTrackBar", controller.SpeedTrackBar);
			info.AddValue("ProgressTrackBar1", controller.ProgressTrackBar1);
			info.AddValue("StartStopButton", controller.StartStopButton);
			info.AddValue("PropertiesButton", controller.PropertiesButton);
			info.AddValue("AddLinkLabel", controller.AddLinkLabel);
			info.AddValue("PauseButton", controller.PauseButton);
			info.AddValue("PauseAtButton", controller.PauseAtButton);
			info.AddValue("SnapButton", controller.SnapButton);
			info.AddValue("MethodInfLabel", controller.MethodInfLabel);
			info.AddValue("EaInfLabel", controller.EaInfLabel);
			info.AddValue("SymbolInfLabel", controller.SymbolInfLabel);
			info.AddValue("FromInfLabel", controller.FromInfLabel);
			info.AddValue("ToInfLabel", controller.ToInfLabel);
			info.AddValue("MethodLabel", controller.MethodLabel);
			info.AddValue("DataPeriodLabel", controller.DataPeriodLabel);
			info.AddValue("ToolTip1", controller.ToolTip1);
		}

	}
	public abstract class EATesterControllerBase : TabPageController, IEATesterController
	{

		bool ___initialized = false;


		public EATesterControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			EATesterControllerProps.AddDependencies(this, false);
		}

		public EATesterControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			EATesterControllerProps.AddDependencies(this, false);
		}

		public EATesterControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			EATesterControllerProps.AddDependencies(this, false);
		}

		public EATesterControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EATesterControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			EATesterControllerProps.AddDependencies(this, false);
		}

		protected EATesterControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EATesterControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EATesterControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EATesterControllerProps.SerializationWrite(this, info, context, false);
		}


		IMainWindowController _IEATesterController_MainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return _IEATesterController_MainWindow;
			}
			set {
				if (!___initialized) {
					_IEATesterController_MainWindow= value;
					changed[EATesterControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridController _IEATesterController_DataGridView1;
		public GridController DataGridView1
		{
			get {
				return _IEATesterController_DataGridView1;
			}
			set {
				if (!___initialized) {
					_IEATesterController_DataGridView1= value;
					changed[EATesterControllerProps.PROPERTY_17_DATAGRIDVIEW1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IEATesterController_SelectEAColumn;
		public GridColumnController SelectEAColumn
		{
			get {
				return _IEATesterController_SelectEAColumn;
			}
			set {
				if (!___initialized) {
					_IEATesterController_SelectEAColumn= value;
					changed[EATesterControllerProps.PROPERTY_18_SELECTEACOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IEATesterController_TableTestEasEACol;
		public GridColumnController TableTestEasEACol
		{
			get {
				return _IEATesterController_TableTestEasEACol;
			}
			set {
				if (!___initialized) {
					_IEATesterController_TableTestEasEACol= value;
					changed[EATesterControllerProps.PROPERTY_19_TABLETESTEASEACOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IEATesterController_TableTestEasPeriodCol;
		public GridColumnController TableTestEasPeriodCol
		{
			get {
				return _IEATesterController_TableTestEasPeriodCol;
			}
			set {
				if (!___initialized) {
					_IEATesterController_TableTestEasPeriodCol= value;
					changed[EATesterControllerProps.PROPERTY_20_TABLETESTEASPERIODCOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IEATesterController_IconColumn;
		public GridColumnController IconColumn
		{
			get {
				return _IEATesterController_IconColumn;
			}
			set {
				if (!___initialized) {
					_IEATesterController_IconColumn= value;
					changed[EATesterControllerProps.PROPERTY_21_ICONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IEATesterController_MethodCombo;
		public ComboController MethodCombo
		{
			get {
				return _IEATesterController_MethodCombo;
			}
			set {
				if (!___initialized) {
					_IEATesterController_MethodCombo= value;
					changed[EATesterControllerProps.PROPERTY_22_METHODCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IEATesterController_DataPeriodCombo;
		public ComboController DataPeriodCombo
		{
			get {
				return _IEATesterController_DataPeriodCombo;
			}
			set {
				if (!___initialized) {
					_IEATesterController_DataPeriodCombo= value;
					changed[EATesterControllerProps.PROPERTY_23_DATAPERIODCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IEATesterController_ScrollAcrossTabsCb;
		public ToggleButtonController ScrollAcrossTabsCb
		{
			get {
				return _IEATesterController_ScrollAcrossTabsCb;
			}
			set {
				if (!___initialized) {
					_IEATesterController_ScrollAcrossTabsCb= value;
					changed[EATesterControllerProps.PROPERTY_24_SCROLLACROSSTABSCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IEATesterController_SkipEmptyPeriodsCb;
		public ToggleButtonController SkipEmptyPeriodsCb
		{
			get {
				return _IEATesterController_SkipEmptyPeriodsCb;
			}
			set {
				if (!___initialized) {
					_IEATesterController_SkipEmptyPeriodsCb= value;
					changed[EATesterControllerProps.PROPERTY_25_SKIPEMPTYPERIODSCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IEATesterController_UpdateSpreadTickCb;
		public ToggleButtonController UpdateSpreadTickCb
		{
			get {
				return _IEATesterController_UpdateSpreadTickCb;
			}
			set {
				if (!___initialized) {
					_IEATesterController_UpdateSpreadTickCb= value;
					changed[EATesterControllerProps.PROPERTY_26_UPDATESPREADTICKCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController _IEATesterController_SpeedTrackBar;
		public ProgressTrackController SpeedTrackBar
		{
			get {
				return _IEATesterController_SpeedTrackBar;
			}
			set {
				if (!___initialized) {
					_IEATesterController_SpeedTrackBar= value;
					changed[EATesterControllerProps.PROPERTY_27_SPEEDTRACKBAR_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController _IEATesterController_ProgressTrackBar1;
		public ProgressTrackController ProgressTrackBar1
		{
			get {
				return _IEATesterController_ProgressTrackBar1;
			}
			set {
				if (!___initialized) {
					_IEATesterController_ProgressTrackBar1= value;
					changed[EATesterControllerProps.PROPERTY_28_PROGRESSTRACKBAR1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEATesterController_StartStopButton;
		public ButtonController StartStopButton
		{
			get {
				return _IEATesterController_StartStopButton;
			}
			set {
				if (!___initialized) {
					_IEATesterController_StartStopButton= value;
					changed[EATesterControllerProps.PROPERTY_29_STARTSTOPBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEATesterController_PropertiesButton;
		public ButtonController PropertiesButton
		{
			get {
				return _IEATesterController_PropertiesButton;
			}
			set {
				if (!___initialized) {
					_IEATesterController_PropertiesButton= value;
					changed[EATesterControllerProps.PROPERTY_30_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEATesterController_AddLinkLabel;
		public ButtonController AddLinkLabel
		{
			get {
				return _IEATesterController_AddLinkLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_AddLinkLabel= value;
					changed[EATesterControllerProps.PROPERTY_31_ADDLINKLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEATesterController_PauseButton;
		public ButtonController PauseButton
		{
			get {
				return _IEATesterController_PauseButton;
			}
			set {
				if (!___initialized) {
					_IEATesterController_PauseButton= value;
					changed[EATesterControllerProps.PROPERTY_32_PAUSEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEATesterController_PauseAtButton;
		public ButtonController PauseAtButton
		{
			get {
				return _IEATesterController_PauseAtButton;
			}
			set {
				if (!___initialized) {
					_IEATesterController_PauseAtButton= value;
					changed[EATesterControllerProps.PROPERTY_33_PAUSEATBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IEATesterController_SnapButton;
		public ButtonController SnapButton
		{
			get {
				return _IEATesterController_SnapButton;
			}
			set {
				if (!___initialized) {
					_IEATesterController_SnapButton= value;
					changed[EATesterControllerProps.PROPERTY_34_SNAPBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_MethodInfLabel;
		public LabelledController MethodInfLabel
		{
			get {
				return _IEATesterController_MethodInfLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_MethodInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_35_METHODINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_EaInfLabel;
		public LabelledController EaInfLabel
		{
			get {
				return _IEATesterController_EaInfLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_EaInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_36_EAINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_SymbolInfLabel;
		public LabelledController SymbolInfLabel
		{
			get {
				return _IEATesterController_SymbolInfLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_SymbolInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_37_SYMBOLINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_FromInfLabel;
		public LabelledController FromInfLabel
		{
			get {
				return _IEATesterController_FromInfLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_FromInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_38_FROMINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_ToInfLabel;
		public LabelledController ToInfLabel
		{
			get {
				return _IEATesterController_ToInfLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_ToInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_39_TOINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_MethodLabel;
		public LabelledController MethodLabel
		{
			get {
				return _IEATesterController_MethodLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_MethodLabel= value;
					changed[EATesterControllerProps.PROPERTY_40_METHODLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IEATesterController_DataPeriodLabel;
		public LabelledController DataPeriodLabel
		{
			get {
				return _IEATesterController_DataPeriodLabel;
			}
			set {
				if (!___initialized) {
					_IEATesterController_DataPeriodLabel= value;
					changed[EATesterControllerProps.PROPERTY_41_DATAPERIODLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ChildControlMap<String> _IEATesterController_ToolTip1;
		public ChildControlMap<String> ToolTip1
		{
			get {
				return _IEATesterController_ToolTip1;
			}
			set {
				if (!___initialized) {
					_IEATesterController_ToolTip1= value;
					changed[EATesterControllerProps.PROPERTY_42_TOOLTIP1_ID] = true;
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
			if (EATesterControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EATesterControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
