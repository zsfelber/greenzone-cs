using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflStructType : ReflDefType
    {
        internal ReflStructType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflType baseType, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            this.baseType = baseType;
        }

        internal ReflStructType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflType baseType, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            this.baseType = baseType;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.StructType;
            }
        }

        readonly ReflType baseType;
        public ReflType BaseType
        {
            get
            {
                return baseType;
            }
        }

        internal override ReflCallableType ApplyGenericArgs(ReflDefType parentType, ReflTypeArg[] genericTypeArgs)
        {
            ReflStructType result;
            if (parentType == null)
            {
                result = new ReflStructType(Resolver, parser, ParseNode, id, namespaceId, name, baseType, Modifiers);
                result.baseInterfaces.AddRange(baseInterfaces);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            else
            {
                result = new ReflStructType(Resolver, parser, ParseNode, id, parentType, name, baseType, Modifiers);
                result.baseInterfaces.AddRange(baseInterfaces);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            return result;
        }
    }
}
