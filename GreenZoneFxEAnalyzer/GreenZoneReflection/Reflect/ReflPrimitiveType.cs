using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflPrimitiveType : ReflType
    {
        internal ReflPrimitiveType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
        }

        internal ReflPrimitiveType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.PrimitiveType;
            }
        }
    }
}
