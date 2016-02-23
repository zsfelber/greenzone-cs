using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientCursorChartPaneControllerProps
	{
		public static bool RmiGetProperty(IClientCursorChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (CursorChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientCursorChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (CursorChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientCursorChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			CursorChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientCursorChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.AddDependencies(controller, true);
			}
			CursorChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientCursorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			CursorChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientCursorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			CursorChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientCursorChartPaneControllerBase : ClientChartPaneControllerEx, IClientCursorChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ICursorChartPaneController_YearFont_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_MonthFont_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_GradientColor_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_BackColor_Changed;
		public event PropertyChangedEventHandler ICursorChartPaneController_ChartFrameColor_Changed;


		public ClientCursorChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientCursorChartPaneControllerProps.AddDependencies(this, false);
		}

		public ClientCursorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientCursorChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientCursorChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientCursorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientCursorChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientCursorChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientCursorChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientCursorChartController Chart
		{
			get {
				return (IClientCursorChartController) ((IClientChartPaneController)this).Chart;
			}
			set {
				((IClientChartPaneController)this).Chart = value;
			}
		}

		public new virtual IClientCursorChartSectionPanelController Parent
		{
			get {
				return (IClientCursorChartSectionPanelController) ((IClientChartPaneController)this).Parent;
			}
		}

		public virtual IClientChartCursorRuntime CursorRuntime
		{
			get {
				return Chart.CursorRuntime;
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientCursorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientCursorChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
