using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class OrdersOverviewControllerProps
	{
		public const int PROPERTY_18_ORDERCHART1_ID = 18;
		public static bool RmiGetProperty(IOrdersOverviewController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (OrdersTabPageControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrdersOverviewControllerProps.PROPERTY_18_ORDERCHART1_ID:
					value = controller.OrderChart1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersOverviewController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (OrdersTabPageControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersOverviewController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.Initialize(controller, buffer, true);
			}
			controller.OrderChart1 = (IOrderChartController) buffer.ChangedProps[OrdersOverviewControllerProps.PROPERTY_18_ORDERCHART1_ID];
		}

		public static void AddDependencies(IOrdersOverviewController controller, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.OrderChart1);
		}

		public static void SerializationRead(IOrdersOverviewController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.OrderChart1 = (IOrderChartController) info.GetValue("OrderChart1", typeof(IOrderChartController));
		}

		public static void SerializationWrite(IOrdersOverviewController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("OrderChart1", controller.OrderChart1);
		}

	}
	public abstract class OrdersOverviewControllerBase : OrdersTabPageControllerBase, IOrdersOverviewController
	{

		bool ___initialized = false;


		public OrdersOverviewControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrdersOverviewControllerProps.AddDependencies(this, false);
		}

		public OrdersOverviewControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			OrdersOverviewControllerProps.AddDependencies(this, false);
		}

		public OrdersOverviewControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			OrdersOverviewControllerProps.AddDependencies(this, false);
		}

		public OrdersOverviewControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersOverviewControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersOverviewControllerProps.AddDependencies(this, false);
		}

		protected OrdersOverviewControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersOverviewControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersOverviewControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersOverviewControllerProps.SerializationWrite(this, info, context, false);
		}


		IOrderChartController _IOrdersOverviewController_OrderChart1;
		public IOrderChartController OrderChart1
		{
			get {
				return _IOrdersOverviewController_OrderChart1;
			}
			set {
				if (!___initialized) {
					_IOrdersOverviewController_OrderChart1= value;
					changed[OrdersOverviewControllerProps.PROPERTY_18_ORDERCHART1_ID] = true;
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
			if (OrdersOverviewControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersOverviewControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
