using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientOrderChartControllerProps
	{
		public static bool RmiGetProperty(IClientOrderChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (OrderChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientOrderChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (OrderChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientOrderChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.Initialize(controller, buffer, true);
			}
			OrderChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientOrderChartController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.AddDependencies(controller, true);
			}
			OrderChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientOrderChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.SerializationRead(controller, info, context, true);
			}
			OrderChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientOrderChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			OrderChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientOrderChartControllerBase : ClientChartControllerEx, IClientOrderChartController
	{

		bool ___initialized = false;



		public ClientOrderChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientOrderChartControllerProps.AddDependencies(this, false);
		}

		public ClientOrderChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientOrderChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientOrderChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientOrderChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientOrderChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientOrderChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientOrderChartControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientOrderChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IClientOrderChartSectionPanelController) ((IClientChartController)this).MasterChartSectionPanel;
			}
		}

		IOrdersHistoryView _IOrderChartController_OrdersView;
		public IOrdersHistoryView OrdersView
		{
			get {
				return _IOrderChartController_OrdersView;
			}
			set {
				if (!___initialized) {
					_IOrderChartController_OrdersView= value;
					changed[OrderChartControllerProps.PROPERTY_30_ORDERSVIEW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersOverviewController _IOrderChartController_OrdersOverviewPanel;
		public IOrdersOverviewController OrdersOverviewPanel
		{
			get {
				return _IOrderChartController_OrdersOverviewPanel;
			}
			set {
				if (!___initialized) {
					_IOrderChartController_OrdersOverviewPanel= value;
					changed[OrderChartControllerProps.PROPERTY_29_ORDERSOVERVIEWPANEL_ID] = true;
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
			if (ClientOrderChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientOrderChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
