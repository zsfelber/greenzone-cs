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
		public static bool RmiGetProperty(INormalSeriesManagerCache controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (SeriesManagerCacheProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case NormalSeriesManagerCacheProps.PROPERTY_27_SERIESRUNTIME_ID:
					value = controller.SeriesRuntime;
					return true;
				case NormalSeriesManagerCacheProps.PROPERTY_28_LASTUPDATEDFILEOFFSET_ID:
					value = controller.LastUpdatedFileOffset;
					return true;
				case NormalSeriesManagerCacheProps.PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID:
					value = controller.LastUpdatedBufferLength;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INormalSeriesManagerCache controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (SeriesManagerCacheProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case NormalSeriesManagerCacheProps.PROPERTY_28_LASTUPDATEDFILEOFFSET_ID:
					controller.LastUpdatedFileOffset = (Int64) value;
					return true;
				case NormalSeriesManagerCacheProps.PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID:
					controller.LastUpdatedBufferLength = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(INormalSeriesManagerCache controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.Initialize(controller, buffer, true);
			}
			controller.SeriesRuntime = (ITimeSeriesRuntime) buffer.ChangedProps[NormalSeriesManagerCacheProps.PROPERTY_27_SERIESRUNTIME_ID];
		}

		public static void AddDependencies(INormalSeriesManagerCache controller, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.AddDependencies(controller, true);
			}
			controller.Dependencies.Add(controller.SeriesRuntime);
		}

		public static void SerializationRead(INormalSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.SerializationRead(controller, info, context, true);
			}
			controller.SeriesRuntime = (ITimeSeriesRuntime) info.GetValue("SeriesRuntime", typeof(ITimeSeriesRuntime));
			controller.LastUpdatedFileOffset = (Int64) info.GetValue("LastUpdatedFileOffset", typeof(Int64));
			controller.LastUpdatedBufferLength = (Int32) info.GetValue("LastUpdatedBufferLength", typeof(Int32));
		}

		public static void SerializationWrite(INormalSeriesManagerCache controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SeriesManagerCacheProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("SeriesRuntime", controller.SeriesRuntime);
			info.AddValue("LastUpdatedFileOffset", controller.LastUpdatedFileOffset);
			info.AddValue("LastUpdatedBufferLength", controller.LastUpdatedBufferLength);
		}

	}
	public abstract class NormalSeriesManagerCacheBase : SeriesManagerCacheEx, INormalSeriesManagerCache
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler INormalSeriesManagerCache_LastUpdatedFileOffset_Changed;
		public event PropertyChangedEventHandler INormalSeriesManagerCache_LastUpdatedBufferLength_Changed;

		public NormalSeriesManagerCacheBase(GreenRmiManager rmiManager, ISeriesManagerRuntime parent, SymbolPeriodId symbolPeriod)
			: base(rmiManager, parent, symbolPeriod)
		{
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public NormalSeriesManagerCacheBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public NormalSeriesManagerCacheBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NormalSeriesManagerCacheProps.Initialize(this, buffer, false);
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this, false);
		}

		protected NormalSeriesManagerCacheBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NormalSeriesManagerCacheProps.SerializationRead(this, info, context, false);
			___initialized = true;
			NormalSeriesManagerCacheProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NormalSeriesManagerCacheProps.SerializationWrite(this, info, context, false);
		}

		public abstract void Load(datetime focusedTime);

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		public abstract void fixSeriesRange(ref SeriesRange seriesRange);


		ITimeSeriesRuntime _INormalSeriesManagerCache_SeriesRuntime;
		public ITimeSeriesRuntime SeriesRuntime
		{
			get {
				return _INormalSeriesManagerCache_SeriesRuntime;
			}
			set {
				if (!___initialized) {
					_INormalSeriesManagerCache_SeriesRuntime= value;
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

		Int64 _INormalSeriesManagerCache_LastUpdatedFileOffset;
		public Int64 LastUpdatedFileOffset
		{
			get {
				return _INormalSeriesManagerCache_LastUpdatedFileOffset;
			}
			set {
				if (_INormalSeriesManagerCache_LastUpdatedFileOffset != value) {
					_INormalSeriesManagerCache_LastUpdatedFileOffset= value;
					changed[NormalSeriesManagerCacheProps.PROPERTY_28_LASTUPDATEDFILEOFFSET_ID] = true;
					if (INormalSeriesManagerCache_LastUpdatedFileOffset_Changed != null)
						INormalSeriesManagerCache_LastUpdatedFileOffset_Changed(this, new PropertyChangedEventArgs("LastUpdatedFileOffset", value));
				}
			}
		}

		Int32 _INormalSeriesManagerCache_LastUpdatedBufferLength;
		public Int32 LastUpdatedBufferLength
		{
			get {
				return _INormalSeriesManagerCache_LastUpdatedBufferLength;
			}
			set {
				if (_INormalSeriesManagerCache_LastUpdatedBufferLength != value) {
					_INormalSeriesManagerCache_LastUpdatedBufferLength= value;
					changed[NormalSeriesManagerCacheProps.PROPERTY_29_LASTUPDATEDBUFFERLENGTH_ID] = true;
					if (INormalSeriesManagerCache_LastUpdatedBufferLength_Changed != null)
						INormalSeriesManagerCache_LastUpdatedBufferLength_Changed(this, new PropertyChangedEventArgs("LastUpdatedBufferLength", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (NormalSeriesManagerCacheProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (NormalSeriesManagerCacheProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
