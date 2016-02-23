using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflDelegateType : ReflCallableType
    {
        internal ReflDelegateType(Resolver resolver, Parser parser, BaseNode parseNode, int id, string namespaceId, string name, ReflMethod invokeMethod, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, name, modifiers, invokeMethod.genericTypeArgDefs)
        {
            this.invokeMethod = invokeMethod;
        }

        internal ReflDelegateType(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflDefType parentType, string name, ReflMethod invokeMethod, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, name, modifiers, invokeMethod.genericTypeArgDefs)
        {
            this.invokeMethod = invokeMethod;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.DelegateType;
            }
        }

        protected readonly ReflMethod invokeMethod;
        public ReflMethod InvokeMethod
        {
            get
            {
                return invokeMethod;
            }
        }

        internal override ReflCallableType ApplyGenericArgs(ReflDefType parentType, ReflTypeArg[] genericTypeArgs)
        {
            ReflDelegateType result;
            ReflMethod newInvokeMethod = invokeMethod.ApplyGenericArgs(genericTypeArgs);
            if (parentType == null)
            {
                result = new ReflDelegateType(Resolver, parser, ParseNode, id, namespaceId, Name, newInvokeMethod, Modifiers);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            else
            {
                result = new ReflDelegateType(Resolver, parser, ParseNode, id, parentType, Name, newInvokeMethod, Modifiers);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            return result;
        }
    }
}
