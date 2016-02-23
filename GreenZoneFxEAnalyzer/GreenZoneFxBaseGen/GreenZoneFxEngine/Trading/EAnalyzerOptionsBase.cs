using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.Util;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class EAnalyzerOptionsProps
	{
		public const int PROPERTY_1_EANALYZERDIRECTORY_ID = 1;
		public const int PROPERTY_2_DEFAULTENVIRONMENT_ID = 2;
		public const int PROPERTY_3_BUFFERSIZE_ID = 3;
		public static bool RmiGetProperty(IEAnalyzerOptions controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_EANALYZERDIRECTORY_ID:
					value = controller.EAnalyzerDirectory;
					return true;
				case PROPERTY_2_DEFAULTENVIRONMENT_ID:
					value = controller.DefaultEnvironment;
					return true;
				case PROPERTY_3_BUFFERSIZE_ID:
					value = controller.BufferSize;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEAnalyzerOptions controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_EANALYZERDIRECTORY_ID:
					controller.EAnalyzerDirectory = (SelectableDir) value;
					return true;
				case PROPERTY_2_DEFAULTENVIRONMENT_ID:
					controller.DefaultEnvironment = (String) value;
					return true;
				case PROPERTY_3_BUFFERSIZE_ID:
					controller.BufferSize = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEAnalyzerOptions controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IEAnalyzerOptions controller)
		{
		}

		public static void SerializationRead(IEAnalyzerOptions controller, SerializationInfo info, StreamingContext context)
		{
			controller.EAnalyzerDirectory = (SelectableDir) info.GetValue("EAnalyzerDirectory", typeof(SelectableDir));
			controller.DefaultEnvironment = (String) info.GetValue("DefaultEnvironment", typeof(String));
			controller.BufferSize = (Int32) info.GetValue("BufferSize", typeof(Int32));
		}

		public static void SerializationWrite(IEAnalyzerOptions controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EAnalyzerDirectory", controller.EAnalyzerDirectory);
			info.AddValue("DefaultEnvironment", controller.DefaultEnvironment);
			info.AddValue("BufferSize", controller.BufferSize);
		}

	}
	public abstract class EAnalyzerOptionsBase : RmiBase, IEAnalyzerOptions
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler EAnalyzerDirectoryChanged;
		public event PropertyChangedEventHandler DefaultEnvironmentChanged;
		public event PropertyChangedEventHandler BufferSizeChanged;

		public EAnalyzerOptionsBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EAnalyzerOptionsProps.AddDependencies(this);
		}

		public EAnalyzerOptionsBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EAnalyzerOptionsProps.Initialize(this, buffer);
			___initialized = true;
			EAnalyzerOptionsProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected EAnalyzerOptionsBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EAnalyzerOptionsProps.SerializationRead(this, info, context);
			___initialized = true;
			EAnalyzerOptionsProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EAnalyzerOptionsProps.SerializationWrite(this, info, context);
		}

		SelectableDir eAnalyzerDirectory;
		public SelectableDir EAnalyzerDirectory
		{
			get {
				return eAnalyzerDirectory;
			}
			set {
				if (eAnalyzerDirectory != value) {
					eAnalyzerDirectory= value;
					changed[EAnalyzerOptionsProps.PROPERTY_1_EANALYZERDIRECTORY_ID] = true;
					if (EAnalyzerDirectoryChanged != null)
						EAnalyzerDirectoryChanged(this, new PropertyChangedEventArgs("EAnalyzerDirectory", value));
				}
			}
		}

		String defaultEnvironment;
		public virtual String DefaultEnvironment
		{
			get {
				return defaultEnvironment;
			}
			set {
				if (defaultEnvironment != value) {
					defaultEnvironment= value;
					changed[EAnalyzerOptionsProps.PROPERTY_2_DEFAULTENVIRONMENT_ID] = true;
					if (DefaultEnvironmentChanged != null)
						DefaultEnvironmentChanged(this, new PropertyChangedEventArgs("DefaultEnvironment", value));
				}
			}
		}

		Int32 bufferSize;
		public Int32 BufferSize
		{
			get {
				return bufferSize;
			}
			set {
				if (bufferSize != value) {
					bufferSize= value;
					changed[EAnalyzerOptionsProps.PROPERTY_3_BUFFERSIZE_ID] = true;
					if (BufferSizeChanged != null)
						BufferSizeChanged(this, new PropertyChangedEventArgs("BufferSize", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EAnalyzerOptionsProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!EAnalyzerOptionsProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
