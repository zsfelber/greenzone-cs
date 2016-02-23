using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientChartRuntimeProps
	{
		public static bool RmiGetProperty(IClientChartRuntime controller, int propertyId, out object value, bool goToParent)
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
		public static bool RmiSetProperty(IClientChartRuntime controller, int propertyId, object value, bool goToParent)
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
		public static void Initialize(IClientChartRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientChartRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientChartRuntimeBase : ChartRuntimeBase, IClientChartRuntime
	{

		bool ___initialized = false;


		public ClientChartRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ClientChartRuntimeProps.AddDependencies(this, false);
		}

		public ClientChartRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);


		public new virtual IClientChartGroupRuntime Group
		{
			get {
				return (IClientChartGroupRuntime) ((IChartRuntime)this).Group;
			}
			set {
				((IChartRuntime)this).Group = value;
			}
		}

		public new virtual IClientChartCursorRuntime CursorRuntime
		{
			get {
				return (IClientChartCursorRuntime) ((IChartRuntime)this).CursorRuntime;
			}
			set {
				((IChartRuntime)this).CursorRuntime = value;
			}
		}

		public new virtual IClientScriptRuntime Script
		{
			get {
				return (IClientScriptRuntime) ((IChartRuntime)this).Script;
			}
			set {
				((IChartRuntime)this).Script = value;
			}
		}

		public new virtual IClientExpertRuntime Expert
		{
			get {
				return (IClientExpertRuntime) ((IChartRuntime)this).Expert;
			}
			set {
				((IChartRuntime)this).Expert = value;
			}
		}

		public abstract datetime ParentScrolledBarTime
		{
			get ;
			set ;
		}

		public virtual Boolean IsCursorBarConnected
		{
			get {
				return Session.IsCursorBarConnected;
			}
		}

		public abstract Int32 ParentCursorPosition
		{
			get ;
			set ;
		}

		public abstract Int32 CursorPosition
		{
			get ;
			set ;
		}

		public virtual Int64 RecordCount
		{
			get {
				return GuiSeriesManager.DefaultCache.RecordCount;
			}
		}

		public virtual Int64 TotalFileOffset
		{
			get {
				return GuiSeriesManager.DefaultCache.TotalFileOffset;
			}
		}

		public virtual Boolean AutoSeriesRange
		{
			get {
				return Session.AutoSeriesRange;
			}
		}

		public virtual datetime From
		{
			get {
				return Session.From;
			}
		}

		public virtual datetime To
		{
			get {
				return Session.To;
			}
		}

		public virtual ISeriesManagerCache SeriesCache
		{
			get {
				return GuiSeriesManager.DefaultCache;
			}
		}

		public virtual Double Point
		{
			get {
				return SymbolContext.Point;
			}
		}

		public virtual Int32 Digits
		{
			get {
				return SymbolContext.Digits;
			}
		}

		public virtual LArr sLTime
		{
			get {
				return GuiSeriesManager.DefaultCache.sLTime;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
