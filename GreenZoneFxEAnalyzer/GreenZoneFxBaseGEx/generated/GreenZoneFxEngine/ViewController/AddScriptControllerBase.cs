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
		public static bool RmiGetProperty(IAddScriptController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case AddScriptControllerProps.PROPERTY_22_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				case AddScriptControllerProps.PROPERTY_23_SYMBOL_ID:
					value = controller.Symbol;
					return true;
				case AddScriptControllerProps.PROPERTY_24_PERIOD_ID:
					value = controller.Period;
					return true;
				case AddScriptControllerProps.PROPERTY_25_SCRIPTCOMBO_ID:
					value = controller.ScriptCombo;
					return true;
				case AddScriptControllerProps.PROPERTY_26_SYMBOLCOMBO_ID:
					value = controller.SymbolCombo;
					return true;
				case AddScriptControllerProps.PROPERTY_27_PERIODCOMBO_ID:
					value = controller.PeriodCombo;
					return true;
				case AddScriptControllerProps.PROPERTY_28_OKBUTTON_ID:
					value = controller.OkButton;
					return true;
				case AddScriptControllerProps.PROPERTY_29_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IAddScriptController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IAddScriptController controller, GreenRmiObjectBuffer buffer, bool goToParent)
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

		public static void AddDependencies(IAddScriptController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.MainWindow);
			controller.Dependencies.Add(controller.ScriptCombo);
			controller.Dependencies.Add(controller.SymbolCombo);
			controller.Dependencies.Add(controller.PeriodCombo);
			controller.Dependencies.Add(controller.OkButton);
			controller.Dependencies.Add(controller.ErrorProvider1);
		}

		public static void SerializationRead(IAddScriptController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IAddScriptController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			AddScriptControllerProps.AddDependencies(this, false);
		}

		public AddScriptControllerBase(GreenRmiManager rmiManager, String text)
			: base(rmiManager, text)
		{
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this, false);
		}

		public AddScriptControllerBase(GreenRmiManager rmiManager, String text, Int32 image)
			: base(rmiManager, text, image)
		{
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this, false);
		}

		public AddScriptControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			AddScriptControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this, false);
		}

		protected AddScriptControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			AddScriptControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			AddScriptControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			AddScriptControllerProps.SerializationWrite(this, info, context, false);
		}


		IMainWindowController _IAddScriptController_MainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return _IAddScriptController_MainWindow;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_MainWindow= value;
					changed[AddScriptControllerProps.PROPERTY_22_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		symbol _IAddScriptController_Symbol;
		public virtual symbol Symbol
		{
			get {
				return _IAddScriptController_Symbol;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_Symbol= value;
					changed[AddScriptControllerProps.PROPERTY_23_SYMBOL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		TimePeriodConst _IAddScriptController_Period;
		public virtual TimePeriodConst Period
		{
			get {
				return _IAddScriptController_Period;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_Period= value;
					changed[AddScriptControllerProps.PROPERTY_24_PERIOD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IAddScriptController_ScriptCombo;
		public ComboController ScriptCombo
		{
			get {
				return _IAddScriptController_ScriptCombo;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_ScriptCombo= value;
					changed[AddScriptControllerProps.PROPERTY_25_SCRIPTCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IAddScriptController_SymbolCombo;
		public ComboController SymbolCombo
		{
			get {
				return _IAddScriptController_SymbolCombo;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_SymbolCombo= value;
					changed[AddScriptControllerProps.PROPERTY_26_SYMBOLCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IAddScriptController_PeriodCombo;
		public ComboController PeriodCombo
		{
			get {
				return _IAddScriptController_PeriodCombo;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_PeriodCombo= value;
					changed[AddScriptControllerProps.PROPERTY_27_PERIODCOMBO_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IAddScriptController_OkButton;
		public ButtonController OkButton
		{
			get {
				return _IAddScriptController_OkButton;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_OkButton= value;
					changed[AddScriptControllerProps.PROPERTY_28_OKBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ChildControlMap<String> _IAddScriptController_ErrorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return _IAddScriptController_ErrorProvider1;
			}
			set {
				if (!___initialized) {
					_IAddScriptController_ErrorProvider1= value;
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
			if (AddScriptControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (AddScriptControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
