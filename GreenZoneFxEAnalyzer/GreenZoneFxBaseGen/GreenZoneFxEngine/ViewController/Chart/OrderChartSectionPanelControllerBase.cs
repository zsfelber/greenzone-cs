using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class OrderChartSectionPanelControllerProps
	{
		public const int PROPERTY_17_PROPERTIESBUTTON_ID = 17;
		public const int PROPERTY_18_SECTIONRANGE_ID = 18;
		public static bool RmiGetProperty(IOrderChartSectionPanelController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_PROPERTIESBUTTON_ID:
					value = controller.PropertiesButton;
					return true;
				case PROPERTY_18_SECTIONRANGE_ID:
					value = controller.SectionRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderChartSectionPanelController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_18_SECTIONRANGE_ID:
					controller.SectionRange = (SeriesRange) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrderChartSectionPanelController controller, GreenRmiObjectBuffer buffer)
		{
			controller.PropertiesButton = (ButtonController) buffer.ChangedProps[OrderChartSectionPanelControllerProps.PROPERTY_17_PROPERTIESBUTTON_ID];
		}

		public static void AddDependencies(IOrderChartSectionPanelController controller)
		{
			controller.Dependencies.Add(controller.PropertiesButton);
		}

		public static void SerializationRead(IOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			controller.PropertiesButton = (ButtonController) info.GetValue("PropertiesButton", typeof(ButtonController));
			controller.SectionRange = (SeriesRange) info.GetValue("SectionRange", typeof(SeriesRange));
		}

		public static void SerializationWrite(IOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("PropertiesButton", controller.PropertiesButton);
			info.AddValue("SectionRange", controller.SectionRange);
		}

	}
	public abstract class OrderChartSectionPanelControllerBase : ChartSectionPanelControllerBase, IOrderChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SectionRangeChanged;

		public OrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrderChartSectionPanelControllerProps.AddDependencies(this);
		}

		public OrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderChartSectionPanelControllerProps.Initialize(this, buffer);
			___initialized = true;
			OrderChartSectionPanelControllerProps.AddDependencies(this);
		}

		protected OrderChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderChartSectionPanelControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			OrderChartSectionPanelControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderChartSectionPanelControllerProps.SerializationWrite(this, info, context);
		}

		public new IOrderChartController Parent
		{
			get {
				return (IOrderChartController) base.Parent;
			}
		}

		public new IOrderChartPaneController ChartPane
		{
			get {
				return (IOrderChartPaneController) base.ChartPane;
			}
			set {
				base.ChartPane = (IChartPaneController) value;
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
					changed[OrderChartSectionPanelControllerProps.PROPERTY_17_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public abstract IOrdersOverviewController OrdersOverviewPanel
		{
			get ;
		}

		public abstract IOrdersHistoryView OrdersView
		{
			get ;
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
					changed[OrderChartSectionPanelControllerProps.PROPERTY_18_SECTIONRANGE_ID] = true;
					if (SectionRangeChanged != null)
						SectionRangeChanged(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrderChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
