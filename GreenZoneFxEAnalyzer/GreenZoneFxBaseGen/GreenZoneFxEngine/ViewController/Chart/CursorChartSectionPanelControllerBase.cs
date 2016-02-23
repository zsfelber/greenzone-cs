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
		public static bool RmiGetProperty(ICursorChartSectionPanelController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_SECTIONRANGE_ID:
					value = controller.SectionRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ICursorChartSectionPanelController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_SECTIONRANGE_ID:
					controller.SectionRange = (SeriesRange) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ICursorChartSectionPanelController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(ICursorChartSectionPanelController controller)
		{
		}

		public static void SerializationRead(ICursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			controller.SectionRange = (SeriesRange) info.GetValue("SectionRange", typeof(SeriesRange));
		}

		public static void SerializationWrite(ICursorChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SectionRange", controller.SectionRange);
		}

	}
	public abstract class CursorChartSectionPanelControllerBase : ChartSectionPanelControllerBase, ICursorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SectionRangeChanged;

		public CursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			CursorChartSectionPanelControllerProps.AddDependencies(this);
		}

		public CursorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			CursorChartSectionPanelControllerProps.Initialize(this, buffer);
			___initialized = true;
			CursorChartSectionPanelControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected CursorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			CursorChartSectionPanelControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			CursorChartSectionPanelControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			CursorChartSectionPanelControllerProps.SerializationWrite(this, info, context);
		}

		public new ICursorChartController Parent
		{
			get {
				return (ICursorChartController) base.Parent;
			}
		}

		public new ICursorChartPaneController ChartPane
		{
			get {
				return (ICursorChartPaneController) base.ChartPane;
			}
			set {
				base.ChartPane = (IChartPaneController) value;
			}
		}

		SeriesRange sectionRange;
		public override SeriesRange SectionRange
		{
			get {
				return sectionRange;
			}
			set {
				if (sectionRange != value) {
					sectionRange= value;
					changed[CursorChartSectionPanelControllerProps.PROPERTY_17_SECTIONRANGE_ID] = true;
					if (SectionRangeChanged != null)
						SectionRangeChanged(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (CursorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!CursorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
