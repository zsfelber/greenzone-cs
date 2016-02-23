using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class NormalChartControllerProps
	{
		public static bool RmiGetProperty(INormalChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(INormalChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INormalChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartChartControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(INormalChartController controller, bool goToParent)
		{
			if (goToParent) {
				ChartChartControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(INormalChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartChartControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(INormalChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartChartControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class NormalChartControllerBase : ChartChartControllerBase, INormalChartController
	{

		bool ___initialized = false;


		public NormalChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NormalChartControllerProps.AddDependencies(this, false);
		}

		public NormalChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			NormalChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected NormalChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			NormalChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalChartControllerProps.SerializationWrite(this, info, context, false);
		}



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (NormalChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
