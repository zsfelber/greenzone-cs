using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class NormalChartPaneControllerProps
	{
		public static bool RmiGetProperty(INormalChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(INormalChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INormalChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(INormalChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(INormalChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(INormalChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class NormalChartPaneControllerBase : ChartChartPaneControllerBase, INormalChartPaneController
	{

		bool ___initialized = false;


		public NormalChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NormalChartPaneControllerProps.AddDependencies(this, false);
		}

		public NormalChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			NormalChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected NormalChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			NormalChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (NormalChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
