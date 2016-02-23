using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExpertRuntimeProps
	{
		public const int PROPERTY_11_SESSION_ID = 11;
		public static bool RmiGetProperty(IExpertRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_11_SESSION_ID:
					value = controller.Session;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExpertRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IExpertRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Session = (IExpertSession) buffer.ChangedProps[ExpertRuntimeProps.PROPERTY_11_SESSION_ID];
		}

		public static void AddDependencies(IExpertRuntime controller)
		{
			controller.Dependencies.Add(controller.Session);
		}

		public static void SerializationRead(IExpertRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Session = (IExpertSession) info.GetValue("Session", typeof(IExpertSession));
		}

		public static void SerializationWrite(IExpertRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Session", controller.Session);
		}

	}
	public abstract class ExpertRuntimeBase : ExecRuntimeBase, IExpertRuntime
	{

		bool ___initialized = false;


		public ExpertRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this);
		}

		public ExpertRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExpertRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this);
		}

		protected ExpertRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExpertRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			ExpertRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExpertRuntimeProps.SerializationWrite(this, info, context);
		}

		IExpertSession session;
		public IExpertSession Session
		{
			get {
				return session;
			}
			set {
				if (!___initialized) {
					session= value;
					changed[ExpertRuntimeProps.PROPERTY_11_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual Mt4ExecutableInfo ExpertInfo
		{
			get {
				return Session.ExpertInfo;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExpertRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExpertRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
