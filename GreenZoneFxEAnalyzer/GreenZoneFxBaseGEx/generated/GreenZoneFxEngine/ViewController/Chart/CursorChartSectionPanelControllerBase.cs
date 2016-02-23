using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class CursorChartSectionPanelControllerProps
	{
		public const int PROPERTY_17_SECTIONRANGE_ID = 17;
		public static bool RmiGetProperty(ICursorChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case CursorChartSectionPanelControllerProps.PROPERTY_17_SECTIONRANGE_ID:
					value = controller.SectionRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ICursorChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case CursorChartSectionPanelControllerProps.PROPERTY_17_SECTIONRANGE_ID:
					controller.SectionRange = (SeriesRange) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ICursorChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(ICursorChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(ICursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.SectionRange = (SeriesRange) info.GetValue("SectionRange", typeof(SeriesRange));
		}

		public static void SerializationWrite(ICursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("SectionRange", controller.SectionRange);
		}

	}
	public abstract class CursorChartSectionPanelControllerBase : ChartSectionPanelControllerBase, ICursorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ICursorChartSectionPanelController_SectionRange_Changed;

		public CursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			CursorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public CursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			CursorChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			CursorChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected CursorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			CursorChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			CursorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			CursorChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
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
			if (CursorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (CursorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
