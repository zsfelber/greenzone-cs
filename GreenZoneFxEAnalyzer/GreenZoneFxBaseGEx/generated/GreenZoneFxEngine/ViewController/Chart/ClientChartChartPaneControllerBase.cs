using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientChartChartPaneControllerProps
	{
		public const int PROPERTY_40_SERIESBARSFROM_ID = 40;
		public const int PROPERTY_41_SERIESBARSTO_ID = 41;
		public static bool RmiGetProperty(IClientChartChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				case ClientChartChartPaneControllerProps.PROPERTY_40_SERIESBARSFROM_ID:
					value = controller.SeriesBarsFrom;
					return true;
				case ClientChartChartPaneControllerProps.PROPERTY_41_SERIESBARSTO_ID:
					value = controller.SeriesBarsTo;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				case ClientChartChartPaneControllerProps.PROPERTY_40_SERIESBARSFROM_ID:
					controller.SeriesBarsFrom = (List<SeriesBar>) value;
					return true;
				case ClientChartChartPaneControllerProps.PROPERTY_41_SERIESBARSTO_ID:
					controller.SeriesBarsTo = (List<SeriesBar>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			ChartChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientChartChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.AddDependencies(controller, true);
			}
			ChartChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientChartChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			ChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			controller.SeriesBarsFrom = (List<SeriesBar>) info.GetValue("SeriesBarsFrom", typeof(List<SeriesBar>));
			controller.SeriesBarsTo = (List<SeriesBar>) info.GetValue("SeriesBarsTo", typeof(List<SeriesBar>));
		}

		public static void SerializationWrite(IClientChartChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			ChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			info.AddValue("SeriesBarsFrom", controller.SeriesBarsFrom);
			info.AddValue("SeriesBarsTo", controller.SeriesBarsTo);
		}

	}
	public abstract class ClientChartChartPaneControllerBase : ClientChartPaneControllerEx, IClientChartChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IClientChartChartPaneController_SeriesBarsFrom_Changed;
		public event PropertyChangedEventHandler IClientChartChartPaneController_SeriesBarsTo_Changed;


		public ClientChartChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartChartPaneControllerProps.AddDependencies(this, false);
		}

		public ClientChartChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientChartChartController Chart
		{
			get {
				return (IClientChartChartController) ((IClientChartPaneController)this).Chart;
			}
			set {
				((IClientChartPaneController)this).Chart = value;
			}
		}

		public virtual IClientChartRuntime ChartRuntime
		{
			get {
				return Chart.ChartRuntime;
			}
		}

		List<SeriesBar> _IClientChartChartPaneController_SeriesBarsFrom;
		public List<SeriesBar> SeriesBarsFrom
		{
			get {
				return _IClientChartChartPaneController_SeriesBarsFrom;
			}
			set {
				if (_IClientChartChartPaneController_SeriesBarsFrom != value) {
					_IClientChartChartPaneController_SeriesBarsFrom= value;
					changed[ClientChartChartPaneControllerProps.PROPERTY_40_SERIESBARSFROM_ID] = true;
					if (IClientChartChartPaneController_SeriesBarsFrom_Changed != null)
						IClientChartChartPaneController_SeriesBarsFrom_Changed(this, new PropertyChangedEventArgs("SeriesBarsFrom", value));
				}
			}
		}

		List<SeriesBar> _IClientChartChartPaneController_SeriesBarsTo;
		public List<SeriesBar> SeriesBarsTo
		{
			get {
				return _IClientChartChartPaneController_SeriesBarsTo;
			}
			set {
				if (_IClientChartChartPaneController_SeriesBarsTo != value) {
					_IClientChartChartPaneController_SeriesBarsTo= value;
					changed[ClientChartChartPaneControllerProps.PROPERTY_41_SERIESBARSTO_ID] = true;
					if (IClientChartChartPaneController_SeriesBarsTo_Changed != null)
						IClientChartChartPaneController_SeriesBarsTo_Changed(this, new PropertyChangedEventArgs("SeriesBarsTo", value));
				}
			}
		}

		Boolean _IChartChartPaneController_SectionOrZigZag;
		public Boolean SectionOrZigZag
		{
			get {
				return _IChartChartPaneController_SectionOrZigZag;
			}
			set {
				if (!___initialized) {
					_IChartChartPaneController_SectionOrZigZag= value;
					changed[ChartChartPaneControllerProps.PROPERTY_38_SECTIONORZIGZAG_ID] = true;
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
			if (ClientChartChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
