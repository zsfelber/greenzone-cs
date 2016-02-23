using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public abstract class ReflMethodBase : ReflMember
    {
        internal ReflMethodBase(Resolver resolver, IParser parser, INode parseNode, int id, ReflCallableType declaringType, string methodName, ReflModifier modifiers, ReflAttribute[] attributes, params ReflMethodArgDefinition[] methodArgs)
            : base(resolver, parser, parseNode, id, declaringType, methodName, modifiers, attributes)
        {
            this.methodArgs = methodArgs;
        }

        protected readonly ReflMethodArgDefinition[] methodArgs;
        public ReflMethodArgDefinition[] MethodArgs
        {
            get
            {
                return methodArgs;
            }
        }

        public ReflMethodArgDefinition ArgByName(string name)
        {
            foreach (var arg in methodArgs)
            {
                if (arg.MethodParameterName == name)
                {
                    return arg;
                }
            }
            return null;
        }
    }
}
