using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerChartControllerProps
	{
		public static bool RmiGetProperty(IServerChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerChartController controller, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerChartControllerBase : ChartControllerBase, IServerChartController
	{

		bool ___initialized = false;


		public ServerChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerChartControllerProps.AddDependencies(this, false);
		}

		public ServerChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IServerChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IServerChartSectionPanelController) ((IChartController)this).MasterChartSectionPanel;
			}
			set {
				((IChartController)this).MasterChartSectionPanel = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
