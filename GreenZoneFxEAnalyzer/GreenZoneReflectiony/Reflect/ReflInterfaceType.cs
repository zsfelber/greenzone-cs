using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflInterfaceType : ReflObjType
    {
        internal ReflInterfaceType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
        }

        internal ReflInterfaceType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.InterfaceType;
            }
        }

        internal override ReflCallableType ApplyGenericArgs(ReflDefType parentType, ReflTypeArg[] genericTypeArgs)
        {
            ReflInterfaceType result;
            if (parentType == null)
            {
                result = new ReflInterfaceType(Resolver, parser, ParseNode, id, namespaceId, name, Modifiers);
                result.baseInterfaces.AddRange(baseInterfaces);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            else
            {
                result = new ReflInterfaceType(Resolver, parser, ParseNode, id, parentType, name, Modifiers);
                result.baseInterfaces.AddRange(baseInterfaces);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            return result;
        }
    }
}
