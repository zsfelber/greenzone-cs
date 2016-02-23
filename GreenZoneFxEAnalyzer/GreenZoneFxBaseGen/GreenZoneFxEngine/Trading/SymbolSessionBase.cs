using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class SymbolSessionProps
	{
		public static bool RmiGetProperty(ISymbolSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolSession controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(ISymbolSession controller)
		{
		}

		public static void SerializationRead(ISymbolSession controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(ISymbolSession controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class SymbolSessionBase : TradingConst, ISymbolSession
	{

		bool ___initialized = false;


		public SymbolSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SymbolSessionProps.AddDependencies(this);
		}

		public SymbolSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolSessionProps.Initialize(this, buffer);
			___initialized = true;
			SymbolSessionProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected SymbolSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			SymbolSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolSessionProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!SymbolSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
