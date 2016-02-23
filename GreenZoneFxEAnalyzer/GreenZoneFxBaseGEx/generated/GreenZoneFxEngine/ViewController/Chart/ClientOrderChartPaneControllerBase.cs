using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientOrderChartPaneControllerProps
	{
		public static bool RmiGetProperty(IClientOrderChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientOrderChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientOrderChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			OrderChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientOrderChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.AddDependencies(controller, true);
			}
			OrderChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientOrderChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			OrderChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientOrderChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			OrderChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientOrderChartPaneControllerBase : ClientChartPaneControllerEx, IClientOrderChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrderChartPaneController_DrawdownColor_Changed;


		public ClientOrderChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientOrderChartPaneControllerProps.AddDependencies(this, false);
		}

		public ClientOrderChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientOrderChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientOrderChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientOrderChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientOrderChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientOrderChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientOrderChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientOrderChartSectionPanelController Parent
		{
			get {
				return (IClientOrderChartSectionPanelController) ((IClientChartPaneController)this).Parent;
			}
		}

		public new virtual IClientOrderChartController Chart
		{
			get {
				return (IClientOrderChartController) ((IClientChartPaneController)this).Chart;
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
			if (ClientOrderChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientOrderChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
