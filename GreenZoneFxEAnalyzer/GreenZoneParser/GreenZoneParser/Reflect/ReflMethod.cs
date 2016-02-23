using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;
using System.Reflection;

namespace GreenZoneParser.Reflect
{
    public class ReflMethod : ReflMethodBaseGen
    {
        internal ReflMethod(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflObjType declaringType, ReflType returnType, string methodName, ReflModifier modifiers, ReflAttribute[] attributes, params ReflMethodArgDefinition[] methodArgs)
            : base(resolver, parser, parseNode, id, declaringType, methodName, modifiers, attributes, methodArgs)
        {
            this.returnType = returnType;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.Method;
            }
        }

        readonly ReflType returnType;
        public ReflType ReturnType
        {
            get
            {
                return returnType;
            }
        }

        public new ReflObjType DeclaringType
        {
            get
            {
                return (ReflObjType)base.DeclaringType;
            }
        }

        internal ReflMethod ApplyGenericArgs(ReflTypeArg[] genericTypeArgs)
        {
            ReflMethod result = new ReflMethod(Resolver, parser, ParseNode, id, DeclaringType, returnType, name, Modifiers, attributes.ToArray(), methodArgs);
            result.genericTypeArgs.AddRange(genericTypeArgs);
            //NOTE unused
            result.LazyResolver = LazyResolver;

            return result;
        }

        MethodInfo nativeMethod;
        public MethodInfo NativeMethod
        {
            get
            {
                if (nativeMethod == null)
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
                    nativeMethod = Resolver.PureReflResolver.GetNativeMethod(declaringType.NativeType, Name, args);
                }
                return nativeMethod;
            }
        }

        public object Invoke(object This, object[] args)
        {
            return Resolver.PureReflResolver.Invoke(NativeMethod, This, args);
        }
    }
}
