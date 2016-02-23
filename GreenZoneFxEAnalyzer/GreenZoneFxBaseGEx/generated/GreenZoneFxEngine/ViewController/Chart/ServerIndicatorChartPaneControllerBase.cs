using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerIndicatorChartPaneControllerProps
	{
		public static bool RmiGetProperty(IServerIndicatorChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (IndicatorChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerIndicatorChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (IndicatorChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerIndicatorChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			IndicatorChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerIndicatorChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.AddDependencies(controller, true);
			}
			IndicatorChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			IndicatorChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			IndicatorChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerIndicatorChartPaneControllerBase : ServerChartChartPaneControllerBase, IServerIndicatorChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorChartPaneController_LevelFont_Changed;


		public ServerIndicatorChartPaneControllerBase(GreenRmiManager rmiManager, ServerChartSectionPanelControllerEx parent, ServerChartControllerEx chart)
			: base(rmiManager, parent, chart)
		{
			___initialized = true;
			ServerIndicatorChartPaneControllerProps.AddDependencies(this, false);
		}

		public ServerIndicatorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerIndicatorChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerIndicatorChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerIndicatorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerIndicatorChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerIndicatorChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerIndicatorChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IServerIndicatorChartSectionPanelController Parent
		{
			get {
				return (IServerIndicatorChartSectionPanelController) ((IServerChartPaneController)this).Parent;
			}
		}

		Font _IIndicatorChartPaneController_LevelFont;
		public virtual Font LevelFont
		{
			get {
				return _IIndicatorChartPaneController_LevelFont;
			}
			set {
				if (_IIndicatorChartPaneController_LevelFont != value) {
					_IIndicatorChartPaneController_LevelFont= value;
					changed[IndicatorChartPaneControllerProps.PROPERTY_39_LEVELFONT_ID] = true;
					if (IIndicatorChartPaneController_LevelFont_Changed != null)
						IIndicatorChartPaneController_LevelFont_Changed(this, new PropertyChangedEventArgs("LevelFont", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerIndicatorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerIndicatorChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
