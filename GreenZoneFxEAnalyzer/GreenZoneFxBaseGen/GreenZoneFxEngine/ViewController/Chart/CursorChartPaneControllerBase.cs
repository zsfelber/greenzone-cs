using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class CursorChartPaneControllerProps
	{
		public const int PROPERTY_38_YEARFONT_ID = 38;
		public const int PROPERTY_39_MONTHFONT_ID = 39;
		public const int PROPERTY_40_GRADIENTCOLOR_ID = 40;
		public const int PROPERTY_41_BACKCOLOR_ID = 41;
		public const int PROPERTY_42_CHARTFRAMECOLOR_ID = 42;
		public const int PROPERTY_43_CHARTFRAMERECT_ID = 43;
		public static bool RmiGetProperty(ICursorChartPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_38_YEARFONT_ID:
					value = controller.YearFont;
					return true;
				case PROPERTY_39_MONTHFONT_ID:
					value = controller.MonthFont;
					return true;
				case PROPERTY_40_GRADIENTCOLOR_ID:
					value = controller.GradientColor;
					return true;
				case PROPERTY_41_BACKCOLOR_ID:
					value = controller.BackColor;
					return true;
				case PROPERTY_42_CHARTFRAMECOLOR_ID:
					value = controller.ChartFrameColor;
					return true;
				case PROPERTY_43_CHARTFRAMERECT_ID:
					value = controller.ChartFrameRect;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ICursorChartPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_38_YEARFONT_ID:
					controller.YearFont = (Font) value;
					return true;
				case PROPERTY_39_MONTHFONT_ID:
					controller.MonthFont = (Font) value;
					return true;
				case PROPERTY_40_GRADIENTCOLOR_ID:
					controller.GradientColor = (Color) value;
					return true;
				case PROPERTY_41_BACKCOLOR_ID:
					controller.BackColor = (Color) value;
					return true;
				case PROPERTY_42_CHARTFRAMECOLOR_ID:
					controller.ChartFrameColor = (Color) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ICursorChartPaneController controller, GreenRmiObjectBuffer buffer)
		{
			controller.ChartFrameRect = (Rectangle) buffer.ChangedProps[CursorChartPaneControllerProps.PROPERTY_43_CHARTFRAMERECT_ID];
		}

		public static void AddDependencies(ICursorChartPaneController controller)
		{
		}

		public static void SerializationRead(ICursorChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.YearFont = (Font) info.GetValue("YearFont", typeof(Font));
			controller.MonthFont = (Font) info.GetValue("MonthFont", typeof(Font));
			controller.GradientColor = (Color) info.GetValue("GradientColor", typeof(Color));
			controller.BackColor = (Color) info.GetValue("BackColor", typeof(Color));
			controller.ChartFrameColor = (Color) info.GetValue("ChartFrameColor", typeof(Color));
			controller.ChartFrameRect = (Rectangle) info.GetValue("ChartFrameRect", typeof(Rectangle));
		}

		public static void SerializationWrite(ICursorChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("YearFont", controller.YearFont);
			info.AddValue("MonthFont", controller.MonthFont);
			info.AddValue("GradientColor", controller.GradientColor);
			info.AddValue("BackColor", controller.BackColor);
			info.AddValue("ChartFrameColor", controller.ChartFrameColor);
			info.AddValue("ChartFrameRect", controller.ChartFrameRect);
		}

	}
	public abstract class CursorChartPaneControllerBase : ChartPaneControllerBase, ICursorChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler YearFontChanged;
		public event PropertyChangedEventHandler MonthFontChanged;
		public event PropertyChangedEventHandler GradientColorChanged;
		public event PropertyChangedEventHandler BackColorChanged;
		public event PropertyChangedEventHandler ChartFrameColorChanged;

		public CursorChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			CursorChartPaneControllerProps.AddDependencies(this);
		}

		public CursorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			CursorChartPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			CursorChartPaneControllerProps.AddDependencies(this);
		}

		protected CursorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			CursorChartPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			CursorChartPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			CursorChartPaneControllerProps.SerializationWrite(this, info, context);
		}

		public new ICursorChartController Chart
		{
			get {
				return (ICursorChartController) base.Chart;
			}
			set {
				base.Chart = (IChartController) value;
			}
		}

		public new ICursorChartSectionPanelController Parent
		{
			get {
				return (ICursorChartSectionPanelController) base.Parent;
			}
		}

		Font yearFont;
		public virtual Font YearFont
		{
			get {
				return yearFont;
			}
			set {
				if (yearFont != value) {
					yearFont= value;
					changed[CursorChartPaneControllerProps.PROPERTY_38_YEARFONT_ID] = true;
					if (YearFontChanged != null)
						YearFontChanged(this, new PropertyChangedEventArgs("YearFont", value));
				}
			}
		}

		Font monthFont;
		public virtual Font MonthFont
		{
			get {
				return monthFont;
			}
			set {
				if (monthFont != value) {
					monthFont= value;
					changed[CursorChartPaneControllerProps.PROPERTY_39_MONTHFONT_ID] = true;
					if (MonthFontChanged != null)
						MonthFontChanged(this, new PropertyChangedEventArgs("MonthFont", value));
				}
			}
		}

		Color gradientColor;
		public virtual Color GradientColor
		{
			get {
				return gradientColor;
			}
			set {
				if (gradientColor != value) {
					gradientColor= value;
					changed[CursorChartPaneControllerProps.PROPERTY_40_GRADIENTCOLOR_ID] = true;
					if (GradientColorChanged != null)
						GradientColorChanged(this, new PropertyChangedEventArgs("GradientColor", value));
				}
			}
		}

		Color backColor;
		public override Color BackColor
		{
			get {
				return backColor;
			}
			set {
				if (backColor != value) {
					backColor= value;
					changed[CursorChartPaneControllerProps.PROPERTY_41_BACKCOLOR_ID] = true;
					if (BackColorChanged != null)
						BackColorChanged(this, new PropertyChangedEventArgs("BackColor", value));
				}
			}
		}

		Color chartFrameColor;
		public virtual Color ChartFrameColor
		{
			get {
				return chartFrameColor;
			}
			set {
				if (chartFrameColor != value) {
					chartFrameColor= value;
					changed[CursorChartPaneControllerProps.PROPERTY_42_CHARTFRAMECOLOR_ID] = true;
					if (ChartFrameColorChanged != null)
						ChartFrameColorChanged(this, new PropertyChangedEventArgs("ChartFrameColor", value));
				}
			}
		}

		Rectangle chartFrameRect;
		public Rectangle ChartFrameRect
		{
			get {
				return chartFrameRect;
			}
			set {
				if (!___initialized) {
					chartFrameRect= value;
					changed[CursorChartPaneControllerProps.PROPERTY_43_CHARTFRAMERECT_ID] = true;
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
			if (CursorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!CursorChartPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
