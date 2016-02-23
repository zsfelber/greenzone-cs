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
		public static bool RmiGetProperty(IScriptRunnerController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IScriptRunnerController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IScriptRunnerController controller, GreenRmiObjectBuffer buffer)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[ScriptRunnerControllerProps.PROPERTY_16_MAINWINDOW_ID];
		}

		public static void AddDependencies(IScriptRunnerController controller)
		{
			controller.Dependencies.Add(controller.MainWindow);
		}

		public static void SerializationRead(IScriptRunnerController controller, SerializationInfo info, StreamingContext context)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
		}

		public static void SerializationWrite(IScriptRunnerController controller, SerializationInfo info, StreamingContext context)
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
			ScriptRunnerControllerProps.AddDependencies(this);
		}

		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this);
		}

		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this);
		}

		public ScriptRunnerControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ScriptRunnerControllerProps.Initialize(this, buffer);
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this);
		}

		protected ScriptRunnerControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ScriptRunnerControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			ScriptRunnerControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ScriptRunnerControllerProps.SerializationWrite(this, info, context);
		}

		IMainWindowController mainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return mainWindow;
			}
			set {
				if (!___initialized) {
					mainWindow= value;
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
			if (ScriptRunnerControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ScriptRunnerControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
