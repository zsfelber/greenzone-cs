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
		public static bool RmiGetProperty(IMainWinTabPageController controller, int propertyId, out object value)
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
		public static bool RmiSetProperty(IMainWinTabPageController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IMainWinTabPageController controller, GreenRmiObjectBuffer buffer)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[MainWinTabPageControllerProps.PROPERTY_16_MAINWINDOW_ID];
		}

		public static void AddDependencies(IMainWinTabPageController controller)
		{
			controller.Dependencies.Add(controller.MainWindow);
		}

		public static void SerializationRead(IMainWinTabPageController controller, SerializationInfo info, StreamingContext context)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
		}

		public static void SerializationWrite(IMainWinTabPageController controller, SerializationInfo info, StreamingContext context)
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
			MainWinTabPageControllerProps.AddDependencies(this);
		}

		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Controller content)
			: base(rmiManager, parent, text, content)
		{
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this);
		}

		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, TabController parent, String text, Int32 image, Controller content)
			: base(rmiManager, parent, text, image, content)
		{
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this);
		}

		public MainWinTabPageControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			MainWinTabPageControllerProps.Initialize(this, buffer);
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this);
		}

		protected MainWinTabPageControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			MainWinTabPageControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			MainWinTabPageControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			MainWinTabPageControllerProps.SerializationWrite(this, info, context);
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
					changed[MainWinTabPageControllerProps.PROPERTY_16_MAINWINDOW_ID] = true;
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
			if (MainWinTabPageControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!MainWinTabPageControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
