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
		public const int PROPERTY_15_TOTALFILEOFFSET_ID = 15;
		public const int PROPERTY_16_INDCOUNT_ID = 16;
		public const int PROPERTY_17_RECORDCOUNT_ID = 17;
		public static bool RmiGetProperty(ITimeSeriesRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_2_PERIOD_ID:
					value = controller.Period;
					return true;
				case PROPERTY_3_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_4_ONLINE_ID:
					value = controller.Online;
					return true;
				case PROPERTY_5_INPUTFILEISREAD_ID:
					value = controller.InputFileIsRead;
					return true;
				case PROPERTY_6_VOLUME_ID:
					value = controller.Volume;
					return true;
				case PROPERTY_7_BEGIN_TIME_ID:
					value = controller.Begin_time;
					return true;
				case PROPERTY_8_HEADERLEN_ID:
					value = controller.HeaderLen;
					return true;
				case PROPERTY_9_RECORDLEN_ID:
					value = controller.RecordLen;
					return true;
				case PROPERTY_10_FROM_ID:
					value = controller.From;
					return true;
				case PROPERTY_11_TO_ID:
					value = controller.To;
					return true;
				case PROPERTY_12_FILEOFFSET_ID:
					value = controller.FileOffset;
					return true;
				case PROPERTY_13_FILEOFFSETTO_ID:
					value = controller.FileOffsetTo;
					return true;
				case PROPERTY_14_INDOFFSET_ID:
					value = controller.IndOffset;
					return true;
				case PROPERTY_15_TOTALFILEOFFSET_ID:
					value = controller.TotalFileOffset;
					return true;
				case PROPERTY_16_INDCOUNT_ID:
					value = controller.IndCount;
					return true;
				case PROPERTY_17_RECORDCOUNT_ID:
					value = controller.RecordCount;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITimeSeriesRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_5_INPUTFILEISREAD_ID:
					controller.InputFileIsRead = (Boolean) value;
					return true;
				case PROPERTY_6_VOLUME_ID:
					controller.Volume = (IDArr) value;
					return true;
				case PROPERTY_7_BEGIN_TIME_ID:
					controller.Begin_time = (ILArr) value;
					return true;
				case PROPERTY_12_FILEOFFSET_ID:
					controller.FileOffset = (Int64) value;
					return true;
				case PROPERTY_13_FILEOFFSETTO_ID:
					controller.FileOffsetTo = (Int64) value;
					return true;
				case PROPERTY_14_INDOFFSET_ID:
					controller.IndOffset = (Int32) value;
					return true;
				case PROPERTY_15_TOTALFILEOFFSET_ID:
					controller.TotalFileOffset = (Int64) value;
					return true;
				case PROPERTY_16_INDCOUNT_ID:
					controller.IndCount = (Int32) value;
					return true;
				case PROPERTY_17_RECORDCOUNT_ID:
					controller.RecordCount = (Int64) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITimeSeriesRuntime controller, GreenRmiObjectBuffer buffer)
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

		public static void AddDependencies(ITimeSeriesRuntime controller)
		{
			controller.Dependencies.Add(controller.Parent);
		}

		public static void SerializationRead(ITimeSeriesRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Parent = (ISymbolRuntime) info.GetValue("Parent", typeof(ISymbolRuntime));
			controller.Period = (TimePeriodConst) info.GetValue("Period", typeof(TimePeriodConst));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Online = (Boolean) info.GetValue("Online", typeof(Boolean));
			controller.InputFileIsRead = (Boolean) info.GetValue("InputFileIsRead", typeof(Boolean));
			controller.Volume = (IDArr) info.GetValue("Volume", typeof(IDArr));
			controller.Begin_time = (ILArr) info.GetValue("Begin_time", typeof(ILArr));
			controller.HeaderLen = (Int32) info.GetValue("HeaderLen", typeof(Int32));
			controller.RecordLen = (Int32) info.GetValue("RecordLen", typeof(Int32));
			controller.From = (datetime) info.GetValue("From", typeof(datetime));
			controller.To = (datetime) info.GetValue("To", typeof(datetime));
			controller.FileOffset = (Int64) info.GetValue("FileOffset", typeof(Int64));
			controller.FileOffsetTo = (Int64) info.GetValue("FileOffsetTo", typeof(Int64));
			controller.IndOffset = (Int32) info.GetValue("IndOffset", typeof(Int32));
			controller.TotalFileOffset = (Int64) info.GetValue("TotalFileOffset", typeof(Int64));
			controller.IndCount = (Int32) info.GetValue("IndCount", typeof(Int32));
			controller.RecordCount = (Int64) info.GetValue("RecordCount", typeof(Int64));
		}

		public static void SerializationWrite(ITimeSeriesRuntime controller, SerializationInfo info, StreamingContext context)
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
			info.AddValue("TotalFileOffset", controller.TotalFileOffset);
			info.AddValue("IndCount", controller.IndCount);
			info.AddValue("RecordCount", controller.RecordCount);
		}

	}
	public abstract class TimeSeriesRuntimeBase : RmiBase, ITimeSeriesRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler InputFileIsReadChanged;
		public event PropertyChangedEventHandler VolumeChanged;
		public event PropertyChangedEventHandler Begin_timeChanged;
		public event PropertyChangedEventHandler FileOffsetChanged;
		public event PropertyChangedEventHandler FileOffsetToChanged;
		public event PropertyChangedEventHandler IndOffsetChanged;
		public event PropertyChangedEventHandler TotalFileOffsetChanged;
		public event PropertyChangedEventHandler IndCountChanged;
		public event PropertyChangedEventHandler RecordCountChanged;

		public TimeSeriesRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			TimeSeriesRuntimeProps.AddDependencies(this);
		}

		public TimeSeriesRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TimeSeriesRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			TimeSeriesRuntimeProps.AddDependencies(this);
		}

		protected TimeSeriesRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TimeSeriesRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			TimeSeriesRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TimeSeriesRuntimeProps.SerializationWrite(this, info, context);
		}

		public abstract void Load(datetime focusedTime);

		public abstract void LoadForward(datetime focusedTime);

		public abstract void LoadForward(Int32 offset);

		public abstract void LoadAtTotal(Int64 total_ind);

		ISymbolRuntime parent;
		public ISymbolRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TimePeriodConst period;
		public TimePeriodConst Period
		{
			get {
				return period;
			}
			set {
				if (!___initialized) {
					period= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_2_PERIOD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol symbol;
		public symbol Symbol
		{
			get {
				return symbol;
			}
			set {
				if (!___initialized) {
					symbol= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_3_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean online;
		public Boolean Online
		{
			get {
				return online;
			}
			set {
				if (!___initialized) {
					online= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_4_ONLINE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean inputFileIsRead;
		public Boolean InputFileIsRead
		{
			get {
				return inputFileIsRead;
			}
			set {
				if (inputFileIsRead != value) {
					inputFileIsRead= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_5_INPUTFILEISREAD_ID] = true;
					if (InputFileIsReadChanged != null)
						InputFileIsReadChanged(this, new PropertyChangedEventArgs("InputFileIsRead", value));
				}
			}
		}

		IDArr volume;
		public IDArr Volume
		{
			get {
				return volume;
			}
			set {
				if (volume != value) {
					volume= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_6_VOLUME_ID] = true;
					if (VolumeChanged != null)
						VolumeChanged(this, new PropertyChangedEventArgs("Volume", value));
				}
			}
		}

		ILArr begin_time;
		public ILArr Begin_time
		{
			get {
				return begin_time;
			}
			set {
				if (begin_time != value) {
					begin_time= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_7_BEGIN_TIME_ID] = true;
					if (Begin_timeChanged != null)
						Begin_timeChanged(this, new PropertyChangedEventArgs("Begin_time", value));
				}
			}
		}

		Int32 headerLen;
		public Int32 HeaderLen
		{
			get {
				return headerLen;
			}
			set {
				if (!___initialized) {
					headerLen= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_8_HEADERLEN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Int32 recordLen;
		public Int32 RecordLen
		{
			get {
				return recordLen;
			}
			set {
				if (!___initialized) {
					recordLen= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_9_RECORDLEN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		datetime from;
		public datetime From
		{
			get {
				return from;
			}
			set {
				if (!___initialized) {
					from= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_10_FROM_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		datetime to;
		public datetime To
		{
			get {
				return to;
			}
			set {
				if (!___initialized) {
					to= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_11_TO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Int64 fileOffset;
		public Int64 FileOffset
		{
			get {
				return fileOffset;
			}
			set {
				if (fileOffset != value) {
					fileOffset= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_12_FILEOFFSET_ID] = true;
					if (FileOffsetChanged != null)
						FileOffsetChanged(this, new PropertyChangedEventArgs("FileOffset", value));
				}
			}
		}

		Int64 fileOffsetTo;
		public Int64 FileOffsetTo
		{
			get {
				return fileOffsetTo;
			}
			set {
				if (fileOffsetTo != value) {
					fileOffsetTo= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_13_FILEOFFSETTO_ID] = true;
					if (FileOffsetToChanged != null)
						FileOffsetToChanged(this, new PropertyChangedEventArgs("FileOffsetTo", value));
				}
			}
		}

		Int32 indOffset;
		public Int32 IndOffset
		{
			get {
				return indOffset;
			}
			set {
				if (indOffset != value) {
					indOffset= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_14_INDOFFSET_ID] = true;
					if (IndOffsetChanged != null)
						IndOffsetChanged(this, new PropertyChangedEventArgs("IndOffset", value));
				}
			}
		}

		Int64 totalFileOffset;
		public Int64 TotalFileOffset
		{
			get {
				return totalFileOffset;
			}
			set {
				if (totalFileOffset != value) {
					totalFileOffset= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_15_TOTALFILEOFFSET_ID] = true;
					if (TotalFileOffsetChanged != null)
						TotalFileOffsetChanged(this, new PropertyChangedEventArgs("TotalFileOffset", value));
				}
			}
		}

		Int32 indCount;
		public Int32 IndCount
		{
			get {
				return indCount;
			}
			set {
				if (indCount != value) {
					indCount= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_16_INDCOUNT_ID] = true;
					if (IndCountChanged != null)
						IndCountChanged(this, new PropertyChangedEventArgs("IndCount", value));
				}
			}
		}

		Int64 recordCount;
		public Int64 RecordCount
		{
			get {
				return recordCount;
			}
			set {
				if (recordCount != value) {
					recordCount= value;
					changed[TimeSeriesRuntimeProps.PROPERTY_17_RECORDCOUNT_ID] = true;
					if (RecordCountChanged != null)
						RecordCountChanged(this, new PropertyChangedEventArgs("RecordCount", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TimeSeriesRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!TimeSeriesRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
