using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ChartChartPaneControllerProps
	{
		public const int PROPERTY_38_SECTIONORZIGZAG_ID = 38;
		public static bool RmiGetProperty(IChartChartPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_38_SECTIONORZIGZAG_ID:
					value = controller.SectionOrZigZag;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartChartPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartChartPaneController controller, GreenRmiObjectBuffer buffer)
		{
			controller.SectionOrZigZag = (Boolean) buffer.ChangedProps[ChartChartPaneControllerProps.PROPERTY_38_SECTIONORZIGZAG_ID];
		}

		public static void AddDependencies(IChartChartPaneController controller)
		{
		}

		public static void SerializationRead(IChartChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.SectionOrZigZag = (Boolean) info.GetValue("SectionOrZigZag", typeof(Boolean));
		}

		public static void SerializationWrite(IChartChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SectionOrZigZag", controller.SectionOrZigZag);
		}

	}
	public abstract class ChartChartPaneControllerBase : ChartPaneControllerBase, IChartChartPaneController
	{

		bool ___initialized = false;


		public ChartChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartChartPaneControllerProps.AddDependencies(this);
		}

		public ChartChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartChartPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartChartPaneControllerProps.AddDependencies(this);
		}

		protected ChartChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartChartPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartChartPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartChartPaneControllerProps.SerializationWrite(this, info, context);
		}

		public new IChartChartController Chart
		{
			get {
				return (IChartChartController) base.Chart;
			}
			set {
				base.Chart = (IChartController) value;
			}
		}

		Boolean sectionOrZigZag;
		public Boolean SectionOrZigZag
		{
			get {
				return sectionOrZigZag;
			}
			set {
				if (!___initialized) {
					sectionOrZigZag= value;
					changed[ChartChartPaneControllerProps.PROPERTY_38_SECTIONORZIGZAG_ID] = true;
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
			if (ChartChartPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartChartPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
