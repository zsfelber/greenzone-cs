using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerSymbolContextProps
	{
		public static bool RmiGetProperty(IServerSymbolContext controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (SymbolContextProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerSymbolContext controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (SymbolContextProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerSymbolContext controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				SymbolContextProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerSymbolContext controller, bool goToParent)
		{
			if (goToParent) {
				SymbolContextProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerSymbolContext controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SymbolContextProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerSymbolContext controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SymbolContextProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerSymbolContextBase : SymbolContextEx, IServerSymbolContext
	{

		bool ___initialized = false;


		internal ServerSymbolContextBase(GreenRmiManager rmiManager, IEnvironmentRuntime parent, symbol symbol)
			: base(rmiManager, parent, symbol)
		{
			___initialized = true;
			ServerSymbolContextProps.AddDependencies(this, false);
		}

		public ServerSymbolContextBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerSymbolContextProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerSymbolContextProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerSymbolContextBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerSymbolContextProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerSymbolContextProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerSymbolContextProps.SerializationWrite(this, info, context, false);
		}

		public abstract void Tick(Double Bid, Double Ask, Double Volume);

		public abstract void Bar(TimePeriodConst Period, Double Open, Double Low, Double High, Double Close, Double Volume, Int32 offset);


		public new virtual IServerSymbolRuntime Runtime
		{
			get {
				return (IServerSymbolRuntime) ((ISymbolContext)this).Runtime;
			}
			set {
				((ISymbolContext)this).Runtime = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerSymbolContextProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerSymbolContextProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
