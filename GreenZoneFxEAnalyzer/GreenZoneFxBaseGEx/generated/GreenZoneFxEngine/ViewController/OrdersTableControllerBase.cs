using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class OrdersTableControllerProps
	{
		public const int PROPERTY_18_WORMSPLITCONTAINER1_ID = 18;
		public static bool RmiGetProperty(IOrdersTableController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (OrdersTabPageControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrdersTableControllerProps.PROPERTY_18_WORMSPLITCONTAINER1_ID:
					value = controller.WormSplitContainer1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersTableController controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IOrdersTableController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.Initialize(controller, buffer, true);
			}
			controller.WormSplitContainer1 = (MultiSplitController) buffer.ChangedProps[OrdersTableControllerProps.PROPERTY_18_WORMSPLITCONTAINER1_ID];
		}

		public static void AddDependencies(IOrdersTableController controller, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.WormSplitContainer1);
		}

		public static void SerializationRead(IOrdersTableController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.WormSplitContainer1 = (MultiSplitController) info.GetValue("WormSplitContainer1", typeof(MultiSplitController));
		}

		public static void SerializationWrite(IOrdersTableController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrdersTabPageControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("WormSplitContainer1", controller.WormSplitContainer1);
		}

	}
	public abstract class OrdersTableControllerBase : OrdersTabPageControllerBase, IOrdersTableController
	{

		bool ___initialized = false;


		public OrdersTableControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrdersTableControllerProps.AddDependencies(this, false);
		}

		public OrdersTableControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			OrdersTableControllerProps.AddDependencies(this, false);
		}

		public OrdersTableControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			OrdersTableControllerProps.AddDependencies(this, false);
		}

		public OrdersTableControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersTableControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersTableControllerProps.AddDependencies(this, false);
		}

		protected OrdersTableControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersTableControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersTableControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersTableControllerProps.SerializationWrite(this, info, context, false);
		}


		MultiSplitController _IOrdersTableController_WormSplitContainer1;
		public MultiSplitController WormSplitContainer1
		{
			get {
				return _IOrdersTableController_WormSplitContainer1;
			}
			set {
				if (!___initialized) {
					_IOrdersTableController_WormSplitContainer1= value;
					changed[OrdersTableControllerProps.PROPERTY_18_WORMSPLITCONTAINER1_ID] = true;
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
			if (OrdersTableControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersTableControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
