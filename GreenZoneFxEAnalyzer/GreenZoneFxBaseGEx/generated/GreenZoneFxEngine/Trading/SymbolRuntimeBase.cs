using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class SymbolRuntimeProps
	{
		public const int PROPERTY_1_PARENT_ID = 1;
		public const int PROPERTY_2_SESSION_ID = 2;
		public const int PROPERTY_3_CONTEXT_ID = 3;
		public const int PROPERTY_4_ORDERS_ID = 4;
		public static bool RmiGetProperty(ISymbolRuntime controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SymbolRuntimeProps.PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case SymbolRuntimeProps.PROPERTY_2_SESSION_ID:
					value = controller.Session;
					return true;
				case SymbolRuntimeProps.PROPERTY_3_CONTEXT_ID:
					value = controller.Context;
					return true;
				case SymbolRuntimeProps.PROPERTY_4_ORDERS_ID:
					value = controller.Orders;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolRuntime controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Parent = (IEnvironmentRuntime) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_1_PARENT_ID];
			controller.Session = (ISymbolSession) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_2_SESSION_ID];
			controller.Context = (ISymbolContext) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_3_CONTEXT_ID];
			controller.Orders = (IOrdersTable) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_4_ORDERS_ID];
		}

		public static void AddDependencies(ISymbolRuntime controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.Add(controller.Context);
			controller.Dependencies.Add(controller.Orders);
		}

		public static void SerializationRead(ISymbolRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Parent = (IEnvironmentRuntime) info.GetValue("Parent", typeof(IEnvironmentRuntime));
			controller.Session = (ISymbolSession) info.GetValue("Session", typeof(ISymbolSession));
			controller.Context = (ISymbolContext) info.GetValue("Context", typeof(ISymbolContext));
			controller.Orders = (IOrdersTable) info.GetValue("Orders", typeof(IOrdersTable));
		}

		public static void SerializationWrite(ISymbolRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("Session", controller.Session);
			info.AddValue("Context", controller.Context);
			info.AddValue("Orders", controller.Orders);
		}

	}
	public abstract class SymbolRuntimeBase : TradingConst, ISymbolRuntime
	{

		bool ___initialized = false;


		public SymbolRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SymbolRuntimeProps.AddDependencies(this, false);
		}

		public SymbolRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			SymbolRuntimeProps.AddDependencies(this, false);
		}

		protected SymbolRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			SymbolRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolRuntimeProps.SerializationWrite(this, info, context, false);
		}


		IEnvironmentRuntime _ISymbolRuntime_Parent;
		public IEnvironmentRuntime Parent
		{
			get {
				return _ISymbolRuntime_Parent;
			}
			set {
				if (!___initialized) {
					_ISymbolRuntime_Parent= value;
					changed[SymbolRuntimeProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolSession _ISymbolRuntime_Session;
		public ISymbolSession Session
		{
			get {
				return _ISymbolRuntime_Session;
			}
			set {
				if (!___initialized) {
					_ISymbolRuntime_Session= value;
					changed[SymbolRuntimeProps.PROPERTY_2_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolContext _ISymbolRuntime_Context;
		public ISymbolContext Context
		{
			get {
				return _ISymbolRuntime_Context;
			}
			set {
				if (!___initialized) {
					_ISymbolRuntime_Context= value;
					changed[SymbolRuntimeProps.PROPERTY_3_CONTEXT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual symbol Symbol
		{
			get {
				return Session.Symbol;
			}
		}

		IOrdersTable _ISymbolRuntime_Orders;
		public IOrdersTable Orders
		{
			get {
				return _ISymbolRuntime_Orders;
			}
			set {
				if (!___initialized) {
					_ISymbolRuntime_Orders= value;
					changed[SymbolRuntimeProps.PROPERTY_4_ORDERS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public abstract List<IHistoryOrderEtc> HistoryOrders
		{
			get ;
		}

		public virtual Boolean Online
		{
			get {
				return Parent.EnvironmentType.IsOnline();
			}
		}

		public abstract String SymbolFormat
		{
			get ;
		}

		public abstract TimePeriodConst BestPeriod
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (SymbolRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
