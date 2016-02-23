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
		public static bool RmiGetProperty(IEAnalyzerOptions controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EAnalyzerOptionsProps.PROPERTY_1_EANALYZERDIRECTORY_ID:
					value = controller.EAnalyzerDirectory;
					return true;
				case EAnalyzerOptionsProps.PROPERTY_2_DEFAULTENVIRONMENT_ID:
					value = controller.DefaultEnvironment;
					return true;
				case EAnalyzerOptionsProps.PROPERTY_3_BUFFERSIZE_ID:
					value = controller.BufferSize;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IEAnalyzerOptions controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case EAnalyzerOptionsProps.PROPERTY_1_EANALYZERDIRECTORY_ID:
					controller.EAnalyzerDirectory = (SelectableDir) value;
					return true;
				case EAnalyzerOptionsProps.PROPERTY_2_DEFAULTENVIRONMENT_ID:
					controller.DefaultEnvironment = (String) value;
					return true;
				case EAnalyzerOptionsProps.PROPERTY_3_BUFFERSIZE_ID:
					controller.BufferSize = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IEAnalyzerOptions controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IEAnalyzerOptions controller, bool goToParent)
		{
		}

		public static void SerializationRead(IEAnalyzerOptions controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.EAnalyzerDirectory = (SelectableDir) info.GetValue("EAnalyzerDirectory", typeof(SelectableDir));
			controller.DefaultEnvironment = (String) info.GetValue("DefaultEnvironment", typeof(String));
			controller.BufferSize = (Int32) info.GetValue("BufferSize", typeof(Int32));
		}

		public static void SerializationWrite(IEAnalyzerOptions controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("EAnalyzerDirectory", controller.EAnalyzerDirectory);
			info.AddValue("DefaultEnvironment", controller.DefaultEnvironment);
			info.AddValue("BufferSize", controller.BufferSize);
		}

	}
	public abstract class EAnalyzerOptionsBase : RmiBase, IEAnalyzerOptions
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IEAnalyzerOptions_EAnalyzerDirectory_Changed;
		public event PropertyChangedEventHandler IEAnalyzerOptions_DefaultEnvironment_Changed;
		public event PropertyChangedEventHandler IEAnalyzerOptions_BufferSize_Changed;

		public EAnalyzerOptionsBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			EAnalyzerOptionsProps.AddDependencies(this, false);
		}

		public EAnalyzerOptionsBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			EAnalyzerOptionsProps.Initialize(this, buffer, false);
			___initialized = true;
			EAnalyzerOptionsProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected EAnalyzerOptionsBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EAnalyzerOptionsProps.SerializationRead(this, info, context, false);
			___initialized = true;
			EAnalyzerOptionsProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			EAnalyzerOptionsProps.SerializationWrite(this, info, context, false);
		}


		SelectableDir _IEAnalyzerOptions_EAnalyzerDirectory;
		public SelectableDir EAnalyzerDirectory
		{
			get {
				return _IEAnalyzerOptions_EAnalyzerDirectory;
			}
			set {
				if (_IEAnalyzerOptions_EAnalyzerDirectory != value) {
					_IEAnalyzerOptions_EAnalyzerDirectory= value;
					changed[EAnalyzerOptionsProps.PROPERTY_1_EANALYZERDIRECTORY_ID] = true;
					if (IEAnalyzerOptions_EAnalyzerDirectory_Changed != null)
						IEAnalyzerOptions_EAnalyzerDirectory_Changed(this, new PropertyChangedEventArgs("EAnalyzerDirectory", value));
				}
			}
		}

		String _IEAnalyzerOptions_DefaultEnvironment;
		public virtual String DefaultEnvironment
		{
			get {
				return _IEAnalyzerOptions_DefaultEnvironment;
			}
			set {
				if (_IEAnalyzerOptions_DefaultEnvironment != value) {
					_IEAnalyzerOptions_DefaultEnvironment= value;
					changed[EAnalyzerOptionsProps.PROPERTY_2_DEFAULTENVIRONMENT_ID] = true;
					if (IEAnalyzerOptions_DefaultEnvironment_Changed != null)
						IEAnalyzerOptions_DefaultEnvironment_Changed(this, new PropertyChangedEventArgs("DefaultEnvironment", value));
				}
			}
		}

		Int32 _IEAnalyzerOptions_BufferSize;
		public Int32 BufferSize
		{
			get {
				return _IEAnalyzerOptions_BufferSize;
			}
			set {
				if (_IEAnalyzerOptions_BufferSize != value) {
					_IEAnalyzerOptions_BufferSize= value;
					changed[EAnalyzerOptionsProps.PROPERTY_3_BUFFERSIZE_ID] = true;
					if (IEAnalyzerOptions_BufferSize_Changed != null)
						IEAnalyzerOptions_BufferSize_Changed(this, new PropertyChangedEventArgs("BufferSize", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (EAnalyzerOptionsProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (EAnalyzerOptionsProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
