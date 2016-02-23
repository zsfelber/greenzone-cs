using System;
using System.Collections.Generic;
using GreenZoneFxEngine.Util;
using System.Reflection;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ComponentModel;
using GreenZoneUtil.Util;
using GreenZoneFxEngine.Types;
using System.IO;
using System.Text;
using GreenZoneUtil.GreenRmi;
using GreenZoneParser.Reflect;

namespace GreenZoneFxEngine.Trading
{

    public class ServerScriptSession : ScriptSessionBase
    {

        public ServerScriptSession(GreenRmiManager rmiManager, IEnvironmentRuntime environment, Mt4ExecutableInfo executableInfo)
            : base(rmiManager)
        {
            Environment = environment;
            EnvironmentId = environment.EnvironmentId;
            ExecutableInfo = executableInfo;
        }

        protected ServerScriptSession(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(EnvironmentId);
        }

        public override void SetParameters(IUserRuntime obj, List<ReflProperty> fields)
        {
            Parameters.Clear();
            foreach (ReflProperty pi in fields)
                Parameters[pi.Name] = pi.GetValue(obj);
        }

    }

    public class ServerExpertSession : ExpertSessionBase
    {
        public ServerExpertSession(GreenRmiManager rmiManager, IEnvironmentRuntime environment, Mt4ExecutableInfo executableInfo)
            : base(rmiManager)
        {
            Environment = environment;
            EnvironmentId = environment.EnvironmentId;
            ExecutableInfo = executableInfo;
        }

        protected ServerExpertSession(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(EnvironmentId);
        }

        public override void SetParameters(IUserRuntime obj, List<ReflProperty> fields)
        {
            Parameters.Clear();
            foreach (ReflProperty pi in fields)
                Parameters[pi.Name] = pi.GetValue(obj);
        }
    }

    public class ServerIndicatorSession : IndicatorSessionBase
    {
        public ServerIndicatorSession(GreenRmiManager rmiManager, IEnvironmentRuntime environment, Mt4ExecutableInfo executableInfo)
            : base(rmiManager)
        {
            Environment = environment;
            EnvironmentId = environment.EnvironmentId;
            ExecutableInfo = executableInfo;

            Buffers = new List<Dictionary<string, object>>();
            Levels = new List<Dictionary<string, object>>();
            IndicatorMinimum = Double.MinValue;
            IndicatorMaximum = Double.MaxValue;
            EnableScroll = true;
        }

        protected ServerIndicatorSession(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Environment = EnvironmentRuntimeRoot.Singleton.GetEnvironment(EnvironmentId);
        }


        public override void SetParameters(IUserRuntime obj, List<ReflProperty> fields)
        {
            Parameters.Clear();
            foreach (ReflProperty pi in fields)
                Parameters[pi.Name] = pi.GetValue(obj);
        }

        public override int NumBuffers
        {
            get
            {
                return Buffers.Count;
            }
            set
            {
                for (int i = Buffers.Count; i < value; i++)
                {
                    Buffers.Add(new Dictionary<string, object>());
                }
            }
        }

        public override int NumLevels
        {
            get
            {
                return Levels.Count;
            }
            set
            {
                for (int i = Levels.Count; i < value; i++)
                {
                    Levels.Add(new Dictionary<string, object>());
                }
            }
        }
    }

}
