using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerChartRuntimeProps
	{
		public static bool RmiGetProperty(IServerChartRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerChartRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerChartRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerChartRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerChartRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerChartRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerChartRuntimeBase : ChartRuntimeBase, IServerChartRuntime
	{

		bool ___initialized = false;


		public ServerChartRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerChartRuntimeProps.AddDependencies(this, false);
		}

		public ServerChartRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerChartRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerChartRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerChartRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerChartRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerChartRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerChartRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void AddIndicator(IServerIndicatorRuntime indicatorRuntime);

		public abstract void ReplaceIndicator(IndicatorId id0, IServerIndicatorRuntime indicatorRuntime);

		public abstract IIndicatorRuntime RemoveIndicator(IndicatorId id0);

		public abstract void Update(symbol _symbol, TimePeriodConst _period);


		public new virtual IServerSymbolRuntime SymbolRuntime
		{
			get {
				return (IServerSymbolRuntime) ((IChartRuntime)this).SymbolRuntime;
			}
			set {
				((IChartRuntime)this).SymbolRuntime = value;
			}
		}

		public new virtual IServerSymbolContext SymbolContext
		{
			get {
				return (IServerSymbolContext) ((IChartRuntime)this).SymbolContext;
			}
		}

		public new virtual IServerSeriesManagerRuntime GuiSeriesManager
		{
			get {
				return (IServerSeriesManagerRuntime) ((IChartRuntime)this).GuiSeriesManager;
			}
			set {
				((IChartRuntime)this).GuiSeriesManager = value;
			}
		}

		public new virtual IServerChartCursorRuntime CursorRuntime
		{
			get {
				return (IServerChartCursorRuntime) ((IChartRuntime)this).CursorRuntime;
			}
			set {
				((IChartRuntime)this).CursorRuntime = value;
			}
		}

		public new virtual IServerScriptRuntime Script
		{
			get {
				return (IServerScriptRuntime) ((IChartRuntime)this).Script;
			}
			set {
				((IChartRuntime)this).Script = value;
			}
		}

		public new virtual IServerExpertRuntime Expert
		{
			get {
				return (IServerExpertRuntime) ((IChartRuntime)this).Expert;
			}
			set {
				((IChartRuntime)this).Expert = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerChartRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerChartRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
