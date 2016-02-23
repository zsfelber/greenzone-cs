using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientChartControllerProps
	{
		public const int PROPERTY_29_TIMELABELXSUPPER_ID = 29;
		public const int PROPERTY_30_TIMELABELXSLOWER_ID = 30;
		public static bool RmiGetProperty(IClientChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ClientChartControllerProps.PROPERTY_29_TIMELABELXSUPPER_ID:
					value = controller.TimeLabelXsUpper;
					return true;
				case ClientChartControllerProps.PROPERTY_30_TIMELABELXSLOWER_ID:
					value = controller.TimeLabelXsLower;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ClientChartControllerProps.PROPERTY_29_TIMELABELXSUPPER_ID:
					controller.TimeLabelXsUpper = (List<TimeLabelX>) value;
					return true;
				case ClientChartControllerProps.PROPERTY_30_TIMELABELXSLOWER_ID:
					controller.TimeLabelXsLower = (List<TimeLabelX>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartController controller, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.TimeLabelXsUpper = (List<TimeLabelX>) info.GetValue("TimeLabelXsUpper", typeof(List<TimeLabelX>));
			controller.TimeLabelXsLower = (List<TimeLabelX>) info.GetValue("TimeLabelXsLower", typeof(List<TimeLabelX>));
		}

		public static void SerializationWrite(IClientChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("TimeLabelXsUpper", controller.TimeLabelXsUpper);
			info.AddValue("TimeLabelXsLower", controller.TimeLabelXsLower);
		}

	}
	public abstract class ClientChartControllerBase : ChartControllerBase, IClientChartController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IClientChartController_TimeLabelXsUpper_Changed;
		public event PropertyChangedEventHandler IClientChartController_TimeLabelXsLower_Changed;

		public ClientChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartControllerProps.AddDependencies(this, false);
		}

		public ClientChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void UpdateAllChartAndCursor();

		public abstract void ParentUpdateAllChartAndCursor();

		public abstract void PrintStatus(SeriesBar bar, IndicatorBar ibar, String f);

		public abstract void UpdateCursor();

		public abstract void UpdateChartAndCursor();

		public abstract void UpdateSeries(Boolean enableAutoFit);

		public abstract void UpdateChartOnScreen();

		public abstract void CalculateSeriesRangeToFit(Boolean includeMainChart);

		public abstract void ScrollYToPrice();

		public abstract void CalculateTimeLabelXs();


		public new virtual IChartOwner Owner
		{
			get {
				return (IChartOwner) ((IChartController)this).Owner;
			}
			set {
				((IChartController)this).Owner = value;
			}
		}

		public new virtual IClientChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IClientChartSectionPanelController) ((IChartController)this).MasterChartSectionPanel;
			}
			set {
				((IChartController)this).MasterChartSectionPanel = value;
			}
		}

		List<TimeLabelX> _IClientChartController_TimeLabelXsUpper;
		public List<TimeLabelX> TimeLabelXsUpper
		{
			get {
				return _IClientChartController_TimeLabelXsUpper;
			}
			set {
				if (_IClientChartController_TimeLabelXsUpper != value) {
					_IClientChartController_TimeLabelXsUpper= value;
					changed[ClientChartControllerProps.PROPERTY_29_TIMELABELXSUPPER_ID] = true;
					if (IClientChartController_TimeLabelXsUpper_Changed != null)
						IClientChartController_TimeLabelXsUpper_Changed(this, new PropertyChangedEventArgs("TimeLabelXsUpper", value));
				}
			}
		}

		List<TimeLabelX> _IClientChartController_TimeLabelXsLower;
		public List<TimeLabelX> TimeLabelXsLower
		{
			get {
				return _IClientChartController_TimeLabelXsLower;
			}
			set {
				if (_IClientChartController_TimeLabelXsLower != value) {
					_IClientChartController_TimeLabelXsLower= value;
					changed[ClientChartControllerProps.PROPERTY_30_TIMELABELXSLOWER_ID] = true;
					if (IClientChartController_TimeLabelXsLower_Changed != null)
						IClientChartController_TimeLabelXsLower_Changed(this, new PropertyChangedEventArgs("TimeLabelXsLower", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
