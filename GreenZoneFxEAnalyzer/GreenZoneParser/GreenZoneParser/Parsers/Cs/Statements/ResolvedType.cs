using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneParser.Parsers.Cs
{
    // TODO
    public class ResolvedType
    {
        internal static ResolvedType Create(TypeSpecifierNode typeSpec)
        {
            ResolvedType result;
            if (typeSpec is SystemTypeSpecifierNode)
            {
                result = new SystemResolvedType((SystemTypeSpecifierNode)typeSpec);
            }
            else
            {
                result = new CompiledResolvedType((UserTypeSpecifierNode)typeSpec);
            }
            return result;
        }
    }

    public class SystemResolvedType : ResolvedType
    {
        public static readonly SystemResolvedType BOOLEAN = new SystemResolvedType(typeof(bool));
        public static readonly SystemResolvedType OBJECT = new SystemResolvedType(typeof(object));

        internal SystemResolvedType(Type systemType)
        {
            this.systemType = systemType;
        }

        internal SystemResolvedType(SystemTypeSpecifierNode systemSpecifier)
        {
            this.systemSpecifier = systemSpecifier;
        }

        readonly Type systemType;
        public Type SystemType
        {
            get
            {
                return systemType;
            }
        }

        readonly SystemTypeSpecifierNode systemSpecifier;
        public SystemTypeSpecifierNode SystemSpecifier
        {
            get
            {
                return systemSpecifier;
            }
        }
    }

    public class CompiledResolvedType : ResolvedType
    {
        internal CompiledResolvedType(UserTypeSpecifierNode compiledType)
        {
            this.compiledType = compiledType;
        }

        readonly UserTypeSpecifierNode compiledType;
        public UserTypeSpecifierNode CompiledType
        {
            get
            {
                return compiledType;
            }
        }
    }
}
