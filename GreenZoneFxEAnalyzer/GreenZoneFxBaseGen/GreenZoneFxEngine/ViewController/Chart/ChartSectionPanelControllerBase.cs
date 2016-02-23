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
		public static bool RmiGetProperty(IChartSectionPanelController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_CHARTPANE_ID:
					value = controller.ChartPane;
					return true;
				case PROPERTY_16_PRICELABELPANE1_ID:
					value = controller.PriceLabelPane1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartSectionPanelController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_CHARTPANE_ID:
					controller.ChartPane = (IChartPaneController) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartSectionPanelController controller, GreenRmiObjectBuffer buffer)
		{
			controller.PriceLabelPane1 = (IPriceLabelPaneController) buffer.ChangedProps[ChartSectionPanelControllerProps.PROPERTY_16_PRICELABELPANE1_ID];
		}

		public static void AddDependencies(IChartSectionPanelController controller)
		{
			controller.Dependencies.Add(controller.PriceLabelPane1);
		}

		public static void SerializationRead(IChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			controller.ChartPane = (IChartPaneController) info.GetValue("ChartPane", typeof(IChartPaneController));
			controller.PriceLabelPane1 = (IPriceLabelPaneController) info.GetValue("PriceLabelPane1", typeof(IPriceLabelPaneController));
		}

		public static void SerializationWrite(IChartSectionPanelController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ChartPane", controller.ChartPane);
			info.AddValue("PriceLabelPane1", controller.PriceLabelPane1);
		}

	}
	public abstract class ChartSectionPanelControllerBase : ClientController, IChartSectionPanelController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ChartPaneChanged;

		public ChartSectionPanelControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartSectionPanelControllerProps.AddDependencies(this);
		}

		public ChartSectionPanelControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartSectionPanelControllerProps.Initialize(this, buffer);
			___initialized = true;
			ChartSectionPanelControllerProps.AddDependencies(this);
		}

		protected ChartSectionPanelControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartSectionPanelControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartSectionPanelControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartSectionPanelControllerProps.SerializationWrite(this, info, context);
		}

		public new IChartController Parent
		{
			get {
				return (IChartController) base.Parent;
			}
		}

		IChartPaneController chartPane;
		public IChartPaneController ChartPane
		{
			get {
				return chartPane;
			}
			set {
				if (chartPane != value) {
					chartPane= value;
					changed[ChartSectionPanelControllerProps.PROPERTY_15_CHARTPANE_ID] = true;
					if (ChartPaneChanged != null)
						ChartPaneChanged(this, new PropertyChangedEventArgs("ChartPane", value));
				}
			}
		}

		IPriceLabelPaneController priceLabelPane1;
		public IPriceLabelPaneController PriceLabelPane1
		{
			get {
				return priceLabelPane1;
			}
			set {
				if (!___initialized) {
					priceLabelPane1= value;
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
			if (ChartSectionPanelControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartSectionPanelControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
