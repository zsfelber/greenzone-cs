using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ChartPaneControllerProps
	{
		public const int PROPERTY_15_CHART_ID = 15;
		public const int PROPERTY_16_DRAGTIMERUSED_ID = 16;
		public const int PROPERTY_17_ASKCOLOR_ID = 17;
		public const int PROPERTY_18_INACTIVECOLOR_ID = 18;
		public const int PROPERTY_19_GRIDCOLOR_ID = 19;
		public const int PROPERTY_20_THUMBRECTANGLE_ID = 20;
		public const int PROPERTY_21_BARRECTANGLE_ID = 21;
		public const int PROPERTY_22_CPBARRECTANGLE_ID = 22;
		public const int PROPERTY_23_CHARTCALCAUTOGAP_ID = 23;
		public const int PROPERTY_24_CHARTLEFTGAP_ID = 24;
		public const int PROPERTY_25_CHARTRIGHTGAP_ID = 25;
		public const int PROPERTY_26_SLIDERBARCOLOR_ID = 26;
		public const int PROPERTY_27_SLIDERMINIMUM_ID = 27;
		public const int PROPERTY_28_SLIDERMAXIMUM_ID = 28;
		public const int PROPERTY_29_SLIDERVALUERANGE_ID = 29;
		public const int PROPERTY_30_SLIDERVALUE_ID = 30;
		public const int PROPERTY_31_CPBARVALUE_ID = 31;
		public const int PROPERTY_32_CPBARVISIBLE_ID = 32;
		public const int PROPERTY_33_THUMBRECTBARVISIBLE_ID = 33;
		public const int PROPERTY_34_SLIDERTHUMBVISIBLE_ID = 34;
		public const int PROPERTY_35_THUMBINITILIAZED_ID = 35;
		public const int PROPERTY_36_SELECTEDINDDEL_ID = 36;
		public const int PROPERTY_37_SELECTEDPROPS_ID = 37;
		public static bool RmiGetProperty(IChartPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_CHART_ID:
					value = controller.Chart;
					return true;
				case PROPERTY_16_DRAGTIMERUSED_ID:
					value = controller.DragTimerUsed;
					return true;
				case PROPERTY_17_ASKCOLOR_ID:
					value = controller.AskColor;
					return true;
				case PROPERTY_18_INACTIVECOLOR_ID:
					value = controller.InactiveColor;
					return true;
				case PROPERTY_19_GRIDCOLOR_ID:
					value = controller.GridColor;
					return true;
				case PROPERTY_20_THUMBRECTANGLE_ID:
					value = controller.ThumbRectangle;
					return true;
				case PROPERTY_21_BARRECTANGLE_ID:
					value = controller.BarRectangle;
					return true;
				case PROPERTY_22_CPBARRECTANGLE_ID:
					value = controller.CpBarRectangle;
					return true;
				case PROPERTY_23_CHARTCALCAUTOGAP_ID:
					value = controller.ChartCalcAutoGap;
					return true;
				case PROPERTY_24_CHARTLEFTGAP_ID:
					value = controller.ChartLeftGap;
					return true;
				case PROPERTY_25_CHARTRIGHTGAP_ID:
					value = controller.ChartRightGap;
					return true;
				case PROPERTY_26_SLIDERBARCOLOR_ID:
					value = controller.SliderBarColor;
					return true;
				case PROPERTY_27_SLIDERMINIMUM_ID:
					value = controller.SliderMinimum;
					return true;
				case PROPERTY_28_SLIDERMAXIMUM_ID:
					value = controller.SliderMaximum;
					return true;
				case PROPERTY_29_SLIDERVALUERANGE_ID:
					value = controller.SliderValueRange;
					return true;
				case PROPERTY_30_SLIDERVALUE_ID:
					value = controller.SliderValue;
					return true;
				case PROPERTY_31_CPBARVALUE_ID:
					value = controller.CpBarValue;
					return true;
				case PROPERTY_32_CPBARVISIBLE_ID:
					value = controller.CpBarVisible;
					return true;
				case PROPERTY_33_THUMBRECTBARVISIBLE_ID:
					value = controller.ThumbRectBarVisible;
					return true;
				case PROPERTY_34_SLIDERTHUMBVISIBLE_ID:
					value = controller.SliderThumbVisible;
					return true;
				case PROPERTY_35_THUMBINITILIAZED_ID:
					value = controller.ThumbInitiliazed;
					return true;
				case PROPERTY_36_SELECTEDINDDEL_ID:
					value = controller.SelectedIndDel;
					return true;
				case PROPERTY_37_SELECTEDPROPS_ID:
					value = controller.SelectedProps;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_16_DRAGTIMERUSED_ID:
					controller.DragTimerUsed = (Boolean) value;
					return true;
				case PROPERTY_17_ASKCOLOR_ID:
					controller.AskColor = (Color) value;
					return true;
				case PROPERTY_18_INACTIVECOLOR_ID:
					controller.InactiveColor = (Color) value;
					return true;
				case PROPERTY_19_GRIDCOLOR_ID:
					controller.GridColor = (Color) value;
					return true;
				case PROPERTY_23_CHARTCALCAUTOGAP_ID:
					controller.ChartCalcAutoGap = (Boolean) value;
					return true;
				case PROPERTY_24_CHARTLEFTGAP_ID:
					controller.ChartLeftGap = (Int32) value;
					return true;
				case PROPERTY_25_CHARTRIGHTGAP_ID:
					controller.ChartRightGap = (Int32) value;
					return true;
				case PROPERTY_26_SLIDERBARCOLOR_ID:
					controller.SliderBarColor = (Color) value;
					return true;
				case PROPERTY_27_SLIDERMINIMUM_ID:
					controller.SliderMinimum = (Int32) value;
					return true;
				case PROPERTY_28_SLIDERMAXIMUM_ID:
					controller.SliderMaximum = (Int32) value;
					return true;
				case PROPERTY_29_SLIDERVALUERANGE_ID:
					controller.SliderValueRange = (Range) value;
					return true;
				case PROPERTY_30_SLIDERVALUE_ID:
					controller.SliderValue = (Int32) value;
					return true;
				case PROPERTY_31_CPBARVALUE_ID:
					controller.CpBarValue = (Int32) value;
					return true;
				case PROPERTY_32_CPBARVISIBLE_ID:
					controller.CpBarVisible = (Boolean) value;
					return true;
				case PROPERTY_33_THUMBRECTBARVISIBLE_ID:
					controller.ThumbRectBarVisible = (Boolean) value;
					return true;
				case PROPERTY_34_SLIDERTHUMBVISIBLE_ID:
					controller.SliderThumbVisible = (Boolean) value;
					return true;
				case PROPERTY_35_THUMBINITILIAZED_ID:
					controller.ThumbInitiliazed = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartPaneController controller, GreenRmiObjectBuffer buffer)
		{
			controller.Chart = (IChartController) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_15_CHART_ID];
			controller.ThumbRectangle = (Rectangle) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_20_THUMBRECTANGLE_ID];
			controller.BarRectangle = (Rectangle) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_21_BARRECTANGLE_ID];
			controller.CpBarRectangle = (Rectangle) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_22_CPBARRECTANGLE_ID];
			controller.SelectedIndDel = (ButtonController) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_36_SELECTEDINDDEL_ID];
			controller.SelectedProps = (ButtonController) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_37_SELECTEDPROPS_ID];
		}

		public static void AddDependencies(IChartPaneController controller)
		{
			controller.Dependencies.Add(controller.Chart);
			controller.Dependencies.Add(controller.SelectedIndDel);
			controller.Dependencies.Add(controller.SelectedProps);
		}

		public static void SerializationRead(IChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.Chart = (IChartController) info.GetValue("Chart", typeof(IChartController));
			controller.DragTimerUsed = (Boolean) info.GetValue("DragTimerUsed", typeof(Boolean));
			controller.AskColor = (Color) info.GetValue("AskColor", typeof(Color));
			controller.InactiveColor = (Color) info.GetValue("InactiveColor", typeof(Color));
			controller.GridColor = (Color) info.GetValue("GridColor", typeof(Color));
			controller.ThumbRectangle = (Rectangle) info.GetValue("ThumbRectangle", typeof(Rectangle));
			controller.BarRectangle = (Rectangle) info.GetValue("BarRectangle", typeof(Rectangle));
			controller.CpBarRectangle = (Rectangle) info.GetValue("CpBarRectangle", typeof(Rectangle));
			controller.ChartCalcAutoGap = (Boolean) info.GetValue("ChartCalcAutoGap", typeof(Boolean));
			controller.ChartLeftGap = (Int32) info.GetValue("ChartLeftGap", typeof(Int32));
			controller.ChartRightGap = (Int32) info.GetValue("ChartRightGap", typeof(Int32));
			controller.SliderBarColor = (Color) info.GetValue("SliderBarColor", typeof(Color));
			controller.SliderMinimum = (Int32) info.GetValue("SliderMinimum", typeof(Int32));
			controller.SliderMaximum = (Int32) info.GetValue("SliderMaximum", typeof(Int32));
			controller.SliderValueRange = (Range) info.GetValue("SliderValueRange", typeof(Range));
			controller.SliderValue = (Int32) info.GetValue("SliderValue", typeof(Int32));
			controller.CpBarValue = (Int32) info.GetValue("CpBarValue", typeof(Int32));
			controller.CpBarVisible = (Boolean) info.GetValue("CpBarVisible", typeof(Boolean));
			controller.ThumbRectBarVisible = (Boolean) info.GetValue("ThumbRectBarVisible", typeof(Boolean));
			controller.SliderThumbVisible = (Boolean) info.GetValue("SliderThumbVisible", typeof(Boolean));
			controller.ThumbInitiliazed = (Boolean) info.GetValue("ThumbInitiliazed", typeof(Boolean));
			controller.SelectedIndDel = (ButtonController) info.GetValue("SelectedIndDel", typeof(ButtonController));
			controller.SelectedProps = (ButtonController) info.GetValue("SelectedProps", typeof(ButtonController));
		}

		public static void SerializationWrite(IChartPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Chart", controller.Chart);
			info.AddValue("DragTimerUsed", controller.DragTimerUsed);
			info.AddValue("AskColor", controller.AskColor);
			info.AddValue("InactiveColor", controller.InactiveColor);
			info.AddValue("GridColor", controller.GridColor);
			info.AddValue("ThumbRectangle", controller.ThumbRectangle);
			info.AddValue("BarRectangle", controller.BarRectangle);
			info.AddValue("CpBarRectangle", controller.CpBarRectangle);
			info.AddValue("ChartCalcAutoGap", controller.ChartCalcAutoGap);
			info.AddValue("ChartLeftGap", controller.ChartLeftGap);
			info.AddValue("ChartRightGap", controller.ChartRightGap);
			info.AddValue("SliderBarColor", controller.SliderBarColor);
			info.AddValue("SliderMinimum", controller.SliderMinimum);
			info.AddValue("SliderMaximum", controller.SliderMaximum);
			info.AddValue("SliderValueRange", controller.SliderValueRange);
			info.AddValue("SliderValue", controller.SliderValue);
			info.AddValue("CpBarValue", controller.CpBarValue);
			info.AddValue("CpBarVisible", controller.CpBarVisible);
			info.AddValue("ThumbRectBarVisible", controller.ThumbRectBarVisible);
			info.AddValue("SliderThumbVisible", controller.SliderThumbVisible);
			info.AddValue("ThumbInitiliazed", controller.ThumbInitiliazed);
			info.AddValue("SelectedIndDel", controller.SelectedIndDel);
			info.AddValue("SelectedProps", controller.SelectedProps);
		}

	}
	public abstract class ChartPaneControllerBase : ClientController, IChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler DragTimerUsedChanged;
		public event PropertyChangedEventHandler AskColorChanged;
		public event PropertyChangedEventHandler InactiveColorChanged;
		public event PropertyChangedEventHandler GridColorChanged;
		public event PropertyChangedEventHandler ChartCalcAutoGapChanged;
		public event PropertyChangedEventHandler ChartLeftGapChanged;
		public event PropertyChangedEventHandler ChartRightGapChanged;
		public event PropertyChangedEventHandler SliderBarColorChanged;
		public event PropertyChangedEventHandler SliderMinimumChanged;
		public event PropertyChangedEventHandler SliderMaximumChanged;
		public event PropertyChangedEventHandler SliderValueRangeChanged;
		public event PropertyChangedEventHandler SliderValueChanged;
		public event PropertyChangedEventHandler CpBarValueChanged;
		public event PropertyChangedEventHandler CpBarVisibleChanged;
		public event PropertyChangedEventHandler ThumbRectBarVisibleChanged;
		public event PropertyChangedEventHandler SliderThumbVisibleChanged;
		public event PropertyChangedEventHandler ThumbInitiliazedChanged;

		public ChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartPaneControllerProps.AddDependencies(this);
		}

		public ChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartPaneControllerProps.AddDependencies(this);
		}

		protected ChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartPaneControllerProps.SerializationWrite(this, info, context);
		}

		IChartController chart;
		public IChartController Chart
		{
			get {
				return chart;
			}
			set {
				if (!___initialized) {
					chart= value;
					changed[ChartPaneControllerProps.PROPERTY_15_CHART_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public abstract IChartOwner Owner
		{
			get ;
		}

		public new IChartSectionPanelController Parent
		{
			get {
				return (IChartSectionPanelController) base.Parent;
			}
		}

		Boolean dragTimerUsed;
		public virtual Boolean DragTimerUsed
		{
			get {
				return dragTimerUsed;
			}
			set {
				if (dragTimerUsed != value) {
					dragTimerUsed= value;
					changed[ChartPaneControllerProps.PROPERTY_16_DRAGTIMERUSED_ID] = true;
					if (DragTimerUsedChanged != null)
						DragTimerUsedChanged(this, new PropertyChangedEventArgs("DragTimerUsed", value));
				}
			}
		}

		Color askColor;
		public virtual Color AskColor
		{
			get {
				return askColor;
			}
			set {
				if (askColor != value) {
					askColor= value;
					changed[ChartPaneControllerProps.PROPERTY_17_ASKCOLOR_ID] = true;
					if (AskColorChanged != null)
						AskColorChanged(this, new PropertyChangedEventArgs("AskColor", value));
				}
			}
		}

		Color inactiveColor;
		public virtual Color InactiveColor
		{
			get {
				return inactiveColor;
			}
			set {
				if (inactiveColor != value) {
					inactiveColor= value;
					changed[ChartPaneControllerProps.PROPERTY_18_INACTIVECOLOR_ID] = true;
					if (InactiveColorChanged != null)
						InactiveColorChanged(this, new PropertyChangedEventArgs("InactiveColor", value));
				}
			}
		}

		Color gridColor;
		public virtual Color GridColor
		{
			get {
				return gridColor;
			}
			set {
				if (gridColor != value) {
					gridColor= value;
					changed[ChartPaneControllerProps.PROPERTY_19_GRIDCOLOR_ID] = true;
					if (GridColorChanged != null)
						GridColorChanged(this, new PropertyChangedEventArgs("GridColor", value));
				}
			}
		}

		Rectangle thumbRectangle;
		public Rectangle ThumbRectangle
		{
			get {
				return thumbRectangle;
			}
			set {
				if (!___initialized) {
					thumbRectangle= value;
					changed[ChartPaneControllerProps.PROPERTY_20_THUMBRECTANGLE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Rectangle barRectangle;
		public Rectangle BarRectangle
		{
			get {
				return barRectangle;
			}
			set {
				if (!___initialized) {
					barRectangle= value;
					changed[ChartPaneControllerProps.PROPERTY_21_BARRECTANGLE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Rectangle cpBarRectangle;
		public Rectangle CpBarRectangle
		{
			get {
				return cpBarRectangle;
			}
			set {
				if (!___initialized) {
					cpBarRectangle= value;
					changed[ChartPaneControllerProps.PROPERTY_22_CPBARRECTANGLE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean chartCalcAutoGap;
		public virtual Boolean ChartCalcAutoGap
		{
			get {
				return chartCalcAutoGap;
			}
			set {
				if (chartCalcAutoGap != value) {
					chartCalcAutoGap= value;
					changed[ChartPaneControllerProps.PROPERTY_23_CHARTCALCAUTOGAP_ID] = true;
					if (ChartCalcAutoGapChanged != null)
						ChartCalcAutoGapChanged(this, new PropertyChangedEventArgs("ChartCalcAutoGap", value));
				}
			}
		}

		Int32 chartLeftGap;
		public virtual Int32 ChartLeftGap
		{
			get {
				return chartLeftGap;
			}
			set {
				if (chartLeftGap != value) {
					chartLeftGap= value;
					changed[ChartPaneControllerProps.PROPERTY_24_CHARTLEFTGAP_ID] = true;
					if (ChartLeftGapChanged != null)
						ChartLeftGapChanged(this, new PropertyChangedEventArgs("ChartLeftGap", value));
				}
			}
		}

		Int32 chartRightGap;
		public virtual Int32 ChartRightGap
		{
			get {
				return chartRightGap;
			}
			set {
				if (chartRightGap != value) {
					chartRightGap= value;
					changed[ChartPaneControllerProps.PROPERTY_25_CHARTRIGHTGAP_ID] = true;
					if (ChartRightGapChanged != null)
						ChartRightGapChanged(this, new PropertyChangedEventArgs("ChartRightGap", value));
				}
			}
		}

		public abstract Int32 ChartMinimumX
		{
			get ;
		}

		public abstract Int32 ChartMaximumX
		{
			get ;
		}

		public abstract Int32 ChartEffectiveWidth
		{
			get ;
		}

		Color sliderBarColor;
		public virtual Color SliderBarColor
		{
			get {
				return sliderBarColor;
			}
			set {
				if (sliderBarColor != value) {
					sliderBarColor= value;
					changed[ChartPaneControllerProps.PROPERTY_26_SLIDERBARCOLOR_ID] = true;
					if (SliderBarColorChanged != null)
						SliderBarColorChanged(this, new PropertyChangedEventArgs("SliderBarColor", value));
				}
			}
		}

		Int32 sliderMinimum;
		public virtual Int32 SliderMinimum
		{
			get {
				return sliderMinimum;
			}
			set {
				if (sliderMinimum != value) {
					sliderMinimum= value;
					changed[ChartPaneControllerProps.PROPERTY_27_SLIDERMINIMUM_ID] = true;
					if (SliderMinimumChanged != null)
						SliderMinimumChanged(this, new PropertyChangedEventArgs("SliderMinimum", value));
				}
			}
		}

		Int32 sliderMaximum;
		public virtual Int32 SliderMaximum
		{
			get {
				return sliderMaximum;
			}
			set {
				if (sliderMaximum != value) {
					sliderMaximum= value;
					changed[ChartPaneControllerProps.PROPERTY_28_SLIDERMAXIMUM_ID] = true;
					if (SliderMaximumChanged != null)
						SliderMaximumChanged(this, new PropertyChangedEventArgs("SliderMaximum", value));
				}
			}
		}

		Range sliderValueRange;
		public virtual Range SliderValueRange
		{
			get {
				return sliderValueRange;
			}
			set {
				if (sliderValueRange != value) {
					sliderValueRange= value;
					changed[ChartPaneControllerProps.PROPERTY_29_SLIDERVALUERANGE_ID] = true;
					if (SliderValueRangeChanged != null)
						SliderValueRangeChanged(this, new PropertyChangedEventArgs("SliderValueRange", value));
				}
			}
		}

		Int32 sliderValue;
		public virtual Int32 SliderValue
		{
			get {
				return sliderValue;
			}
			set {
				if (sliderValue != value) {
					sliderValue= value;
					changed[ChartPaneControllerProps.PROPERTY_30_SLIDERVALUE_ID] = true;
					if (SliderValueChanged != null)
						SliderValueChanged(this, new PropertyChangedEventArgs("SliderValue", value));
				}
			}
		}

		public abstract Int32 SliderPosition
		{
			get ;
			set ;
		}

		Int32 cpBarValue;
		public virtual Int32 CpBarValue
		{
			get {
				return cpBarValue;
			}
			set {
				if (cpBarValue != value) {
					cpBarValue= value;
					changed[ChartPaneControllerProps.PROPERTY_31_CPBARVALUE_ID] = true;
					if (CpBarValueChanged != null)
						CpBarValueChanged(this, new PropertyChangedEventArgs("CpBarValue", value));
				}
			}
		}

		public abstract Int32 CpBarPosition
		{
			get ;
		}

		Boolean cpBarVisible;
		public virtual Boolean CpBarVisible
		{
			get {
				return cpBarVisible;
			}
			set {
				if (cpBarVisible != value) {
					cpBarVisible= value;
					changed[ChartPaneControllerProps.PROPERTY_32_CPBARVISIBLE_ID] = true;
					if (CpBarVisibleChanged != null)
						CpBarVisibleChanged(this, new PropertyChangedEventArgs("CpBarVisible", value));
				}
			}
		}

		Boolean thumbRectBarVisible;
		public virtual Boolean ThumbRectBarVisible
		{
			get {
				return thumbRectBarVisible;
			}
			set {
				if (thumbRectBarVisible != value) {
					thumbRectBarVisible= value;
					changed[ChartPaneControllerProps.PROPERTY_33_THUMBRECTBARVISIBLE_ID] = true;
					if (ThumbRectBarVisibleChanged != null)
						ThumbRectBarVisibleChanged(this, new PropertyChangedEventArgs("ThumbRectBarVisible", value));
				}
			}
		}

		Boolean sliderThumbVisible;
		public virtual Boolean SliderThumbVisible
		{
			get {
				return sliderThumbVisible;
			}
			set {
				if (sliderThumbVisible != value) {
					sliderThumbVisible= value;
					changed[ChartPaneControllerProps.PROPERTY_34_SLIDERTHUMBVISIBLE_ID] = true;
					if (SliderThumbVisibleChanged != null)
						SliderThumbVisibleChanged(this, new PropertyChangedEventArgs("SliderThumbVisible", value));
				}
			}
		}

		public abstract Int32 ChartAutoGap
		{
			get ;
		}

		public abstract Int32 SliderDefaultAutoGap
		{
			get ;
		}

		public abstract Size ThumbRectangleSize
		{
			get ;
			set ;
		}

		Boolean thumbInitiliazed;
		public Boolean ThumbInitiliazed
		{
			get {
				return thumbInitiliazed;
			}
			set {
				if (thumbInitiliazed != value) {
					thumbInitiliazed= value;
					changed[ChartPaneControllerProps.PROPERTY_35_THUMBINITILIAZED_ID] = true;
					if (ThumbInitiliazedChanged != null)
						ThumbInitiliazedChanged(this, new PropertyChangedEventArgs("ThumbInitiliazed", value));
				}
			}
		}

		ButtonController selectedIndDel;
		public ButtonController SelectedIndDel
		{
			get {
				return selectedIndDel;
			}
			set {
				if (!___initialized) {
					selectedIndDel= value;
					changed[ChartPaneControllerProps.PROPERTY_36_SELECTEDINDDEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController selectedProps;
		public ButtonController SelectedProps
		{
			get {
				return selectedProps;
			}
			set {
				if (!___initialized) {
					selectedProps= value;
					changed[ChartPaneControllerProps.PROPERTY_37_SELECTEDPROPS_ID] = true;
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
			if (ChartPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
