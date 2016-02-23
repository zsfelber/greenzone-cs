using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController.Assistant;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class DukascopyPage1ControllerProps
	{
		public const int PROPERTY_17_ERRORPROVIDER1_ID = 17;
		public const int PROPERTY_18_TOOLTIP1_ID = 18;
		public const int PROPERTY_19_OPENFILEDIALOG1_ID = 19;
		public const int PROPERTY_20_GENERATECHL_ID = 20;
		public const int PROPERTY_21_GENERATEDEFTB_ID = 21;
		public const int PROPERTY_22_UPDATENONERB_ID = 22;
		public const int PROPERTY_23_DOWNLOADTICKGENPERIODSRB_ID = 23;
		public const int PROPERTY_24_UPDATETICKSGENPERIODSRB_ID = 24;
		public const int PROPERTY_25_UPDATEALLRB_ID = 25;
		public const int PROPERTY_26_DELETECORRUPTPERIODSCB_ID = 26;
		public const int PROPERTY_27_TODATEP_ID = 27;
		public const int PROPERTY_28_FROMDATEP_ID = 28;
		public const int PROPERTY_29_IMPORTEDPARAMETERS_ID = 29;
		public const int PROPERTY_30_ENVIRONMENTNAME_ID = 30;
		public const int PROPERTY_31_UPDATEMODE_ID = 31;
		public const int PROPERTY_32_CUSTOMPERIODS_ID = 32;
		public const int PROPERTY_33_ALLSELECTEDPERIODS_ID = 33;
		public const int PROPERTY_34_FROM_ID = 34;
		public const int PROPERTY_35_TO_ID = 35;
		public const int PROPERTY_36_DELETECORRUPTPERIODS_ID = 36;
		public static bool RmiGetProperty(IDukascopyPage1Controller controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case DukascopyPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_18_TOOLTIP1_ID:
					value = controller.ToolTip1;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_19_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_20_GENERATECHL_ID:
					value = controller.GenerateChl;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_21_GENERATEDEFTB_ID:
					value = controller.GenerateDefTb;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_22_UPDATENONERB_ID:
					value = controller.UpdateNoneRb;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_23_DOWNLOADTICKGENPERIODSRB_ID:
					value = controller.DownloadTickGenPeriodsRb;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_24_UPDATETICKSGENPERIODSRB_ID:
					value = controller.UpdateTicksGenPeriodsRb;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_25_UPDATEALLRB_ID:
					value = controller.UpdateAllRb;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_26_DELETECORRUPTPERIODSCB_ID:
					value = controller.DeleteCorruptPeriodsCb;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_27_TODATEP_ID:
					value = controller.ToDateP;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_28_FROMDATEP_ID:
					value = controller.FromDateP;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_29_IMPORTEDPARAMETERS_ID:
					value = controller.ImportedParameters;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_30_ENVIRONMENTNAME_ID:
					value = controller.EnvironmentName;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_31_UPDATEMODE_ID:
					value = controller.UpdateMode;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_32_CUSTOMPERIODS_ID:
					value = controller.CustomPeriods;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_33_ALLSELECTEDPERIODS_ID:
					value = controller.AllSelectedPeriods;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_34_FROM_ID:
					value = controller.From;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_35_TO_ID:
					value = controller.To;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_36_DELETECORRUPTPERIODS_ID:
					value = controller.DeleteCorruptPeriods;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IDukascopyPage1Controller controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case DukascopyPage1ControllerProps.PROPERTY_29_IMPORTEDPARAMETERS_ID:
					controller.ImportedParameters = (String[]) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_30_ENVIRONMENTNAME_ID:
					controller.EnvironmentName = (String) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_31_UPDATEMODE_ID:
					controller.UpdateMode = (UpdateMode) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_32_CUSTOMPERIODS_ID:
					controller.CustomPeriods = (List<TimePeriodConst>) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_33_ALLSELECTEDPERIODS_ID:
					controller.AllSelectedPeriods = (List<TimePeriodConst>) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_34_FROM_ID:
					controller.From = (DateTime) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_35_TO_ID:
					controller.To = (DateTime) value;
					return true;
				case DukascopyPage1ControllerProps.PROPERTY_36_DELETECORRUPTPERIODS_ID:
					controller.DeleteCorruptPeriods = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IDukascopyPage1Controller controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID];
			controller.ToolTip1 = (ChildControlMap<String>) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_18_TOOLTIP1_ID];
			controller.OpenFileDialog1 = (FileDialogController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_19_OPENFILEDIALOG1_ID];
			controller.GenerateChl = (ListController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_20_GENERATECHL_ID];
			controller.GenerateDefTb = (LabelledController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_21_GENERATEDEFTB_ID];
			controller.UpdateNoneRb = (RadioButtonController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_22_UPDATENONERB_ID];
			controller.DownloadTickGenPeriodsRb = (RadioButtonController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_23_DOWNLOADTICKGENPERIODSRB_ID];
			controller.UpdateTicksGenPeriodsRb = (RadioButtonController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_24_UPDATETICKSGENPERIODSRB_ID];
			controller.UpdateAllRb = (RadioButtonController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_25_UPDATEALLRB_ID];
			controller.DeleteCorruptPeriodsCb = (ToggleButtonController) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_26_DELETECORRUPTPERIODSCB_ID];
			controller.ToDateP = (FieldController<DateTime>) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_27_TODATEP_ID];
			controller.FromDateP = (FieldController<DateTime>) buffer.ChangedProps[DukascopyPage1ControllerProps.PROPERTY_28_FROMDATEP_ID];
		}

		public static void AddDependencies(IDukascopyPage1Controller controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.ErrorProvider1);
			controller.Dependencies.Add(controller.ToolTip1);
			controller.Dependencies.Add(controller.OpenFileDialog1);
			controller.Dependencies.Add(controller.GenerateChl);
			controller.Dependencies.Add(controller.GenerateDefTb);
			controller.Dependencies.Add(controller.UpdateNoneRb);
			controller.Dependencies.Add(controller.DownloadTickGenPeriodsRb);
			controller.Dependencies.Add(controller.UpdateTicksGenPeriodsRb);
			controller.Dependencies.Add(controller.UpdateAllRb);
			controller.Dependencies.Add(controller.DeleteCorruptPeriodsCb);
			controller.Dependencies.Add(controller.ToDateP);
			controller.Dependencies.Add(controller.FromDateP);
		}

		public static void SerializationRead(IDukascopyPage1Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) info.GetValue("ErrorProvider1", typeof(ChildControlMap<String>));
			controller.ToolTip1 = (ChildControlMap<String>) info.GetValue("ToolTip1", typeof(ChildControlMap<String>));
			controller.OpenFileDialog1 = (FileDialogController) info.GetValue("OpenFileDialog1", typeof(FileDialogController));
			controller.GenerateChl = (ListController) info.GetValue("GenerateChl", typeof(ListController));
			controller.GenerateDefTb = (LabelledController) info.GetValue("GenerateDefTb", typeof(LabelledController));
			controller.UpdateNoneRb = (RadioButtonController) info.GetValue("UpdateNoneRb", typeof(RadioButtonController));
			controller.DownloadTickGenPeriodsRb = (RadioButtonController) info.GetValue("DownloadTickGenPeriodsRb", typeof(RadioButtonController));
			controller.UpdateTicksGenPeriodsRb = (RadioButtonController) info.GetValue("UpdateTicksGenPeriodsRb", typeof(RadioButtonController));
			controller.UpdateAllRb = (RadioButtonController) info.GetValue("UpdateAllRb", typeof(RadioButtonController));
			controller.DeleteCorruptPeriodsCb = (ToggleButtonController) info.GetValue("DeleteCorruptPeriodsCb", typeof(ToggleButtonController));
			controller.ToDateP = (FieldController<DateTime>) info.GetValue("ToDateP", typeof(FieldController<DateTime>));
			controller.FromDateP = (FieldController<DateTime>) info.GetValue("FromDateP", typeof(FieldController<DateTime>));
			controller.ImportedParameters = (String[]) info.GetValue("ImportedParameters", typeof(String[]));
			controller.EnvironmentName = (String) info.GetValue("EnvironmentName", typeof(String));
			controller.UpdateMode = (UpdateMode) info.GetValue("UpdateMode", typeof(UpdateMode));
			controller.CustomPeriods = (List<TimePeriodConst>) info.GetValue("CustomPeriods", typeof(List<TimePeriodConst>));
			controller.AllSelectedPeriods = (List<TimePeriodConst>) info.GetValue("AllSelectedPeriods", typeof(List<TimePeriodConst>));
			controller.From = (DateTime) info.GetValue("From", typeof(DateTime));
			controller.To = (DateTime) info.GetValue("To", typeof(DateTime));
			controller.DeleteCorruptPeriods = (Boolean) info.GetValue("DeleteCorruptPeriods", typeof(Boolean));
		}

		public static void SerializationWrite(IDukascopyPage1Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("ErrorProvider1", controller.ErrorProvider1);
			info.AddValue("ToolTip1", controller.ToolTip1);
			info.AddValue("OpenFileDialog1", controller.OpenFileDialog1);
			info.AddValue("GenerateChl", controller.GenerateChl);
			info.AddValue("GenerateDefTb", controller.GenerateDefTb);
			info.AddValue("UpdateNoneRb", controller.UpdateNoneRb);
			info.AddValue("DownloadTickGenPeriodsRb", controller.DownloadTickGenPeriodsRb);
			info.AddValue("UpdateTicksGenPeriodsRb", controller.UpdateTicksGenPeriodsRb);
			info.AddValue("UpdateAllRb", controller.UpdateAllRb);
			info.AddValue("DeleteCorruptPeriodsCb", controller.DeleteCorruptPeriodsCb);
			info.AddValue("ToDateP", controller.ToDateP);
			info.AddValue("FromDateP", controller.FromDateP);
			info.AddValue("ImportedParameters", controller.ImportedParameters);
			info.AddValue("EnvironmentName", controller.EnvironmentName);
			info.AddValue("UpdateMode", controller.UpdateMode);
			info.AddValue("CustomPeriods", controller.CustomPeriods);
			info.AddValue("AllSelectedPeriods", controller.AllSelectedPeriods);
			info.AddValue("From", controller.From);
			info.AddValue("To", controller.To);
			info.AddValue("DeleteCorruptPeriods", controller.DeleteCorruptPeriods);
		}

	}
	public abstract class DukascopyPage1ControllerBase : AssistantPageController, IDukascopyPage1Controller
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IDukascopyPage1Controller_ImportedParameters_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_EnvironmentName_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_UpdateMode_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_CustomPeriods_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_AllSelectedPeriods_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_From_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_To_Changed;
		public event PropertyChangedEventHandler IDukascopyPage1Controller_DeleteCorruptPeriods_Changed;

		public DukascopyPage1ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			DukascopyPage1ControllerProps.AddDependencies(this, false);
		}

		public DukascopyPage1ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			DukascopyPage1ControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			DukascopyPage1ControllerProps.AddDependencies(this, false);
		}

		protected DukascopyPage1ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			DukascopyPage1ControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			DukascopyPage1ControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			DukascopyPage1ControllerProps.SerializationWrite(this, info, context, false);
		}


		ChildControlMap<String> _IDukascopyPage1Controller_ErrorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return _IDukascopyPage1Controller_ErrorProvider1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_ErrorProvider1= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ChildControlMap<String> _IDukascopyPage1Controller_ToolTip1;
		public ChildControlMap<String> ToolTip1
		{
			get {
				return _IDukascopyPage1Controller_ToolTip1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_ToolTip1= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_18_TOOLTIP1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FileDialogController _IDukascopyPage1Controller_OpenFileDialog1;
		public FileDialogController OpenFileDialog1
		{
			get {
				return _IDukascopyPage1Controller_OpenFileDialog1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_OpenFileDialog1= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_19_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ListController _IDukascopyPage1Controller_GenerateChl;
		public ListController GenerateChl
		{
			get {
				return _IDukascopyPage1Controller_GenerateChl;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_GenerateChl= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_20_GENERATECHL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage1Controller_GenerateDefTb;
		public LabelledController GenerateDefTb
		{
			get {
				return _IDukascopyPage1Controller_GenerateDefTb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_GenerateDefTb= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_21_GENERATEDEFTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController _IDukascopyPage1Controller_UpdateNoneRb;
		public RadioButtonController UpdateNoneRb
		{
			get {
				return _IDukascopyPage1Controller_UpdateNoneRb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_UpdateNoneRb= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_22_UPDATENONERB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController _IDukascopyPage1Controller_DownloadTickGenPeriodsRb;
		public RadioButtonController DownloadTickGenPeriodsRb
		{
			get {
				return _IDukascopyPage1Controller_DownloadTickGenPeriodsRb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_DownloadTickGenPeriodsRb= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_23_DOWNLOADTICKGENPERIODSRB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController _IDukascopyPage1Controller_UpdateTicksGenPeriodsRb;
		public RadioButtonController UpdateTicksGenPeriodsRb
		{
			get {
				return _IDukascopyPage1Controller_UpdateTicksGenPeriodsRb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_UpdateTicksGenPeriodsRb= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_24_UPDATETICKSGENPERIODSRB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		RadioButtonController _IDukascopyPage1Controller_UpdateAllRb;
		public RadioButtonController UpdateAllRb
		{
			get {
				return _IDukascopyPage1Controller_UpdateAllRb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_UpdateAllRb= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_25_UPDATEALLRB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IDukascopyPage1Controller_DeleteCorruptPeriodsCb;
		public ToggleButtonController DeleteCorruptPeriodsCb
		{
			get {
				return _IDukascopyPage1Controller_DeleteCorruptPeriodsCb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_DeleteCorruptPeriodsCb= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_26_DELETECORRUPTPERIODSCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> _IDukascopyPage1Controller_ToDateP;
		public FieldController<DateTime> ToDateP
		{
			get {
				return _IDukascopyPage1Controller_ToDateP;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_ToDateP= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_27_TODATEP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> _IDukascopyPage1Controller_FromDateP;
		public FieldController<DateTime> FromDateP
		{
			get {
				return _IDukascopyPage1Controller_FromDateP;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage1Controller_FromDateP= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_28_FROMDATEP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String[] _IDukascopyPage1Controller_ImportedParameters;
		public String[] ImportedParameters
		{
			get {
				return _IDukascopyPage1Controller_ImportedParameters;
			}
			set {
				if (_IDukascopyPage1Controller_ImportedParameters != value) {
					_IDukascopyPage1Controller_ImportedParameters= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_29_IMPORTEDPARAMETERS_ID] = true;
					if (IDukascopyPage1Controller_ImportedParameters_Changed != null)
						IDukascopyPage1Controller_ImportedParameters_Changed(this, new PropertyChangedEventArgs("ImportedParameters", value));
				}
			}
		}

		String _IDukascopyPage1Controller_EnvironmentName;
		public String EnvironmentName
		{
			get {
				return _IDukascopyPage1Controller_EnvironmentName;
			}
			set {
				if (_IDukascopyPage1Controller_EnvironmentName != value) {
					_IDukascopyPage1Controller_EnvironmentName= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_30_ENVIRONMENTNAME_ID] = true;
					if (IDukascopyPage1Controller_EnvironmentName_Changed != null)
						IDukascopyPage1Controller_EnvironmentName_Changed(this, new PropertyChangedEventArgs("EnvironmentName", value));
				}
			}
		}

		UpdateMode _IDukascopyPage1Controller_UpdateMode;
		public virtual UpdateMode UpdateMode
		{
			get {
				return _IDukascopyPage1Controller_UpdateMode;
			}
			set {
				if (_IDukascopyPage1Controller_UpdateMode != value) {
					_IDukascopyPage1Controller_UpdateMode= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_31_UPDATEMODE_ID] = true;
					if (IDukascopyPage1Controller_UpdateMode_Changed != null)
						IDukascopyPage1Controller_UpdateMode_Changed(this, new PropertyChangedEventArgs("UpdateMode", value));
				}
			}
		}

		List<TimePeriodConst> _IDukascopyPage1Controller_CustomPeriods;
		public List<TimePeriodConst> CustomPeriods
		{
			get {
				return _IDukascopyPage1Controller_CustomPeriods;
			}
			set {
				if (_IDukascopyPage1Controller_CustomPeriods != value) {
					_IDukascopyPage1Controller_CustomPeriods= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_32_CUSTOMPERIODS_ID] = true;
					if (IDukascopyPage1Controller_CustomPeriods_Changed != null)
						IDukascopyPage1Controller_CustomPeriods_Changed(this, new PropertyChangedEventArgs("CustomPeriods", value));
				}
			}
		}

		List<TimePeriodConst> _IDukascopyPage1Controller_AllSelectedPeriods;
		public List<TimePeriodConst> AllSelectedPeriods
		{
			get {
				return _IDukascopyPage1Controller_AllSelectedPeriods;
			}
			set {
				if (_IDukascopyPage1Controller_AllSelectedPeriods != value) {
					_IDukascopyPage1Controller_AllSelectedPeriods= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_33_ALLSELECTEDPERIODS_ID] = true;
					if (IDukascopyPage1Controller_AllSelectedPeriods_Changed != null)
						IDukascopyPage1Controller_AllSelectedPeriods_Changed(this, new PropertyChangedEventArgs("AllSelectedPeriods", value));
				}
			}
		}

		DateTime _IDukascopyPage1Controller_From;
		public DateTime From
		{
			get {
				return _IDukascopyPage1Controller_From;
			}
			set {
				if (_IDukascopyPage1Controller_From != value) {
					_IDukascopyPage1Controller_From= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_34_FROM_ID] = true;
					if (IDukascopyPage1Controller_From_Changed != null)
						IDukascopyPage1Controller_From_Changed(this, new PropertyChangedEventArgs("From", value));
				}
			}
		}

		DateTime _IDukascopyPage1Controller_To;
		public DateTime To
		{
			get {
				return _IDukascopyPage1Controller_To;
			}
			set {
				if (_IDukascopyPage1Controller_To != value) {
					_IDukascopyPage1Controller_To= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_35_TO_ID] = true;
					if (IDukascopyPage1Controller_To_Changed != null)
						IDukascopyPage1Controller_To_Changed(this, new PropertyChangedEventArgs("To", value));
				}
			}
		}

		Boolean _IDukascopyPage1Controller_DeleteCorruptPeriods;
		public Boolean DeleteCorruptPeriods
		{
			get {
				return _IDukascopyPage1Controller_DeleteCorruptPeriods;
			}
			set {
				if (_IDukascopyPage1Controller_DeleteCorruptPeriods != value) {
					_IDukascopyPage1Controller_DeleteCorruptPeriods= value;
					changed[DukascopyPage1ControllerProps.PROPERTY_36_DELETECORRUPTPERIODS_ID] = true;
					if (IDukascopyPage1Controller_DeleteCorruptPeriods_Changed != null)
						IDukascopyPage1Controller_DeleteCorruptPeriods_Changed(this, new PropertyChangedEventArgs("DeleteCorruptPeriods", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (DukascopyPage1ControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (DukascopyPage1ControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
