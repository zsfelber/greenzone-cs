using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers;
using GreenZoneParser.Lexer;

namespace GreenZoneParser.Reflect
{
    public class ReflEnumType : ReflType
    {
        internal ReflEnumType(Resolver resolver, Parser parser, BaseNode parseNode, int id, string namespaceId, string typeName, ReflType baseType, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, namespaceId, typeName, modifiers)
        {
            this.baseType = baseType;
            enumConstants = new List<ReflEnumConstant>();
            enumConstantsUm = enumConstants.AsReadOnly();
        }

        internal ReflEnumType(Resolver resolver, Parser parser, BaseNode parseNode, int id, ReflDefType parentType, string typeName, ReflType baseType, ReflModifier modifiers)
            : base(resolver, parser, parseNode, id, parentType, typeName, modifiers)
        {
            this.baseType = baseType;
            enumConstants = new List<ReflEnumConstant>();
            enumConstantsUm = enumConstants.AsReadOnly();
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.EnumType;
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

        protected readonly List<ReflEnumConstant> enumConstants;
        protected readonly IList<ReflEnumConstant> enumConstantsUm;
        public IList<ReflEnumConstant> EnumConstants
        {
            get
            {
                return enumConstantsUm;
            }
        }

        internal void AddEnumConstant(ReflEnumConstant enumConstant)
        {
            enumConstants.Add(enumConstant);
        }

        protected internal override bool isSubtypeOf(ReflType fromType)
        {
            if (base.isSubtypeOf(fromType))
            {
                return true;
            }
            else if (baseType != null && baseType.isSubtypeOf(fromType))
            {
                return true;
            }
            return false;
        }
    }
}
