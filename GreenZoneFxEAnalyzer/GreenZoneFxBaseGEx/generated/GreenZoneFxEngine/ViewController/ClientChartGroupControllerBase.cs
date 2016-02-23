using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class ClientChartGroupControllerProps
	{
		public static bool RmiGetProperty(IClientChartGroupController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartGroupControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientChartGroupController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartGroupControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartGroupController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartGroupControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartGroupController controller, bool goToParent)
		{
			if (goToParent) {
				ChartGroupControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartGroupController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartGroupControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientChartGroupController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartGroupControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientChartGroupControllerBase : ChartGroupControllerBase, IClientChartGroupController
	{

		bool ___initialized = false;


		public ClientChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartGroupControllerProps.AddDependencies(this, false);
		}

		public ClientChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			ClientChartGroupControllerProps.AddDependencies(this, false);
		}

		public ClientChartGroupControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			ClientChartGroupControllerProps.AddDependencies(this, false);
		}

		public ClientChartGroupControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartGroupControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartGroupControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartGroupControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartGroupControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartGroupControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartGroupControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void UpdateAllCursor(IClientChartViewController invoker);

		public abstract void UpdateAllChartAndCursor(IClientChartViewController invoker);



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartGroupControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartGroupControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
