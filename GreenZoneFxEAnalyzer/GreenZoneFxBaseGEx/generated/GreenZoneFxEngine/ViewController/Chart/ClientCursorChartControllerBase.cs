using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientCursorChartControllerProps
	{
		public static bool RmiGetProperty(IClientCursorChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientCursorChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IClientCursorChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.Initialize(controller, buffer, true);
			}
			CursorChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientCursorChartController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.AddDependencies(controller, true);
			}
			CursorChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientCursorChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.SerializationRead(controller, info, context, true);
			}
			CursorChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientCursorChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			CursorChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientCursorChartControllerBase : ClientChartControllerEx, IClientCursorChartController
	{

		bool ___initialized = false;



		public ClientCursorChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientCursorChartControllerProps.AddDependencies(this, false);
		}

		public ClientCursorChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientCursorChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientCursorChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientCursorChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientCursorChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientCursorChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientCursorChartControllerProps.SerializationWrite(this, info, context, false);
		}



		public virtual IClientChartGroupController ChartGroupPanel
		{
			get {
				return (IClientChartGroupController) ((ICursorChartController)this).ChartGroupPanel;
			}
			set {
				((ICursorChartController)this).ChartGroupPanel = value;
			}
		}

		public virtual IClientChartViewController ChartPanel
		{
			get {
				return (IClientChartViewController) ((ICursorChartController)this).ChartPanel;
			}
			set {
				((ICursorChartController)this).ChartPanel = value;
			}
		}

		public virtual IClientChartCursorRuntime CursorRuntime
		{
			get {
				return ChartRuntime.CursorRuntime;
			}
		}

		public virtual IClientChartRuntime ChartRuntime
		{
			get {
				return (IClientChartRuntime)Owner;
			}
		}

		public virtual IClientChartGroupRuntime ChartGroupRuntime
		{
			get {
				return (IClientChartGroupRuntime)ChartRuntime.Group;
			}
		}

		public new virtual IClientCursorChartSectionPanelController MasterChartSectionPanel
		{
			get {
				return (IClientCursorChartSectionPanelController) ((IClientChartController)this).MasterChartSectionPanel;
			}
			set {
				((IClientChartController)this).MasterChartSectionPanel = value;
			}
		}

		// WARNING Property duplication : ChartGroupRuntime

		IChartGroupRuntime ICursorChartController.ChartGroupRuntime
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

		// WARNING Property duplication : ChartPanel

		IChartViewController _ICursorChartController_ChartPanel;
		IChartViewController ICursorChartController.ChartPanel
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

		// WARNING Property duplication : ChartGroupPanel

		IChartGroupController _ICursorChartController_ChartGroupPanel;
		IChartGroupController ICursorChartController.ChartGroupPanel
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
			if (ClientCursorChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientCursorChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
