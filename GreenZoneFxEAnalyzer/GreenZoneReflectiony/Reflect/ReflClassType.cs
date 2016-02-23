using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflClassType : ReflDefType
    {
        internal ReflClassType(Resolver resolver, IParser parser, INode parseNode, int id, string namespaceId, string typeName, ReflClassType baseClass, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            this.baseClass = baseClass;
        }

        internal ReflClassType(Resolver resolver, IParser parser, INode parseNode, int id, ReflDefType parentType, string typeName, ReflClassType baseClass, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            this.baseClass = baseClass;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.ClassType;
            }
        }

        readonly ReflClassType baseClass;
        public ReflClassType BaseClass
        {
            get
            {
                return baseClass;
            }
        }

        internal override ReflCallableType ApplyGenericArgs(ReflDefType parentType, ReflTypeArg[] genericTypeArgs)
        {
            ReflClassType result;
            if (parentType == null)
            {
                result = new ReflClassType(Resolver, parser, ParseNode, id, namespaceId, name, baseClass, Modifiers);
                result.baseInterfaces.AddRange(baseInterfaces);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            else
            {
                result = new ReflClassType(Resolver, parser, ParseNode, id, parentType, name, baseClass, Modifiers);
                result.baseInterfaces.AddRange(baseInterfaces);
                result.genericTypeArgs.AddRange(genericTypeArgs);
                result.attributes.AddRange(attributes);
                result.LazyResolver = LazyResolver;
            }
            return result;
        }

        protected internal override bool isSubtypeOf(ReflType fromType)
        {
            if (base.isSubtypeOf(fromType))
            {
                return true;
            }
            else if (baseClass != null && baseClass.isSubtypeOf(fromType))
            {
                return true;
            }
            return false;
        }
    }
}
