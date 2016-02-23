using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflAttributeArg : ReflBaseNode
    {
        internal ReflAttributeArg(Resolver resolver, Parser parser, BaseNode parseNode, ReflAttribute parent, int argIndex, string value)
            : base(resolver, parser, parseNode, parent)
        {
            this.parent = parent;
            this.argIndex = argIndex;
            this.argProperty = null;
            this.value = value;
            parent.AddArg(this);
        }

        internal ReflAttributeArg(Resolver resolver, Parser parser, BaseNode parseNode, ReflAttribute parent, string argProperty, string value)
            : base(resolver, parser, parseNode, parent)
        {
            this.parent = parent;
            this.argIndex = -1;
            this.argProperty = argProperty;
            this.value = value;
            parent.AddNamedArg(this);
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.AttributeArg;
            }
        }

        readonly new ReflAttribute parent;
        public new ReflAttribute Parent
        {
            get
            {
                return parent;
            }
        }

        readonly int argIndex;
        public int ArgIndex
        {
            get
            {
                return argIndex;
            }
        }

        readonly string argProperty;
        public string ArgProperty
        {
            get
            {
                return argProperty;
            }
        }

        public string Name
        {
            get
            {
                return argProperty == null ? "" + argIndex : argProperty;
            }
        }

        ReflMethodArgDefinition methodArg;
        public ReflMethodArgDefinition MethodArg
        {
            get
            {
                if (this.methodArg == null)
                {
                    if (argProperty == null)
                    {
                        this.methodArg = parent.AttributeConstr.MethodArgs[argIndex];
                    }
                    else
                    {
                        this.methodArg = parent.AttributeConstr.ArgByName(argProperty);
                    }
                }
                return methodArg;
            }
        }

        readonly string value;
        public string Value
        {
            get
            {
                return value;
            }
        }
    }
}