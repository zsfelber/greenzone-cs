using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ChartControllerProps
	{
		public const int PROPERTY_15_MAINWINDOW_ID = 15;
		public const int PROPERTY_16_TABPANEL_ID = 16;
		public const int PROPERTY_17_SLIDERBARCOLOR_ID = 17;
		public const int PROPERTY_18_CHARTSECTIONPANELS_ID = 18;
		public const int PROPERTY_19_MASTERCHARTSECTIONPANEL_ID = 19;
		public const int PROPERTY_20_TABLELAYOUTPANEL1_ID = 20;
		public const int PROPERTY_21_TIMELABELPANE1_ID = 21;
		public const int PROPERTY_22_ZOOMOUTVBUTTON_ID = 22;
		public const int PROPERTY_23_ZOOMOUTHBUTTON_ID = 23;
		public const int PROPERTY_24_ZOOMINVBUTTON_ID = 24;
		public const int PROPERTY_25_ZOOMINHBUTTON_ID = 25;
		public const int PROPERTY_26_ZOOMTOFITBUTTON_ID = 26;
		public const int PROPERTY_27_ZOOMTOSCROLLPRICEBUTTON_ID = 27;
		public const int PROPERTY_28_OWNER_ID = 28;
		public const int PROPERTY_29_ENVIRONMENT_ID = 29;
		public static bool RmiGetProperty(IChartController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case PROPERTY_16_TABPANEL_ID:
					value = controller.TabPanel;
					return true;
				case PROPERTY_17_SLIDERBARCOLOR_ID:
					value = controller.SliderBarColor;
					return true;
				case PROPERTY_18_CHARTSECTIONPANELS_ID:
					value = controller.ChartSectionPanels;
					return true;
				case PROPERTY_19_MASTERCHARTSECTIONPANEL_ID:
					value = controller.MasterChartSectionPanel;
					return true;
				case PROPERTY_20_TABLELAYOUTPANEL1_ID:
					value = controller.TableLayoutPanel1;
					return true;
				case PROPERTY_21_TIMELABELPANE1_ID:
					value = controller.TimeLabelPane1;
					return true;
				case PROPERTY_22_ZOOMOUTVBUTTON_ID:
					value = controller.ZoomOutVButton;
					return true;
				case PROPERTY_23_ZOOMOUTHBUTTON_ID:
					value = controller.ZoomOutHButton;
					return true;
				case PROPERTY_24_ZOOMINVBUTTON_ID:
					value = controller.ZoomInVButton;
					return true;
				case PROPERTY_25_ZOOMINHBUTTON_ID:
					value = controller.ZoomInHButton;
					return true;
				case PROPERTY_26_ZOOMTOFITBUTTON_ID:
					value = controller.ZoomToFitButton;
					return true;
				case PROPERTY_27_ZOOMTOSCROLLPRICEBUTTON_ID:
					value = controller.ZoomToScrollPriceButton;
					return true;
				case PROPERTY_28_OWNER_ID:
					value = controller.Owner;
					return true;
				case PROPERTY_29_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_SLIDERBARCOLOR_ID:
					controller.SliderBarColor = (Color) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartController controller, GreenRmiObjectBuffer buffer)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[ChartControllerProps.PROPERTY_15_MAINWINDOW_ID];
			controller.TabPanel = (IMainWinTabPageController) buffer.ChangedProps[ChartControllerProps.PROPERTY_16_TABPANEL_ID];
			controller.ChartSectionPanels = (IList<IChartSectionPanelController>) buffer.ChangedProps[ChartControllerProps.PROPERTY_18_CHARTSECTIONPANELS_ID];
			controller.MasterChartSectionPanel = (IChartSectionPanelController) buffer.ChangedProps[ChartControllerProps.PROPERTY_19_MASTERCHARTSECTIONPANEL_ID];
			controller.TableLayoutPanel1 = (MultiSplitController) buffer.ChangedProps[ChartControllerProps.PROPERTY_20_TABLELAYOUTPANEL1_ID];
			controller.TimeLabelPane1 = (ITimeLabelPaneController) buffer.ChangedProps[ChartControllerProps.PROPERTY_21_TIMELABELPANE1_ID];
			controller.ZoomOutVButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_22_ZOOMOUTVBUTTON_ID];
			controller.ZoomOutHButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_23_ZOOMOUTHBUTTON_ID];
			controller.ZoomInVButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_24_ZOOMINVBUTTON_ID];
			controller.ZoomInHButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_25_ZOOMINHBUTTON_ID];
			controller.ZoomToFitButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_26_ZOOMTOFITBUTTON_ID];
			controller.ZoomToScrollPriceButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_27_ZOOMTOSCROLLPRICEBUTTON_ID];
			controller.Owner = (IChartOwner) buffer.ChangedProps[ChartControllerProps.PROPERTY_28_OWNER_ID];
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[ChartControllerProps.PROPERTY_29_ENVIRONMENT_ID];
		}

		public static void AddDependencies(IChartController controller)
		{
			controller.Dependencies.Add(controller.MainWindow);
			controller.Dependencies.Add(controller.TabPanel);
			controller.Dependencies.AddRange(controller.ChartSectionPanels);
			controller.Dependencies.Add(controller.MasterChartSectionPanel);
			controller.Dependencies.Add(controller.TableLayoutPanel1);
			controller.Dependencies.Add(controller.TimeLabelPane1);
			controller.Dependencies.Add(controller.ZoomOutVButton);
			controller.Dependencies.Add(controller.ZoomOutHButton);
			controller.Dependencies.Add(controller.ZoomInVButton);
			controller.Dependencies.Add(controller.ZoomInHButton);
			controller.Dependencies.Add(controller.ZoomToFitButton);
			controller.Dependencies.Add(controller.ZoomToScrollPriceButton);
			controller.Dependencies.Add(controller.Environment);
		}

		public static void SerializationRead(IChartController controller, SerializationInfo info, StreamingContext context)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
			controller.TabPanel = (IMainWinTabPageController) info.GetValue("TabPanel", typeof(IMainWinTabPageController));
			controller.SliderBarColor = (Color) info.GetValue("SliderBarColor", typeof(Color));
			controller.ChartSectionPanels = (IList<IChartSectionPanelController>) info.GetValue("ChartSectionPanels", typeof(IList<IChartSectionPanelController>));
			controller.MasterChartSectionPanel = (IChartSectionPanelController) info.GetValue("MasterChartSectionPanel", typeof(IChartSectionPanelController));
			controller.TableLayoutPanel1 = (MultiSplitController) info.GetValue("TableLayoutPanel1", typeof(MultiSplitController));
			controller.TimeLabelPane1 = (ITimeLabelPaneController) info.GetValue("TimeLabelPane1", typeof(ITimeLabelPaneController));
			controller.ZoomOutVButton = (ButtonController) info.GetValue("ZoomOutVButton", typeof(ButtonController));
			controller.ZoomOutHButton = (ButtonController) info.GetValue("ZoomOutHButton", typeof(ButtonController));
			controller.ZoomInVButton = (ButtonController) info.GetValue("ZoomInVButton", typeof(ButtonController));
			controller.ZoomInHButton = (ButtonController) info.GetValue("ZoomInHButton", typeof(ButtonController));
			controller.ZoomToFitButton = (ButtonController) info.GetValue("ZoomToFitButton", typeof(ButtonController));
			controller.ZoomToScrollPriceButton = (ButtonController) info.GetValue("ZoomToScrollPriceButton", typeof(ButtonController));
			controller.Owner = (IChartOwner) info.GetValue("Owner", typeof(IChartOwner));
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
		}

		public static void SerializationWrite(IChartController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("MainWindow", controller.MainWindow);
			info.AddValue("TabPanel", controller.TabPanel);
			info.AddValue("SliderBarColor", controller.SliderBarColor);
			info.AddValue("ChartSectionPanels", controller.ChartSectionPanels);
			info.AddValue("MasterChartSectionPanel", controller.MasterChartSectionPanel);
			info.AddValue("TableLayoutPanel1", controller.TableLayoutPanel1);
			info.AddValue("TimeLabelPane1", controller.TimeLabelPane1);
			info.AddValue("ZoomOutVButton", controller.ZoomOutVButton);
			info.AddValue("ZoomOutHButton", controller.ZoomOutHButton);
			info.AddValue("ZoomInVButton", controller.ZoomInVButton);
			info.AddValue("ZoomInHButton", controller.ZoomInHButton);
			info.AddValue("ZoomToFitButton", controller.ZoomToFitButton);
			info.AddValue("ZoomToScrollPriceButton", controller.ZoomToScrollPriceButton);
			info.AddValue("Owner", controller.Owner);
			info.AddValue("Environment", controller.Environment);
		}

	}
	public abstract class ChartControllerBase : ClientController, IChartController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SliderBarColorChanged;

		public ChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartControllerProps.AddDependencies(this);
		}

		public ChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartControllerProps.AddDependencies(this);
		}

		protected ChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartControllerProps.SerializationWrite(this, info, context);
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
					changed[ChartControllerProps.PROPERTY_15_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IMainWinTabPageController tabPanel;
		public IMainWinTabPageController TabPanel
		{
			get {
				return tabPanel;
			}
			set {
				if (!___initialized) {
					tabPanel= value;
					changed[ChartControllerProps.PROPERTY_16_TABPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Color sliderBarColor;
		public Color SliderBarColor
		{
			get {
				return sliderBarColor;
			}
			set {
				if (sliderBarColor != value) {
					sliderBarColor= value;
					changed[ChartControllerProps.PROPERTY_17_SLIDERBARCOLOR_ID] = true;
					if (SliderBarColorChanged != null)
						SliderBarColorChanged(this, new PropertyChangedEventArgs("SliderBarColor", value));
				}
			}
		}

		IList<IChartSectionPanelController> chartSectionPanels;
		public IList<IChartSectionPanelController> ChartSectionPanels
		{
			get {
				return chartSectionPanels;
			}
			set {
				if (!___initialized) {
					chartSectionPanels= value;
					changed[ChartControllerProps.PROPERTY_18_CHARTSECTIONPANELS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartSectionPanelController masterChartSectionPanel;
		public IChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return masterChartSectionPanel;
			}
			set {
				if (!___initialized) {
					masterChartSectionPanel= value;
					changed[ChartControllerProps.PROPERTY_19_MASTERCHARTSECTIONPANEL_ID] = true;
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
					changed[ChartControllerProps.PROPERTY_20_TABLELAYOUTPANEL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ITimeLabelPaneController timeLabelPane1;
		public ITimeLabelPaneController TimeLabelPane1
		{
			get {
				return timeLabelPane1;
			}
			set {
				if (!___initialized) {
					timeLabelPane1= value;
					changed[ChartControllerProps.PROPERTY_21_TIMELABELPANE1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController zoomOutVButton;
		public ButtonController ZoomOutVButton
		{
			get {
				return zoomOutVButton;
			}
			set {
				if (!___initialized) {
					zoomOutVButton= value;
					changed[ChartControllerProps.PROPERTY_22_ZOOMOUTVBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController zoomOutHButton;
		public ButtonController ZoomOutHButton
		{
			get {
				return zoomOutHButton;
			}
			set {
				if (!___initialized) {
					zoomOutHButton= value;
					changed[ChartControllerProps.PROPERTY_23_ZOOMOUTHBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController zoomInVButton;
		public ButtonController ZoomInVButton
		{
			get {
				return zoomInVButton;
			}
			set {
				if (!___initialized) {
					zoomInVButton= value;
					changed[ChartControllerProps.PROPERTY_24_ZOOMINVBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController zoomInHButton;
		public ButtonController ZoomInHButton
		{
			get {
				return zoomInHButton;
			}
			set {
				if (!___initialized) {
					zoomInHButton= value;
					changed[ChartControllerProps.PROPERTY_25_ZOOMINHBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController zoomToFitButton;
		public ButtonController ZoomToFitButton
		{
			get {
				return zoomToFitButton;
			}
			set {
				if (!___initialized) {
					zoomToFitButton= value;
					changed[ChartControllerProps.PROPERTY_26_ZOOMTOFITBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController zoomToScrollPriceButton;
		public ButtonController ZoomToScrollPriceButton
		{
			get {
				return zoomToScrollPriceButton;
			}
			set {
				if (!___initialized) {
					zoomToScrollPriceButton= value;
					changed[ChartControllerProps.PROPERTY_27_ZOOMTOSCROLLPRICEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartOwner owner;
		public IChartOwner Owner
		{
			get {
				return owner;
			}
			set {
				if (!___initialized) {
					owner= value;
					changed[ChartControllerProps.PROPERTY_28_OWNER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IEnvironmentRuntime environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return environment;
			}
			set {
				if (!___initialized) {
					environment= value;
					changed[ChartControllerProps.PROPERTY_29_ENVIRONMENT_ID] = true;
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
			if (ChartControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
