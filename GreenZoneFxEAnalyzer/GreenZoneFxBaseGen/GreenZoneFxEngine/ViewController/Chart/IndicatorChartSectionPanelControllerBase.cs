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
		public static bool RmiGetProperty(IIndicatorChartSectionPanelController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_CLOSEBUTTON_ID:
					value = controller.CloseButton;
					return true;
				case PROPERTY_18_PROPERTIESBUTTON_ID:
					value = controller.PropertiesButton;
					return true;
				case PROPERTY_19_SECTIONRANGE_ID:
					value = controller.SectionRange;
					return true;
				case PROPERTY_20_INDICATOR_ID:
					value = controller.Indicator;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorChartSectionPanelController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_19_SECTIONRANGE_ID:
					controller.SectionRange = (SeriesRange) value;
					return true;
				case PROPERTY_20_INDICATOR_ID:
					controller.Indicator = (IIndicatorRuntime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorChartSectionPanelController controller, GreenRmiObjectBuffer buffer)
		{
			controller.CloseButton = (ButtonController) buffer.ChangedProps[IndicatorChartSectionPanelControllerProps.PROPERTY_17_CLOSEBUTTON_ID];
			controller.PropertiesButton = (ButtonController) buffer.ChangedProps[IndicatorChartSectionPanelControllerProps.PROPERTY_18_PROPERTIESBUTTON_ID];
		}

		public static void AddDependencies(IIndicatorChartSectionPanelController controller)
		{
			controller.Dependencies.Add(controller.CloseButton);
			controller.Dependencies.Add(controller.PropertiesButton);
		}

		public static void SerializationRead(IIndicatorChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			controller.CloseButton = (ButtonController) info.GetValue("CloseButton", typeof(ButtonController));
			controller.PropertiesButton = (ButtonController) info.GetValue("PropertiesButton", typeof(ButtonController));
			controller.SectionRange = (SeriesRange) info.GetValue("SectionRange", typeof(SeriesRange));
			controller.Indicator = (IIndicatorRuntime) info.GetValue("Indicator", typeof(IIndicatorRuntime));
		}

		public static void SerializationWrite(IIndicatorChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("CloseButton", controller.CloseButton);
			info.AddValue("PropertiesButton", controller.PropertiesButton);
			info.AddValue("SectionRange", controller.SectionRange);
			info.AddValue("Indicator", controller.Indicator);
		}

	}
	public abstract class IndicatorChartSectionPanelControllerBase : ChartSectionPanelControllerBase, IIndicatorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SectionRangeChanged;
		public event PropertyChangedEventHandler IndicatorChanged;

		public IndicatorChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorChartSectionPanelControllerProps.AddDependencies(this);
		}

		public IndicatorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorChartSectionPanelControllerProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorChartSectionPanelControllerProps.AddDependencies(this);
		}

		protected IndicatorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorChartSectionPanelControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorChartSectionPanelControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorChartSectionPanelControllerProps.SerializationWrite(this, info, context);
		}

		public new IIndicatorChartPaneController ChartPane
		{
			get {
				return (IIndicatorChartPaneController) base.ChartPane;
			}
			set {
				base.ChartPane = (IChartPaneController) value;
			}
		}

		public new INormalChartController Parent
		{
			get {
				return (INormalChartController) base.Parent;
			}
		}

		ButtonController closeButton;
		public ButtonController CloseButton
		{
			get {
				return closeButton;
			}
			set {
				if (!___initialized) {
					closeButton= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_17_CLOSEBUTTON_ID] = true;
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
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_18_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		SeriesRange sectionRange;
		public override SeriesRange SectionRange
		{
			get {
				return sectionRange;
			}
			set {
				if (sectionRange != value) {
					sectionRange= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_19_SECTIONRANGE_ID] = true;
					if (SectionRangeChanged != null)
						SectionRangeChanged(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}

		IIndicatorRuntime indicator;
		public IIndicatorRuntime Indicator
		{
			get {
				return indicator;
			}
			set {
				if (indicator != value) {
					indicator= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_20_INDICATOR_ID] = true;
					if (IndicatorChanged != null)
						IndicatorChanged(this, new PropertyChangedEventArgs("Indicator", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
