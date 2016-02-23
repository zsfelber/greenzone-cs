using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.Util;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerIndicatorRuntimeProps
	{
		public static bool RmiGetProperty(IServerIndicatorRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (IndicatorRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerIndicatorRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ServerExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (IndicatorRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerIndicatorRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.Initialize(controller, buffer, true);
			}
			IndicatorRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IServerIndicatorRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.AddDependencies(controller, true);
			}
			IndicatorRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IServerIndicatorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
			IndicatorRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IServerIndicatorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ServerExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			IndicatorRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ServerIndicatorRuntimeBase : ServerExecRuntimeEx, IServerIndicatorRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IIndicatorRuntime_Buffers_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_Levels_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_NumIndicatorBuffers_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_NumIndicatorLevels_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_Visible_Changed;
		public event PropertyChangedEventHandler IIndicatorRuntime_NewClientInstance_Changed;


		public ServerIndicatorRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ServerIndicatorRuntimeProps.AddDependencies(this, false);
		}

		public ServerIndicatorRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ServerIndicatorRuntimeProps.AddDependencies(this, false);
		}

		public ServerIndicatorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerIndicatorRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerIndicatorRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerIndicatorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerIndicatorRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerIndicatorRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerIndicatorRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void CopyParamsTo(IIndicatorRuntime ind2);

		public abstract Int32 Init();

		public abstract Int32 Deinit();

		public abstract Int32 OnTick();

		public abstract void IndicatorBuffers(Int32 count);

		public abstract void IndicatorShortName(String shortName);

		public abstract void IndicatorDigits(Int32 digits);

		public abstract void SetIndicatorBuffer(Int32 index, IndicatorBuffer buffer);

		public abstract Boolean SetIndexBuffer(Int32 index, ref DArr array);

		public abstract void SetIndexDrawBegin(Int32 index, Int32 begin);

		public abstract void SetIndexShift(Int32 index, Int32 shift);

		public abstract void SetIndexStyle(Int32 index, DrawingStyle style);

		public abstract void SetIndexStyleWidth(Int32 index, DrawingWidth width);

		public abstract void SetIndexLabel(Int32 index, String label);

		public abstract void SetIndexArrow(Int32 index, IWingdingsChar arrow);

		public abstract void SetIndexColor(Int32 index, Color color);

		public abstract void SetIndexEmptyValue(Int32 index, Double value);

		public abstract void SetIndexStyle(Int32 index, DrawingStyle type, DrawingStylesWidth1 style, DrawingWidth width, Color clr);

		public abstract void SetLevelStyle(DrawingStylesWidth1 draw_style, DrawingWidth line_width, Color clr, Int32 levelInd, Double value);

		public abstract void SetLevelColor(Color clr, Int32 levelInd);

		public abstract void SetLevelValue(Int32 level, Double value);

		public IndicatorLevel GetLevel(Int32 index)
		{
			return Levels[index];
		}

		public abstract void RaiseInstanceChanged(IIndicatorRuntime newInstance);



		public abstract IndicatorId Id
		{
			get ;
		}

		public virtual IndicatorBuffer  this[Int32 index]
		{
			get {
				return Buffers[index];
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

		public virtual Boolean Initialized
		{
			get {
				return Buffers.Length > 0 && Buffers[0].Buffer != null && Buffers[0].Buffer.Length != 0 && Buffers[0].SBuffer != null;
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

		public virtual Mt4ExecutableInfo IndicatorInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}

		public new virtual IIndicatorSession Session
		{
			get {
				return (IIndicatorSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerIndicatorRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerIndicatorRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
