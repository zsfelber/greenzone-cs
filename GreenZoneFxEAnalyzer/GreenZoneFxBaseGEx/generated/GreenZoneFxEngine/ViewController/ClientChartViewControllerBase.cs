using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class ClientChartViewControllerProps
	{
		public static bool RmiGetProperty(IClientChartViewController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartViewControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientChartViewController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartViewControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartViewController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartViewControllerProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartViewController controller, bool goToParent)
		{
			if (goToParent) {
				ChartViewControllerProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartViewController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartViewControllerProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientChartViewController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartViewControllerProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientChartViewControllerBase : ChartViewControllerBase, IClientChartViewController
	{

		bool ___initialized = false;


		public ClientChartViewControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartViewControllerProps.AddDependencies(this, false);
		}

		public ClientChartViewControllerBase(GreenRmiManager rmiManager, Controller parent, String text)
			: base(rmiManager, parent, text)
		{
			___initialized = true;
			ClientChartViewControllerProps.AddDependencies(this, false);
		}

		public ClientChartViewControllerBase(GreenRmiManager rmiManager, Controller parent, String text, Int32 image)
			: base(rmiManager, parent, text, image)
		{
			___initialized = true;
			ClientChartViewControllerProps.AddDependencies(this, false);
		}

		public ClientChartViewControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartViewControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartViewControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartViewControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartViewControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartViewControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartViewControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void UpdateChartAndCursor();

		public abstract void UpdateCursor();

		public abstract void UpdateSeries();


		public new virtual IClientNormalChartController Chart1
		{
			get {
				return (IClientNormalChartController) ((IChartViewController)this).Chart1;
			}
			set {
				((IChartViewController)this).Chart1 = value;
			}
		}

		public new virtual IClientCursorChartController CursorChart
		{
			get {
				return (IClientCursorChartController) ((IChartViewController)this).CursorChart;
			}
			set {
				((IChartViewController)this).CursorChart = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartViewControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartViewControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
