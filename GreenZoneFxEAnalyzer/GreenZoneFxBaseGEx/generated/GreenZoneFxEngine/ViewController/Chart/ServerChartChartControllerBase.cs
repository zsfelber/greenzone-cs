using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class ServerChartChartControllerProps
	{
		public static bool RmiGetProperty(IServerChartChartController controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartControllerProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerChartChartController controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerChartControllerProps.RmiSetProperty(controller, propertyId, value, true)) {
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
		public static void Initialize(IServerChartChartController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.Initialize(controller, buffer, true);
			}
			ChartChartControllerProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerChartChartController controller, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.AddDependencies(controller, true);
			}
			ChartChartControllerProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerChartChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.SerializationRead(controller, info, context, true);
			}
			ChartChartControllerProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerChartChartController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerChartControllerProps.SerializationWrite(controller, info, context, true);
			}
			ChartChartControllerProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerChartChartControllerBase : ServerChartControllerEx, IServerChartChartController
	{

		bool ___initialized = false;



		public ServerChartChartControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ServerChartChartControllerProps.AddDependencies(this, false);
		}

		public ServerChartChartControllerBase(GreenRmiManager rmiManager, IMainWinTabPageController tabPanel, IServerChartOwner owner)
			: base(rmiManager, tabPanel, owner)
		{
			___initialized = true;
			ServerChartChartControllerProps.AddDependencies(this, false);
		}

		public ServerChartChartControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartChartControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartChartControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartChartControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartChartControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartChartControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartChartControllerProps.SerializationWrite(this, info, context, false);
		}



		public virtual IServerChartRuntime ChartRuntime
		{
			get {
				return (IServerChartRuntime)Owner;
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartChartControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartChartControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
