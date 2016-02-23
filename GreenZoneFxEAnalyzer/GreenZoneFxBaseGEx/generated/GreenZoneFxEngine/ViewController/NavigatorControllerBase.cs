using GreenZoneFxEngine.ViewController;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController
{
	public static class NavigatorControllerProps
	{
		public const int PROPERTY_14_MAINWINDOW_ID = 14;
		public static bool RmiGetProperty(INavigatorController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case NavigatorControllerProps.PROPERTY_14_MAINWINDOW_ID:
					value = controller.MainWindow;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(INavigatorController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(INavigatorController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) buffer.ChangedProps[NavigatorControllerProps.PROPERTY_14_MAINWINDOW_ID];
		}

		public static void AddDependencies(INavigatorController controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.MainWindow);
		}

		public static void SerializationRead(INavigatorController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.MainWindow = (IMainWindowController) info.GetValue("MainWindow", typeof(IMainWindowController));
		}

		public static void SerializationWrite(INavigatorController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("MainWindow", controller.MainWindow);
		}

	}
	public abstract class NavigatorControllerBase : Controller, INavigatorController
	{

		bool ___initialized = false;


		public NavigatorControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			NavigatorControllerProps.AddDependencies(this, false);
		}

		public NavigatorControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			NavigatorControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			NavigatorControllerProps.AddDependencies(this, false);
		}

		protected NavigatorControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			NavigatorControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			NavigatorControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			NavigatorControllerProps.SerializationWrite(this, info, context, false);
		}


		IMainWindowController _INavigatorController_MainWindow;
		public IMainWindowController MainWindow
		{
			get {
				return _INavigatorController_MainWindow;
			}
			set {
				if (!___initialized) {
					_INavigatorController_MainWindow= value;
					changed[NavigatorControllerProps.PROPERTY_14_MAINWINDOW_ID] = true;
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
			if (NavigatorControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (NavigatorControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
