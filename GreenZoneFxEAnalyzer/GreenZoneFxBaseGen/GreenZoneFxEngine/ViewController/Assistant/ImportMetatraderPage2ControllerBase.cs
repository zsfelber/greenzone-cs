using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class ImportMetatraderPage2ControllerProps
	{
		public const int PROPERTY_17_ERRORPROVIDER1_ID = 17;
		public const int PROPERTY_18_OPENFILEDIALOG1_ID = 18;
		public const int PROPERTY_19_CHECKBOX1_ID = 19;
		public const int PROPERTY_20_ACCOUNTNAMETB_ID = 20;
		public const int PROPERTY_21_ACCOUNTCOMPANYTB_ID = 21;
		public const int PROPERTY_22_HISTORYDIRECTORYCB_ID = 22;
		public const int PROPERTY_23_NAMETB_ID = 23;
		public const int PROPERTY_24_ACCOUNTNUMBERNUPD_ID = 24;
		public const int PROPERTY_25_ACCOUNTCURRENCYTB_ID = 25;
		public const int PROPERTY_26_LEVERAGENUPD_ID = 26;
		public const int PROPERTY_27_IMPORTEDPARAMETERS_ID = 27;
		public const int PROPERTY_28_HISTORYDIRECTORIES_ID = 28;
		public const int PROPERTY_29_IMPORTEDMETATARDERDIR_ID = 29;
		public const int PROPERTY_30_IMPORTEDMETATARDERVERSION_ID = 30;
		public const int PROPERTY_31_HISTORYDIRECTORY_ID = 31;
		public const int PROPERTY_32_ENVIRONMENTNAME_ID = 32;
		public const int PROPERTY_33_CLOSEMETATRADER_ID = 33;
		public static bool RmiGetProperty(IImportMetatraderPage2Controller controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case PROPERTY_18_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case PROPERTY_19_CHECKBOX1_ID:
					value = controller.CheckBox1;
					return true;
				case PROPERTY_20_ACCOUNTNAMETB_ID:
					value = controller.AccountNameTb;
					return true;
				case PROPERTY_21_ACCOUNTCOMPANYTB_ID:
					value = controller.AccountCompanyTb;
					return true;
				case PROPERTY_22_HISTORYDIRECTORYCB_ID:
					value = controller.HistoryDirectoryCb;
					return true;
				case PROPERTY_23_NAMETB_ID:
					value = controller.NameTb;
					return true;
				case PROPERTY_24_ACCOUNTNUMBERNUPD_ID:
					value = controller.AccountNumberNupd;
					return true;
				case PROPERTY_25_ACCOUNTCURRENCYTB_ID:
					value = controller.AccountCurrencyTb;
					return true;
				case PROPERTY_26_LEVERAGENUPD_ID:
					value = controller.LeverageNupd;
					return true;
				case PROPERTY_27_IMPORTEDPARAMETERS_ID:
					value = controller.ImportedParameters;
					return true;
				case PROPERTY_28_HISTORYDIRECTORIES_ID:
					value = controller.HistoryDirectories;
					return true;
				case PROPERTY_29_IMPORTEDMETATARDERDIR_ID:
					value = controller.ImportedMetatarderDir;
					return true;
				case PROPERTY_30_IMPORTEDMETATARDERVERSION_ID:
					value = controller.ImportedMetatarderVersion;
					return true;
				case PROPERTY_31_HISTORYDIRECTORY_ID:
					value = controller.HistoryDirectory;
					return true;
				case PROPERTY_32_ENVIRONMENTNAME_ID:
					value = controller.EnvironmentName;
					return true;
				case PROPERTY_33_CLOSEMETATRADER_ID:
					value = controller.CloseMetatrader;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IImportMetatraderPage2Controller controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_27_IMPORTEDPARAMETERS_ID:
					controller.ImportedParameters = (String[]) value;
					return true;
				case PROPERTY_28_HISTORYDIRECTORIES_ID:
					controller.HistoryDirectories = (String[]) value;
					return true;
				case PROPERTY_29_IMPORTEDMETATARDERDIR_ID:
					controller.ImportedMetatarderDir = (String) value;
					return true;
				case PROPERTY_30_IMPORTEDMETATARDERVERSION_ID:
					controller.ImportedMetatarderVersion = (String) value;
					return true;
				case PROPERTY_31_HISTORYDIRECTORY_ID:
					controller.HistoryDirectory = (String) value;
					return true;
				case PROPERTY_32_ENVIRONMENTNAME_ID:
					controller.EnvironmentName = (String) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IImportMetatraderPage2Controller controller, GreenRmiObjectBuffer buffer)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID];
			controller.OpenFileDialog1 = (FileDialogController) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID];
			controller.CheckBox1 = (ToggleButtonController) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_19_CHECKBOX1_ID];
			controller.AccountNameTb = (LabelledController) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_20_ACCOUNTNAMETB_ID];
			controller.AccountCompanyTb = (LabelledController) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_21_ACCOUNTCOMPANYTB_ID];
			controller.HistoryDirectoryCb = (ComboController) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_22_HISTORYDIRECTORYCB_ID];
			controller.NameTb = (FieldController<String>) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_23_NAMETB_ID];
			controller.AccountNumberNupd = (FieldController<Int32>) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_24_ACCOUNTNUMBERNUPD_ID];
			controller.AccountCurrencyTb = (LabelledController) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_25_ACCOUNTCURRENCYTB_ID];
			controller.LeverageNupd = (FieldController<Int32>) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_26_LEVERAGENUPD_ID];
			controller.CloseMetatrader = (Boolean) buffer.ChangedProps[ImportMetatraderPage2ControllerProps.PROPERTY_33_CLOSEMETATRADER_ID];
		}

		public static void AddDependencies(IImportMetatraderPage2Controller controller)
		{
			controller.Dependencies.Add(controller.ErrorProvider1);
			controller.Dependencies.Add(controller.OpenFileDialog1);
			controller.Dependencies.Add(controller.CheckBox1);
			controller.Dependencies.Add(controller.AccountNameTb);
			controller.Dependencies.Add(controller.AccountCompanyTb);
			controller.Dependencies.Add(controller.HistoryDirectoryCb);
			controller.Dependencies.Add(controller.NameTb);
			controller.Dependencies.Add(controller.AccountNumberNupd);
			controller.Dependencies.Add(controller.AccountCurrencyTb);
			controller.Dependencies.Add(controller.LeverageNupd);
		}

		public static void SerializationRead(IImportMetatraderPage2Controller controller, SerializationInfo info, StreamingContext context)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) info.GetValue("ErrorProvider1", typeof(ChildControlMap<String>));
			controller.OpenFileDialog1 = (FileDialogController) info.GetValue("OpenFileDialog1", typeof(FileDialogController));
			controller.CheckBox1 = (ToggleButtonController) info.GetValue("CheckBox1", typeof(ToggleButtonController));
			controller.AccountNameTb = (LabelledController) info.GetValue("AccountNameTb", typeof(LabelledController));
			controller.AccountCompanyTb = (LabelledController) info.GetValue("AccountCompanyTb", typeof(LabelledController));
			controller.HistoryDirectoryCb = (ComboController) info.GetValue("HistoryDirectoryCb", typeof(ComboController));
			controller.NameTb = (FieldController<String>) info.GetValue("NameTb", typeof(FieldController<String>));
			controller.AccountNumberNupd = (FieldController<Int32>) info.GetValue("AccountNumberNupd", typeof(FieldController<Int32>));
			controller.AccountCurrencyTb = (LabelledController) info.GetValue("AccountCurrencyTb", typeof(LabelledController));
			controller.LeverageNupd = (FieldController<Int32>) info.GetValue("LeverageNupd", typeof(FieldController<Int32>));
			controller.ImportedParameters = (String[]) info.GetValue("ImportedParameters", typeof(String[]));
			controller.HistoryDirectories = (String[]) info.GetValue("HistoryDirectories", typeof(String[]));
			controller.ImportedMetatarderDir = (String) info.GetValue("ImportedMetatarderDir", typeof(String));
			controller.ImportedMetatarderVersion = (String) info.GetValue("ImportedMetatarderVersion", typeof(String));
			controller.HistoryDirectory = (String) info.GetValue("HistoryDirectory", typeof(String));
			controller.EnvironmentName = (String) info.GetValue("EnvironmentName", typeof(String));
			controller.CloseMetatrader = (Boolean) info.GetValue("CloseMetatrader", typeof(Boolean));
		}

		public static void SerializationWrite(IImportMetatraderPage2Controller controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ErrorProvider1", controller.ErrorProvider1);
			info.AddValue("OpenFileDialog1", controller.OpenFileDialog1);
			info.AddValue("CheckBox1", controller.CheckBox1);
			info.AddValue("AccountNameTb", controller.AccountNameTb);
			info.AddValue("AccountCompanyTb", controller.AccountCompanyTb);
			info.AddValue("HistoryDirectoryCb", controller.HistoryDirectoryCb);
			info.AddValue("NameTb", controller.NameTb);
			info.AddValue("AccountNumberNupd", controller.AccountNumberNupd);
			info.AddValue("AccountCurrencyTb", controller.AccountCurrencyTb);
			info.AddValue("LeverageNupd", controller.LeverageNupd);
			info.AddValue("ImportedParameters", controller.ImportedParameters);
			info.AddValue("HistoryDirectories", controller.HistoryDirectories);
			info.AddValue("ImportedMetatarderDir", controller.ImportedMetatarderDir);
			info.AddValue("ImportedMetatarderVersion", controller.ImportedMetatarderVersion);
			info.AddValue("HistoryDirectory", controller.HistoryDirectory);
			info.AddValue("EnvironmentName", controller.EnvironmentName);
			info.AddValue("CloseMetatrader", controller.CloseMetatrader);
		}

	}
	public abstract class ImportMetatraderPage2ControllerBase : AssistantPageController, IImportMetatraderPage2Controller
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ImportedParametersChanged;
		public event PropertyChangedEventHandler HistoryDirectoriesChanged;
		public event PropertyChangedEventHandler ImportedMetatarderDirChanged;
		public event PropertyChangedEventHandler ImportedMetatarderVersionChanged;
		public event PropertyChangedEventHandler HistoryDirectoryChanged;
		public event PropertyChangedEventHandler EnvironmentNameChanged;

		public ImportMetatraderPage2ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			ImportMetatraderPage2ControllerProps.AddDependencies(this);
		}

		public ImportMetatraderPage2ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ImportMetatraderPage2ControllerProps.Initialize(this, buffer);
			___initialized = true;
			ImportMetatraderPage2ControllerProps.AddDependencies(this);
		}

		protected ImportMetatraderPage2ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ImportMetatraderPage2ControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ImportMetatraderPage2ControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ImportMetatraderPage2ControllerProps.SerializationWrite(this, info, context);
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController checkBox1;
		public ToggleButtonController CheckBox1
		{
			get {
				return checkBox1;
			}
			set {
				if (!___initialized) {
					checkBox1= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_19_CHECKBOX1_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_20_ACCOUNTNAMETB_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_21_ACCOUNTCOMPANYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController historyDirectoryCb;
		public ComboController HistoryDirectoryCb
		{
			get {
				return historyDirectoryCb;
			}
			set {
				if (!___initialized) {
					historyDirectoryCb= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_22_HISTORYDIRECTORYCB_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_23_NAMETB_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_24_ACCOUNTNUMBERNUPD_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_25_ACCOUNTCURRENCYTB_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_26_LEVERAGENUPD_ID] = true;
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_27_IMPORTEDPARAMETERS_ID] = true;
					if (ImportedParametersChanged != null)
						ImportedParametersChanged(this, new PropertyChangedEventArgs("ImportedParameters", value));
				}
			}
		}

		String[] historyDirectories;
		public String[] HistoryDirectories
		{
			get {
				return historyDirectories;
			}
			set {
				if (historyDirectories != value) {
					historyDirectories= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_28_HISTORYDIRECTORIES_ID] = true;
					if (HistoryDirectoriesChanged != null)
						HistoryDirectoriesChanged(this, new PropertyChangedEventArgs("HistoryDirectories", value));
				}
			}
		}

		String importedMetatarderDir;
		public String ImportedMetatarderDir
		{
			get {
				return importedMetatarderDir;
			}
			set {
				if (importedMetatarderDir != value) {
					importedMetatarderDir= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_29_IMPORTEDMETATARDERDIR_ID] = true;
					if (ImportedMetatarderDirChanged != null)
						ImportedMetatarderDirChanged(this, new PropertyChangedEventArgs("ImportedMetatarderDir", value));
				}
			}
		}

		String importedMetatarderVersion;
		public String ImportedMetatarderVersion
		{
			get {
				return importedMetatarderVersion;
			}
			set {
				if (importedMetatarderVersion != value) {
					importedMetatarderVersion= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_30_IMPORTEDMETATARDERVERSION_ID] = true;
					if (ImportedMetatarderVersionChanged != null)
						ImportedMetatarderVersionChanged(this, new PropertyChangedEventArgs("ImportedMetatarderVersion", value));
				}
			}
		}

		String historyDirectory;
		public String HistoryDirectory
		{
			get {
				return historyDirectory;
			}
			set {
				if (historyDirectory != value) {
					historyDirectory= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_31_HISTORYDIRECTORY_ID] = true;
					if (HistoryDirectoryChanged != null)
						HistoryDirectoryChanged(this, new PropertyChangedEventArgs("HistoryDirectory", value));
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
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_32_ENVIRONMENTNAME_ID] = true;
					if (EnvironmentNameChanged != null)
						EnvironmentNameChanged(this, new PropertyChangedEventArgs("EnvironmentName", value));
				}
			}
		}

		Boolean closeMetatrader;
		public Boolean CloseMetatrader
		{
			get {
				return closeMetatrader;
			}
			set {
				if (!___initialized) {
					closeMetatrader= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_33_CLOSEMETATRADER_ID] = true;
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
			if (ImportMetatraderPage2ControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ImportMetatraderPage2ControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
