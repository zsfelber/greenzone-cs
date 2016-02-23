using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientIndicatorChartPaneControllerProps
	{
		public static bool RmiGetProperty(IClientIndicatorChartPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartChartPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientIndicatorChartPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartChartPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientIndicatorChartPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.Initialize(controller, buffer, true);
			}
			IndicatorChartPaneControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientIndicatorChartPaneController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.AddDependencies(controller, true);
			}
			IndicatorChartPaneControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.SerializationRead(controller, info, context, true);
			}
			IndicatorChartPaneControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientIndicatorChartPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartChartPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
			IndicatorChartPaneControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientIndicatorChartPaneControllerBase : ClientChartChartPaneControllerEx, IClientIndicatorChartPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorChartPaneController_LevelFont_Changed;


		public ClientIndicatorChartPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientIndicatorChartPaneControllerProps.AddDependencies(this, false);
		}

		public ClientIndicatorChartPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientIndicatorChartPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientIndicatorChartPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientIndicatorChartPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientIndicatorChartPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientIndicatorChartPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientIndicatorChartPaneControllerProps.SerializationWrite(this, info, context, false);
		}



		public new virtual IClientIndicatorChartSectionPanelController Parent
		{
			get {
				return (IClientIndicatorChartSectionPanelController) ((IClientChartPaneController)this).Parent;
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
			if (ClientIndicatorChartPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientIndicatorChartPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
