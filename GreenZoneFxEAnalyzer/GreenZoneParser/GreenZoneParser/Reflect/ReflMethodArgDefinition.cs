using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public enum ReflMethodArgType
    {
        Value,
        Ref,
        Out,
        This,
        Params
    }

    public class ReflMethodArgDefinition : ReflBaseNode
    {
        internal ReflMethodArgDefinition(Resolver resolver, Parser parser, BaseNode parseNode, ReflType parameterType, string methodParameterName, ReflMethodArgType argType, params ReflAttribute[] attributes)
            : base(resolver, parser, parseNode, null)
        {
            this.parameterType = parameterType;
            this.methodParameterName = methodParameterName;
            this.argType = argType;
            this.attributes = attributes;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.MethodArgDefinition;
            }
        }

        readonly string methodParameterName;
        public string MethodParameterName
        {
            get
            {
                return methodParameterName;
            }
        }

        readonly ReflType parameterType;
        public ReflType ParameterType
        {
            get
            {
                return parameterType;
            }
        }

        readonly ReflMethodArgType argType;
        public ReflMethodArgType ArgType
        {
            get
            {
                return argType;
            }
        }

        readonly ReflAttribute[] attributes;
        public ReflAttribute[] Attributes
        {
            get
            {
                return attributes;
            }
        }
    }
}
