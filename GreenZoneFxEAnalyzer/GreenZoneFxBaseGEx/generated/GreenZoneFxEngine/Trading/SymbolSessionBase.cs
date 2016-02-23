using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class SymbolSessionProps
	{
		public const int PROPERTY_1_SYMBOL_ID = 1;
		public const int PROPERTY_2_EATESTINGGLOBALFROM_ID = 2;
		public const int PROPERTY_3_EATESTINGGLOBALTO_ID = 3;
		public const int PROPERTY_4_TESTTYPE_ID = 4;
		public const int PROPERTY_5_DATAPERIOD_ID = 5;
		public static bool RmiGetProperty(ISymbolSession controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SymbolSessionProps.PROPERTY_1_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case SymbolSessionProps.PROPERTY_2_EATESTINGGLOBALFROM_ID:
					value = controller.EATestingGlobalFrom;
					return true;
				case SymbolSessionProps.PROPERTY_3_EATESTINGGLOBALTO_ID:
					value = controller.EATestingGlobalTo;
					return true;
				case SymbolSessionProps.PROPERTY_4_TESTTYPE_ID:
					value = controller.TestType;
					return true;
				case SymbolSessionProps.PROPERTY_5_DATAPERIOD_ID:
					value = controller.DataPeriod;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ISymbolSession controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case SymbolSessionProps.PROPERTY_2_EATESTINGGLOBALFROM_ID:
					controller.EATestingGlobalFrom = (datetime) value;
					return true;
				case SymbolSessionProps.PROPERTY_3_EATESTINGGLOBALTO_ID:
					controller.EATestingGlobalTo = (datetime) value;
					return true;
				case SymbolSessionProps.PROPERTY_4_TESTTYPE_ID:
					controller.TestType = (TestType) value;
					return true;
				case SymbolSessionProps.PROPERTY_5_DATAPERIOD_ID:
					controller.DataPeriod = (TimePeriodConst) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ISymbolSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Symbol = (symbol) buffer.ChangedProps[SymbolSessionProps.PROPERTY_1_SYMBOL_ID];
		}

		public static void AddDependencies(ISymbolSession controller, bool goToParent)
		{
		}

		public static void SerializationRead(ISymbolSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.EATestingGlobalFrom = (datetime) info.GetValue("EATestingGlobalFrom", typeof(datetime));
			controller.EATestingGlobalTo = (datetime) info.GetValue("EATestingGlobalTo", typeof(datetime));
			controller.TestType = (TestType) info.GetValue("TestType", typeof(TestType));
			controller.DataPeriod = (TimePeriodConst) info.GetValue("DataPeriod", typeof(TimePeriodConst));
		}

		public static void SerializationWrite(ISymbolSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("EATestingGlobalFrom", controller.EATestingGlobalFrom);
			info.AddValue("EATestingGlobalTo", controller.EATestingGlobalTo);
			info.AddValue("TestType", controller.TestType);
			info.AddValue("DataPeriod", controller.DataPeriod);
		}

	}
	public abstract class SymbolSessionBase : TradingConst, ISymbolSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ISymbolSession_EATestingGlobalFrom_Changed;
		public event PropertyChangedEventHandler ISymbolSession_EATestingGlobalTo_Changed;
		public event PropertyChangedEventHandler ISymbolSession_TestType_Changed;
		public event PropertyChangedEventHandler ISymbolSession_DataPeriod_Changed;

		public SymbolSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			SymbolSessionProps.AddDependencies(this, false);
		}

		public SymbolSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			SymbolSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			SymbolSessionProps.AddDependencies(this, false);
		}

		protected SymbolSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SymbolSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			SymbolSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			SymbolSessionProps.SerializationWrite(this, info, context, false);
		}


		symbol _ISymbolSession_Symbol;
		public symbol Symbol
		{
			get {
				return _ISymbolSession_Symbol;
			}
			set {
				if (!___initialized) {
					_ISymbolSession_Symbol= value;
					changed[SymbolSessionProps.PROPERTY_1_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		datetime _ISymbolSession_EATestingGlobalFrom;
		public datetime EATestingGlobalFrom
		{
			get {
				return _ISymbolSession_EATestingGlobalFrom;
			}
			set {
				if (_ISymbolSession_EATestingGlobalFrom != value) {
					_ISymbolSession_EATestingGlobalFrom= value;
					changed[SymbolSessionProps.PROPERTY_2_EATESTINGGLOBALFROM_ID] = true;
					if (ISymbolSession_EATestingGlobalFrom_Changed != null)
						ISymbolSession_EATestingGlobalFrom_Changed(this, new PropertyChangedEventArgs("EATestingGlobalFrom", value));
				}
			}
		}

		datetime _ISymbolSession_EATestingGlobalTo;
		public datetime EATestingGlobalTo
		{
			get {
				return _ISymbolSession_EATestingGlobalTo;
			}
			set {
				if (_ISymbolSession_EATestingGlobalTo != value) {
					_ISymbolSession_EATestingGlobalTo= value;
					changed[SymbolSessionProps.PROPERTY_3_EATESTINGGLOBALTO_ID] = true;
					if (ISymbolSession_EATestingGlobalTo_Changed != null)
						ISymbolSession_EATestingGlobalTo_Changed(this, new PropertyChangedEventArgs("EATestingGlobalTo", value));
				}
			}
		}

		TestType _ISymbolSession_TestType;
		public TestType TestType
		{
			get {
				return _ISymbolSession_TestType;
			}
			set {
				if (_ISymbolSession_TestType != value) {
					_ISymbolSession_TestType= value;
					changed[SymbolSessionProps.PROPERTY_4_TESTTYPE_ID] = true;
					if (ISymbolSession_TestType_Changed != null)
						ISymbolSession_TestType_Changed(this, new PropertyChangedEventArgs("TestType", value));
				}
			}
		}

		TimePeriodConst _ISymbolSession_DataPeriod;
		public TimePeriodConst DataPeriod
		{
			get {
				return _ISymbolSession_DataPeriod;
			}
			set {
				if (_ISymbolSession_DataPeriod != value) {
					_ISymbolSession_DataPeriod= value;
					changed[SymbolSessionProps.PROPERTY_5_DATAPERIOD_ID] = true;
					if (ISymbolSession_DataPeriod_Changed != null)
						ISymbolSession_DataPeriod_Changed(this, new PropertyChangedEventArgs("DataPeriod", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (SymbolSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (SymbolSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
