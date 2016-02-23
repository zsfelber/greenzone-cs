using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ChartCursorRuntimeProps
	{
		public const int PROPERTY_1_PARENT_ID = 1;
		public const int PROPERTY_2_SERIESMANAGER_ID = 2;
		public const int PROPERTY_3_SERIESRANGE_ID = 3;
		public static bool RmiGetProperty(IChartCursorRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartCursorRuntimeProps.PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case ChartCursorRuntimeProps.PROPERTY_2_SERIESMANAGER_ID:
					value = controller.SeriesManager;
					return true;
				case ChartCursorRuntimeProps.PROPERTY_3_SERIESRANGE_ID:
					value = controller.SeriesRange;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartCursorRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartCursorRuntimeProps.PROPERTY_2_SERIESMANAGER_ID:
					controller.SeriesManager = (ISeriesManagerRuntime) value;
					return true;
				case ChartCursorRuntimeProps.PROPERTY_3_SERIESRANGE_ID:
					controller.SeriesRange = (SeriesRange) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartCursorRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Parent = (IChartRuntime) buffer.ChangedProps[ChartCursorRuntimeProps.PROPERTY_1_PARENT_ID];
		}

		public static void AddDependencies(IChartCursorRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Parent);
		}

		public static void SerializationRead(IChartCursorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Parent = (IChartRuntime) info.GetValue("Parent", typeof(IChartRuntime));
			controller.SeriesManager = (ISeriesManagerRuntime) info.GetValue("SeriesManager", typeof(ISeriesManagerRuntime));
			controller.SeriesRange = (SeriesRange) info.GetValue("SeriesRange", typeof(SeriesRange));
		}

		public static void SerializationWrite(IChartCursorRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SeriesManager", controller.SeriesManager);
			info.AddValue("SeriesRange", controller.SeriesRange);
		}

	}
	public abstract class ChartCursorRuntimeBase : RmiBase, IChartCursorRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IChartCursorRuntime_SeriesManager_Changed;
		public event PropertyChangedEventHandler IChartCursorRuntime_SeriesRange_Changed;

		public ChartCursorRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartCursorRuntimeProps.AddDependencies(this, false);
		}

		public ChartCursorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartCursorRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartCursorRuntimeProps.AddDependencies(this, false);
		}

		protected ChartCursorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartCursorRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartCursorRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartCursorRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void UpdateSeriesManager();

		public abstract void UpdateSeriesRange();


		IChartRuntime _IChartCursorRuntime_Parent;
		public IChartRuntime Parent
		{
			get {
				return _IChartCursorRuntime_Parent;
			}
			set {
				if (!___initialized) {
					_IChartCursorRuntime_Parent= value;
					changed[ChartCursorRuntimeProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISeriesManagerRuntime _IChartCursorRuntime_SeriesManager;
		public virtual ISeriesManagerRuntime SeriesManager
		{
			get {
				return _IChartCursorRuntime_SeriesManager;
			}
			set {
				if (_IChartCursorRuntime_SeriesManager != value) {
					_IChartCursorRuntime_SeriesManager= value;
					changed[ChartCursorRuntimeProps.PROPERTY_2_SERIESMANAGER_ID] = true;
					if (IChartCursorRuntime_SeriesManager_Changed != null)
						IChartCursorRuntime_SeriesManager_Changed(this, new PropertyChangedEventArgs("SeriesManager", value));
				}
			}
		}

		public virtual INormalSeriesManagerCache SeriesCache
		{
			get {
				return SeriesManager.DefaultCache;
			}
		}

		public virtual datetime ScrolledBarTime
		{
			get {
				return Parent.ScrolledBarTime;
			}
			set {
				Parent.ScrolledBarTime = value;
			}
		}

		SeriesRange _IChartCursorRuntime_SeriesRange;
		public SeriesRange SeriesRange
		{
			get {
				return _IChartCursorRuntime_SeriesRange;
			}
			set {
				if (_IChartCursorRuntime_SeriesRange != value) {
					_IChartCursorRuntime_SeriesRange= value;
					changed[ChartCursorRuntimeProps.PROPERTY_3_SERIESRANGE_ID] = true;
					if (IChartCursorRuntime_SeriesRange_Changed != null)
						IChartCursorRuntime_SeriesRange_Changed(this, new PropertyChangedEventArgs("SeriesRange", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartCursorRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartCursorRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
