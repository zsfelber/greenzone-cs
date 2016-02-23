using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class OrderChartControllerProps
	{
		public const int PROPERTY_29_ORDERSOVERVIEWPANEL_ID = 29;
		public const int PROPERTY_30_ORDERSVIEW_ID = 30;
		public static bool RmiGetProperty(IOrderChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrderChartControllerProps.PROPERTY_29_ORDERSOVERVIEWPANEL_ID:
					value = controller.OrdersOverviewPanel;
					return true;
				case OrderChartControllerProps.PROPERTY_30_ORDERSVIEW_ID:
					value = controller.OrdersView;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrderChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.Initialize(controller, buffer, true);
			}
			controller.OrdersOverviewPanel = (IOrdersOverviewController) buffer.ChangedProps[OrderChartControllerProps.PROPERTY_29_ORDERSOVERVIEWPANEL_ID];
			controller.OrdersView = (IOrdersHistoryView) buffer.ChangedProps[OrderChartControllerProps.PROPERTY_30_ORDERSVIEW_ID];
		}

		public static void AddDependencies(IOrderChartController controller, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.OrdersOverviewPanel);
			controller.Dependencies.Add(controller.OrdersView);
		}

		public static void SerializationRead(IOrderChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.OrdersOverviewPanel = (IOrdersOverviewController) info.GetValue("OrdersOverviewPanel", typeof(IOrdersOverviewController));
			controller.OrdersView = (IOrdersHistoryView) info.GetValue("OrdersView", typeof(IOrdersHistoryView));
		}

		public static void SerializationWrite(IOrderChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("OrdersOverviewPanel", controller.OrdersOverviewPanel);
			info.AddValue("OrdersView", controller.OrdersView);
		}

	}
	public abstract class OrderChartControllerBase : ChartControllerBase, IOrderChartController
	{

		bool ___initialized = false;


		public OrderChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrderChartControllerProps.AddDependencies(this, false);
		}

		public OrderChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrderChartControllerProps.AddDependencies(this, false);
		}

		protected OrderChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrderChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderChartControllerProps.SerializationWrite(this, info, context, false);
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrderChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
