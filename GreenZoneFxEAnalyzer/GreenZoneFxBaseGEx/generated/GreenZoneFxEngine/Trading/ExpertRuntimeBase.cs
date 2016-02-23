using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExpertRuntimeProps
	{
		public static bool RmiGetProperty(IExpertRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IExpertRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IExpertRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IExpertRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IExpertRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ExpertRuntimeBase : ExecRuntimeBase, IExpertRuntime
	{

		bool ___initialized = false;


		public ExpertRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this, false);
		}

		public ExpertRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this, false);
		}

		public ExpertRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ExpertRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Int32 Init();

		public abstract Int32 Deinit();

		public abstract Int32 OnTick();


		public new virtual IExpertSession Session
		{
			get {
				return (IExpertSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}

		public virtual Mt4ExecutableInfo ExpertInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExpertRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
