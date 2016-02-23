using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class NormalChartControllerProps
	{
		public static bool RmiGetProperty(INormalChartController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INormalChartController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INormalChartController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(INormalChartController controller)
		{
		}

		public static void SerializationRead(INormalChartController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(INormalChartController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class NormalChartControllerBase : ChartChartControllerBase, INormalChartController
	{

		bool ___initialized = false;


		public NormalChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NormalChartControllerProps.AddDependencies(this);
		}

		public NormalChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalChartControllerProps.Initialize(this, buffer);
			___initialized = true;
			NormalChartControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected NormalChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalChartControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			NormalChartControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalChartControllerProps.SerializationWrite(this, info, context);
		}

		// parent property type is the same, no property generated : 
		// IChartSectionPanelController  MasterChartSectionPanel
		// in parents : IChartController,IChartChartController


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalChartControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!NormalChartControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
