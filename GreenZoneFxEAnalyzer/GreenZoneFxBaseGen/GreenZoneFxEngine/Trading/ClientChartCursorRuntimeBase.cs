using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientChartCursorRuntimeProps
	{
		public const int PROPERTY_3_SERIESRANGE_ID = 3;
		public static bool RmiGetProperty(IClientChartCursorRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_3_SERIESRANGE_ID:
					value = controller.SeriesRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartCursorRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_3_SERIESRANGE_ID:
					controller.SeriesRange = (SeriesRange) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartCursorRuntime controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IClientChartCursorRuntime controller)
		{
		}

		public static void SerializationRead(IClientChartCursorRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.SeriesRange = (SeriesRange) info.GetValue("SeriesRange", typeof(SeriesRange));
		}

		public static void SerializationWrite(IClientChartCursorRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SeriesRange", controller.SeriesRange);
		}

	}
	public abstract class ClientChartCursorRuntimeBase : ChartCursorRuntimeBase, IClientChartCursorRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SeriesRangeChanged;

		public ClientChartCursorRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ClientChartCursorRuntimeProps.AddDependencies(this);
		}

		public ClientChartCursorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartCursorRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ClientChartCursorRuntimeProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartCursorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartCursorRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ClientChartCursorRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartCursorRuntimeProps.SerializationWrite(this, info, context);
		}

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		public abstract void UpdateSeriesManager();

		public abstract void UpdateSeriesRange();

		public new IClientChartRuntime Parent
		{
			get {
				return (IClientChartRuntime) base.Parent;
			}
			set {
				base.Parent = (IChartRuntime) value;
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

		SeriesRange seriesRange;
		public SeriesRange SeriesRange
		{
			get {
				return seriesRange;
			}
			set {
				if (seriesRange != value) {
					seriesRange= value;
					changed[ClientChartCursorRuntimeProps.PROPERTY_3_SERIESRANGE_ID] = true;
					if (SeriesRangeChanged != null)
						SeriesRangeChanged(this, new PropertyChangedEventArgs("SeriesRange", value));
				}
			}
		}

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

		public virtual ILArr sLTime
		{
			get {
				return SeriesCache.sLTime;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartCursorRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ClientChartCursorRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
