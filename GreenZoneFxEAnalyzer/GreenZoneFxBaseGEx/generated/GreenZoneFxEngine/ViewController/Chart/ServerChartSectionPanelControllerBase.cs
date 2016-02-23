using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IServerChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerChartSectionPanelControllerBase : ChartSectionPanelControllerBase, IServerChartSectionPanelController
	{

		bool ___initialized = false;


		public ServerChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ServerChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IServerChartPaneController ChartPane
		{
			get {
				return (IServerChartPaneController) ((IChartSectionPanelController)this).ChartPane;
			}
			set {
				((IChartSectionPanelController)this).ChartPane = value;
			}
		}

		public new virtual IServerChartController Parent
		{
			get {
				return (IServerChartController) ((IChartSectionPanelController)this).Parent;
			}
		}

		public virtual IServerChartOwner Owner
		{
			get {
				return Parent.Owner;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
