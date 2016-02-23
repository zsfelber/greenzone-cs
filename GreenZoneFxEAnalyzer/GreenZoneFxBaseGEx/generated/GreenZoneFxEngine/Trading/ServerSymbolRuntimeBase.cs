using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerSymbolRuntimeProps
	{
		public static bool RmiGetProperty(IServerSymbolRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (SymbolRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerSymbolRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (SymbolRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerSymbolRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				SymbolRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerSymbolRuntime controller, bool goToParent)
		{
			if (goToParent) {
				SymbolRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerSymbolRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SymbolRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerSymbolRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				SymbolRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerSymbolRuntimeBase : SymbolRuntimeEx, IServerSymbolRuntime
	{

		bool ___initialized = false;


		internal ServerSymbolRuntimeBase(GreenRmiManager rmiManager, IEnvironmentRuntime parent, ISymbolContext context)
			: base(rmiManager, parent, context)
		{
			___initialized = true;
			ServerSymbolRuntimeProps.AddDependencies(this, false);
		}

		internal ServerSymbolRuntimeBase(GreenRmiManager rmiManager, IEnvironmentRuntime parent, ISymbolContext context, ISymbolSession session)
			: base(rmiManager, parent, context, session)
		{
			___initialized = true;
			ServerSymbolRuntimeProps.AddDependencies(this, false);
		}

		public ServerSymbolRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerSymbolRuntimeProps.AddDependencies(this, false);
		}

		public ServerSymbolRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerSymbolRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerSymbolRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerSymbolRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerSymbolRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerSymbolRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerSymbolRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract ITimeSeriesRuntime LoadSeries(TimePeriodConst period, datetime focusedTime);

		public abstract void LoadOrders();

		public abstract void SaveOrders();

		public abstract void Tick(Double Bid, Double Ask, Double Volume);


		public new virtual IServerSymbolContext Context
		{
			get {
				return (IServerSymbolContext) ((ISymbolRuntime)this).Context;
			}
			set {
				((ISymbolRuntime)this).Context = value;
			}
		}

		public abstract TimePeriodConst BestCursorPeriod
		{
			get ;
		}

		public new virtual IServerOrdersTable Orders
		{
			get {
				return (IServerOrdersTable) ((ISymbolRuntime)this).Orders;
			}
			set {
				((ISymbolRuntime)this).Orders = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerSymbolRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerSymbolRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
