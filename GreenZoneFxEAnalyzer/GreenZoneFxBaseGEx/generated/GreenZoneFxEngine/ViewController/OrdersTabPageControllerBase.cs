using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Orders;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class OrdersTabPageControllerProps
	{
		public const int PROPERTY_17_ORDERSTOOLBAR1_ID = 17;
		public static bool RmiGetProperty(IOrdersTabPageController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (MainWinTabPageControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrdersTabPageControllerProps.PROPERTY_17_ORDERSTOOLBAR1_ID:
					value = controller.OrdersToolbar1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersTabPageController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (MainWinTabPageControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersTabPageController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.Initialize(controller, buffer, true);
			}
			controller.OrdersToolbar1 = (IOrdersToolbarController) buffer.ChangedProps[OrdersTabPageControllerProps.PROPERTY_17_ORDERSTOOLBAR1_ID];
		}

		public static void AddDependencies(IOrdersTabPageController controller, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.OrdersToolbar1);
		}

		public static void SerializationRead(IOrdersTabPageController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.OrdersToolbar1 = (IOrdersToolbarController) info.GetValue("OrdersToolbar1", typeof(IOrdersToolbarController));
		}

		public static void SerializationWrite(IOrdersTabPageController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				MainWinTabPageControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("OrdersToolbar1", controller.OrdersToolbar1);
		}

	}
	public abstract class OrdersTabPageControllerBase : MainWinTabPageControllerBase, IOrdersTabPageController
	{

		bool ___initialized = false;


		public OrdersTabPageControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrdersTabPageControllerProps.AddDependencies(this, false);
		}

		public OrdersTabPageControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			OrdersTabPageControllerProps.AddDependencies(this, false);
		}

		public OrdersTabPageControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			OrdersTabPageControllerProps.AddDependencies(this, false);
		}

		public OrdersTabPageControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersTabPageControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersTabPageControllerProps.AddDependencies(this, false);
		}

		protected OrdersTabPageControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersTabPageControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersTabPageControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersTabPageControllerProps.SerializationWrite(this, info, context, false);
		}


		IOrdersToolbarController _IOrdersTabPageController_OrdersToolbar1;
		public IOrdersToolbarController OrdersToolbar1
		{
			get {
				return _IOrdersTabPageController_OrdersToolbar1;
			}
			set {
				if (!___initialized) {
					_IOrdersTabPageController_OrdersToolbar1= value;
					changed[OrdersTabPageControllerProps.PROPERTY_17_ORDERSTOOLBAR1_ID] = true;
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
			if (OrdersTabPageControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersTabPageControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
