using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerNormalChartPaneControllerProps
	{
		public static bool RmiGetProperty(IServerNormalChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (NormalChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerNormalChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (NormalChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerNormalChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			NormalChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerNormalChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.AddDependencies(controller, true);
			}
			NormalChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerNormalChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			NormalChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerNormalChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			NormalChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerNormalChartPaneControllerBase : ServerChartChartPaneControllerBase, IServerNormalChartPaneController
	{

		bool ___initialized = false;



		public ServerNormalChartPaneControllerBase(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, ServerChartControllerEx chart)
			: base(rmiManager, parent, chart)
		{
			___initialized = true;
			ServerNormalChartPaneControllerProps.AddDependencies(this, false);
		}

		public ServerNormalChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerNormalChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerNormalChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerNormalChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerNormalChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerNormalChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerNormalChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}




		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerNormalChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerNormalChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
