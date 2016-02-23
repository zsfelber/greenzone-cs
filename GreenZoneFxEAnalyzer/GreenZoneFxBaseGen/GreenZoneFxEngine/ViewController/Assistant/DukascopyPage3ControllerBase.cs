using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class DukascopyPage3ControllerProps
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
		public static bool RmiGetProperty(IDukascopyPage3Controller controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case PROPERTY_18_OPENFILEDIALOG1_ID:
					value = controller.OpenFileDialog1;
					return true;
				case PROPERTY_19_DATELABEL_ID:
					value = controller.DateLabel;
					return true;
				case PROPERTY_20_SYMBOLLABEL_ID:
					value = controller.SymbolLabel;
					return true;
				case PROPERTY_21_SYMBOLPROGRESSBAR_ID:
					value = controller.SymbolProgressBar;
					return true;
				case PROPERTY_22_DATEPROGRESSBAR_ID:
					value = controller.DateProgressBar;
					return true;
				case PROPERTY_23_TOLABEL_ID:
					value = controller.ToLabel;
					return true;
				case PROPERTY_24_FROMLABEL_ID:
					value = controller.FromLabel;
					return true;
				case PROPERTY_25_ESTIMATEDLABEL_ID:
					value = controller.EstimatedLabel;
					return true;
				case PROPERTY_26_ELAPSEDLABEL_ID:
					value = controller.ElapsedLabel;
					return true;
				case PROPERTY_27_SYMBOLSTB_ID:
					value = controller.SymbolsTb;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IDukascopyPage3Controller controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IDukascopyPage3Controller controller, GreenRmiObjectBuffer buffer)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID];
			controller.OpenFileDialog1 = (FileDialogController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID];
			controller.DateLabel = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_19_DATELABEL_ID];
			controller.SymbolLabel = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_20_SYMBOLLABEL_ID];
			controller.SymbolProgressBar = (ProgressTrackController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_21_SYMBOLPROGRESSBAR_ID];
			controller.DateProgressBar = (ProgressTrackController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_22_DATEPROGRESSBAR_ID];
			controller.ToLabel = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_23_TOLABEL_ID];
			controller.FromLabel = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_24_FROMLABEL_ID];
			controller.EstimatedLabel = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_25_ESTIMATEDLABEL_ID];
			controller.ElapsedLabel = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_26_ELAPSEDLABEL_ID];
			controller.SymbolsTb = (LabelledController) buffer.ChangedProps[DukascopyPage3ControllerProps.PROPERTY_27_SYMBOLSTB_ID];
		}

		public static void AddDependencies(IDukascopyPage3Controller controller)
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
		}

		public static void SerializationRead(IDukascopyPage3Controller controller, SerializationInfo info, StreamingContext context)
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
		}

		public static void SerializationWrite(IDukascopyPage3Controller controller, SerializationInfo info, StreamingContext context)
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
		}

	}
	public abstract class DukascopyPage3ControllerBase : AssistantPageController, IDukascopyPage3Controller
	{

		bool ___initialized = false;


		public DukascopyPage3ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			DukascopyPage3ControllerProps.AddDependencies(this);
		}

		public DukascopyPage3ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			DukascopyPage3ControllerProps.Initialize(this, buffer);
			___initialized = true;
			DukascopyPage3ControllerProps.AddDependencies(this);
		}

		protected DukascopyPage3ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			DukascopyPage3ControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			DukascopyPage3ControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			DukascopyPage3ControllerProps.SerializationWrite(this, info, context);
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
					changed[DukascopyPage3ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
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
					changed[DukascopyPage3ControllerProps.PROPERTY_18_OPENFILEDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController dateLabel;
		public LabelledController DateLabel
		{
			get {
				return dateLabel;
			}
			set {
				if (!___initialized) {
					dateLabel= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_19_DATELABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController symbolLabel;
		public LabelledController SymbolLabel
		{
			get {
				return symbolLabel;
			}
			set {
				if (!___initialized) {
					symbolLabel= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_20_SYMBOLLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController symbolProgressBar;
		public ProgressTrackController SymbolProgressBar
		{
			get {
				return symbolProgressBar;
			}
			set {
				if (!___initialized) {
					symbolProgressBar= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_21_SYMBOLPROGRESSBAR_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ProgressTrackController dateProgressBar;
		public ProgressTrackController DateProgressBar
		{
			get {
				return dateProgressBar;
			}
			set {
				if (!___initialized) {
					dateProgressBar= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_22_DATEPROGRESSBAR_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController toLabel;
		public LabelledController ToLabel
		{
			get {
				return toLabel;
			}
			set {
				if (!___initialized) {
					toLabel= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_23_TOLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController fromLabel;
		public LabelledController FromLabel
		{
			get {
				return fromLabel;
			}
			set {
				if (!___initialized) {
					fromLabel= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_24_FROMLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController estimatedLabel;
		public LabelledController EstimatedLabel
		{
			get {
				return estimatedLabel;
			}
			set {
				if (!___initialized) {
					estimatedLabel= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_25_ESTIMATEDLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController elapsedLabel;
		public LabelledController ElapsedLabel
		{
			get {
				return elapsedLabel;
			}
			set {
				if (!___initialized) {
					elapsedLabel= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_26_ELAPSEDLABEL_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController symbolsTb;
		public LabelledController SymbolsTb
		{
			get {
				return symbolsTb;
			}
			set {
				if (!___initialized) {
					symbolsTb= value;
					changed[DukascopyPage3ControllerProps.PROPERTY_27_SYMBOLSTB_ID] = true;
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
			if (DukascopyPage3ControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!DukascopyPage3ControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
