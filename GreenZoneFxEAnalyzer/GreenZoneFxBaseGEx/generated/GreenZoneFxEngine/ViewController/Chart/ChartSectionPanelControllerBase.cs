using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ChartSectionPanelControllerProps
	{
		public const int PROPERTY_15_CHARTPANE_ID = 15;
		public const int PROPERTY_16_PRICELABELPANE1_ID = 16;
		public static bool RmiGetProperty(IChartSectionPanelController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartSectionPanelControllerProps.PROPERTY_15_CHARTPANE_ID:
					value = controller.ChartPane;
					return true;
				case ChartSectionPanelControllerProps.PROPERTY_16_PRICELABELPANE1_ID:
					value = controller.PriceLabelPane1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartSectionPanelController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartSectionPanelControllerProps.PROPERTY_15_CHARTPANE_ID:
					controller.ChartPane = (IChartPaneController) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartSectionPanelController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.PriceLabelPane1 = (IPriceLabelPaneController) buffer.ChangedProps[ChartSectionPanelControllerProps.PROPERTY_16_PRICELABELPANE1_ID];
		}

		public static void AddDependencies(IChartSectionPanelController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.PriceLabelPane1);
		}

		public static void SerializationRead(IChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.ChartPane = (IChartPaneController) info.GetValue("ChartPane", typeof(IChartPaneController));
			controller.PriceLabelPane1 = (IPriceLabelPaneController) info.GetValue("PriceLabelPane1", typeof(IPriceLabelPaneController));
		}

		public static void SerializationWrite(IChartSectionPanelController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("ChartPane", controller.ChartPane);
			info.AddValue("PriceLabelPane1", controller.PriceLabelPane1);
		}

	}
	public abstract class ChartSectionPanelControllerBase : ClientController, IChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IChartSectionPanelController_ChartPane_Changed;

		public ChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public ChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartSectionPanelControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		protected ChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartSectionPanelControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartSectionPanelControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartSectionPanelControllerProps.SerializationWrite(this, info, context, false);
		}


		IChartPaneController _IChartSectionPanelController_ChartPane;
		public IChartPaneController ChartPane
		{
			get {
				return _IChartSectionPanelController_ChartPane;
			}
			set {
				if (_IChartSectionPanelController_ChartPane != value) {
					_IChartSectionPanelController_ChartPane= value;
					changed[ChartSectionPanelControllerProps.PROPERTY_15_CHARTPANE_ID] = true;
					if (IChartSectionPanelController_ChartPane_Changed != null)
						IChartSectionPanelController_ChartPane_Changed(this, new PropertyChangedEventArgs("ChartPane", value));
				}
			}
		}

		public new virtual IChartController Parent
		{
			get {
				return (IChartController) ((Controller)this).Parent;
			}
		}

		public virtual IEnvironmentRuntime Environment
		{
			get {
				return Parent.Environment;
			}
		}

		IPriceLabelPaneController _IChartSectionPanelController_PriceLabelPane1;
		public IPriceLabelPaneController PriceLabelPane1
		{
			get {
				return _IChartSectionPanelController_PriceLabelPane1;
			}
			set {
				if (!___initialized) {
					_IChartSectionPanelController_PriceLabelPane1= value;
					changed[ChartSectionPanelControllerProps.PROPERTY_16_PRICELABELPANE1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public abstract IndicatorWindowType WindowType
		{
			get ;
		}

		public abstract String PriceFormat
		{
			get ;
		}

		public abstract Int32 Scale
		{
			get ;
		}

		public abstract SeriesRange SectionRange
		{
			get ;
			set ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
