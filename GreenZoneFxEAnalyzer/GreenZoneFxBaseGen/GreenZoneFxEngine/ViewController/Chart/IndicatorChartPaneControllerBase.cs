using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class IndicatorChartPaneControllerProps
	{
		public const int PROPERTY_39_LEVELFONT_ID = 39;
		public static bool RmiGetProperty(IIndicatorChartPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_39_LEVELFONT_ID:
					value = controller.LevelFont;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorChartPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_39_LEVELFONT_ID:
					controller.LevelFont = (Font) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorChartPaneController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IIndicatorChartPaneController controller)
		{
		}

		public static void SerializationRead(IIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.LevelFont = (Font) info.GetValue("LevelFont", typeof(Font));
		}

		public static void SerializationWrite(IIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("LevelFont", controller.LevelFont);
		}

	}
	public abstract class IndicatorChartPaneControllerBase : ChartChartPaneControllerBase, IIndicatorChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler LevelFontChanged;

		public IndicatorChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorChartPaneControllerProps.AddDependencies(this);
		}

		public IndicatorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorChartPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorChartPaneControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorChartPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorChartPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorChartPaneControllerProps.SerializationWrite(this, info, context);
		}

		public new IIndicatorChartSectionPanelController Parent
		{
			get {
				return (IIndicatorChartSectionPanelController) base.Parent;
			}
		}

		Font levelFont;
		public virtual Font LevelFont
		{
			get {
				return levelFont;
			}
			set {
				if (levelFont != value) {
					levelFont= value;
					changed[IndicatorChartPaneControllerProps.PROPERTY_39_LEVELFONT_ID] = true;
					if (LevelFontChanged != null)
						LevelFontChanged(this, new PropertyChangedEventArgs("LevelFont", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorChartPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
