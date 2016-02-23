using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class AddScriptControllerProps
	{
		public const int PROPERTY_22_MAINWINDOW_ID = 22;
		public const int PROPERTY_23_SYMBOL_ID = 23;
		public const int PROPERTY_24_PERIOD_ID = 24;
		public const int PROPERTY_25_SCRIPTCOMBO_ID = 25;
		public const int PROPERTY_26_SYMBOLCOMBO_ID = 26;
		public const int PROPERTY_27_PERIODCOMBO_ID = 27;
		public const int PROPERTY_28_OKBUTTON_ID = 28;
		public const int PROPERTY_29_ERRORPROVIDER1_ID = 29;
		public static bool RmiGetProperty(IAddScriptController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_22_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case PROPERTY_23_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case PROPERTY_24_PERIOD_ID:
					value = controller.Period;
					return true;
				case PROPERTY_25_SCRIPTCOMBO_ID:
					value = controller.ScriptCombo;
					return true;
				case PROPERTY_26_SYMBOLCOMBO_ID:
					value = controller.SymbolCombo;
					return true;
				case PROPERTY_27_PERIODCOMBO_ID:
					value = controller.PeriodCombo;
					return true;
				case PROPERTY_28_OKBUTTON_ID:
					value = controller.OkButton;
					return true;
				case PROPERTY_29_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IAddScriptController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IAddScriptController controller, GreenRmiObjectBuffer buffer)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_22_MAINWINDOW_ID];
			controller.Symbol = (symbol) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_23_SYMBOL_ID];
			controller.Period = (TimePeriodConst) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_24_PERIOD_ID];
			controller.ScriptCombo = (ComboController) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_25_SCRIPTCOMBO_ID];
			controller.SymbolCombo = (ComboController) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_26_SYMBOLCOMBO_ID];
			controller.PeriodCombo = (ComboController) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_27_PERIODCOMBO_ID];
			controller.OkButton = (ButtonController) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_28_OKBUTTON_ID];
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[AddScriptControllerProps.PROPERTY_29_ERRORPROVIDER1_ID];
		}

		public static void AddDependencies(IAddScriptController controller)
		{
			controller.Dependencies.Add(controller.MainWindow);
			controller.Dependencies.Add(controller.ScriptCombo);
			controller.Dependencies.Add(controller.SymbolCombo);
			controller.Dependencies.Add(controller.PeriodCombo);
			controller.Dependencies.Add(controller.OkButton);
			controller.Dependencies.Add(controller.ErrorProvider1);
		}

		public static void SerializationRead(IAddScriptController controller, SerializationInfo info, StreamingContext context)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
			controller.Symbol = (symbol) info.GetValue("Symbol", typeof(symbol));
			controller.Period = (TimePeriodConst) info.GetValue("Period", typeof(TimePeriodConst));
			controller.ScriptCombo = (ComboController) info.GetValue("ScriptCombo", typeof(ComboController));
			controller.SymbolCombo = (ComboController) info.GetValue("SymbolCombo", typeof(ComboController));
			controller.PeriodCombo = (ComboController) info.GetValue("PeriodCombo", typeof(ComboController));
			controller.OkButton = (ButtonController) info.GetValue("OkButton", typeof(ButtonController));
			controller.ErrorProvider1 = (ChildControlMap<String>) info.GetValue("ErrorProvider1", typeof(ChildControlMap<String>));
		}

		public static void SerializationWrite(IAddScriptController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("MainWindow", controller.MainWindow);
			info.AddValue("Symbol", controller.Symbol);
			info.AddValue("Period", controller.Period);
			info.AddValue("ScriptCombo", controller.ScriptCombo);
			info.AddValue("SymbolCombo", controller.SymbolCombo);
			info.AddValue("PeriodCombo", controller.PeriodCombo);
			info.AddValue("OkButton", controller.OkButton);
			info.AddValue("ErrorProvider1", controller.ErrorProvider1);
		}

	}
	public abstract class AddScriptControllerBase : DialogController, IAddScriptController
	{

		bool ___initialized = false;


		public AddScriptControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this);
		}

		public AddScriptControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this);
		}

		public AddScriptControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this);
		}

		public AddScriptControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			AddScriptControllerProps.Initialize(this, buffer);
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this);
		}

		protected AddScriptControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			AddScriptControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			AddScriptControllerProps.SerializationWrite(this, info, context);
		}

		IMainWindowController mainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return mainWindow;
			}
			set {
				if (!___initialized) {
					mainWindow= value;
					changed[AddScriptControllerProps.PROPERTY_22_MAINWINDOW_ID] = true;
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
					changed[AddScriptControllerProps.PROPERTY_23_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TimePeriodConst period;
		public TimePeriodConst Period
		{
			get {
				return period;
			}
			set {
				if (!___initialized) {
					period= value;
					changed[AddScriptControllerProps.PROPERTY_24_PERIOD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController scriptCombo;
		public ComboController ScriptCombo
		{
			get {
				return scriptCombo;
			}
			set {
				if (!___initialized) {
					scriptCombo= value;
					changed[AddScriptControllerProps.PROPERTY_25_SCRIPTCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController symbolCombo;
		public ComboController SymbolCombo
		{
			get {
				return symbolCombo;
			}
			set {
				if (!___initialized) {
					symbolCombo= value;
					changed[AddScriptControllerProps.PROPERTY_26_SYMBOLCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController periodCombo;
		public ComboController PeriodCombo
		{
			get {
				return periodCombo;
			}
			set {
				if (!___initialized) {
					periodCombo= value;
					changed[AddScriptControllerProps.PROPERTY_27_PERIODCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController okButton;
		public ButtonController OkButton
		{
			get {
				return okButton;
			}
			set {
				if (!___initialized) {
					okButton= value;
					changed[AddScriptControllerProps.PROPERTY_28_OKBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ChildControlMap<String> errorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return errorProvider1;
			}
			set {
				if (!___initialized) {
					errorProvider1= value;
					changed[AddScriptControllerProps.PROPERTY_29_ERRORPROVIDER1_ID] = true;
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
			if (AddScriptControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!AddScriptControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
