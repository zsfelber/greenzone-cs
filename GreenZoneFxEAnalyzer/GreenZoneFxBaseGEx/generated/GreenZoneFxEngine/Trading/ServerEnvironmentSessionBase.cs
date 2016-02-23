using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerEnvironmentSessionProps
	{
		public static bool RmiGetProperty(IServerEnvironmentSession controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (EnvironmentSessionProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerEnvironmentSession controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (EnvironmentSessionProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerEnvironmentSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				EnvironmentSessionProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerEnvironmentSession controller, bool goToParent)
		{
			if (goToParent) {
				EnvironmentSessionProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerEnvironmentSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				EnvironmentSessionProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerEnvironmentSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				EnvironmentSessionProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerEnvironmentSessionBase : EnvironmentSessionBase, IServerEnvironmentSession
	{

		bool ___initialized = false;


		public ServerEnvironmentSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerEnvironmentSessionProps.AddDependencies(this, false);
		}

		public ServerEnvironmentSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerEnvironmentSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerEnvironmentSessionProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerEnvironmentSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerEnvironmentSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerEnvironmentSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerEnvironmentSessionProps.SerializationWrite(this, info, context, false);
		}

		public abstract Boolean Save();

		public abstract void AddSymbol(ISymbolSession symbolSession);

		public abstract void AddChart(IChartGroupSession chartSession);

		public abstract void RemoveChart(IChartGroupSession chart);



		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerEnvironmentSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerEnvironmentSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
