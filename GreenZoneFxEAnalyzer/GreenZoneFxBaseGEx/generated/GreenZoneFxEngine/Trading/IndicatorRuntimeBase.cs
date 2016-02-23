using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class IndicatorRuntimeProps
	{
		public const int PROPERTY_11_BUFFERS_ID = 11;
		public const int PROPERTY_12_LEVELS_ID = 12;
		public const int PROPERTY_13_NUMINDICATORBUFFERS_ID = 13;
		public const int PROPERTY_14_NUMINDICATORLEVELS_ID = 14;
		public const int PROPERTY_15_VISIBLE_ID = 15;
		public const int PROPERTY_16_NEWCLIENTINSTANCE_ID = 16;
		public static bool RmiGetProperty(IIndicatorRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorRuntimeProps.PROPERTY_11_BUFFERS_ID:
					value = controller.Buffers;
					return true;
				case IndicatorRuntimeProps.PROPERTY_12_LEVELS_ID:
					value = controller.Levels;
					return true;
				case IndicatorRuntimeProps.PROPERTY_13_NUMINDICATORBUFFERS_ID:
					value = controller.NumIndicatorBuffers;
					return true;
				case IndicatorRuntimeProps.PROPERTY_14_NUMINDICATORLEVELS_ID:
					value = controller.NumIndicatorLevels;
					return true;
				case IndicatorRuntimeProps.PROPERTY_15_VISIBLE_ID:
					value = controller.Visible;
					return true;
				case IndicatorRuntimeProps.PROPERTY_16_NEWCLIENTINSTANCE_ID:
					value = controller.NewClientInstance;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorRuntimeProps.PROPERTY_11_BUFFERS_ID:
					controller.Buffers = (IndicatorBuffer[]) value;
					return true;
				case IndicatorRuntimeProps.PROPERTY_12_LEVELS_ID:
					controller.Levels = (IndicatorLevel[]) value;
					return true;
				case IndicatorRuntimeProps.PROPERTY_13_NUMINDICATORBUFFERS_ID:
					controller.NumIndicatorBuffers = (Int32) value;
					return true;
				case IndicatorRuntimeProps.PROPERTY_14_NUMINDICATORLEVELS_ID:
					controller.NumIndicatorLevels = (Int32) value;
					return true;
				case IndicatorRuntimeProps.PROPERTY_15_VISIBLE_ID:
					controller.Visible = (Boolean) value;
					return true;
				case IndicatorRuntimeProps.PROPERTY_16_NEWCLIENTINSTANCE_ID:
					controller.NewClientInstance = (IIndicatorRuntime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IIndicatorRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IIndicatorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
			controller.Buffers = (IndicatorBuffer[]) info.GetValue("Buffers", typeof(IndicatorBuffer[]));
			controller.Levels = (IndicatorLevel[]) info.GetValue("Levels", typeof(IndicatorLevel[]));
			controller.NumIndicatorBuffers = (Int32) info.GetValue("NumIndicatorBuffers", typeof(Int32));
			controller.NumIndicatorLevels = (Int32) info.GetValue("NumIndicatorLevels", typeof(Int32));
			controller.Visible = (Boolean) info.GetValue("Visible", typeof(Boolean));
			controller.NewClientInstance = (IIndicatorRuntime) info.GetValue("NewClientInstance", typeof(IIndicatorRuntime));
		}

		public static void SerializationWrite(IIndicatorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("Buffers", controller.Buffers);
			info.AddValue("Levels", controller.Levels);
			info.AddValue("NumIndicatorBuffers", controller.NumIndicatorBuffers);
			info.AddValue("NumIndicatorLevels", controller.NumIndicatorLevels);
			info.AddValue("Visible", controller.Visible);
			info.AddValue("NewClientInstance", controller.NewClientInstance);
		}

	}
	public abstract class IndicatorRuntimeBase : ExecRuntimeBase, IIndicatorRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorRuntime_Buffers_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_Levels_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_NumIndicatorBuffers_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_NumIndicatorLevels_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_Visible_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_NewClientInstance_Changed;

		public IndicatorRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this, false);
		}

		public IndicatorRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this, false);
		}

		public IndicatorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected IndicatorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public IndicatorLevel GetLevel(Int32 index)
		{
			return Levels[index];
		}

		public abstract void RaiseInstanceChanged(IIndicatorRuntime newInstance);


		public new virtual IIndicatorSession Session
		{
			get {
				return (IIndicatorSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}

		public virtual Mt4ExecutableInfo IndicatorInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}

		IndicatorBuffer[] _IIndicatorRuntime_Buffers;
		public IndicatorBuffer[] Buffers
		{
			get {
				return _IIndicatorRuntime_Buffers;
			}
			set {
				if (_IIndicatorRuntime_Buffers != value) {
					_IIndicatorRuntime_Buffers= value;
					changed[IndicatorRuntimeProps.PROPERTY_11_BUFFERS_ID] = true;
					if (IIndicatorRuntime_Buffers_Changed != null)
						IIndicatorRuntime_Buffers_Changed(this, new PropertyChangedEventArgs("Buffers", value));
				}
			}
		}

		IndicatorLevel[] _IIndicatorRuntime_Levels;
		public IndicatorLevel[] Levels
		{
			get {
				return _IIndicatorRuntime_Levels;
			}
			set {
				if (_IIndicatorRuntime_Levels != value) {
					_IIndicatorRuntime_Levels= value;
					changed[IndicatorRuntimeProps.PROPERTY_12_LEVELS_ID] = true;
					if (IIndicatorRuntime_Levels_Changed != null)
						IIndicatorRuntime_Levels_Changed(this, new PropertyChangedEventArgs("Levels", value));
				}
			}
		}

		Int32 _IIndicatorRuntime_NumIndicatorBuffers;
		public virtual Int32 NumIndicatorBuffers
		{
			get {
				return _IIndicatorRuntime_NumIndicatorBuffers;
			}
			set {
				if (_IIndicatorRuntime_NumIndicatorBuffers != value) {
					_IIndicatorRuntime_NumIndicatorBuffers= value;
					changed[IndicatorRuntimeProps.PROPERTY_13_NUMINDICATORBUFFERS_ID] = true;
					if (IIndicatorRuntime_NumIndicatorBuffers_Changed != null)
						IIndicatorRuntime_NumIndicatorBuffers_Changed(this, new PropertyChangedEventArgs("NumIndicatorBuffers", value));
				}
			}
		}

		Int32 _IIndicatorRuntime_NumIndicatorLevels;
		public virtual Int32 NumIndicatorLevels
		{
			get {
				return _IIndicatorRuntime_NumIndicatorLevels;
			}
			set {
				if (_IIndicatorRuntime_NumIndicatorLevels != value) {
					_IIndicatorRuntime_NumIndicatorLevels= value;
					changed[IndicatorRuntimeProps.PROPERTY_14_NUMINDICATORLEVELS_ID] = true;
					if (IIndicatorRuntime_NumIndicatorLevels_Changed != null)
						IIndicatorRuntime_NumIndicatorLevels_Changed(this, new PropertyChangedEventArgs("NumIndicatorLevels", value));
				}
			}
		}

		Boolean _IIndicatorRuntime_Visible;
		public Boolean Visible
		{
			get {
				return _IIndicatorRuntime_Visible;
			}
			set {
				if (_IIndicatorRuntime_Visible != value) {
					_IIndicatorRuntime_Visible= value;
					changed[IndicatorRuntimeProps.PROPERTY_15_VISIBLE_ID] = true;
					if (IIndicatorRuntime_Visible_Changed != null)
						IIndicatorRuntime_Visible_Changed(this, new PropertyChangedEventArgs("Visible", value));
				}
			}
		}

		public virtual Boolean Initialized
		{
			get {
				return Buffers.Length > 0 && Buffers[0].Buffer != null && Buffers[0].Buffer.Length != 0 && Buffers[0].SBuffer != null;
			}
		}

		IIndicatorRuntime _IIndicatorRuntime_NewClientInstance;
		public IIndicatorRuntime NewClientInstance
		{
			get {
				return _IIndicatorRuntime_NewClientInstance;
			}
			set {
				if (_IIndicatorRuntime_NewClientInstance != value) {
					_IIndicatorRuntime_NewClientInstance= value;
					changed[IndicatorRuntimeProps.PROPERTY_16_NEWCLIENTINSTANCE_ID] = true;
					if (IIndicatorRuntime_NewClientInstance_Changed != null)
						IIndicatorRuntime_NewClientInstance_Changed(this, new PropertyChangedEventArgs("NewClientInstance", value));
				}
			}
		}

		public virtual IndicatorBuffer  this[Int32 index]
		{
			get {
				return Buffers[index];
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
