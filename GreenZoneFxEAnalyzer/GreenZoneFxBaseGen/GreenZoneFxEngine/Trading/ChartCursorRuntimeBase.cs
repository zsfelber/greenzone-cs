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
		public static bool RmiGetProperty(IChartCursorRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_2_SERIESMANAGER_ID:
					value = controller.SeriesManager;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartCursorRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_2_SERIESMANAGER_ID:
					controller.SeriesManager = (ISeriesManagerRuntime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartCursorRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Parent = (IChartRuntime) buffer.ChangedProps[ChartCursorRuntimeProps.PROPERTY_1_PARENT_ID];
		}

		public static void AddDependencies(IChartCursorRuntime controller)
		{
			controller.Dependencies.Add(controller.Parent);
		}

		public static void SerializationRead(IChartCursorRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Parent = (IChartRuntime) info.GetValue("Parent", typeof(IChartRuntime));
			controller.SeriesManager = (ISeriesManagerRuntime) info.GetValue("SeriesManager", typeof(ISeriesManagerRuntime));
		}

		public static void SerializationWrite(IChartCursorRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("SeriesManager", controller.SeriesManager);
		}

	}
	public abstract class ChartCursorRuntimeBase : RmiBase, IChartCursorRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler SeriesManagerChanged;

		public ChartCursorRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartCursorRuntimeProps.AddDependencies(this);
		}

		public ChartCursorRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartCursorRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ChartCursorRuntimeProps.AddDependencies(this);
		}

		protected ChartCursorRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartCursorRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartCursorRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartCursorRuntimeProps.SerializationWrite(this, info, context);
		}

		IChartRuntime parent;
		public IChartRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[ChartCursorRuntimeProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISeriesManagerRuntime seriesManager;
		public virtual ISeriesManagerRuntime SeriesManager
		{
			get {
				return seriesManager;
			}
			set {
				if (seriesManager != value) {
					seriesManager= value;
					changed[ChartCursorRuntimeProps.PROPERTY_2_SERIESMANAGER_ID] = true;
					if (SeriesManagerChanged != null)
						SeriesManagerChanged(this, new PropertyChangedEventArgs("SeriesManager", value));
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartCursorRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartCursorRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
