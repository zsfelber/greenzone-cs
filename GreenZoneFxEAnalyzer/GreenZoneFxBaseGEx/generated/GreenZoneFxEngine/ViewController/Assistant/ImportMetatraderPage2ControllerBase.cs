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
		public static bool RmiGetProperty(IImportMetatraderPage2Controller controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ImportMetatraderPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_19_CHECKBOX1_ID:
					value = controller.CheckBox1;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_20_ACCOUNTNAMETB_ID:
					value = controller.AccountNameTb;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_21_ACCOUNTCOMPANYTB_ID:
					value = controller.AccountCompanyTb;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_22_HISTORYDIRECTORYCB_ID:
					value = controller.HistoryDirectoryCb;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_23_NAMETB_ID:
					value = controller.NameTb;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_24_ACCOUNTNUMBERNUPD_ID:
					value = controller.AccountNumberNupd;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_25_ACCOUNTCURRENCYTB_ID:
					value = controller.AccountCurrencyTb;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_26_LEVERAGENUPD_ID:
					value = controller.LeverageNupd;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_27_IMPORTEDPARAMETERS_ID:
					value = controller.ImportedParameters;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_28_HISTORYDIRECTORIES_ID:
					value = controller.HistoryDirectories;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_29_IMPORTEDMETATARDERDIR_ID:
					value = controller.ImportedMetatarderDir;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_30_IMPORTEDMETATARDERVERSION_ID:
					value = controller.ImportedMetatarderVersion;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_31_HISTORYDIRECTORY_ID:
					value = controller.HistoryDirectory;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_32_ENVIRONMENTNAME_ID:
					value = controller.EnvironmentName;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_33_CLOSEMETATRADER_ID:
					value = controller.CloseMetatrader;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IImportMetatraderPage2Controller controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ImportMetatraderPage2ControllerProps.PROPERTY_27_IMPORTEDPARAMETERS_ID:
					controller.ImportedParameters = (String[]) value;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_28_HISTORYDIRECTORIES_ID:
					controller.HistoryDirectories = (String[]) value;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_29_IMPORTEDMETATARDERDIR_ID:
					controller.ImportedMetatarderDir = (String) value;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_30_IMPORTEDMETATARDERVERSION_ID:
					controller.ImportedMetatarderVersion = (String) value;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_31_HISTORYDIRECTORY_ID:
					controller.HistoryDirectory = (String) value;
					return true;
				case ImportMetatraderPage2ControllerProps.PROPERTY_32_ENVIRONMENTNAME_ID:
					controller.EnvironmentName = (String) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IImportMetatraderPage2Controller controller, GreenRmiObjectBuffer buffer, bool goToParent)
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

		public static void AddDependencies(IImportMetatraderPage2Controller controller, bool goToParent)
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

		public static void SerializationRead(IImportMetatraderPage2Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IImportMetatraderPage2Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IImportMetatraderPage2Controller_ImportedParameters_Changed;
		public event PropertyChangedEventHandler IImportMetatraderPage2Controller_HistoryDirectories_Changed;
		public event PropertyChangedEventHandler IImportMetatraderPage2Controller_ImportedMetatarderDir_Changed;
		public event PropertyChangedEventHandler IImportMetatraderPage2Controller_ImportedMetatarderVersion_Changed;
		public event PropertyChangedEventHandler IImportMetatraderPage2Controller_HistoryDirectory_Changed;
		public event PropertyChangedEventHandler IImportMetatraderPage2Controller_EnvironmentName_Changed;

		public ImportMetatraderPage2ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			ImportMetatraderPage2ControllerProps.AddDependencies(this, false);
		}

		public ImportMetatraderPage2ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ImportMetatraderPage2ControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ImportMetatraderPage2ControllerProps.AddDependencies(this, false);
		}

		protected ImportMetatraderPage2ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ImportMetatraderPage2ControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ImportMetatraderPage2ControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ImportMetatraderPage2ControllerProps.SerializationWrite(this, info, context, false);
		}


		ChildControlMap<String> _IImportMetatraderPage2Controller_ErrorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return _IImportMetatraderPage2Controller_ErrorProvider1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_ErrorProvider1= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FileDialogController _IImportMetatraderPage2Controller_OpenFileDialog1;
		public FileDialogController OpenFileDialog1
		{
			get {
				return _IImportMetatraderPage2Controller_OpenFileDialog1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_OpenFileDialog1= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IImportMetatraderPage2Controller_CheckBox1;
		public ToggleButtonController CheckBox1
		{
			get {
				return _IImportMetatraderPage2Controller_CheckBox1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_CheckBox1= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_19_CHECKBOX1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IImportMetatraderPage2Controller_AccountNameTb;
		public LabelledController AccountNameTb
		{
			get {
				return _IImportMetatraderPage2Controller_AccountNameTb;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_AccountNameTb= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_20_ACCOUNTNAMETB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IImportMetatraderPage2Controller_AccountCompanyTb;
		public LabelledController AccountCompanyTb
		{
			get {
				return _IImportMetatraderPage2Controller_AccountCompanyTb;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_AccountCompanyTb= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_21_ACCOUNTCOMPANYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IImportMetatraderPage2Controller_HistoryDirectoryCb;
		public ComboController HistoryDirectoryCb
		{
			get {
				return _IImportMetatraderPage2Controller_HistoryDirectoryCb;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_HistoryDirectoryCb= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_22_HISTORYDIRECTORYCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<String> _IImportMetatraderPage2Controller_NameTb;
		public FieldController<String> NameTb
		{
			get {
				return _IImportMetatraderPage2Controller_NameTb;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_NameTb= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_23_NAMETB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<Int32> _IImportMetatraderPage2Controller_AccountNumberNupd;
		public FieldController<Int32> AccountNumberNupd
		{
			get {
				return _IImportMetatraderPage2Controller_AccountNumberNupd;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_AccountNumberNupd= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_24_ACCOUNTNUMBERNUPD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IImportMetatraderPage2Controller_AccountCurrencyTb;
		public LabelledController AccountCurrencyTb
		{
			get {
				return _IImportMetatraderPage2Controller_AccountCurrencyTb;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_AccountCurrencyTb= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_25_ACCOUNTCURRENCYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<Int32> _IImportMetatraderPage2Controller_LeverageNupd;
		public FieldController<Int32> LeverageNupd
		{
			get {
				return _IImportMetatraderPage2Controller_LeverageNupd;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_LeverageNupd= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_26_LEVERAGENUPD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String[] _IImportMetatraderPage2Controller_ImportedParameters;
		public String[] ImportedParameters
		{
			get {
				return _IImportMetatraderPage2Controller_ImportedParameters;
			}
			set {
				if (_IImportMetatraderPage2Controller_ImportedParameters != value) {
					_IImportMetatraderPage2Controller_ImportedParameters= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_27_IMPORTEDPARAMETERS_ID] = true;
					if (IImportMetatraderPage2Controller_ImportedParameters_Changed != null)
						IImportMetatraderPage2Controller_ImportedParameters_Changed(this, new PropertyChangedEventArgs("ImportedParameters", value));
				}
			}
		}

		String[] _IImportMetatraderPage2Controller_HistoryDirectories;
		public String[] HistoryDirectories
		{
			get {
				return _IImportMetatraderPage2Controller_HistoryDirectories;
			}
			set {
				if (_IImportMetatraderPage2Controller_HistoryDirectories != value) {
					_IImportMetatraderPage2Controller_HistoryDirectories= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_28_HISTORYDIRECTORIES_ID] = true;
					if (IImportMetatraderPage2Controller_HistoryDirectories_Changed != null)
						IImportMetatraderPage2Controller_HistoryDirectories_Changed(this, new PropertyChangedEventArgs("HistoryDirectories", value));
				}
			}
		}

		String _IImportMetatraderPage2Controller_ImportedMetatarderDir;
		public String ImportedMetatarderDir
		{
			get {
				return _IImportMetatraderPage2Controller_ImportedMetatarderDir;
			}
			set {
				if (_IImportMetatraderPage2Controller_ImportedMetatarderDir != value) {
					_IImportMetatraderPage2Controller_ImportedMetatarderDir= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_29_IMPORTEDMETATARDERDIR_ID] = true;
					if (IImportMetatraderPage2Controller_ImportedMetatarderDir_Changed != null)
						IImportMetatraderPage2Controller_ImportedMetatarderDir_Changed(this, new PropertyChangedEventArgs("ImportedMetatarderDir", value));
				}
			}
		}

		String _IImportMetatraderPage2Controller_ImportedMetatarderVersion;
		public String ImportedMetatarderVersion
		{
			get {
				return _IImportMetatraderPage2Controller_ImportedMetatarderVersion;
			}
			set {
				if (_IImportMetatraderPage2Controller_ImportedMetatarderVersion != value) {
					_IImportMetatraderPage2Controller_ImportedMetatarderVersion= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_30_IMPORTEDMETATARDERVERSION_ID] = true;
					if (IImportMetatraderPage2Controller_ImportedMetatarderVersion_Changed != null)
						IImportMetatraderPage2Controller_ImportedMetatarderVersion_Changed(this, new PropertyChangedEventArgs("ImportedMetatarderVersion", value));
				}
			}
		}

		String _IImportMetatraderPage2Controller_HistoryDirectory;
		public String HistoryDirectory
		{
			get {
				return _IImportMetatraderPage2Controller_HistoryDirectory;
			}
			set {
				if (_IImportMetatraderPage2Controller_HistoryDirectory != value) {
					_IImportMetatraderPage2Controller_HistoryDirectory= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_31_HISTORYDIRECTORY_ID] = true;
					if (IImportMetatraderPage2Controller_HistoryDirectory_Changed != null)
						IImportMetatraderPage2Controller_HistoryDirectory_Changed(this, new PropertyChangedEventArgs("HistoryDirectory", value));
				}
			}
		}

		String _IImportMetatraderPage2Controller_EnvironmentName;
		public String EnvironmentName
		{
			get {
				return _IImportMetatraderPage2Controller_EnvironmentName;
			}
			set {
				if (_IImportMetatraderPage2Controller_EnvironmentName != value) {
					_IImportMetatraderPage2Controller_EnvironmentName= value;
					changed[ImportMetatraderPage2ControllerProps.PROPERTY_32_ENVIRONMENTNAME_ID] = true;
					if (IImportMetatraderPage2Controller_EnvironmentName_Changed != null)
						IImportMetatraderPage2Controller_EnvironmentName_Changed(this, new PropertyChangedEventArgs("EnvironmentName", value));
				}
			}
		}

		Boolean _IImportMetatraderPage2Controller_CloseMetatrader;
		public virtual Boolean CloseMetatrader
		{
			get {
				return _IImportMetatraderPage2Controller_CloseMetatrader;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage2Controller_CloseMetatrader= value;
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
			if (ImportMetatraderPage2ControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ImportMetatraderPage2ControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
