using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflEnumConstant : ReflBaseNode
    {
        internal ReflEnumConstant(Resolver resolver, IParser parser, INode parseNode, ReflEnumType declaringType, string name, decimal value)
            : base(resolver, parser, parseNode, declaringType)
        {
            this.declaringType = declaringType;
            this.name = name;
            this.value = value;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.EnumConstant;
            }
        }

        readonly ReflEnumType declaringType;
        public ReflEnumType DeclaringType
        {
            get
            {
                return declaringType;
            }
        }

        readonly string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        readonly decimal value;
        public decimal Value
        {
            get
            {
                return value;
            }
        }
    }
}
