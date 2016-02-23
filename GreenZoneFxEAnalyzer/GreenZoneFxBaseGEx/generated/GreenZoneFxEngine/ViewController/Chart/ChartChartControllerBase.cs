using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ChartChartControllerProps
	{
		public const int PROPERTY_29_CHARTGROUPPANEL_ID = 29;
		public const int PROPERTY_30_CHARTPANEL_ID = 30;
		public static bool RmiGetProperty(IChartChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ChartChartControllerProps.PROPERTY_29_CHARTGROUPPANEL_ID:
					value = controller.ChartGroupPanel;
					return true;
				case ChartChartControllerProps.PROPERTY_30_CHARTPANEL_ID:
					value = controller.ChartPanel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.Initialize(controller, buffer, true);
			}
			controller.ChartGroupPanel = (IChartGroupController) buffer.ChangedProps[ChartChartControllerProps.PROPERTY_29_CHARTGROUPPANEL_ID];
			controller.ChartPanel = (IChartViewController) buffer.ChangedProps[ChartChartControllerProps.PROPERTY_30_CHARTPANEL_ID];
		}

		public static void AddDependencies(IChartChartController controller, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.ChartGroupPanel);
			controller.Dependencies.Add(controller.ChartPanel);
		}

		public static void SerializationRead(IChartChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.ChartGroupPanel = (IChartGroupController) info.GetValue("ChartGroupPanel", typeof(IChartGroupController));
			controller.ChartPanel = (IChartViewController) info.GetValue("ChartPanel", typeof(IChartViewController));
		}

		public static void SerializationWrite(IChartChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("ChartGroupPanel", controller.ChartGroupPanel);
			info.AddValue("ChartPanel", controller.ChartPanel);
		}

	}
	public abstract class ChartChartControllerBase : ChartControllerBase, IChartChartController
	{

		bool ___initialized = false;


		public ChartChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ChartChartControllerProps.AddDependencies(this, false);
		}

		public ChartChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartChartControllerProps.AddDependencies(this, false);
		}

		protected ChartChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartChartControllerProps.SerializationWrite(this, info, context, false);
		}


		IChartGroupController _IChartChartController_ChartGroupPanel;
		public IChartGroupController ChartGroupPanel
		{
			get {
				return _IChartChartController_ChartGroupPanel;
			}
			set {
				if (!___initialized) {
					_IChartChartController_ChartGroupPanel= value;
					changed[ChartChartControllerProps.PROPERTY_29_CHARTGROUPPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartViewController _IChartChartController_ChartPanel;
		public IChartViewController ChartPanel
		{
			get {
				return _IChartChartController_ChartPanel;
			}
			set {
				if (!___initialized) {
					_IChartChartController_ChartPanel= value;
					changed[ChartChartControllerProps.PROPERTY_30_CHARTPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IChartRuntime ChartRuntime
		{
			get {
				return (IChartRuntime)Owner;
			}
		}

		public virtual IChartGroupRuntime ChartGroupRuntime
		{
			get {
				return ChartRuntime.Group;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
