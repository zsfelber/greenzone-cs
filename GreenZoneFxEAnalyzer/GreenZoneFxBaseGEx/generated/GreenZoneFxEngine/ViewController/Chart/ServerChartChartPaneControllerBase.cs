using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerChartChartPaneControllerProps
	{
		public static bool RmiGetProperty(IServerChartChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerChartChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerChartChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			ChartChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerChartChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.AddDependencies(controller, true);
			}
			ChartChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerChartChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			ChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerChartChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			ChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerChartChartPaneControllerBase : ServerChartPaneControllerEx, IServerChartChartPaneController
	{

		bool ___initialized = false;



		public ServerChartChartPaneControllerBase(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, ServerChartControllerEx chart)
			: base(rmiManager, parent, chart)
		{
			___initialized = true;
			ServerChartChartPaneControllerProps.AddDependencies(this, false);
		}

		public ServerChartChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerChartChartController Chart
		{
			get {
				return (IServerChartChartController) ((IServerChartPaneController)this).Chart;
			}
			set {
				((IServerChartPaneController)this).Chart = value;
			}
		}

		public virtual IServerChartRuntime ChartRuntime
		{
			get {
				return Chart.ChartRuntime;
			}
		}

		Boolean _IChartChartPaneController_SectionOrZigZag;
		public Boolean SectionOrZigZag
		{
			get {
				return _IChartChartPaneController_SectionOrZigZag;
			}
			set {
				if (!___initialized) {
					_IChartChartPaneController_SectionOrZigZag= value;
					changed[ChartChartPaneControllerProps.PROPERTY_38_SECTIONORZIGZAG_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
