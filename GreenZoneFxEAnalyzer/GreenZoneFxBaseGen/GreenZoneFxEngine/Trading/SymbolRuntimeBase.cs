using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class SymbolRuntimeProps
	{
		public const int PROPERTY_1_PARENT_ID = 1;
		public const int PROPERTY_2_SESSION_ID = 2;
		public const int PROPERTY_3_CONTEXT_ID = 3;
		public const int PROPERTY_4_SYMBOL_ID = 4;
		public const int PROPERTY_5_SYMBOLFORMAT_ID = 5;
		public static bool RmiGetProperty(ISymbolRuntime controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_PARENT_ID:
					value = controller.Parent;
					return true;
				case PROPERTY_2_SESSION_ID:
					value = controller.Session;
					return true;
				case PROPERTY_3_CONTEXT_ID:
					value = controller.Context;
					return true;
				case PROPERTY_4_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_5_SYMBOLFORMAT_ID:
					value = controller.SymbolFormat;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolRuntime controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolRuntime controller, GreenRmiObjectBuffer buffer)
		{
			controller.Parent = (IEnvironmentRuntime) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_1_PARENT_ID];
			controller.Session = (ISymbolSession) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_2_SESSION_ID];
			controller.Context = (ISymbolContext) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_3_CONTEXT_ID];
			controller.Symbol = (symbol) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_4_SYMBOL_ID];
			controller.SymbolFormat = (String) buffer.ChangedProps[SymbolRuntimeProps.PROPERTY_5_SYMBOLFORMAT_ID];
		}

		public static void AddDependencies(ISymbolRuntime controller)
		{
			controller.Dependencies.Add(controller.Parent);
			controller.Dependencies.Add(controller.Session);
			controller.Dependencies.Add(controller.Context);
		}

		public static void SerializationRead(ISymbolRuntime controller, SerializationInfo info, StreamingContext context)
		{
			controller.Parent = (IEnvironmentRuntime) info.GetValue("Parent", typeof(IEnvironmentRuntime));
			controller.Session = (ISymbolSession) info.GetValue("Session", typeof(ISymbolSession));
			controller.Context = (ISymbolContext) info.GetValue("Context", typeof(ISymbolContext));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.SymbolFormat = (String) info.GetValue("SymbolFormat", typeof(String));
		}

		public static void SerializationWrite(ISymbolRuntime controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Parent", controller.Parent);
			info.AddValue("Session", controller.Session);
			info.AddValue("Context", controller.Context);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("SymbolFormat", controller.SymbolFormat);
		}

	}
	public abstract class SymbolRuntimeBase : TradingConst, ISymbolRuntime
	{

		bool ___initialized = false;


		public SymbolRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SymbolRuntimeProps.AddDependencies(this);
		}

		public SymbolRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolRuntimeProps.Initialize(this, buffer);
			___initialized = true;
			SymbolRuntimeProps.AddDependencies(this);
		}

		protected SymbolRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolRuntimeProps.SerializationRead(this, info, context);
			___initialized = true;
			SymbolRuntimeProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolRuntimeProps.SerializationWrite(this, info, context);
		}

		IEnvironmentRuntime parent;
		public IEnvironmentRuntime Parent
		{
			get {
				return parent;
			}
			set {
				if (!___initialized) {
					parent= value;
					changed[SymbolRuntimeProps.PROPERTY_1_PARENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolSession session;
		public ISymbolSession Session
		{
			get {
				return session;
			}
			set {
				if (!___initialized) {
					session= value;
					changed[SymbolRuntimeProps.PROPERTY_2_SESSION_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISymbolContext context;
		public ISymbolContext Context
		{
			get {
				return context;
			}
			set {
				if (!___initialized) {
					context= value;
					changed[SymbolRuntimeProps.PROPERTY_3_CONTEXT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol symbol;
		public symbol Symbol
		{
			get {
				return symbol;
			}
			set {
				if (!___initialized) {
					symbol= value;
					changed[SymbolRuntimeProps.PROPERTY_4_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String symbolFormat;
		public String SymbolFormat
		{
			get {
				return symbolFormat;
			}
			set {
				if (!___initialized) {
					symbolFormat= value;
					changed[SymbolRuntimeProps.PROPERTY_5_SYMBOLFORMAT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolRuntimeProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!SymbolRuntimeProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
