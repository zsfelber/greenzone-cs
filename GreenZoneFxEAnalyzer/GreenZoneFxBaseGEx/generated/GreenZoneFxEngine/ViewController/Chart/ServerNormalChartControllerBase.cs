using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerNormalChartControllerProps
	{
		public static bool RmiGetProperty(IServerNormalChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (NormalChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerNormalChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (NormalChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerNormalChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartControllerProps.Initialize(controller, buffer, true);
			}
			NormalChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerNormalChartController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartControllerProps.AddDependencies(controller, true);
			}
			NormalChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerNormalChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartControllerProps.SerializationRead(controller, info, context, true);
			}
			NormalChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerNormalChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			NormalChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerNormalChartControllerBase : ServerChartChartControllerEx, IServerNormalChartController
	{

		bool ___initialized = false;



		public ServerNormalChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerNormalChartControllerProps.AddDependencies(this, false);
		}

		public ServerNormalChartControllerBase(GreenRmiManager rmiManager, IChartViewController chartPanel, IChartRuntime chartRuntime)
			: base(rmiManager, chartPanel, chartRuntime)
		{
			___initialized = true;
			ServerNormalChartControllerProps.AddDependencies(this, false);
		}

		public ServerNormalChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerNormalChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerNormalChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerNormalChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerNormalChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerNormalChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerNormalChartControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void AddIndicatorPanel(IServerIndicatorRuntime indicatorRuntime);

		public abstract void RemoveIndicatorPanel(IServerIndicatorRuntime indicatorRuntime);

		public abstract void RemoveIndicatorPanel(IServerIndicatorChartSectionPanelController p);

		public abstract void UpdateIndicatorPanels();



		// parent property type is the same, no property generated : 
		// IServerChartSectionPanelController  MasterChartSectionPanel
		// in parents : IServerChartController,IServerChartChartController


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerNormalChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerNormalChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
