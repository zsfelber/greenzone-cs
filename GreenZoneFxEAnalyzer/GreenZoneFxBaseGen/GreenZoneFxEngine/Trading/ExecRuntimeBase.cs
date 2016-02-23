using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExecRuntimeProps
	{
		public static bool RmiGetProperty(IExecRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExecRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExecRuntime controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IExecRuntime controller)
		{
		}

		public static void SerializationRead(IExecRuntime controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IExecRuntime controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ExecRuntimeBase : UserRuntimeBase, IExecRuntime
	{

		bool ___initialized = false;


		public ExecRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this);
		}

		public ExecRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExecRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExecRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExecRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ExecRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExecRuntimeProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExecRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExecRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
