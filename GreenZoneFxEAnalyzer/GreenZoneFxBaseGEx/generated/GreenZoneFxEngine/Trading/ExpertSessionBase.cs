using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExpertSessionProps
	{
		public static bool RmiGetProperty(IExpertSession controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecSessionProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertSession controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecSessionProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IExpertSession controller, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IExpertSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IExpertSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ExpertSessionBase : ExecSessionBase, IExpertSession
	{

		bool ___initialized = false;


		public ExpertSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExpertSessionProps.AddDependencies(this, false);
		}

		public ExpertSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			ExpertSessionProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExpertSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertSessionProps.SerializationWrite(this, info, context, false);
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
			if (ExpertSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExpertSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
