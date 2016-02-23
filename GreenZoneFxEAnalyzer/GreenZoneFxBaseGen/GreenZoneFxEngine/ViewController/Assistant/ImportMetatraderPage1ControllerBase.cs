using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Assistant
{
	public static class ImportMetatraderPage1ControllerProps
	{
		public const int PROPERTY_17_ERRORPROVIDER1_ID = 17;
		public const int PROPERTY_18_DATAGRIDVIEW1_ID = 18;
		public const int PROPERTY_19_PATHCOLUMN_ID = 19;
		public const int PROPERTY_20_VERSIONCOLUMN_ID = 20;
		public const int PROPERTY_21_BUTTON1_ID = 21;
		public const int PROPERTY_22_CHECKBOX1_ID = 22;
		public const int PROPERTY_23_FOLDERBROWSERDIALOG1_ID = 23;
		public const int PROPERTY_24_SELECTEDIMPORTDIRECTORY_ID = 24;
		public const int PROPERTY_25_STARTMETATRADER_ID = 25;
		public static bool RmiGetProperty(IImportMetatraderPage1Controller controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case PROPERTY_18_DATAGRIDVIEW1_ID:
					value = controller.DataGridView1;
					return true;
				case PROPERTY_19_PATHCOLUMN_ID:
					value = controller.PathColumn;
					return true;
				case PROPERTY_20_VERSIONCOLUMN_ID:
					value = controller.VersionColumn;
					return true;
				case PROPERTY_21_BUTTON1_ID:
					value = controller.Button1;
					return true;
				case PROPERTY_22_CHECKBOX1_ID:
					value = controller.CheckBox1;
					return true;
				case PROPERTY_23_FOLDERBROWSERDIALOG1_ID:
					value = controller.FolderBrowserDialog1;
					return true;
				case PROPERTY_24_SELECTEDIMPORTDIRECTORY_ID:
					value = controller.SelectedImportDirectory;
					return true;
				case PROPERTY_25_STARTMETATRADER_ID:
					value = controller.StartMetatrader;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IImportMetatraderPage1Controller controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IImportMetatraderPage1Controller controller, GreenRmiObjectBuffer buffer)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID];
			controller.DataGridView1 = (GridController) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_18_DATAGRIDVIEW1_ID];
			controller.PathColumn = (GridColumnController) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_19_PATHCOLUMN_ID];
			controller.VersionColumn = (GridColumnController) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_20_VERSIONCOLUMN_ID];
			controller.Button1 = (ButtonController) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_21_BUTTON1_ID];
			controller.CheckBox1 = (ToggleButtonController) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_22_CHECKBOX1_ID];
			controller.FolderBrowserDialog1 = (FolderBrowserController) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_23_FOLDERBROWSERDIALOG1_ID];
			controller.SelectedImportDirectory = (String[]) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_24_SELECTEDIMPORTDIRECTORY_ID];
			controller.StartMetatrader = (Boolean) buffer.ChangedProps[ImportMetatraderPage1ControllerProps.PROPERTY_25_STARTMETATRADER_ID];
		}

		public static void AddDependencies(IImportMetatraderPage1Controller controller)
		{
			controller.Dependencies.Add(controller.ErrorProvider1);
			controller.Dependencies.Add(controller.DataGridView1);
			controller.Dependencies.Add(controller.PathColumn);
			controller.Dependencies.Add(controller.VersionColumn);
			controller.Dependencies.Add(controller.Button1);
			controller.Dependencies.Add(controller.CheckBox1);
			controller.Dependencies.Add(controller.FolderBrowserDialog1);
		}

		public static void SerializationRead(IImportMetatraderPage1Controller controller, SerializationInfo info, StreamingContext context)
		{
			controller.ErrorProvider1 = (ChildControlMap<String>) info.GetValue("ErrorProvider1", typeof(ChildControlMap<String>));
			controller.DataGridView1 = (GridController) info.GetValue("DataGridView1", typeof(GridController));
			controller.PathColumn = (GridColumnController) info.GetValue("PathColumn", typeof(GridColumnController));
			controller.VersionColumn = (GridColumnController) info.GetValue("VersionColumn", typeof(GridColumnController));
			controller.Button1 = (ButtonController) info.GetValue("Button1", typeof(ButtonController));
			controller.CheckBox1 = (ToggleButtonController) info.GetValue("CheckBox1", typeof(ToggleButtonController));
			controller.FolderBrowserDialog1 = (FolderBrowserController) info.GetValue("FolderBrowserDialog1", typeof(FolderBrowserController));
			controller.SelectedImportDirectory = (String[]) info.GetValue("SelectedImportDirectory", typeof(String[]));
			controller.StartMetatrader = (Boolean) info.GetValue("StartMetatrader", typeof(Boolean));
		}

		public static void SerializationWrite(IImportMetatraderPage1Controller controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ErrorProvider1", controller.ErrorProvider1);
			info.AddValue("DataGridView1", controller.DataGridView1);
			info.AddValue("PathColumn", controller.PathColumn);
			info.AddValue("VersionColumn", controller.VersionColumn);
			info.AddValue("Button1", controller.Button1);
			info.AddValue("CheckBox1", controller.CheckBox1);
			info.AddValue("FolderBrowserDialog1", controller.FolderBrowserDialog1);
			info.AddValue("SelectedImportDirectory", controller.SelectedImportDirectory);
			info.AddValue("StartMetatrader", controller.StartMetatrader);
		}

	}
	public abstract class ImportMetatraderPage1ControllerBase : AssistantPageController, IImportMetatraderPage1Controller
	{

		bool ___initialized = false;


		public ImportMetatraderPage1ControllerBase(GreenRmiManager rmiManager, AssistantFormController assistant)
			: base(rmiManager, assistant)
		{
			___initialized = true;
			ImportMetatraderPage1ControllerProps.AddDependencies(this);
		}

		public ImportMetatraderPage1ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ImportMetatraderPage1ControllerProps.Initialize(this, buffer);
			___initialized = true;
			ImportMetatraderPage1ControllerProps.AddDependencies(this);
		}

		protected ImportMetatraderPage1ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ImportMetatraderPage1ControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ImportMetatraderPage1ControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ImportMetatraderPage1ControllerProps.SerializationWrite(this, info, context);
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
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridController dataGridView1;
		public GridController DataGridView1
		{
			get {
				return dataGridView1;
			}
			set {
				if (!___initialized) {
					dataGridView1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_18_DATAGRIDVIEW1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController pathColumn;
		public GridColumnController PathColumn
		{
			get {
				return pathColumn;
			}
			set {
				if (!___initialized) {
					pathColumn= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_19_PATHCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController versionColumn;
		public GridColumnController VersionColumn
		{
			get {
				return versionColumn;
			}
			set {
				if (!___initialized) {
					versionColumn= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_20_VERSIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController button1;
		public ButtonController Button1
		{
			get {
				return button1;
			}
			set {
				if (!___initialized) {
					button1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_21_BUTTON1_ID] = true;
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
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_22_CHECKBOX1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FolderBrowserController folderBrowserDialog1;
		public FolderBrowserController FolderBrowserDialog1
		{
			get {
				return folderBrowserDialog1;
			}
			set {
				if (!___initialized) {
					folderBrowserDialog1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_23_FOLDERBROWSERDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String[] selectedImportDirectory;
		public String[] SelectedImportDirectory
		{
			get {
				return selectedImportDirectory;
			}
			set {
				if (!___initialized) {
					selectedImportDirectory= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_24_SELECTEDIMPORTDIRECTORY_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean startMetatrader;
		public Boolean StartMetatrader
		{
			get {
				return startMetatrader;
			}
			set {
				if (!___initialized) {
					startMetatrader= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_25_STARTMETATRADER_ID] = true;
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
			if (ImportMetatraderPage1ControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ImportMetatraderPage1ControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
