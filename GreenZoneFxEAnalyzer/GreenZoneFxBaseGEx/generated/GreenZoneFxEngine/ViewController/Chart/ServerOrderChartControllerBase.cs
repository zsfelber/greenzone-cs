using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerOrderChartControllerProps
	{
		public static bool RmiGetProperty(IServerOrderChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerOrderChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IServerOrderChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.Initialize(controller, buffer, true);
			}
			OrderChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerOrderChartController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.AddDependencies(controller, true);
			}
			OrderChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerOrderChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.SerializationRead(controller, info, context, true);
			}
			OrderChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerOrderChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			OrderChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerOrderChartControllerBase : ServerChartControllerEx, IServerOrderChartController
	{

		bool ___initialized = false;



		public ServerOrderChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerOrderChartControllerProps.AddDependencies(this, false);
		}

		public ServerOrderChartControllerBase(GreenRmiManager rmiManager, IMainWinTabPageController tabPanel, IServerChartOwner owner)
			: base(rmiManager, tabPanel, owner)
		{
			___initialized = true;
			ServerOrderChartControllerProps.AddDependencies(this, false);
		}

		public ServerOrderChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerOrderChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerOrderChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerOrderChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerOrderChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerOrderChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerOrderChartControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerOrderChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IServerOrderChartSectionPanelController) ((IServerChartController)this).MasterChartSectionPanel;
			}
			set {
				((IServerChartController)this).MasterChartSectionPanel = value;
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
			if (ServerOrderChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerOrderChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
