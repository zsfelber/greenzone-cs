using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class TimeSeriesRuntimeProps
	{
		public const int PROPERTY_1_PARENT_ID = 1;
		public const int PROPERTY_2_PERIOD_ID = 2;
		public const int PROPERTY_3_SYMBOL_ID = 3;
		public const int PROPERTY_4_ONLINE_ID = 4;
		public const int PROPERTY_5_INPUTFILEISREAD_ID = 5;
		public const int PROPERTY_6_VOLUME_ID = 6;
		public const int PROPERTY_7_BEGIN_TIME_ID = 7;
		public const int PROPERTY_8_HEADERLEN_ID = 8;
		public const int PROPERTY_9_RECORDLEN_ID = 9;
		public const int PROPERTY_10_FROM_ID = 10;
		public const int PROPERTY_11_TO_ID = 11;
		public const int PROPERTY_12_FILEOFFSET_ID = 12;
		public const int PROPERTY_13_FILEOFFSETTO_ID = 13;
		public const int PROPERTY_14_INDOFFSET_ID = 14;
		public const int PROPERTY_15_INDCOUNT_ID = 15;
		public const int PROPERTY_16_RECORDCOUNT_ID = 16;
		public static bool RmiGetProperty(ITimeSeriesRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case TimeSeriesRuntimeProps.PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_2_PERIOD_ID:
					value = controller.Period;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_3_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_4_ONLINE_ID:
					value = controller.Online;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_5_INPUTFILEISREAD_ID:
					value = controller.InputFileIsRead;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_6_VOLUME_ID:
					value = controller.Volume;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_7_BEGIN_TIME_ID:
					value = controller.Begin_time;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_8_HEADERLEN_ID:
					value = controller.HeaderLen;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_9_RECORDLEN_ID:
					value = controller.RecordLen;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_10_FROM_ID:
					value = controller.From;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_11_TO_ID:
					value = controller.To;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_12_FILEOFFSET_ID:
					value = controller.FileOffset;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_13_FILEOFFSETTO_ID:
					value = controller.FileOffsetTo;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_14_INDOFFSET_ID:
					value = controller.IndOffset;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_15_INDCOUNT_ID:
					value = controller.IndCount;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_16_RECORDCOUNT_ID:
					value = controller.RecordCount;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITimeSeriesRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case TimeSeriesRuntimeProps.PROPERTY_5_INPUTFILEISREAD_ID:
					controller.InputFileIsRead = (Boolean) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_6_VOLUME_ID:
					controller.Volume = (DArr) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_7_BEGIN_TIME_ID:
					controller.Begin_time = (LArr) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_12_FILEOFFSET_ID:
					controller.FileOffset = (Int64) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_13_FILEOFFSETTO_ID:
					controller.FileOffsetTo = (Int64) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_14_INDOFFSET_ID:
					controller.IndOffset = (Int32) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_15_INDCOUNT_ID:
					controller.IndCount = (Int32) value;
					return true;
				case TimeSeriesRuntimeProps.PROPERTY_16_RECORDCOUNT_ID:
					controller.RecordCount = (Int64) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITimeSeriesRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Parent = (ISymbolRuntime) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_1_PARENT_ID];
			controller.Period = (TimePeriodConst) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_2_PERIOD_ID];
			controller.Symbol = (symbol) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_3_SYMBOL_ID];
			controller.Online = (Boolean) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_4_ONLINE_ID];
			controller.HeaderLen = (Int32) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_8_HEADERLEN_ID];
			controller.RecordLen = (Int32) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_9_RECORDLEN_ID];
			controller.From = (datetime) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_10_FROM_ID];
			controller.To = (datetime) buffer.ChangedProps[TimeSeriesRuntimeProps.PROPERTY_11_TO_ID];
		}

		public static void AddDependencies(ITimeSeriesRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Parent);
		}

		public static void SerializationRead(ITimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Parent = (ISymbolRuntime) info.GetValue("Parent", typeof(ISymbolRuntime));
			controller.Period = (TimePeriodConst) info.GetValue("Period", typeof(TimePeriodConst));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Online = (Boolean) info.GetValue("Online", typeof(Boolean));
			controller.InputFileIsRead = (Boolean) info.GetValue("InputFileIsRead", typeof(Boolean));
			controller.Volume = (DArr) info.GetValue("Volume", typeof(DArr));
			controller.Begin_time = (LArr) info.GetValue("Begin_time", typeof(LArr));
			controller.HeaderLen = (Int32) info.GetValue("HeaderLen", typeof(Int32));
			controller.RecordLen = (Int32) info.GetValue("RecordLen", typeof(Int32));
			controller.From = (datetime) info.GetValue("From", typeof(datetime));
			controller.To = (datetime) info.GetValue("To", typeof(datetime));
			controller.FileOffset = (Int64) info.GetValue("FileOffset", typeof(Int64));
			controller.FileOffsetTo = (Int64) info.GetValue("FileOffsetTo", typeof(Int64));
			controller.IndOffset = (Int32) info.GetValue("IndOffset", typeof(Int32));
			controller.IndCount = (Int32) info.GetValue("IndCount", typeof(Int32));
			controller.RecordCount = (Int64) info.GetValue("RecordCount", typeof(Int64));
		}

		public static void SerializationWrite(ITimeSeriesRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("Period", controller.Period);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Online", controller.Online);
			info.AddValue("InputFileIsRead", controller.InputFileIsRead);
			info.AddValue("Volume", controller.Volume);
			info.AddValue("Begin_time", controller.Begin_time);
			info.AddValue("HeaderLen", controller.HeaderLen);
			info.AddValue("RecordLen", controller.RecordLen);
			info.AddValue("From", controller.From);
			info.AddValue("To", controller.To);
			info.AddValue("FileOffset", controller.FileOffset);
			info.AddValue("FileOffsetTo", controller.FileOffsetTo);
			info.AddValue("IndOffset", controller.IndOffset);
			info.AddValue("IndCount", controller.IndCount);
			info.AddValue("RecordCount", controller.RecordCount);
		}

	}
	public abstract class TimeSeriesRuntimeBase : RmiBase, ITimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ITimeSeriesRuntime_InputFileIsRead_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_Volume_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_Begin_time_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_FileOffset_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_FileOffsetTo_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_IndOffset_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_IndCount_Changed;
		public event PropertyChangedEventHandler ITimeSeriesRuntime_RecordCount_Changed;

		public TimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			TimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public TimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TimeSeriesRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			TimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		protected TimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TimeSeriesRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			TimeSeriesRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TimeSeriesRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void Load(datetime focusedTime);

		public abstract void LoadForward(datetime focusedTime);

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);


		ISymbolRuntime _ITimeSeriesRuntime_Parent;
		public ISymbolRuntime Parent
		{
			get {
				return _ITimeSeriesRuntime_Parent;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_Parent= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TimePeriodConst _ITimeSeriesRuntime_Period;
		public TimePeriodConst Period
		{
			get {
				return _ITimeSeriesRuntime_Period;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_Period= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_2_PERIOD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol _ITimeSeriesRuntime_Symbol;
		public symbol Symbol
		{
			get {
				return _ITimeSeriesRuntime_Symbol;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_Symbol= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_3_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean _ITimeSeriesRuntime_Online;
		public Boolean Online
		{
			get {
				return _ITimeSeriesRuntime_Online;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_Online= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_4_ONLINE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean _ITimeSeriesRuntime_InputFileIsRead;
		public Boolean InputFileIsRead
		{
			get {
				return _ITimeSeriesRuntime_InputFileIsRead;
			}
			set {
				if (_ITimeSeriesRuntime_InputFileIsRead != value) {
					_ITimeSeriesRuntime_InputFileIsRead= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_5_INPUTFILEISREAD_ID] = true;
					if (ITimeSeriesRuntime_InputFileIsRead_Changed != null)
						ITimeSeriesRuntime_InputFileIsRead_Changed(this, new PropertyChangedEventArgs("InputFileIsRead", value));
				}
			}
		}

		DArr _ITimeSeriesRuntime_Volume;
		public DArr Volume
		{
			get {
				return _ITimeSeriesRuntime_Volume;
			}
			set {
				if (_ITimeSeriesRuntime_Volume != value) {
					_ITimeSeriesRuntime_Volume= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_6_VOLUME_ID] = true;
					if (ITimeSeriesRuntime_Volume_Changed != null)
						ITimeSeriesRuntime_Volume_Changed(this, new PropertyChangedEventArgs("Volume", value));
				}
			}
		}

		LArr _ITimeSeriesRuntime_Begin_time;
		public LArr Begin_time
		{
			get {
				return _ITimeSeriesRuntime_Begin_time;
			}
			set {
				if (_ITimeSeriesRuntime_Begin_time != value) {
					_ITimeSeriesRuntime_Begin_time= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_7_BEGIN_TIME_ID] = true;
					if (ITimeSeriesRuntime_Begin_time_Changed != null)
						ITimeSeriesRuntime_Begin_time_Changed(this, new PropertyChangedEventArgs("Begin_time", value));
				}
			}
		}

		Int32 _ITimeSeriesRuntime_HeaderLen;
		public Int32 HeaderLen
		{
			get {
				return _ITimeSeriesRuntime_HeaderLen;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_HeaderLen= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_8_HEADERLEN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Int32 _ITimeSeriesRuntime_RecordLen;
		public Int32 RecordLen
		{
			get {
				return _ITimeSeriesRuntime_RecordLen;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_RecordLen= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_9_RECORDLEN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		datetime _ITimeSeriesRuntime_From;
		public datetime From
		{
			get {
				return _ITimeSeriesRuntime_From;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_From= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_10_FROM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		datetime _ITimeSeriesRuntime_To;
		public datetime To
		{
			get {
				return _ITimeSeriesRuntime_To;
			}
			set {
				if (!___initialized) {
					_ITimeSeriesRuntime_To= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_11_TO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Int64 _ITimeSeriesRuntime_FileOffset;
		public Int64 FileOffset
		{
			get {
				return _ITimeSeriesRuntime_FileOffset;
			}
			set {
				if (_ITimeSeriesRuntime_FileOffset != value) {
					_ITimeSeriesRuntime_FileOffset= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_12_FILEOFFSET_ID] = true;
					if (ITimeSeriesRuntime_FileOffset_Changed != null)
						ITimeSeriesRuntime_FileOffset_Changed(this, new PropertyChangedEventArgs("FileOffset", value));
				}
			}
		}

		Int64 _ITimeSeriesRuntime_FileOffsetTo;
		public Int64 FileOffsetTo
		{
			get {
				return _ITimeSeriesRuntime_FileOffsetTo;
			}
			set {
				if (_ITimeSeriesRuntime_FileOffsetTo != value) {
					_ITimeSeriesRuntime_FileOffsetTo= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_13_FILEOFFSETTO_ID] = true;
					if (ITimeSeriesRuntime_FileOffsetTo_Changed != null)
						ITimeSeriesRuntime_FileOffsetTo_Changed(this, new PropertyChangedEventArgs("FileOffsetTo", value));
				}
			}
		}

		Int32 _ITimeSeriesRuntime_IndOffset;
		public Int32 IndOffset
		{
			get {
				return _ITimeSeriesRuntime_IndOffset;
			}
			set {
				if (_ITimeSeriesRuntime_IndOffset != value) {
					_ITimeSeriesRuntime_IndOffset= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_14_INDOFFSET_ID] = true;
					if (ITimeSeriesRuntime_IndOffset_Changed != null)
						ITimeSeriesRuntime_IndOffset_Changed(this, new PropertyChangedEventArgs("IndOffset", value));
				}
			}
		}

		public virtual Int64 TotalFileOffset
		{
			get {
				return FileOffsetTo - IndOffset;
			}
		}

		Int32 _ITimeSeriesRuntime_IndCount;
		public Int32 IndCount
		{
			get {
				return _ITimeSeriesRuntime_IndCount;
			}
			set {
				if (_ITimeSeriesRuntime_IndCount != value) {
					_ITimeSeriesRuntime_IndCount= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_15_INDCOUNT_ID] = true;
					if (ITimeSeriesRuntime_IndCount_Changed != null)
						ITimeSeriesRuntime_IndCount_Changed(this, new PropertyChangedEventArgs("IndCount", value));
				}
			}
		}

		Int64 _ITimeSeriesRuntime_RecordCount;
		public Int64 RecordCount
		{
			get {
				return _ITimeSeriesRuntime_RecordCount;
			}
			set {
				if (_ITimeSeriesRuntime_RecordCount != value) {
					_ITimeSeriesRuntime_RecordCount= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_16_RECORDCOUNT_ID] = true;
					if (ITimeSeriesRuntime_RecordCount_Changed != null)
						ITimeSeriesRuntime_RecordCount_Changed(this, new PropertyChangedEventArgs("RecordCount", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (TimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
