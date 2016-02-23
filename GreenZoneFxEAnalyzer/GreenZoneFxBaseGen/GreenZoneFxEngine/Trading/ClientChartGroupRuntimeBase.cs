using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientChartGroupRuntimeProps
	{
		public static bool RmiGetProperty(IClientChartGroupRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientChartGroupRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientChartGroupRuntime controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IClientChartGroupRuntime controller)
		{
		}

		public static void SerializationRead(IClientChartGroupRuntime controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IClientChartGroupRuntime controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ClientChartGroupRuntimeBase : ChartGroupRuntimeBase, IClientChartGroupRuntime
	{

		bool ___initialized = false;


		public ClientChartGroupRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ClientChartGroupRuntimeProps.AddDependencies(this);
		}

		public ClientChartGroupRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientChartGroupRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ClientChartGroupRuntimeProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientChartGroupRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientChartGroupRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ClientChartGroupRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientChartGroupRuntimeProps.SerializationWrite(this, info, context);
		}

		public abstract Int32 CursorPosition
		{
			get ;
			set ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientChartGroupRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ClientChartGroupRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
