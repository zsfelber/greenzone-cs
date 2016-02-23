using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerCursorChartControllerProps
	{
		public static bool RmiGetProperty(IServerCursorChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (CursorChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerCursorChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (CursorChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerCursorChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.Initialize(controller, buffer, true);
			}
			CursorChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerCursorChartController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.AddDependencies(controller, true);
			}
			CursorChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerCursorChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.SerializationRead(controller, info, context, true);
			}
			CursorChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerCursorChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			CursorChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerCursorChartControllerBase : ServerChartControllerEx, IServerCursorChartController
	{

		bool ___initialized = false;



		public ServerCursorChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerCursorChartControllerProps.AddDependencies(this, false);
		}

		public ServerCursorChartControllerBase(GreenRmiManager rmiManager, IMainWinTabPageController tabPanel, IServerChartOwner owner)
			: base(rmiManager, tabPanel, owner)
		{
			___initialized = true;
			ServerCursorChartControllerProps.AddDependencies(this, false);
		}

		public ServerCursorChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerCursorChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerCursorChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerCursorChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerCursorChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerCursorChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerCursorChartControllerProps.SerializationWrite(this, info, context, false);
		}



		public virtual IServerChartCursorRuntime CursorRuntime
		{
			get {
				return ChartRuntime.CursorRuntime;
			}
		}

		public virtual IServerChartRuntime ChartRuntime
		{
			get {
				return (IServerChartRuntime)Owner;
			}
		}

		public new virtual IServerCursorChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IServerCursorChartSectionPanelController) ((IServerChartController)this).MasterChartSectionPanel;
			}
			set {
				((IServerChartController)this).MasterChartSectionPanel = value;
			}
		}

		public virtual IChartGroupRuntime ChartGroupRuntime
		{
			get {
				return ChartRuntime.Group;
			}
		}

		// WARNING Property duplication : ChartRuntime

		IChartRuntime ICursorChartController.ChartRuntime
		{
			get {
				return (IChartRuntime)Owner;
			}
		}

		// WARNING Property duplication : CursorRuntime

		IChartCursorRuntime ICursorChartController.CursorRuntime
		{
			get {
				return ChartRuntime.CursorRuntime;
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerCursorChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerCursorChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
