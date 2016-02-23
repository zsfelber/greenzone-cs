using GreenZoneFxEngine.Types;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class DukascopyPage2ControllerProps
	{
		public const int PROPERTY_17_ERRORPROVIDER1_ID = 17;
		public const int PROPERTY_18_OPENFILEDIALOG1_ID = 18;
		public const int PROPERTY_19_DATELABEL_ID = 19;
		public const int PROPERTY_20_SYMBOLLABEL_ID = 20;
		public const int PROPERTY_21_SYMBOLPROGRESSBAR_ID = 21;
		public const int PROPERTY_22_DATEPROGRESSBAR_ID = 22;
		public const int PROPERTY_23_TOLABEL_ID = 23;
		public const int PROPERTY_24_FROMLABEL_ID = 24;
		public const int PROPERTY_25_ESTIMATEDLABEL_ID = 25;
		public const int PROPERTY_26_ELAPSEDLABEL_ID = 26;
		public const int PROPERTY_27_SYMBOLSTB_ID = 27;
		public const int PROPERTY_28_LABEL2_ID = 28;
		public const int PROPERTY_29_FIRSTUPDATEDFILETIME_ID = 29;
		public static bool RmiGetProperty(IDukascopyPage2Controller controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case DukascopyPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_19_DATELABEL_ID:
					value = controller.DateLabel;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_20_SYMBOLLABEL_ID:
					value = controller.SymbolLabel;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_21_SYMBOLPROGRESSBAR_ID:
					value = controller.SymbolProgressBar;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_22_DATEPROGRESSBAR_ID:
					value = controller.DateProgressBar;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_23_TOLABEL_ID:
					value = controller.ToLabel;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_24_FROMLABEL_ID:
					value = controller.FromLabel;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_25_ESTIMATEDLABEL_ID:
					value = controller.EstimatedLabel;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_26_ELAPSEDLABEL_ID:
					value = controller.ElapsedLabel;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_27_SYMBOLSTB_ID:
					value = controller.SymbolsTb;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_28_LABEL2_ID:
					value = controller.Label2;
					return true;
				case DukascopyPage2ControllerProps.PROPERTY_29_FIRSTUPDATEDFILETIME_ID:
					value = controller.FirstUpdatedFileTime;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IDukascopyPage2Controller controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case DukascopyPage2ControllerProps.PROPERTY_29_FIRSTUPDATEDFILETIME_ID:
					controller.FirstUpdatedFileTime = (datetime) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IDukascopyPage2Controller controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID];
			controller.OpenFileDialog1 = (FileDialogController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID];
			controller.DateLabel = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_19_DATELABEL_ID];
			controller.SymbolLabel = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_20_SYMBOLLABEL_ID];
			controller.SymbolProgressBar = (ProgressTrackController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_21_SYMBOLPROGRESSBAR_ID];
			controller.DateProgressBar = (ProgressTrackController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_22_DATEPROGRESSBAR_ID];
			controller.ToLabel = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_23_TOLABEL_ID];
			controller.FromLabel = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_24_FROMLABEL_ID];
			controller.EstimatedLabel = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_25_ESTIMATEDLABEL_ID];
			controller.ElapsedLabel = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_26_ELAPSEDLABEL_ID];
			controller.SymbolsTb = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_27_SYMBOLSTB_ID];
			controller.Label2 = (LabelledController) buffer.ChangedProps[DukascopyPage2ControllerProps.PROPERTY_28_LABEL2_ID];
		}

		public static void AddDependencies(IDukascopyPage2Controller controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.ErrorProvider1);
			controller.Dependencies.Add(controller.OpenFileDialog1);
			controller.Dependencies.Add(controller.DateLabel);
			controller.Dependencies.Add(controller.SymbolLabel);
			controller.Dependencies.Add(controller.SymbolProgressBar);
			controller.Dependencies.Add(controller.DateProgressBar);
			controller.Dependencies.Add(controller.ToLabel);
			controller.Dependencies.Add(controller.FromLabel);
			controller.Dependencies.Add(controller.EstimatedLabel);
			controller.Dependencies.Add(controller.ElapsedLabel);
			controller.Dependencies.Add(controller.SymbolsTb);
			controller.Dependencies.Add(controller.Label2);
		}

		public static void SerializationRead(IDukascopyPage2Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) info.GetValue("ErrorProvider1", typeof(ChildControlMap<String>));
			controller.OpenFileDialog1 = (FileDialogController) info.GetValue("OpenFileDialog1", typeof(FileDialogController));
			controller.DateLabel = (LabelledController) info.GetValue("DateLabel", typeof(LabelledController));
			controller.SymbolLabel = (LabelledController) info.GetValue("SymbolLabel", typeof(LabelledController));
			controller.SymbolProgressBar = (ProgressTrackController) info.GetValue("SymbolProgressBar", typeof(ProgressTrackController));
			controller.DateProgressBar = (ProgressTrackController) info.GetValue("DateProgressBar", typeof(ProgressTrackController));
			controller.ToLabel = (LabelledController) info.GetValue("ToLabel", typeof(LabelledController));
			controller.FromLabel = (LabelledController) info.GetValue("FromLabel", typeof(LabelledController));
			controller.EstimatedLabel = (LabelledController) info.GetValue("EstimatedLabel", typeof(LabelledController));
			controller.ElapsedLabel = (LabelledController) info.GetValue("ElapsedLabel", typeof(LabelledController));
			controller.SymbolsTb = (LabelledController) info.GetValue("SymbolsTb", typeof(LabelledController));
			controller.Label2 = (LabelledController) info.GetValue("Label2", typeof(LabelledController));
			controller.FirstUpdatedFileTime = (datetime) info.GetValue("FirstUpdatedFileTime", typeof(datetime));
		}

		public static void SerializationWrite(IDukascopyPage2Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("ErrorProvider1", controller.ErrorProvider1);
			info.AddValue("OpenFileDialog1", controller.OpenFileDialog1);
			info.AddValue("DateLabel", controller.DateLabel);
			info.AddValue("SymbolLabel", controller.SymbolLabel);
			info.AddValue("SymbolProgressBar", controller.SymbolProgressBar);
			info.AddValue("DateProgressBar", controller.DateProgressBar);
			info.AddValue("ToLabel", controller.ToLabel);
			info.AddValue("FromLabel", controller.FromLabel);
			info.AddValue("EstimatedLabel", controller.EstimatedLabel);
			info.AddValue("ElapsedLabel", controller.ElapsedLabel);
			info.AddValue("SymbolsTb", controller.SymbolsTb);
			info.AddValue("Label2", controller.Label2);
			info.AddValue("FirstUpdatedFileTime", controller.FirstUpdatedFileTime);
		}

	}
	public abstract class DukascopyPage2ControllerBase : AssistantPageController, IDukascopyPage2Controller
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IDukascopyPage2Controller_FirstUpdatedFileTime_Changed;

		public DukascopyPage2ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			DukascopyPage2ControllerProps.AddDependencies(this, false);
		}

		public DukascopyPage2ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			DukascopyPage2ControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			DukascopyPage2ControllerProps.AddDependencies(this, false);
		}

		protected DukascopyPage2ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			DukascopyPage2ControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			DukascopyPage2ControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			DukascopyPage2ControllerProps.SerializationWrite(this, info, context, false);
		}


		ChildControlMap<String> _IDukascopyPage2Controller_ErrorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return _IDukascopyPage2Controller_ErrorProvider1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_ErrorProvider1= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FileDialogController _IDukascopyPage2Controller_OpenFileDialog1;
		public FileDialogController OpenFileDialog1
		{
			get {
				return _IDukascopyPage2Controller_OpenFileDialog1;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_OpenFileDialog1= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_DateLabel;
		public LabelledController DateLabel
		{
			get {
				return _IDukascopyPage2Controller_DateLabel;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_DateLabel= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_19_DATELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_SymbolLabel;
		public LabelledController SymbolLabel
		{
			get {
				return _IDukascopyPage2Controller_SymbolLabel;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_SymbolLabel= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_20_SYMBOLLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController _IDukascopyPage2Controller_SymbolProgressBar;
		public ProgressTrackController SymbolProgressBar
		{
			get {
				return _IDukascopyPage2Controller_SymbolProgressBar;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_SymbolProgressBar= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_21_SYMBOLPROGRESSBAR_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController _IDukascopyPage2Controller_DateProgressBar;
		public ProgressTrackController DateProgressBar
		{
			get {
				return _IDukascopyPage2Controller_DateProgressBar;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_DateProgressBar= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_22_DATEPROGRESSBAR_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_ToLabel;
		public LabelledController ToLabel
		{
			get {
				return _IDukascopyPage2Controller_ToLabel;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_ToLabel= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_23_TOLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_FromLabel;
		public LabelledController FromLabel
		{
			get {
				return _IDukascopyPage2Controller_FromLabel;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_FromLabel= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_24_FROMLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_EstimatedLabel;
		public LabelledController EstimatedLabel
		{
			get {
				return _IDukascopyPage2Controller_EstimatedLabel;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_EstimatedLabel= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_25_ESTIMATEDLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_ElapsedLabel;
		public LabelledController ElapsedLabel
		{
			get {
				return _IDukascopyPage2Controller_ElapsedLabel;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_ElapsedLabel= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_26_ELAPSEDLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_SymbolsTb;
		public LabelledController SymbolsTb
		{
			get {
				return _IDukascopyPage2Controller_SymbolsTb;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_SymbolsTb= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_27_SYMBOLSTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IDukascopyPage2Controller_Label2;
		public LabelledController Label2
		{
			get {
				return _IDukascopyPage2Controller_Label2;
			}
			set {
				if (!___initialized) {
					_IDukascopyPage2Controller_Label2= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_28_LABEL2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		datetime _IDukascopyPage2Controller_FirstUpdatedFileTime;
		public datetime FirstUpdatedFileTime
		{
			get {
				return _IDukascopyPage2Controller_FirstUpdatedFileTime;
			}
			set {
				if (_IDukascopyPage2Controller_FirstUpdatedFileTime != value) {
					_IDukascopyPage2Controller_FirstUpdatedFileTime= value;
					changed[DukascopyPage2ControllerProps.PROPERTY_29_FIRSTUPDATEDFILETIME_ID] = true;
					if (IDukascopyPage2Controller_FirstUpdatedFileTime_Changed != null)
						IDukascopyPage2Controller_FirstUpdatedFileTime_Changed(this, new PropertyChangedEventArgs("FirstUpdatedFileTime", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (DukascopyPage2ControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (DukascopyPage2ControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
