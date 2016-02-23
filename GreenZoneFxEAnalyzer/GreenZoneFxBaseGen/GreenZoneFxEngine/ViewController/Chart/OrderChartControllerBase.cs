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
		public const int PROPERTY_30_ORDERSOVERVIEWPANEL_ID = 30;
		public const int PROPERTY_31_ORDERSVIEW_ID = 31;
		public static bool RmiGetProperty(IOrderChartController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_30_ORDERSOVERVIEWPANEL_ID:
					value = controller.OrdersOverviewPanel;
					return true;
				case PROPERTY_31_ORDERSVIEW_ID:
					value = controller.OrdersView;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderChartController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrderChartController controller, GreenRmiObjectBuffer buffer)
		{
			controller.OrdersOverviewPanel = (IOrdersOverviewController) buffer.ChangedProps[OrderChartControllerProps.PROPERTY_30_ORDERSOVERVIEWPANEL_ID];
			controller.OrdersView = (IOrdersHistoryView) buffer.ChangedProps[OrderChartControllerProps.PROPERTY_31_ORDERSVIEW_ID];
		}

		public static void AddDependencies(IOrderChartController controller)
		{
			controller.Dependencies.Add(controller.OrdersOverviewPanel);
			controller.Dependencies.Add(controller.OrdersView);
		}

		public static void SerializationRead(IOrderChartController controller, SerializationInfo info, StreamingContext context)
		{
			controller.OrdersOverviewPanel = (IOrdersOverviewController) info.GetValue("OrdersOverviewPanel", typeof(IOrdersOverviewController));
			controller.OrdersView = (IOrdersHistoryView) info.GetValue("OrdersView", typeof(IOrdersHistoryView));
		}

		public static void SerializationWrite(IOrderChartController controller, SerializationInfo info, StreamingContext context)
		{
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
			OrderChartControllerProps.AddDependencies(this);
		}

		public OrderChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderChartControllerProps.Initialize(this, buffer);
			___initialized = true;
			OrderChartControllerProps.AddDependencies(this);
		}

		protected OrderChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderChartControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			OrderChartControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderChartControllerProps.SerializationWrite(this, info, context);
		}

		public new IOrderChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IOrderChartSectionPanelController) base.MasterChartSectionPanel;
			}
			set {
				base.MasterChartSectionPanel = (IChartSectionPanelController) value;
			}
		}

		IOrdersOverviewController ordersOverviewPanel;
		public IOrdersOverviewController OrdersOverviewPanel
		{
			get {
				return ordersOverviewPanel;
			}
			set {
				if (!___initialized) {
					ordersOverviewPanel= value;
					changed[OrderChartControllerProps.PROPERTY_30_ORDERSOVERVIEWPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IOrdersHistoryView ordersView;
		public IOrdersHistoryView OrdersView
		{
			get {
				return ordersView;
			}
			set {
				if (!___initialized) {
					ordersView= value;
					changed[OrderChartControllerProps.PROPERTY_31_ORDERSVIEW_ID] = true;
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
			if (OrderChartControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrderChartControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
