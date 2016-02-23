using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Properties
{
	public static class SymbolPropertiesControllerProps
	{
		public static bool RmiGetProperty(ISymbolPropertiesController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolPropertiesController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolPropertiesController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(ISymbolPropertiesController controller)
		{
		}

		public static void SerializationRead(ISymbolPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

		public static void SerializationWrite(ISymbolPropertiesController controller, SerializationInfo info, StreamingContext context)
		{
		}

	}
	public abstract class SymbolPropertiesControllerBase : PropertyPanelController, ISymbolPropertiesController
	{

		bool ___initialized = false;


		public SymbolPropertiesControllerBase(GreenRmiManager rmiManager, Controller parent, Object selectedObject)
			: base(rmiManager, parent, selectedObject)
		{
			___initialized = true;
			SymbolPropertiesControllerProps.AddDependencies(this);
		}

		public SymbolPropertiesControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolPropertiesControllerProps.Initialize(this, buffer);
			___initialized = true;
			SymbolPropertiesControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected SymbolPropertiesControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolPropertiesControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			SymbolPropertiesControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolPropertiesControllerProps.SerializationWrite(this, info, context);
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolPropertiesControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!SymbolPropertiesControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
