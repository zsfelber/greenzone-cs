using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class CursorChartControllerProps
	{
		public const int PROPERTY_29_CHARTGROUPPANEL_ID = 29;
		public const int PROPERTY_30_CHARTPANEL_ID = 30;
		public static bool RmiGetProperty(ICursorChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case CursorChartControllerProps.PROPERTY_29_CHARTGROUPPANEL_ID:
					value = controller.ChartGroupPanel;
					return true;
				case CursorChartControllerProps.PROPERTY_30_CHARTPANEL_ID:
					value = controller.ChartPanel;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ICursorChartController controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(ICursorChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.Initialize(controller, buffer, true);
			}
			controller.ChartGroupPanel = (IChartGroupController) buffer.ChangedProps[CursorChartControllerProps.PROPERTY_29_CHARTGROUPPANEL_ID];
			controller.ChartPanel = (IChartViewController) buffer.ChangedProps[CursorChartControllerProps.PROPERTY_30_CHARTPANEL_ID];
		}

		public static void AddDependencies(ICursorChartController controller, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.ChartGroupPanel);
			controller.Dependencies.Add(controller.ChartPanel);
		}

		public static void SerializationRead(ICursorChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationRead(controller, info, context, true);
			}
			controller.ChartGroupPanel = (IChartGroupController) info.GetValue("ChartGroupPanel", typeof(IChartGroupController));
			controller.ChartPanel = (IChartViewController) info.GetValue("ChartPanel", typeof(IChartViewController));
		}

		public static void SerializationWrite(ICursorChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("ChartGroupPanel", controller.ChartGroupPanel);
			info.AddValue("ChartPanel", controller.ChartPanel);
		}

	}
	public abstract class CursorChartControllerBase : ChartControllerBase, ICursorChartController
	{

		bool ___initialized = false;


		public CursorChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			CursorChartControllerProps.AddDependencies(this, false);
		}

		public CursorChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			CursorChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			CursorChartControllerProps.AddDependencies(this, false);
		}

		protected CursorChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			CursorChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			CursorChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			CursorChartControllerProps.SerializationWrite(this, info, context, false);
		}


		IChartGroupController _ICursorChartController_ChartGroupPanel;
		public IChartGroupController ChartGroupPanel
		{
			get {
				return _ICursorChartController_ChartGroupPanel;
			}
			set {
				if (!___initialized) {
					_ICursorChartController_ChartGroupPanel= value;
					changed[CursorChartControllerProps.PROPERTY_29_CHARTGROUPPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartViewController _ICursorChartController_ChartPanel;
		public IChartViewController ChartPanel
		{
			get {
				return _ICursorChartController_ChartPanel;
			}
			set {
				if (!___initialized) {
					_ICursorChartController_ChartPanel= value;
					changed[CursorChartControllerProps.PROPERTY_30_CHARTPANEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IChartCursorRuntime CursorRuntime
		{
			get {
				return ChartRuntime.CursorRuntime;
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
			if (CursorChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (CursorChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
