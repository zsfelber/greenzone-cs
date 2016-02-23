using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ExecSessionProps
	{
		public const int PROPERTY_1_ENVIRONMENT_ID = 1;
		public const int PROPERTY_2_ENVIRONMENTID_ID = 2;
		public const int PROPERTY_3_EXECUTABLEINFO_ID = 3;
		public const int PROPERTY_4_PARAMETERS_ID = 4;
		public static bool RmiGetProperty(IExecSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_1_ENVIRONMENT_ID:
					value = controller.Environment;
					return true;
				case PROPERTY_2_ENVIRONMENTID_ID:
					value = controller.EnvironmentId;
					return true;
				case PROPERTY_3_EXECUTABLEINFO_ID:
					value = controller.ExecutableInfo;
					return true;
				case PROPERTY_4_PARAMETERS_ID:
					value = controller.Parameters;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IExecSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_3_EXECUTABLEINFO_ID:
					controller.ExecutableInfo = (Mt4ExecutableInfo) value;
					return true;
				case PROPERTY_4_PARAMETERS_ID:
					controller.Parameters = (Dictionary<String,Object>) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IExecSession controller, GreenRmiObjectBuffer buffer)
		{
			controller.Environment = (IEnvironmentRuntime) buffer.ChangedProps[ExecSessionProps.PROPERTY_1_ENVIRONMENT_ID];
			controller.EnvironmentId = (String) buffer.ChangedProps[ExecSessionProps.PROPERTY_2_ENVIRONMENTID_ID];
		}

		public static void AddDependencies(IExecSession controller)
		{
			controller.Dependencies.Add(controller.Environment);
		}

		public static void SerializationRead(IExecSession controller, SerializationInfo info, StreamingContext context)
		{
			controller.EnvironmentId = (String) info.GetValue("EnvironmentId", typeof(String));
			controller.ExecutableInfo = (Mt4ExecutableInfo) info.GetValue("ExecutableInfo", typeof(Mt4ExecutableInfo));
			controller.Parameters = (Dictionary<String,Object>) info.GetValue("Parameters", typeof(Dictionary<String,Object>));
		}

		public static void SerializationWrite(IExecSession controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("EnvironmentId", controller.EnvironmentId);
			info.AddValue("ExecutableInfo", controller.ExecutableInfo);
			info.AddValue("Parameters", controller.Parameters);
		}

	}
	public abstract class ExecSessionBase : RmiBase, IExecSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ExecutableInfoChanged;
		public event PropertyChangedEventHandler ParametersChanged;

		public ExecSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ExecSessionProps.AddDependencies(this);
		}

		public ExecSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ExecSessionProps.Initialize(this, buffer);
			___initialized = true;
			ExecSessionProps.AddDependencies(this);
		}

		protected ExecSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExecSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			ExecSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ExecSessionProps.SerializationWrite(this, info, context);
		}

		public abstract void SetParameters(IExecRuntime obj, PropertyInfo[] fields);

		IEnvironmentRuntime environment;
		public IEnvironmentRuntime Environment
		{
			get {
				return environment;
			}
			set {
				if (!___initialized) {
					environment= value;
					changed[ExecSessionProps.PROPERTY_1_ENVIRONMENT_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		String environmentId;
		public String EnvironmentId
		{
			get {
				return environmentId;
			}
			set {
				if (!___initialized) {
					environmentId= value;
					changed[ExecSessionProps.PROPERTY_2_ENVIRONMENTID_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Mt4ExecutableInfo executableInfo;
		public Mt4ExecutableInfo ExecutableInfo
		{
			get {
				return executableInfo;
			}
			set {
				if (executableInfo != value) {
					executableInfo= value;
					changed[ExecSessionProps.PROPERTY_3_EXECUTABLEINFO_ID] = true;
					if (ExecutableInfoChanged != null)
						ExecutableInfoChanged(this, new PropertyChangedEventArgs("ExecutableInfo", value));
				}
			}
		}

		Dictionary<String,Object> parameters;
		public Dictionary<String,Object> Parameters
		{
			get {
				return parameters;
			}
			set {
				if (parameters != value) {
					parameters= value;
					changed[ExecSessionProps.PROPERTY_4_PARAMETERS_ID] = true;
					if (ParametersChanged != null)
						ParametersChanged(this, new PropertyChangedEventArgs("Parameters", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ExecSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!ExecSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
