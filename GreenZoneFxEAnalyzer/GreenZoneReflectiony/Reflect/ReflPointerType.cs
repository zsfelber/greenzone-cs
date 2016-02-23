using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GreenZoneParser.Reflect
{
    public class ReflPointerType : ReflType, TypeNameItem
    {
        internal ReflPointerType(Resolver resolver, IParser parser, INode parseNode, ReflType elementType)
            : base(resolver, parser, parseNode, -1, (string)null, "pointer", 0)
        {
            this.elementType = elementType;
        }

        public override ReflTypeId ReflTypeId
        {
            get
            {
                return ReflTypeId.PointerType;
            }
        }

        readonly ReflType elementType;
        public ReflType ElementType
        {
            get
            {
                return elementType;
            }
        }

        bool TypeNameItem.IsPointer
        {
            get { return true; }
        }

        TypeNameItem TypeNameItem.ElementType
        {
            get { return elementType; }
        }
    }
}
