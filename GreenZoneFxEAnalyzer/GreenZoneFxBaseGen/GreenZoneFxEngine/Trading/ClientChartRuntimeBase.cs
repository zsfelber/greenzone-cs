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
		public static bool RmiGetProperty(IClientChartRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartRuntime controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IClientChartRuntime controller)
		{
		}

		public static void SerializationRead(IClientChartRuntime controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IClientChartRuntime controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ClientChartRuntimeBase : ChartRuntimeBase, IClientChartRuntime
	{

		bool ___initialized = false;


		public ClientChartRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ClientChartRuntimeProps.AddDependencies(this);
		}

		public ClientChartRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ClientChartRuntimeProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ClientChartRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartRuntimeProps.SerializationWrite(this, info, context);
		}

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		public new IClientChartGroupRuntime Group
		{
			get {
				return (IClientChartGroupRuntime) base.Group;
			}
			set {
				base.Group = (IChartGroupRuntime) value;
			}
		}

		public new IClientChartCursorRuntime CursorRuntime
		{
			get {
				return (IClientChartCursorRuntime) base.CursorRuntime;
			}
			set {
				base.CursorRuntime = (IChartCursorRuntime) value;
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

		public virtual String SymbolFormat
		{
			get {
				return SymbolRuntime.SymbolFormat;
			}
		}

		public virtual ILArr sLTime
		{
			get {
				return GuiSeriesManager.DefaultCache.sLTime;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ClientChartRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
