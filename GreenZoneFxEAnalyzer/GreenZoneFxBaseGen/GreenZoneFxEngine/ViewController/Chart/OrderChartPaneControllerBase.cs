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
		public static bool RmiGetProperty(IOrderChartPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_38_DRAWDOWNCOLOR_ID:
					value = controller.DrawdownColor;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrderChartPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_38_DRAWDOWNCOLOR_ID:
					controller.DrawdownColor = (Color) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IOrderChartPaneController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IOrderChartPaneController controller)
		{
		}

		public static void SerializationRead(IOrderChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.DrawdownColor = (Color) info.GetValue("DrawdownColor", typeof(Color));
		}

		public static void SerializationWrite(IOrderChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DrawdownColor", controller.DrawdownColor);
		}

	}
	public abstract class OrderChartPaneControllerBase : ChartPaneControllerBase, IOrderChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler DrawdownColorChanged;

		public OrderChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrderChartPaneControllerProps.AddDependencies(this);
		}

		public OrderChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrderChartPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			OrderChartPaneControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected OrderChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrderChartPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			OrderChartPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrderChartPaneControllerProps.SerializationWrite(this, info, context);
		}

		public new IOrderChartSectionPanelController Parent
		{
			get {
				return (IOrderChartSectionPanelController) base.Parent;
			}
		}

		public new IOrderChartController Chart
		{
			get {
				return (IOrderChartController) base.Chart;
			}
			set {
				base.Chart = (IChartController) value;
			}
		}

		Color drawdownColor;
		public Color DrawdownColor
		{
			get {
				return drawdownColor;
			}
			set {
				if (drawdownColor != value) {
					drawdownColor= value;
					changed[OrderChartPaneControllerProps.PROPERTY_38_DRAWDOWNCOLOR_ID] = true;
					if (DrawdownColorChanged != null)
						DrawdownColorChanged(this, new PropertyChangedEventArgs("DrawdownColor", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (OrderChartPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrderChartPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
