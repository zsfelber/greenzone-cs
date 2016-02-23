using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientIndicatorChartSectionPanelControllerProps
	{
		public static bool RmiGetProperty(IClientIndicatorChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (IndicatorChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientIndicatorChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (IndicatorChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientIndicatorChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
			IndicatorChartSectionPanelControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientIndicatorChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
			IndicatorChartSectionPanelControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientIndicatorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			IndicatorChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientIndicatorChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			IndicatorChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientIndicatorChartSectionPanelControllerBase : ClientChartSectionPanelControllerEx, IClientIndicatorChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorChartSectionPanelController_SectionRange_Changed;
		public event PropertyChangedEventHandler IIndicatorChartSectionPanelController_Indicator_Changed;


		public ClientIndicatorChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientIndicatorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ClientIndicatorChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientIndicatorChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientIndicatorChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientIndicatorChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientIndicatorChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientIndicatorChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientIndicatorChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientIndicatorChartPaneController ChartPane
		{
			get {
				return (IClientIndicatorChartPaneController) ((IClientChartSectionPanelController)this).ChartPane;
			}
		}

		public new virtual IClientNormalChartController Parent
		{
			get {
				return (IClientNormalChartController) ((IClientChartSectionPanelController)this).Parent;
			}
		}

		public virtual IClientIndicatorRuntime Indicator
		{
			get {
				return (IClientIndicatorRuntime) ((IIndicatorChartSectionPanelController)this).Indicator;
			}
			set {
				((IIndicatorChartSectionPanelController)this).Indicator = value;
			}
		}

		public override SeriesRange SectionRange
		{
			get {
				return ((IIndicatorChartSectionPanelController)this).SectionRange;
			}
			set {
				((IIndicatorChartSectionPanelController)this).SectionRange = value;
			}
		}

		public virtual IClientChartRuntime ChartRuntime
		{
			get {
				return Parent.ChartRuntime;
			}
		}

		// WARNING Property duplication : Indicator

		IIndicatorRuntime _IIndicatorChartSectionPanelController_Indicator;
		IIndicatorRuntime IIndicatorChartSectionPanelController.Indicator
		{
			get {
				return _IIndicatorChartSectionPanelController_Indicator;
			}
			set {
				if (_IIndicatorChartSectionPanelController_Indicator != value) {
					_IIndicatorChartSectionPanelController_Indicator= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_20_INDICATOR_ID] = true;
					if (IIndicatorChartSectionPanelController_Indicator_Changed != null)
						IIndicatorChartSectionPanelController_Indicator_Changed(this, new PropertyChangedEventArgs("Indicator", value));
				}
			}
		}

		// WARNING Property duplication : SectionRange

		SeriesRange _IIndicatorChartSectionPanelController_SectionRange;
		SeriesRange IIndicatorChartSectionPanelController.SectionRange
		{
			get {
				return _IIndicatorChartSectionPanelController_SectionRange;
			}
			set {
				if (_IIndicatorChartSectionPanelController_SectionRange != value) {
					_IIndicatorChartSectionPanelController_SectionRange= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_19_SECTIONRANGE_ID] = true;
					if (IIndicatorChartSectionPanelController_SectionRange_Changed != null)
						IIndicatorChartSectionPanelController_SectionRange_Changed(this, new PropertyChangedEventArgs("SectionRange", value));
				}
			}
		}

		ButtonController _IIndicatorChartSectionPanelController_PropertiesButton;
		public ButtonController PropertiesButton
		{
			get {
				return _IIndicatorChartSectionPanelController_PropertiesButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorChartSectionPanelController_PropertiesButton= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_18_PROPERTIESBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IIndicatorChartSectionPanelController_CloseButton;
		public ButtonController CloseButton
		{
			get {
				return _IIndicatorChartSectionPanelController_CloseButton;
			}
			set {
				if (!___initialized) {
					_IIndicatorChartSectionPanelController_CloseButton= value;
					changed[IndicatorChartSectionPanelControllerProps.PROPERTY_17_CLOSEBUTTON_ID] = true;
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
			if (ClientIndicatorChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientIndicatorChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
