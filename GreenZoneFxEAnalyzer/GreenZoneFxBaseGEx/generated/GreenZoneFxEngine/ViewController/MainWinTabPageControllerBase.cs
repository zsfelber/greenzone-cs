using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class MainWinTabPageControllerProps
	{
		public const int PROPERTY_16_MAINWINDOW_ID = 16;
		public static bool RmiGetProperty(IMainWinTabPageController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case MainWinTabPageControllerProps.PROPERTY_16_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IMainWinTabPageController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IMainWinTabPageController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[MainWinTabPageControllerProps.PROPERTY_16_MAINWINDOW_ID];
		}

		public static void AddDependencies(IMainWinTabPageController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.MainWindow);
		}

		public static void SerializationRead(IMainWinTabPageController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
		}

		public static void SerializationWrite(IMainWinTabPageController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("MainWindow", controller.MainWindow);
		}

	}
	public abstract class MainWinTabPageControllerBase : TabPageController, IMainWinTabPageController
	{

		bool ___initialized = false;


		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, TabController parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this, false);
		}

		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this, false);
		}

		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this, false);
		}

		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			MainWinTabPageControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this, false);
		}

		protected MainWinTabPageControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			MainWinTabPageControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			MainWinTabPageControllerProps.SerializationWrite(this, info, context, false);
		}


		IMainWindowController _IMainWinTabPageController_MainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return _IMainWinTabPageController_MainWindow;
			}
			set {
				if (!___initialized) {
					_IMainWinTabPageController_MainWindow= value;
					changed[MainWinTabPageControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		public virtual IEnvironmentRuntime Environment
		{
			get {
				return MainWindow.Environment;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (MainWinTabPageControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (MainWinTabPageControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
