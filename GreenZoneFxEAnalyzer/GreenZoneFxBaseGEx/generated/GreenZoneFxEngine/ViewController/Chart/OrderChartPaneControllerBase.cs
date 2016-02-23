using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class OrderChartPaneControllerProps
	{
		public const int PROPERTY_38_DRAWDOWNCOLOR_ID = 38;
		public static bool RmiGetProperty(IOrderChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrderChartPaneControllerProps.PROPERTY_38_DRAWDOWNCOLOR_ID:
					value = controller.DrawdownColor;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case OrderChartPaneControllerProps.PROPERTY_38_DRAWDOWNCOLOR_ID:
					controller.DrawdownColor = (Color) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrderChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IOrderChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IOrderChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.DrawdownColor = (Color) info.GetValue("DrawdownColor", typeof(Color));
		}

		public static void SerializationWrite(IOrderChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("DrawdownColor", controller.DrawdownColor);
		}

	}
	public abstract class OrderChartPaneControllerBase : ChartPaneControllerBase, IOrderChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IOrderChartPaneController_DrawdownColor_Changed;

		public OrderChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrderChartPaneControllerProps.AddDependencies(this, false);
		}

		public OrderChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrderChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected OrderChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrderChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IOrderChartSectionPanelController Parent
		{
			get {
				return (IOrderChartSectionPanelController) ((Controller)this).Parent;
			}
		}

		public new virtual IOrderChartController Chart
		{
			get {
				return (IOrderChartController) ((IChartPaneController)this).Chart;
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

		public virtual IOrdersHistoryView OrdersView
		{
			get {
				return Chart.OrdersView;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrderChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
