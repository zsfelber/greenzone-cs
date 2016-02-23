using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class IndicatorRuntimeProps
	{
		public const int PROPERTY_11_SESSION_ID = 11;
		public const int PROPERTY_12_BUFFERS_ID = 12;
		public const int PROPERTY_13_LEVELS_ID = 13;
		public const int PROPERTY_14_NUMINDICATORBUFFERS_ID = 14;
		public const int PROPERTY_15_NUMINDICATORLEVELS_ID = 15;
		public const int PROPERTY_16_VISIBLE_ID = 16;
		public const int PROPERTY_17_INITIALIZED_ID = 17;
		public static bool RmiGetProperty(IIndicatorRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_11_SESSION_ID:
					value = controller.Session;
					return true;
				case PROPERTY_12_BUFFERS_ID:
					value = controller.Buffers;
					return true;
				case PROPERTY_13_LEVELS_ID:
					value = controller.Levels;
					return true;
				case PROPERTY_14_NUMINDICATORBUFFERS_ID:
					value = controller.NumIndicatorBuffers;
					return true;
				case PROPERTY_15_NUMINDICATORLEVELS_ID:
					value = controller.NumIndicatorLevels;
					return true;
				case PROPERTY_16_VISIBLE_ID:
					value = controller.Visible;
					return true;
				case PROPERTY_17_INITIALIZED_ID:
					value = controller.Initialized;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_12_BUFFERS_ID:
					controller.Buffers = (IndicatorBuffer[]) value;
					return true;
				case PROPERTY_13_LEVELS_ID:
					controller.Levels = (IndicatorLevel[]) value;
					return true;
				case PROPERTY_14_NUMINDICATORBUFFERS_ID:
					controller.NumIndicatorBuffers = (Int32) value;
					return true;
				case PROPERTY_15_NUMINDICATORLEVELS_ID:
					controller.NumIndicatorLevels = (Int32) value;
					return true;
				case PROPERTY_16_VISIBLE_ID:
					controller.Visible = (Boolean) value;
					return true;
				case PROPERTY_17_INITIALIZED_ID:
					controller.Initialized = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Session = (IIndicatorSession) buffer.ChangedProps[IndicatorRuntimeProps.PROPERTY_11_SESSION_ID];
		}

		public static void AddDependencies(IIndicatorRuntime controller)
		{
			controller.Dependencies.Add(controller.Session);
		}

		public static void SerializationRead(IIndicatorRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Session = (IIndicatorSession) info.GetValue("Session", typeof(IIndicatorSession));
			controller.Buffers = (IndicatorBuffer[]) info.GetValue("Buffers", typeof(IndicatorBuffer[]));
			controller.Levels = (IndicatorLevel[]) info.GetValue("Levels", typeof(IndicatorLevel[]));
			controller.NumIndicatorBuffers = (Int32) info.GetValue("NumIndicatorBuffers", typeof(Int32));
			controller.NumIndicatorLevels = (Int32) info.GetValue("NumIndicatorLevels", typeof(Int32));
			controller.Visible = (Boolean) info.GetValue("Visible", typeof(Boolean));
			controller.Initialized = (Boolean) info.GetValue("Initialized", typeof(Boolean));
		}

		public static void SerializationWrite(IIndicatorRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Session", controller.Session);
			info.AddValue("Buffers", controller.Buffers);
			info.AddValue("Levels", controller.Levels);
			info.AddValue("NumIndicatorBuffers", controller.NumIndicatorBuffers);
			info.AddValue("NumIndicatorLevels", controller.NumIndicatorLevels);
			info.AddValue("Visible", controller.Visible);
			info.AddValue("Initialized", controller.Initialized);
		}

	}
	public abstract class IndicatorRuntimeBase : ExecRuntimeBase, IIndicatorRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler BuffersChanged;
		public event PropertyChangedEventHandler LevelsChanged;
		public event PropertyChangedEventHandler NumIndicatorBuffersChanged;
		public event PropertyChangedEventHandler NumIndicatorLevelsChanged;
		public event PropertyChangedEventHandler VisibleChanged;
		public event PropertyChangedEventHandler InitializedChanged;

		public IndicatorRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this);
		}

		public IndicatorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this);
		}

		protected IndicatorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorRuntimeProps.SerializationWrite(this, info, context);
		}

		IIndicatorSession session;
		public IIndicatorSession Session
		{
			get {
				return session;
			}
			set {
				if (!___initialized) {
					session= value;
					changed[IndicatorRuntimeProps.PROPERTY_11_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual Mt4ExecutableInfo IndicatorInfo
		{
			get {
				return Session.IndicatorInfo;
			}
		}

		IndicatorBuffer[] buffers;
		public IndicatorBuffer[] Buffers
		{
			get {
				return buffers;
			}
			set {
				if (buffers != value) {
					buffers= value;
					changed[IndicatorRuntimeProps.PROPERTY_12_BUFFERS_ID] = true;
					if (BuffersChanged != null)
						BuffersChanged(this, new PropertyChangedEventArgs("Buffers", value));
				}
			}
		}

		IndicatorLevel[] levels;
		public IndicatorLevel[] Levels
		{
			get {
				return levels;
			}
			set {
				if (levels != value) {
					levels= value;
					changed[IndicatorRuntimeProps.PROPERTY_13_LEVELS_ID] = true;
					if (LevelsChanged != null)
						LevelsChanged(this, new PropertyChangedEventArgs("Levels", value));
				}
			}
		}

		Int32 numIndicatorBuffers;
		public virtual Int32 NumIndicatorBuffers
		{
			get {
				return numIndicatorBuffers;
			}
			set {
				if (numIndicatorBuffers != value) {
					numIndicatorBuffers= value;
					changed[IndicatorRuntimeProps.PROPERTY_14_NUMINDICATORBUFFERS_ID] = true;
					if (NumIndicatorBuffersChanged != null)
						NumIndicatorBuffersChanged(this, new PropertyChangedEventArgs("NumIndicatorBuffers", value));
				}
			}
		}

		Int32 numIndicatorLevels;
		public virtual Int32 NumIndicatorLevels
		{
			get {
				return numIndicatorLevels;
			}
			set {
				if (numIndicatorLevels != value) {
					numIndicatorLevels= value;
					changed[IndicatorRuntimeProps.PROPERTY_15_NUMINDICATORLEVELS_ID] = true;
					if (NumIndicatorLevelsChanged != null)
						NumIndicatorLevelsChanged(this, new PropertyChangedEventArgs("NumIndicatorLevels", value));
				}
			}
		}

		Boolean visible;
		public Boolean Visible
		{
			get {
				return visible;
			}
			set {
				if (visible != value) {
					visible= value;
					changed[IndicatorRuntimeProps.PROPERTY_16_VISIBLE_ID] = true;
					if (VisibleChanged != null)
						VisibleChanged(this, new PropertyChangedEventArgs("Visible", value));
				}
			}
		}

		Boolean initialized;
		public Boolean Initialized
		{
			get {
				return initialized;
			}
			set {
				if (initialized != value) {
					initialized= value;
					changed[IndicatorRuntimeProps.PROPERTY_17_INITIALIZED_ID] = true;
					if (InitializedChanged != null)
						InitializedChanged(this, new PropertyChangedEventArgs("Initialized", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
