using GreenZoneFxEngine.ViewController.Assistant;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class EnvironmentAssistantControllerProps
	{
		public const int PROPERTY_27_UPDATEDENVIRONMENT_ID = 27;
		public const int PROPERTY_28_UPDATEDENVIRONMENTDIR_ID = 28;
		public const int PROPERTY_29_UPDATEDENVIRONMENTTYPE_ID = 29;
		public const int PROPERTY_30_UPDATEDENVIRONMENTHISTORYDIR_ID = 30;
		public const int PROPERTY_31_ENVIRONMENTS_ID = 31;
		public const int PROPERTY_32_UPDATEDENVIRONMENTDATA_ID = 32;
		public const int PROPERTY_33_STARTPAGECONTROLLER_ID = 33;
		public const int PROPERTY_34_SELECTENVTYPEPAGECONTROLLER_ID = 34;
		public const int PROPERTY_35_IMPORTMETATRADERPAGE1CONTROLLER_ID = 35;
		public const int PROPERTY_36_IMPORTMETATRADERPAGE2CONTROLLER_ID = 36;
		public const int PROPERTY_37_DUKASCOPYPAGE0CONTROLLER_ID = 37;
		public const int PROPERTY_38_DUKASCOPYPAGE1CONTROLLER_ID = 38;
		public const int PROPERTY_39_DUKASCOPYPAGE2CONTROLLER_ID = 39;
		public const int PROPERTY_40_DUKASCOPYPAGE3CONTROLLER_ID = 40;
		public static bool RmiGetProperty(IEnvironmentAssistantController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentAssistantControllerProps.PROPERTY_27_UPDATEDENVIRONMENT_ID:
					value = controller.UpdatedEnvironment;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_28_UPDATEDENVIRONMENTDIR_ID:
					value = controller.UpdatedEnvironmentDir;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_29_UPDATEDENVIRONMENTTYPE_ID:
					value = controller.UpdatedEnvironmentType;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_30_UPDATEDENVIRONMENTHISTORYDIR_ID:
					value = controller.UpdatedEnvironmentHistoryDir;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_31_ENVIRONMENTS_ID:
					value = controller.Environments;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_32_UPDATEDENVIRONMENTDATA_ID:
					value = controller.UpdatedEnvironmentData;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_33_STARTPAGECONTROLLER_ID:
					value = controller.StartPageController;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_34_SELECTENVTYPEPAGECONTROLLER_ID:
					value = controller.SelectEnvTypePageController;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_35_IMPORTMETATRADERPAGE1CONTROLLER_ID:
					value = controller.ImportMetatraderPage1Controller;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_36_IMPORTMETATRADERPAGE2CONTROLLER_ID:
					value = controller.ImportMetatraderPage2Controller;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_37_DUKASCOPYPAGE0CONTROLLER_ID:
					value = controller.DukascopyPage0Controller;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_38_DUKASCOPYPAGE1CONTROLLER_ID:
					value = controller.DukascopyPage1Controller;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_39_DUKASCOPYPAGE2CONTROLLER_ID:
					value = controller.DukascopyPage2Controller;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_40_DUKASCOPYPAGE3CONTROLLER_ID:
					value = controller.DukascopyPage3Controller;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEnvironmentAssistantController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EnvironmentAssistantControllerProps.PROPERTY_27_UPDATEDENVIRONMENT_ID:
					controller.UpdatedEnvironment = (String) value;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_28_UPDATEDENVIRONMENTDIR_ID:
					controller.UpdatedEnvironmentDir = (String) value;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_29_UPDATEDENVIRONMENTTYPE_ID:
					controller.UpdatedEnvironmentType = (String) value;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_30_UPDATEDENVIRONMENTHISTORYDIR_ID:
					controller.UpdatedEnvironmentHistoryDir = (String) value;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_31_ENVIRONMENTS_ID:
					controller.Environments = (ISet<String>) value;
					return true;
				case EnvironmentAssistantControllerProps.PROPERTY_32_UPDATEDENVIRONMENTDATA_ID:
					controller.UpdatedEnvironmentData = (String[]) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEnvironmentAssistantController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.StartPageController = (IStartPageController) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_33_STARTPAGECONTROLLER_ID];
			controller.SelectEnvTypePageController = (ISelectEnvTypePageController) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_34_SELECTENVTYPEPAGECONTROLLER_ID];
			controller.ImportMetatraderPage1Controller = (IImportMetatraderPage1Controller) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_35_IMPORTMETATRADERPAGE1CONTROLLER_ID];
			controller.ImportMetatraderPage2Controller = (IImportMetatraderPage2Controller) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_36_IMPORTMETATRADERPAGE2CONTROLLER_ID];
			controller.DukascopyPage0Controller = (IDukascopyPage0Controller) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_37_DUKASCOPYPAGE0CONTROLLER_ID];
			controller.DukascopyPage1Controller = (IDukascopyPage1Controller) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_38_DUKASCOPYPAGE1CONTROLLER_ID];
			controller.DukascopyPage2Controller = (IDukascopyPage2Controller) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_39_DUKASCOPYPAGE2CONTROLLER_ID];
			controller.DukascopyPage3Controller = (IDukascopyPage3Controller) buffer.ChangedProps[EnvironmentAssistantControllerProps.PROPERTY_40_DUKASCOPYPAGE3CONTROLLER_ID];
		}

		public static void AddDependencies(IEnvironmentAssistantController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.StartPageController);
			controller.Dependencies.Add(controller.SelectEnvTypePageController);
			controller.Dependencies.Add(controller.ImportMetatraderPage1Controller);
			controller.Dependencies.Add(controller.ImportMetatraderPage2Controller);
			controller.Dependencies.Add(controller.DukascopyPage0Controller);
			controller.Dependencies.Add(controller.DukascopyPage1Controller);
			controller.Dependencies.Add(controller.DukascopyPage2Controller);
			controller.Dependencies.Add(controller.DukascopyPage3Controller);
		}

		public static void SerializationRead(IEnvironmentAssistantController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.UpdatedEnvironment = (String) info.GetValue("UpdatedEnvironment", typeof(String));
			controller.UpdatedEnvironmentDir = (String) info.GetValue("UpdatedEnvironmentDir", typeof(String));
			controller.UpdatedEnvironmentType = (String) info.GetValue("UpdatedEnvironmentType", typeof(String));
			controller.UpdatedEnvironmentHistoryDir = (String) info.GetValue("UpdatedEnvironmentHistoryDir", typeof(String));
			controller.Environments = (ISet<String>) info.GetValue("Environments", typeof(ISet<String>));
			controller.UpdatedEnvironmentData = (String[]) info.GetValue("UpdatedEnvironmentData", typeof(String[]));
			controller.StartPageController = (IStartPageController) info.GetValue("StartPageController", typeof(IStartPageController));
			controller.SelectEnvTypePageController = (ISelectEnvTypePageController) info.GetValue("SelectEnvTypePageController", typeof(ISelectEnvTypePageController));
			controller.ImportMetatraderPage1Controller = (IImportMetatraderPage1Controller) info.GetValue("ImportMetatraderPage1Controller", typeof(IImportMetatraderPage1Controller));
			controller.ImportMetatraderPage2Controller = (IImportMetatraderPage2Controller) info.GetValue("ImportMetatraderPage2Controller", typeof(IImportMetatraderPage2Controller));
			controller.DukascopyPage0Controller = (IDukascopyPage0Controller) info.GetValue("DukascopyPage0Controller", typeof(IDukascopyPage0Controller));
			controller.DukascopyPage1Controller = (IDukascopyPage1Controller) info.GetValue("DukascopyPage1Controller", typeof(IDukascopyPage1Controller));
			controller.DukascopyPage2Controller = (IDukascopyPage2Controller) info.GetValue("DukascopyPage2Controller", typeof(IDukascopyPage2Controller));
			controller.DukascopyPage3Controller = (IDukascopyPage3Controller) info.GetValue("DukascopyPage3Controller", typeof(IDukascopyPage3Controller));
		}

		public static void SerializationWrite(IEnvironmentAssistantController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("UpdatedEnvironment", controller.UpdatedEnvironment);
			info.AddValue("UpdatedEnvironmentDir", controller.UpdatedEnvironmentDir);
			info.AddValue("UpdatedEnvironmentType", controller.UpdatedEnvironmentType);
			info.AddValue("UpdatedEnvironmentHistoryDir", controller.UpdatedEnvironmentHistoryDir);
			info.AddValue("Environments", controller.Environments);
			info.AddValue("UpdatedEnvironmentData", controller.UpdatedEnvironmentData);
			info.AddValue("StartPageController", controller.StartPageController);
			info.AddValue("SelectEnvTypePageController", controller.SelectEnvTypePageController);
			info.AddValue("ImportMetatraderPage1Controller", controller.ImportMetatraderPage1Controller);
			info.AddValue("ImportMetatraderPage2Controller", controller.ImportMetatraderPage2Controller);
			info.AddValue("DukascopyPage0Controller", controller.DukascopyPage0Controller);
			info.AddValue("DukascopyPage1Controller", controller.DukascopyPage1Controller);
			info.AddValue("DukascopyPage2Controller", controller.DukascopyPage2Controller);
			info.AddValue("DukascopyPage3Controller", controller.DukascopyPage3Controller);
		}

	}
	public abstract class EnvironmentAssistantControllerBase : AssistantFormController, IEnvironmentAssistantController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IEnvironmentAssistantController_UpdatedEnvironment_Changed;
		public event PropertyChangedEventHandler IEnvironmentAssistantController_UpdatedEnvironmentDir_Changed;
		public event PropertyChangedEventHandler IEnvironmentAssistantController_UpdatedEnvironmentType_Changed;
		public event PropertyChangedEventHandler IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir_Changed;
		public event PropertyChangedEventHandler IEnvironmentAssistantController_Environments_Changed;
		public event PropertyChangedEventHandler IEnvironmentAssistantController_UpdatedEnvironmentData_Changed;

		public EnvironmentAssistantControllerBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EnvironmentAssistantControllerProps.AddDependencies(this, false);
		}

		public EnvironmentAssistantControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EnvironmentAssistantControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			EnvironmentAssistantControllerProps.AddDependencies(this, false);
		}

		protected EnvironmentAssistantControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EnvironmentAssistantControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EnvironmentAssistantControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EnvironmentAssistantControllerProps.SerializationWrite(this, info, context, false);
		}


		String _IEnvironmentAssistantController_UpdatedEnvironment;
		public String UpdatedEnvironment
		{
			get {
				return _IEnvironmentAssistantController_UpdatedEnvironment;
			}
			set {
				if (_IEnvironmentAssistantController_UpdatedEnvironment != value) {
					_IEnvironmentAssistantController_UpdatedEnvironment= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_27_UPDATEDENVIRONMENT_ID] = true;
					if (IEnvironmentAssistantController_UpdatedEnvironment_Changed != null)
						IEnvironmentAssistantController_UpdatedEnvironment_Changed(this, new PropertyChangedEventArgs("UpdatedEnvironment", value));
				}
			}
		}

		String _IEnvironmentAssistantController_UpdatedEnvironmentDir;
		public String UpdatedEnvironmentDir
		{
			get {
				return _IEnvironmentAssistantController_UpdatedEnvironmentDir;
			}
			set {
				if (_IEnvironmentAssistantController_UpdatedEnvironmentDir != value) {
					_IEnvironmentAssistantController_UpdatedEnvironmentDir= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_28_UPDATEDENVIRONMENTDIR_ID] = true;
					if (IEnvironmentAssistantController_UpdatedEnvironmentDir_Changed != null)
						IEnvironmentAssistantController_UpdatedEnvironmentDir_Changed(this, new PropertyChangedEventArgs("UpdatedEnvironmentDir", value));
				}
			}
		}

		String _IEnvironmentAssistantController_UpdatedEnvironmentType;
		public String UpdatedEnvironmentType
		{
			get {
				return _IEnvironmentAssistantController_UpdatedEnvironmentType;
			}
			set {
				if (_IEnvironmentAssistantController_UpdatedEnvironmentType != value) {
					_IEnvironmentAssistantController_UpdatedEnvironmentType= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_29_UPDATEDENVIRONMENTTYPE_ID] = true;
					if (IEnvironmentAssistantController_UpdatedEnvironmentType_Changed != null)
						IEnvironmentAssistantController_UpdatedEnvironmentType_Changed(this, new PropertyChangedEventArgs("UpdatedEnvironmentType", value));
				}
			}
		}

		String _IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir;
		public String UpdatedEnvironmentHistoryDir
		{
			get {
				return _IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir;
			}
			set {
				if (_IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir != value) {
					_IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_30_UPDATEDENVIRONMENTHISTORYDIR_ID] = true;
					if (IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir_Changed != null)
						IEnvironmentAssistantController_UpdatedEnvironmentHistoryDir_Changed(this, new PropertyChangedEventArgs("UpdatedEnvironmentHistoryDir", value));
				}
			}
		}

		ISet<String> _IEnvironmentAssistantController_Environments;
		public ISet<String> Environments
		{
			get {
				return _IEnvironmentAssistantController_Environments;
			}
			set {
				if (_IEnvironmentAssistantController_Environments != value) {
					_IEnvironmentAssistantController_Environments= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_31_ENVIRONMENTS_ID] = true;
					if (IEnvironmentAssistantController_Environments_Changed != null)
						IEnvironmentAssistantController_Environments_Changed(this, new PropertyChangedEventArgs("Environments", value));
				}
			}
		}

		String[] _IEnvironmentAssistantController_UpdatedEnvironmentData;
		public String[] UpdatedEnvironmentData
		{
			get {
				return _IEnvironmentAssistantController_UpdatedEnvironmentData;
			}
			set {
				if (_IEnvironmentAssistantController_UpdatedEnvironmentData != value) {
					_IEnvironmentAssistantController_UpdatedEnvironmentData= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_32_UPDATEDENVIRONMENTDATA_ID] = true;
					if (IEnvironmentAssistantController_UpdatedEnvironmentData_Changed != null)
						IEnvironmentAssistantController_UpdatedEnvironmentData_Changed(this, new PropertyChangedEventArgs("UpdatedEnvironmentData", value));
				}
			}
		}

		IStartPageController _IEnvironmentAssistantController_StartPageController;
		public IStartPageController StartPageController
		{
			get {
				return _IEnvironmentAssistantController_StartPageController;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_StartPageController= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_33_STARTPAGECONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ISelectEnvTypePageController _IEnvironmentAssistantController_SelectEnvTypePageController;
		public ISelectEnvTypePageController SelectEnvTypePageController
		{
			get {
				return _IEnvironmentAssistantController_SelectEnvTypePageController;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_SelectEnvTypePageController= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_34_SELECTENVTYPEPAGECONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IImportMetatraderPage1Controller _IEnvironmentAssistantController_ImportMetatraderPage1Controller;
		public IImportMetatraderPage1Controller ImportMetatraderPage1Controller
		{
			get {
				return _IEnvironmentAssistantController_ImportMetatraderPage1Controller;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_ImportMetatraderPage1Controller= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_35_IMPORTMETATRADERPAGE1CONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IImportMetatraderPage2Controller _IEnvironmentAssistantController_ImportMetatraderPage2Controller;
		public IImportMetatraderPage2Controller ImportMetatraderPage2Controller
		{
			get {
				return _IEnvironmentAssistantController_ImportMetatraderPage2Controller;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_ImportMetatraderPage2Controller= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_36_IMPORTMETATRADERPAGE2CONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IDukascopyPage0Controller _IEnvironmentAssistantController_DukascopyPage0Controller;
		public IDukascopyPage0Controller DukascopyPage0Controller
		{
			get {
				return _IEnvironmentAssistantController_DukascopyPage0Controller;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_DukascopyPage0Controller= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_37_DUKASCOPYPAGE0CONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IDukascopyPage1Controller _IEnvironmentAssistantController_DukascopyPage1Controller;
		public IDukascopyPage1Controller DukascopyPage1Controller
		{
			get {
				return _IEnvironmentAssistantController_DukascopyPage1Controller;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_DukascopyPage1Controller= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_38_DUKASCOPYPAGE1CONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IDukascopyPage2Controller _IEnvironmentAssistantController_DukascopyPage2Controller;
		public IDukascopyPage2Controller DukascopyPage2Controller
		{
			get {
				return _IEnvironmentAssistantController_DukascopyPage2Controller;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_DukascopyPage2Controller= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_39_DUKASCOPYPAGE2CONTROLLER_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IDukascopyPage3Controller _IEnvironmentAssistantController_DukascopyPage3Controller;
		public IDukascopyPage3Controller DukascopyPage3Controller
		{
			get {
				return _IEnvironmentAssistantController_DukascopyPage3Controller;
			}
			set {
				if (!___initialized) {
					_IEnvironmentAssistantController_DukascopyPage3Controller= value;
					changed[EnvironmentAssistantControllerProps.PROPERTY_40_DUKASCOPYPAGE3CONTROLLER_ID] = true;
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
			if (EnvironmentAssistantControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EnvironmentAssistantControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
