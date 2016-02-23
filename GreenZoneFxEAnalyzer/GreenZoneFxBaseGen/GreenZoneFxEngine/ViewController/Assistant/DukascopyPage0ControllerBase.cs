using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class DukascopyPage0ControllerProps
	{
		public const int PROPERTY_17_ERRORPROVIDER1_ID = 17;
		public const int PROPERTY_18_OPENFILEDIALOG1_ID = 18;
		public const int PROPERTY_19_FROMDATEP_ID = 19;
		public const int PROPERTY_20_TODATEP_ID = 20;
		public const int PROPERTY_21_ACCOUNTCURRENCYTB_ID = 21;
		public const int PROPERTY_22_LEVERAGENUPD_ID = 22;
		public const int PROPERTY_23_ACCOUNTNUMBERNUPD_ID = 23;
		public const int PROPERTY_24_NAMETB_ID = 24;
		public const int PROPERTY_25_ACCOUNTNAMETB_ID = 25;
		public const int PROPERTY_26_ACCOUNTCOMPANYTB_ID = 26;
		public const int PROPERTY_27_SYMBOLSCHL_ID = 27;
		public const int PROPERTY_28_IMPORTEDPARAMETERS_ID = 28;
		public const int PROPERTY_29_ENVIRONMENTNAME_ID = 29;
		public const int PROPERTY_30_SELECTEDSYMBOLS_ID = 30;
		public static bool RmiGetProperty(IDukascopyPage0Controller controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case PROPERTY_18_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case PROPERTY_19_FROMDATEP_ID:
					value = controller.FromDateP;
					return true;
				case PROPERTY_20_TODATEP_ID:
					value = controller.ToDateP;
					return true;
				case PROPERTY_21_ACCOUNTCURRENCYTB_ID:
					value = controller.AccountCurrencyTb;
					return true;
				case PROPERTY_22_LEVERAGENUPD_ID:
					value = controller.LeverageNupd;
					return true;
				case PROPERTY_23_ACCOUNTNUMBERNUPD_ID:
					value = controller.AccountNumberNupd;
					return true;
				case PROPERTY_24_NAMETB_ID:
					value = controller.NameTb;
					return true;
				case PROPERTY_25_ACCOUNTNAMETB_ID:
					value = controller.AccountNameTb;
					return true;
				case PROPERTY_26_ACCOUNTCOMPANYTB_ID:
					value = controller.AccountCompanyTb;
					return true;
				case PROPERTY_27_SYMBOLSCHL_ID:
					value = controller.SymbolsChl;
					return true;
				case PROPERTY_28_IMPORTEDPARAMETERS_ID:
					value = controller.ImportedParameters;
					return true;
				case PROPERTY_29_ENVIRONMENTNAME_ID:
					value = controller.EnvironmentName;
					return true;
				case PROPERTY_30_SELECTEDSYMBOLS_ID:
					value = controller.SelectedSymbols;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IDukascopyPage0Controller controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_28_IMPORTEDPARAMETERS_ID:
					controller.ImportedParameters = (String[]) value;
					return true;
				case PROPERTY_29_ENVIRONMENTNAME_ID:
					controller.EnvironmentName = (String) value;
					return true;
				case PROPERTY_30_SELECTEDSYMBOLS_ID:
					controller.SelectedSymbols = (List<String>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IDukascopyPage0Controller controller, GreenRmiObjectBuffer buffer)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID];
			controller.OpenFileDialog1 = (FileDialogController) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID];
			controller.FromDateP = (FieldController<DateTime>) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_19_FROMDATEP_ID];
			controller.ToDateP = (FieldController<DateTime>) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_20_TODATEP_ID];
			controller.AccountCurrencyTb = (LabelledController) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_21_ACCOUNTCURRENCYTB_ID];
			controller.LeverageNupd = (FieldController<Int32>) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_22_LEVERAGENUPD_ID];
			controller.AccountNumberNupd = (FieldController<Int32>) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_23_ACCOUNTNUMBERNUPD_ID];
			controller.NameTb = (FieldController<String>) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_24_NAMETB_ID];
			controller.AccountNameTb = (LabelledController) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_25_ACCOUNTNAMETB_ID];
			controller.AccountCompanyTb = (LabelledController) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_26_ACCOUNTCOMPANYTB_ID];
			controller.SymbolsChl = (ListController) buffer.ChangedProps[DukascopyPage0ControllerProps.PROPERTY_27_SYMBOLSCHL_ID];
		}

		public static void AddDependencies(IDukascopyPage0Controller controller)
		{
			controller.Dependencies.Add(controller.ErrorProvider1);
			controller.Dependencies.Add(controller.OpenFileDialog1);
			controller.Dependencies.Add(controller.FromDateP);
			controller.Dependencies.Add(controller.ToDateP);
			controller.Dependencies.Add(controller.AccountCurrencyTb);
			controller.Dependencies.Add(controller.LeverageNupd);
			controller.Dependencies.Add(controller.AccountNumberNupd);
			controller.Dependencies.Add(controller.NameTb);
			controller.Dependencies.Add(controller.AccountNameTb);
			controller.Dependencies.Add(controller.AccountCompanyTb);
			controller.Dependencies.Add(controller.SymbolsChl);
		}

		public static void SerializationRead(IDukascopyPage0Controller controller, SerializationInfo info, StreamingContext context)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) info.GetValue("ErrorProvider1", typeof(ChildControlMap<String>));
			controller.OpenFileDialog1 = (FileDialogController) info.GetValue("OpenFileDialog1", typeof(FileDialogController));
			controller.FromDateP = (FieldController<DateTime>) info.GetValue("FromDateP", typeof(FieldController<DateTime>));
			controller.ToDateP = (FieldController<DateTime>) info.GetValue("ToDateP", typeof(FieldController<DateTime>));
			controller.AccountCurrencyTb = (LabelledController) info.GetValue("AccountCurrencyTb", typeof(LabelledController));
			controller.LeverageNupd = (FieldController<Int32>) info.GetValue("LeverageNupd", typeof(FieldController<Int32>));
			controller.AccountNumberNupd = (FieldController<Int32>) info.GetValue("AccountNumberNupd", typeof(FieldController<Int32>));
			controller.NameTb = (FieldController<String>) info.GetValue("NameTb", typeof(FieldController<String>));
			controller.AccountNameTb = (LabelledController) info.GetValue("AccountNameTb", typeof(LabelledController));
			controller.AccountCompanyTb = (LabelledController) info.GetValue("AccountCompanyTb", typeof(LabelledController));
			controller.SymbolsChl = (ListController) info.GetValue("SymbolsChl", typeof(ListController));
			controller.ImportedParameters = (String[]) info.GetValue("ImportedParameters", typeof(String[]));
			controller.EnvironmentName = (String) info.GetValue("EnvironmentName", typeof(String));
			controller.SelectedSymbols = (List<String>) info.GetValue("SelectedSymbols", typeof(List<String>));
		}

		public static void SerializationWrite(IDukascopyPage0Controller controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ErrorProvider1", controller.ErrorProvider1);
			info.AddValue("OpenFileDialog1", controller.OpenFileDialog1);
			info.AddValue("FromDateP", controller.FromDateP);
			info.AddValue("ToDateP", controller.ToDateP);
			info.AddValue("AccountCurrencyTb", controller.AccountCurrencyTb);
			info.AddValue("LeverageNupd", controller.LeverageNupd);
			info.AddValue("AccountNumberNupd", controller.AccountNumberNupd);
			info.AddValue("NameTb", controller.NameTb);
			info.AddValue("AccountNameTb", controller.AccountNameTb);
			info.AddValue("AccountCompanyTb", controller.AccountCompanyTb);
			info.AddValue("SymbolsChl", controller.SymbolsChl);
			info.AddValue("ImportedParameters", controller.ImportedParameters);
			info.AddValue("EnvironmentName", controller.EnvironmentName);
			info.AddValue("SelectedSymbols", controller.SelectedSymbols);
		}

	}
	public abstract class DukascopyPage0ControllerBase : AssistantPageController, IDukascopyPage0Controller
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ImportedParametersChanged;
		public event PropertyChangedEventHandler EnvironmentNameChanged;
		public event PropertyChangedEventHandler SelectedSymbolsChanged;

		public DukascopyPage0ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			DukascopyPage0ControllerProps.AddDependencies(this);
		}

		public DukascopyPage0ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			DukascopyPage0ControllerProps.Initialize(this, buffer);
			___initialized = true;
			DukascopyPage0ControllerProps.AddDependencies(this);
		}

		protected DukascopyPage0ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			DukascopyPage0ControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			DukascopyPage0ControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			DukascopyPage0ControllerProps.SerializationWrite(this, info, context);
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
					changed[DukascopyPage0ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FileDialogController openFileDialog1;
		public FileDialogController OpenFileDialog1
		{
			get {
				return openFileDialog1;
			}
			set {
				if (!___initialized) {
					openFileDialog1= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> fromDateP;
		public FieldController<DateTime> FromDateP
		{
			get {
				return fromDateP;
			}
			set {
				if (!___initialized) {
					fromDateP= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_19_FROMDATEP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> toDateP;
		public FieldController<DateTime> ToDateP
		{
			get {
				return toDateP;
			}
			set {
				if (!___initialized) {
					toDateP= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_20_TODATEP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController accountCurrencyTb;
		public LabelledController AccountCurrencyTb
		{
			get {
				return accountCurrencyTb;
			}
			set {
				if (!___initialized) {
					accountCurrencyTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_21_ACCOUNTCURRENCYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<Int32> leverageNupd;
		public FieldController<Int32> LeverageNupd
		{
			get {
				return leverageNupd;
			}
			set {
				if (!___initialized) {
					leverageNupd= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_22_LEVERAGENUPD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<Int32> accountNumberNupd;
		public FieldController<Int32> AccountNumberNupd
		{
			get {
				return accountNumberNupd;
			}
			set {
				if (!___initialized) {
					accountNumberNupd= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_23_ACCOUNTNUMBERNUPD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<String> nameTb;
		public FieldController<String> NameTb
		{
			get {
				return nameTb;
			}
			set {
				if (!___initialized) {
					nameTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_24_NAMETB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController accountNameTb;
		public LabelledController AccountNameTb
		{
			get {
				return accountNameTb;
			}
			set {
				if (!___initialized) {
					accountNameTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_25_ACCOUNTNAMETB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController accountCompanyTb;
		public LabelledController AccountCompanyTb
		{
			get {
				return accountCompanyTb;
			}
			set {
				if (!___initialized) {
					accountCompanyTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_26_ACCOUNTCOMPANYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ListController symbolsChl;
		public ListController SymbolsChl
		{
			get {
				return symbolsChl;
			}
			set {
				if (!___initialized) {
					symbolsChl= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_27_SYMBOLSCHL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String[] importedParameters;
		public String[] ImportedParameters
		{
			get {
				return importedParameters;
			}
			set {
				if (importedParameters != value) {
					importedParameters= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_28_IMPORTEDPARAMETERS_ID] = true;
					if (ImportedParametersChanged != null)
						ImportedParametersChanged(this, new PropertyChangedEventArgs("ImportedParameters", value));
				}
			}
		}

		String environmentName;
		public String EnvironmentName
		{
			get {
				return environmentName;
			}
			set {
				if (environmentName != value) {
					environmentName= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_29_ENVIRONMENTNAME_ID] = true;
					if (EnvironmentNameChanged != null)
						EnvironmentNameChanged(this, new PropertyChangedEventArgs("EnvironmentName", value));
				}
			}
		}

		List<String> selectedSymbols;
		public List<String> SelectedSymbols
		{
			get {
				return selectedSymbols;
			}
			set {
				if (selectedSymbols != value) {
					selectedSymbols= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_30_SELECTEDSYMBOLS_ID] = true;
					if (SelectedSymbolsChanged != null)
						SelectedSymbolsChanged(this, new PropertyChangedEventArgs("SelectedSymbols", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (DukascopyPage0ControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!DukascopyPage0ControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
