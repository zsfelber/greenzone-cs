using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class NormalChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(INormalChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INormalChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INormalChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(INormalChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(INormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(INormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class NormalChartSectionPanelControllerBase : ChartSectionPanelControllerBase, INormalChartSectionPanelController
	{

		bool ___initialized = false;


		public NormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NormalChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public NormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			NormalChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected NormalChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			NormalChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (NormalChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
