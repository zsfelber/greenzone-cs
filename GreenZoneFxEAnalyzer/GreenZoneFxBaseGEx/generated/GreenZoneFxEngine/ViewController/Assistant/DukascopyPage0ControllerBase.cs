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
		public static bool RmiGetProperty(IDukascopyPage0Controller controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case DukascopyPage0ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_19_FROMDATEP_ID:
					value = controller.FromDateP;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_20_TODATEP_ID:
					value = controller.ToDateP;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_21_ACCOUNTCURRENCYTB_ID:
					value = controller.AccountCurrencyTb;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_22_LEVERAGENUPD_ID:
					value = controller.LeverageNupd;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_23_ACCOUNTNUMBERNUPD_ID:
					value = controller.AccountNumberNupd;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_24_NAMETB_ID:
					value = controller.NameTb;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_25_ACCOUNTNAMETB_ID:
					value = controller.AccountNameTb;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_26_ACCOUNTCOMPANYTB_ID:
					value = controller.AccountCompanyTb;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_27_SYMBOLSCHL_ID:
					value = controller.SymbolsChl;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_28_IMPORTEDPARAMETERS_ID:
					value = controller.ImportedParameters;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_29_ENVIRONMENTNAME_ID:
					value = controller.EnvironmentName;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_30_SELECTEDSYMBOLS_ID:
					value = controller.SelectedSymbols;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IDukascopyPage0Controller controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case DukascopyPage0ControllerProps.PROPERTY_28_IMPORTEDPARAMETERS_ID:
					controller.ImportedParameters = (String[]) value;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_29_ENVIRONMENTNAME_ID:
					controller.EnvironmentName = (String) value;
					return true;
				case DukascopyPage0ControllerProps.PROPERTY_30_SELECTEDSYMBOLS_ID:
					controller.SelectedSymbols = (List<String>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IDukascopyPage0Controller controller, GreenRmiObjectBuffer buffer, bool goToParent)
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

		public static void AddDependencies(IDukascopyPage0Controller controller, bool goToParent)
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

		public static void SerializationRead(IDukascopyPage0Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IDukascopyPage0Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IDukascopyPage0Controller_ImportedParameters_Changed;
		public event PropertyChangedEventHandler IDukascopyPage0Controller_EnvironmentName_Changed;
		public event PropertyChangedEventHandler IDukascopyPage0Controller_SelectedSymbols_Changed;

		public DukascopyPage0ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			DukascopyPage0ControllerProps.AddDependencies(this, false);
		}

		public DukascopyPage0ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			DukascopyPage0ControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			DukascopyPage0ControllerProps.AddDependencies(this, false);
		}

		protected DukascopyPage0ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			DukascopyPage0ControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			DukascopyPage0ControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			DukascopyPage0ControllerProps.SerializationWrite(this, info, context, false);
		}


		ChildControlMap<String> _IDukascopyPage0Controller_ErrorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return _IDukascopyPage0Controller_ErrorProvider1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_ErrorProvider1= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FileDialogController _IDukascopyPage0Controller_OpenFileDialog1;
		public FileDialogController OpenFileDialog1
		{
			get {
				return _IDukascopyPage0Controller_OpenFileDialog1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_OpenFileDialog1= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> _IDukascopyPage0Controller_FromDateP;
		public FieldController<DateTime> FromDateP
		{
			get {
				return _IDukascopyPage0Controller_FromDateP;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_FromDateP= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_19_FROMDATEP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> _IDukascopyPage0Controller_ToDateP;
		public FieldController<DateTime> ToDateP
		{
			get {
				return _IDukascopyPage0Controller_ToDateP;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_ToDateP= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_20_TODATEP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage0Controller_AccountCurrencyTb;
		public LabelledController AccountCurrencyTb
		{
			get {
				return _IDukascopyPage0Controller_AccountCurrencyTb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_AccountCurrencyTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_21_ACCOUNTCURRENCYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<Int32> _IDukascopyPage0Controller_LeverageNupd;
		public FieldController<Int32> LeverageNupd
		{
			get {
				return _IDukascopyPage0Controller_LeverageNupd;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_LeverageNupd= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_22_LEVERAGENUPD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<Int32> _IDukascopyPage0Controller_AccountNumberNupd;
		public FieldController<Int32> AccountNumberNupd
		{
			get {
				return _IDukascopyPage0Controller_AccountNumberNupd;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_AccountNumberNupd= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_23_ACCOUNTNUMBERNUPD_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<String> _IDukascopyPage0Controller_NameTb;
		public FieldController<String> NameTb
		{
			get {
				return _IDukascopyPage0Controller_NameTb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_NameTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_24_NAMETB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage0Controller_AccountNameTb;
		public LabelledController AccountNameTb
		{
			get {
				return _IDukascopyPage0Controller_AccountNameTb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_AccountNameTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_25_ACCOUNTNAMETB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage0Controller_AccountCompanyTb;
		public LabelledController AccountCompanyTb
		{
			get {
				return _IDukascopyPage0Controller_AccountCompanyTb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_AccountCompanyTb= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_26_ACCOUNTCOMPANYTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ListController _IDukascopyPage0Controller_SymbolsChl;
		public ListController SymbolsChl
		{
			get {
				return _IDukascopyPage0Controller_SymbolsChl;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage0Controller_SymbolsChl= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_27_SYMBOLSCHL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String[] _IDukascopyPage0Controller_ImportedParameters;
		public String[] ImportedParameters
		{
			get {
				return _IDukascopyPage0Controller_ImportedParameters;
			}
			set {
				if (_IDukascopyPage0Controller_ImportedParameters != value) {
					_IDukascopyPage0Controller_ImportedParameters= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_28_IMPORTEDPARAMETERS_ID] = true;
					if (IDukascopyPage0Controller_ImportedParameters_Changed != null)
						IDukascopyPage0Controller_ImportedParameters_Changed(this, new PropertyChangedEventArgs("ImportedParameters", value));
				}
			}
		}

		String _IDukascopyPage0Controller_EnvironmentName;
		public String EnvironmentName
		{
			get {
				return _IDukascopyPage0Controller_EnvironmentName;
			}
			set {
				if (_IDukascopyPage0Controller_EnvironmentName != value) {
					_IDukascopyPage0Controller_EnvironmentName= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_29_ENVIRONMENTNAME_ID] = true;
					if (IDukascopyPage0Controller_EnvironmentName_Changed != null)
						IDukascopyPage0Controller_EnvironmentName_Changed(this, new PropertyChangedEventArgs("EnvironmentName", value));
				}
			}
		}

		List<String> _IDukascopyPage0Controller_SelectedSymbols;
		public List<String> SelectedSymbols
		{
			get {
				return _IDukascopyPage0Controller_SelectedSymbols;
			}
			set {
				if (_IDukascopyPage0Controller_SelectedSymbols != value) {
					_IDukascopyPage0Controller_SelectedSymbols= value;
					changed[DukascopyPage0ControllerProps.PROPERTY_30_SELECTEDSYMBOLS_ID] = true;
					if (IDukascopyPage0Controller_SelectedSymbols_Changed != null)
						IDukascopyPage0Controller_SelectedSymbols_Changed(this, new PropertyChangedEventArgs("SelectedSymbols", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (DukascopyPage0ControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (DukascopyPage0ControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
