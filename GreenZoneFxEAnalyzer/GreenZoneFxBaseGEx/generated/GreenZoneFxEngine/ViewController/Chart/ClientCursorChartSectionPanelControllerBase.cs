using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientCursorChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IClientCursorChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientCursorChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientCursorChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			CursorChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientCursorChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			CursorChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientCursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			CursorChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientCursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			CursorChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientCursorChartSectionPanelControllerBase : ClientChartSectionPanelControllerEx, IClientCursorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ICursorChartSectionPanelController_SectionRange_Changed;


		public ClientCursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientCursorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ClientCursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientCursorChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientCursorChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientCursorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientCursorChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientCursorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientCursorChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void ScrollYToPrice();

		public abstract void FindPriceMinMax(ref Double min, ref Double max);



		public override SeriesRange SectionRange
		{
			get {
				return ((ICursorChartSectionPanelController)this).SectionRange;
			}
			set {
				((ICursorChartSectionPanelController)this).SectionRange = value;
			}
		}

		public new virtual IClientCursorChartController Parent
		{
			get {
				return (IClientCursorChartController) ((IClientChartSectionPanelController)this).Parent;
			}
		}

		public new virtual IClientCursorChartPaneController ChartPane
		{
			get {
				return (IClientCursorChartPaneController) ((IClientChartSectionPanelController)this).ChartPane;
			}
		}

		public virtual IClientChartCursorRuntime CursorRuntime
		{
			get {
				return (IClientChartCursorRuntime)Owner;
			}
		}

		public override IndicatorWindowType WindowType
		{
			get {
				return IndicatorWindowType.CHART_WINDOW;
			}
		}

		// WARNING Property duplication : SectionRange

		SeriesRange _ICursorChartSectionPanelController_SectionRange;
		SeriesRange ICursorChartSectionPanelController.SectionRange
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
			if (ClientCursorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientCursorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
