using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneParser.Reflect;
using GreenZoneUtil.GreenRmi;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerEnvironmentRuntimeProps
	{
		public static bool RmiGetProperty(IServerEnvironmentRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (EnvironmentRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
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
		public static bool RmiSetProperty(IServerEnvironmentRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (EnvironmentRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerEnvironmentRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				EnvironmentRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerEnvironmentRuntime controller, bool goToParent)
		{
			if (goToParent) {
				EnvironmentRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerEnvironmentRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				EnvironmentRuntimeProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerEnvironmentRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				EnvironmentRuntimeProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerEnvironmentRuntimeBase : EnvironmentRuntimeEx, IServerEnvironmentRuntime
	{

		bool ___initialized = false;


		public ServerEnvironmentRuntimeBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerEnvironmentRuntimeProps.AddDependencies(this, false);
		}

		public ServerEnvironmentRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerEnvironmentRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerEnvironmentRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerEnvironmentRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerEnvironmentRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerEnvironmentRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerEnvironmentRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract void Save();

		public abstract void LoadOrders();

		public abstract void SaveOrders();

		public abstract void SetImportDir(String importDir, Object tester);

		public abstract IServerSymbolContext AddSymbol(params String[] symbolRow);

		public abstract Mt4ExecutableInfo RegisterScript(ReflType scriptType);

		public abstract Mt4ExecutableInfo RegisterEA(ReflType eaType);

		public abstract Mt4ExecutableInfo RegisterIndicator(ReflType indicatorType);

		public abstract void AddChart(IServerChartGroupRuntime chart);

		public abstract void RemoveChart(IServerChartGroupRuntime chart);


		public new virtual IServerEnvironmentSession Session
		{
			get {
				return (IServerEnvironmentSession) ((IEnvironmentRuntime)this).Session;
			}
			set {
				((IEnvironmentRuntime)this).Session = value;
			}
		}

		public new virtual IServerOrdersTable Orders
		{
			get {
				return (IServerOrdersTable) ((IEnvironmentRuntime)this).Orders;
			}
			set {
				((IEnvironmentRuntime)this).Orders = value;
			}
		}

		public abstract ISet<symbol> Symbols
		{
			get ;
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerEnvironmentRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerEnvironmentRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
