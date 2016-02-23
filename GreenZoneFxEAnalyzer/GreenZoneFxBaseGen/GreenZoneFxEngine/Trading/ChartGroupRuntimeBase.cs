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
		public const int PROPERTY_1_SESSION_ID = 1;
		public const int PROPERTY_2_CHARTS_ID = 2;
		public static bool RmiGetProperty(IChartGroupRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_SESSION_ID:
					value = controller.Session;
					return true;
				case PROPERTY_2_CHARTS_ID:
					value = controller.Charts;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartGroupRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IChartGroupRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Session = (IChartGroupSession) buffer.ChangedProps[ChartGroupRuntimeProps.PROPERTY_1_SESSION_ID];
			controller.Charts = (IList<IChartRuntime>) buffer.ChangedProps[ChartGroupRuntimeProps.PROPERTY_2_CHARTS_ID];
		}

		public static void AddDependencies(IChartGroupRuntime controller)
		{
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.AddRange(controller.Charts);
		}

		public static void SerializationRead(IChartGroupRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Session = (IChartGroupSession) info.GetValue("Session", typeof(IChartGroupSession));
			controller.Charts = (IList<IChartRuntime>) info.GetValue("Charts", typeof(IList<IChartRuntime>));
		}

		public static void SerializationWrite(IChartGroupRuntime controller, SerializationInfo info, StreamingContext context)
		{
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
			ChartGroupRuntimeProps.AddDependencies(this);
		}

		public ChartGroupRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartGroupRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ChartGroupRuntimeProps.AddDependencies(this);
		}

		protected ChartGroupRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartGroupRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ChartGroupRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartGroupRuntimeProps.SerializationWrite(this, info, context);
		}

		IChartGroupSession session;
		public IChartGroupSession Session
		{
			get {
				return session;
			}
			set {
				if (!___initialized) {
					session= value;
					changed[ChartGroupRuntimeProps.PROPERTY_1_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IList<IChartRuntime> charts;
		public IList<IChartRuntime> Charts
		{
			get {
				return charts;
			}
			set {
				if (!___initialized) {
					charts= value;
					changed[ChartGroupRuntimeProps.PROPERTY_2_CHARTS_ID] = true;
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


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartGroupRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ChartGroupRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
