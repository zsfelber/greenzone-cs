using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientNormalChartControllerProps
	{
		public static bool RmiGetProperty(IClientNormalChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientNormalChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientNormalChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartControllerProps.Initialize(controller, buffer, true);
			}
			NormalChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientNormalChartController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartControllerProps.AddDependencies(controller, true);
			}
			NormalChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientNormalChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartControllerProps.SerializationRead(controller, info, context, true);
			}
			NormalChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientNormalChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			NormalChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientNormalChartControllerBase : ClientChartChartControllerEx, IClientNormalChartController
	{

		bool ___initialized = false;



		public ClientNormalChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientNormalChartControllerProps.AddDependencies(this, false);
		}

		public ClientNormalChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientNormalChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientNormalChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientNormalChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientNormalChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientNormalChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientNormalChartControllerProps.SerializationWrite(this, info, context, false);
		}



		// parent property type is the same, no property generated : 
		// IClientChartSectionPanelController  MasterChartSectionPanel
		// in parents : IClientChartController,IClientChartChartController


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientNormalChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientNormalChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
