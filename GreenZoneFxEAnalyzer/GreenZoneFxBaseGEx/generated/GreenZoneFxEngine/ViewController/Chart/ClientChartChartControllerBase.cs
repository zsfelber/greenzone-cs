using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ClientChartChartControllerProps
	{
		public static bool RmiGetProperty(IClientChartChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ChartChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ChartChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.Initialize(controller, buffer, true);
			}
			ChartChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientChartChartController controller, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.AddDependencies(controller, true);
			}
			ChartChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientChartChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.SerializationRead(controller, info, context, true);
			}
			ChartChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientChartChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			ChartChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientChartChartControllerBase : ClientChartControllerEx, IClientChartChartController
	{

		bool ___initialized = false;



		public ClientChartChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ClientChartChartControllerProps.AddDependencies(this, false);
		}

		public ClientChartChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartChartControllerProps.SerializationWrite(this, info, context, false);
		}

		public abstract void FindPriceMinMax(ref Double min, ref Double max);



		public virtual IClientChartGroupController ChartGroupPanel
		{
			get {
				return (IClientChartGroupController) ((IChartChartController)this).ChartGroupPanel;
			}
			set {
				((IChartChartController)this).ChartGroupPanel = value;
			}
		}

		public virtual IClientChartViewController ChartPanel
		{
			get {
				return (IClientChartViewController) ((IChartChartController)this).ChartPanel;
			}
			set {
				((IChartChartController)this).ChartPanel = value;
			}
		}

		public virtual IClientChartRuntime ChartRuntime
		{
			get {
				return (IClientChartRuntime)Owner;
			}
		}

		public virtual IChartGroupRuntime ChartGroupRuntime
		{
			get {
				return ChartRuntime.Group;
			}
		}

		// WARNING Property duplication : ChartRuntime

		IChartRuntime IChartChartController.ChartRuntime
		{
			get {
				return (IChartRuntime)Owner;
			}
		}

		// WARNING Property duplication : ChartPanel

		IChartViewController _IChartChartController_ChartPanel;
		IChartViewController IChartChartController.ChartPanel
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

		// WARNING Property duplication : ChartGroupPanel

		IChartGroupController _IChartChartController_ChartGroupPanel;
		IChartGroupController IChartChartController.ChartGroupPanel
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
