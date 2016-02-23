using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientNormalChartPaneControllerProps
	{
		public static bool RmiGetProperty(IClientNormalChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientNormalChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientNormalChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			NormalChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientNormalChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.AddDependencies(controller, true);
			}
			NormalChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientNormalChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			NormalChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientNormalChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			NormalChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientNormalChartPaneControllerBase : ClientChartChartPaneControllerEx, IClientNormalChartPaneController
	{

		bool ___initialized = false;



		public ClientNormalChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientNormalChartPaneControllerProps.AddDependencies(this, false);
		}

		public ClientNormalChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientNormalChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientNormalChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientNormalChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientNormalChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientNormalChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientNormalChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}




		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientNormalChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientNormalChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
