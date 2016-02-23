using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientOrderChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IClientOrderChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (OrderChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientOrderChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (OrderChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientOrderChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			OrderChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientOrderChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			OrderChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			OrderChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			OrderChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientOrderChartSectionPanelControllerBase : ClientChartSectionPanelControllerEx, IClientOrderChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrderChartSectionPanelController_SectionRange_Changed;


		public ClientOrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientOrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ClientOrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientOrderChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientOrderChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientOrderChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientOrderChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientOrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientOrderChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientOrderChartController Parent
		{
			get {
				return (IClientOrderChartController) ((IClientChartSectionPanelController)this).Parent;
			}
		}

		public new virtual IClientOrderChartPaneController ChartPane
		{
			get {
				return (IClientOrderChartPaneController) ((IClientChartSectionPanelController)this).ChartPane;
			}
		}

		public override SeriesRange SectionRange
		{
			get {
				return ((IOrderChartSectionPanelController)this).SectionRange;
			}
			set {
				((IOrderChartSectionPanelController)this).SectionRange = value;
			}
		}

		// WARNING Property duplication : SectionRange

		SeriesRange _IOrderChartSectionPanelController_SectionRange;
		SeriesRange IOrderChartSectionPanelController.SectionRange
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

		public virtual IOrdersHistoryView OrdersView
		{
			get {
				return OrderParent.OrdersView;
			}
		}

		public virtual IOrdersOverviewController OrdersOverviewPanel
		{
			get {
				return OrderParent.OrdersOverviewPanel;
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

		public virtual IOrderChartController OrderParent
		{
			get {
				return (IOrderChartController)Parent;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientOrderChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientOrderChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
