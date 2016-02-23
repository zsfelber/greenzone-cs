using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ChartChartControllerProps
	{
		public const int PROPERTY_30_CHARTGROUPPANEL_ID = 30;
		public const int PROPERTY_31_CHARTPANEL_ID = 31;
		public static bool RmiGetProperty(IChartChartController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_30_CHARTGROUPPANEL_ID:
					value = controller.ChartGroupPanel;
					return true;
				case PROPERTY_31_CHARTPANEL_ID:
					value = controller.ChartPanel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartChartController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartChartController controller, GreenRmiObjectBuffer buffer)
		{
			controller.ChartGroupPanel = (IChartGroupController) buffer.ChangedProps[ChartChartControllerProps.PROPERTY_30_CHARTGROUPPANEL_ID];
			controller.ChartPanel = (IChartViewController) buffer.ChangedProps[ChartChartControllerProps.PROPERTY_31_CHARTPANEL_ID];
		}

		public static void AddDependencies(IChartChartController controller)
		{
			controller.Dependencies.Add(controller.ChartGroupPanel);
			controller.Dependencies.Add(controller.ChartPanel);
		}

		public static void SerializationRead(IChartChartController controller, SerializationInfo info, StreamingContext context)
		{
			controller.ChartGroupPanel = (IChartGroupController) info.GetValue("ChartGroupPanel", typeof(IChartGroupController));
			controller.ChartPanel = (IChartViewController) info.GetValue("ChartPanel", typeof(IChartViewController));
		}

		public static void SerializationWrite(IChartChartController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ChartGroupPanel", controller.ChartGroupPanel);
			info.AddValue("ChartPanel", controller.ChartPanel);
		}

	}
	public abstract class ChartChartControllerBase : ChartControllerBase, IChartChartController
	{

		bool ___initialized = false;


		public ChartChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartChartControllerProps.AddDependencies(this);
		}

		public ChartChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartChartControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartChartControllerProps.AddDependencies(this);
		}

		protected ChartChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartChartControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartChartControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartChartControllerProps.SerializationWrite(this, info, context);
		}

		IChartGroupController chartGroupPanel;
		public IChartGroupController ChartGroupPanel
		{
			get {
				return chartGroupPanel;
			}
			set {
				if (!___initialized) {
					chartGroupPanel= value;
					changed[ChartChartControllerProps.PROPERTY_30_CHARTGROUPPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartViewController chartPanel;
		public IChartViewController ChartPanel
		{
			get {
				return chartPanel;
			}
			set {
				if (!___initialized) {
					chartPanel= value;
					changed[ChartChartControllerProps.PROPERTY_31_CHARTPANEL_ID] = true;
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
			if (ChartChartControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartChartControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
