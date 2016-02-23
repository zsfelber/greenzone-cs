using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ChartGroupRuntimeProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_SESSION_ID = 2;
		public const int PROPERTY_3_CHARTS_ID = 3;
		public static bool RmiGetProperty(IChartGroupRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartGroupRuntimeProps.PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case ChartGroupRuntimeProps.PROPERTY_2_SESSION_ID:
					value = controller.Session;
					return true;
				case ChartGroupRuntimeProps.PROPERTY_3_CHARTS_ID:
					value = controller.Charts;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartGroupRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartGroupRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[ChartGroupRuntimeProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.Session = (IChartGroupSession) buffer.ChangedProps[ChartGroupRuntimeProps.PROPERTY_2_SESSION_ID];
			controller.Charts = (IList<IChartRuntime>) buffer.ChangedProps[ChartGroupRuntimeProps.PROPERTY_3_CHARTS_ID];
		}

		public static void AddDependencies(IChartGroupRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Environment);
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.AddRange(controller.Charts);
		}

		public static void SerializationRead(IChartGroupRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) info.GetValue("Environment", typeof(IEnvironmentRuntime));
			controller.Session = (IChartGroupSession) info.GetValue("Session", typeof(IChartGroupSession));
			controller.Charts = (IList<IChartRuntime>) info.GetValue("Charts", typeof(IList<IChartRuntime>));
		}

		public static void SerializationWrite(IChartGroupRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Environment", controller.Environment);
			info.AddValue("Session", controller.Session);
			info.AddValue("Charts", controller.Charts);
		}

	}
	public abstract class ChartGroupRuntimeBase : RmiBase, IChartGroupRuntime
	{

		bool ___initialized = false;


		public ChartGroupRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartGroupRuntimeProps.AddDependencies(this, false);
		}

		public ChartGroupRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartGroupRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartGroupRuntimeProps.AddDependencies(this, false);
		}

		protected ChartGroupRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartGroupRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartGroupRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartGroupRuntimeProps.SerializationWrite(this, info, context, false);
		}


		IEnvironmentRuntime _IChartGroupRuntime_Environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return _IChartGroupRuntime_Environment;
			}
			set {
				if (!___initialized) {
					_IChartGroupRuntime_Environment= value;
					changed[ChartGroupRuntimeProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IChartGroupSession _IChartGroupRuntime_Session;
		public IChartGroupSession Session
		{
			get {
				return _IChartGroupRuntime_Session;
			}
			set {
				if (!___initialized) {
					_IChartGroupRuntime_Session= value;
					changed[ChartGroupRuntimeProps.PROPERTY_2_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IList<IChartRuntime> _IChartGroupRuntime_Charts;
		public IList<IChartRuntime> Charts
		{
			get {
				return _IChartGroupRuntime_Charts;
			}
			set {
				if (!___initialized) {
					_IChartGroupRuntime_Charts= value;
					changed[ChartGroupRuntimeProps.PROPERTY_3_CHARTS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public abstract datetime ScrolledBarTime
		{
			get ;
			set ;
		}

		public abstract IChartRuntime MasterChart
		{
			get ;
		}

		public abstract IChartRuntime FirstConnectedChart
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartGroupRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartGroupRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
