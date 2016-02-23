using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ClientExpertRuntimeProps
	{
		public static bool RmiGetProperty(IClientExpertRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientExecRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			if (ExpertRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IClientExpertRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ClientExecRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			if (ExpertRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
				return true;
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IClientExpertRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.Initialize(controller, buffer, true);
			}
			ExpertRuntimeProps.Initialize(controller, buffer, true);
		}

		public static void AddDependencies(IClientExpertRuntime controller, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.AddDependencies(controller, true);
			}
			ExpertRuntimeProps.AddDependencies(controller, true);
		}

		public static void SerializationRead(IClientExpertRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.SerializationRead(controller, info, context, true);
			}
			ExpertRuntimeProps.SerializationRead(controller, info, context, true);
		}

		public static void SerializationWrite(IClientExpertRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ClientExecRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			ExpertRuntimeProps.SerializationWrite(controller, info, context, true);
		}

	}
	public abstract class ClientExpertRuntimeBase : ClientExecRuntimeBase, IClientExpertRuntime
	{

		bool ___initialized = false;



		public ClientExpertRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ClientExpertRuntimeProps.AddDependencies(this, false);
		}

		public ClientExpertRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ClientExpertRuntimeProps.AddDependencies(this, false);
		}

		public ClientExpertRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ClientExpertRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ClientExpertRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ClientExpertRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ClientExpertRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ClientExpertRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ClientExpertRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Int32 Init();

		public abstract Int32 Deinit();

		public abstract Int32 OnTick();



		public virtual Mt4ExecutableInfo ExpertInfo
		{
			get {
				return Session.ExecutableInfo;
			}
		}

		public new virtual IExpertSession Session
		{
			get {
				return (IExpertSession) ((IUserRuntime)this).Session;
			}
			set {
				((IUserRuntime)this).Session = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ClientExpertRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ClientExpertRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
