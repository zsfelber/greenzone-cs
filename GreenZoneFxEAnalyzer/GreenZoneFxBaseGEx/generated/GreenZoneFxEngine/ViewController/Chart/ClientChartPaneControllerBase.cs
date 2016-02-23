using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientChartPaneControllerProps
	{
		public const int PROPERTY_38_SERIESBARS_ID = 38;
		public const int PROPERTY_39_GRIDPENS_ID = 39;
		public static bool RmiGetProperty(IClientChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ClientChartPaneControllerProps.PROPERTY_38_SERIESBARS_ID:
					value = controller.SeriesBars;
					return true;
				case ClientChartPaneControllerProps.PROPERTY_39_GRIDPENS_ID:
					value = controller.GridPens;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ClientChartPaneControllerProps.PROPERTY_38_SERIESBARS_ID:
					controller.SeriesBars = (List<SeriesBar>) value;
					return true;
				case ClientChartPaneControllerProps.PROPERTY_39_GRIDPENS_ID:
					controller.GridPens = (ReadOnlyCollection<Pen>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.SeriesBars = (List<SeriesBar>) info.GetValue("SeriesBars", typeof(List<SeriesBar>));
			controller.GridPens = (ReadOnlyCollection<Pen>) info.GetValue("GridPens", typeof(ReadOnlyCollection<Pen>));
		}

		public static void SerializationWrite(IClientChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("SeriesBars", controller.SeriesBars);
			info.AddValue("GridPens", controller.GridPens);
		}

	}
	public abstract class ClientChartPaneControllerBase : ChartPaneControllerEx, IClientChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IClientChartPaneController_SeriesBars_Changed;
		public event PropertyChangedEventHandler IClientChartPaneController_GridPens_Changed;

		public ClientChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartPaneControllerProps.AddDependencies(this, false);
		}

		public ClientChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IClientChartController Chart
		{
			get {
				return (IClientChartController) ((IChartPaneController)this).Chart;
			}
			set {
				((IChartPaneController)this).Chart = value;
			}
		}

		public new virtual IClientChartSectionPanelController Parent
		{
			get {
				return (IClientChartSectionPanelController) ((Controller)this).Parent;
			}
		}

		public new virtual IChartOwner Owner
		{
			get {
				return (IChartOwner) ((IChartPaneController)this).Owner;
			}
		}

		List<SeriesBar> _IClientChartPaneController_SeriesBars;
		public List<SeriesBar> SeriesBars
		{
			get {
				return _IClientChartPaneController_SeriesBars;
			}
			set {
				if (_IClientChartPaneController_SeriesBars != value) {
					_IClientChartPaneController_SeriesBars= value;
					changed[ClientChartPaneControllerProps.PROPERTY_38_SERIESBARS_ID] = true;
					if (IClientChartPaneController_SeriesBars_Changed != null)
						IClientChartPaneController_SeriesBars_Changed(this, new PropertyChangedEventArgs("SeriesBars", value));
				}
			}
		}

		public abstract Int32 SeriesBarWidth
		{
			get ;
		}

		public abstract Double ChartWindowTopGapValue
		{
			get ;
		}

		ReadOnlyCollection<Pen> _IClientChartPaneController_GridPens;
		public ReadOnlyCollection<Pen> GridPens
		{
			get {
				return _IClientChartPaneController_GridPens;
			}
			set {
				if (_IClientChartPaneController_GridPens != value) {
					_IClientChartPaneController_GridPens= value;
					changed[ClientChartPaneControllerProps.PROPERTY_39_GRIDPENS_ID] = true;
					if (IClientChartPaneController_GridPens_Changed != null)
						IClientChartPaneController_GridPens_Changed(this, new PropertyChangedEventArgs("GridPens", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
