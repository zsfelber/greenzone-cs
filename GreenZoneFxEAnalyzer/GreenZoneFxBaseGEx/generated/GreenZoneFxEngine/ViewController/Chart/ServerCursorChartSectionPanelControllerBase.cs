using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerCursorChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IServerCursorChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (CursorChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerCursorChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (CursorChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerCursorChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			CursorChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerCursorChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			CursorChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerCursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			CursorChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerCursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			CursorChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerCursorChartSectionPanelControllerBase : ServerChartSectionPanelControllerEx, IServerCursorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ICursorChartSectionPanelController_SectionRange_Changed;


		public ServerCursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, ServerChartControllerEx parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerCursorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ServerCursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerCursorChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerCursorChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerCursorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerCursorChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerCursorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerCursorChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerCursorChartController Parent
		{
			get {
				return (IServerCursorChartController) ((IServerChartSectionPanelController)this).Parent;
			}
		}

		public new virtual IServerCursorChartPaneController ChartPane
		{
			get {
				return (IServerCursorChartPaneController) ((IServerChartSectionPanelController)this).ChartPane;
			}
		}

		SeriesRange _ICursorChartSectionPanelController_SectionRange;
		public override SeriesRange SectionRange
		{
			get {
				return _ICursorChartSectionPanelController_SectionRange;
			}
			set {
				if (_ICursorChartSectionPanelController_SectionRange != value) {
					_ICursorChartSectionPanelController_SectionRange= value;
					changed[CursorChartSectionPanelControllerProps.PROPERTY_17_SECTIONRANGE_ID] = true;
					if (ICursorChartSectionPanelController_SectionRange_Changed != null)
						ICursorChartSectionPanelController_SectionRange_Changed(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerCursorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerCursorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
