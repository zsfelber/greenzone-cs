using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerOrderChartPaneControllerProps
	{
		public static bool RmiGetProperty(IServerOrderChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (OrderChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerOrderChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (OrderChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerOrderChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			OrderChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerOrderChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.AddDependencies(controller, true);
			}
			OrderChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerOrderChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			OrderChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerOrderChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			OrderChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerOrderChartPaneControllerBase : ServerChartPaneControllerEx, IServerOrderChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrderChartPaneController_DrawdownColor_Changed;


		public ServerOrderChartPaneControllerBase(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, ServerChartControllerEx chart)
			: base(rmiManager, parent, chart)
		{
			___initialized = true;
			ServerOrderChartPaneControllerProps.AddDependencies(this, false);
		}

		public ServerOrderChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerOrderChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerOrderChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerOrderChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerOrderChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerOrderChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerOrderChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerOrderChartSectionPanelController Parent
		{
			get {
				return (IServerOrderChartSectionPanelController) ((IServerChartPaneController)this).Parent;
			}
		}

		public new virtual IServerOrderChartController Chart
		{
			get {
				return (IServerOrderChartController) ((IServerChartPaneController)this).Chart;
			}
		}

		public virtual IOrdersHistoryView OrdersView
		{
			get {
				return Chart.OrdersView;
			}
		}

		Color _IOrderChartPaneController_DrawdownColor;
		public Color DrawdownColor
		{
			get {
				return _IOrderChartPaneController_DrawdownColor;
			}
			set {
				if (_IOrderChartPaneController_DrawdownColor != value) {
					_IOrderChartPaneController_DrawdownColor= value;
					changed[OrderChartPaneControllerProps.PROPERTY_38_DRAWDOWNCOLOR_ID] = true;
					if (IOrderChartPaneController_DrawdownColor_Changed != null)
						IOrderChartPaneController_DrawdownColor_Changed(this, new PropertyChangedEventArgs("DrawdownColor", value));
				}
			}
		}

		// WARNING Property duplication : Chart

		IOrderChartController IOrderChartPaneController.Chart
		{
			get {
				return (IOrderChartController) ((IChartPaneController)this).Chart;
			}
		}

		// WARNING Property duplication : Parent

		IOrderChartSectionPanelController IOrderChartPaneController.Parent
		{
			get {
				return (IOrderChartSectionPanelController) ((Controller)this).Parent;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerOrderChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerOrderChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
