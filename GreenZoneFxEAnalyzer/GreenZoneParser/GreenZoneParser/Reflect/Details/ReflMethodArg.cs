using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflMethodArg : ReflBaseNode
    {
        internal ReflMethodArg(Resolver resolver, Parser parser, BaseNode parseNode, ReflMethodArgDefinition definition, object actualValue)
            : base(resolver, parser, parseNode, null)
        {
            this.definition = definition;
            this.actualValue = actualValue;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.MethodArg;
            }
        }

        readonly ReflMethodArgDefinition definition;
        public ReflMethodArgDefinition Definition
        {
            get
            {
                return definition;
            }
        }

        public string MethodParameterName
        {
            get
            {
                return definition.MethodParameterName;
            }
        }

        readonly object actualValue;
        public object ActualValue
        {
            get
            {
                return actualValue;
            }
        }
    }
}
