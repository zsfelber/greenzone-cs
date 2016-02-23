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
		public static bool RmiGetProperty(IIndicatorChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorChartPaneControllerProps.PROPERTY_39_LEVELFONT_ID:
					value = controller.LevelFont;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorChartPaneControllerProps.PROPERTY_39_LEVELFONT_ID:
					controller.LevelFont = (Font) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IIndicatorChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.LevelFont = (Font) info.GetValue("LevelFont", typeof(Font));
		}

		public static void SerializationWrite(IIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("LevelFont", controller.LevelFont);
		}

	}
	public abstract class IndicatorChartPaneControllerBase : ChartChartPaneControllerBase, IIndicatorChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorChartPaneController_LevelFont_Changed;

		public IndicatorChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			IndicatorChartPaneControllerProps.AddDependencies(this, false);
		}

		public IndicatorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		Font _IIndicatorChartPaneController_LevelFont;
		public virtual Font LevelFont
		{
			get {
				return _IIndicatorChartPaneController_LevelFont;
			}
			set {
				if (_IIndicatorChartPaneController_LevelFont != value) {
					_IIndicatorChartPaneController_LevelFont= value;
					changed[IndicatorChartPaneControllerProps.PROPERTY_39_LEVELFONT_ID] = true;
					if (IIndicatorChartPaneController_LevelFont_Changed != null)
						IIndicatorChartPaneController_LevelFont_Changed(this, new PropertyChangedEventArgs("LevelFont", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
