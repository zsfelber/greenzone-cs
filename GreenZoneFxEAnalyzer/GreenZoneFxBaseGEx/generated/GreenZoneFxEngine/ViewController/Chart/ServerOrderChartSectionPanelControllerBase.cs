using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerOrderChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IServerOrderChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerOrderChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IServerOrderChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			OrderChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerOrderChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			OrderChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			OrderChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerOrderChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			OrderChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerOrderChartSectionPanelControllerBase : ServerChartSectionPanelControllerEx, IServerOrderChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrderChartSectionPanelController_SectionRange_Changed;


		public ServerOrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, ServerChartControllerEx parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerOrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ServerOrderChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerOrderChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerOrderChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerOrderChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerOrderChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerOrderChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerOrderChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerOrderChartController Parent
		{
			get {
				return (IServerOrderChartController) ((IServerChartSectionPanelController)this).Parent;
			}
		}

		public new virtual IServerOrderChartPaneController ChartPane
		{
			get {
				return (IServerOrderChartPaneController) ((IServerChartSectionPanelController)this).ChartPane;
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
			if (ServerOrderChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerOrderChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
