using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class NormalSeriesManagerCacheProps
	{
		public const int PROPERTY_27_SERIESRUNTIME_ID = 27;
		public const int PROPERTY_28_LASTUPDATEDFILEOFFSET_ID = 28;
		public const int PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID = 29;
		public static bool RmiGetProperty(INormalSeriesManagerCache controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_27_SERIESRUNTIME_ID:
					value = controller.SeriesRuntime;
					return true;
				case PROPERTY_28_LASTUPDATEDFILEOFFSET_ID:
					value = controller.LastUpdatedFileOffset;
					return true;
				case PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID:
					value = controller.LastUpdatedBufferLength;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INormalSeriesManagerCache controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_28_LASTUPDATEDFILEOFFSET_ID:
					controller.LastUpdatedFileOffset = (Int64) value;
					return true;
				case PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID:
					controller.LastUpdatedBufferLength = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(INormalSeriesManagerCache controller, GreenRmiObjectBuffer buffer)
		{
			controller.SeriesRuntime = (ITimeSeriesRuntime) buffer.ChangedProps[NormalSeriesManagerCacheProps.PROPERTY_27_SERIESRUNTIME_ID];
		}

		public static void AddDependencies(INormalSeriesManagerCache controller)
		{
			controller.Dependencies.Add(controller.SeriesRuntime);
		}

		public static void SerializationRead(INormalSeriesManagerCache controller, SerializationInfo info, StreamingContext context)
		{
			controller.SeriesRuntime = (ITimeSeriesRuntime) info.GetValue("SeriesRuntime", typeof(ITimeSeriesRuntime));
			controller.LastUpdatedFileOffset = (Int64) info.GetValue("LastUpdatedFileOffset", typeof(Int64));
			controller.LastUpdatedBufferLength = (Int32) info.GetValue("LastUpdatedBufferLength", typeof(Int32));
		}

		public static void SerializationWrite(INormalSeriesManagerCache controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SeriesRuntime", controller.SeriesRuntime);
			info.AddValue("LastUpdatedFileOffset", controller.LastUpdatedFileOffset);
			info.AddValue("LastUpdatedBufferLength", controller.LastUpdatedBufferLength);
		}

	}
	public abstract class NormalSeriesManagerCacheBase : SeriesManagerCacheBase, INormalSeriesManagerCache
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler LastUpdatedFileOffsetChanged;
		public event PropertyChangedEventHandler LastUpdatedBufferLengthChanged;

		public NormalSeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this);
		}

		public NormalSeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalSeriesManagerCacheProps.Initialize(this, buffer);
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this);
		}

		protected NormalSeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalSeriesManagerCacheProps.SerializationRead(this, info, context);
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalSeriesManagerCacheProps.SerializationWrite(this, info, context);
		}

		public abstract void Load(datetime focusedTime);

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		public abstract void fixSeriesRange(ref SeriesRange seriesRange);

		ITimeSeriesRuntime seriesRuntime;
		public ITimeSeriesRuntime SeriesRuntime
		{
			get {
				return seriesRuntime;
			}
			set {
				if (!___initialized) {
					seriesRuntime= value;
					changed[NormalSeriesManagerCacheProps.PROPERTY_27_SERIESRUNTIME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public override Int32 Bars
		{
			get {
				return SeriesRuntime.IndCount;
			}
		}

		public virtual Int32 IndOffset
		{
			get {
				return SeriesRuntime.IndOffset;
			}
		}

		public virtual Int32 IndCount
		{
			get {
				return SeriesRuntime.IndCount;
			}
		}

		public virtual Int64 FileOffset
		{
			get {
				return SeriesRuntime.FileOffset;
			}
		}

		public virtual Int64 TotalFileOffset
		{
			get {
				return SeriesRuntime.TotalFileOffset;
			}
		}

		public virtual Int32 BufferFromIndex
		{
			get {
				return 0;
			}
		}

		public virtual Int64 RecordCount
		{
			get {
				return SeriesRuntime.RecordCount;
			}
		}

		public virtual datetime From
		{
			get {
				return SeriesRuntime.From;
			}
		}

		public virtual datetime To
		{
			get {
				return SeriesRuntime.To;
			}
		}

		Int64 lastUpdatedFileOffset;
		public Int64 LastUpdatedFileOffset
		{
			get {
				return lastUpdatedFileOffset;
			}
			set {
				if (lastUpdatedFileOffset != value) {
					lastUpdatedFileOffset= value;
					changed[NormalSeriesManagerCacheProps.PROPERTY_28_LASTUPDATEDFILEOFFSET_ID] = true;
					if (LastUpdatedFileOffsetChanged != null)
						LastUpdatedFileOffsetChanged(this, new PropertyChangedEventArgs("LastUpdatedFileOffset", value));
				}
			}
		}

		Int32 lastUpdatedBufferLength;
		public Int32 LastUpdatedBufferLength
		{
			get {
				return lastUpdatedBufferLength;
			}
			set {
				if (lastUpdatedBufferLength != value) {
					lastUpdatedBufferLength= value;
					changed[NormalSeriesManagerCacheProps.PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID] = true;
					if (LastUpdatedBufferLengthChanged != null)
						LastUpdatedBufferLengthChanged(this, new PropertyChangedEventArgs("LastUpdatedBufferLength", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalSeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!NormalSeriesManagerCacheProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
