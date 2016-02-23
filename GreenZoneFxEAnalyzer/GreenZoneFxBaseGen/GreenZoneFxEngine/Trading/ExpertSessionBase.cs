using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExpertSessionProps
	{
		public static bool RmiGetProperty(IExpertSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertSession controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IExpertSession controller)
		{
		}

		public static void SerializationRead(IExpertSession controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(IExpertSession controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class ExpertSessionBase : ExecSessionBase, IExpertSession
	{

		bool ___initialized = false;


		public ExpertSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExpertSessionProps.AddDependencies(this);
		}

		public ExpertSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertSessionProps.Initialize(this, buffer);
			___initialized = true;
			ExpertSessionProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			ExpertSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertSessionProps.SerializationWrite(this, info, context);
		}

		public virtual Mt4ExecutableInfo ExpertInfo
		{
			get {
				return ExecutableInfo;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExpertSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
