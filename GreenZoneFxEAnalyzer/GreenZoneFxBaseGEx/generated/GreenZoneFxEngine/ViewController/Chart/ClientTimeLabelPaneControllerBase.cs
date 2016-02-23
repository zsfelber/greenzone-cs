using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientTimeLabelPaneControllerProps
	{
		public static bool RmiGetProperty(IClientTimeLabelPaneController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeLabelPaneControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientTimeLabelPaneController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (TimeLabelPaneControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientTimeLabelPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				TimeLabelPaneControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientTimeLabelPaneController controller, bool goToParent)
		{
			if (goToParent) {
				TimeLabelPaneControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientTimeLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeLabelPaneControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientTimeLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				TimeLabelPaneControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientTimeLabelPaneControllerBase : TimeLabelPaneControllerBase, IClientTimeLabelPaneController
	{

		bool ___initialized = false;


		public ClientTimeLabelPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientTimeLabelPaneControllerProps.AddDependencies(this, false);
		}

		public ClientTimeLabelPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientTimeLabelPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientTimeLabelPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientTimeLabelPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientTimeLabelPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientTimeLabelPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientTimeLabelPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IClientChartController Parent
		{
			get {
				return (IClientChartController) ((ITimeLabelPaneController)this).Parent;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientTimeLabelPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientTimeLabelPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
