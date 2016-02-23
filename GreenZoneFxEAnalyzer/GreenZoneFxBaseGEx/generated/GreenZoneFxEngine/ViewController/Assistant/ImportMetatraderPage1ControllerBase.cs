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
		public static bool RmiGetProperty(IImportMetatraderPage1Controller controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ImportMetatraderPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID:
					value = controller.ErrorProvider1;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_18_DATAGRIDVIEW1_ID:
					value = controller.DataGridView1;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_19_PATHCOLUMN_ID:
					value = controller.PathColumn;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_20_VERSIONCOLUMN_ID:
					value = controller.VersionColumn;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_21_BUTTON1_ID:
					value = controller.Button1;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_22_CHECKBOX1_ID:
					value = controller.CheckBox1;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_23_FOLDERBROWSERDIALOG1_ID:
					value = controller.FolderBrowserDialog1;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_24_SELECTEDIMPORTDIRECTORY_ID:
					value = controller.SelectedImportDirectory;
					return true;
				case ImportMetatraderPage1ControllerProps.PROPERTY_25_STARTMETATRADER_ID:
					value = controller.StartMetatrader;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IImportMetatraderPage1Controller controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IImportMetatraderPage1Controller controller, GreenRmiObjectBuffer buffer, bool goToParent)
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

		public static void AddDependencies(IImportMetatraderPage1Controller controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.ErrorProvider1);
			controller.Dependencies.Add(controller.DataGridView1);
			controller.Dependencies.Add(controller.PathColumn);
			controller.Dependencies.Add(controller.VersionColumn);
			controller.Dependencies.Add(controller.Button1);
			controller.Dependencies.Add(controller.CheckBox1);
			controller.Dependencies.Add(controller.FolderBrowserDialog1);
		}

		public static void SerializationRead(IImportMetatraderPage1Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IImportMetatraderPage1Controller controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			ImportMetatraderPage1ControllerProps.AddDependencies(this, false);
		}

		public ImportMetatraderPage1ControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ImportMetatraderPage1ControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ImportMetatraderPage1ControllerProps.AddDependencies(this, false);
		}

		protected ImportMetatraderPage1ControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ImportMetatraderPage1ControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ImportMetatraderPage1ControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ImportMetatraderPage1ControllerProps.SerializationWrite(this, info, context, false);
		}


		ChildControlMap<String> _IImportMetatraderPage1Controller_ErrorProvider1;
		public ChildControlMap<String> ErrorProvider1
		{
			get {
				return _IImportMetatraderPage1Controller_ErrorProvider1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_ErrorProvider1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_17_ERRORPROVIDER1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridController _IImportMetatraderPage1Controller_DataGridView1;
		public GridController DataGridView1
		{
			get {
				return _IImportMetatraderPage1Controller_DataGridView1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_DataGridView1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_18_DATAGRIDVIEW1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IImportMetatraderPage1Controller_PathColumn;
		public GridColumnController PathColumn
		{
			get {
				return _IImportMetatraderPage1Controller_PathColumn;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_PathColumn= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_19_PATHCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IImportMetatraderPage1Controller_VersionColumn;
		public GridColumnController VersionColumn
		{
			get {
				return _IImportMetatraderPage1Controller_VersionColumn;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_VersionColumn= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_20_VERSIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IImportMetatraderPage1Controller_Button1;
		public ButtonController Button1
		{
			get {
				return _IImportMetatraderPage1Controller_Button1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_Button1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_21_BUTTON1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IImportMetatraderPage1Controller_CheckBox1;
		public ToggleButtonController CheckBox1
		{
			get {
				return _IImportMetatraderPage1Controller_CheckBox1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_CheckBox1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_22_CHECKBOX1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FolderBrowserController _IImportMetatraderPage1Controller_FolderBrowserDialog1;
		public FolderBrowserController FolderBrowserDialog1
		{
			get {
				return _IImportMetatraderPage1Controller_FolderBrowserDialog1;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_FolderBrowserDialog1= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_23_FOLDERBROWSERDIALOG1_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String[] _IImportMetatraderPage1Controller_SelectedImportDirectory;
		public virtual String[] SelectedImportDirectory
		{
			get {
				return _IImportMetatraderPage1Controller_SelectedImportDirectory;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_SelectedImportDirectory= value;
					changed[ImportMetatraderPage1ControllerProps.PROPERTY_24_SELECTEDIMPORTDIRECTORY_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Boolean _IImportMetatraderPage1Controller_StartMetatrader;
		public virtual Boolean StartMetatrader
		{
			get {
				return _IImportMetatraderPage1Controller_StartMetatrader;
			}
			set {
				if (!___initialized) {
					_IImportMetatraderPage1Controller_StartMetatrader= value;
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
			if (ImportMetatraderPage1ControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ImportMetatraderPage1ControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
