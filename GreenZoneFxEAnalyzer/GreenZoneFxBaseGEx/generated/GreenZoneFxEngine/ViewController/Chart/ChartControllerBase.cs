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
		public const int PROPERTY_15_OWNER_ID = 15;
		public const int PROPERTY_16_MAINWINDOW_ID = 16;
		public const int PROPERTY_17_TABPANEL_ID = 17;
		public const int PROPERTY_18_SLIDERBARCOLOR_ID = 18;
		public const int PROPERTY_19_CHARTSECTIONPANELS_ID = 19;
		public const int PROPERTY_20_MASTERCHARTSECTIONPANEL_ID = 20;
		public const int PROPERTY_21_TABLELAYOUTPANEL1_ID = 21;
		public const int PROPERTY_22_TIMELABELPANE1_ID = 22;
		public const int PROPERTY_23_ZOOMOUTVBUTTON_ID = 23;
		public const int PROPERTY_24_ZOOMOUTHBUTTON_ID = 24;
		public const int PROPERTY_25_ZOOMINVBUTTON_ID = 25;
		public const int PROPERTY_26_ZOOMINHBUTTON_ID = 26;
		public const int PROPERTY_27_ZOOMTOFITBUTTON_ID = 27;
		public const int PROPERTY_28_ZOOMTOSCROLLPRICEBUTTON_ID = 28;
		public static bool RmiGetProperty(IChartController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartControllerProps.PROPERTY_15_OWNER_ID:
					value = controller.Owner;
					return true;
				case ChartControllerProps.PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case ChartControllerProps.PROPERTY_17_TABPANEL_ID:
					value = controller.TabPanel;
					return true;
				case ChartControllerProps.PROPERTY_18_SLIDERBARCOLOR_ID:
					value = controller.SliderBarColor;
					return true;
				case ChartControllerProps.PROPERTY_19_CHARTSECTIONPANELS_ID:
					value = controller.ChartSectionPanels;
					return true;
				case ChartControllerProps.PROPERTY_20_MASTERCHARTSECTIONPANEL_ID:
					value = controller.MasterChartSectionPanel;
					return true;
				case ChartControllerProps.PROPERTY_21_TABLELAYOUTPANEL1_ID:
					value = controller.TableLayoutPanel1;
					return true;
				case ChartControllerProps.PROPERTY_22_TIMELABELPANE1_ID:
					value = controller.TimeLabelPane1;
					return true;
				case ChartControllerProps.PROPERTY_23_ZOOMOUTVBUTTON_ID:
					value = controller.ZoomOutVButton;
					return true;
				case ChartControllerProps.PROPERTY_24_ZOOMOUTHBUTTON_ID:
					value = controller.ZoomOutHButton;
					return true;
				case ChartControllerProps.PROPERTY_25_ZOOMINVBUTTON_ID:
					value = controller.ZoomInVButton;
					return true;
				case ChartControllerProps.PROPERTY_26_ZOOMINHBUTTON_ID:
					value = controller.ZoomInHButton;
					return true;
				case ChartControllerProps.PROPERTY_27_ZOOMTOFITBUTTON_ID:
					value = controller.ZoomToFitButton;
					return true;
				case ChartControllerProps.PROPERTY_28_ZOOMTOSCROLLPRICEBUTTON_ID:
					value = controller.ZoomToScrollPriceButton;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartControllerProps.PROPERTY_18_SLIDERBARCOLOR_ID:
					controller.SliderBarColor = (Color) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Owner = (IServerChartOwner) buffer.ChangedProps[ChartControllerProps.PROPERTY_15_OWNER_ID];
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[ChartControllerProps.PROPERTY_16_MAINWINDOW_ID];
			controller.TabPanel = (IMainWinTabPageController) buffer.ChangedProps[ChartControllerProps.PROPERTY_17_TABPANEL_ID];
			controller.ChartSectionPanels = (IList<IChartSectionPanelController>) buffer.ChangedProps[ChartControllerProps.PROPERTY_19_CHARTSECTIONPANELS_ID];
			controller.MasterChartSectionPanel = (IChartSectionPanelController) buffer.ChangedProps[ChartControllerProps.PROPERTY_20_MASTERCHARTSECTIONPANEL_ID];
			controller.TableLayoutPanel1 = (MultiSplitController) buffer.ChangedProps[ChartControllerProps.PROPERTY_21_TABLELAYOUTPANEL1_ID];
			controller.TimeLabelPane1 = (ITimeLabelPaneController) buffer.ChangedProps[ChartControllerProps.PROPERTY_22_TIMELABELPANE1_ID];
			controller.ZoomOutVButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_23_ZOOMOUTVBUTTON_ID];
			controller.ZoomOutHButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_24_ZOOMOUTHBUTTON_ID];
			controller.ZoomInVButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_25_ZOOMINVBUTTON_ID];
			controller.ZoomInHButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_26_ZOOMINHBUTTON_ID];
			controller.ZoomToFitButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_27_ZOOMTOFITBUTTON_ID];
			controller.ZoomToScrollPriceButton = (ButtonController) buffer.ChangedProps[ChartControllerProps.PROPERTY_28_ZOOMTOSCROLLPRICEBUTTON_ID];
		}

		public static void AddDependencies(IChartController controller, bool goToParent)
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
		}

		public static void SerializationRead(IChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Owner = (IServerChartOwner) info.GetValue("Owner", typeof(IServerChartOwner));
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
		}

		public static void SerializationWrite(IChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Owner", controller.Owner);
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
		}

	}
	public abstract class ChartControllerBase : ClientController, IChartController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IChartController_SliderBarColor_Changed;

		public ChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartControllerProps.AddDependencies(this, false);
		}

		public ChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartControllerProps.AddDependencies(this, false);
		}

		protected ChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartControllerProps.SerializationWrite(this, info, context, false);
		}


		IServerChartOwner _IChartController_Owner;
		public IServerChartOwner Owner
		{
			get {
				return _IChartController_Owner;
			}
			set {
				if (!___initialized) {
					_IChartController_Owner= value;
					changed[ChartControllerProps.PROPERTY_15_OWNER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual SeriesRange SeriesRange
		{
			get {
				return Owner.SeriesRange;
			}
			set {
				Owner.SeriesRange = value;
			}
		}

		IMainWindowController _IChartController_MainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return _IChartController_MainWindow;
			}
			set {
				if (!___initialized) {
					_IChartController_MainWindow= value;
					changed[ChartControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IMainWinTabPageController _IChartController_TabPanel;
		public IMainWinTabPageController TabPanel
		{
			get {
				return _IChartController_TabPanel;
			}
			set {
				if (!___initialized) {
					_IChartController_TabPanel= value;
					changed[ChartControllerProps.PROPERTY_17_TABPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Color _IChartController_SliderBarColor;
		public virtual Color SliderBarColor
		{
			get {
				return _IChartController_SliderBarColor;
			}
			set {
				if (_IChartController_SliderBarColor != value) {
					_IChartController_SliderBarColor= value;
					changed[ChartControllerProps.PROPERTY_18_SLIDERBARCOLOR_ID] = true;
					if (IChartController_SliderBarColor_Changed != null)
						IChartController_SliderBarColor_Changed(this, new PropertyChangedEventArgs("SliderBarColor", value));
				}
			}
		}

		IList<IChartSectionPanelController> _IChartController_ChartSectionPanels;
		public IList<IChartSectionPanelController> ChartSectionPanels
		{
			get {
				return _IChartController_ChartSectionPanels;
			}
			set {
				if (!___initialized) {
					_IChartController_ChartSectionPanels= value;
					changed[ChartControllerProps.PROPERTY_19_CHARTSECTIONPANELS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartSectionPanelController _IChartController_MasterChartSectionPanel;
		public IChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return _IChartController_MasterChartSectionPanel;
			}
			set {
				if (!___initialized) {
					_IChartController_MasterChartSectionPanel= value;
					changed[ChartControllerProps.PROPERTY_20_MASTERCHARTSECTIONPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		MultiSplitController _IChartController_TableLayoutPanel1;
		public MultiSplitController TableLayoutPanel1
		{
			get {
				return _IChartController_TableLayoutPanel1;
			}
			set {
				if (!___initialized) {
					_IChartController_TableLayoutPanel1= value;
					changed[ChartControllerProps.PROPERTY_21_TABLELAYOUTPANEL1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ITimeLabelPaneController _IChartController_TimeLabelPane1;
		public ITimeLabelPaneController TimeLabelPane1
		{
			get {
				return _IChartController_TimeLabelPane1;
			}
			set {
				if (!___initialized) {
					_IChartController_TimeLabelPane1= value;
					changed[ChartControllerProps.PROPERTY_22_TIMELABELPANE1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartController_ZoomOutVButton;
		public ButtonController ZoomOutVButton
		{
			get {
				return _IChartController_ZoomOutVButton;
			}
			set {
				if (!___initialized) {
					_IChartController_ZoomOutVButton= value;
					changed[ChartControllerProps.PROPERTY_23_ZOOMOUTVBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartController_ZoomOutHButton;
		public ButtonController ZoomOutHButton
		{
			get {
				return _IChartController_ZoomOutHButton;
			}
			set {
				if (!___initialized) {
					_IChartController_ZoomOutHButton= value;
					changed[ChartControllerProps.PROPERTY_24_ZOOMOUTHBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartController_ZoomInVButton;
		public ButtonController ZoomInVButton
		{
			get {
				return _IChartController_ZoomInVButton;
			}
			set {
				if (!___initialized) {
					_IChartController_ZoomInVButton= value;
					changed[ChartControllerProps.PROPERTY_25_ZOOMINVBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartController_ZoomInHButton;
		public ButtonController ZoomInHButton
		{
			get {
				return _IChartController_ZoomInHButton;
			}
			set {
				if (!___initialized) {
					_IChartController_ZoomInHButton= value;
					changed[ChartControllerProps.PROPERTY_26_ZOOMINHBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartController_ZoomToFitButton;
		public ButtonController ZoomToFitButton
		{
			get {
				return _IChartController_ZoomToFitButton;
			}
			set {
				if (!___initialized) {
					_IChartController_ZoomToFitButton= value;
					changed[ChartControllerProps.PROPERTY_27_ZOOMTOFITBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartController_ZoomToScrollPriceButton;
		public ButtonController ZoomToScrollPriceButton
		{
			get {
				return _IChartController_ZoomToScrollPriceButton;
			}
			set {
				if (!___initialized) {
					_IChartController_ZoomToScrollPriceButton= value;
					changed[ChartControllerProps.PROPERTY_28_ZOOMTOSCROLLPRICEBUTTON_ID] = true;
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
				return TabPanel.Environment;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
