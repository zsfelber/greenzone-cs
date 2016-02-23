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
		public static bool RmiGetProperty(IEATesterController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case PROPERTY_17_DATAGRIDVIEW1_ID:
					value = controller.DataGridView1;
					return true;
				case PROPERTY_18_SELECTEACOLUMN_ID:
					value = controller.SelectEAColumn;
					return true;
				case PROPERTY_19_TABLETESTEASEACOL_ID:
					value = controller.TableTestEasEACol;
					return true;
				case PROPERTY_20_TABLETESTEASPERIODCOL_ID:
					value = controller.TableTestEasPeriodCol;
					return true;
				case PROPERTY_21_ICONCOLUMN_ID:
					value = controller.IconColumn;
					return true;
				case PROPERTY_22_METHODCOMBO_ID:
					value = controller.MethodCombo;
					return true;
				case PROPERTY_23_DATAPERIODCOMBO_ID:
					value = controller.DataPeriodCombo;
					return true;
				case PROPERTY_24_SCROLLACROSSTABSCB_ID:
					value = controller.ScrollAcrossTabsCb;
					return true;
				case PROPERTY_25_SKIPEMPTYPERIODSCB_ID:
					value = controller.SkipEmptyPeriodsCb;
					return true;
				case PROPERTY_26_UPDATESPREADTICKCB_ID:
					value = controller.UpdateSpreadTickCb;
					return true;
				case PROPERTY_27_SPEEDTRACKBAR_ID:
					value = controller.SpeedTrackBar;
					return true;
				case PROPERTY_28_PROGRESSTRACKBAR1_ID:
					value = controller.ProgressTrackBar1;
					return true;
				case PROPERTY_29_STARTSTOPBUTTON_ID:
					value = controller.StartStopButton;
					return true;
				case PROPERTY_30_PROPERTIESBUTTON_ID:
					value = controller.PropertiesButton;
					return true;
				case PROPERTY_31_ADDLINKLABEL_ID:
					value = controller.AddLinkLabel;
					return true;
				case PROPERTY_32_PAUSEBUTTON_ID:
					value = controller.PauseButton;
					return true;
				case PROPERTY_33_PAUSEATBUTTON_ID:
					value = controller.PauseAtButton;
					return true;
				case PROPERTY_34_SNAPBUTTON_ID:
					value = controller.SnapButton;
					return true;
				case PROPERTY_35_METHODINFLABEL_ID:
					value = controller.MethodInfLabel;
					return true;
				case PROPERTY_36_EAINFLABEL_ID:
					value = controller.EaInfLabel;
					return true;
				case PROPERTY_37_SYMBOLINFLABEL_ID:
					value = controller.SymbolInfLabel;
					return true;
				case PROPERTY_38_FROMINFLABEL_ID:
					value = controller.FromInfLabel;
					return true;
				case PROPERTY_39_TOINFLABEL_ID:
					value = controller.ToInfLabel;
					return true;
				case PROPERTY_40_METHODLABEL_ID:
					value = controller.MethodLabel;
					return true;
				case PROPERTY_41_DATAPERIODLABEL_ID:
					value = controller.DataPeriodLabel;
					return true;
				case PROPERTY_42_TOOLTIP1_ID:
					value = controller.ToolTip1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEATesterController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IEATesterController controller, GreenRmiObjectBuffer buffer)
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

		public static void AddDependencies(IEATesterController controller)
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

		public static void SerializationRead(IEATesterController controller, SerializationInfo info, StreamingContext context)
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

		public static void SerializationWrite(IEATesterController controller, SerializationInfo info, StreamingContext context)
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
			EATesterControllerProps.AddDependencies(this);
		}

		public EATesterControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			EATesterControllerProps.AddDependencies(this);
		}

		public EATesterControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			EATesterControllerProps.AddDependencies(this);
		}

		public EATesterControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EATesterControllerProps.Initialize(this, buffer);
			___initialized = true;
			EATesterControllerProps.AddDependencies(this);
		}

		protected EATesterControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EATesterControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			EATesterControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EATesterControllerProps.SerializationWrite(this, info, context);
		}

		IMainWindowController mainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return mainWindow;
			}
			set {
				if (!___initialized) {
					mainWindow= value;
					changed[EATesterControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridController dataGridView1;
		public GridController DataGridView1
		{
			get {
				return dataGridView1;
			}
			set {
				if (!___initialized) {
					dataGridView1= value;
					changed[EATesterControllerProps.PROPERTY_17_DATAGRIDVIEW1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController selectEAColumn;
		public GridColumnController SelectEAColumn
		{
			get {
				return selectEAColumn;
			}
			set {
				if (!___initialized) {
					selectEAColumn= value;
					changed[EATesterControllerProps.PROPERTY_18_SELECTEACOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController tableTestEasEACol;
		public GridColumnController TableTestEasEACol
		{
			get {
				return tableTestEasEACol;
			}
			set {
				if (!___initialized) {
					tableTestEasEACol= value;
					changed[EATesterControllerProps.PROPERTY_19_TABLETESTEASEACOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController tableTestEasPeriodCol;
		public GridColumnController TableTestEasPeriodCol
		{
			get {
				return tableTestEasPeriodCol;
			}
			set {
				if (!___initialized) {
					tableTestEasPeriodCol= value;
					changed[EATesterControllerProps.PROPERTY_20_TABLETESTEASPERIODCOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController iconColumn;
		public GridColumnController IconColumn
		{
			get {
				return iconColumn;
			}
			set {
				if (!___initialized) {
					iconColumn= value;
					changed[EATesterControllerProps.PROPERTY_21_ICONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController methodCombo;
		public ComboController MethodCombo
		{
			get {
				return methodCombo;
			}
			set {
				if (!___initialized) {
					methodCombo= value;
					changed[EATesterControllerProps.PROPERTY_22_METHODCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController dataPeriodCombo;
		public ComboController DataPeriodCombo
		{
			get {
				return dataPeriodCombo;
			}
			set {
				if (!___initialized) {
					dataPeriodCombo= value;
					changed[EATesterControllerProps.PROPERTY_23_DATAPERIODCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController scrollAcrossTabsCb;
		public ToggleButtonController ScrollAcrossTabsCb
		{
			get {
				return scrollAcrossTabsCb;
			}
			set {
				if (!___initialized) {
					scrollAcrossTabsCb= value;
					changed[EATesterControllerProps.PROPERTY_24_SCROLLACROSSTABSCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController skipEmptyPeriodsCb;
		public ToggleButtonController SkipEmptyPeriodsCb
		{
			get {
				return skipEmptyPeriodsCb;
			}
			set {
				if (!___initialized) {
					skipEmptyPeriodsCb= value;
					changed[EATesterControllerProps.PROPERTY_25_SKIPEMPTYPERIODSCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController updateSpreadTickCb;
		public ToggleButtonController UpdateSpreadTickCb
		{
			get {
				return updateSpreadTickCb;
			}
			set {
				if (!___initialized) {
					updateSpreadTickCb= value;
					changed[EATesterControllerProps.PROPERTY_26_UPDATESPREADTICKCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController speedTrackBar;
		public ProgressTrackController SpeedTrackBar
		{
			get {
				return speedTrackBar;
			}
			set {
				if (!___initialized) {
					speedTrackBar= value;
					changed[EATesterControllerProps.PROPERTY_27_SPEEDTRACKBAR_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController progressTrackBar1;
		public ProgressTrackController ProgressTrackBar1
		{
			get {
				return progressTrackBar1;
			}
			set {
				if (!___initialized) {
					progressTrackBar1= value;
					changed[EATesterControllerProps.PROPERTY_28_PROGRESSTRACKBAR1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController startStopButton;
		public ButtonController StartStopButton
		{
			get {
				return startStopButton;
			}
			set {
				if (!___initialized) {
					startStopButton= value;
					changed[EATesterControllerProps.PROPERTY_29_STARTSTOPBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController propertiesButton;
		public ButtonController PropertiesButton
		{
			get {
				return propertiesButton;
			}
			set {
				if (!___initialized) {
					propertiesButton= value;
					changed[EATesterControllerProps.PROPERTY_30_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController addLinkLabel;
		public ButtonController AddLinkLabel
		{
			get {
				return addLinkLabel;
			}
			set {
				if (!___initialized) {
					addLinkLabel= value;
					changed[EATesterControllerProps.PROPERTY_31_ADDLINKLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController pauseButton;
		public ButtonController PauseButton
		{
			get {
				return pauseButton;
			}
			set {
				if (!___initialized) {
					pauseButton= value;
					changed[EATesterControllerProps.PROPERTY_32_PAUSEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController pauseAtButton;
		public ButtonController PauseAtButton
		{
			get {
				return pauseAtButton;
			}
			set {
				if (!___initialized) {
					pauseAtButton= value;
					changed[EATesterControllerProps.PROPERTY_33_PAUSEATBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController snapButton;
		public ButtonController SnapButton
		{
			get {
				return snapButton;
			}
			set {
				if (!___initialized) {
					snapButton= value;
					changed[EATesterControllerProps.PROPERTY_34_SNAPBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController methodInfLabel;
		public LabelledController MethodInfLabel
		{
			get {
				return methodInfLabel;
			}
			set {
				if (!___initialized) {
					methodInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_35_METHODINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController eaInfLabel;
		public LabelledController EaInfLabel
		{
			get {
				return eaInfLabel;
			}
			set {
				if (!___initialized) {
					eaInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_36_EAINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController symbolInfLabel;
		public LabelledController SymbolInfLabel
		{
			get {
				return symbolInfLabel;
			}
			set {
				if (!___initialized) {
					symbolInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_37_SYMBOLINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController fromInfLabel;
		public LabelledController FromInfLabel
		{
			get {
				return fromInfLabel;
			}
			set {
				if (!___initialized) {
					fromInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_38_FROMINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController toInfLabel;
		public LabelledController ToInfLabel
		{
			get {
				return toInfLabel;
			}
			set {
				if (!___initialized) {
					toInfLabel= value;
					changed[EATesterControllerProps.PROPERTY_39_TOINFLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController methodLabel;
		public LabelledController MethodLabel
		{
			get {
				return methodLabel;
			}
			set {
				if (!___initialized) {
					methodLabel= value;
					changed[EATesterControllerProps.PROPERTY_40_METHODLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController dataPeriodLabel;
		public LabelledController DataPeriodLabel
		{
			get {
				return dataPeriodLabel;
			}
			set {
				if (!___initialized) {
					dataPeriodLabel= value;
					changed[EATesterControllerProps.PROPERTY_41_DATAPERIODLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ChildControlMap<String> toolTip1;
		public ChildControlMap<String> ToolTip1
		{
			get {
				return toolTip1;
			}
			set {
				if (!___initialized) {
					toolTip1= value;
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
			if (EATesterControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!EATesterControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
