using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class ChartViewControllerProps
	{
		public const int PROPERTY_16_MAINWINDOW_ID = 16;
		public const int PROPERTY_17_CHARTGROUPCONTROLLER_ID = 17;
		public const int PROPERTY_18_CHART1_ID = 18;
		public const int PROPERTY_19_CURSORCHART_ID = 19;
		public const int PROPERTY_20_FROMTIMEPICKER_ID = 20;
		public const int PROPERTY_21_TOTIMEPICKER_ID = 21;
		public const int PROPERTY_22_CLOSECHARTBUTTON1_ID = 22;
		public const int PROPERTY_23_CLOSECHARTBUTTON2_ID = 23;
		public const int PROPERTY_24_CONNECTCURSORBUTTON_ID = 24;
		public const int PROPERTY_25_AUTOSERIESRANGEBUTTON_ID = 25;
		public const int PROPERTY_26_TOGGLETOPBARBUTTON1_ID = 26;
		public const int PROPERTY_27_TOGGLETOPBARBUTTON2_ID = 27;
		public const int PROPERTY_28_ADDCHARTBUTTON_ID = 28;
		public const int PROPERTY_29_SYMBOLDDBUTTON_ID = 29;
		public const int PROPERTY_30_PERIODDDBUTTON_ID = 30;
		public const int PROPERTY_31_CHARTTYPEDDBUTTON_ID = 31;
		public const int PROPERTY_32_INDICATORSDDBUTTON_ID = 32;
		public const int PROPERTY_33_SYMPERMINILABEL_ID = 33;
		public const int PROPERTY_34_TOPTOOLSTRIP_ID = 34;
		public static bool RmiGetProperty(IChartViewController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case PROPERTY_17_CHARTGROUPCONTROLLER_ID:
					value = controller.ChartGroupController;
					return true;
				case PROPERTY_18_CHART1_ID:
					value = controller.Chart1;
					return true;
				case PROPERTY_19_CURSORCHART_ID:
					value = controller.CursorChart;
					return true;
				case PROPERTY_20_FROMTIMEPICKER_ID:
					value = controller.FromTimePicker;
					return true;
				case PROPERTY_21_TOTIMEPICKER_ID:
					value = controller.ToTimePicker;
					return true;
				case PROPERTY_22_CLOSECHARTBUTTON1_ID:
					value = controller.CloseChartButton1;
					return true;
				case PROPERTY_23_CLOSECHARTBUTTON2_ID:
					value = controller.CloseChartButton2;
					return true;
				case PROPERTY_24_CONNECTCURSORBUTTON_ID:
					value = controller.ConnectCursorButton;
					return true;
				case PROPERTY_25_AUTOSERIESRANGEBUTTON_ID:
					value = controller.AutoSeriesRangeButton;
					return true;
				case PROPERTY_26_TOGGLETOPBARBUTTON1_ID:
					value = controller.ToggleTopBarButton1;
					return true;
				case PROPERTY_27_TOGGLETOPBARBUTTON2_ID:
					value = controller.ToggleTopBarButton2;
					return true;
				case PROPERTY_28_ADDCHARTBUTTON_ID:
					value = controller.AddChartButton;
					return true;
				case PROPERTY_29_SYMBOLDDBUTTON_ID:
					value = controller.SymbolDdButton;
					return true;
				case PROPERTY_30_PERIODDDBUTTON_ID:
					value = controller.PeriodDdButton;
					return true;
				case PROPERTY_31_CHARTTYPEDDBUTTON_ID:
					value = controller.ChartTypeDdButton;
					return true;
				case PROPERTY_32_INDICATORSDDBUTTON_ID:
					value = controller.IndicatorsDdButton;
					return true;
				case PROPERTY_33_SYMPERMINILABEL_ID:
					value = controller.SymPerMiniLabel;
					return true;
				case PROPERTY_34_TOPTOOLSTRIP_ID:
					value = controller.TopToolStrip;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartViewController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartViewController controller, GreenRmiObjectBuffer buffer)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_16_MAINWINDOW_ID];
			controller.ChartGroupController = (IChartGroupController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_17_CHARTGROUPCONTROLLER_ID];
			controller.Chart1 = (INormalChartController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_18_CHART1_ID];
			controller.CursorChart = (ICursorChartController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_19_CURSORCHART_ID];
			controller.FromTimePicker = (FieldController<DateTime>) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_20_FROMTIMEPICKER_ID];
			controller.ToTimePicker = (FieldController<DateTime>) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_21_TOTIMEPICKER_ID];
			controller.CloseChartButton1 = (ButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_22_CLOSECHARTBUTTON1_ID];
			controller.CloseChartButton2 = (ButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_23_CLOSECHARTBUTTON2_ID];
			controller.ConnectCursorButton = (ToggleButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_24_CONNECTCURSORBUTTON_ID];
			controller.AutoSeriesRangeButton = (ToggleButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_25_AUTOSERIESRANGEBUTTON_ID];
			controller.ToggleTopBarButton1 = (ButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_26_TOGGLETOPBARBUTTON1_ID];
			controller.ToggleTopBarButton2 = (ButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_27_TOGGLETOPBARBUTTON2_ID];
			controller.AddChartButton = (ComboController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_28_ADDCHARTBUTTON_ID];
			controller.SymbolDdButton = (ComboController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_29_SYMBOLDDBUTTON_ID];
			controller.PeriodDdButton = (ComboController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_30_PERIODDDBUTTON_ID];
			controller.ChartTypeDdButton = (ComboController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_31_CHARTTYPEDDBUTTON_ID];
			controller.IndicatorsDdButton = (ComboController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_32_INDICATORSDDBUTTON_ID];
			controller.SymPerMiniLabel = (ButtonController) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_33_SYMPERMINILABEL_ID];
			controller.TopToolStrip = (Controller) buffer.ChangedProps[ChartViewControllerProps.PROPERTY_34_TOPTOOLSTRIP_ID];
		}

		public static void AddDependencies(IChartViewController controller)
		{
			controller.Dependencies.Add(controller.MainWindow);
			controller.Dependencies.Add(controller.ChartGroupController);
			controller.Dependencies.Add(controller.Chart1);
			controller.Dependencies.Add(controller.CursorChart);
			controller.Dependencies.Add(controller.FromTimePicker);
			controller.Dependencies.Add(controller.ToTimePicker);
			controller.Dependencies.Add(controller.CloseChartButton1);
			controller.Dependencies.Add(controller.CloseChartButton2);
			controller.Dependencies.Add(controller.ConnectCursorButton);
			controller.Dependencies.Add(controller.AutoSeriesRangeButton);
			controller.Dependencies.Add(controller.ToggleTopBarButton1);
			controller.Dependencies.Add(controller.ToggleTopBarButton2);
			controller.Dependencies.Add(controller.AddChartButton);
			controller.Dependencies.Add(controller.SymbolDdButton);
			controller.Dependencies.Add(controller.PeriodDdButton);
			controller.Dependencies.Add(controller.ChartTypeDdButton);
			controller.Dependencies.Add(controller.IndicatorsDdButton);
			controller.Dependencies.Add(controller.SymPerMiniLabel);
			controller.Dependencies.Add(controller.TopToolStrip);
		}

		public static void SerializationRead(IChartViewController controller, SerializationInfo info, StreamingContext context)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
			controller.ChartGroupController = (IChartGroupController) info.GetValue("ChartGroupController", typeof(IChartGroupController));
			controller.Chart1 = (INormalChartController) info.GetValue("Chart1", typeof(INormalChartController));
			controller.CursorChart = (ICursorChartController) info.GetValue("CursorChart", typeof(ICursorChartController));
			controller.FromTimePicker = (FieldController<DateTime>) info.GetValue("FromTimePicker", typeof(FieldController<DateTime>));
			controller.ToTimePicker = (FieldController<DateTime>) info.GetValue("ToTimePicker", typeof(FieldController<DateTime>));
			controller.CloseChartButton1 = (ButtonController) info.GetValue("CloseChartButton1", typeof(ButtonController));
			controller.CloseChartButton2 = (ButtonController) info.GetValue("CloseChartButton2", typeof(ButtonController));
			controller.ConnectCursorButton = (ToggleButtonController) info.GetValue("ConnectCursorButton", typeof(ToggleButtonController));
			controller.AutoSeriesRangeButton = (ToggleButtonController) info.GetValue("AutoSeriesRangeButton", typeof(ToggleButtonController));
			controller.ToggleTopBarButton1 = (ButtonController) info.GetValue("ToggleTopBarButton1", typeof(ButtonController));
			controller.ToggleTopBarButton2 = (ButtonController) info.GetValue("ToggleTopBarButton2", typeof(ButtonController));
			controller.AddChartButton = (ComboController) info.GetValue("AddChartButton", typeof(ComboController));
			controller.SymbolDdButton = (ComboController) info.GetValue("SymbolDdButton", typeof(ComboController));
			controller.PeriodDdButton = (ComboController) info.GetValue("PeriodDdButton", typeof(ComboController));
			controller.ChartTypeDdButton = (ComboController) info.GetValue("ChartTypeDdButton", typeof(ComboController));
			controller.IndicatorsDdButton = (ComboController) info.GetValue("IndicatorsDdButton", typeof(ComboController));
			controller.SymPerMiniLabel = (ButtonController) info.GetValue("SymPerMiniLabel", typeof(ButtonController));
			controller.TopToolStrip = (Controller) info.GetValue("TopToolStrip", typeof(Controller));
		}

		public static void SerializationWrite(IChartViewController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("MainWindow", controller.MainWindow);
			info.AddValue("ChartGroupController", controller.ChartGroupController);
			info.AddValue("Chart1", controller.Chart1);
			info.AddValue("CursorChart", controller.CursorChart);
			info.AddValue("FromTimePicker", controller.FromTimePicker);
			info.AddValue("ToTimePicker", controller.ToTimePicker);
			info.AddValue("CloseChartButton1", controller.CloseChartButton1);
			info.AddValue("CloseChartButton2", controller.CloseChartButton2);
			info.AddValue("ConnectCursorButton", controller.ConnectCursorButton);
			info.AddValue("AutoSeriesRangeButton", controller.AutoSeriesRangeButton);
			info.AddValue("ToggleTopBarButton1", controller.ToggleTopBarButton1);
			info.AddValue("ToggleTopBarButton2", controller.ToggleTopBarButton2);
			info.AddValue("AddChartButton", controller.AddChartButton);
			info.AddValue("SymbolDdButton", controller.SymbolDdButton);
			info.AddValue("PeriodDdButton", controller.PeriodDdButton);
			info.AddValue("ChartTypeDdButton", controller.ChartTypeDdButton);
			info.AddValue("IndicatorsDdButton", controller.IndicatorsDdButton);
			info.AddValue("SymPerMiniLabel", controller.SymPerMiniLabel);
			info.AddValue("TopToolStrip", controller.TopToolStrip);
		}

	}
	public abstract class ChartViewControllerBase : LabelledController, IChartViewController
	{

		bool ___initialized = false;


		public ChartViewControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartViewControllerProps.AddDependencies(this);
		}

		public ChartViewControllerBase(GreenRmiManager rmiManager, Controller parent, String text)
			: base(rmiManager, parent, text)
		{
			___initialized = true;
			ChartViewControllerProps.AddDependencies(this);
		}

		public ChartViewControllerBase(GreenRmiManager rmiManager, Controller parent, String text, Int32 image)
			: base(rmiManager, parent, text, image)
		{
			___initialized = true;
			ChartViewControllerProps.AddDependencies(this);
		}

		public ChartViewControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartViewControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartViewControllerProps.AddDependencies(this);
		}

		protected ChartViewControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartViewControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartViewControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartViewControllerProps.SerializationWrite(this, info, context);
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
					changed[ChartViewControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public new IChartGroupController Parent
		{
			get {
				return (IChartGroupController) base.Parent;
			}
		}

		IChartGroupController chartGroupController;
		public IChartGroupController ChartGroupController
		{
			get {
				return chartGroupController;
			}
			set {
				if (!___initialized) {
					chartGroupController= value;
					changed[ChartViewControllerProps.PROPERTY_17_CHARTGROUPCONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		INormalChartController chart1;
		public INormalChartController Chart1
		{
			get {
				return chart1;
			}
			set {
				if (!___initialized) {
					chart1= value;
					changed[ChartViewControllerProps.PROPERTY_18_CHART1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ICursorChartController cursorChart;
		public ICursorChartController CursorChart
		{
			get {
				return cursorChart;
			}
			set {
				if (!___initialized) {
					cursorChart= value;
					changed[ChartViewControllerProps.PROPERTY_19_CURSORCHART_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> fromTimePicker;
		public FieldController<DateTime> FromTimePicker
		{
			get {
				return fromTimePicker;
			}
			set {
				if (!___initialized) {
					fromTimePicker= value;
					changed[ChartViewControllerProps.PROPERTY_20_FROMTIMEPICKER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> toTimePicker;
		public FieldController<DateTime> ToTimePicker
		{
			get {
				return toTimePicker;
			}
			set {
				if (!___initialized) {
					toTimePicker= value;
					changed[ChartViewControllerProps.PROPERTY_21_TOTIMEPICKER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController closeChartButton1;
		public ButtonController CloseChartButton1
		{
			get {
				return closeChartButton1;
			}
			set {
				if (!___initialized) {
					closeChartButton1= value;
					changed[ChartViewControllerProps.PROPERTY_22_CLOSECHARTBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController closeChartButton2;
		public ButtonController CloseChartButton2
		{
			get {
				return closeChartButton2;
			}
			set {
				if (!___initialized) {
					closeChartButton2= value;
					changed[ChartViewControllerProps.PROPERTY_23_CLOSECHARTBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController connectCursorButton;
		public ToggleButtonController ConnectCursorButton
		{
			get {
				return connectCursorButton;
			}
			set {
				if (!___initialized) {
					connectCursorButton= value;
					changed[ChartViewControllerProps.PROPERTY_24_CONNECTCURSORBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController autoSeriesRangeButton;
		public ToggleButtonController AutoSeriesRangeButton
		{
			get {
				return autoSeriesRangeButton;
			}
			set {
				if (!___initialized) {
					autoSeriesRangeButton= value;
					changed[ChartViewControllerProps.PROPERTY_25_AUTOSERIESRANGEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toggleTopBarButton1;
		public ButtonController ToggleTopBarButton1
		{
			get {
				return toggleTopBarButton1;
			}
			set {
				if (!___initialized) {
					toggleTopBarButton1= value;
					changed[ChartViewControllerProps.PROPERTY_26_TOGGLETOPBARBUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController toggleTopBarButton2;
		public ButtonController ToggleTopBarButton2
		{
			get {
				return toggleTopBarButton2;
			}
			set {
				if (!___initialized) {
					toggleTopBarButton2= value;
					changed[ChartViewControllerProps.PROPERTY_27_TOGGLETOPBARBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController addChartButton;
		public ComboController AddChartButton
		{
			get {
				return addChartButton;
			}
			set {
				if (!___initialized) {
					addChartButton= value;
					changed[ChartViewControllerProps.PROPERTY_28_ADDCHARTBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController symbolDdButton;
		public ComboController SymbolDdButton
		{
			get {
				return symbolDdButton;
			}
			set {
				if (!___initialized) {
					symbolDdButton= value;
					changed[ChartViewControllerProps.PROPERTY_29_SYMBOLDDBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController periodDdButton;
		public ComboController PeriodDdButton
		{
			get {
				return periodDdButton;
			}
			set {
				if (!___initialized) {
					periodDdButton= value;
					changed[ChartViewControllerProps.PROPERTY_30_PERIODDDBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController chartTypeDdButton;
		public ComboController ChartTypeDdButton
		{
			get {
				return chartTypeDdButton;
			}
			set {
				if (!___initialized) {
					chartTypeDdButton= value;
					changed[ChartViewControllerProps.PROPERTY_31_CHARTTYPEDDBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController indicatorsDdButton;
		public ComboController IndicatorsDdButton
		{
			get {
				return indicatorsDdButton;
			}
			set {
				if (!___initialized) {
					indicatorsDdButton= value;
					changed[ChartViewControllerProps.PROPERTY_32_INDICATORSDDBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController symPerMiniLabel;
		public ButtonController SymPerMiniLabel
		{
			get {
				return symPerMiniLabel;
			}
			set {
				if (!___initialized) {
					symPerMiniLabel= value;
					changed[ChartViewControllerProps.PROPERTY_33_SYMPERMINILABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Controller topToolStrip;
		public Controller TopToolStrip
		{
			get {
				return topToolStrip;
			}
			set {
				if (!___initialized) {
					topToolStrip= value;
					changed[ChartViewControllerProps.PROPERTY_34_TOPTOOLSTRIP_ID] = true;
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
			if (ChartViewControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartViewControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
