using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class NormalChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(INormalChartSectionPanelController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INormalChartSectionPanelController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INormalChartSectionPanelController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(INormalChartSectionPanelController controller)
		{
		}

		public static void SerializationRead(INormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(INormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class NormalChartSectionPanelControllerBase : ChartSectionPanelControllerBase, INormalChartSectionPanelController
	{

		bool ___initialized = false;


		public NormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NormalChartSectionPanelControllerProps.AddDependencies(this);
		}

		public NormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalChartSectionPanelControllerProps.Initialize(this, buffer);
			___initialized = true;
			NormalChartSectionPanelControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected NormalChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalChartSectionPanelControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			NormalChartSectionPanelControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalChartSectionPanelControllerProps.SerializationWrite(this, info, context);
		}

		public new INormalChartController Parent
		{
			get {
				return (INormalChartController) base.Parent;
			}
		}

		// parent property type is the same, no property generated : 
		// IChartPaneController  ChartPane
		// in parents : IChartSectionPanelController


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!NormalChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
