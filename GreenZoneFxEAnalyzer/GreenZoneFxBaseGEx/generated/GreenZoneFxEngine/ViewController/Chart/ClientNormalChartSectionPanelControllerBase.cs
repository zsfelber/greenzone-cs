using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientNormalChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IClientNormalChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientNormalChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientNormalChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			NormalChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientNormalChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			NormalChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientNormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			NormalChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientNormalChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			NormalChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientNormalChartSectionPanelControllerBase : ClientChartSectionPanelControllerEx, IClientNormalChartSectionPanelController
	{

		bool ___initialized = false;



		public ClientNormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientNormalChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ClientNormalChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientNormalChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientNormalChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientNormalChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientNormalChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientNormalChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientNormalChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientNormalChartController Parent
		{
			get {
				return (IClientNormalChartController) ((IClientChartSectionPanelController)this).Parent;
			}
		}

		// parent property type is the same, no property generated : 
		// IClientChartPaneController  ChartPane
		// in parents : IClientChartSectionPanelController


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientNormalChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientNormalChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
