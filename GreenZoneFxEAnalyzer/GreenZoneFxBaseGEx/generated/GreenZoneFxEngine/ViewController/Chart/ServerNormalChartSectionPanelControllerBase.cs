using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerNormalChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IServerNormalChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (NormalChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerNormalChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (NormalChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerNormalChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			NormalChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerNormalChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			NormalChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerNormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			NormalChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerNormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			NormalChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerNormalChartSectionPanelControllerBase : ServerChartSectionPanelControllerEx, IServerNormalChartSectionPanelController
	{

		bool ___initialized = false;



		public ServerNormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, ServerChartControllerEx parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerNormalChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ServerNormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerNormalChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerNormalChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerNormalChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerNormalChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerNormalChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerNormalChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerNormalChartController Parent
		{
			get {
				return (IServerNormalChartController) ((IServerChartSectionPanelController)this).Parent;
			}
		}

		// parent property type is the same, no property generated : 
		// IServerChartPaneController  ChartPane
		// in parents : IServerChartSectionPanelController


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerNormalChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerNormalChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
