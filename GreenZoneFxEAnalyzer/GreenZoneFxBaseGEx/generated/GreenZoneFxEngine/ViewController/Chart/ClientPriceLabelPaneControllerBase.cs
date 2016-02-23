using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientPriceLabelPaneControllerProps
	{
		public static bool RmiGetProperty(IClientPriceLabelPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (PriceLabelPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientPriceLabelPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (PriceLabelPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientPriceLabelPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				PriceLabelPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientPriceLabelPaneController controller, bool goToParent)
		{
			if (goToParent) {
				PriceLabelPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientPriceLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				PriceLabelPaneControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientPriceLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				PriceLabelPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientPriceLabelPaneControllerBase : PriceLabelPaneControllerBase, IClientPriceLabelPaneController
	{

		bool ___initialized = false;


		public ClientPriceLabelPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientPriceLabelPaneControllerProps.AddDependencies(this, false);
		}

		public ClientPriceLabelPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientPriceLabelPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientPriceLabelPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientPriceLabelPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientPriceLabelPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientPriceLabelPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientPriceLabelPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IClientChartSectionPanelController Parent
		{
			get {
				return (IClientChartSectionPanelController) ((IPriceLabelPaneController)this).Parent;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientPriceLabelPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientPriceLabelPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
