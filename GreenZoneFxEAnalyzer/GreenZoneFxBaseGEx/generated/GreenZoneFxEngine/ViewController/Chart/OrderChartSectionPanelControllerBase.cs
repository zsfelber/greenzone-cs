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
		public static bool RmiGetProperty(IOrderChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrderChartSectionPanelControllerProps.PROPERTY_17_PROPERTIESBUTTON_ID:
					value = controller.PropertiesButton;
					return true;
				case OrderChartSectionPanelControllerProps.PROPERTY_18_SECTIONRANGE_ID:
					value = controller.SectionRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrderChartSectionPanelControllerProps.PROPERTY_18_SECTIONRANGE_ID:
					controller.SectionRange = (SeriesRange) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrderChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			controller.PropertiesButton = (ButtonController) buffer.ChangedProps[OrderChartSectionPanelControllerProps.PROPERTY_17_PROPERTIESBUTTON_ID];
		}

		public static void AddDependencies(IOrderChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.PropertiesButton);
		}

		public static void SerializationRead(IOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.PropertiesButton = (ButtonController) info.GetValue("PropertiesButton", typeof(ButtonController));
			controller.SectionRange = (SeriesRange) info.GetValue("SectionRange", typeof(SeriesRange));
		}

		public static void SerializationWrite(IOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("PropertiesButton", controller.PropertiesButton);
			info.AddValue("SectionRange", controller.SectionRange);
		}

	}
	public abstract class OrderChartSectionPanelControllerBase : ChartSectionPanelControllerBase, IOrderChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrderChartSectionPanelController_SectionRange_Changed;

		public OrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public OrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		protected OrderChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}


		public virtual IOrderChartController OrderParent
		{
			get {
				return (IOrderChartController)Parent;
			}
		}

		ButtonController _IOrderChartSectionPanelController_PropertiesButton;
		public ButtonController PropertiesButton
		{
			get {
				return _IOrderChartSectionPanelController_PropertiesButton;
			}
			set {
				if (!___initialized) {
					_IOrderChartSectionPanelController_PropertiesButton= value;
					changed[OrderChartSectionPanelControllerProps.PROPERTY_17_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IOrdersOverviewController OrdersOverviewPanel
		{
			get {
				return OrderParent.OrdersOverviewPanel;
			}
		}

		public virtual IOrdersHistoryView OrdersView
		{
			get {
				return OrderParent.OrdersView;
			}
		}

		SeriesRange _IOrderChartSectionPanelController_SectionRange;
		public override SeriesRange SectionRange
		{
			get {
				return _IOrderChartSectionPanelController_SectionRange;
			}
			set {
				if (_IOrderChartSectionPanelController_SectionRange != value) {
					_IOrderChartSectionPanelController_SectionRange= value;
					changed[OrderChartSectionPanelControllerProps.PROPERTY_18_SECTIONRANGE_ID] = true;
					if (IOrderChartSectionPanelController_SectionRange_Changed != null)
						IOrderChartSectionPanelController_SectionRange_Changed(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrderChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
