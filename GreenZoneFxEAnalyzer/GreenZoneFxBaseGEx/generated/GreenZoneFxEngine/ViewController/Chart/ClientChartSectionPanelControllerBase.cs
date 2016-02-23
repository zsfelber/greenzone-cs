using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientChartSectionPanelControllerProps
	{
		public const int PROPERTY_17_PRICELABELYS_ID = 17;
		public static bool RmiGetProperty(IClientChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ClientChartSectionPanelControllerProps.PROPERTY_17_PRICELABELYS_ID:
					value = controller.PriceLabelYs;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartSectionPanelControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ClientChartSectionPanelControllerProps.PROPERTY_17_PRICELABELYS_ID:
					controller.PriceLabelYs = (IList<PriceLabelY>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartSectionPanelController controller, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.PriceLabelYs = (IList<PriceLabelY>) info.GetValue("PriceLabelYs", typeof(IList<PriceLabelY>));
		}

		public static void SerializationWrite(IClientChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartSectionPanelControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("PriceLabelYs", controller.PriceLabelYs);
		}

	}
	public abstract class ClientChartSectionPanelControllerBase : ChartSectionPanelControllerBase, IClientChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IClientChartSectionPanelController_PriceLabelYs_Changed;

		public ClientChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ClientChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartSectionPanelControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void CalculateSeriesRangeToFit();

		public abstract void UpdateCursor();

		public abstract void UpdateChartOnScreen(Boolean layout);

		public abstract void CalculatePriceLabelYs();

		public abstract void CalculatePriceLabelYs(Int32 levelPix);


		public new virtual IClientChartPaneController ChartPane
		{
			get {
				return (IClientChartPaneController) ((IChartSectionPanelController)this).ChartPane;
			}
			set {
				((IChartSectionPanelController)this).ChartPane = value;
			}
		}

		public new virtual IClientChartController Parent
		{
			get {
				return (IClientChartController) ((IChartSectionPanelController)this).Parent;
			}
		}

		IList<PriceLabelY> _IClientChartSectionPanelController_PriceLabelYs;
		public IList<PriceLabelY> PriceLabelYs
		{
			get {
				return _IClientChartSectionPanelController_PriceLabelYs;
			}
			set {
				if (_IClientChartSectionPanelController_PriceLabelYs != value) {
					_IClientChartSectionPanelController_PriceLabelYs= value;
					changed[ClientChartSectionPanelControllerProps.PROPERTY_17_PRICELABELYS_ID] = true;
					if (IClientChartSectionPanelController_PriceLabelYs_Changed != null)
						IClientChartSectionPanelController_PriceLabelYs_Changed(this, new PropertyChangedEventArgs("PriceLabelYs", value));
				}
			}
		}

		public virtual IChartOwner Owner
		{
			get {
				return Parent.Owner;
			}
		}

		public override SeriesRange SectionRange
		{
			get {
				return Owner.SeriesRange;
			}
			set {
				throw new NotSupportedException();
			}
		}

		public virtual SeriesRange DragYRange
		{
			get {
				return Owner.SeriesRange;
			}
			set {
				Owner.SeriesRange = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
