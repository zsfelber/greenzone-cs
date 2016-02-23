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
		public static bool RmiGetProperty(ICursorChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case CursorChartPaneControllerProps.PROPERTY_38_YEARFONT_ID:
					value = controller.YearFont;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_39_MONTHFONT_ID:
					value = controller.MonthFont;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_40_GRADIENTCOLOR_ID:
					value = controller.GradientColor;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_41_BACKCOLOR_ID:
					value = controller.BackColor;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_42_CHARTFRAMECOLOR_ID:
					value = controller.ChartFrameColor;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_43_CHARTFRAMERECT_ID:
					value = controller.ChartFrameRect;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ICursorChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case CursorChartPaneControllerProps.PROPERTY_38_YEARFONT_ID:
					controller.YearFont = (Font) value;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_39_MONTHFONT_ID:
					controller.MonthFont = (Font) value;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_40_GRADIENTCOLOR_ID:
					controller.GradientColor = (Color) value;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_41_BACKCOLOR_ID:
					controller.BackColor = (Color) value;
					return true;
				case CursorChartPaneControllerProps.PROPERTY_42_CHARTFRAMECOLOR_ID:
					controller.ChartFrameColor = (Color) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ICursorChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			controller.ChartFrameRect = (Rectangle) buffer.ChangedProps[CursorChartPaneControllerProps.PROPERTY_43_CHARTFRAMERECT_ID];
		}

		public static void AddDependencies(ICursorChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(ICursorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.YearFont = (Font) info.GetValue("YearFont", typeof(Font));
			controller.MonthFont = (Font) info.GetValue("MonthFont", typeof(Font));
			controller.GradientColor = (Color) info.GetValue("GradientColor", typeof(Color));
			controller.BackColor = (Color) info.GetValue("BackColor", typeof(Color));
			controller.ChartFrameColor = (Color) info.GetValue("ChartFrameColor", typeof(Color));
			controller.ChartFrameRect = (Rectangle) info.GetValue("ChartFrameRect", typeof(Rectangle));
		}

		public static void SerializationWrite(ICursorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
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

		public event PropertyChangedEventHandler ICursorChartPaneController_YearFont_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_MonthFont_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_GradientColor_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_BackColor_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_ChartFrameColor_Changed;

		public CursorChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			CursorChartPaneControllerProps.AddDependencies(this, false);
		}

		public CursorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			CursorChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			CursorChartPaneControllerProps.AddDependencies(this, false);
		}

		protected CursorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			CursorChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			CursorChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			CursorChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		Font _ICursorChartPaneController_YearFont;
		public virtual Font YearFont
		{
			get {
				return _ICursorChartPaneController_YearFont;
			}
			set {
				if (_ICursorChartPaneController_YearFont != value) {
					_ICursorChartPaneController_YearFont= value;
					changed[CursorChartPaneControllerProps.PROPERTY_38_YEARFONT_ID] = true;
					if (ICursorChartPaneController_YearFont_Changed != null)
						ICursorChartPaneController_YearFont_Changed(this, new PropertyChangedEventArgs("YearFont", value));
				}
			}
		}

		Font _ICursorChartPaneController_MonthFont;
		public virtual Font MonthFont
		{
			get {
				return _ICursorChartPaneController_MonthFont;
			}
			set {
				if (_ICursorChartPaneController_MonthFont != value) {
					_ICursorChartPaneController_MonthFont= value;
					changed[CursorChartPaneControllerProps.PROPERTY_39_MONTHFONT_ID] = true;
					if (ICursorChartPaneController_MonthFont_Changed != null)
						ICursorChartPaneController_MonthFont_Changed(this, new PropertyChangedEventArgs("MonthFont", value));
				}
			}
		}

		Color _ICursorChartPaneController_GradientColor;
		public virtual Color GradientColor
		{
			get {
				return _ICursorChartPaneController_GradientColor;
			}
			set {
				if (_ICursorChartPaneController_GradientColor != value) {
					_ICursorChartPaneController_GradientColor= value;
					changed[CursorChartPaneControllerProps.PROPERTY_40_GRADIENTCOLOR_ID] = true;
					if (ICursorChartPaneController_GradientColor_Changed != null)
						ICursorChartPaneController_GradientColor_Changed(this, new PropertyChangedEventArgs("GradientColor", value));
				}
			}
		}

		Color _ICursorChartPaneController_BackColor;
		public override Color BackColor
		{
			get {
				return _ICursorChartPaneController_BackColor;
			}
			set {
				if (_ICursorChartPaneController_BackColor != value) {
					_ICursorChartPaneController_BackColor= value;
					changed[CursorChartPaneControllerProps.PROPERTY_41_BACKCOLOR_ID] = true;
					if (ICursorChartPaneController_BackColor_Changed != null)
						ICursorChartPaneController_BackColor_Changed(this, new PropertyChangedEventArgs("BackColor", value));
				}
			}
		}

		Color _ICursorChartPaneController_ChartFrameColor;
		public virtual Color ChartFrameColor
		{
			get {
				return _ICursorChartPaneController_ChartFrameColor;
			}
			set {
				if (_ICursorChartPaneController_ChartFrameColor != value) {
					_ICursorChartPaneController_ChartFrameColor= value;
					changed[CursorChartPaneControllerProps.PROPERTY_42_CHARTFRAMECOLOR_ID] = true;
					if (ICursorChartPaneController_ChartFrameColor_Changed != null)
						ICursorChartPaneController_ChartFrameColor_Changed(this, new PropertyChangedEventArgs("ChartFrameColor", value));
				}
			}
		}

		Rectangle _ICursorChartPaneController_ChartFrameRect;
		public Rectangle ChartFrameRect
		{
			get {
				return _ICursorChartPaneController_ChartFrameRect;
			}
			set {
				if (!___initialized) {
					_ICursorChartPaneController_ChartFrameRect= value;
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
			if (CursorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (CursorChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
