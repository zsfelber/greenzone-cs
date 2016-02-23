using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class NormalChartPaneControllerProps
	{
		public static bool RmiGetProperty(INormalChartPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INormalChartPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INormalChartPaneController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(INormalChartPaneController controller)
		{
		}

		public static void SerializationRead(INormalChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(INormalChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class NormalChartPaneControllerBase : ChartChartPaneControllerBase, INormalChartPaneController
	{

		bool ___initialized = false;


		public NormalChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NormalChartPaneControllerProps.AddDependencies(this);
		}

		public NormalChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalChartPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			NormalChartPaneControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected NormalChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalChartPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			NormalChartPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalChartPaneControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalChartPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!NormalChartPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
