using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;
using System.Reflection;

namespace GreenZoneParser.Reflect
{
    public class ReflConstructor : ReflMethodBase
    {
        internal ReflConstructor(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflModifier modifiers, ReflAttribute[] attributes, params ReflMethodArgDefinition[] methodArgs)
            : base(resolver, parser, parseNode, id, declaringType, declaringType.Name, modifiers, attributes, methodArgs)
        {
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.Constructor;
            }
        }

        ConstructorInfo nativeConstructor;
        public ConstructorInfo NativeConstructor
        {
            get
            {
                if (nativeConstructor == null)
                {
                    Type[] args;
                    if (methodArgs == null)
                    {
                        args = new Type[0];
                    }
                    else
                    {
                        args = new Type[methodArgs.Length];
                        for (int i = 0; i < methodArgs.Length; i++)
                        {
                            args[i] = methodArgs[i].ParameterType.NativeType;
                        }
                    }
                    nativeConstructor = Resolver.PureReflResolver.GetNativeConstructor(declaringType.NativeType, args);
                }
                return nativeConstructor;
            }
        }

        public object CreateObject(object[] args)
        {
            return Resolver.PureReflResolver.CreateObject(NativeConstructor, args);
        }
    }
}
