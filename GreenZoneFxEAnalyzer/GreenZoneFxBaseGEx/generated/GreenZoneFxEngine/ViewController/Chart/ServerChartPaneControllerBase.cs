using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerChartPaneControllerProps
	{
		public static bool RmiGetProperty(IServerChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerChartPaneControllerBase : ChartPaneControllerEx, IServerChartPaneController
	{

		bool ___initialized = false;


		public ServerChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerChartPaneControllerProps.AddDependencies(this, false);
		}

		public ServerChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IServerChartController Chart
		{
			get {
				return (IServerChartController) ((IChartPaneController)this).Chart;
			}
			set {
				((IChartPaneController)this).Chart = value;
			}
		}

		public new virtual IServerChartSectionPanelController Parent
		{
			get {
				return (IServerChartSectionPanelController) ((Controller)this).Parent;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
