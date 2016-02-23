using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class ScriptRunnerControllerProps
	{
		public const int PROPERTY_16_MAINWINDOW_ID = 16;
		public static bool RmiGetProperty(IScriptRunnerController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ScriptRunnerControllerProps.PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptRunnerController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptRunnerController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[ScriptRunnerControllerProps.PROPERTY_16_MAINWINDOW_ID];
		}

		public static void AddDependencies(IScriptRunnerController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.MainWindow);
		}

		public static void SerializationRead(IScriptRunnerController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
		}

		public static void SerializationWrite(IScriptRunnerController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("MainWindow", controller.MainWindow);
		}

	}
	public abstract class ScriptRunnerControllerBase : TabPageController, IScriptRunnerController
	{

		bool ___initialized = false;


		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this, false);
		}

		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this, false);
		}

		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this, false);
		}

		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptRunnerControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this, false);
		}

		protected ScriptRunnerControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptRunnerControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptRunnerControllerProps.SerializationWrite(this, info, context, false);
		}


		IMainWindowController _IScriptRunnerController_MainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return _IScriptRunnerController_MainWindow;
			}
			set {
				if (!___initialized) {
					_IScriptRunnerController_MainWindow= value;
					changed[ScriptRunnerControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
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
			if (ScriptRunnerControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ScriptRunnerControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
