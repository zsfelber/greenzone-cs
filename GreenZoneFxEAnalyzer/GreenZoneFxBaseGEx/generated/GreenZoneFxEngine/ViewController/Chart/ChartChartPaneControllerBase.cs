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
		public static bool RmiGetProperty(IChartChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ChartChartPaneControllerProps.PROPERTY_38_SECTIONORZIGZAG_ID:
					value = controller.SectionOrZigZag;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			controller.SectionOrZigZag = (Boolean) buffer.ChangedProps[ChartChartPaneControllerProps.PROPERTY_38_SECTIONORZIGZAG_ID];
		}

		public static void AddDependencies(IChartChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IChartChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.SectionOrZigZag = (Boolean) info.GetValue("SectionOrZigZag", typeof(Boolean));
		}

		public static void SerializationWrite(IChartChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
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
			ChartChartPaneControllerProps.AddDependencies(this, false);
		}

		public ChartChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartChartPaneControllerProps.AddDependencies(this, false);
		}

		protected ChartChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		Boolean _IChartChartPaneController_SectionOrZigZag;
		public Boolean SectionOrZigZag
		{
			get {
				return _IChartChartPaneController_SectionOrZigZag;
			}
			set {
				if (!___initialized) {
					_IChartChartPaneController_SectionOrZigZag= value;
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
			if (ChartChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
