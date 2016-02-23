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
		public static bool RmiGetProperty(IChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartPaneControllerProps.PROPERTY_15_CHART_ID:
					value = controller.Chart;
					return true;
				case ChartPaneControllerProps.PROPERTY_16_DRAGTIMERUSED_ID:
					value = controller.DragTimerUsed;
					return true;
				case ChartPaneControllerProps.PROPERTY_17_ASKCOLOR_ID:
					value = controller.AskColor;
					return true;
				case ChartPaneControllerProps.PROPERTY_18_INACTIVECOLOR_ID:
					value = controller.InactiveColor;
					return true;
				case ChartPaneControllerProps.PROPERTY_19_GRIDCOLOR_ID:
					value = controller.GridColor;
					return true;
				case ChartPaneControllerProps.PROPERTY_20_THUMBRECTANGLE_ID:
					value = controller.ThumbRectangle;
					return true;
				case ChartPaneControllerProps.PROPERTY_21_BARRECTANGLE_ID:
					value = controller.BarRectangle;
					return true;
				case ChartPaneControllerProps.PROPERTY_22_CPBARRECTANGLE_ID:
					value = controller.CpBarRectangle;
					return true;
				case ChartPaneControllerProps.PROPERTY_23_CHARTCALCAUTOGAP_ID:
					value = controller.ChartCalcAutoGap;
					return true;
				case ChartPaneControllerProps.PROPERTY_24_CHARTLEFTGAP_ID:
					value = controller.ChartLeftGap;
					return true;
				case ChartPaneControllerProps.PROPERTY_25_CHARTRIGHTGAP_ID:
					value = controller.ChartRightGap;
					return true;
				case ChartPaneControllerProps.PROPERTY_26_SLIDERBARCOLOR_ID:
					value = controller.SliderBarColor;
					return true;
				case ChartPaneControllerProps.PROPERTY_27_SLIDERMINIMUM_ID:
					value = controller.SliderMinimum;
					return true;
				case ChartPaneControllerProps.PROPERTY_28_SLIDERMAXIMUM_ID:
					value = controller.SliderMaximum;
					return true;
				case ChartPaneControllerProps.PROPERTY_29_SLIDERVALUERANGE_ID:
					value = controller.SliderValueRange;
					return true;
				case ChartPaneControllerProps.PROPERTY_30_SLIDERVALUE_ID:
					value = controller.SliderValue;
					return true;
				case ChartPaneControllerProps.PROPERTY_31_CPBARVALUE_ID:
					value = controller.CpBarValue;
					return true;
				case ChartPaneControllerProps.PROPERTY_32_CPBARVISIBLE_ID:
					value = controller.CpBarVisible;
					return true;
				case ChartPaneControllerProps.PROPERTY_33_THUMBRECTBARVISIBLE_ID:
					value = controller.ThumbRectBarVisible;
					return true;
				case ChartPaneControllerProps.PROPERTY_34_SLIDERTHUMBVISIBLE_ID:
					value = controller.SliderThumbVisible;
					return true;
				case ChartPaneControllerProps.PROPERTY_35_THUMBINITILIAZED_ID:
					value = controller.ThumbInitiliazed;
					return true;
				case ChartPaneControllerProps.PROPERTY_36_SELECTEDINDDEL_ID:
					value = controller.SelectedIndDel;
					return true;
				case ChartPaneControllerProps.PROPERTY_37_SELECTEDPROPS_ID:
					value = controller.SelectedProps;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartPaneControllerProps.PROPERTY_16_DRAGTIMERUSED_ID:
					controller.DragTimerUsed = (Boolean) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_17_ASKCOLOR_ID:
					controller.AskColor = (Color) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_18_INACTIVECOLOR_ID:
					controller.InactiveColor = (Color) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_19_GRIDCOLOR_ID:
					controller.GridColor = (Color) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_23_CHARTCALCAUTOGAP_ID:
					controller.ChartCalcAutoGap = (Boolean) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_24_CHARTLEFTGAP_ID:
					controller.ChartLeftGap = (Int32) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_25_CHARTRIGHTGAP_ID:
					controller.ChartRightGap = (Int32) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_26_SLIDERBARCOLOR_ID:
					controller.SliderBarColor = (Color) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_27_SLIDERMINIMUM_ID:
					controller.SliderMinimum = (Int32) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_28_SLIDERMAXIMUM_ID:
					controller.SliderMaximum = (Int32) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_29_SLIDERVALUERANGE_ID:
					controller.SliderValueRange = (Range) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_30_SLIDERVALUE_ID:
					controller.SliderValue = (Int32) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_31_CPBARVALUE_ID:
					controller.CpBarValue = (Int32) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_32_CPBARVISIBLE_ID:
					controller.CpBarVisible = (Boolean) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_33_THUMBRECTBARVISIBLE_ID:
					controller.ThumbRectBarVisible = (Boolean) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_34_SLIDERTHUMBVISIBLE_ID:
					controller.SliderThumbVisible = (Boolean) value;
					return true;
				case ChartPaneControllerProps.PROPERTY_35_THUMBINITILIAZED_ID:
					controller.ThumbInitiliazed = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Chart = (IChartController) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_15_CHART_ID];
			controller.ThumbRectangle = (Rectangle) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_20_THUMBRECTANGLE_ID];
			controller.BarRectangle = (Rectangle) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_21_BARRECTANGLE_ID];
			controller.CpBarRectangle = (Rectangle) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_22_CPBARRECTANGLE_ID];
			controller.SelectedIndDel = (ButtonController) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_36_SELECTEDINDDEL_ID];
			controller.SelectedProps = (ButtonController) buffer.ChangedProps[ChartPaneControllerProps.PROPERTY_37_SELECTEDPROPS_ID];
		}

		public static void AddDependencies(IChartPaneController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Chart);
			controller.Dependencies.Add(controller.SelectedIndDel);
			controller.Dependencies.Add(controller.SelectedProps);
		}

		public static void SerializationRead(IChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IChartPaneController_DragTimerUsed_Changed;
		public event PropertyChangedEventHandler IChartPaneController_AskColor_Changed;
		public event PropertyChangedEventHandler IChartPaneController_InactiveColor_Changed;
		public event PropertyChangedEventHandler IChartPaneController_GridColor_Changed;
		public event PropertyChangedEventHandler IChartPaneController_ChartCalcAutoGap_Changed;
		public event PropertyChangedEventHandler IChartPaneController_ChartLeftGap_Changed;
		public event PropertyChangedEventHandler IChartPaneController_ChartRightGap_Changed;
		public event PropertyChangedEventHandler IChartPaneController_SliderBarColor_Changed;
		public event PropertyChangedEventHandler IChartPaneController_SliderMinimum_Changed;
		public event PropertyChangedEventHandler IChartPaneController_SliderMaximum_Changed;
		public event PropertyChangedEventHandler IChartPaneController_SliderValueRange_Changed;
		public event PropertyChangedEventHandler IChartPaneController_SliderValue_Changed;
		public event PropertyChangedEventHandler IChartPaneController_CpBarValue_Changed;
		public event PropertyChangedEventHandler IChartPaneController_CpBarVisible_Changed;
		public event PropertyChangedEventHandler IChartPaneController_ThumbRectBarVisible_Changed;
		public event PropertyChangedEventHandler IChartPaneController_SliderThumbVisible_Changed;
		public event PropertyChangedEventHandler IChartPaneController_ThumbInitiliazed_Changed;

		public ChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartPaneControllerProps.AddDependencies(this, false);
		}

		public ChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartPaneControllerProps.AddDependencies(this, false);
		}

		protected ChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public virtual IEnvironmentRuntime Environment
		{
			get {
				return Chart.Environment;
			}
		}

		IChartController _IChartPaneController_Chart;
		public IChartController Chart
		{
			get {
				return _IChartPaneController_Chart;
			}
			set {
				if (!___initialized) {
					_IChartPaneController_Chart= value;
					changed[ChartPaneControllerProps.PROPERTY_15_CHART_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IServerChartOwner Owner
		{
			get {
				return Chart.Owner;
			}
		}

		Boolean _IChartPaneController_DragTimerUsed;
		public virtual Boolean DragTimerUsed
		{
			get {
				return _IChartPaneController_DragTimerUsed;
			}
			set {
				if (_IChartPaneController_DragTimerUsed != value) {
					_IChartPaneController_DragTimerUsed= value;
					changed[ChartPaneControllerProps.PROPERTY_16_DRAGTIMERUSED_ID] = true;
					if (IChartPaneController_DragTimerUsed_Changed != null)
						IChartPaneController_DragTimerUsed_Changed(this, new PropertyChangedEventArgs("DragTimerUsed", value));
				}
			}
		}

		Color _IChartPaneController_AskColor;
		public virtual Color AskColor
		{
			get {
				return _IChartPaneController_AskColor;
			}
			set {
				if (_IChartPaneController_AskColor != value) {
					_IChartPaneController_AskColor= value;
					changed[ChartPaneControllerProps.PROPERTY_17_ASKCOLOR_ID] = true;
					if (IChartPaneController_AskColor_Changed != null)
						IChartPaneController_AskColor_Changed(this, new PropertyChangedEventArgs("AskColor", value));
				}
			}
		}

		Color _IChartPaneController_InactiveColor;
		public virtual Color InactiveColor
		{
			get {
				return _IChartPaneController_InactiveColor;
			}
			set {
				if (_IChartPaneController_InactiveColor != value) {
					_IChartPaneController_InactiveColor= value;
					changed[ChartPaneControllerProps.PROPERTY_18_INACTIVECOLOR_ID] = true;
					if (IChartPaneController_InactiveColor_Changed != null)
						IChartPaneController_InactiveColor_Changed(this, new PropertyChangedEventArgs("InactiveColor", value));
				}
			}
		}

		Color _IChartPaneController_GridColor;
		public virtual Color GridColor
		{
			get {
				return _IChartPaneController_GridColor;
			}
			set {
				if (_IChartPaneController_GridColor != value) {
					_IChartPaneController_GridColor= value;
					changed[ChartPaneControllerProps.PROPERTY_19_GRIDCOLOR_ID] = true;
					if (IChartPaneController_GridColor_Changed != null)
						IChartPaneController_GridColor_Changed(this, new PropertyChangedEventArgs("GridColor", value));
				}
			}
		}

		Rectangle _IChartPaneController_ThumbRectangle;
		public Rectangle ThumbRectangle
		{
			get {
				return _IChartPaneController_ThumbRectangle;
			}
			set {
				if (!___initialized) {
					_IChartPaneController_ThumbRectangle= value;
					changed[ChartPaneControllerProps.PROPERTY_20_THUMBRECTANGLE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Rectangle _IChartPaneController_BarRectangle;
		public Rectangle BarRectangle
		{
			get {
				return _IChartPaneController_BarRectangle;
			}
			set {
				if (!___initialized) {
					_IChartPaneController_BarRectangle= value;
					changed[ChartPaneControllerProps.PROPERTY_21_BARRECTANGLE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Rectangle _IChartPaneController_CpBarRectangle;
		public Rectangle CpBarRectangle
		{
			get {
				return _IChartPaneController_CpBarRectangle;
			}
			set {
				if (!___initialized) {
					_IChartPaneController_CpBarRectangle= value;
					changed[ChartPaneControllerProps.PROPERTY_22_CPBARRECTANGLE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean _IChartPaneController_ChartCalcAutoGap;
		public virtual Boolean ChartCalcAutoGap
		{
			get {
				return _IChartPaneController_ChartCalcAutoGap;
			}
			set {
				if (_IChartPaneController_ChartCalcAutoGap != value) {
					_IChartPaneController_ChartCalcAutoGap= value;
					changed[ChartPaneControllerProps.PROPERTY_23_CHARTCALCAUTOGAP_ID] = true;
					if (IChartPaneController_ChartCalcAutoGap_Changed != null)
						IChartPaneController_ChartCalcAutoGap_Changed(this, new PropertyChangedEventArgs("ChartCalcAutoGap", value));
				}
			}
		}

		Int32 _IChartPaneController_ChartLeftGap;
		public virtual Int32 ChartLeftGap
		{
			get {
				return _IChartPaneController_ChartLeftGap;
			}
			set {
				if (_IChartPaneController_ChartLeftGap != value) {
					_IChartPaneController_ChartLeftGap= value;
					changed[ChartPaneControllerProps.PROPERTY_24_CHARTLEFTGAP_ID] = true;
					if (IChartPaneController_ChartLeftGap_Changed != null)
						IChartPaneController_ChartLeftGap_Changed(this, new PropertyChangedEventArgs("ChartLeftGap", value));
				}
			}
		}

		Int32 _IChartPaneController_ChartRightGap;
		public virtual Int32 ChartRightGap
		{
			get {
				return _IChartPaneController_ChartRightGap;
			}
			set {
				if (_IChartPaneController_ChartRightGap != value) {
					_IChartPaneController_ChartRightGap= value;
					changed[ChartPaneControllerProps.PROPERTY_25_CHARTRIGHTGAP_ID] = true;
					if (IChartPaneController_ChartRightGap_Changed != null)
						IChartPaneController_ChartRightGap_Changed(this, new PropertyChangedEventArgs("ChartRightGap", value));
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

		Color _IChartPaneController_SliderBarColor;
		public virtual Color SliderBarColor
		{
			get {
				return _IChartPaneController_SliderBarColor;
			}
			set {
				if (_IChartPaneController_SliderBarColor != value) {
					_IChartPaneController_SliderBarColor= value;
					changed[ChartPaneControllerProps.PROPERTY_26_SLIDERBARCOLOR_ID] = true;
					if (IChartPaneController_SliderBarColor_Changed != null)
						IChartPaneController_SliderBarColor_Changed(this, new PropertyChangedEventArgs("SliderBarColor", value));
				}
			}
		}

		Int32 _IChartPaneController_SliderMinimum;
		public virtual Int32 SliderMinimum
		{
			get {
				return _IChartPaneController_SliderMinimum;
			}
			set {
				if (_IChartPaneController_SliderMinimum != value) {
					_IChartPaneController_SliderMinimum= value;
					changed[ChartPaneControllerProps.PROPERTY_27_SLIDERMINIMUM_ID] = true;
					if (IChartPaneController_SliderMinimum_Changed != null)
						IChartPaneController_SliderMinimum_Changed(this, new PropertyChangedEventArgs("SliderMinimum", value));
				}
			}
		}

		Int32 _IChartPaneController_SliderMaximum;
		public virtual Int32 SliderMaximum
		{
			get {
				return _IChartPaneController_SliderMaximum;
			}
			set {
				if (_IChartPaneController_SliderMaximum != value) {
					_IChartPaneController_SliderMaximum= value;
					changed[ChartPaneControllerProps.PROPERTY_28_SLIDERMAXIMUM_ID] = true;
					if (IChartPaneController_SliderMaximum_Changed != null)
						IChartPaneController_SliderMaximum_Changed(this, new PropertyChangedEventArgs("SliderMaximum", value));
				}
			}
		}

		Range _IChartPaneController_SliderValueRange;
		public virtual Range SliderValueRange
		{
			get {
				return _IChartPaneController_SliderValueRange;
			}
			set {
				if (_IChartPaneController_SliderValueRange != value) {
					_IChartPaneController_SliderValueRange= value;
					changed[ChartPaneControllerProps.PROPERTY_29_SLIDERVALUERANGE_ID] = true;
					if (IChartPaneController_SliderValueRange_Changed != null)
						IChartPaneController_SliderValueRange_Changed(this, new PropertyChangedEventArgs("SliderValueRange", value));
				}
			}
		}

		Int32 _IChartPaneController_SliderValue;
		public virtual Int32 SliderValue
		{
			get {
				return _IChartPaneController_SliderValue;
			}
			set {
				if (_IChartPaneController_SliderValue != value) {
					_IChartPaneController_SliderValue= value;
					changed[ChartPaneControllerProps.PROPERTY_30_SLIDERVALUE_ID] = true;
					if (IChartPaneController_SliderValue_Changed != null)
						IChartPaneController_SliderValue_Changed(this, new PropertyChangedEventArgs("SliderValue", value));
				}
			}
		}

		public abstract Int32 SliderPosition
		{
			get ;
			set ;
		}

		Int32 _IChartPaneController_CpBarValue;
		public virtual Int32 CpBarValue
		{
			get {
				return _IChartPaneController_CpBarValue;
			}
			set {
				if (_IChartPaneController_CpBarValue != value) {
					_IChartPaneController_CpBarValue= value;
					changed[ChartPaneControllerProps.PROPERTY_31_CPBARVALUE_ID] = true;
					if (IChartPaneController_CpBarValue_Changed != null)
						IChartPaneController_CpBarValue_Changed(this, new PropertyChangedEventArgs("CpBarValue", value));
				}
			}
		}

		public abstract Int32 CpBarPosition
		{
			get ;
		}

		Boolean _IChartPaneController_CpBarVisible;
		public virtual Boolean CpBarVisible
		{
			get {
				return _IChartPaneController_CpBarVisible;
			}
			set {
				if (_IChartPaneController_CpBarVisible != value) {
					_IChartPaneController_CpBarVisible= value;
					changed[ChartPaneControllerProps.PROPERTY_32_CPBARVISIBLE_ID] = true;
					if (IChartPaneController_CpBarVisible_Changed != null)
						IChartPaneController_CpBarVisible_Changed(this, new PropertyChangedEventArgs("CpBarVisible", value));
				}
			}
		}

		Boolean _IChartPaneController_ThumbRectBarVisible;
		public virtual Boolean ThumbRectBarVisible
		{
			get {
				return _IChartPaneController_ThumbRectBarVisible;
			}
			set {
				if (_IChartPaneController_ThumbRectBarVisible != value) {
					_IChartPaneController_ThumbRectBarVisible= value;
					changed[ChartPaneControllerProps.PROPERTY_33_THUMBRECTBARVISIBLE_ID] = true;
					if (IChartPaneController_ThumbRectBarVisible_Changed != null)
						IChartPaneController_ThumbRectBarVisible_Changed(this, new PropertyChangedEventArgs("ThumbRectBarVisible", value));
				}
			}
		}

		Boolean _IChartPaneController_SliderThumbVisible;
		public virtual Boolean SliderThumbVisible
		{
			get {
				return _IChartPaneController_SliderThumbVisible;
			}
			set {
				if (_IChartPaneController_SliderThumbVisible != value) {
					_IChartPaneController_SliderThumbVisible= value;
					changed[ChartPaneControllerProps.PROPERTY_34_SLIDERTHUMBVISIBLE_ID] = true;
					if (IChartPaneController_SliderThumbVisible_Changed != null)
						IChartPaneController_SliderThumbVisible_Changed(this, new PropertyChangedEventArgs("SliderThumbVisible", value));
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

		Boolean _IChartPaneController_ThumbInitiliazed;
		public Boolean ThumbInitiliazed
		{
			get {
				return _IChartPaneController_ThumbInitiliazed;
			}
			set {
				if (_IChartPaneController_ThumbInitiliazed != value) {
					_IChartPaneController_ThumbInitiliazed= value;
					changed[ChartPaneControllerProps.PROPERTY_35_THUMBINITILIAZED_ID] = true;
					if (IChartPaneController_ThumbInitiliazed_Changed != null)
						IChartPaneController_ThumbInitiliazed_Changed(this, new PropertyChangedEventArgs("ThumbInitiliazed", value));
				}
			}
		}

		ButtonController _IChartPaneController_SelectedIndDel;
		public ButtonController SelectedIndDel
		{
			get {
				return _IChartPaneController_SelectedIndDel;
			}
			set {
				if (!___initialized) {
					_IChartPaneController_SelectedIndDel= value;
					changed[ChartPaneControllerProps.PROPERTY_36_SELECTEDINDDEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IChartPaneController_SelectedProps;
		public ButtonController SelectedProps
		{
			get {
				return _IChartPaneController_SelectedProps;
			}
			set {
				if (!___initialized) {
					_IChartPaneController_SelectedProps= value;
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
			if (ChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
