using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientChartCursorRuntimeProps
	{
		public static bool RmiGetProperty(IClientChartCursorRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartCursorRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IClientChartCursorRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ChartCursorRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartCursorRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IClientChartCursorRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IClientChartCursorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IClientChartCursorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ChartCursorRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ClientChartCursorRuntimeBase : ChartCursorRuntimeEx, IClientChartCursorRuntime
	{

		bool ___initialized = false;


		public ClientChartCursorRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ClientChartCursorRuntimeProps.AddDependencies(this, false);
		}

		public ClientChartCursorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartCursorRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientChartCursorRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartCursorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartCursorRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientChartCursorRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartCursorRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);


		public new virtual IClientChartRuntime Parent
		{
			get {
				return (IClientChartRuntime) ((IChartCursorRuntime)this).Parent;
			}
			set {
				((IChartCursorRuntime)this).Parent = value;
			}
		}

		public virtual datetime ParentScrolledBarTime
		{
			get {
				return Parent.ParentScrolledBarTime;
			}
			set {
				Parent.ParentScrolledBarTime = value;
			}
		}

		// parent property type is the same, no property generated : 
		// SeriesRange  SeriesRange
		// in parents : IChartCursorRuntime

		public virtual Boolean AutoSeriesRange
		{
			get {
				return Parent.AutoSeriesRange;
			}
		}

		public virtual Boolean IsCursorBarConnected
		{
			get {
				return true;
			}
		}

		public virtual Int32 ParentCursorPosition
		{
			get {
				return 0; //dummy property
			}
			set {
				//dummy property
			}
		}

		public virtual Int32 CursorPosition
		{
			get {
				return 0; //dummy property
			}
			set {
				//dummy property
			}
		}

		public virtual Int64 RecordCount
		{
			get {
				return SeriesCache.RecordCount;
			}
		}

		public virtual Int64 TotalFileOffset
		{
			get {
				return SeriesCache.TotalFileOffset;
			}
		}

		public virtual datetime From
		{
			get {
				return Parent.From;
			}
		}

		public virtual datetime To
		{
			get {
				return Parent.To;
			}
		}

		public virtual Double Point
		{
			get {
				return Parent.Point;
			}
		}

		public virtual Int32 Digits
		{
			get {
				return Parent.Digits;
			}
		}

		public virtual String SymbolFormat
		{
			get {
				return Parent.SymbolFormat;
			}
		}

		public virtual LArr sLTime
		{
			get {
				return SeriesCache.sLTime;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartCursorRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientChartCursorRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
