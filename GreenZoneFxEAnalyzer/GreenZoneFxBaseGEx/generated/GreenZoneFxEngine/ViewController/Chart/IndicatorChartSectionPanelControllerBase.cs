using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class IndicatorChartSectionPanelControllerProps
	{
		public const int PROPERTY_17_CLOSEBUTTON_ID = 17;
		public const int PROPERTY_18_PROPERTIESBUTTON_ID = 18;
		public const int PROPERTY_19_SECTIONRANGE_ID = 19;
		public const int PROPERTY_20_INDICATOR_ID = 20;
		public static bool RmiGetProperty(IIndicatorChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorChartSectionPanelControllerProps.PROPERTY_17_CLOSEBUTTON_ID:
					value = controller.CloseButton;
					return true;
				case IndicatorChartSectionPanelControllerProps.PROPERTY_18_PROPERTIESBUTTON_ID:
					value = controller.PropertiesButton;
					return true;
				case IndicatorChartSectionPanelControllerProps.PROPERTY_19_SECTIONRANGE_ID:
					value = controller.SectionRange;
					return true;
				case IndicatorChartSectionPanelControllerProps.PROPERTY_20_INDICATOR_ID:
					value = controller.Indicator;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorChartSectionPanelControllerProps.PROPERTY_19_SECTIONRANGE_ID:
					controller.SectionRange = (SeriesRange) value;
					return true;
				case IndicatorChartSectionPanelControllerProps.PROPERTY_20_INDICATOR_ID:
					controller.Indicator = (IIndicatorRuntime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			controller.CloseButton = (ButtonController) buffer.ChangedProps[IndicatorChartSectionPanelControllerProps.PROPERTY_17_CLOSEBUTTON_ID];
			controller.PropertiesButton = (ButtonController) buffer.ChangedProps[IndicatorChartSectionPanelControllerProps.PROPERTY_18_PROPERTIESBUTTON_ID];
		}

		public static void AddDependencies(IIndicatorChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.CloseButton);
			controller.Dependencies.Add(controller.PropertiesButton);
		}

		public static void SerializationRead(IIndicatorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.CloseButton = (ButtonController) info.GetValue("CloseButton", typeof(ButtonController));
			controller.PropertiesButton = (ButtonController) info.GetValue("PropertiesButton", typeof(ButtonController));
			controller.SectionRange = (SeriesRange) info.GetValue("SectionRange", typeof(SeriesRange));
			controller.Indicator = (IIndicatorRuntime) info.GetValue("Indicator", typeof(IIndicatorRuntime));
		}

		public static void SerializationWrite(IIndicatorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("CloseButton", controller.CloseButton);
			info.AddValue("PropertiesButton", controller.PropertiesButton);
			info.AddValue("SectionRange", controller.SectionRange);
			info.AddValue("Indicator", controller.Indicator);
		}

	}
	public abstract class IndicatorChartSectionPanelControllerBase : ChartSectionPanelControllerBase, IIndicatorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorChartSectionPanelController_SectionRange_Changed;
		public event PropertyChangedEventHandler IIndicatorChartSectionPanelController_Indicator_Changed;

		public IndicatorChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public IndicatorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		protected IndicatorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}


		ButtonController _IIndicatorChartSectionPanelController_CloseButton;
		public ButtonController CloseButton
		{
			get {
				return _IIndicatorChartSectionPanelController_CloseButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorChartSectionPanelController_CloseButton= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_17_CLOSEBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorChartSectionPanelController_PropertiesButton;
		public ButtonController PropertiesButton
		{
			get {
				return _IIndicatorChartSectionPanelController_PropertiesButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorChartSectionPanelController_PropertiesButton= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_18_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SeriesRange _IIndicatorChartSectionPanelController_SectionRange;
		public override SeriesRange SectionRange
		{
			get {
				return _IIndicatorChartSectionPanelController_SectionRange;
			}
			set {
				if (_IIndicatorChartSectionPanelController_SectionRange != value) {
					_IIndicatorChartSectionPanelController_SectionRange= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_19_SECTIONRANGE_ID] = true;
					if (IIndicatorChartSectionPanelController_SectionRange_Changed != null)
						IIndicatorChartSectionPanelController_SectionRange_Changed(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}

		IIndicatorRuntime _IIndicatorChartSectionPanelController_Indicator;
		public IIndicatorRuntime Indicator
		{
			get {
				return _IIndicatorChartSectionPanelController_Indicator;
			}
			set {
				if (_IIndicatorChartSectionPanelController_Indicator != value) {
					_IIndicatorChartSectionPanelController_Indicator= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_20_INDICATOR_ID] = true;
					if (IIndicatorChartSectionPanelController_Indicator_Changed != null)
						IIndicatorChartSectionPanelController_Indicator_Changed(this, new PropertyChangedEventArgs("Indicator", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
