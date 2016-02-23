using GreenZoneFxEngine.Trading;
using GreenZoneParser.Reflect;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExecSessionProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_ENVIRONMENTID_ID = 2;
		public const int PROPERTY_3_EXECUTABLEINFO_ID = 3;
		public const int PROPERTY_4_PARAMETERS_ID = 4;
		public static bool RmiGetProperty(IExecSession controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ExecSessionProps.PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case ExecSessionProps.PROPERTY_2_ENVIRONMENTID_ID:
					value = controller.EnvironmentId;
					return true;
				case ExecSessionProps.PROPERTY_3_EXECUTABLEINFO_ID:
					value = controller.ExecutableInfo;
					return true;
				case ExecSessionProps.PROPERTY_4_PARAMETERS_ID:
					value = controller.Parameters;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExecSession controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ExecSessionProps.PROPERTY_3_EXECUTABLEINFO_ID:
					controller.ExecutableInfo = (Mt4ExecutableInfo) value;
					return true;
				case ExecSessionProps.PROPERTY_4_PARAMETERS_ID:
					controller.Parameters = (Dictionary<String,Object>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IExecSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[ExecSessionProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.EnvironmentId = (String) buffer.ChangedProps[ExecSessionProps.PROPERTY_2_ENVIRONMENTID_ID];
		}

		public static void AddDependencies(IExecSession controller, bool goToParent)
		{
			controller.Dependencies.Add(controller.Environment);
		}

		public static void SerializationRead(IExecSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.EnvironmentId = (String) info.GetValue("EnvironmentId", typeof(String));
			controller.ExecutableInfo = (Mt4ExecutableInfo) info.GetValue("ExecutableInfo", typeof(Mt4ExecutableInfo));
			controller.Parameters = (Dictionary<String,Object>) info.GetValue("Parameters", typeof(Dictionary<String,Object>));
		}

		public static void SerializationWrite(IExecSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("EnvironmentId", controller.EnvironmentId);
			info.AddValue("ExecutableInfo", controller.ExecutableInfo);
			info.AddValue("Parameters", controller.Parameters);
		}

	}
	public abstract class ExecSessionBase : RmiBase, IExecSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IExecSession_ExecutableInfo_Changed;
		public event PropertyChangedEventHandler IExecSession_Parameters_Changed;

		public ExecSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExecSessionProps.AddDependencies(this, false);
		}

		public ExecSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExecSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			ExecSessionProps.AddDependencies(this, false);
		}

		protected ExecSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExecSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ExecSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExecSessionProps.SerializationWrite(this, info, context, false);
		}

		public abstract void SetParameters(IUserRuntime obj, List<ReflProperty> fields);


		IEnvironmentRuntime _IExecSession_Environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return _IExecSession_Environment;
			}
			set {
				if (!___initialized) {
					_IExecSession_Environment= value;
					changed[ExecSessionProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String _IExecSession_EnvironmentId;
		public String EnvironmentId
		{
			get {
				return _IExecSession_EnvironmentId;
			}
			set {
				if (!___initialized) {
					_IExecSession_EnvironmentId= value;
					changed[ExecSessionProps.PROPERTY_2_ENVIRONMENTID_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Mt4ExecutableInfo _IExecSession_ExecutableInfo;
		public Mt4ExecutableInfo ExecutableInfo
		{
			get {
				return _IExecSession_ExecutableInfo;
			}
			set {
				if (_IExecSession_ExecutableInfo != value) {
					_IExecSession_ExecutableInfo= value;
					changed[ExecSessionProps.PROPERTY_3_EXECUTABLEINFO_ID] = true;
					if (IExecSession_ExecutableInfo_Changed != null)
						IExecSession_ExecutableInfo_Changed(this, new PropertyChangedEventArgs("ExecutableInfo", value));
				}
			}
		}

		Dictionary<String,Object> _IExecSession_Parameters;
		public Dictionary<String,Object> Parameters
		{
			get {
				return _IExecSession_Parameters;
			}
			set {
				if (_IExecSession_Parameters != value) {
					_IExecSession_Parameters= value;
					changed[ExecSessionProps.PROPERTY_4_PARAMETERS_ID] = true;
					if (IExecSession_Parameters_Changed != null)
						IExecSession_Parameters_Changed(this, new PropertyChangedEventArgs("Parameters", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExecSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ExecSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
